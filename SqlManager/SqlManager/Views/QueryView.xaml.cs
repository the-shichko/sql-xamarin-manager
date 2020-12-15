using System;
using SqlManager.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryView : ContentPage
    {
        public QueryView(string database)
        {
            InitializeComponent();

            _sqlService = new SqlService();
            _database = database;
            BindingContext = this;
        }

        public string Command { get; set; } = "SELECT * FROM dbo.SaleRepository";
        private readonly ISqlService _sqlService;
        private string _database;

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var result = _sqlService.Execute(_database, Command);

            EditorResult.Text = result.IsSuccess ? result.Value.ToString() : result.Message;
        }
    }
}