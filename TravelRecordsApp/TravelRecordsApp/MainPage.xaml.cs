using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecordsApp.Assets.Images.plane.png", assembly);
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            bool IsEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool IsPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            if(IsEmailEmpty || IsPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }

        private void RegisterUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
