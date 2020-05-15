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
    /// Interaction logic for RoomControl.xaml
    /// </summary>
    public partial class RoomControl : UserControl
    {
        public static Notify room_notification = new Notify();

        public RoomControl()
        {
            InitializeComponent();
            List<Reservation> resList = (from reservation in MainWindow.db.Reservations select reservation).ToList();
            foreach (var res in resList)
            {
                if(res.EndDate <= DateTime.Now)
                {
                    var updRoom = (from room in MainWindow.db.Rooms where room.ID == res.RoomID select room).Single();
                    updRoom.Status = "Ready";
                }
            }
            MainWindow.db.SaveChanges();
            roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
            room_notification.PropertyChanged += RoomNotification_PropertyChanged;
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
                    var categoryid = sheet.Cells[$"B{row}"].StringValue;

                    bool has = MainWindow.db.Categories.ToList().Any(cus => cus.ID == categoryid);
                    if (has == true)
                    {
                        var price = sheet.Cells[$"C{row}"].DoubleValue;
                        var status = sheet.Cells[$"D{row}"].StringValue;
                        var imageName = sheet.Cells[$"E{row}"].StringValue;

                        var imageSourceInfo = new FileInfo(screen.FileName);
                        var imageSourceFullPath = $"{imageSourceInfo.DirectoryName}\\images\\{imageName}";
                        var imageSourceFileInfo = new FileInfo(imageSourceFullPath);

                        var uniqueName = $"{Guid.NewGuid()}.{imageSourceFileInfo.Extension}";

                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var destinationPath = $"{baseDirectory}image\\{uniqueName}";

                        File.Copy(imageSourceFullPath, destinationPath);

                        var newRoom = new Room()
                        {
                            ID = IDGenerator.createID("R"),
                            CategoryID = categoryid,
                            Price = price,
                            Status = status,
                            isDeleted = false,
                            Picture = uniqueName,
                        };

                        MainWindow.db.Rooms.Add(newRoom);
                        MainWindow.db.SaveChanges();

                        row++;
                        cell = sheet.Cells[$"{col}{row}"];
                    }
                    else
                    {
                        string message = "The category '" + categoryid + "' does not exist. Do you want to add new category?";

                        if (MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var newCategory = new AddCategoryWindow();

                            if (newCategory.ShowDialog() == true)
                            {
                                if (newCategory.CT_Name != null)
                                {
                                    var category = new Category()
                                    {
                                        ID = categoryid,
                                        Name = newCategory.CT_Name,
                                        isDeleted = false,
                                    };

                                    MainWindow.db.Categories.Add(category);
                                    MainWindow.db.SaveChanges();
                                    CategoryControl.category_notification.CategoryChange = true;

                                    var price = sheet.Cells[$"C{row}"].DoubleValue;
                                    var status = sheet.Cells[$"D{row}"].StringValue;
                                    var imageName = sheet.Cells[$"E{row}"].StringValue;

                                    var imageSourceInfo = new FileInfo(screen.FileName);
                                    var imageSourceFullPath = $"{imageSourceInfo.DirectoryName}\\images\\{imageName}";
                                    var imageSourceFileInfo = new FileInfo(imageSourceFullPath);

                                    var uniqueName = $"{Guid.NewGuid()}.{imageSourceFileInfo.Extension}";

                                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                    var destinationPath = $"{baseDirectory}image\\{uniqueName}";

                                    File.Copy(imageSourceFullPath, destinationPath);

                                    var newRoom = new Room()
                                    {
                                        ID = IDGenerator.createID("R"),
                                        CategoryID = categoryid,
                                        Price = price,
                                        Status = status,
                                        isDeleted = false,
                                        Picture = uniqueName,
                                    };

                                    MainWindow.db.Rooms.Add(newRoom);
                                    MainWindow.db.SaveChanges();

                                }
                            }
                            row++;
                            cell = sheet.Cells[$"{col}{row}"];
                        }
                        else
                        {
                            row++;
                            cell = sheet.Cells[$"{col}{row}"];
                        }
                    }
                }

                MessageBox.Show("Import successfully!", "Message");

                roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
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

                roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)roomDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var newRoom = new EditRoomWindow(selectedItem);                

                if (newRoom.ShowDialog() == true)
                {
                    var update = (from room in MainWindow.db.Rooms where room.ID == selectedItem.ID select room).Single();
                    update.CategoryID = newRoom.R_CategoryID;
                    update.Price = newRoom.R_Price;
                    update.Status = newRoom.R_Status;
                    update.Picture = newRoom.R_Picture;
                    MainWindow.db.SaveChanges();

                    roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Room selectedItem = (Room)roomDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                if ((from reservation in MainWindow.db.Reservations where reservation.RoomID == selectedItem.ID select reservation).Count() == 0)
                {
                    var delete = (from room in MainWindow.db.Rooms where room.ID == selectedItem.ID select room).Single();
                    MainWindow.db.Rooms.Remove(delete);
                    MainWindow.db.SaveChanges();

                    roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
                }
                else
                {
                    MessageBox.Show("This room is on reservation now, cannot remove.", "Warning");
                }
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = keywordTextBox.Text;

            var data = from room in MainWindow.db.Rooms.ToList()
                       where room.ID.ToLower().AccentRemoved().Contains(keyword.ToLower().AccentRemoved())
                       select room;
            roomDataGrid.ItemsSource = data;
        }

        private void RoomNotification_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
        }

        private void roomDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                Room selectedItem = (Room)row.Item;

                var newRoom = new EditRoomWindow(selectedItem);


                if (newRoom.ShowDialog() == true)
                {
                    var update = (from room in MainWindow.db.Rooms where room.ID == selectedItem.ID select room).Single();
                    update.CategoryID = newRoom.R_CategoryID;
                    update.Price = newRoom.R_Price;
                    update.Status = newRoom.R_Status;
                    update.Picture = newRoom.R_Picture;
                    MainWindow.db.SaveChanges();

                    roomDataGrid.ItemsSource = MainWindow.db.Rooms.ToList();
                }
            }
        }
    }
}