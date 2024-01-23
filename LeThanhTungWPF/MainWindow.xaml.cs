using BLL;
using DAL.Interfaces;
using DAL.Models;
using DAL.ModelsDTOs;
using DAL.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeThanhTungWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CustomerObject customerObject;
        private readonly RoomInformationObject roomInformation;
        private readonly BookingReservationObject bookingReservationObject;
        public MainWindow()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
            roomInformation = new RoomInformationObject();
            bookingReservationObject = new BookingReservationObject();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ICollection<Customer> customers = await customerObject.GetCustomerList();
            customerDataGrid.ItemsSource = customers;
            
            var roomInfoList = await roomInformation.GetRoomListWithType();
            roomInfoDataGrid.ItemsSource = roomInfoList;
            
            var orderList = await bookingReservationObject.GetOrderList();
            orderDataGrid.ItemsSource = orderList;


        }

        private void customerDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchKey = txtSearchCutomer.Text.Trim();
            ICollection<Customer> customers = await customerObject.SearchCustomer(searchKey);
            customerDataGrid.ItemsSource = customers;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            WindowAddCustomer windowAddCustomer = new WindowAddCustomer();
            windowAddCustomer.Show();
            this.Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)customerDataGrid.SelectedItem;
            if(customer == null)
            {
                MessageBox.Show("please choice to update!");
            }
            else
            {
                WindowUpdateCustomer updateCustomer = new WindowUpdateCustomer(customer);
                updateCustomer.Show();
                this.Close();
            }
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = (Customer)customerDataGrid.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("please choice to update!");
            }
            else
            {
                var check = await customerObject.RemoveCustomer(customer.CustomerId);
                if (check)
                {
                    MessageBox.Show("Delete successfully !!");
                    var updatedCustomerList = await customerObject.GetCustomerList();
                    customerDataGrid.ItemsSource = updatedCustomerList;
                }
                else
                {
                    MessageBox.Show("Delete Failed !!");
                }
            }
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            WindowAddRoom windowAddRoom = new WindowAddRoom();
            windowAddRoom.Show();
            this.Close();
        }

        private void ButtonUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomInformationDTO room = (RoomInformationDTO)roomInfoDataGrid.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("please choice to update!");
            }
            else
            {
                WindowUpdateRoom updateRoom = new WindowUpdateRoom(room);
                updateRoom.Show();
                this.Close();
            }
        }

        private async void ButtonDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomInformationDTO room = (RoomInformationDTO)roomInfoDataGrid.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("please choice to update!");
            }
            else
            {
                var check = await roomInformation.RemoveRoom(room.RoomId);
                if (check)
                {
                    MessageBox.Show("Delete successfully !!");
                    var updatedRoomList = await roomInformation.GetRoomListWithType();
                    roomInfoDataGrid.ItemsSource = updatedRoomList;
                }
                else
                {
                    MessageBox.Show("Delete Failed !!");
                }
            }
        }

        private async void btnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            var keySearch = txtSearchRoomInfo.Text.Trim();
            var roomSearch = await roomInformation.SearchRoom(keySearch);
            roomInfoDataGrid.ItemsSource = roomSearch;
        }
    }
}
