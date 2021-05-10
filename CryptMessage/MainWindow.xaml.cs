using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public string senUsername, recUsername, message;
        public DateTime dateEntered;
        public Message(string sender, string reciever, string mess, DateTime entered)
        {
            senUsername = sender;
            recUsername = reciever;
            message = mess;
            dateEntered = entered;
        }
    }
    class Messages
    {
        public string _id, senUsername, recUsername, message;
        public DateTime dateEntered;
        public Messages(string id, string sender, string reciever, string mess, DateTime entered)
        {
            _id = id;
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
    class Conversation
    {
        public string sender, reciever;
        public Conversation(string u1, string u2)
        {
            sender = u1;
            reciever = u2;
        }
    }
    class sendFriendRequest
    {
        public string senUsername, recUsername;
        public DateTime date;
        public sendFriendRequest(string sen, string rec)
        {
            senUsername = sen;
            recUsername = rec;
            date = DateTime.Now;
        }
    }
    class friendRequest
    {
        public string firstUsername, secondUsername;
        public bool accepted;
        public DateTime date;
        public friendRequest(string sen,string rec, bool stat)
        {
            firstUsername = sen;
            secondUsername = rec;
            accepted = stat;
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
    static class selFri
    {
        public static string friend;
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
            Dispatcher.Invoke(new Action(() =>
            {
                selFri.friend = ConvoList.SelectedItem.ToString();
            }));
            string sen = theUser, 
                rec = selFri.friend, 
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
            sendFriendRequest req = new sendFriendRequest(sen, rec);
            string json = JsonConvert.SerializeObject(req);
            Console.WriteLine(json.ToString());
            var reqData = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync(url + "sendFriendRequest", reqData);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if(body.Contains("request sent"))
            {
                NewFriendTxtBox.Text = "";
            }
        }
        private async void requestAccept_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                selFri.friend = friendRequestList.SelectedItem.ToString();
            }));
            var s = selFri.friend;
            friendRequest acc = new friendRequest(s, theUser, true);
            string json = JsonConvert.SerializeObject(acc);
            Console.WriteLine(json.ToString());
            var Data = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url + "acceptFriendRequest", Data);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if(body.Contains("Friend Request Accepted"))
            {
                friendRequestList.Items.Remove(friendRequestList.SelectedItem);
            }
        }
        private async void requestDeny_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                selFri.friend = friendRequestList.SelectedItem.ToString();
            }));
            var s = selFri.friend;
            friendRequest acc = new friendRequest(s, theUser, true);
            string json = JsonConvert.SerializeObject(acc);
            Console.WriteLine(json.ToString());
            var Data = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url + "acceptFriendRequest", Data);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if (body.Contains("Friend Request Denied"))
            {
                friendRequestList.Items.Remove(friendRequestList.SelectedItem);
            }
        }
        private async void DelFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                selFri.friend = ConvoList.SelectedItem.ToString();
            }));
            var s = selFri.friend;
            friendRequest del = new friendRequest(s, theUser, true);
            string json = JsonConvert.SerializeObject(del);
            Console.WriteLine(json.ToString());
            var Data = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await client.PostAsync(url + "sendFriendRequest", Data);
            string body = res.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);
            if (body.Contains("Friend Request Denied"))
            {
                friendRequestList.Items.Remove(ConvoList.SelectedItem);
            }
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
            Dispatcher.Invoke(new Action(() =>
            {
                ConvoList.Items.Clear();
                ConvoList.Items.Add(" ");
                ConvoList.SelectedItem = " ";
            }));
            usr userName = new usr(theUser);
            string json = JsonConvert.SerializeObject(userName);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var rec = await client.PostAsync(url + "getFriends", data);
            var res = rec.Content.ReadAsStringAsync().Result;
            Console.WriteLine(res);
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
            timer1.Interval = 1000;
            timer1.Start();
        }
        public async void getMsg(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                selFri.friend = ConvoList.SelectedItem.ToString();
            }));
            if (selFri.friend != " ")
            {
                Console.WriteLine(selFri.friend);
                Conversation c = new Conversation(theUser, selFri.friend.ToString());
                string json = JsonConvert.SerializeObject(c);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var msg = await client.PostAsync(url + "allMessages", data);
                string msgBody = msg.Content.ReadAsStringAsync().Result;
                Console.WriteLine(msgBody);
                var expConverter = new Newtonsoft.Json.Converters.ExpandoObjectConverter();
                dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(msgBody, expConverter);
                var msgJson = JsonConvert.SerializeObject(obj.messages);
                //var mmsgJson = JsonConvert.SerializeObject(obj.moreMessages);
                var msgList = JsonConvert.DeserializeObject<List<Messages>>(msgJson);
                
                Dispatcher.Invoke(new Action(() =>
                {
                    foreach (Messages m in msgList)
                    {
                        if (((m.recUsername == theUser && m.senUsername == selFri.friend) ||
                            (m.recUsername == selFri.friend && m.senUsername == theUser)) &&
                                m.dateEntered > Convert.ToDateTime(msg1Date.Content.ToString())
                                && m._id != msg1ID.Text.ToString()
                                )
                        {
                            user3Lbl.Content = user2Lbl.Content;
                            msg3Lbl.Content = msg2Lbl.Content;
                            user2Lbl.Content = user1Lbl.Content;
                            msg2Lbl.Content = msg1Lbl.Content;
                            user1Lbl.Content = m.senUsername;
                            msg1Lbl.Content = m.message;
                            msg1ID.Text = m._id;
                            msg1Date.Content = m.dateEntered;
                        }
                    }
                }));
            }
            else { Dispatcher.Invoke(new Action(() =>{ msg1Lbl.Content = "Select A Conversation"; })); }

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
            Console.WriteLine(res);
            if (res != "[]" && res != null)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    var friendReq = JsonConvert.DeserializeObject<List<friendRequest>>(res);
                    Console.WriteLine(res);
                    int reqs = 0;
                    foreach (friendRequest f in friendReq)
                    {
                        if (f.accepted != true)
                        {
                            reqs++;
                        }
                    }
                    if (reqs !=0&&friendReq!=null)
                    {
                        newFriendRequestLbl.Content = reqs + " New Friend Requests!";
                        
                        friendRequestList.Items.Clear();
                        foreach (friendRequest f in friendReq)
                        {
                            if (f != null&&f.accepted!=true)
                            {
                                Console.WriteLine(f.secondUsername.ToString());
                                friendRequestList.Items.Add(f.secondUsername.ToString());
                            }
                        }
                        
                    }
                }));
            }
        }
        public void getSelectedRequest(object sender, EventArgs e)
        {
            if(friendRequestList.SelectedItem.ToString()!=" ")
            {
                requestAccept.Visibility = visible;
                requestDeny.Visibility = visible;
            }
            else
            {
                requestAccept.Visibility = invisible;
                requestDeny.Visibility = invisible;
            }
        }


        #endregion
    }
}
