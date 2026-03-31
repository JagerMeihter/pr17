using pr17.Services;
using System;
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

            // Тестовые записи для мастера
            var testAppointments = new[]
            {
                new
                {
                    Id = 101,
                    Client = new User { FullName = "Иван Клиент" },
                    ServiceType = new ServiceType { Name = "Маникюр классический" },
                    DateTime = DateTime.Now.AddHours(3),
                    Comment = "Просит сделать дизайн звёздное небо",
                    Status = AppointmentStatus.Scheduled
                },
                new
                {
                    Id = 102,
                    Client = new User { FullName = "Мария Смирнова" },
                    ServiceType = new ServiceType { Name = "Окрашивание волос" },
                    DateTime = DateTime.Now.AddHours(6),
                    Comment = "",
                    Status = AppointmentStatus.Scheduled
                }
            };

            foreach (var appointment in testAppointments)
            {
                lvMyAppointments.Items.Add(appointment);
            }
        }

        private void BtnComplete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                // Здесь в будущем будет обновление статуса записи
                MessageBox.Show("Услуга успешно завершена!\nСтатус записи обновлён.",
                    "Выполнено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Перезагружаем список
                LoadMyAppointments();
            }
        }
    }
}