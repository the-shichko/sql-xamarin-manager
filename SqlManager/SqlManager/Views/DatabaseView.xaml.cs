using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlManager.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabaseView : ContentPage
    {
        public DatabaseView(string databaseName)
        {
            InitializeComponent();
            _databaseName = databaseName;
            Title = _databaseName;
            
            _sqlService = new SqlService();
        }

        private string _databaseName;
        private ISqlService _sqlService;

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            // tables
            await Navigation.PushAsync(new TablesView(_databaseName));
        }

        private void Button1_OnClicked(object sender, EventArgs e)
        {
            //views
        }

        private async void Button2_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QueryView(_databaseName));
        }
    }
}