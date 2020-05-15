using Aspose.Cells;
using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for GuestControl.xaml
    /// </summary>
    public partial class GuestControl : UserControl
    {
        public static Notify guest_notification = new Notify();
        public GuestControl()
        {
            InitializeComponent();
            guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
            guest_notification.PropertyChanged += GuestNotification_PropertyChanged;
        }

        private void GuestNotification_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
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
                    var name = sheet.Cells[$"B{row}"].StringValue;
                    var telephone = sheet.Cells[$"C{row}"].StringValue;
                    var identitycard = sheet.Cells[$"D{row}"].StringValue;

                    var newGuest = new Guest()
                    {
                        ID = IDGenerator.createID("G"),
                        Name = name,
                        Telephone = telephone,
                        IdentityCard = identitycard,
                        isDeleted = false,
                    };

                    MainWindow.db.Guests.Add(newGuest);
                    MainWindow.db.SaveChanges();

                    row++;
                    cell = sheet.Cells[$"{col}{row}"];
                }

                MessageBox.Show("Import successfully!", "Message");

                guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newGuest = new AddGuestWindow();

            if (newGuest.ShowDialog() == true)
            {
                var guest = new Guest()
                {
                    ID = IDGenerator.createID("G"),
                    Name = newGuest.G_Name,
                    Telephone = newGuest.G_Telephone,
                    IdentityCard = newGuest.G_IdentityCard,
                    isDeleted = false,
                };

                MainWindow.db.Guests.Add(guest);
                MainWindow.db.SaveChanges();

                guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Guest selectedItem = (Guest)guestDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var newGuest = new EditGuestWindow(selectedItem);

                if (newGuest.ShowDialog() == true)
                {
                    var update = (from guest in MainWindow.db.Guests where guest.ID == selectedItem.ID select guest).Single();
                    update.Name = newGuest.G_Name;
                    update.Telephone = newGuest.G_Telephone;
                    update.IdentityCard = newGuest.G_IdentityCard;
                    MainWindow.db.SaveChanges();

                    guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Guest selectedItem = (Guest)guestDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                if ((from reservation in MainWindow.db.Reservations where reservation.GuestID == selectedItem.ID select reservation).Count() == 0)
                {
                    var delete = (from guest in MainWindow.db.Guests where guest.ID == selectedItem.ID select guest).Single();
                    MainWindow.db.Guests.Remove(delete);
                    MainWindow.db.SaveChanges();

                    guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
                }
                else
                {
                    MessageBox.Show("This guest is on reservation now, cannot remove.", "Warning");
                }
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = keywordTextBox.Text;

            var data = from guest in MainWindow.db.Guests.ToList()
                       where guest.Name.ToLower().AccentRemoved().Contains(keyword.ToLower().AccentRemoved())
                       select guest;
            guestDataGrid.ItemsSource = data;
        }

        private void guestDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                Guest selectedItem = (Guest)row.Item;

                var newGuest = new EditGuestWindow(selectedItem);

                if (newGuest.ShowDialog() == true)
                {
                    var update = (from guest in MainWindow.db.Guests where guest.ID == selectedItem.ID select guest).Single();
                    update.Name = newGuest.G_Name;
                    update.Telephone = newGuest.G_Telephone;
                    update.IdentityCard = newGuest.G_IdentityCard;
                    MainWindow.db.SaveChanges();

                    guestDataGrid.ItemsSource = MainWindow.db.Guests.ToList();
                }
            }
        }
    }
}
