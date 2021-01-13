using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordsApp.Logic;
using TravelRecordsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public double MetersToMiles(int distanceMeters)
        {
            var conversion = 1609.344;//convert from meters to miles
            return distanceMeters / conversion;
        }

        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            foreach (var meters in venues)
            {

                meters.location.distanceInMiles = MetersToMiles(meters.location.distance);

            }
            venueListView.ItemsSource = venues;

        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCatagory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = experienceEditor.Text,
                    Catagoryid = firstCatagory.id,
                    Catagoryname = firstCatagory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name

                };


                /*
                SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
                conn.CreateTable<Post>();
                int rows = conn.Insert(post);
                conn.Close();

                if(rows > 0)
                {
                    DisplayAlert("Success","Experience Successfully Inserted","OK");
                }
                else
                {
                    DisplayAlert("Failure", "Experience Failed to be Inserted", "OK");
                }
                */

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    // this is the same as the above code
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(post);
                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience Successfully Inserted", "OK");
                        Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        DisplayAlert("Failure", "Experience Failed to be Inserted", "OK");
                    }
                }
            }
            catch(NullReferenceException nre)
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}