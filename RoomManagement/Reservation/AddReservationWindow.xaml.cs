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
    /// Interaction logic for AddReservationWindow.xaml
    /// </summary>
    public partial class AddReservationWindow : Window
    {
        public static Notify reservationDetail_notification = new Notify();

        private BindingList<ReservationDetail> tempList = new BindingList<ReservationDetail>();

        public AddReservationWindow()
        {
            InitializeComponent();

            RoomIDComboBox.ItemsSource = (from room in MainWindow.db.Rooms where room.Status == "Ready" select room).ToList();

            GuestIDComboBox.ItemsSource = MainWindow.db.Guests.ToList();

            ServiceComboBox.ItemsSource = MainWindow.db.Services.ToList();

            StartDateDatePicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
            EndDateDatePicker.Text = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");

            RoomIDComboBox.SelectedIndex = 0;

            this.DataContext = this;
        }

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var service = ServiceComboBox.SelectedItem as Service;
            var guest = GuestIDComboBox.SelectedItem as Guest;

            if (service != null && guest != null)
            {
                var newReservationDetail = new ReservationDetail()
                {
                    GuestID = (from guest1 in MainWindow.db.Guests where guest1.ID == guest.ID select guest1.ID).Single(),
                    ServiceID = service.ID,
                    ServicePrice = (from service1 in MainWindow.db.Services where service1.ID == service.ID select service1.Price).Single() ?? 0,
                    ServiceName = (from service2 in MainWindow.db.Services where service2.ID == service.ID select service2.Name).Single(),
                    Date = DateTime.Now,
                    isDeleted = false,
                };
                bool added = false;
                foreach (var detail in tempList)
                {
                    if(detail.ServiceID == newReservationDetail.ServiceID)
                    {
                        added = true;
                        MessageBox.Show("Service is already added");
                        break;
                    }
                }
                if (added == false)
                {
                    tempList.Add(newReservationDetail);
                    reservationDataGrid.ItemsSource = tempList;
                }

                TotalTextBox.Text = "$" + TotalCalculator().ToString();
            }
        }

        private double TotalCalculator()
        {
            double sum = 0;
            if (RoomIDComboBox.SelectedValue != null && EndDateDatePicker.SelectedDate.Value != null && StartDateDatePicker.SelectedDate.Value != null)
            {
                sum = (EndDateDatePicker.SelectedDate.Value - StartDateDatePicker.SelectedDate.Value).TotalDays * (double)((from room in MainWindow.db.Rooms where room.ID == RoomIDComboBox.Text select room.Price).Single() ?? 0);
                foreach (var detail in tempList)
                {
                    double temp = (from service in MainWindow.db.Services where service.ID == detail.ServiceID select service.Price).Single() ?? 0;
                    sum = sum + temp;
                }
            }
            return sum;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationDetail selectedItem = (ReservationDetail)reservationDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                foreach (var detail in tempList)
                {
                    if (detail.GuestID == selectedItem.GuestID)
                    {
                        tempList.Remove(detail);
                        break;
                    }
                }

                reservationDataGrid.ItemsSource = tempList;
                TotalTextBox.Text = "$" + TotalCalculator().ToString();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var room = RoomIDComboBox.SelectedItem as Room;
            var guest = GuestIDComboBox.SelectedItem as Guest;

            if (RoomIDComboBox.SelectedItem != null && GuestIDComboBox.SelectedItem != null && StartDateDatePicker.Text != null && EndDateDatePicker.Text != null && EndDateDatePicker.SelectedDate.Value > StartDateDatePicker.SelectedDate.Value)
            {
                var newReservation = new Reservation()
                {
                    RoomID = room.ID, //////////
                    GuestID = (from guest1 in MainWindow.db.Guests where guest1.ID == guest.ID select guest1.ID).Single(),
                    Date = DateTime.Now,
                    StartDate = StartDateDatePicker.SelectedDate.Value,
                    EndDate = EndDateDatePicker.SelectedDate.Value,
                    TotalPrice = TotalCalculator(),
                    isDeleted = false,
                };

                MainWindow.db.Reservations.Add(newReservation);
                MainWindow.db.SaveChanges();

                //ProductNametoIDConverter(tempList);

                foreach(var item in tempList)
                {
                    item.RoomID = newReservation.RoomID;
                    MainWindow.db.ReservationDetails.Add(item);
                    MainWindow.db.SaveChanges();
                }

                var chosenRoom = (from chosenroom in MainWindow.db.Rooms where chosenroom.ID == room.ID select chosenroom).Single();
                chosenRoom.Status = "Rented";
                MainWindow.db.SaveChanges();

                this.DialogResult = true;
            }
        }

        //private void ProductNametoIDConverter(BindingList<ReservationDetail> list)
        //{
        //    foreach (var item in list)
        //    {
        //        item.ServiceID = (from service in MainWindow.db.Services where service.Name == item.ServiceID select service).Single().ID;
        //    }
        //}

        private void EndDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalTextBox.Text = "$" + TotalCalculator().ToString();
        }

        private void RoomIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedroom = RoomIDComboBox.SelectedItem as Room;
            if (selectedroom != null && EndDateDatePicker.SelectedDate.Value != null && StartDateDatePicker.SelectedDate.Value != null)
            {
                double sum = (EndDateDatePicker.SelectedDate.Value - StartDateDatePicker.SelectedDate.Value).TotalDays * (double)((from room in MainWindow.db.Rooms where room.ID == selectedroom.ID select room.Price).Single() ?? 0);
                foreach (var detail in tempList)
                {
                    double temp = (from service in MainWindow.db.Services where service.ID == detail.ServiceID select service.Price).Single() ?? 0;
                    sum = sum + temp;
                }
                TotalTextBox.Text = "$" + sum.ToString();
            }
        }
    }
}
