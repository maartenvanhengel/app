using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
        public UpgradePageViewModel(INavigationService navigationservice)
            :base(navigationservice)
        {
            /*var patht = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filenamet = Path.Combine(patht, "playerData.txt");
            File.Delete(filenamet);*/
            //HealthPotion = 0;
            HealthPotionCost = 1;

            //Shield = 0;
            ShieldCost = 2;

            //AttackDamage = 1;
            AttackDamageCost = 1;

            healthPotionCommand = new DelegateCommand(executeHealthPotion);
            shieldCommand = new DelegateCommand(executeShield);
            attackDamageCommand = new DelegateCommand(executeAttackDamage);
            enterArenaCommand = new DelegateCommand(executeEnterArena);
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var filename = Path.Combine(path, "playerData.txt");
                StreamReader reader = new StreamReader(filename);
                string line = reader.ReadLine();
                string[] words;
                char[] separators = { ';' };
                words = line.Split(separators);
                Coins = Convert.ToInt32(words[0]);
                line = reader.ReadLine();
                words = line.Split(separators);
                HealthPotion = Convert.ToInt32(words[0]);
                Shield = Convert.ToInt32(words[1]);
                AttackDamage = Convert.ToInt32(words[2]);
            }
            catch (Exception)
            {
                Coins = 2;
                HealthPotion = 0;
                Shield = 0;
                attackDamage = 0;
            }

        }
        private int coins;
        public int Coins
        {
            get { return coins; }
            set { SetProperty(ref coins, value); }
        }
        public ICommand healthPotionCommand { get; private set; }
        private int healthPotion ;
        public int HealthPotion
        {
            get { return healthPotion; }
            set { if (SetProperty(ref healthPotion, value))
                {
                    HealthPotionCost = HealthPotionCost * 2;
                    if (HealthPotion == 0)
                    {
                        HealthPotion1 = "Gray";
                        HealthPotion2 = "Gray";
                        HealthPotion3 = "Gray";
                        HealthPotion4 = "Gray";
                    }
                    else if (HealthPotion == 1)
                    {
                        HealthPotion1 = "Green";
                        HealthPotion2 = "Gray";
                        HealthPotion3 = "Gray";
                        HealthPotion4 = "Gray";
                    }
                    else if (HealthPotion == 2)
                    {
                        HealthPotion1 = "Green";
                        HealthPotion2 = "Green";
                        HealthPotion3 = "Gray";
                        HealthPotion4 = "Gray";
                    }
                    else if (HealthPotion == 3)
                    {
                        HealthPotion1 = "Green";
                        HealthPotion2 = "Green";
                        HealthPotion3 = "Green";
                        HealthPotion4 = "Gray";
                    }
                    else if (HealthPotion == 4)
                    {
                        HealthPotion1 = "Green";
                        HealthPotion2 = "Green";
                        HealthPotion3 = "Green";
                        HealthPotion4 = "Green";
                    }
                }
            ; }
        }
        private string healthPotion1;
        public string HealthPotion1
        {
            get { return healthPotion1; }
            set { SetProperty(ref healthPotion1, value); }
        }
        private string healthPotion2;
        public string HealthPotion2
        {
            get { return healthPotion2; }
            set { SetProperty(ref healthPotion2, value); }
        }
        private string healthPotion3;
        public string HealthPotion3
        {
            get { return healthPotion3; }
            set { SetProperty(ref healthPotion3, value); }
        }
        private string healthPotion4;
        public string HealthPotion4
        {
            get { return healthPotion4; }
            set { SetProperty(ref healthPotion4, value); }
        }
        private int healthPotionCost;
        public int HealthPotionCost
        {
            get { return healthPotionCost; }
            set { SetProperty(ref healthPotionCost, value); }
        }
        private void executeHealthPotion()
        {
            if (Coins >= HealthPotionCost)
            {
                if (HealthPotion < 4)
                {
                    Coins = Coins - HealthPotionCost;
                    HealthPotion = HealthPotion + 1;
                }
            }
        }

        private int shield;
        public int Shield
        {
            get { return shield; }
            set { if (SetProperty(ref shield, value))
                {
                    ShieldCost = ShieldCost * 2;
                    if (Shield == 0)
                    {
                        Shield1 = "gray";
                        Shield2 = "gray";
                        Shield3 = "gray";
                        Shield4 = "gray";
                    }
                    else if (shield == 1)
                    {
                        Shield1 = "green";
                        Shield2 = "gray";
                        Shield3 = "gray";
                        Shield4 = "gray";
                    }
                    else if (shield == 2)
                    {
                        Shield1 = "green";
                        Shield2 = "green";
                        Shield3 = "gray";
                        Shield4 = "gray";
                    }
                    else if (shield == 3)
                    {
                        Shield1 = "green";
                        Shield2 = "green";
                        Shield3 = "green";
                        Shield4 = "gray";
                    }
                    else if (shield == 4)
                    {
                        Shield1 = "green";
                        Shield2 = "green";
                        Shield3 = "green";
                        Shield4 = "green";
                    }
                }
            ; }
        }
        private string shield1;
        public string Shield1
        {
            get { return shield1; }
            set { SetProperty(ref shield1, value); }
        }
        private string shield2;
        public string Shield2
        {
            get { return shield2; }
            set { SetProperty(ref shield2, value); }
        }
        private string shield3;
        public string Shield3
        {
            get { return shield3; }
            set { SetProperty(ref shield3, value); }
        }
        private string shield4;
        public string Shield4
        {
            get { return shield4; }
            set { SetProperty(ref shield4, value); }
        }
        private int shieldCost;
        public int ShieldCost
        {
            get { return shieldCost; }
            set { SetProperty(ref shieldCost, value); }
        }
        public ICommand shieldCommand { get; private set; }
        public void executeShield()
        {
            if (Coins >= ShieldCost)
            {
                if (Shield < 4)
                {
                    Coins = Coins - ShieldCost;
                    Shield++;
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
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filename = Path.Combine(path, "playerData.txt");
            StreamWriter writer;
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            writer = File.CreateText(filename);
            writer.WriteLine($"{coins};");
            writer.WriteLine($"{HealthPotion};{Shield};{AttackDamage}");
            writer.Close();

            /*Player Player = new Player();
            Player.AttackDamage = 12;
            Player.HealthPotion = HealthPotion;
            Player.Name = "test";
            Player.Shield = Shield;

            var p = new NavigationParameters();
            p.Add("Player", Player);*/
            await NavigationService.NavigateAsync(nameof(BattlePage));
        }
    }
}
