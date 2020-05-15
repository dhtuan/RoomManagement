using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public string R_ID { get; set; }
        public string R_CategoryID { get; set; }
        public double R_Price { get; set; }
        public string R_Status { get; set; }
        public string R_Picture { get; set; }

        public AddRoomWindow()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = MainWindow.db.Categories.ToList();

            //var statusList = new List<string>() { "Ready", "Rented" };
            //StatusComboBox.ItemsSource = statusList;
            this.DataContext = this;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null && PriceTextBox.Value > 0 && StatusComboBox.SelectedItem != null)
            {
                var cat = CategoryComboBox.SelectedItem as Category;
                R_CategoryID = cat.ID;
                R_Price = PriceTextBox.Value;
                R_Status = StatusComboBox.Text.ToString();
                this.DialogResult = true;
            }
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Image files|*.jpg;*.jpeg;*.png";

            if (screen.ShowDialog() == true)
            {
                Uri fileUri = new Uri(screen.FileName);
                roomImage.Source = new BitmapImage(fileUri);
                BrowseBtn.Content = "Change";

                var imageSourceFileInfo = new FileInfo(screen.FileName);

                var uniqueName = $"{Guid.NewGuid()}.{imageSourceFileInfo.Extension}";
                R_Picture = uniqueName;

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var destinationPath = $"{baseDirectory}image\\{uniqueName}";

                File.Copy(screen.FileName, destinationPath);
            }
        }
    }
}