using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
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
using System.Windows.Threading;
using System.Xml;
using Newtonsoft.Json;
using System.Dynamic;
using System.Globalization;

namespace CryptMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// thinking of using the dragEnter and Drop properties to prevent screenshots, more info later
    /// may also use lostFocus
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value is string str && str.All(ch =>
            ch < 155 || (ch > 159 && ch < 166) || (ch > 180 && ch < 185) || (ch > 197 && ch < 200) ||
            (ch > 208 && ch < 217) || ch == 222 || (ch > 223 && ch < 240))
                ? ValidationResult.ValidResult
                : new ValidationResult(false, "The name contains illegal characters");
        }
    }
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
    class usr
    {
        public string username;
        public usr(string u)
        {
            username = u;
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
    class RecMsg
    {
        public string msgId, senUsername, recUsername, message;
        public DateTime dateEntered;
        public RecMsg(string id,string sender, string reciever, string mess, DateTime entered)
        {
            msgId = id;
            senUsername = sender;
            recUsername = reciever;
            message = mess;
            dateEntered = entered;
        }
    }
    class SenMsg
    {
        public string msgId, senUsername, recUsername, message;
        public DateTime dateEntered;
        public SenMsg(string id, string sender, string reciever, string mess, DateTime entered)
        {
            msgId = id;
            senUsername = sender;
            recUsername = reciever;
            message = mess;
            dateEntered = entered;
        }
    }
    class newUser
    {
        public string username, email, password;
        public newUser(string u,string e,string p)
        {
            username = u;
            email = e;
            password = p;
        }
    }
    class recievedMessages
    {
        public List<RecMsg> recieved = new List<RecMsg>(); 
        public recievedMessages(List<RecMsg> rec)
        {
            recieved = rec;
        }
    }
    class sentMessages
    {
        public List<SenMsg> sent = new List<SenMsg>();
        public sentMessages(List<SenMsg> sen)
        {
            sent = sen;
        }
    }
    class Conversation
    {
        public List<Message> convo = new List<Message>();
    }
    class friendRequest
    {
        public string senUsername, recUsername;
        public bool status;
        public DateTime date;
        public friendRequest(string sen, string rec)
        {
            senUsername = sen;
            recUsername = rec;
            date = DateTime.Now;
        }
        public friendRequest(string sen,string rec, bool stat)
        {
            senUsername = sen;
            recUsername = rec;
            status = stat;
            date = DateTime.Now;
        }
    }
    class friendRequestList
    {
        public List<friendRequest> requests = new List<friendRequest>();
        public friendRequestList(List<friendRequest> req)
        {
            requests = req;
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
        User curUser = new User(null,null);
        string theUser;
        int ticks = 0;
        Timer timer2 = new Timer();
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
            if (page == "login"||page=="newUser")
            {
                menuVis = false;
                AboutMnu.Visibility = Visibility.Hidden;
            }
            else { menuVis = true; AboutMnu.Visibility = Visibility.Visible; }
            if (page == "newUser")
            {
                timer2.Elapsed += new ElapsedEventHandler(setCreateUserBtn);
                timer2.Interval = 100;
                timer2.Start();
            }
            SettingsMnu.IsEnabled = menuVis;
            HomeMnu.IsEnabled = menuVis;
            FriendsMnu.IsEnabled = menuVis;

            homeVis(page);
            aboutVis(page);
            friendManageVis(page);
            settingsVis(page);
            loginVis(page); 
            newUserVis(page);
        }
        private void homeVis(String page)
            {//set the visibilty for each page in its own function, run if statement on the page name, adjust visibility accordingly.
                System.Windows.Visibility visState;
                if (page == "home")
                {
                    visState = visible;
                }
                else { visState = invisible; }
                if (page == "home" || page == "friends")
                {
                    ConvoList.Visibility = visible;
                    listLbl.Visibility = visible;
                }
                else 
                {
                    ConvoList.Visibility = invisible;
                    listLbl.Visibility = invisible;
                }
                MsgTxtBox.Visibility = visState;
                MsgSendBtn.Visibility = visState;
                UsernameDisplayLbl.Visibility = visState;
                SendToLbl.Visibility = visState;
                SendToTxt.Visibility = visState;
                user1Lbl.Visibility = visState;
                user2Lbl.Visibility = visState;
                user3Lbl.Visibility = visState;
                msg1Lbl.Visibility = visState;
                msg2Lbl.Visibility = visState;
                msg3Lbl.Visibility = visState;
                newFriendRequestLbl.Visibility = visState;
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
            private void friendManageVis(String page)
            {
                System.Windows.Visibility visState;
                if (page == "friends")
                {
                    visState = visible;
                }
                else { visState = invisible; }
                friendRequestList.Visibility = visState;
                NewFriendLbl.Visibility = visState;
                NewFriendTxtBox.Visibility = visState;
                sendFriendRequestBtn.Visibility = visState;
            }
            private void settingsVis(String page) {
                System.Windows.Visibility visState;
                if (page == "settings")
                {
                    visState = visible;
                }
                else { visState = invisible; }
            }
        private void delFriendVis(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if ((ConvoList.SelectedItem.ToString() != " "
                && ConvoList.SelectedItem.ToString() != null)&&page=="friends")
                {
                    DelFriendBtn.Visibility = visible;
                } else { DelFriendBtn.Visibility = invisible; }
            }));
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
                NewUserBtn.Visibility = visState;
            }
        private void newUserVis(string page)
        {
            System.Windows.Visibility visState;
            if (page == "newUser")
            {
                visState = visible;
            }
            else { visState = invisible; }
            newUsernameLbl.Visibility = visState;
            newUsernameTxtBox.Visibility = visState;
            newPasswordLbl.Visibility = visState;
            newPassBox.Visibility = visState;
            repeatNewPasswordLbl.Visibility = visState;
            repeatNewPassBox.Visibility = visState;
            createUserMessageLbl.Visibility = visState;
            newEmailLbl.Visibility = visState;
            newEmailTxtBox.Visibility = visState;
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

        #region UI Controls
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
        private void FriendsMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "friends";
            updatePages(page);
        }
        private void AboutMnu_Click(object sender, RoutedEventArgs e)
        {
            page = "about";
            updatePages(page);
        }
        private void NewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            page = "newUser";
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
                curUser.username = usernameTxtBox.Text;
                theUser = curUser.username;
                UsernameDisplayLbl.Content=theUser;
                getFriends();
                ConvoList.SelectedItem = " ";
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
        public async void MsgSendBtn_Click(object sender, RoutedEventArgs e)
        {
            string sen = theUser, 
                rec = SendToTxt.Text, 
                mes = MsgTxtBox.Text;
            Message msg = new Message(sen, rec, mes, DateTime.Now);
            string json = JsonConvert.SerializeObject(msg);
            Console.WriteLine(json.ToString());
            var msgData = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url + "messages", msgData);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if (body.Contains("senUsername"))
            {
                Console.WriteLine("Message Sent");
                MsgTxtBox.Text = "";
            }
            else 
            {
                Console.WriteLine("Message Failed");
            }
        }
        private async void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            newUser newUser = new newUser(newUsernameTxtBox.Text,newEmailTxtBox.Text, newPassBox.Password);
            string json = JsonConvert.SerializeObject(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var rec = await client.PostAsync(url + "createUser", data);
            var res = rec.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
            createUserMessageLbl.Content = res;
            if(res== "\"User created\"")
            {
                timer2.Stop();
                page = "login"; 
                newUsernameTxtBox.Text = "";
                newEmailTxtBox.Text = "";
                newPassBox.Password = "";
                repeatNewPassBox.Password = "";
                createUserMessageLbl.Content = "";
                CreateUserBtn.Visibility = invisible;
                updatePages(page);
                LoginStatusLbl.Content = "User created. You may login.";
            }
        }
        private async void sendFriendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            string sen = theUser,
                rec = NewFriendTxtBox.Text;
            friendRequest req = new friendRequest(sen, rec);
            string json = JsonConvert.SerializeObject(req);
            Console.WriteLine(json.ToString());
            var reqData = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url + "sendFriendRequest", reqData);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
        }

        #endregion

        #region Get/Send/Passive functions
        private void repeatNewPassBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (repeatNewPassBox.Password != newPassBox.Password)
            {
                createUserMessageLbl.Content = "Passwords must match!";
            }
            else
            {
                createUserMessageLbl.Content = "";
            }
        }
        private void newPassBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (repeatNewPassBox.Password != newPassBox.Password)
            {
                createUserMessageLbl.Content = "Passwords must match!";
            }
            else
            {
                createUserMessageLbl.Content = "";
            }
        }
        public async void getFriends()
        {
            usr userName = new usr(theUser);
            string json = JsonConvert.SerializeObject(userName);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var rec = await client.PostAsync(url + "getFriends", data);
            var res = rec.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
            Dispatcher.Invoke(new Action(() =>
            {
                ConvoList.Items.Clear();
                ConvoList.Items.Add(" ");
                ConvoList.SelectedItem = " ";
            }));
            if (res.Contains(theUser))
            {
                res = res.Replace("\"", "");
                res = res.Replace("_id:", "");
                res = res.Replace("accepted:", "");
                res = res.Replace("firstUsername:", "");
                res = res.Replace("secondUsername:", "");
                res = res.Replace("dateEntered:", "");
                var friendList = res.Split(',');
                Console.WriteLine(res);
                int numFriends = friendList.Length / 6;
                Console.WriteLine(numFriends);
                for (int i = 3; i < friendList.Length; i += 6)
                {
                    if (friendList[i - 2] == "true")
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ConvoList.Items.Add(friendList[i]);
                        }));
                    }
                }
            }
        }
        public void checkMsg()
        {
            Timer timer1 = new Timer();
            timer1.Elapsed += new ElapsedEventHandler(getMsg);
            timer1.Elapsed += new ElapsedEventHandler(getFriendRequest);
            timer1.Interval = 100;
            timer1.Start();
        }
        public async void getMsg(object sender, EventArgs e)
        {
            usr userName = new usr(theUser);
            string json = JsonConvert.SerializeObject(userName);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var data1 = new StringContent(json, Encoding.UTF8, "application/json");

            var rec = await client.PostAsync(url + "recMessages", data);
            var sent = await client.PostAsync(url + "sentMessages", data1);

            string recBody = rec.Content.ReadAsStringAsync().Result;
            string sentBody = sent.Content.ReadAsStringAsync().Result;

            var expConverter = new Newtonsoft.Json.Converters.ExpandoObjectConverter();
            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(recBody, expConverter);
            dynamic obj1 = JsonConvert.DeserializeObject<ExpandoObject>(sentBody, expConverter);

            var recJson = JsonConvert.SerializeObject(obj.recievedMessages);
            var senJson = JsonConvert.SerializeObject(obj1.sentMessages);

            var recMsg = JsonConvert.DeserializeObject<List<RecMsg>>(recJson);
            var senMsg = JsonConvert.DeserializeObject<List<SenMsg>>(senJson);
            foreach (RecMsg rsm in recMsg)
            {
                Console.WriteLine(rsm.msgId);
            }
            Console.WriteLine(recBody);
            Console.WriteLine(sentBody);
            Dispatcher.Invoke(new Action(() =>
            {
                if (ConvoList.SelectedItem.ToString() != " ")
                {
                    foreach (RecMsg r in recMsg)
                    {
                        if ((r.recUsername == theUser && r.senUsername == ConvoList.SelectedItem.ToString()) &&
                                r.dateEntered > Convert.ToDateTime(msg1Date.Content.ToString())
                                && r.msgId != msg1ID.Text.ToString())
                            Console.WriteLine(msg1ID.Text);
                        {
                            user3Lbl.Content = user2Lbl.Content;
                            msg3Lbl.Content = msg2Lbl.Content;
                            user2Lbl.Content = user1Lbl.Content;
                            msg2Lbl.Content = msg1Lbl.Content;
                            user1Lbl.Content = r.senUsername;
                            msg1Lbl.Content = r.message;
                            msg1ID.Text = r.msgId;
                            msg1Date.Content = r.dateEntered;
                        }
                    }
                    foreach (SenMsg s in senMsg)
                    {
                        if ((s.recUsername == theUser && s.senUsername == ConvoList.SelectedItem.ToString()) &&
                                s.dateEntered > Convert.ToDateTime(msg1Date.Content.ToString())
                                &&s.msgId != msg1ID.Text.ToString())
                        {
                            user3Lbl.Content = user2Lbl.Content;
                            msg3Lbl.Content = msg2Lbl.Content;
                            user2Lbl.Content = user1Lbl.Content;
                            msg2Lbl.Content = msg1Lbl.Content;
                            user1Lbl.Content = s.senUsername;
                            msg1Lbl.Content = s.message;
                            msg1ID.Text = s.msgId;
                            msg1Date.Content = s.dateEntered;
                        }
                    }
                }
                else { msg1Lbl.Content = "Select A Conversation"; }
            }));
            //if (recBody.Contains("\"recUsername\":\"" + theUser + "\""))
            //{  
            //    Console.WriteLine("Message Recieved");
            //    //join comma messages back together later
            //    List<Message> recMsgList = new List<Message>();
            //    #region string simplification
            //    recBody =recBody.Replace("{\"recievedMessages\":[", "");
            //    recBody = recBody.Replace("],servMessage:undefinedrecieved}", "");
            //    recBody = recBody.Replace("{\"_id\":", "");
            //    recBody = recBody.Replace("\"senUsername\":", "");
            //    recBody = recBody.Replace("\"recUsername\":", "");
            //    recBody = recBody.Replace("\"message\":", "");
            //    recBody = recBody.Replace("\"dateEntered\":", "");
            //    recBody = recBody.Replace("__v\":0},\"", "");
            //    recBody = recBody.Replace("\",\"", "§");

            //    sentBody = sentBody.Replace("{\"sentMessages\":[", "");
            //    sentBody = sentBody.Replace("],servMessage:undefinedrecieved}", "");
            //    sentBody = sentBody.Replace("{\"_id\":", "");
            //    sentBody = sentBody.Replace("\"senUsername\":", "");
            //    sentBody = sentBody.Replace("\"recUsername\":", "");
            //    sentBody = sentBody.Replace("\"message\":", "");
            //    sentBody = sentBody.Replace("\"dateEntered\":", "");
            //    sentBody = sentBody.Replace("__v\":0},\"", "");
            //    sentBody = sentBody.Replace("\",\"", "§");
            //    #endregion
            //    Console.WriteLine(recBody);
            //    Console.WriteLine(sentBody);
            //    var recSub = recBody.Split('§');
            //    var sentSub = sentBody.Split('§');
            //    Console.WriteLine(recSub[0]);
            //    //Console.WriteLine(convoSelect());
            //        Dispatcher.Invoke(new Action(() => {
            //            if (ConvoList.SelectedItem.ToString() != " ")
            //            {
            //                if (msg1Date.Content != null && msg1Date.Content.ToString() != "")
            //                {
            //                    //for (int i = 0; i < recSub.Length; i += 5)
            //                    //{
            //                    if (recSub[1] == ConvoList.SelectedItem.ToString() && DateTime.Parse(recSub[4]) > DateTime.Parse(msg1Date.Content.ToString()))
            //                    {
            //                        Console.WriteLine(ConvoList.SelectedItem.ToString());
            //                        user3Lbl.Content = user2Lbl.Content;
            //                        msg3Lbl.Content = msg2Lbl.Content;
            //                        user2Lbl.Content = user1Lbl.Content;
            //                        msg2Lbl.Content = msg1Lbl.Content;
            //                        user1Lbl.Content = recSub[1];
            //                        msg1Lbl.Content = recSub[3];
            //                        msg1Date.Content = recSub[4];
            //                    }
            //                    //}
            //                    //for (int i = 0; i < recSub.Length; i += 5)
            //                    //{
            //                    if (sentSub[2] == ConvoList.SelectedItem.ToString() && DateTime.Parse(sentSub[4]) > DateTime.Parse(msg1Date.Content.ToString()))
            //                    {
            //                        user3Lbl.Content = user2Lbl.Content;
            //                        msg3Lbl.Content = msg2Lbl.Content;
            //                        user2Lbl.Content = user1Lbl.Content;
            //                        msg2Lbl.Content = msg1Lbl.Content;
            //                        user1Lbl.Content = sentSub[1];
            //                        msg1Lbl.Content = sentSub[3];
            //                        msg1Date.Content = sentSub[4];
            //                    }
            //                    //}
            //                }
            //            }
            //            else { msg1Lbl.Content = "Select A Conversation"; }
            //        }), DispatcherPriority.ContextIdle);
            //    //}
            //}
        }
        public void setCreateUserBtn(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (newUsernameTxtBox.Text == "" || newEmailTxtBox.Text == "" ||
                newPassBox.Password == "" || repeatNewPassBox.Password == "" ||
                repeatNewPassBox.Password != newPassBox.Password)
                {
                    CreateUserBtn.Visibility = invisible;
                }
                else if (newUsernameTxtBox.Text != "" && newEmailTxtBox.Text != "" &&
                newPassBox.Password != "" && repeatNewPassBox.Password != "")
                { CreateUserBtn.Visibility = visible; }
            }));
        }
        public async void getFriendRequest(object sender, EventArgs e)
        {
            usr userName = new usr(theUser);
            string json = JsonConvert.SerializeObject(userName);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var rec = await client.PostAsync(url + "getFriendRequests", data);
            var res = rec.Content.ReadAsStringAsync().Result;
            if (res != "[]" && res != null)
            {
                var expConverter = new Newtonsoft.Json.Converters.ExpandoObjectConverter();
                dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(res, expConverter);
                var Json = JsonConvert.SerializeObject(obj.friendRequest);
                var friendReq = JsonConvert.DeserializeObject<List<friendRequest>>(Json);
                Console.WriteLine(res);
                if (friendReq != null)
                {
                    newFriendRequestLbl.Content = friendReq.length() + " New Friend Requests!";
                    foreach (friendRequest f in friendReq)
                    {
                        Console.WriteLine(f.senUsername);
                    }
                    int c = 0;
                    foreach (friendRequest f in friendReq)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            friendRequestList.Items.Clear();
                            friendRequestList.Items.Add(new Grid().Name = f.senUsername);
                        }));
                        c++;
                    }
                    foreach (Grid g in friendRequestList.Items)
                    {

                        System.Windows.Thickness l = new Thickness
                        {
                            Top = -4.0,
                            Bottom = -4.0
                        };
                        System.Windows.Thickness b1 = new Thickness
                        {
                            Right = 10
                        };
                        System.Windows.Thickness b2 = new Thickness
                        {
                            Left = 10
                        };
                        Dispatcher.Invoke(new Action(() =>
                        {
                            g.RowDefinitions.Add(new RowDefinition());
                            g.ColumnDefinitions.Add(new ColumnDefinition());//width 320
                            g.ColumnDefinitions.Add(new ColumnDefinition());//width 70
                            g.ColumnDefinitions.Add(new ColumnDefinition());//width 70
                            new Label()
                            {
                                Content = g.Name,
                                FontSize = 16,
                                Margin = l,
                                Width=320,
                                //.SetValue(Grid.ColumnProperty, 0)
                                
                            };
                            new Button()
                            {
                               Content = "Accept",
                                Margin = b1,
                                FontSize = 13,
                                Width=60,
                                //Background=
                                //Column=1
                                //Click=friendRequestResponse(g.name,theUser,true)
                            };
                            new Button()
                            {
                                Content = "Deny",
                                Margin = b2,
                                FontSize = 13,
                                Width = 60,
                                //Background=
                                //Column=2
                                //Click=friendRequestResponse(g.name,theUser,null)
                            };
                        }));
                        c++;
                    }
                }
            }
        }


        #endregion

        
    }
}
