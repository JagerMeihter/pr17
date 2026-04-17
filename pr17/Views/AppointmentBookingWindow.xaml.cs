using pr17.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class AppointmentBookingWindow : Window
    {
        public AppointmentBookingWindow()
        {
            InitializeComponent();
            LoadMasters();
            LoadServices();
            LoadTimeSlots();
            datePicker.SelectedDate = DateTime.Today.AddDays(1); // по умолчанию завтра
        }

        private void LoadMasters()
        {
            using (var db = new AppDbContext())
            {
                var masters = db.Users
                    .Where(u => u.Role == UserRole.Master && u.IsActive)
                    .ToList();

                cmbMasters.ItemsSource = masters;
                cmbMasters.DisplayMemberPath = "FullName";
            }
        }

        private void LoadServices()
        {
            using (var db = new AppDbContext())
            {
                // Если услуг нет — создаём тестовые
                if (!db.ServiceTypes.Any())
                {
                    var services = new[]
                    {
                new ServiceType { Name = "Маникюр классический", BasePrice = 2500 },
                new ServiceType { Name = "Маникюр с дизайном", BasePrice = 3500 },
                new ServiceType { Name = "Педикюр", BasePrice = 2800 },
                new ServiceType { Name = "Окрашивание волос", BasePrice = 4500 },
                new ServiceType { Name = "Стрижка", BasePrice = 1800 },
                new ServiceType { Name = "Ламинирование ресниц", BasePrice = 2200 },
                new ServiceType { Name = "Уход за лицом", BasePrice = 3200 }
            };

                    db.ServiceTypes.AddRange(services);
                    db.SaveChanges();
                }

                var serviceList = db.ServiceTypes.ToList();
                cmbServices.ItemsSource = serviceList;
                cmbServices.DisplayMemberPath = "Name";
            }
        }

        private void LoadTimeSlots()
        {
            cmbTime.Items.Clear();
            string[] times = { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00" };
            foreach (var time in times)
                cmbTime.Items.Add(time);
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMasters.SelectedItem == null || cmbServices.SelectedItem == null ||
                datePicker.SelectedDate == null || cmbTime.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var master = cmbMasters.SelectedItem as User;
            var service = cmbServices.SelectedItem as ServiceType;
            var date = datePicker.SelectedDate.Value;
            var time = cmbTime.SelectedItem.ToString();

            DateTime appointmentTime = date.Date.Add(TimeSpan.Parse(time));

            try
            {
                using (var db = new AppDbContext())
                {
                    var appointment = new Appointment
                    {
                        ClientId = AuthService.CurrentUser.Id,
                        MasterId = master.Id,
                        ServiceTypeId = service.Id,
                        DateTime = appointmentTime,
                        PaymentMethod = PaymentMethod.Cash,
                        Comment = txtComment.Text.Trim(),
                        Status = AppointmentStatus.Scheduled
                    };

                    db.Appointments.Add(appointment);
                    db.SaveChanges();

                    MessageBox.Show($"Запись успешно создана!\n\n" +
                                   $"Мастер: {master.FullName}\n" +
                                   $"Услуга: {service.Name}\n" +
                                   $"Дата и время: {appointmentTime:dd.MM.yyyy HH:mm}",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании записи:\n{ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}