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
    /// Interaction logic for ServiceControl.xaml
    /// </summary>
    public partial class ServiceControl : UserControl
    {
        public static Notify service_notification = new Notify();
        public ServiceControl()
        {
            InitializeComponent();
            serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
            service_notification.PropertyChanged += ServiceNotification_PropertyChanged;
        }

        private void ServiceNotification_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
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
                    var price = sheet.Cells[$"C{row}"].DoubleValue;

                    var newService = new Service()
                    {
                        ID = IDGenerator.createID("S"),
                        Name = name,
                        Price = price,
                        isDeleted = false,
                    };

                    MainWindow.db.Services.Add(newService);
                    MainWindow.db.SaveChanges();

                    row++;
                    cell = sheet.Cells[$"{col}{row}"];
                }

                MessageBox.Show("Import successfully!", "Message");

                serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newService = new AddServiceWindow();

            if (newService.ShowDialog() == true)
            {
                var service = new Service()
                {
                    ID = IDGenerator.createID("S"),
                    Name = newService.S_Name,
                    Price = newService.S_Price,
                    isDeleted = false,
                };

                MainWindow.db.Services.Add(service);
                MainWindow.db.SaveChanges();

                serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Service selectedItem = (Service)serviceDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var newService = new EditServiceWindow(selectedItem);

                if (newService.ShowDialog() == true)
                {
                    var update = (from service in MainWindow.db.Services where service.ID == selectedItem.ID select service).Single();
                    update.Name = newService.S_Name;
                    update.Price = newService.S_Price;
                    MainWindow.db.SaveChanges();

                    serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Service selectedItem = (Service)serviceDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var delete = (from service in MainWindow.db.Services where service.ID == selectedItem.ID select service).Single();
                MainWindow.db.Services.Remove(delete);
                MainWindow.db.SaveChanges();

                serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
            }
        }

        private void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = keywordTextBox.Text;

            var data = from service in MainWindow.db.Services.ToList()
                       where service.Name.ToLower().AccentRemoved().Contains(keyword.ToLower().AccentRemoved())
                       select service;
            serviceDataGrid.ItemsSource = data;
        }

        private void serviceDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                Service selectedItem = (Service)row.Item;

                var newService = new EditServiceWindow(selectedItem);

                if (newService.ShowDialog() == true)
                {
                    var update = (from service in MainWindow.db.Services where service.ID == selectedItem.ID select service).Single();
                    update.Name = newService.S_Name;
                    update.Price = newService.S_Price;
                    MainWindow.db.SaveChanges();

                    serviceDataGrid.ItemsSource = MainWindow.db.Services.ToList();
                }
            }
        }
    }
}
