using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Maps;

namespace locator3.ViewModels
{
    public class testPageViewModel : BindableBase
    {
        public ObservableCollection<Location> Locations { get; private set; }
        public testPageViewModel()
        {
            Locations = new ObservableCollection<Location>();

            Locations.Add(new Location() { Position = new Position(50.88982848322846, 5.628560006138596), Address ="test", Description="uitleg" });
            
        }
    }
}
