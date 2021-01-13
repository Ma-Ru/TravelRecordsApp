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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();
                //Query using Linq
                var categories = (from p in postTable
                                  orderby p.Catagoryid
                                  select p.Catagoryname).Distinct().ToList();

                // Method that does the same as the above Query, both using Linq
                var categories2 = postTable.OrderBy(p => p.Catagoryid).Select(p => p.Catagoryname).Distinct().ToList();

                Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
                foreach (var category in categories)
                {
                    // Query using Linq
                    var count = (from post in postTable
                                 where post.Catagoryname == category
                                 select post).ToList().Count;

                    // Method that does the same as the above Query, both use Linq
                    var count2 = postTable.Where(p => p.Catagoryname == category).ToList().Count;

                    categoriesCount.Add(category, count);// use either the Query variable count, or the Method variable count2
                }

                categoriesListView.ItemsSource = categoriesCount;

                postCountLabel.Text = postTable.Count.ToString();
            }
        }
    }
}