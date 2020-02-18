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
    /// Interaction logic for AddMeeting.xaml
    /// </summary>
    public partial class AddMeeting : Window
    {
        public AddMeeting()
        {
            InitializeComponent();
        }

        private void AddMeetingClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            //This part of the code is commented because DateTime in System and DateTime in MySQL
            //behave in a weird fashion...

            //DateTime StartTime = DateTime.ParseExact(startTime.Text, "yyyy-MM-dd HH:mm", null);
            //DateTime EndTime = DateTime.ParseExact(endTime.Text, "yyyy-MM-dd HH:mm", null);

            addMeetingToDB(meetingTitle.Text, meetingRoom.Text, startTime.Text, endTime.Text);
        }

        private void addMeetingToDB(String title, String room, String startTime, String endTime)
        {
            //Before adding a new meeting, need to check if given room exists in the database
            if(checkRoom(room))
            {
                //Database connection
                string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
                MySqlConnection conn = null;

                conn = new MySqlConnection(cs);
                conn.Open();

                //SQL-code to add meeting to the database
                string stm = @"INSERT INTO meeting(title, room, startTime, endTime)
                           VALUES ('" + title + "','" + room + "','" + startTime + "','" + endTime + "')";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                int check = cmd.ExecuteNonQuery();
                conn.Close();
                if (check != 0)
                {
                    MessageBox.Show("Meeting added succesfully!");
                }
                
                //Here we add the meeting to attendance-table
                addToAttendaceDB(title, room);
            }

  
        }

        private Boolean checkRoom(String room)
        {
            bool check = true;
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to get of the room
            string stm = @"SELECT id
                           FROM room
                           WHERE name ='"+room+"'";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            //Need to let rdr check database
           if( rdr.Read() == false)
             {
               MessageBox.Show("That room does not exist!");
                check = false;
             }

            rdr.Close();
            conn.Close();
            return check;
        }

        private void addToAttendaceDB(String meetingTitle, String roomName)
        {
            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";

            //Before we add the meeting information to attendance-table
            //we need to get meeting id and room id for the table.
            int roomId, meetingId;

            //First meeting id
            MySqlConnection connToMeeting = null;
            MySqlDataReader MeetingRdr = null;
            connToMeeting = new MySqlConnection(cs);
            connToMeeting.Open();

            string queryForMeeting = @"SELECT id
                                       FROM meeting
                                       WHERE title = '"+meetingTitle+"'";
            MySqlCommand MeetingCmd = new MySqlCommand(queryForMeeting, connToMeeting);
            MeetingRdr = MeetingCmd.ExecuteReader();
            MeetingRdr.Read();
            meetingId = MeetingRdr.GetInt32(0);
            connToMeeting.Close();
            MeetingRdr.Close();

            //Then room id
            MySqlConnection connToRoom = null;
            MySqlDataReader RoomRdr = null;
            connToRoom = new MySqlConnection(cs);
            connToRoom.Open();

            string queryForRoom = @"SELECT id
                                       FROM room
                                       WHERE name = '" + roomName + "'";
            MySqlCommand RoomCmd = new MySqlCommand(queryForRoom, connToRoom);
            RoomRdr = RoomCmd.ExecuteReader();
            RoomRdr.Read();
            roomId = RoomRdr.GetInt32(0);
            connToRoom.Close();
            RoomRdr.Close();

            //Here we send the data to the table
            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to add attendance to the database
            string stm = @"INSERT INTO attendance (room, meeting)
                           VALUES ('" + roomId + "', '" + meetingId + "')";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
    }
