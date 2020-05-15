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
    /// Interaction logic for EditGuestWindow.xaml
    /// </summary>
    public partial class EditGuestWindow : Window
    {
        public string G_Name { get; set; }
        public string G_Telephone { get; set; }
        public string G_IdentityCard { get; set; }
        public EditGuestWindow(Guest item)
        {
            InitializeComponent();

            NameTextBox.Text = item.Name;
            TelephoneTextBox.Text = item.Telephone;
            IdentityCardTextBox.Text = item.IdentityCard;

            this.DataContext = this;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text) && !string.IsNullOrWhiteSpace(IdentityCardTextBox.Text) && !string.IsNullOrWhiteSpace(TelephoneTextBox.Text))
            {
                G_Name = NameTextBox.Text;
                G_Telephone = TelephoneTextBox.Text;
                G_IdentityCard = IdentityCardTextBox.Text;
                this.DialogResult = true;
            }
        }
    }
}
