using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlManager.Logic;
using SqlManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablesView : ContentPage
    {
        public TablesView(string databaseName)
        {
            InitializeComponent();
            
            Title = databaseName;
            _sqlService = new SqlService();
            var result = _sqlService.GetTables(databaseName);

            if (result.IsSuccess)
                DatabaseViewModel = new ObservableCollection<DatabaseModel>(result.Value);

            BindingContext = this;
        }

        private readonly ISqlService _sqlService;
        public ObservableCollection<DatabaseModel> DatabaseViewModel { get; set; }
    }
}