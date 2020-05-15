using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement
{
    public class Notify : INotifyPropertyChanged
    {
        private bool _categoryChange = false;

        public bool CategoryChange
        {
            get => _categoryChange;
            set
            {
                _categoryChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_categoryChange"));
                }
            }
        }

        private bool _roomChange = false;

        public bool RoomChange
        {
            get => _roomChange;
            set
            {
                _roomChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_roomChange"));
                }
            }
        }

        private bool _guestChange = false;

        public bool GuestChange
        {
            get => _guestChange;
            set
            {
                _guestChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_guestChange"));
                }
            }
        }

        private bool _reservationChange = false;

        public bool ReservationChange
        {
            get => _reservationChange;
            set
            {
                _reservationChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_reservationChange"));
                }
            }
        }

        private bool _serviceChange = false;

        public bool ServiceChange
        {
            get => _serviceChange;
            set
            {
                _serviceChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_serviceChange"));
                }
            }
        }

        private bool _reservationDetailChange = false;

        public bool ReservationDetailChange
        {
            get => _reservationDetailChange;
            set
            {
                _reservationDetailChange = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("_reservationDetailChange"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
