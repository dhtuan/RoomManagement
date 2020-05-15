using HandyControl.Controls;
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

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static RoomEntities db = new RoomEntities();
        public static RoomControl roompg = new RoomControl();
        public static CategoryControl categorypg = new CategoryControl();
        public static GuestControl guestpg = new GuestControl();
        public static ReservationControl reservationpg = new ReservationControl();
        public static ServiceControl servicepg = new ServiceControl();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void sideMenu_Selected(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as SideMenuItem;
            if (item != null)
            {
                if (item.Name == "Rooms")
                {
                    Control.Show(MainContent, roompg);
                }
                else if (item.Name == "Categories")
                {
                    Control.Show(MainContent, categorypg);
                }
                else if (item.Name == "Guests")
                {
                    Control.Show(MainContent, guestpg);
                }
                else if (item.Name == "Reservations")
                {
                    Control.Show(MainContent, reservationpg);
                }
                else if (item.Name == "Services")
                {
                    Control.Show(MainContent, servicepg);
                }
            }
        }
    }
}
