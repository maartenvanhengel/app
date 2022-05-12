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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    public class BattlePageViewModel : ViewModelBase
    {
        int level;
        int maxComputerHealth;
        int maxPlayerHealth;
        bool specialAttack;
        Player player;
        Dragon dragon;
        Dragon opponentDragon;
        private IPageDialogService pageDialogService;
        public BattlePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {

            Button1Pressed = 0;
            Button2Pressed = 0;
            Button3Pressed = 0;

            player = new Player();
            dragon = new Dragon();
            opponentDragon = new Dragon();

            Attack1Enabled = true;
            Attack2Enabled = true;
            Attack3Enabled = true;
            specialAttack = true;

            attack1Command = new DelegateCommand(executeAttack1);
            attack2Command = new DelegateCommand(executeAttack2);
            attack3Command = new DelegateCommand(executeAttack3);
            this.pageDialogService = pageDialogService;
            //  attackStrength = 2;

            PopUpVisuable = true;
            GameVisuable = false;
            SpecialAttackIceVisuable = false;
            int teller = 0;

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                if (teller ==0)
                {
                    teller++;
                }
                else
                {
                    PopUpVisuable = false;
                    GameVisuable = true;
                }
                return true;
            });
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("player"))
            {
                player = parameters.GetValue<Player>("player");
                AreaName = parameters.GetValue<string>("name");
                dragon = player.Dragon;
                Coins = player.Coins;

                AttackStrength = dragon.Damage;
                Armour = dragon.Armour;
                maxPlayerHealth = dragon.Health;
                PlayerHealth = dragon.Health;
                DragonImage = dragon.Image;
                DragonName = dragon.Name;
                PlayerName = player.Name;
            }
            if (parameters.ContainsKey("level"))
            {
                level = parameters.GetValue<int> ("level");
                checkLevel(level);
            }
            else if (parameters.ContainsKey("endgame"))
            {
                level = parameters.GetValue<int>("endgame");

                ComputerImage = "endDragon.gif";
                ComputerHealth = 1100 + level * 150;
                ComputerName = "Herensuge";
                ComputerDamage = 100 + level * 10;
                ComputerArmour = 80 + level * 5;
                Background = "backgroundFire.png";

                opponentDragon.Armour = ComputerArmour;
                opponentDragon.Damage = ComputerDamage;
                opponentDragon.Health = Convert.ToInt32(ComputerHealth);
                maxComputerHealth = Convert.ToInt32(ComputerHealth);
            }
            else
            {
                level = 1;
                checkLevel(level);
            }
        }
        private int button1Pressed;
        public int Button1Pressed
        {
            get { return button1Pressed; }
            set {if (SetProperty(ref button1Pressed, value))
                {
                     if (Button1Pressed <0)
                    {
                        Button1Pressed = 0;
                    }
                }; }
        }
        private int button2Pressed;
        public int Button2Pressed
        {
            get { return button2Pressed; }
            set {if( SetProperty(ref button2Pressed, value))
                     if (Button2Pressed < 0)
                    {
                        Button2Pressed = 0;
                    }; }
        }
        private int button3Pressed;
        public int Button3Pressed
        {
            get { return button3Pressed; }
            set {if (SetProperty(ref button3Pressed, value))
                {
                    if (Button3Pressed < 0)
                    {
                        Button3Pressed = 0;
                    }
                }; }
        }
        private int coins;
        public int Coins
        {
            get { return coins; }
            set {if (SetProperty(ref coins, value))
                {
                    player.Coins = Coins;
                } ; }
        }
        private string dragonName;
        public string DragonName
        {
            get { return dragonName; }
            set { SetProperty(ref dragonName, value); }
        }
        private int attackStrength;
        public int AttackStrength
        {
            get { return attackStrength; }
            set { SetProperty(ref attackStrength, value); }
        }
        private int armour;
        public int Armour
        {
            get { return armour; }
            set {if (SetProperty(ref armour, value)) {
                    dragon.Armour = Armour;
                }; }
        }
        private string dragonImage;
        public string DragonImage
        {
            get { return dragonImage; }
            set { SetProperty(ref dragonImage, value); }
        }
        private string areaName;
        public string AreaName
        {
            get { return areaName; }
            set { SetProperty(ref areaName, value); }
        }

        private bool attack1Enabled;
        public bool Attack1Enabled
        {
            get { return attack1Enabled; }
            set { SetProperty(ref attack1Enabled, value); }
        }
        private bool attack2Enabled;
        public bool Attack2Enabled
        {
            get { return attack2Enabled; }
            set { SetProperty(ref attack2Enabled, value); }
        }
        private bool attack3Enabled;
        public bool Attack3Enabled
        {
            get { return attack3Enabled; }
            set { SetProperty(ref attack3Enabled, value); }
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
            set {if (SetProperty(ref playerHealth, value))
                {
                    PlayerHealthProgress = PlayerHealth / maxPlayerHealth;
                }; }
        }
        private double playerHealthProgress;
        public double PlayerHealthProgress
        {
            get { return playerHealthProgress; }
            set { SetProperty(ref playerHealthProgress, value); }
        }
        private double computerHealth;
        public double ComputerHealth
        {
            get { return computerHealth; }
            set {if (SetProperty(ref computerHealth, value))
                {
                    ComputerHealthProgress = ComputerHealth / maxComputerHealth;
                }
            ; }
        }
        private double computerHealthProgress;
        public double ComputerHealthProgress
        {
            get { return computerHealthProgress; }
            set { SetProperty(ref computerHealthProgress, value); }
        }
        private bool popUpVisuable;
        public bool PopUpVisuable
        {
            get { return popUpVisuable; }
            set { SetProperty(ref popUpVisuable, value); }
        }
        private bool gameVisuable;
        public bool GameVisuable
        {
            get { return gameVisuable; }
            set { SetProperty(ref gameVisuable, value); }
        }
        private int computerDamage;
        public int ComputerDamage
        {
            get { return computerDamage; }
            set { SetProperty(ref computerDamage, value); }
        }
        private int computerArmour;
        public int ComputerArmour
        {
            get { return  computerArmour; }
            set { SetProperty(ref  computerArmour, value); }
        }
        private string computerImage;
        public string ComputerImage
        {
            get { return computerImage; }
            set { SetProperty(ref computerImage, value); }
        }
        private string background;
        public string Background
        {
            get { return background; }
            set { SetProperty(ref background, value); }
        }
        public ICommand attack1Command { get; private set; }
        public ICommand attack2Command { get; private set; }
        public ICommand attack3Command { get; private set; }

        private void checkLevel(int level)
        {
            if (level == 1) //nature dragon
            {
                ComputerImage = "dragon6.gif";
                ComputerHealth = 800;
                ComputerName = "viridisaliquid";
                ComputerDamage = 60;
                ComputerArmour = 10;
                Background = "backgroundNature.png";
                opponentDragon.SpecialAttack = "leaves";
            }
            else if (level == 2)    //fire dragon
            {
                ComputerImage = "dragon1.1.gif";
                ComputerHealth = 1000;
                ComputerName = "Brantley";
                ComputerDamage = 65;
                ComputerArmour = 20;
                Background = "backgroundLava.png";
            }
            else if (level == 3) //ice dragon
            {
                ComputerImage = "dragonIce.gif";
                ComputerHealth = 1100;
                ComputerName = "Frostine";
                ComputerDamage = 100;
                ComputerArmour = 40;
                Background = "backgroundSnow.jpg";
            }
            else if (level == 4) //sand dragon
            {
                ComputerImage = "dragonSand.gif";
                ComputerHealth = 1300;
                ComputerName = "Danbala";
                ComputerDamage = 120;
                ComputerArmour = 40;
                Background = "backgroundSand.jpg";
            }
            else if (level == 5) //money dragon
            {
                ComputerImage = "dragonMoney.gif";
                ComputerHealth = 1300;
                ComputerName = "Moneyline";
                ComputerDamage = 120;
                ComputerArmour = 40;
                Background = "backgroundMoney.jpg";
            }
            else
            {
                Random rnd = new Random();
                int random = rnd.Next(1, 7);
                string dragonstring = "dragon" + Convert.ToString(random)+".1.gif";

                ComputerImage = dragonstring;
                ComputerHealth = 1100 + level * 150;
                ComputerName = "Dragon";
                ComputerDamage = 100 + level* 10;
                ComputerArmour = 100 + level *5;
                Background = "backgroundNature.png";
            }
            opponentDragon.Armour = ComputerArmour;
            opponentDragon.Damage = ComputerDamage;
            opponentDragon.Health = Convert.ToInt32(ComputerHealth);
            maxComputerHealth = Convert.ToInt32(ComputerHealth);
        }
        private void executeAttack1()
        {

            double attack = dragon.Damage - opponentDragon.Armour;
            if (attack < 0)
            {
                LastPlayerAttack = "armour to strong";
            }
            else
            {
                ComputerHealth = ComputerHealth - attack;
                LastPlayerAttack ="-" + Convert.ToString(attack);
            }
            checkPlayerHealth();


            Button1Pressed++;
            Button2Pressed--;
            Button3Pressed--;
        }
        private void executeAttack2()
        {
            Button1Pressed--;
            Button2Pressed++;
            Button3Pressed--;
            Random rnd = new Random();
            int random = rnd.Next(0, 2);
            double attack = 0;
            if (random == 0)
            {
                attack = dragon.Damage +10 - opponentDragon.Armour;
                if (attack < 0)
                {
                    LastPlayerAttack = "armour to strong";
                }
                else
                {
                    LastPlayerAttack = Convert.ToString(attack);
                    ComputerHealth = ComputerHealth - attack;
                }
            }
            else
            {
                LastPlayerAttack = "attack failed";
            }
            checkPlayerHealth();
        }
        private void executeAttack3()
        {
            /*      Button1Pressed--;
                  Button2Pressed--;
                  Button3Pressed++;
                  Random rnd = new Random();
                  int random = rnd.Next(0, 3);
                  double attack = 0;
                  if (random == 0)
                  {
                      attack = dragon.Damage + 20 - opponentDragon.Armour;
                    if (attack < 0)
                    {
                        LastPlayerAttack = "armour to strong";
                    }
                else
                {
                    LastPlayerAttack = Convert.ToString(attack);
                    ComputerHealth = ComputerHealth - attack;
                }
                    }
                  else
                  {
                      LastPlayerAttack = "attack failed";
                  }
                  checkPlayerHealth();

            //    playAnimation("fireGif.gif"); */
            //  playAnimation("leaves");
            
            ComputerHealth = 0;
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
                Attack1Enabled = false;
                Attack2Enabled = false;
                Attack3Enabled = false;
                timer();
            }
        }

        private void computerAttack()
        {
            double attack = 0;
            double Procent = 0.25 * maxComputerHealth;
            if (computerHealth < Procent && specialAttack )
            {
                attack = 0.2 * maxPlayerHealth;
                playAnimation(opponentDragon.SpecialAttack);
                specialAttack = false;
            }
            else
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 3);
                if (random == 0)     //computer doet attack1
                {
                    attack = opponentDragon.Damage - dragon.Armour;
                    LastComputerAttack = Convert.ToString(attack);
                }
                else if (random == 1) //computer doet attack2
                {
                    Random rndAttack = new Random();
                    int randomAttack = rnd.Next(0, 2);

                    if (randomAttack == 0)
                    {
                        attack = opponentDragon.Damage + 10 - dragon.Armour;
                        LastComputerAttack = Convert.ToString(attack);
                    }
                    else
                    {
                        LastComputerAttack = "attack failed";
                    }

                }
                else //computer doet attack 3
                {
                    Random rndAttack = new Random();
                    int randomAttack = rnd.Next(0, 4);
                    if (randomAttack == 0)
                    {
                        attack = opponentDragon.Damage + 20 - dragon.Armour;
                        LastComputerAttack = Convert.ToString(attack);
                    }
                    else
                    {
                        LastComputerAttack = "missed";
                    }

                }
            }
                if (PlayerHealth <= 2)
                {
                    PlayerHealth = PlayerHealth - attack;
                }
                else if (playerHealth <= 4)
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
            //Coins vermenigvuldigen indien winst
            if (who != computerName)
            {
                switch (level)
                {
                    case 1:
                        Coins = Coins + 2;
                        break;
                    case 2:
                        Coins = Coins + 4;
                        break;
                    case 3:
                        Coins = Coins + 3;
                        break;
                    case 4:
                        Coins = Coins + 4;
                        break;
                    case 5:
                        Coins = Coins + 6;
                        break;
                }
            }

            var p = new NavigationParameters();
            p.Add("player", player);
            await pageDialogService.DisplayAlertAsync("Game ended", who + " won the game", "OK");
        //    attack1Enabled = false;
            attack2Enabled = false;
            attack3Enabled = false;
            await NavigationService.GoBackAsync(p);
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
                    Attack1Enabled = true;
                    Attack2Enabled = true;
                    Attack3Enabled = true;

                    if (Button1Pressed == 3)
                    {
                        Attack1Enabled = false;
                    }
                    else if (Button2Pressed == 3)
                    {
                        Attack2Enabled = false;
                    }
                    else if (Button3Pressed == 3)
                    {
                        Attack3Enabled = false;
                    }
                    return false;
                }
            });
        }
        private string animationString;
        public string AnimationString
        {
            get { return animationString; }
            set { SetProperty(ref animationString, value); }
        }
        private bool specialAttackIceVisuable;
        public bool SpecialAttackIceVisuable
        {
            get { return specialAttackIceVisuable; }
            set { SetProperty(ref specialAttackIceVisuable, value); }
        }
        private bool specialAttackLeavesVisuable;
        public bool SpecialAttackLeavesVisuable
        {
            get { return specialAttackLeavesVisuable; }
            set { SetProperty(ref specialAttackLeavesVisuable, value); }
        }

        private async void playAnimation(string specialAttack)
        {
            if (specialAttack == "ice")
            {
                SpecialAttackIceVisuable = true;
                await Task.Delay(TimeSpan.FromSeconds(3));  //wachten tot verdwijnen
                SpecialAttackIceVisuable = false;
            }
            else if (specialAttack == "leaves")
            {
                SpecialAttackLeavesVisuable = true;
                await Task.Delay(TimeSpan.FromSeconds(3));  //wachten tot verdwijnen
                SpecialAttackLeavesVisuable = false;
            }
        }

        
    }
}
