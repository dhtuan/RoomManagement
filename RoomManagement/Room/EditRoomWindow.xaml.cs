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
using System.Windows.Shapes;

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window
    {
        public string R_ID { get; set; }
        public string R_CategoryID { get; set; }
        public double R_Price { get; set; }
        public string R_Status { get; set; }
        public string R_Picture { get; set; }

        public EditRoomWindow(Room item)
        {
            InitializeComponent();

            CategoryComboBox.ItemsSource = MainWindow.db.Categories.ToList();
            CategoryComboBox.SelectedIndex = FindIndex(MainWindow.db.Categories.ToList(), item.CategoryID);

            PriceTextBox.Value = double.Parse(item.Price.ToString());

            var statusList = new BindingList<string>() { "Ready", "Rented" };
            StatusComboBox.ItemsSource = statusList;

            StatusComboBox.SelectedIndex = FindIndex(statusList, item.Status);


            if (item.Picture != null)
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var destinationPath = $"{baseDirectory}image\\{item.Picture}";
                Uri fileUri = new Uri(destinationPath);

                roomImage.Source = new BitmapImage(fileUri);
                R_Picture = item.Picture;
            }
            else
            {
                ChangeBtn.Content = "Browse";
            }
            this.DataContext = this;
        }

        private int FindIndex(List<Category> list, string id)
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

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null && PriceTextBox.Value > 0 && StatusComboBox.SelectedItem != null)
            {
                var temp = CategoryComboBox.SelectedItem as Category;
                R_CategoryID = temp.ID;
                R_Price = PriceTextBox.Value;
                string status = StatusComboBox.SelectedItem.ToString();
                R_Status = status;
                this.DialogResult = true;
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Image files|*.jpg;*.jpeg;*.png";

            if (screen.ShowDialog() == true)
            {
                Uri fileUri = new Uri(screen.FileName);
                roomImage.Source = new BitmapImage(fileUri);

                var imageSourceFileInfo = new FileInfo(screen.FileName);

                var uniqueName = $"{Guid.NewGuid()}.{imageSourceFileInfo.Extension}";
                R_Picture = uniqueName;

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var destinationPath = $"{baseDirectory}image\\{uniqueName}";

                File.Copy(screen.FileName, destinationPath);
            }
        }

        private int FindIndex(BindingList<string> list, string name)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (item == name)
                {
                    return i;
                }
                i++;
            }
            return i;
        }
    }
}