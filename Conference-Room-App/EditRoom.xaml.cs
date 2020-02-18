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
    /// Interaction logic for EditRoom.xaml
    /// </summary>
    public partial class EditRoom : Window
    {
        private int id;

        public EditRoom(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void EditRoomCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditRoomButton_Click(object sender, RoutedEventArgs e)
        {
            String name = this.roomName.Text;

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;


            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to find the right room from the database
            //and to save the information for new form
            string stm = @"UPDATE room
                           SET name ='" + name + "' " +
                           "WHERE id = '" + this.id + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            int check = cmd.ExecuteNonQuery();
            conn.Close();

            if (check != 0)
            {
                MessageBox.Show("Room updated succesfully! Remember to refresh the list of rooms!");
            }
            else
                MessageBox.Show("Something went wrong!");
        }
    }
}
