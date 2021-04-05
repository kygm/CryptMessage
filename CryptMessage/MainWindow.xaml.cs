using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CryptMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool home = true;//this var and others similar will hopefully be used to turn on and off elements belonging to different pages. 
        string page = "home";
        System.Windows.Visibility visible = Visibility.Visible;
        System.Windows.Visibility invisible = Visibility.Hidden;

        public MainWindow()
        {
            InitializeComponent();
            page = "home";
            updatePages(page);
        }
        private void mnuAbout(object sender, RoutedEventArgs e)
        {
            page = "about";
            updatePages(page);
        }
        private void mnuConversations(object sender, RoutedEventArgs e)
        {
            page = "conversations";
            updatePages(page);
        }
        private void mnuSettings(object sender, RoutedEventArgs e)
        {
            page = "settings";
            updatePages(page);
        }
        private void updatePages(String page)
        {
            homeVis(page);
            aboutVis(page);
            conversationListVis(page);
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
        private void conversationListVis(String page)
        {
            System.Windows.Visibility visState;
            if (page == "conversations")
            {
                visState = visible;
            }
            else { visState = invisible; }
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
    }

}
