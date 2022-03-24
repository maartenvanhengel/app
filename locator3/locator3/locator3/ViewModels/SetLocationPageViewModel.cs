using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    public class SetLocationPageViewModel : ViewModelBase
    {
        private IPageDialogService pageDialogService;
        public SetLocationPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            this.pageDialogService = pageDialogService;
            clickCommand = new DelegateCommand<Xamarin.Forms.Maps.MapClickedEventArgs>(executeClick);
            QuestionCommand = new DelegateCommand(executeQuestion);
        }
        public ICommand clickCommand { get; private set; }
        public ICommand QuestionCommand { get; set; }
        private async void executeClick(Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            string text = "";
            var result2 = await Xamarin.Essentials.Geocoding.GetPlacemarksAsync(e.Position.Latitude, e.Position.Longitude);
            if (result2.Any())
            {
                text = $" {result2.FirstOrDefault()?.Locality} {result2.FirstOrDefault()?.Thoroughfare} {result2.FirstOrDefault()?.SubThoroughfare}";
            }
            var result = await pageDialogService.DisplayAlertAsync("Location", $"{e.Position.Latitude},{e.Position.Longitude} \n {text}" , "ok", "cancel");
            if (result)
            {
                var p = new NavigationParameters();
                p.Add("lat", e.Position.Latitude);
                p.Add("long", e.Position.Longitude);
                await NavigationService.GoBackAsync( p);
            }
        }
        public async void convertCoordinatesToAdress(double lat, double lng)
        {
            try
            {
                string text = "";
                var result2 = await Xamarin.Essentials.Geocoding.GetPlacemarksAsync(lat, lng);
                if (result2.Any())
                {
                    text= $" {result2.FirstOrDefault()?.Locality}, {result2.FirstOrDefault()?.SubLocality}";
                }
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("ex", ex.ToString(), "OK");
            }
        }
        private void executeQuestion()
        {
            pageDialogService.DisplayAlertAsync("?", "click on the map to set the location of your pointer", "ok");
        }
    }
}
