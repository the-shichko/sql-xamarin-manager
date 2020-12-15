using System;
using System.Collections.Generic;
using SqlManager.Models;
using System.Data.SqlClient;
using System.Linq;
using SqlManager.ViewModels;

namespace SqlManager.Logic
{
    public class SqlService : ISqlService
    {
        public ResponseResult<bool> CheckConnection()
        {
            try
            {
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();
                    con.Close();
                }

                return new ResponseResult<bool>
                {
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<bool>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public ResponseResult<IEnumerable<DatabaseModel>> GetDatabases()
        {
            try
            {
                var models = new List<DatabaseModel>();
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                models.Add(new DatabaseModel
                                {
                                    Name = dr[0].ToString()
                                });
                            }
                        }
                    }
                }

                return new ResponseResult<IEnumerable<DatabaseModel>>
                {
                    Value = models.OrderBy(x => x.Name),
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<IEnumerable<DatabaseModel>>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public ResponseResult<IEnumerable<DatabaseModel>> GetTables(string databaseName)
        {
            try
            {
                var models = new List<DatabaseModel>();
                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand($"USE {databaseName}" +
                                                    $" SELECT TABLE_SCHEMA, TABLE_NAME from INFORMATION_SCHEMA.TABLES " +
                                                    $"WHERE TABLE_TYPE LIKE 'BASE TABLE'", con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                models.Add(new DatabaseModel
                                {
                                    Name = $"{dr[0]}.{dr[1]}"
                                });
                            }
                        }
                    }
                }

                return new ResponseResult<IEnumerable<DatabaseModel>>
                {
                    Value = models.OrderBy(x => x.Name),
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<IEnumerable<DatabaseModel>>
                {
                    Message = e.Message,
                    IsSuccess = false
                };
            }
        }

        public ResponseResult<object> Execute(string database, string command)
        {
            try
            {
                var result = "";
                var columns = new List<string>();
                var list = new List<List<string>>();

                using (var con = new SqlConnection(Connection.ConnectionString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand($"USE {database}\n" +
                                                    $" {command}", con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            columns = Enumerable.Range(0, dr.VisibleFieldCount).Select(dr.GetName).ToList();
                            for (var i = 0; i < columns.Count; i++)
                                list.Add(new List<string>());

                            while (dr.Read())
                            {
                                for (var i = 0; i < columns.Count; i++)
                                {
                                    list[i].Add(dr[i].ToString());
                                }
                            }
                        }
                    }

                    var maxIndex = new List<int>();
                    maxIndex.AddRange(columns.Select(x => x.Length));

                    var index = 0;
                    for (var i = 0; i < columns.Count; i++)
                    {
                        var newMax = list[i].Max(x => x.Length);
                        maxIndex[i] = newMax > maxIndex[i] ? newMax : maxIndex[i];
                    }
                    // for (var i = 0; i < columns.Count; i++)
                    // {
                    //     if (list[index][i].Length > maxIndex[i])
                    //         maxIndex[i] = list[index][i].Length;
                    // }
                    var index2 = 0;

                    result += new string('-', maxIndex.Sum(x => x + 2)) + "\n";
                    foreach (var column in columns)
                    {
                        result += $"|{column}{new string(' ', maxIndex[index2] - column.Length + 1)}|";
                        index2++;
                    }

                    result += "\n";
                    result += new string('-', maxIndex.Sum(x => x + 2)) + "\n";

                    for (var j = 0; j < list[0].Count; j++)
                    {
                        for (var i = 0; i < columns.Count; i++)
                        {
                            result +=
                                $"|{list[i][j]}{new string(' ', maxIndex[i] - list[i][j].Length + 1)}|";
                        }

                        result += "\n";
                    }
                }

                return new ResponseResult<object>
                {
                    IsSuccess = true,
                    Value = result
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<object>
                {
                    Message = e.Message.Split('\n')[0],
                    IsSuccess = false
                };
            }
        }
    }
}