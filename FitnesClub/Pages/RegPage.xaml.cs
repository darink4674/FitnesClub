using FitnesClub.Conn;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FitnesClub.Pages
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) ||
                string.IsNullOrWhiteSpace(TBFam.Text) ||
                string.IsNullOrWhiteSpace(TBPhone.Text) ||
                CBPosition.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TBPassword.Password))
            {
                MessageBox.Show("Заполните все обязательные поля!!!");
                return;
            }

            try
            {
                Employees employee = new Employees();
                employee.FirstName = TBName.Text.Trim();
                employee.LastName = TBFam.Text.Trim();
                employee.MiddleName = TBMid.Text.Trim();
                employee.Phone = TBPhone.Text.Trim();
                employee.Email = TBEmail.Text.Trim();
                employee.PassportData = TBPassport.Text.Trim();
                employee.HireDate = DateTime.Today;

                if (DPBirthDate.SelectedDate.HasValue)
                {
                    employee.BirthDate = DPBirthDate.SelectedDate.Value;
                }

                employee.Position = ((ComboBoxItem)CBPosition.SelectedItem).Content.ToString();

                if (!string.IsNullOrWhiteSpace(TBSalary.Text))
                {
                    employee.Salary = Convert.ToDecimal(TBSalary.Text);
                }

                employee.Password = TBPassword.Password.Trim();
                employee.IsActive = CBActive.IsChecked == true;

                DB.fitnessDB.Employees.Add(employee);
                DB.fitnessDB.SaveChanges();

                if (employee.Position == "Trainer")
                {
                    Trainers trainer = new Trainers();
                    trainer.EmployeeId = employee.Id;
                    trainer.TrainerType = "Both"; // хардкод
                    trainer.IsAvailableForPersonal = true; // хардкод
                    trainer.MaxPersonalClients = 10; // хардкод
                    trainer.Rating = 0;

                    DB.fitnessDB.Trainers.Add(trainer);
                    DB.fitnessDB.SaveChanges();
                }

                MessageBox.Show("Сотрудник успешно добавлен!");
                NavigationService.Navigate(new AuthPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }
    }
}