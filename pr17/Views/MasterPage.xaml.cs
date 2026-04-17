using pr17.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
            LoadMasterInfo();
            LoadMyAppointments();
        }

        private void LoadMasterInfo()
        {
            if (AuthService.CurrentUser != null)
            {
                txtMasterName.Text = " — " + AuthService.CurrentUser.FullName;
            }
        }

        private void LoadMyAppointments()
        {
            lvMyAppointments.Items.Clear();

            if (AuthService.CurrentUser == null) return;

            using (var db = new AppDbContext())
            {
                var myAppointments = db.Appointments
                    .Where(a => a.MasterId == AuthService.CurrentUser.Id)
                    .OrderBy(a => a.DateTime)
                    .ToList();

                if (myAppointments.Count == 0)
                {
                    txtNoAppointments.Visibility = Visibility.Visible;
                    return;
                }

                txtNoAppointments.Visibility = Visibility.Collapsed;

                foreach (var app in myAppointments)
                {
                    lvMyAppointments.Items.Add(app);
                }
            }
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Appointment appointment)
            {
                try
                {
                    using (var db = new AppDbContext())
                    {
                        var record = db.Appointments.Find(appointment.Id);
                        if (record != null)
                        {
                            record.Status = AppointmentStatus.Completed;
                            db.SaveChanges();
                        }
                    }

                    MessageBox.Show("Услуга успешно завершена!", "Выполнено",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadMyAppointments(); // обновляем список
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении услуги:\n{ex.Message}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}