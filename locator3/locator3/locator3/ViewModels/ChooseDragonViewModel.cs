using locator3.Models;
using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace locator3.ViewModels
{
    public class ChooseDragonViewModel : ViewModelBase
    {
        Dragon dragon1;
        Dragon dragon2;
        Dragon dragon3;
        Dragon dragon4;
        Dragon dragon5;

        Dragon[] dragons;
        private IPageDialogService pageDialogService;
        public ChooseDragonViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            dragons = new Dragon[5];

            this.pageDialogService = pageDialogService;

            leftCommand = new DelegateCommand(executeLeft);
            rightCommand = new DelegateCommand(executeRight);
            useCommand = new DelegateCommand(executeUse);
            DragonNumber = 0;

            dragon1 = new Dragon()
            {
                Name = "Gunde",
                Image = "dragon1.gif",
                Health = 800,
                Damage = 115,
                Armour = 20,
                Height = 2.5,
                Weight = 250
            };
            dragons[0] = dragon1;
            dragon2 = new Dragon()
            {
                Name = "Neldro",
                Image = "dragon2.gif",
                Health = 1000,
                Damage = 100,
                Armour = 20,
                Height = 2.5,
                Weight = 250
            };
            dragons[1] = dragon2;
            dragon3 = new Dragon()
            {
                Name = "Smaug",
                Image = "dragon3.gif",
                Health = 800,
                Damage = 100,
                Armour = 30,
                Height = 2.5,
                Weight = 250
            };
            dragons[2]= dragon3;
            dragon4 = new Dragon()
            {
                Name = "Igud",
                Image = "dragon4.gif",
                Health = 800,
                Damage = 110,
                Armour = 25,
                Height = 2.5,
                Weight = 250
            };
            dragons[3] = dragon4;
            dragon5 = new Dragon()
            {
                Name = "Byrsis",
                Image = "dragon5.gif",
                Health = 900,
                Damage = 105,
                Armour = 22,
                Height = 2.5,
                Weight = 250
            };
            dragons[4] = dragon5;

            //eerste bepalen

            DragonImage = dragons[0].Image;
            DragonArmour = dragons[0].Armour;
            DragonDamage = dragons[0].Damage;
            DragonHealth = dragons[0].Health;
            DragonName = dragons[0].Name;
        }
        public ICommand leftCommand { get; private set; }
        public ICommand rightCommand { get; private set; }
        public ICommand useCommand { get; private set; }
        private void executeLeft()
        {
            if (DragonNumber == 0)
            {
                DragonNumber = 4;
            }
            else
            {
                DragonNumber = DragonNumber - 1;
            }
        }
        private void executeRight()
        {
            if (DragonNumber == 4)
            {
                DragonNumber = 0;
            }
            else
            {
                DragonNumber = DragonNumber + 1;
            }
        }
        private async void executeUse()
        {
           var result = await pageDialogService.DisplayAlertAsync("Dragon", "are you sure?", "ok", "Cancel");
            if (result)
            {
                var p = new NavigationParameters();
                p.Add("dragon", dragons[dragonNumber]);
                await NavigationService.GoBackAsync(p);
            }
        }
        private int dragonNumber;
        public int DragonNumber
        {
            get { return dragonNumber; }
            set {if (SetProperty(ref dragonNumber, value))
                {
                    Dragon chosenDragon = new Dragon();
                    chosenDragon = dragons[DragonNumber];
                    DragonImage = chosenDragon.Image;
                    DragonDamage = chosenDragon.Damage;
                    DragonArmour = chosenDragon.Armour;
                    DragonHealth = chosenDragon.Health;
                    DragonName = chosenDragon.Name;
                    DragonWeight = chosenDragon.Weight;
                    DragonHeight = chosenDragon.Height;
                }; }
        }
        private string dragonImage;
        public string DragonImage
        {
            get { return dragonImage; }
            set { SetProperty(ref dragonImage, value); }
        }
        private int dragonHealth;
        public int DragonHealth
        {
            get { return dragonHealth; }
            set { SetProperty(ref dragonHealth, value); }
        }
        private int dragonDamage;
        public int DragonDamage
        {
            get { return dragonDamage; }
            set { SetProperty(ref dragonDamage, value); }
        }
        private int dragonArmour;
        public int DragonArmour
        {
            get { return dragonArmour; }
            set { SetProperty(ref dragonArmour, value); }
        }
        private string dragonName;
        public string DragonName
        {
            get { return dragonName; }
            set { SetProperty(ref dragonName, value); }
        }
        private double dragonWeight;
        public double DragonWeight
        {
            get { return dragonWeight; }
            set { SetProperty(ref dragonWeight, value); }
        }
        private double dragonheight;
        public double DragonHeight
        {
            get { return dragonheight; }
            set { SetProperty(ref dragonheight, value); }
        }
    }
}
