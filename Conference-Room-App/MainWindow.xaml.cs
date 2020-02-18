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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conference_Room_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Open the add person view
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson = new AddPerson();
            addPerson.Show();
        }

        //Open the add room view
        private void AddRoom_Button_Click(object sender, RoutedEventArgs e)
        {
            AddRoom addRoom = new AddRoom();
            addRoom.Show();
        }

        private void AddMeeting_Button_Click(object sender, RoutedEventArgs e)
        {
            AddMeeting addMeeting = new AddMeeting();
            addMeeting.Show();
        }

        private void ListPeople_Button_Click(object sender, RoutedEventArgs e)
        {
            PersonList personList = new PersonList();
            personList.Show();

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to select all of the people in the database for printing
            string stm = @"SELECT id, firstname, lastname, title
                           FROM person";
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            //Executing SQL query
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //Printing all of the data into the list object
                personList.PeopleList.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1) + " " + rdr.GetString(2) + " - "+ rdr.GetString(3));
            }
            rdr.Close();
            conn.Close();
        }
    

        private void ListMeetings_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingList meetingList = new MeetingList();
            meetingList.Show();

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to select all of the meetings from the dabase for printing
            string stm = @"SELECT id, title
                           FROM meeting";
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            //Executing SQL query
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //Printing all of the data into the list object
                meetingList.MeetingsList.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            rdr.Close();
            conn.Close();
        }

        private void ListRooms_Button_Click(object sender, RoutedEventArgs e)
        {
            RoomList roomList = new RoomList();
            roomList.Show();

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to select all of the rooms from the dabase for printing
            string stm = @"SELECT id, name
                           FROM room";
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            //Executing SQL query
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //Printing all of the data into the list object
                roomList.RoomsList.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            rdr.Close();
            conn.Close();
        }

        private void Attendance_Button_Click(object sender, RoutedEventArgs e)
        {
            AddAttendance addAttendance = new AddAttendance();
            addAttendance.Show();
        }
    }
}
