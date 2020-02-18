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
    /// Interaction logic for EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        private int id;

        public EditPerson(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void EditPersonButton_Click(object sender, RoutedEventArgs e)
        {
            String firstName = this.firstname.Text;
            String lastName = this.lastname.Text;
            String title = this.title.Text;

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;


            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to find the right person from the database
            //and to save the information for new form
            string stm = @"UPDATE person
                           SET firstname ='" + firstName + "', lastname = '" + lastName + "', title ='"+ title +"' " +
                           "WHERE id = '" + this.id + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            int check = cmd.ExecuteNonQuery();
            conn.Close();

            if (check != 0)
            {
                MessageBox.Show("Person updated succesfully! Remember to refresh the list of people!");
            }
            else
                MessageBox.Show("Something went wrong!");
        }

        private void EditPersonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
