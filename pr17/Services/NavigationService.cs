using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using pr17.Models;

namespace pr17.Services
{
    public static class NavigationService
    {
        public static Frame MainFrame { get; set; }

        public static void Navigate(Page page)
        {
            MainFrame?.Navigate(page);
        }

        public static void GoBack()
        {
            if (MainFrame != null && MainFrame.CanGoBack)
                MainFrame.GoBack();
        }
    }
}
