using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for EditReservationWindow.xaml
    /// </summary>
    public partial class EditReservationWindow : Window
    {
        private BindingList<ReservationDetail> tempList = new BindingList<ReservationDetail>();

        public EditReservationWindow(Reservation item)
        {
            InitializeComponent();

            RoomIDComboBox.ItemsSource = MainWindow.db.Rooms.ToList();
            RoomIDComboBox.SelectedIndex = FindIndex(MainWindow.db.Rooms.ToList(), item.RoomID);

            GuestIDComboBox.ItemsSource = MainWindow.db.Guests.ToList();
            GuestIDComboBox.SelectedIndex = FindIndex(MainWindow.db.Guests.ToList(), item.GuestID);

            StartDateDatePicker.Text = item.StartDate.ToString();

            EndDateDatePicker.Text = item.EndDate.ToString();

            LoadDetails(item.RoomID, item.GuestID);

            TotalTextBox.Text = "$" + item.TotalPrice.ToString();

            this.DataContext = this;
        }

        private void LoadDetails(string roomid, string guestid)
        {
            var data = from reservation in MainWindow.db.ReservationDetails.ToList()
                       where reservation.RoomID.Contains(roomid) && reservation.GuestID.Contains(guestid)
                       select reservation;
            reservationDataGrid.ItemsSource = data;
        }

        private int FindIndex(List<Guest> list, string id)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (item.ID == id)
                {
                    return i;
                }
                i++;
            }
            return i;
        }

        private int FindIndex(List<Room> list, string id)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (item.ID == id)
                {
                    return i;
                }
                i++;
            }
            return i;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (EndDateDatePicker.SelectedDate.Value < StartDateDatePicker.SelectedDate.Value)
            {
                MessageBox.Show("The start date cannot be later than the end date.\nPlease try again.");                
            }
            else
            {
                if (EndDateDatePicker.Text != null)
                {
                    var update = (from reservation in MainWindow.db.Reservations where reservation.RoomID == RoomIDComboBox.Text select reservation).Single();
                    update.StartDate = StartDateDatePicker.SelectedDate.Value;
                    update.EndDate = EndDateDatePicker.SelectedDate.Value;
                    update.TotalPrice = (EndDateDatePicker.SelectedDate.Value - StartDateDatePicker.SelectedDate.Value).TotalDays * (double)((from room in MainWindow.db.Rooms where room.ID == RoomIDComboBox.Text select room.Price).Single() ?? 0);
                    List<ReservationDetail> List = (from service in MainWindow.db.ReservationDetails where service.GuestID == update.GuestID select service).ToList();
                    foreach (var detail in List)
                    {
                        update.TotalPrice = update.TotalPrice + detail.ServicePrice;
                    }
                    MainWindow.db.SaveChanges();
                    ReservationControl.reservation_notification.ReservationDetailChange = true;
                    this.DataContext = this;
                    Close();
                }
            }
        }
    }
}
