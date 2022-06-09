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
        public mapsPage()
        {
            InitializeComponent();

            DisplayCurrentLocation();
        }
        public async void DisplayCurrentLocation()
        {

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(System.TimeSpan.FromSeconds(1));
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMeters(20)));
            //myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(50.90230, 5.65703), Xamarin.Forms.Maps.Distance.FromMeters(20)));

        }
        
    }
}