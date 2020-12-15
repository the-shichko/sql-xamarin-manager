using System;
using System.Collections.Generic;
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
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();

            _sqlService = new SqlService();
            AuthorizationModel = new AuthorizationModel
            {
                Ip = "10.81.80.6",
                Login = "sa",
                Password = "Qwerty1",
                Port = "1433"
            };
            BindingContext = AuthorizationModel;
        }

        private readonly ISqlService _sqlService;
        public AuthorizationModel AuthorizationModel { get; set; }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            Connection.AuthorizationModel = AuthorizationModel;

            var result = _sqlService.CheckConnection();

            if (result.IsSuccess)
                await Navigation.PushAsync(new DatabasesView());
            else
                await DisplayAlert("Error", result.Message, "Close");
        }
    }
}