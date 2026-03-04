using FitnesClub.Conn;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnesClub.Pages
{
    public partial class AuthPage : Page
    {
        public Employees employees1 {  get; set; }
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(tbLog.Text.Trim().Length != 0 && tbPass.Password.Trim().Length != 0)
            {
                string Email = tbLog.Text.Trim();
                string Password = tbPass.Password.Trim();
                employees1 = DB.fitnessDB.Employees.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            }

            if(employees1 != null)
            {
                NavigationService.Navigate(new NavigationPage(employees1));
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

    }//изменения 03.03.2026
}
