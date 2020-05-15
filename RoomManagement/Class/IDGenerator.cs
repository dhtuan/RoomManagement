using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement
{
    class IDGenerator
    {
        public static string createID(string prefix)
        {
            var count = 0;

            if (prefix == "R")
            {
                if (MainWindow.db.Rooms.Count() > 0)
                {
                    count = int.Parse(MainWindow.db.Rooms.ToList()[MainWindow.db.Rooms.Count() - 1].ID.ToString().Substring(1, 3));
                }
            }
            else if (prefix == "CT")
            {
                if (MainWindow.db.Categories.Count() > 0)
                {
                    count = int.Parse(MainWindow.db.Categories.ToList()[MainWindow.db.Categories.Count() - 1].ID.ToString().Substring(2, 3));
                }
            }
            else if (prefix == "G")
            {
                if (MainWindow.db.Guests.Count() > 0)
                {
                    count = int.Parse(MainWindow.db.Guests.ToList()[MainWindow.db.Guests.Count() - 1].ID.ToString().Substring(1, 3));
                }    
            }
            else if (prefix == "S")
            {
                if (MainWindow.db.Services.Count() > 0)
                {
                    count = int.Parse(MainWindow.db.Services.ToList()[MainWindow.db.Services.Count() - 1].ID.ToString().Substring(1, 3));
                }
            }

            return prefix + (count + 1).ToString("000");
        }
    }
}
