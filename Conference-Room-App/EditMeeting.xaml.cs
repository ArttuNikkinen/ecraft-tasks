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
    /// Interaction logic for EditMeeting.xaml
    /// </summary>
    public partial class EditMeeting : Window
    {
        private int id;

        public EditMeeting(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void CancelEditMeeting_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            String meetingTitle = this.meetingTitle.Text;
            String roomName = this.roomName.Text;
            String startTime = this.startTime.Text;
            String endTime = this.endTime.Text;

            //Database connection
            string cs = @"server=127.111.11.1;userid=root;password=;database=meetings";
            MySqlConnection conn = null;


            conn = new MySqlConnection(cs);
            conn.Open();

            //SQL-code to find the right meeting from the database
            //and to save the information from new form
            string stm = @"UPDATE meeting
                           SET title ='" + meetingTitle + "', room = '" + roomName + "', startTime ='" + startTime + "', endTime = '"+ endTime+"'" +
                           "WHERE id = '" + this.id + "'";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            int check = cmd.ExecuteNonQuery();
            conn.Close();

            if (check != 0)
            {
                MessageBox.Show("Meeting updated succesfully! Remember to refresh the list of meetings!");
            }
            else
                MessageBox.Show("Something went wrong!");
        }
    }
}
