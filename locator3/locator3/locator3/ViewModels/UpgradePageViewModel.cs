using locator3.Models;
using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    public class UpgradePageViewModel : ViewModelBase
    {
        private IPageDialogService pageDialogService;
        Player player;
        Dragon dragon;
        public UpgradePageViewModel(INavigationService navigationservice, IPageDialogService pageDialogService)
            :base(navigationservice)
        {

            player = new Player();
            dragon = new Dragon();

            this.pageDialogService = pageDialogService;
            /*var patht = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filenamet = Path.Combine(patht, "playerData.txt");
            File.Delete(filenamet);*/
           

            healthCommand = new DelegateCommand(executeHealth);
            ArmourCommand = new DelegateCommand(executeArmour);
            attackDamageCommand = new DelegateCommand(executeAttackDamage);
            enterArenaCommand = new DelegateCommand(executeEnterArena);
           
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            HealthCost = Preferences.Get("HealthCost", 1);
            ArmourCost = Preferences.Get("ArmourCost", 2);
            AttackDamageCost = Preferences.Get("AttackDamageCost", 1);

            if (parameters.ContainsKey("player"))
            {
                player = parameters.GetValue<Player>("player");
                Coins = player.Coins;
                PlayerName = player.Name;
                dragon = player.Dragon;


                Health =dragon.Health;
                Armour = dragon.Armour;
                AttackDamage = dragon.Damage;
                DragonSource = dragon.Image;
                DragonName = dragon.Name;
            }
            else
            {
         //       await pageDialogService.DisplayAlertAsync("Error", "error happend, try again", "ok");
               await NavigationService.GoBackAsync();
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Preferences.Set("HealthCost", HealthCost);
            Preferences.Set("AttackDamageCost", AttackDamageCost);
            Preferences.Set("ArmourCost", ArmourCost);
        }
        private string dragonName;
        public string DragonName
        {
            get { return dragonName; }
            set { SetProperty(ref dragonName, value); }
        }
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { SetProperty(ref playerName, value); }
        }
        private int coins;
        public int Coins
        {
            get { return coins; }
            set {if (SetProperty(ref coins, value))
                {
                    player.Coins = Coins;
                }; }
        }
        private string dragonSource;
        public string DragonSource
        {
            get { return dragonSource; }
            set { SetProperty(ref dragonSource, value); }
        }
        public ICommand healthCommand { get; private set; }
        private int health ;
        public int Health
        {
            get { return health; }
            set { if (SetProperty(ref health, value))
                {
                    dragon.Health = Health;
                    
                }
            ; }
        }
        
        private int healthCost;
        public int HealthCost
        {
            get { return healthCost; }
            set { SetProperty(ref healthCost, value); }
        }
        private void executeHealth()
        {
            if (Coins >= HealthCost)
            {
                Coins = Coins - HealthCost;
                Health = Health + 100;
                HealthCost = HealthCost * 2;
            }
        }

        private int armour;
        public int Armour
        {
            get { return armour; }
            set { if (SetProperty(ref armour, value))
                {
                    dragon.Armour = Armour;
                }
            ; }
        }
       
        private int armourCost;
        public int ArmourCost
        {
            get { return armourCost; }
            set { SetProperty(ref armourCost, value); }
        }
        public ICommand ArmourCommand { get; private set; }
        public void executeArmour()
        {
            if (Coins >= ArmourCost)
            {
                    Coins = Coins - ArmourCost;
                    Armour = armour +10;
                    ArmourCost = ArmourCost * 2;
            }
        }

        private int attackDamage;
        public int AttackDamage
        {
            get { return attackDamage; }
            set {if (SetProperty(ref attackDamage, value))
                {
                    dragon.Damage = AttackDamage;
                }
            ; }
        }
        
        private int attackDamageCost;
        public int AttackDamageCost
        {
            get { return attackDamageCost; }
            set { SetProperty(ref attackDamageCost, value); }
        }
        public ICommand attackDamageCommand { get; private set; }
        public void executeAttackDamage()
        {
            if (Coins >= AttackDamageCost)
            {
                Coins = Coins - AttackDamageCost;
                AttackDamage = AttackDamage + 10;
                AttackDamageCost = AttackDamageCost * 2;
            }
        }

        public ICommand enterArenaCommand { get; private set; }
        public async void executeEnterArena()
        {
            /*    var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var filename = Path.Combine(path, "playerData.txt");
                StreamWriter writer;
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                writer = File.CreateText(filename);
                writer.WriteLine($"{Coins};");
                writer.WriteLine($"{Health};{Shield};{AttackDamage}");
                writer.Close();

                /*Player Player = new Player();
                Player.AttackDamage = 12;
                Player.Health = Health;
                Player.Name = "test";
                Player.Shield = Shield;

                var p = new NavigationParameters();
                p.Add("Player", Player);*/
            //   await NavigationService.NavigateAsync(nameof(BattlePage));

            var p = new NavigationParameters();
            p.Add("player", player);
            await NavigationService.GoBackAsync(p);
        }
    }
}
