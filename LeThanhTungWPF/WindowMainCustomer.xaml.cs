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
    /// Interaction logic for WindowMainCustomer.xaml
    /// </summary>
    public partial class WindowMainCustomer : Window
    {
        private readonly RoomTypeObject _roomTypeObject;
        private readonly RoomInformationObject _roomInformation;
        private readonly CustomerObject customerObject;
        private Customer _customer;
        public WindowMainCustomer(Customer account)
        {
            InitializeComponent();
            LoadImage();
            _roomTypeObject = new RoomTypeObject();
            _roomInformation = new RoomInformationObject();
            _customer = account;
            customerObject = new CustomerObject();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var RoomType = await _roomTypeObject.GetRoomTypeList();
            CbbRoomType.ItemsSource = RoomType;
            CbbRoomType.DisplayMemberPath = "RoomTypeName";
            CbbRoomType.SelectedValuePath = "RoomTypeId";

            txtEmail.Text = _customer.EmailAddress;
            txtFullName.Text = _customer.CustomerFullName;
            txtPassword.Text = _customer.Password;
            txtPhoneNumber.Text = _customer.Telephone.ToString();
            dtpBirthday.Text = _customer.CustomerBirthday.ToString();
            /*            ckbStatus.IsChecked = (_customer.CustomerStatus == 1) ? true : false;*/
        }

        private void TabControlAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void btnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            var RoomTypeId = CbbRoomType.SelectedValue != null ? Convert.ToInt32(CbbRoomType.SelectedValue) : 0;
            DateTime startDate = dpStartDate.SelectedDate.HasValue ? dpStartDate.SelectedDate.Value : DateTime.MinValue;
            DateTime endDate = dpEndDate.SelectedDate.HasValue ? dpEndDate.SelectedDate.Value : DateTime.MinValue;

            // Ensure that the end date is greater than the start date
            if (endDate < startDate)
            {
                MessageBox.Show("End date must be greater than or equal to start date", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var roomInfoList = await _roomInformation.GetRoomListAvailable(startDate, endDate, RoomTypeId);
            roomInfoDataGrid.ItemsSource = roomInfoList;
        }

        private void LoadImage()
        {
            try
            {
                // Specify the path to your image file
                string imagePath = @"C:\Users\ROG\Documents\GitHub\FUMiniHotelManagement\Image\account.jpg";

                // Create a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();

                // Set the BitmapImage as the Source of the Image control
                imgAccount.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                CustomerId = _customer.CustomerId,
                EmailAddress = txtEmail.Text,
                CustomerFullName = txtFullName.Text,
                Password = txtPassword.Text,
                Telephone = txtPhoneNumber.Text,
                CustomerBirthday = birthday,
            };

            if (update != null)
            {
                bool updateCustomer = await customerObject.UpdateCustomer(update);
                if (updateCustomer)
                {
                    MessageBox.Show("Update successful.");
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
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

