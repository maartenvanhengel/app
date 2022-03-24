﻿using locator3.Views;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace locator3.ViewModels
{
    class mapsPageViewModel :ViewModelBase
    {
        List<Pointer> pointers = new List<Pointer>();
        Game game;
        int level;
        bool timerEnabled = true;
        private IPageDialogService pageDialogService;
        public mapsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Locations = new ObservableCollection<Location>();
            this.pageDialogService = pageDialogService;


            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filename = Path.Combine(path, "playerData.txt");
            File.Delete(filename);
            StreamWriter writer;
            writer = File.CreateText(filename);
            writer.WriteLine($"0;");
            writer.WriteLine($"0;0;5;");
            writer.Close();

            BtnInsideVisuable = false;
            PopUpVisuable = false;
            UpgradeButtonVisuable = false;
            PopUpAnswer = "";
            PopUpText = "";
            PopUpTitle = "";
            Coins = 0;
            level = 1;
            game = new Game();



            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (timerEnabled == true)
                {
                    checkLocation();
                    bool gameEnded = true;
                    foreach (Pointer item in pointers)
                    {
                        if (item.isEanabled == true)
                        {
                            gameEnded = false;
                        }
                    }
                    if (gameEnded == true)
                    {
                        timerEnabled = false;
                        EndGame();
                    }
                }
                var current = Connectivity.NetworkAccess;

                if (current != NetworkAccess.Internet)
                {
                    NoInternetVisuable = true;
                }
                else 
                { 
                    NoInternetVisuable = false;
                }

                return true;// return true to repeat counting, false to stop timer
            });

            cancelPopUpCommand = new DelegateCommand(executeCancelPopUp);
            okPopUpCommand = new DelegateCommand(executeOkPopUp);
            showUpgradeCommand = new DelegateCommand(executeShowUpgrade);

        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            List<Location> locationsList = new List<Location>();
            if (parameters.ContainsKey("game"))
            {
                this.game = parameters.GetValue<Game>("game");
                this.pointers = game.Pointers;
                addPointers(game.Pointers);

                Title = game.name;
                ChechIfBattle("battle"); //kijken of er een battle in zit en knoppen getoond moeten worden
                CoinsVisuable = game.coinsEanabled;
                await Task.Delay(TimeSpan.FromSeconds(5));  //zeker zijn dat alles geladen is
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            timerEnabled = false;
        }
        public void addPointers(List<Pointer> pointerss)
        {
            Locations.Clear();
            for (int i = 0; i < pointerss.Count; i++)
            {
                if (pointerss[i].isEanabled  )
                {
                    Locations.Add(new Location() { Position = new Position(pointerss[i].Latitude, pointerss[i].Longitude), Address = "", Description = pointerss[i].Name });
                }
            }
        }
        public ObservableCollection<Location> Locations { get; private set; }
        private bool noInternetVisuable;
        public bool NoInternetVisuable
        {
            get { return noInternetVisuable; }
            set { SetProperty(ref noInternetVisuable, value); }
        }
        private bool upgradeButtonVisuable;
        public bool UpgradeButtonVisuable
        {
            get { return upgradeButtonVisuable; }
            set { SetProperty(ref upgradeButtonVisuable, value); }
        }
        private string gameName;
        public string GameName
        {
            get { return gameName; }
            set { SetProperty(ref gameName, value); }
        }
        private bool coinsVisuable;
        public bool CoinsVisuable
        {
            get { return coinsVisuable; }
            set { SetProperty(ref coinsVisuable, value); }
        }
        private int coins;
        public int Coins
        {
            get { return coins; }
            set { SetProperty(ref coins, value); }
        }
        private bool popUpVisuable;
        public bool PopUpVisuable
        {
            get { return popUpVisuable; }
            set { SetProperty(ref popUpVisuable, value); }
        }
        public ICommand cancelPopUpCommand { get; private set; }
        public ICommand okPopUpCommand { get; private set; }
        public ICommand showUpgradeCommand { get; private set; }
        public async void executeShowUpgrade()
        {
            await NavigationService.NavigateAsync(nameof(UpgradePage));
        }
        public void executeCancelPopUp()
        {
            PopUpVisuable = false;
            PopUpAnswer = null;
            timerEnabled = true;
        }
        public void executeOkPopUp()
        {
            try
            {
                Coins = Coins + Convert.ToInt32(popUpAnswer);
                timerEnabled = true; //timer aan als op waarde is geklikt
                executeCancelPopUp();
            }
            catch (Exception)
            {
                pageDialogService.DisplayAlertAsync("Error", "wrong value", "OK");
            }
        }

        private bool btnInsideVisuable;
        public bool BtnInsideVisuable
        {
            get { return btnInsideVisuable; }
            set { SetProperty(ref btnInsideVisuable, value); }
        }
        private string popUpTitle;
        public string PopUpTitle
        {
            get { return popUpTitle; }
            set { SetProperty(ref popUpTitle, value); }
        }
        private string popUpText;
        public string PopUpText
        {
            get { return popUpText; }
            set { SetProperty(ref popUpText, value); }
        }
        private string popUpAnswer;
        public string PopUpAnswer
        {
            get { return popUpAnswer; }
            set { SetProperty(ref popUpAnswer, value); }
        }

        public void setMarkers()
        {
            foreach (Pointer pointer in pointers)
            {
                Pin marker = new Pin
                {
                    Type = PinType.Generic,
                    Label = pointer.Name,
                    Position = new Position(pointer.Longitude, pointer.Latitude)
                };
            }
        }
        public async void checkLocation() //kijken of Player in zone is
        {
            var locator = CrossGeolocator.Current;      //huidige locatie zoeken
            var position = await locator.GetPositionAsync(System.TimeSpan.FromSeconds(1));
            foreach (Pointer pointer in pointers)
            {
                if (position.Latitude <= pointer.Latitude +0.0004 && position.Latitude >= pointer.Latitude -0.004 && position.Longitude <= pointer.Longitude +0.0004 && position.Longitude >= pointer.Longitude -0.0004 && pointer.isEanabled ==true)
                {
                    timerEnabled = false;
                    if (pointer.type == "explanation")
                    {
                        await pageDialogService.DisplayAlertAsync("Alert", pointer.text,"OK" );
                        timerEnabled = true;
                        //timer aanzetten na klik op ok
                    }
                    else if (pointer.type == "command")
                    {
                        PopUpTitle = pointer.Name;
                        PopUpText = pointer.text;
                        PopUpVisuable = true;
                        //timer aan als op waarde is geklikt
                    }
                    else if (pointer.type =="goTo")
                    {
                        //BtnInsideVisuable = true;
                        timerEnabled = true; //kan aanblijven: enkel naartoe gaan
                    }
                    else if (pointer.type == "battle")
                    {
                        await pageDialogService.DisplayAlertAsync("Point", "you are invited to a battle", "OK");
                        level++;
                        var p = new NavigationParameters();
                        p.Add("level", level);
                        await NavigationService.NavigateAsync(nameof(BattlePage), p, true, false);
                        //timer uit als battle voltooid is
                    }
                    else if (pointer.type == "yesNo")
                    {
                        char[] separators = { ';' };
                        string[] words = pointer.text.Split(separators);
                        var result = await pageDialogService.DisplayAlertAsync("Question", words[0], "Yes", "No");
                        if (words[1] == "True" && result || words[1] == "False" && result == false)
                        {
                            await pageDialogService.DisplayAlertAsync("Right", "right answer", "ok");
                            Coins = Coins + 1;
                        }
                        else await pageDialogService.DisplayAlertAsync("False", "false answer", "ok");
                        timerEnabled = true; //aan na beantwoorden vraag
                    }
                    pointer.isEanabled = false;
                    addPointers(pointers);
                }
            }
            
        }
        public async void EndGame()
        {
            await Application.Current.MainPage.DisplayAlert("done", "game ended", "ok");
            await NavigationService.NavigateAsync(nameof(MainPage));
        }
        public void ChechIfBattle(string type)
        {
            foreach (Pointer item in pointers) //kijken type van 1 pointer van het type is
            {
                if (item.type == type)
                {
                    UpgradeButtonVisuable = true;
                }
            }
            if (game.endType == type)   //kijken of eindtype battle is
            {
                upgradeButtonVisuable = true;
            }
        }
        
    }
}