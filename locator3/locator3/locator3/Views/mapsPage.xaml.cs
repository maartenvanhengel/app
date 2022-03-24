using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace locator3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mapsPage : ContentPage
    {
        List<Pin> pinList = new List<Pin>();
        double[,] markers = new double[5, 2];
        public mapsPage()
        {
            InitializeComponent();

            DisplayCurrentLocation();
            setMarkers();

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {

                return true; // return true to repeat counting, false to stop timer
            });
        }
        public async void DisplayCurrentLocation()
        {

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(System.TimeSpan.FromSeconds(1));
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMeters(20)));
            //myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(50.90230, 5.65703), Xamarin.Forms.Maps.Distance.FromMeters(20)));

        }
        public void setMarkers()
        {
            markers[0, 0] = 50.8905596998499;
            markers[0, 1] = 5.6313498367138575;
            /*markers[0, 0] = 50.90230;
            markers[0, 1] =  5.65703;*/
            markers[1, 0] = 50.89142588764658;
            markers[1, 1] = 5.633614831652177;

            for (int i = 0; i < 5; i++)
            {
                Pin marker = new Pin
                {
                    Type = PinType.Generic,
                    Label = "marker " + Convert.ToString(i),
                    Position = new Position(markers[i, 0], markers[i, 1])

                };
                myMap.Pins.Add(marker);
                pinList.Add(marker);
            }
        }

        public async void checkLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(System.TimeSpan.FromSeconds(1));
            bool binnen = false;
            for (int i = 0; i < 4; i++)
            {
                if (position.Latitude <= markers[i, 0] + 0.0004 && position.Latitude >= markers[i, 1] - 0.004 && position.Longitude <= markers[i, 1] + 0.0004 && position.Longitude >= markers[i, 1] - 0.0004)
                {
                    await Application.Current.MainPage.DisplayAlert("found",Convert.ToString( i) , "ok");
                    //await DisplayAlert("Alert", "binnen", "OK");
                    binnen = true;
                }
                else if (binnen != true)
                {
                    //await DisplayAlert("Alert", "You haven't alerted", "OK");
                    binnen = false;
                }
            }
            if (binnen == true)
            {
                //await DisplayAlert("Alert", "binnen", "OK");
                //           btnTest.IsVisible = true;
            }
            else
            {
                //          btnTest.IsVisible = false;
            }
        }
    }
}