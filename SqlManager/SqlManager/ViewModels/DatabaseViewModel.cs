using System.Collections.Generic;
using SqlManager.Models;

namespace SqlManager.ViewModels
{
    public class DatabaseViewModel
    {
        public IEnumerable<DatabaseModel> DatabaseModels { get; set; }
    }
}