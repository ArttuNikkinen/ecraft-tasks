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
    /// Interaction logic for MeetingList.xaml
    /// </summary>
    public partial class MeetingList : Window
    {
        public MeetingList()
        {
            InitializeComponent();
        }

        private void EditMeetings_Button_Click(object sender, RoutedEventArgs e)
        {
            //First we check if a meeting has been selected
            if (MeetingsList.SelectedItem != null)
            {
                //Let's save the id of the meeting for editing
                String id = MeetingsList.SelectedItem.ToString().Substring(0, 2);
                editMeeting(id);
            }
        }

        private void editMeeting(String id)
        {
            int ID = Int32.Parse(id);

            EditMeeting edit = new EditMeeting(ID);
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to find the right meeting from the database
            //and to save the information for new form
            string stm = @"SELECT title, room, startTime, endTime
                           FROM meeting
                           WHERE id='" + ID + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                edit.meetingTitle.Text = rdr.GetString(0);
                edit.roomName.Text = rdr.GetString(1);
                edit.startTime.Text = rdr.GetString(2);
                edit.endTime.Text = rdr.GetString(3);
            }
            conn.Close();
            rdr.Close();

            edit.Show();
        }

        private void RefreshMeetings_Click(object sender, RoutedEventArgs e)
        {
            this.MeetingsList.Items.Clear();

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
                MeetingsList.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            rdr.Close();
            conn.Close();
        }

        private void DeleteMeetings_Button_Click(object sender, RoutedEventArgs e)
        {
            //First we check that a meeting has been selected
            if(MeetingsList.SelectedItem != null)
            {
                //Let's save the id of the meeting for deleting
                String id = MeetingsList.SelectedItem.ToString().Substring(0, 2);
                deleteMeeting(id);
            }
        }

        private void deleteMeeting(String id)
        {
            int ID = Int32.Parse(id);

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;


            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to delete selected meeting
            string stm = @"DELETE FROM meeting
                           WHERE id = '" + ID + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            //Executing SQL query
            int check = cmd.ExecuteNonQuery();
            conn.Close();

            if (check != 0)
            {
                MessageBox.Show("Meeting deleted succesfully! Remember to refresh the list of meetings!");
            }
        }

        private void ShowPeople_Button_Click(object sender, RoutedEventArgs e)
        {
            //First we check if a meeting has been selected
            if (MeetingsList.SelectedItem != null)
            {
                String id = MeetingsList.SelectedItem.ToString().Substring(0, 2);
                showAttendees(id);
            }
        }

        private void showAttendees(String id)
        {
            PeopleInMeetingsList.Items.Clear();
            int ID = Int32.Parse(id);
            

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to get the names of the participants
            string stm = @"SELECT participant
                           FROM attendance
                           WHERE meeting = '" + ID + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);

            //Executing SQL query
            rdr = cmd.ExecuteReader();
            
            while(rdr.Read())
            {
                PeopleInMeetingsList.Items.Add(rdr.GetString(0));
            }

            conn.Close();
            rdr.Close();
        }
    }
}
