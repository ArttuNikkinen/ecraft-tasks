using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using MySql.Data.MySqlClient;


namespace Conference_Room_App
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public AddPerson()
        {
            InitializeComponent();
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            addPersonToDB(firstname.Text, lastname.Text, title.Text);
        }

        private void addPersonToDB(String firstName, String lastName, String title)
        {
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to add person to the database
            string stm = @"INSERT INTO person(firstname, lastname, title)
                           VALUES ('"+firstName+"','"+lastName+"','"+title+"')";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            int check = cmd.ExecuteNonQuery();
            conn.Close();
            if(check!=0)
            {
                MessageBox.Show("Person added succesfully!");
            }
        }

        private void AddPersonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
