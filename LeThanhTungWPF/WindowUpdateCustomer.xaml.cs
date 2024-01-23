using BLL;
using DAL.Models;
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
    /// Interaction logic for WindowUpdateCustomer.xaml
    /// </summary>
    public partial class WindowUpdateCustomer : Window
    {
        private readonly CustomerObject customerObject;
        private Customer _customer;
        public WindowUpdateCustomer(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            customerObject = new CustomerObject();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime? birthday;
            DateTime parsedBirthday;
            if (DateTime.TryParse(dtpBirthday.Text, out parsedBirthday))
            {
                birthday = parsedBirthday;
            }
            else
            {
                birthday = null;
            }

            Customer update = new Customer
            {
                CustomerId = int.Parse(txtId.Text),
                EmailAddress = txtEmail.Text,
                CustomerFullName = txtFullName.Text,
                Password = txtPassword.Text,
                Telephone = txtPhoneNumber.Text,
                CustomerBirthday = birthday,
                CustomerStatus = (byte)(ckbStatus.IsChecked ?? false ? 1 : 0)
            };

            if(update != null)
            {
                bool updateCustomer = await customerObject.UpdateCustomer(update);
                if(updateCustomer)
                {
                    MessageBox.Show("Update successful.");
/*
                    this.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();*/
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtId.Text = _customer.CustomerId.ToString();
            txtEmail.Text = _customer.EmailAddress;
            txtFullName.Text = _customer.CustomerFullName;
            txtPassword.Text = _customer.Password;
            txtPhoneNumber.Text = _customer.Telephone.ToString();
            dtpBirthday.Text = _customer.CustomerBirthday.ToString();
            ckbStatus.IsChecked = (_customer.CustomerStatus ==1 ) ? true : false;
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
