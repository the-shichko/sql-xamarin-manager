using System.Collections.ObjectModel;
using SqlManager.Logic;
using SqlManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqlManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabasesView : ContentPage
    {
        private readonly ISqlService _sqlService;

        public DatabasesView()
        {
            InitializeComponent();
            Title = $"{Connection.AuthorizationModel.Ip},{Connection.AuthorizationModel.Port}";

            _sqlService = new SqlService();
            var result = _sqlService.GetDatabases();

            if (result.IsSuccess)
                DatabaseViewModel = new ObservableCollection<DatabaseModel>(result.Value);

            BindingContext = this;
        }

        public ObservableCollection<DatabaseModel> DatabaseViewModel { get; set; }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView) sender).SelectedItem == null) return;

            await Navigation.PushAsync(new DatabaseView(((DatabaseModel) e.SelectedItem).Name));
            ((ListView) sender).SelectedItem = null;
        }
    }
}