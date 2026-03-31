using pr17.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pr17
{
    public partial class CheckoutWindow : Window
    {
        private decimal totalAmount = 0;

        public CheckoutWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Расчёт общей суммы
            totalAmount = CartService.Items.Sum(item => item.Product.Price * item.Quantity);
            txtTotalAmount.Text = $"{totalAmount:0} ₽";

            // Устанавливаем минимальную дату — сегодня, максимальную — +7 дней
            datePicker.DisplayDateStart = DateTime.Today;
            datePicker.DisplayDateEnd = DateTime.Today.AddDays(7);
            datePicker.SelectedDate = DateTime.Today.AddDays(1); // по умолчанию завтра
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Можно добавить проверку, если нужно
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату доставки!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Здесь можно сохранить заказ в будущем (пока просто сообщение)
            string paymentMethod = (cmbPaymentMethod.SelectedItem as ComboBoxItem)?.Content.ToString();

            string message = $"Заказ успешно оформлен!\n\n" +
                             $"Дата доставки: {datePicker.SelectedDate.Value.ToShortDateString()}\n" +
                             $"Способ оплаты: {paymentMethod}\n" +
                             $"Сумма: {totalAmount:0} ₽\n\n" +
                             $"Спасибо, что выбрали Космическую ложу!";

            MessageBox.Show(message, "Заказ оформлен", MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}