using SQLite;
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
	public partial class PostDetailPage : ContentPage
	{
        Post selectedPost;

        public PostDetailPage()
        {
            InitializeComponent();
            
        }

        public PostDetailPage (Post selectedPost)
		{
			InitializeComponent ();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
		}

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // this is the same as the above code
                conn.CreateTable<Post>();
                int rows = conn.Update(selectedPost);
                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience Successfully Updated", "OK");
                    //Navigation.PushAsync(new HomePage());
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Failure", "Experience Failed to be Updated", "OK");
                }
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // this is the same as the above code
                conn.CreateTable<Post>();
                int rows = conn.Delete(selectedPost);
                if (rows > 0)
                {
                    DisplayAlert("Success", "Experience Successfully Deleted", "OK");
                    //Navigation.PushAsync(new HomePage());
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Failure", "Experience Failed to be Deleted", "OK");
                }
            }
        }
    }
}