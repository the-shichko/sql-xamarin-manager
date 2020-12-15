using System.Collections.Generic;
using SqlManager.Models;
using SqlManager.ViewModels;

namespace SqlManager.Logic
{
    public interface ISqlService
    {
        ResponseResult<bool> CheckConnection();
        ResponseResult<IEnumerable<DatabaseModel>> GetDatabases();
        ResponseResult<IEnumerable<DatabaseModel>> GetTables(string databaseName);
        ResponseResult<object> Execute(string database, string command);
    }
}