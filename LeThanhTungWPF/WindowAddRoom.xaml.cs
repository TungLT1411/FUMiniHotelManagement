using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeThanhTungWPF
{
    /// <summary>
    /// Interaction logic for WindowAddRoom.xaml
    /// </summary>
    public partial class WindowAddRoom : Window
    {
        private readonly RoomTypeObject _roomTypeObject;
        private readonly RoomInformationObject _roomInformationObject;
        public WindowAddRoom()
        {
            InitializeComponent();
            _roomTypeObject = new RoomTypeObject();
            _roomInformationObject = new RoomInformationObject();
        }

        private int selectedCapacity;
        private void CbbCapacity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbbCapacity.SelectedItem != null)
            {
                selectedCapacity = (int)CbbCapacity.SelectedItem;
            }
            else
            {
                MessageBox.Show("Please select a capacity.", "Error");
            }
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var RoomNumber = txtRoomNumber.Text;
            var Desciption = txtDescription.Text;
            var Price = txtPrice.Text;
            var Capacity = selectedCapacity;
            var RoomTypeId = CbbRoomType.SelectedValue != null ? Convert.ToInt32(CbbRoomType.SelectedValue) : 0;
            byte Status = (byte)(ckbStatus.IsChecked ?? false ? 1 : 0);


            if (ValidateInput(RoomNumber, Desciption, RoomTypeId))
            {
                if(decimal.TryParse(Price, out var RoomPrice))
                {
                    
                    RoomInformation newRoom = new RoomInformation
                    {
                        RoomNumber = RoomNumber,
                        RoomDetailDescription = Desciption,
                        RoomMaxCapacity = Capacity,
                        RoomPricePerDay = RoomPrice,
                        RoomTypeId = RoomTypeId,
                        RoomStatus = Status
                    };
                    
                    var check = await _roomInformationObject.AddRoom(newRoom);
                    if (check)
                    {
                        MessageBox.Show("Room added successfully.", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Room added failed!!! Please Try Again.", "Failed");
                    }
                }
            }
        }

        private bool ValidateInput(string RoomNumber, string Description, int RoomTypeId)
        {
            if (string.IsNullOrEmpty(RoomNumber))
            {
                MessageBox.Show("Please enter a RoomNumber.", "Error");
                return false;
            }

            if (string.IsNullOrEmpty(Description))
            {
                MessageBox.Show("Please enter a description.", "Error");
                return false;
            }
            if (RoomTypeId == 0)
            {
                MessageBox.Show("Please select a room type.", "Error");
                return false;
            }

            return true;
        }

        private List<int> ListCapacity()
        {
            List<int> numberList = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                numberList.Add(i);
            }
            return numberList;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CbbCapacity.ItemsSource = ListCapacity();

            var RoomType = await _roomTypeObject.GetRoomTypeList();
            CbbRoomType.ItemsSource = RoomType;
            CbbRoomType.DisplayMemberPath = "RoomTypeName";
            CbbRoomType.SelectedValuePath = "RoomTypeId";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            var tabControl = mainWindow.FindName("TabControlAdmin") as TabControl;

            if (tabControl != null && tabControl.Items.Count >= 2)
            {
                // Chuyển đến tab thứ hai
                tabControl.SelectedIndex = 1;
            }

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
