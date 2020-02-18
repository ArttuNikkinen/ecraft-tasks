using MySql.Data.MySqlClient;
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

namespace Conference_Room_App
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {
        public AddRoom()
        {
            InitializeComponent();
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            addRoomToDB(roomName.Text);
        }

        private void addRoomToDB(String name)
        {
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to add room to the database
            string stm = @"INSERT INTO room (name)
                           VALUES ('" + name +"')";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            int check = cmd.ExecuteNonQuery();
            conn.Close();
            if (check != 0)
            {
                MessageBox.Show("Room added succesfully!");
            }
        }

        private void AddRoomCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }
