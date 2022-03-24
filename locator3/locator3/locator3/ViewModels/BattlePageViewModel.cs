using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    public class BattlePageViewModel : ViewModelBase
    {
        int attackStrength;
        int coins;
        int HealthPotion;
        int Shield;
        int level;
        bool shieldEnabled = false;
        Player player;
        private IPageDialogService pageDialogService;
        public BattlePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            :base(navigationService)
        {
            player = new Player();
            PlayerHealth = 1;
            ComputerHealth = 1;
            ComputerName = "computer";
            AttackEnabled = true;
            shieldEnabled = false;
            attack1Command = new DelegateCommand(executeAttack1);
            attack2Command = new DelegateCommand(executeAttack2);
            attack3Command = new DelegateCommand(executeAttack3);
            useShieldCommand = new DelegateCommand(executeUseShield);
            usePotionCommand = new DelegateCommand(executeUsePotion);
            this.pageDialogService = pageDialogService;
            attackStrength = 2;

            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var filename = Path.Combine(path, "playerData.txt");
                StreamReader reader = new StreamReader(filename);
                string line = reader.ReadLine();
                string[] words;
                char[] separators = { ';' };
                words = line.Split(separators);
                coins = Convert.ToInt32(words[0]);
                line = reader.ReadLine();
                words = line.Split(separators);
                HealthPotion = Convert.ToInt32(words[0]);
                Shield = Convert.ToInt32(words[1]);
                attackStrength = Convert.ToInt32(words[2]);
                reader.Close();
            }
            catch
            {
                pageDialogService.DisplayAlertAsync("Error", "error happend try again", "OK");
            }
            pageDialogService.DisplayAlertAsync("Error", $"{coins} HealthP {HealthPotion}, shieldEnabled{Shield}, strenght{attackStrength}", "OK");
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                player = parameters.GetValue<Player>("Player");
                attackStrength = player.AttackDamage;
                PlayerName = player.Name;
            }
            else if (parameters.ContainsKey("level"))
            {
                level = parameters.GetValue<int> ("level");
            }
            else
            {
                level = 1;
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
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
            writer.WriteLine($"{HealthPotion};{Shield};{attackStrength}");
            writer.Close();
        }

        private bool attackEnabled;
        public bool AttackEnabled
        {
            get { return attackEnabled; }
            set { SetProperty(ref attackEnabled, value); }
        }
        private string computerName;
        public string ComputerName
        {
            get { return computerName; }
            set { SetProperty(ref computerName, value); }
        }
        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { SetProperty(ref playerName, value); }
        }
        private bool lastAttackVisuable;
        public bool LastAttackVisuable
        {
            get { return lastAttackVisuable; }
            set { SetProperty(ref lastAttackVisuable, value); }
        }
        private string lastComputerAttack;
        public string LastComputerAttack
        {
            get { return lastComputerAttack; }
            set { SetProperty(ref lastComputerAttack, value); }
        }
        private string lastPlayerAttack;
        public string LastPlayerAttack
        {
            get { return lastPlayerAttack; }
            set { SetProperty(ref lastPlayerAttack, value); }
        }
        private double playerHealth;
        public double  PlayerHealth
        {
            get { return playerHealth; }
            set { SetProperty(ref playerHealth, value); }
        }
        private double computerHealth;
        public double ComputerHealth
        {
            get { return computerHealth; }
            set { SetProperty(ref computerHealth, value); }
        }
        public ICommand attack1Command { get; private set; }
        public ICommand attack2Command { get; private set; }
        public ICommand attack3Command { get; private set; }

        private void executeAttack1()
        {
            double attack = 0;
            if (level < 2)
            {
                attack = attackStrength * 0.05;
                LastPlayerAttack = "-" + (attack * 100);
            }
            else
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 2);
                if (random == 0)
                {
                    attack = attackStrength * 0.05;
                    LastPlayerAttack = "-" + (attack * 100);
                }
                else
                {
                    LastPlayerAttack = "attack not effective";
                }

            }
            ComputerHealth = ComputerHealth - attack;
            checkPlayerHealth();
        }
        private void executeAttack2()
        {
            Random rnd = new Random();
            int random = rnd.Next(0, 2);
            double attack = 0;
            if (random == 0)
            {
                attack = attackStrength * 0.08;
                LastPlayerAttack = "-" + (attack * 100);
            }
            else
            {
                LastPlayerAttack = "attack failed";
            }
            ComputerHealth = ComputerHealth - attack;
            checkPlayerHealth();
        }
        private void executeAttack3()
        {
            Random rnd = new Random();
            int random = rnd.Next(0, 3);
            double attack = 0;
            if (random == 0)
            {
                attack = attackStrength * 0.12;
                LastPlayerAttack = "-" + (attack * 100);
            }
            else
            {
                LastPlayerAttack = "attack failed";
            }
            ComputerHealth = ComputerHealth - attack;
            checkPlayerHealth();
        }
        private void checkPlayerHealth()
        {
            if (ComputerHealth <= 0)
            {
                endGame(PlayerName);
            }
            else
            {
                //computers beurt
                AttackEnabled = false;
                timer();
            }
        }

        private void computerAttack()
        {
            Random rnd = new Random();
            int random = rnd.Next(0, 3);
            double attack =0;
            if (random ==0)
            {
                if (shieldEnabled == false)
                {
                    attack = attackStrength * 0.05;
                    LastComputerAttack = "-" + (attack * 100);
                }
                else
                {
                    LastComputerAttack = "blocked by shieldEnabled";
                    shieldEnabled = false;
                }
            }
            else if(random == 1)
            {
                Random rndAttack = new Random();
                int randomAttack = rnd.Next(0, 2);
                if (shieldEnabled == false)
                {
                    if (randomAttack == 0)
                    {
                        attack = attackStrength * 0.08;
                        LastComputerAttack = "-" + (attack * 100);
                    }
                    else
                    {
                        LastComputerAttack = "attack failed";
                    }
                }
                else
                {
                    LastComputerAttack = "blocked by shieldEnabled";
                    shieldEnabled = false;
                }
            }
            else
            {
                if (shieldEnabled == false)
                {
                    Random rndAttack = new Random();
                    int randomAttack = rnd.Next(0, 4);
                    if (randomAttack == 0)
                    {
                        attack = attackStrength * 0.12;

                        LastComputerAttack = "-" + (attack * 100);
                    }
                    else
                    {
                        LastComputerAttack = "missed";
                    }
                }
                else
                {
                    Random rndAttack = new Random();
                    int randomAttack = rnd.Next(0, 2);
                    if (randomAttack == 0)
                    {
                        attack = attackStrength * 0.10;

                        LastComputerAttack = "-" + (attack * 100);
                    }
                    else
                    {
                        LastComputerAttack = "missed";
                    }
                    shieldEnabled = false;
                }
                
            }
            if (PlayerHealth <= 2)
            {
                PlayerHealth = PlayerHealth - attack;
            }
            else if(playerHealth <= 4)
            {
                PlayerHealth = PlayerHealth - (attack / 2);
            }
            else
            {
                PlayerHealth = PlayerHealth - (attack / 3);
            }
            if (playerHealth <= 0)
            {
                endGame(ComputerName);
            }
        }
        private async void endGame(string who)
        {
            //coins vermenigvuldigen indien winst
            if (who != computerName)
            {
                coins = coins + (level * 3);
            }
            //save data in file
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filename = Path.Combine(path, "playerData.txt");
            StreamWriter writer;
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            writer = File.CreateText(filename);
            writer.WriteLine($"{coins};");
            writer.WriteLine($"{HealthPotion};{Shield};{attackStrength}");
            writer.Close();


            await pageDialogService.DisplayAlertAsync("Game ended", who + " won the game", "OK");
            AttackEnabled = false;
            await NavigationService.GoBackAsync();
        }
        private void timer()
        {
            int value = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(1100), () =>
            {
                value++;
                if (value == 1)
                {
                    LastPlayerAttack = "";
                    return true;
                }
                else if (value == 2)
                {
                    computerAttack();
                    return true;
                }
                else
                {
                    LastComputerAttack = "";
                    AttackEnabled = true;
                    return false;
                }
            });
        }

        public ICommand useShieldCommand { get; private set; }
        public void executeUseShield()
        {
            if (Shield >= 0)
            {
                Shield = Shield - 1;
                shieldEnabled = true;
                checkPlayerHealth();
            }
        }

        public ICommand usePotionCommand { get; private set; }
        public void executeUsePotion()
        {
            if (HealthPotion >= 1)
            {
                if (PlayerHealth < 0.80)
                {
                    PlayerHealth = PlayerHealth + 0.20;
                    HealthPotion = HealthPotion - 1;
                }
            }
        }
    }
}
