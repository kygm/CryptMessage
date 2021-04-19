using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    class Message
    {
        public string senUsername,recUsername,message;
        public DateTime dateEntered;
        public Message(string sender, string reciever, string mess,DateTime entered)
        {
            senUsername = sender;
            recUsername = reciever;
            message = mess;
            dateEntered =entered;
        }
        public override string ToString()
        {
            return "From: "+senUsername+"\n To: "+recUsername+"\n Message: "+message;
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
        public MainWindow()
        {
            client.BaseAddress = server;
            InitializeComponent();
            page = "login";
            updatePages(page);
        }


        #region Page Functions       
        private void updatePages(String page)
        {
            bool menuVis;
            if (page == "login")
            {
                menuVis = false;
            }
            else { menuVis = true; }
            SettingsMnu.IsEnabled = menuVis;
            HomeMnu.IsEnabled = menuVis;
            ConversationsMnu.IsEnabled = menuVis;

            homeVis(page);
            aboutVis(page);
            convoManageVis(page);
            settingsVis(page);
            loginVis(page);
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
                debugLbl.Visibility = visState;
                LoginStatusLbl.Visibility = visState;
            }
        #endregion

        #region Window Privacy Functions
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
        #endregion

        //UI control functions
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
        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxtBox.Text;
            string password = loginPassBox.Password.ToString();
            User user = new User(username, password);

            string json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var res = await client.PostAsync(url + "login", data);
            Console.WriteLine(user.ToString());

            string body = res.Content.ReadAsStringAsync().Result;
            debugLbl.Content = body.ToString();
            Console.WriteLine(body.ToString());

            Console.WriteLine(body);

            bool auth;


            if (body.Equals("\"Yes\""))
            {
                //setting visibiilty status to invisible for login status
                LoginStatusLbl.Visibility = invisible;
                Console.WriteLine("User authenticated");
                page = "home";
                updatePages(page);
                UsernameDisplayLbl.Content=usernameTxtBox.Text;
                checkMsg();
            }
            else if (body.Equals("No"))
            {
                LoginStatusLbl.Content = "Incorrect Password!";
            }
            else if(body.ToString() == "No user found!")
            {
                LoginStatusLbl.Content = "No User Found!";
            }
            else
            {
                LoginStatusLbl.Content = "Server Error!";
            }
            
        }
        private async void MsgSendBtn_Click(object sender, RoutedEventArgs e)
        {
            string sen = UsernameDisplayLbl.Content.ToString(), 
                rec = SendToTxt.Text, 
                mes = MsgTxtBox.Text;
            Message msg = new Message(sen, rec, mes, DateTime.Now);
            string json = JsonConvert.SerializeObject(msg);
            var msgData = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url + "messages", msgData);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if (body.Contains("senUsername"))
            {
                Console.WriteLine("Message Sent");
            }
            else 
            {
                Console.WriteLine("Message Failed");
            }
        }
        private Timer timer1;
        public void checkMsg()
        {
            timer1 = new Timer();
            timer1.Elapsed+= new ElapsedEventHandler(getMsg);
            timer1.Interval = 5000;
            timer1.Start();
        }
        private async void getMsg(object sender, EventArgs e)
        {
            //string userName = UsernameDisplayLbl.Content.ToString();
            var res = await client.GetAsync(url + "messages");
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if (body.Contains("\"recUsername\":\"admin\""))
            {
                Console.WriteLine("Message Recieved");
            }
            
        }
    }
}
