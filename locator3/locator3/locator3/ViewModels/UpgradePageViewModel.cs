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
            Health1 = "Gray";
            Health2 = "Gray";
            Health3 = "Gray";
            Health4 = "Gray";

            Armour1 = "gray";
            Armour2 = "gray";
            Armour3 = "gray";
            Armour4 = "gray";

            AttackDamage1 = "gray";
            AttackDamage2 = "gray";
            AttackDamage3 = "gray";
            AttackDamage4 = "gray";

            player = new Player();
            dragon = new Dragon();

            this.pageDialogService = pageDialogService;
            /*var patht = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filenamet = Path.Combine(patht, "playerData.txt");
            File.Delete(filenamet);*/
            //Health = 0;
            HealthCost = 1;

            //Shield = 0;
            ArmourCost = 2;

            //AttackDamage = 1;
            AttackDamageCost = 1;

            healthPotionCommand = new DelegateCommand(executeHealthPotion);
            shieldCommand = new DelegateCommand(executeShield);
            attackDamageCommand = new DelegateCommand(executeAttackDamage);
            enterArenaCommand = new DelegateCommand(executeEnterArena);
            

        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("player"))
            {
                player = parameters.GetValue<Player>("player");
                Coins = player.Coins;
                PlayerName = player.Name;
                dragon = player.Dragon;


                Health =dragon.Health;
                Armour = dragon.Armour;
                AttackDamage = dragon.Damage;
            }
            else
            {
         //       await pageDialogService.DisplayAlertAsync("Error", "error happend, try again", "ok");
               await NavigationService.GoBackAsync();
            }
        }
        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            var p = new NavigationParameters();
            p.Add("player", player);
            await NavigationService.GoBackAsync(p);
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
        public ICommand healthPotionCommand { get; private set; }
        private int health ;
        public int Health
        {
            get { return health; }
            set { if (SetProperty(ref health, value))
                {
                    dragon.Health = Health;
                    HealthCost = HealthCost * 2;
                    if (Health == 0)
                    {
                        Health1 = "Gray";
                        Health2 = "Gray";
                        Health3 = "Gray";
                        Health4 = "Gray";
                    }
                    else if (Health == 1)
                    {
                        Health1 = "Green";
                        Health2 = "Gray";
                        Health3 = "Gray";
                        Health4 = "Gray";
                    }
                    else if (Health == 2)
                    {
                        Health1 = "Green";
                        Health2 = "Green";
                        Health3 = "Gray";
                        Health4 = "Gray";
                    }
                    else if (Health == 3)
                    {
                        Health1 = "Green";
                        Health2 = "Green";
                        Health3 = "Green";
                        Health4 = "Gray";
                    }
                    else if (Health == 4)
                    {
                        Health1 = "Green";
                        Health2 = "Green";
                        Health3 = "Green";
                        Health4 = "Green";
                    }
                }
            ; }
        }
        private string health1;
        public string Health1
        {
            get { return health1; }
            set { SetProperty(ref health1, value); }
        }
        private string health2;
        public string Health2
        {
            get { return health2; }
            set { SetProperty(ref health2, value); }
        }
        private string health3;
        public string Health3
        {
            get { return health3; }
            set { SetProperty(ref health3, value); }
        }
        private string health4;
        public string Health4
        {
            get { return health4; }
            set { SetProperty(ref health4, value); }
        }
        private int healthCost;
        public int HealthCost
        {
            get { return healthCost; }
            set { SetProperty(ref healthCost, value); }
        }
        private void executeHealthPotion()
        {
            if (Coins >= HealthCost)
            {
                if (Health < 4)
                {
                    Coins = Coins - HealthCost;
                    Health = Health + 1;
                }
            }
        }

        private int armour;
        public int Armour
        {
            get { return armour; }
            set { if (SetProperty(ref armour, value))
                {
                    ArmourCost = ArmourCost * 2;
                    if (Armour == 0)
                    {
                        Armour1 = "gray";
                        Armour2 = "gray";
                        Armour3 = "gray";
                        Armour4 = "gray";
                    }
                    else if (armour == 1)
                    {
                        Armour1 = "green";
                        Armour2 = "gray";
                        Armour3 = "gray";
                        Armour4 = "gray";
                    }
                    else if (armour == 2)
                    {
                        Armour1 = "green";
                        Armour2 = "green";
                        Armour3 = "gray";
                        Armour4 = "gray";
                    }
                    else if (armour == 3)
                    {
                        Armour1 = "green";
                        Armour2 = "green";
                        Armour3 = "green";
                        Armour4 = "gray";
                    }
                    else if (armour == 4)
                    {
                        Armour1 = "green";
                        Armour2 = "green";
                        Armour3 = "green";
                        Armour4 = "green";
                    }
                    dragon.Armour = Armour;
                }
            ; }
        }
        private string armour1;
        public string Armour1
        {
            get { return armour1; }
            set { SetProperty(ref armour1, value); }
        }
        private string armour2;
        public string Armour2
        {
            get { return armour2; }
            set { SetProperty(ref armour2, value); }
        }
        private string armour3;
        public string Armour3
        {
            get { return armour3; }
            set { SetProperty(ref armour3, value); }
        }
        private string armour4;
        public string Armour4
        {
            get { return armour4; }
            set { SetProperty(ref armour4, value); }
        }
        private int armourCost;
        public int ArmourCost
        {
            get { return armourCost; }
            set { SetProperty(ref armourCost, value); }
        }
        public ICommand shieldCommand { get; private set; }
        public void executeShield()
        {
            if (Coins >= ArmourCost)
            {
                if (Armour < 4)
                {
                    Coins = Coins - ArmourCost;
                    Armour++;
                }
            }
        }

        private int attackDamage;
        public int AttackDamage
        {
            get { return attackDamage; }
            set {if (SetProperty(ref attackDamage, value))
                {
                    AttackDamageCost = AttackDamageCost * 2;
                    if (AttackDamage == 0)
                    {
                        AttackDamage1 = "gray";
                        AttackDamage2 = "gray";
                        AttackDamage3 = "gray";
                        AttackDamage4 = "gray";
                    }
                    else if (AttackDamage == 1)
                    {
                        AttackDamage1 = "green";
                        AttackDamage2 = "gray";
                        AttackDamage3 = "gray";
                        AttackDamage4 = "gray";
                    }
                    else if (AttackDamage == 2)
                    {
                        AttackDamage1 = "green";
                        AttackDamage2 = "green";
                        AttackDamage3 = "gray";
                        AttackDamage4 = "gray";
                    }
                    else if (AttackDamage == 3)
                    {
                        AttackDamage1 = "green";
                        AttackDamage2 = "green";
                        AttackDamage3 = "green";
                        AttackDamage4 = "gray";
                    }
                    else if (AttackDamage == 4)
                    {
                        AttackDamage1 = "green";
                        AttackDamage2 = "green";
                        AttackDamage3 = "green";
                        AttackDamage4 = "green";
                    }
                  //  player.AttackDamage = attackDamage;
                }
            ; }
        }

        private string attackDamage1;
        public string AttackDamage1
        {
            get { return attackDamage1; }
            set { SetProperty(ref attackDamage1, value); }
        }
        private string attackDamage2;
        public string AttackDamage2
        {
            get { return attackDamage2; }
            set { SetProperty(ref attackDamage2, value); }
        }

        private string attackDamage3;
        public string AttackDamage3
        {
            get { return attackDamage3; }
            set { SetProperty(ref attackDamage3, value); }
        }
        private string attackDamage4;
        public string AttackDamage4
        {
            get { return attackDamage4; }
            set { SetProperty(ref attackDamage4, value); }
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
                if (AttackDamage < 4)
                {
                    Coins = Coins - AttackDamageCost;
                    AttackDamage++;
                }
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
