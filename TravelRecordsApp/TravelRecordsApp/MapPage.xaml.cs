using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using TravelRecordsApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        public bool hasLocationPermission = false;
		public MapPage ()
		{
			InitializeComponent ();
            GetPermissions();
		}

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }

                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                    hasLocationPermission = true;
                    locationsMap.IsShowingUser = true;
                    GetLocation();
                    
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //location denied
                    hasLocationPermission = false;
                    locationsMap.IsShowingUser = false;
                    await DisplayAlert("Permission Denied", "", "OK");
                }
            }
            catch (Exception ex)
            {
                //Something went wrong
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged; // subscribe to the event handeler
                await locator.StartListeningAsync(TimeSpan.Zero,100);
            }
                GetLocation();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // using the using statement like this doesn't need the Close() function because it is declared in the iIDisposable interface
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                DisplayInMap(posts);
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CrossGeolocator.Current.StopListeningAsync(); //stop listening
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged; //unsubscribe from the event handeler
        }

        // event handeler for position change
        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                //locationsMap.MoveToRegion(new Xamarin.Forms.Maps.MapSpan(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),1,1).WithZoom(zoom));
                MoveMap(position);
            }
        }
        private void MoveMap(Position position)
        {
            double zoom = .5;
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 0.01, 0.01).WithZoom(zoom);
            locationsMap.MoveToRegion(span);
            
        }
        private void DisplayInMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };
                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre)
                {

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}