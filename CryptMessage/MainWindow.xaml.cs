using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Newtonsoft.Json;

namespace CryptMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// thinking of using the dragEnter and Drop properties to prevent screenshots, more info later
    /// may also use lostFocus

    class User
    {
        public string username;
        public string password;

        public User(string u, string p)
        {
            username = u;
            password = p;
        }
        public override string ToString()
        {
            return "Username: " + username + "\n Password: " + password;
        }

    }

    public partial class MainWindow : Window
    {
        static readonly HttpClient client = new HttpClient();
        Uri server = new Uri("https://crypt-message.herokuapp.com/");
        private const string url = "https://crypt-message.herokuapp.com/";
        bool home = true;//this var and others similar will hopefully be used to turn on and off elements belonging to different pages. 
        string page = "";
        System.Windows.Visibility visible = Visibility.Visible;
        System.Windows.Visibility invisible = Visibility.Hidden;

        private static async Task<char> Main()
        {

            try
            {
                string responseBody = await client.GetStringAsync("https://crypt-message.herokuapp.com/");
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!\nMessage: {0}", e.Message);
            }
            return 'a';
        }
        public MainWindow()
        {
            client.BaseAddress = server;
            InitializeComponent();
            page = "login";
            updatePages(page);
        }
        private void updatePages(String page)
        {
            bool menuVis;
            if (page == "login"){ 
                menuVis = false; 
            } else { menuVis = true; }
            SettingsMnu.IsEnabled = menuVis;
            HomeMnu.IsEnabled = menuVis;
            ConversationsMnu.IsEnabled = menuVis;

            homeVis(page);
            aboutVis(page);
            convoManageVis(page);
            settingsVis(page);
            loginVis(page);
        }

        //page switching functions
        private void SettingsMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "settings";
            updatePages(page);
        }
        private void HomeMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "home";
            updatePages(page);
        }
        private void ConversationsMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "conversations";
            updatePages(page);
        }
        private void AboutMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "about";
            updatePages(page);
        }
        
        private void homeVis(String page)
        {//set the visibilty for each page in its own function, run if statement on the page name, adjust visibility accordingly.
            System.Windows.Visibility visState;
            if (page == "home")
            {
                visState = visible;
            }
            else { visState = invisible; }
            ConvoList.Visibility = visState;
            MsgTxtBox.Visibility = visState;
            MsgSendBtn.Visibility = visState;
            UsernameDisplayLbl.Visibility = visState;
            SendToLbl.Visibility = visState;
            SendToTxt.Visibility = visState;
        }
        private void aboutVis(String page)
        {
            System.Windows.Visibility visState;
            if (page == "about")
            {
                visState = visible;
            }
            else { visState = invisible; }
            About1.Visibility = visState;
        }
        private void convoManageVis(String page)
        {
            System.Windows.Visibility visState;
            if (page == "conversations")
            {
                visState = visible;
            }
            else { visState = invisible; }
            AddConvoBtn.Visibility = visState;
            NewConvoCreateBtn.Visibility = visState;
            DelConvoBtn.Visibility = visState;
        }
        private void settingsVis(String page) {
            System.Windows.Visibility visState;
            if (page == "settings")
            {
                visState = visible;
            }
            else { visState = invisible; }
        }
        private void newConvoVis(object sender, RoutedEventArgs e)
        {
            System.Windows.Visibility visState = NewConvoTitleLbl.Visibility;
            if (visState == invisible)
            {
                visState = visible;
            }
            else if (visState == visible)
            {
                visState = invisible;
            }
            NewConvoTitleLbl.Visibility = visState;
            NewConvoTitleTxtBox.Visibility = visState;
            NewRecipientLbl.Visibility = visState;
            NewRecipientTxtBox.Visibility = visState;
        }
        private void loginVis(string page)
        {
            System.Windows.Visibility visState;
            if (page == "login")
            {
                visState = visible;
            }
            else { visState = invisible; }
            usernameLbl.Visibility = visState;
            usernameTxtBox.Visibility = visState;
            loginPasswordLbl.Visibility = visState;
            loginPassBox.Visibility = visState;
            LoginBtn.Visibility = visState;
        }

        //window privacy functions
        private void Window_DragEnter(object sender, DragEventArgs e)//prevent copy paste
        {
            var pageView = "blank";
            updatePages(pageView);
        }
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            var pageView = "blank";
            updatePages(pageView);
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            var pageView = "blank";
            updatePages(pageView);
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)
                || Keyboard.IsKeyDown(Key.LWin) || Keyboard.IsKeyDown(Key.RWin)
                || Keyboard.IsKeyDown(Key.Print) || Keyboard.IsKeyDown(Key.PrintScreen)
                || Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                var pageView = "blank";
                updatePages(pageView);
            }
        }
        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            
            if (Keyboard.IsKeyUp(Key.LeftCtrl) || Keyboard.IsKeyUp(Key.RightCtrl)
                || Keyboard.IsKeyUp(Key.LWin) || Keyboard.IsKeyUp(Key.RWin)
                || Keyboard.IsKeyUp(Key.Print) || Keyboard.IsKeyUp(Key.PrintScreen)
                || Keyboard.IsKeyUp(Key.LeftAlt) || Keyboard.IsKeyUp(Key.RightAlt))
            {
                updatePages(page);
            }
        }
        private void Window_MouseEnter(object sender, MouseEventArgs e){ updatePages(page); }
        private void Window_Activated(object sender, EventArgs e) { updatePages(page); }
        private void Window_Drop(object sender, DragEventArgs e) { updatePages(page); }

        //UI control functions
        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxtBox.Text;
            string password = loginPassBox.Password.ToString();
            var user = new User(username, password);

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var res = await client.PostAsync(url + "login", data);
            Console.WriteLine(user.ToString());

            var body = res.Content.ReadAsStringAsync().Result;
            debugLabel.Content = body.ToString();
            Console.WriteLine(body.ToString());

            bool auth;


            if (body.Equals("Yes"))
            {
                //setting visibiilty status to invisible for login status
                LblLoginStatus.Visibility = invisible;
                Console.WriteLine("User authenticated");
                page = "home";
                updatePages(page);
                UsernameDisplayLbl.Content=usernameTxtBox.Text;
            }
            else if (body.Equals("No"))
            {
                LblLoginStatus.Content = "Incorrect Password!";
            }
            else if(body.ToString() == "No user found!")
            {
                LblLoginStatus.Content = "No User Found!";
            }
            else
            {
                LblLoginStatus.Content = "Server Error!";
            }
            
           
 
        }
        private void MsgSendBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var values = new Dictionary<string, string>
            {
                { "senUsername",UsernameDisplayLbl.Content.ToString()},
                { "recUsername",SendToTxt.Text},
                { "message",MsgTxtBox.Text},
                { "dateEntered",""}
            };
            var json =JsonConvert.SerializeObject(values, Newtonsoft.Json.Formatting.Indented);
            var messageDetails = new StringContent(json);
            
            client.PostAsync(UsernameDisplayLbl.Content.ToString()+","+ SendToTxt.Text+","+ MsgTxtBox.Text, messageDetails);
        }

        //private void checkForMsg()
        //{
        //    string msg = null;
        //    Dictionary<string,string>inMsg=
        //    var result=JsonConvert.DeserializeObject()
        //}
    }
}
