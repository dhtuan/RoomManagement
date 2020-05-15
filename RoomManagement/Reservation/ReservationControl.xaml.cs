using Aspose.Cells;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for ReservationControl.xaml
    /// </summary>
    public partial class ReservationControl : UserControl
    {
        public static Notify reservation_notification = new Notify();
        public ReservationControl()
        {
            InitializeComponent();
            reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
            reservation_notification.PropertyChanged += ReservationNotification_PropertyChanged;
            this.DataContext = this;
        }

        private void ReservationNotification_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Excel files|*.xls;*.xlsx";

            if (screen.ShowDialog() == true)
            {
                var workbook = new Workbook(screen.FileName);

                var sheet = workbook.Worksheets[0];

                var col = "A";
                var row = 2;

                var cell = sheet.Cells[$"{col}{row}"];

                while (cell.Value != null)
                {
                    bool isContinue = true;

                    var roomid = sheet.Cells[$"A{row}"].StringValue;
                    var guestid = sheet.Cells[$"B{row}"].StringValue;

                    bool gotRoomID = MainWindow.db.Rooms.ToList().Any(cus => cus.ID == roomid);
                    bool gotGuestID = MainWindow.db.Guests.ToList().Any(cus => cus.ID == guestid);

                    if (gotRoomID == false)
                    {
                        string message = "The room with ID \"" + roomid + "\" in reservations does not exist.\nDo you want to add a new room? You can skip importing this reservation by clicking \"No\".";

                        if (MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            if (MainWindow.db.Categories.Count() == 0)
                            {
                                string message2 = "There are no categories for rooms. Click \"Yes\" to create a new category or \"No\" to skip importing this reservation.";

                                if (MessageBox.Show(message2, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                {
                                    var newCategory = new AddCategoryWindow();
                                    if (newCategory.ShowDialog() == true)
                                    {
                                        if (newCategory.CT_Name != null)
                                        {
                                            var category = new Category()
                                            {
                                                ID = IDGenerator.createID("CT"),
                                                Name = newCategory.CT_Name,
                                                isDeleted = false,
                                            };

                                            MainWindow.db.Categories.Add(category);
                                            MainWindow.db.SaveChanges();
                                            CategoryControl.category_notification.CategoryChange = true;
                                        }
                                    }
                                    else
                                    {
                                        isContinue = false;
                                    }
                                }
                            }

                            if (isContinue == true)
                            {  
                            var newRoom = new AddRoomWindow();

                                if (newRoom.ShowDialog() == true)
                                {
                                    if (newRoom.CategoryComboBox.SelectedItem != null && newRoom.PriceTextBox.Value > 0 && newRoom.StatusComboBox.SelectedItem != null)
                                    {
                                        var room = new Room()
                                        {
                                            ID = roomid,
                                            CategoryID = newRoom.R_CategoryID,
                                            Price = newRoom.R_Price,
                                            Status = newRoom.R_Status,
                                            Picture = newRoom.R_Picture,
                                            isDeleted = false,
                                        };

                                        MainWindow.db.Rooms.Add(room);
                                        MainWindow.db.SaveChanges();
                                        RoomControl.room_notification.RoomChange = true;
                                    }
                                }
                            }
                        }
                    }

                    if (gotGuestID == false && isContinue == true)
                    {
                        string message = "The guest with ID \"" + guestid + "\" in reservations does not exist.\nDo you want to add a new guest? You can skip importing this reservation by clicking \"No\".";

                        if (MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var newGuest = new AddGuestWindow();

                            if (newGuest.ShowDialog() == true)
                            {
                                if (!string.IsNullOrWhiteSpace(newGuest.NameTextBox.Text) && !string.IsNullOrWhiteSpace(newGuest.IdentityCardTextBox.Text) && !string.IsNullOrWhiteSpace(newGuest.TelephoneTextBox.Text))
                                {
                                    var guest = new Guest()
                                    {
                                        ID = guestid,
                                        Name = newGuest.G_Name,
                                        Telephone = newGuest.G_Telephone,
                                        IdentityCard = newGuest.G_IdentityCard,
                                        isDeleted = false,
                                    };

                                    MainWindow.db.Guests.Add(guest);
                                    MainWindow.db.SaveChanges();
                                    GuestControl.guest_notification.GuestChange = true;
                                }
                            }
                        }
                    }

                    if (MainWindow.db.Rooms.ToList().Any(cus => cus.ID == roomid) == true && MainWindow.db.Rooms.ToList().Any(cus => cus.ID == roomid) == true)
                    {
                        var date = sheet.Cells[$"C{row}"].DateTimeValue;
                        var startdate = sheet.Cells[$"D{row}"].DateTimeValue;
                        var enddate = sheet.Cells[$"E{row}"].DateTimeValue;
                        var totalprice = sheet.Cells[$"F{row}"].DoubleValue;

                        var newReservation = new Reservation()
                        {
                            RoomID = roomid,
                            GuestID = guestid,
                            Date = date,
                            StartDate = startdate,
                            EndDate = enddate,
                            TotalPrice = totalprice,
                        };

                        var detail_Sheet = workbook.Worksheets[1];

                        var detail_Col = "A";
                        var detail_Row = 2;

                        var detail_Cell = detail_Sheet.Cells[$"{detail_Col}{detail_Row}"];

                        List<ReservationDetail> detail_List = new List<ReservationDetail>();

                        while (detail_Cell.Value != null)
                        {
                            var detail_roomID = detail_Sheet.Cells[$"A{detail_Row}"].StringValue;
                            var detail_guestID = detail_Sheet.Cells[$"B{detail_Row}"].StringValue;
                            if (detail_roomID == newReservation.RoomID && detail_guestID == newReservation.GuestID)
                            {
                                var detail_serviceID = detail_Sheet.Cells[$"C{detail_Row}"].StringValue;
                                var detail_date = detail_Sheet.Cells[$"D{detail_Row}"].DateTimeValue;

                                var newDetail = new ReservationDetail()
                                {
                                    RoomID = detail_roomID,
                                    GuestID = detail_guestID,
                                    ServiceName = (from service in MainWindow.db.Services where service.ID == detail_serviceID select service.Name).Single(),
                                    ServicePrice = (double)(from service in MainWindow.db.Services where service.ID == detail_serviceID select service.Price).Single(),
                                    ServiceID = detail_serviceID,
                                    Date = detail_date,
                                };

                                detail_List.Add(newDetail);
                            }

                            detail_Row++;
                            detail_Cell = detail_Sheet.Cells[$"{detail_Col}{detail_Row}"];
                        }

                        foreach(var detail in detail_List)
                        {
                            MainWindow.db.ReservationDetails.Add(detail);
                            MainWindow.db.SaveChanges();
                        }

                        MainWindow.db.Reservations.Add(newReservation);
                        MainWindow.db.SaveChanges();

                        var chosenRoom = (from chosenroom in MainWindow.db.Rooms where chosenroom.ID == newReservation.RoomID select chosenroom).Single();
                        chosenRoom.Status = "Rented";
                        MainWindow.db.SaveChanges();
                    }

                    row++;
                    cell = sheet.Cells[$"{col}{row}"];
                }

                MessageBox.Show("Import completed!", "Message");

                reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
                RoomControl.room_notification.RoomChange = true;
            }
        }

                    

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if ((from room in MainWindow.db.Rooms where room.Status == "Ready" select room).Count() > 0)
            {                
                var newInvoice = new AddReservationWindow();

                if (newInvoice.ShowDialog() == true)
                {
                    reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
                    RoomControl.room_notification.RoomChange = true;
                }
            }
            else
            {
                string message = "There is no rooms available now. Do you want to add a new room?\nClick \"Yes\" to add a new room.";

                if (MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if ((from category in MainWindow.db.Categories select category).Count() == 0)
                    {
                        MessageBox.Show("There is no categories either. Please add one.", "Warning");
                        var newCategory = new AddCategoryWindow();

                        if (newCategory.ShowDialog() == true)
                        {
                            var category = new Category()
                            {
                                ID = IDGenerator.createID("CT"),
                                Name = newCategory.CT_Name,
                                isDeleted = false,
                            };

                            MainWindow.db.Categories.Add(category);
                            MainWindow.db.SaveChanges();

                            CategoryControl.category_notification.CategoryChange = true;
                        }
                    }
                    var newRoom = new AddRoomWindow();

                    if (newRoom.ShowDialog() == true)
                    {
                        var room = new Room()
                        {
                            ID = IDGenerator.createID("R"),
                            CategoryID = newRoom.R_CategoryID,
                            Price = newRoom.R_Price,
                            Status = newRoom.R_Status,
                            isDeleted = false,
                            Picture = newRoom.R_Picture,
                        };
                        MainWindow.db.Rooms.Add(room);
                        MainWindow.db.SaveChanges();

                        RoomControl.room_notification.RoomChange = true;
                        if (room.Status == "Ready")
                        {
                            var newInvoice = new AddReservationWindow();

                            if (newInvoice.ShowDialog() == true)
                            {
                                reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
                                RoomControl.room_notification.RoomChange = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("You should add a ready room.", "Warning");
                        }
                    }                   
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation selectedItem = (Reservation)reservationDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var newProduct = new EditReservationWindow(selectedItem);

                if (newProduct.ShowDialog() == true)
                {

                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Reservation selectedItem = (Reservation)reservationDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var deletedetails = from detail in MainWindow.db.ReservationDetails
                                    where detail.GuestID == selectedItem.GuestID
                                    select detail;
                foreach (var detail in deletedetails)
                {
                    MainWindow.db.ReservationDetails.Remove(detail);
                }
                MainWindow.db.SaveChanges();

                var delete = (from reservation in MainWindow.db.Reservations where reservation.RoomID == selectedItem.RoomID select reservation).Single();
                MainWindow.db.Reservations.Remove(delete);
                MainWindow.db.SaveChanges();

                var delRoom = (from delroom in MainWindow.db.Rooms where delroom.ID == selectedItem.RoomID select delroom).Single();
                delRoom.Status = "Ready";
                MainWindow.db.SaveChanges();

                reservationDataGrid.ItemsSource = MainWindow.db.Reservations.ToList();
                RoomControl.room_notification.RoomChange = true;
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = keywordTextBox.Text;

            var data = from reservation in MainWindow.db.Reservations.ToList()
                       where reservation.RoomID.ToLower().AccentRemoved().Contains(keyword.ToLower().AccentRemoved())
                       select reservation;
            reservationDataGrid.ItemsSource = data;
        }

        private void reservationDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                Reservation selectedItem = (Reservation)row.Item;

                var newService = new EditReservationWindow(selectedItem);

                if (newService.ShowDialog() == true)
                {

                }
            }
        }
    }
}
