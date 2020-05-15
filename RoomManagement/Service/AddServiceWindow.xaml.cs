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
using System.Windows.Shapes;

namespace RoomManagement
{
    /// <summary>
    /// Interaction logic for AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        public string S_Name { get; set; }
        public double S_Price { get; set; }
        public AddServiceWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text) && PriceTextBox.Value > 0)
            {
                S_Name = NameTextBox.Text;
                S_Price = PriceTextBox.Value;
                this.DialogResult = true;
            }
        }
    }
}
