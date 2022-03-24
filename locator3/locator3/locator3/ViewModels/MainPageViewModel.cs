using locator3.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace locator3.ViewModels
{
    
    public class MainPageViewModel : ViewModelBase
    {
        private IGameRepository<Game> GameRepository;

        List<Pointer> pointers = new List<Pointer>();
        int nextStep;
        int pointerNumber = 1;
        string questionMessage;
        private IPageDialogService pageDialogService;

        public IList<Game> games;
        public ObservableCollection<Game> Games { get; private set; }
        public MainPageViewModel(INavigationService navigationService, IGameRepository<Game> gameRepository, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Games = new ObservableCollection<Game>();

            this.pageDialogService = pageDialogService;
            this.GameRepository = gameRepository;

            AddGameVisuable = false;        
            NextButtonVisuable = false;
            PublicVisuable = false;
            ExtraTextVisuable = false;
            YesNoVisuable = false;
            JoinGameVisuable = false;
            StackVisuable = false;
            EndGameVisuable = false;
            CoinsEnabled = true;
            IndicatorVisible = false;

            ButtonAddText = "Add game";
            ButtonJoinText = "Join Game";
            PointerNumberText = "Pointer " + pointerNumber + " options";
            ExtraText = "";
            pickerTypeSelectedIndex = -1;
            IsPublic = false;
            CoinsChecked = false;
            GameLat = 50.928721;
            GameLong = 5.395291;

            NewGameCommand = new DelegateCommand(ExecuteAddGame);
            JoinGameCommand = new DelegateCommand(ExecuteJoinGame);
            JoinGameFileCommand = new DelegateCommand(ExecuteJoinGameFile);
            JoinOptionsCommand = new DelegateCommand(ExecuteJoinOptions);
            AddPointerCommand = new DelegateCommand(ExecuteAddPointer);
            NextCommand = new DelegateCommand(ExecuteNext);
            openMapCommand = new DelegateCommand(ExecuteOpenMap);
            QuestionCommand = new DelegateCommand(ExecuteQuestion);
        }
        public async void setPublicItems()
        {
            try
            {
                Games.Clear();
                var items = await GameRepository.GetItemsAsync(true);
                foreach (var item in items)
                {
                    if (item.isPublic == true)
                    {
                        Games.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            IndicatorVisible = false;
        }
        private bool indicatorVisible;
        public bool IndicatorVisible
        {
            get { return indicatorVisible; }
            set { SetProperty(ref indicatorVisible, value); }
        }
        private Game selectedPublicGame;
        public Game SelectedPublicGame
        {
            get { return selectedPublicGame; }
            set {if (SetProperty(ref selectedPublicGame, value))
                {
                    try
                    {
                        Name = Convert.ToString(SelectedPublicGame.Id);
                    }
                    catch (Exception ex)
                    {

                        Debug.Write(ex);
                    }
                }; }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("lat")) //indien SetLocation gevruikt is
            {
                GameLat = parameters.GetValue<double>("lat");
                gameLong =parameters.GetValue<double>("long");
                Location = $"{gameLat};{gameLong}";
            }
        }
        private bool endGameVisuable;
        public bool EndGameVisuable
        {
            get { return endGameVisuable; }
            set { SetProperty(ref endGameVisuable, value); }
        }
        private int selectedEndGame;
        public int SelectedEndGame
        {
            get { return selectedEndGame; }
            set { SetProperty(ref selectedEndGame, value); }
        }
        private string pointerNumberText;
        public string PointerNumberText
        {
            get { return pointerNumberText; }
            set { SetProperty(ref pointerNumberText, value); }
        }

        private int pickerTypeSelectedIndex;
        public int PickerTypeSelectedIndex
        {
            get { return pickerTypeSelectedIndex; }
            set { if (SetProperty(ref pickerTypeSelectedIndex, value))
                {
                    if (PickerTypeSelectedIndex == 1)
                    {
                        ExtraText = "explenation: ";
                        ExtraTextVisuable = true;
                        YesNoVisuable = false;
                    }
                    else if (PickerTypeSelectedIndex == 2)
                    {
                        ExtraText = "command: ";
                        ExtraTextVisuable = true;
                        YesNoVisuable = false;
                    }
                    else if (PickerTypeSelectedIndex == 4)
                    {
                        ExtraText = "question: ";
                        ExtraTextVisuable = true;
                        YesNoVisuable = true;
                    }
                    else if (PickerTypeSelectedIndex == 5)
                    {
                        ExtraText = "question: ";
                        ExtraTextVisuable = true;
                        YesNoVisuable = false;
                    }
                    else
                    {
                        ExtraTextVisuable = false;
                        YesNoVisuable = false;
                    }
                }
            ; }
        }
        private bool stackVisuable;
        public bool StackVisuable
        {
            get { return stackVisuable; }
            set { SetProperty(ref stackVisuable, value); }
        }
        private bool joinGameVisuable;
        public bool JoinGameVisuable
        {
            get { return joinGameVisuable; }
            set { SetProperty(ref joinGameVisuable, value); }
        }
        private bool addGameVisuable;
        public bool AddGameVisuable
        {
            get { return addGameVisuable; }
            set { SetProperty(ref addGameVisuable, value); }
        }
        private bool gridRow2Visuable;
        public bool GridRow2Visuable
        {
            get { return gridRow2Visuable; }
            set { SetProperty(ref gridRow2Visuable, value); }
        }
        private bool publiVisuable;
        public bool PublicVisuable
        {
            get { return publiVisuable; }
            set { SetProperty(ref publiVisuable, value); }
        }
        private bool yesNoVisuable;
        public bool YesNoVisuable
        {
            get { return yesNoVisuable; }
            set { SetProperty(ref yesNoVisuable, value); }
        }
        private bool radioBtnChecked;
        public bool RadioBtnChecked
        {
            get { return radioBtnChecked; }
            set { SetProperty(ref radioBtnChecked, value); }
        }
        private bool extraTextVisuable;
        public bool ExtraTextVisuable
        {
            get { return extraTextVisuable; }
            set { SetProperty(ref extraTextVisuable, value); }
        }
        private string extraText;
        public string ExtraText
        {
            get { return extraText; }
            set { SetProperty(ref extraText, value); }
        }
        private string extraTextInput;
        public string ExtraTextInput
        {
            get { return extraTextInput; }
            set { SetProperty(ref extraTextInput, value); }
        }
        private string gridRow2Text;
        public string GridRow2Text
        {
            get { return gridRow2Text; }
            set { SetProperty(ref gridRow2Text, value); }
        }
        private bool nextButtonVisuable;
        public bool NextButtonVisuable
        {
            get { return nextButtonVisuable; }
            set { SetProperty(ref nextButtonVisuable, value); }
        }
        private string buttonAddText;
        public string ButtonAddText
        {
            get { return buttonAddText; }
            set { SetProperty(ref buttonAddText, value); }
        }
        private string name;

        private string buttonJoinText;
        public string ButtonJoinText
        {
            get { return buttonJoinText; }
            set { SetProperty(ref buttonJoinText, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string gameName;

        private string location;
        public string Location
        {
            get { return location; }
            set {if (SetProperty(ref location, value))
                {
                    //convertCoordinatesToAdress(Location);
                    convertAdressToCoordinates(location);
                }
            ; }
        }
        private string locationCoords;
        public string LocationCoords
        {
            get { return locationCoords; }
            set { SetProperty(ref locationCoords, value); }
        }
        private double gameLat;
        public double GameLat
        {
            get { return gameLat; }
            set { SetProperty(ref gameLat, value); }
        }
        private double gameLong;
        public double GameLong
        {
            get { return gameLong; }
            set { SetProperty(ref gameLong, value); }
        }
        private bool isPublic;
        public bool IsPublic
        {
            get { return isPublic; }
            set { SetProperty(ref isPublic, value); }
        }
        private bool coinsEanbled;
        public bool CoinsChecked
        {
            get { return coinsEanbled; }
            set { SetProperty(ref coinsEanbled, value); }
        }
        private bool coinsEnabled;
        public bool CoinsEnabled
        {
            get { return coinsEnabled; }
            set { SetProperty(ref coinsEnabled, value); }
        }
        private int pickerPublicSelected;
        public int PickerPublicSelected
        {
            get { return pickerPublicSelected; }
            set {SetProperty(ref pickerPublicSelected, value); }
        }
        public ICommand NewGameCommand { get; private set; }
        public ICommand JoinGameFileCommand { get; private set; }
        public ICommand JoinGameCommand { get; private set; }
        public ICommand JoinOptionsCommand { get; private set; }
        public ICommand AddPointerCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        public ICommand QuestionCommand { get; private set; }

        public void ExecuteAddGame()
        {
            questionMessage = "Add an game an give and play with friends. Add pointers where the where the player should go. Give them an assignment, an explanation or let them battle";
            JoinGameVisuable = false;
            ButtonJoinText = "Join Game";
            if (ButtonAddText == "Add game")
            {
                ButtonAddText = "close";
                GridRow2Text = "Name: ";
                StackVisuable = true;
                GridRow2Visuable = true;
                PublicVisuable = false;
                NextButtonVisuable = true;
                pointerNumber = 1;
                PointerNumberText = "Pointer " + pointerNumber + " options";
                nextStep = 0;
            }
            else
            {
                ButtonAddText = "Add game";
                StackVisuable = false;
                GridRow2Visuable = false;
                NextButtonVisuable = false;
                AddGameVisuable = false;
                PublicVisuable = false;
                EndGameVisuable = false;
                PickerTypeSelectedIndex = -1;

            }
        }
        async void ExecuteJoinGame()
        {
            var p = new NavigationParameters();

            if (name != null) //id opzoeken
            {
                try
                {
                   var item=  await GameRepository.GetItemByIdAsync(Convert.ToInt32(name));
                    if (item == null)
                    {
                        await pageDialogService.DisplayAlertAsync("ERROR", "Id not found", "OK");
                    }
                    else
                    {
                        IndicatorVisible = true;
                        ButtonJoinText = "Join Game";
                        StackVisuable = false;
                        p.Add("game", item);
                        await NavigationService.NavigateAsync("mapsPage", p);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
            }
            else
            {
              await pageDialogService.DisplayAlertAsync("Error", "error happend try again", "OK");
            }
            //gegevens ophalen uit database
        }
        public async void ExecuteJoinGameFile()
        {
            pointers.Clear();
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    StreamReader reader = new StreamReader(result.FileName);
                    string line = reader.ReadLine();
                    string[] words;
                    char[] separators = { ';' };
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        Pointer pointer = new Pointer();
                        words = line.Split(separators);
                        pointer.Name = words[0];
                        pointer.Latitude = Convert.ToDouble(words[1]);
                        pointer.Longitude = Convert.ToDouble(words[2]);
                        pointer.type = words[3];
                        pointer.text = words[4];

                        pointers.Add(pointer);
                    }
                }
            }
            catch
            {
                await pageDialogService.DisplayAlertAsync("Error", "error happend try again", "OK");
            }
        }
        public void ExecuteJoinOptions()
        {
            questionMessage = "fill in an game id or select an public game to enter";
            ButtonAddText = "Add game";
            Name = "";
            GridRow2Text = "ID: ";
            AddGameVisuable = false;
            ExtraTextVisuable = false;
            NextButtonVisuable = false;
            PublicVisuable = false;
            NextButtonVisuable = false;

            pickerTypeSelectedIndex = -1;
            if (ButtonJoinText == "Join Game")
            {
                ButtonJoinText = "close";
                StackVisuable = true;
                JoinGameVisuable = true;
                GridRow2Visuable = true;
                SelectedPublicGame = null;
                setPublicItems();
            }
            else
            {
                ButtonJoinText = "Join Game";
                StackVisuable = false;
                JoinGameVisuable = false;
                GridRow2Visuable = false;
            }
        }
        public void ExecuteAddPointer()
        {
            try
            {
            Application.Current.MainPage.DisplayAlert("Pointer ADD", "Pointer " +Name + " add", "ok");
            Pointer pointer = new Pointer();
            pointer.Name = Name;
            pointer.Latitude = GameLat;
            pointer.Longitude = GameLong;
            pointer.type = selectedIndexName(pickerTypeSelectedIndex);
                string extraText = ExtraTextInput;
                if (selectedIndexName(pickerTypeSelectedIndex) == "yesNo")
                {
                    extraText = extraText + ";" + RadioBtnChecked; ;
                }
                pageDialogService.DisplayAlertAsync("ok", extraText, "ok");
            pointer.text = extraText;
            pointers.Add(pointer);
                
            Name = null;
            pointerNumber = pointerNumber + 1;
            PointerNumberText = "Pointer " + pointerNumber + " options";
            GameLat = 0;
            GameLong = 0;
            Location = "";
            ExtraText = "";
            ExtraTextInput = "";
            PickerTypeSelectedIndex = -1;
            RadioBtnChecked = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string selectedIndexName(int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return "goTo";
            }
            else if (selectedIndex == 1)
            {
                return "explanation";
            }
            else if (selectedIndex == 2)
            {
                return "command";
            }
            else if (selectedIndex == 3)
            {
                return "battle";
            }
            else if (selectedIndex == 4)
            {
                return "yesNo";
            }
            else if (selectedIndex ==5)
            {
                return "question";
            }
            else
            {
                return "Null";
            }
        }
        async void ExecuteNext()
        {
             if (nextStep == 0)
            {
                //GridRow2Visuable = false;
                GridRow2Text = "name of pointer: ";
                AddGameVisuable = true;
                nextStep = 1;
                gameName = Name;
                Name = "";
                questionMessage = "add an pointer for the player. Give the name of the pointer, lat-long coards or an adres and select an type. ";
            }
            else if (nextStep ==1)
            {
                var result = await pageDialogService.DisplayAlertAsync("Alert", "Are all pointers add?", "Yes", "No");
                if (result)
                {
                    nextStep = 2;
                    AddGameVisuable = false;
                    GridRow2Visuable = false;
                    PublicVisuable = false; //kijken of die standaard aan staat 
                    checkCoins();
                    EndGameVisuable = true;
                    PublicVisuable = true;
                    questionMessage = "Select an andgame";
                }
            }
            else if (nextStep == 2)
            {
                StackVisuable = false;
                EndGameVisuable = false;
                NextButtonVisuable = false;
                ButtonAddText = "Add game";
                /*
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    StreamWriter writer = File.CreateText(result.FileName);
                writer.WriteLine($"{Name};{game.ID}");
                writer.Close();
                }
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var filename = Path.Combine(path, $"{game.name}.txt");
                StreamWriter writer = File.CreateText(filename);
                writer.WriteLine($"{game.Id};{gameName};{game.isPublic};{game.endType};");
                foreach (Pointer pointer in pointers)
                {
                    writer.WriteLine($"{pointer.Name};{pointer.Latitude};{pointer.Longitude};{pointer.type};{pointer.text};");
                }
                writer.Close();
                var result = await pageDialogService.DisplayAlertAsync("Share", "Would you like to share game data", "Yes","No");
                if (result)
                {
                    var fn = "Attachment.txt";
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllText(file, "Hello World");

                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "Iets?",
                        File = new ShareFile(filename)
                    }) ;
                }*/
                int id = 25;
                Game newGame = new Game()
                {
                    name = gameName,
                    endType = convertEndGame(SelectedEndGame),
                    isPublic = IsPublic,
                    Pointers = pointers,
                    Id = id
                };
                await GameRepository.AddItemAsync(newGame);
                await pageDialogService.DisplayAlertAsync("ID", "Game add, you'r id is: " + id, "Ok");
            }
        }
        private void checkCoins()
        {
            foreach (Pointer item in pointers)
            {
                if (item.type == "battle")
                {
                    CoinsChecked = true;
                    CoinsEnabled = false;
                }
            }
        }
        public async void convertAdressToCoordinates(string location)
        {
            if (location != "")
            {
                    //convertCoordinatesToAdress(location);
                    string text = "";

                    var result = await Geocoding.GetLocationsAsync(location);
                    if (result.Any())
                    {
                        text = $" {result.FirstOrDefault()?.Latitude};{result.FirstOrDefault()?.Longitude}";
                        GameLat = result.FirstOrDefault().Latitude;
                        GameLong = result.FirstOrDefault().Longitude;
                    }
                    LocationCoords = text;
            }
        }
        public string convertEndGame(int selectedIndex)
        {
            if (selectedIndex == 1) return "battle";
            else return "none";
        }
        public async void convertCoordinatesToAdress(string locationInput)
        {
            try
            {
                string text = "";
                double lat = Convert.ToDouble(locationInput.Split(';')[0]);
                double lng = Convert.ToDouble(locationInput.Split(';')[1]);
                var result2 = await Geocoding.GetPlacemarksAsync(lat, lng);
                if (result2.Any())
                {
                    text = $" {result2.FirstOrDefault()?.Location}";
                }
                LocationCoords = text;
            }
            catch(Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("ex" , ex.ToString() , "OK");
            }
        }
        public ICommand openMapCommand { get; private set; }

        private void ExecuteOpenMap()
        {
            NavigationService.NavigateAsync(nameof(SetLocationPage));
        }
        private void ExecuteQuestion()
        {
            pageDialogService.DisplayAlertAsync("?", questionMessage, "OK");
        }
    }
}
