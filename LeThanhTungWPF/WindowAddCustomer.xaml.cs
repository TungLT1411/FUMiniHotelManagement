using BLL;
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
using System.Text.RegularExpressions;

namespace LeThanhTungWPF
{
    /// <summary>
    /// Interaction logic for WindowAddCustomer.xaml
    /// </summary>
    public partial class WindowAddCustomer : Window
    {
        private readonly CustomerObject customerObject;
        public WindowAddCustomer()
        {
            InitializeComponent();
            customerObject = new CustomerObject();  
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var Email = txtEmail.Text;
            var Password = txtPassword.Password;
            var FullName = txtFullName.Text;
            var PhoneNumber = txtPhoneNumber.Text;
            var Birthday = dtpBirthday.Text;
            byte Status = (byte)(ckbStatus.IsChecked ?? false ? 1 : 0);


            if (ValidateInput(Email, Password, FullName, PhoneNumber, Birthday))
            {
                if(DateTime.TryParse(Birthday, out var birthDay))
                {
                    customerObject.AddCustomer(Email, Password, FullName, birthDay, PhoneNumber, Status);
                    MessageBox.Show("Customer added successfully.", "Success");
                }
                else
                {
                    MessageBox.Show("Customer added failed!!! Please Try Again.", "Failed");
                }
                
            }

        }

        private bool ValidateInput(string email, string password, string fullName, string phoneNumber, string birthday)
        {
            // Kiểm tra các điều kiện validation ở đây
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Error");
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Error");
                return false;
            }

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter a full name.", "Error");
                return false;
            }


            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Chỉ cho phép nhập số
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Xác nhận giá trị là số
            if (!int.TryParse(textBox.Text, out int result))
            {
                MessageBox.Show("Vui lòng nhập một giá trị số.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Text = ""; // hoặc có thể đặt giá trị khác tùy ý
            }
        }

        private bool IsTextAllowed(string text)
        {
            // Kiểm tra xem văn bản chỉ chứa số không
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }
    }
}
