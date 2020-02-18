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
    /// Interaction logic for AddAttendance.xaml
    /// </summary>
    public partial class AddAttendance : Window
    {
        public AddAttendance()
        {
            InitializeComponent();
        }

        private void AttendanceCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAttendance_Button_Click(object sender, RoutedEventArgs e)
        {
            addAttendanceToDB(meetingTitle.Text, personName.Text);
        }

        private void addAttendanceToDB(String meetingTitle, String participantName)
        {
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";

            //First we need to get the meeting id for attendance table
            int meetingId = 0;
            MySqlConnection connToMeeting = null;
            MySqlDataReader MeetingRdr = null;

            connToMeeting = new MySqlConnection(cs);
            connToMeeting.Open();

            //SQL-code to find the right meeting from the database
            //and to save the information of id
            string MeetingStm = @"SELECT id
                           FROM meeting
                           WHERE title = '"+meetingTitle+"'";
            MySqlCommand MeetingCmd = new MySqlCommand(MeetingStm, connToMeeting);
            MeetingRdr = MeetingCmd.ExecuteReader();
            MeetingRdr.Read();
            meetingId = MeetingRdr.GetInt32(0);
            connToMeeting.Close();
            MeetingRdr.Close();
            if (meetingId != 0)
            {
                //Now we update the attendace table with participant name
                MySqlConnection conn = null;
                conn = new MySqlConnection(cs);
                conn.Open();

                //SQL-code to add participant name into attendance table
                string stm = @"INSERT INTO attendance (meeting, participant)
                               VALUES ('"+meetingId+"','"+participantName+"')";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                int check = cmd.ExecuteNonQuery();
                conn.Close();
                if (check != 0)
                {
                    MessageBox.Show("Attendace updated succesfully!");
                }
                else
                    MessageBox.Show("Something went wrong!");
            }
            else
                MessageBox.Show("Something went wrong! Meeting could not be found!");
        }
    }
}
