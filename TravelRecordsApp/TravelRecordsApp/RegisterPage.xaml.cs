using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if(passwordEntry.Text == confirmPasswordEntry.Text)
            {
                // we can confirm register
                Users user = new Users
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text
                };
                await App.mobileService.GetTable<Users>().InsertAsync(user);
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }

       
    }
}