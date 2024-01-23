using BLL;
using DAL.Models;
using DAL.ModelsDTOs;
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
    /// Interaction logic for WindowUpdateRoom.xaml
    /// </summary>
    public partial class WindowUpdateRoom : Window
    {
        private readonly RoomInformationObject roomInformationObject;
        private readonly RoomTypeObject _roomTypeObject;
        private RoomInformationDTO _room;
        public WindowUpdateRoom(RoomInformationDTO room)
        {
            InitializeComponent();
            roomInformationObject = new RoomInformationObject();
            _roomTypeObject = new RoomTypeObject();
            _room = room;
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

        private List<int> ListCapacity()
        {
            List<int> numberList = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                numberList.Add(i);
            }
            return numberList;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var roomId = _room.RoomId;
            var RoomNumber = txtRoomNumber.Text;
            var Desciption = txtDescription.Text;
            var Price = txtPrice.Text;
            var Capacity = selectedCapacity;
            var RoomTypeId = CbbRoomType.SelectedValue != null ? Convert.ToInt32(CbbRoomType.SelectedValue) : 0;
            byte Status = (byte)(ckbStatus.IsChecked ?? false ? 1 : 0);


            if (ValidateInput(RoomNumber, Desciption, RoomTypeId))
            {
                if (decimal.TryParse(Price, out var RoomPrice))
                {

                    RoomInformation newRoom = new RoomInformation
                    {
                        RoomId = roomId,
                        RoomNumber = RoomNumber,
                        RoomDetailDescription = Desciption,
                        RoomMaxCapacity = Capacity,
                        RoomPricePerDay = RoomPrice,
                        RoomTypeId = RoomTypeId,
                        RoomStatus = Status
                    };

                    var check = await roomInformationObject.UpdateRoom(newRoom);
                    if (check)
                    {
                        MessageBox.Show("Room updated successfully.", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Room updated failed!!! Please Try Again.", "Failed");
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

        private async void WindowUpdateRoom_Loaded(object sender, RoutedEventArgs e)
        {
            CbbCapacity.ItemsSource = ListCapacity();

            var RoomType = await _roomTypeObject.GetRoomTypeList();
            CbbRoomType.ItemsSource = RoomType;
            CbbRoomType.DisplayMemberPath = "RoomTypeName";
            CbbRoomType.SelectedValuePath = "RoomTypeId";

            txtRoomNumber.Text = _room.RoomNumber.ToString();
            txtDescription.Text = _room.RoomDetailDescription;
            txtPrice.Text = _room.RoomPricePerDay.ToString();
            ckbStatus.IsChecked = (_room.RoomStatus == 1) ? true : false;
            CbbCapacity.SelectedItem = _room.RoomMaxCapacity;
            CbbRoomType.SelectedItem = RoomType.FirstOrDefault(item => item.RoomTypeId == _room.RoomTypeId);
        }


    }
}
