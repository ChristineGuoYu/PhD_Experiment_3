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
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Mixed_Initiative_Exp1_v0._1
{
    /// <summary>
    /// Interaction logic for Table_UnsureMessages.xaml
    /// </summary>
    /// 

    public partial class Table_UnsureMessages : UserControl
    {
        public System.Windows.Forms.Timer uiTimer1 = new System.Windows.Forms.Timer();
        public int interval = 1000;
        public Table_UnsureMessages()
        {
            InitializeComponent();
            var main = App.Current.MainWindow as MainWindow;
            //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            //MySqlConnection myConn = new MySqlConnection(myConnection);

            //myConn.Open();
            // MessageBox.Show("Connected");
            MySqlCommand cmd = new MySqlCommand("SELECT id_unsure_messages, time, content, note, is_about_delivery, is_manually_tagged FROM unsure_messages", main.myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            
            adp.Fill(ds, "LoadDataBinding_Unsure");

            /*
            if (Convert.ToString(ds.Tables[0].Rows[0][3]) == "True")
            {
                ds.Tables[0].Rows[0][3] = "It is about delivery.";
            }
            else
            {
                ds.Tables[0].Rows[0][3] = "It is NOT about delivery.";
            }

            ds.Tables[0].AcceptChanges();
            */


            DG1.DataContext = ds;
            //main.myConn.Close();
        }

        void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Get the DataRow corresponding to the DataGridRow that is loading.
            DataRowView item = e.Row.Item as DataRowView;

            if (item != null)
            {
                DataRow row = item.Row;

                // Access cell values values if needed...
                // var colValue = row["ColumnName1]";
                // var colValue2 = row["ColumName2]";

                // Set the background color of the DataGrid row based on whatever data you like from 
                // the row.
                var colValue = row["is_about_delivery"];
                if (Convert.ToString(colValue) == "True")
                {
                    e.Row.Background = new SolidColorBrush(Colors.LightCyan);
                    row["note"] = "It is about delivery.";
                }
                else if (Convert.ToString(colValue) == "False")
                {
                    e.Row.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    row["note"] = "It is NOT about delivery.";
                }
                else
                {
                    e.Row.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Eureka, it's checked!");
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox) as DataGridRow;
            if (dataGridRow != null)
            {
                dataGridRow.Background = new SolidColorBrush(Colors.LightCyan);

                DataRowView row = (DataRowView)dataGridRow.Item;
                var id_unsure_messages = row["id_unsure_messages"]; int id_unsure_messages_num = Convert.ToInt32(id_unsure_messages);
                var is_about_delivery = row["is_about_delivery"]; bool bool_Value = Convert.ToBoolean(is_about_delivery);

                //MessageBox.Show(id_all_num.ToString()+" "+ bool_Value.ToString());

                /*
                DataRowView row = (DataRowView)dataGridRow.Item;

                    // Set the background color of the DataGrid row based on whatever data you like from 
                    // the row.
                    var colValue = row["bool"];
                    if (Convert.ToString(colValue) == "True")
                    {
                    dataGridRow.Background = new SolidColorBrush(Colors.LightCyan);
                    }
                    else
                    {
                    dataGridRow.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    }
                    */
                e.Handled = true;

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();
                // MessageBox.Show("Connected");
                var main = App.Current.MainWindow as MainWindow;

                MySqlCommand cmd = new MySqlCommand("UPDATE unsure_messages SET is_about_delivery='1' WHERE id_unsure_messages='" + id_unsure_messages_num.ToString() + "'", main.myConn);
                cmd.ExecuteNonQuery();
                //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                //myConn.Close();

            }

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Eureka, it's unchecked!");
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox) as DataGridRow;
            if (dataGridRow != null)
            {
                dataGridRow.Background = new SolidColorBrush(Colors.WhiteSmoke);

                DataRowView row = (DataRowView)dataGridRow.Item;
                var id_unsure_messages = row["id_unsure_messages"]; int id_unsure_messages_num = Convert.ToInt32(id_unsure_messages);
                var is_about_delivery = row["is_about_delivery"]; bool bool_Value = Convert.ToBoolean(is_about_delivery);

                // MessageBox.Show(id_all_num.ToString() + " " + bool_Value.ToString());

                /*
                DataRowView row = (DataRowView)dataGridRow.Item;

                // Set the background color of the DataGrid row based on whatever data you like from 
                // the row.
                var colValue = row["bool"];
                if (Convert.ToString(colValue) == "True")
                {
                    dataGridRow.Background = new SolidColorBrush(Colors.LightCyan);
                }
                else
                {
                    dataGridRow.Background = new SolidColorBrush(Colors.WhiteSmoke);
                }
                */
                e.Handled = true;

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();
                // MessageBox.Show("Connected");
                var main = App.Current.MainWindow as MainWindow;

                MySqlCommand cmd = new MySqlCommand("UPDATE unsure_messages SET is_about_delivery='0' WHERE id_unsure_messages='" + id_unsure_messages_num.ToString() + "'", main.myConn);
                cmd.ExecuteNonQuery();
                //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                //myConn.Close();
            }
        }

       
        private void correct_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;
            main.count_user_click++;
            if (main.current_task_type == "Aligned")
            {
                // if (main.count_user_click < 30)
                if (main.count_user_click < 10)
                {
                    main.user_respond[main.count_user_click] = System.DateTime.Now;
                    write_subtasktime(main.user_respond[main.count_user_click], main.current_task_type, main.count_user_click, "User", "Confirm_system", DG1.Items.Count);

                    if (main.count_user_click == main.count_trigger)
                    {
                        main.push_respond_interval[main.count_trigger] = main.user_respond[main.count_user_click] - main.system_push[main.count_trigger];
                        main.interval_ms[main.count_trigger] = Convert.ToInt32(main.push_respond_interval[main.count_trigger].TotalMilliseconds) + main.t0;
                    }
                    else
                    {
                        main.push_respond_interval[main.count_user_click] = main.user_respond[main.count_user_click] - main.system_push[main.count_user_click];
                        main.interval_ms[main.count_user_click] = Convert.ToInt32(main.push_respond_interval[main.count_user_click].TotalMilliseconds) + main.t0;
                    }

                    if (main.count_trigger == 1)
                    {
                        main.uiTimer_aligned.Interval = main.t0;
                        main.uiTimer_aligned.Tick += main.Aligned_Timer_PushMessages;
                        main.uiTimer_aligned.Start();
                        main.Aligned_timer_flag = 1;
                        //main.update_count_trigger_and_messages();
                    }
                }
                else 
                {
                    main.user_respond[main.count_user_click] = System.DateTime.Now;
                    write_subtasktime(main.user_respond[main.count_user_click], main.current_task_type, main.count_user_click, "User", "Confirm_system", DG1.Items.Count);
                    //end of task
                    main.Unsure_Messages.Visibility = main.Status_Bar.Visibility = System.Windows.Visibility.Hidden;
                    main.Start_Aligned_Timer.Visibility = main.Start_Random_Timer.Visibility = main.Start_Rhythmic_Timer.Visibility = main.Start_Manual.Visibility = main.Stop_Aligned_Timer.Visibility = main.Stop_Random_Timer.Visibility = main.Stop_Rhythmic_Timer.Visibility = main.Next_btn.Visibility = System.Windows.Visibility.Hidden;
                    main.Task_Ratings.Visibility = System.Windows.Visibility.Visible;

                }

            }
            else
            {
                write_subtasktime(System.DateTime.Now, main.current_task_type, main.count_user_click, "User", "Confirm_system", DG1.Items.Count);
                //if (main.count_user_click == 30)
                if (main.count_user_click == 10)
                {
                    main.Unsure_Messages.Visibility = main.Status_Bar.Visibility = System.Windows.Visibility.Hidden;
                    main.Start_Aligned_Timer.Visibility = main.Start_Random_Timer.Visibility = main.Start_Rhythmic_Timer.Visibility = main.Start_Manual.Visibility= main.Stop_Aligned_Timer.Visibility = main.Stop_Random_Timer.Visibility = main.Stop_Rhythmic_Timer.Visibility = main.Next_btn.Visibility = System.Windows.Visibility.Hidden;
                    main.Task_Ratings.Visibility = System.Windows.Visibility.Visible;
                }
               
            }

            Button button = (Button)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(button) as DataGridRow;
            if (dataGridRow != null)
            {
               // dataGridRow.Background = new SolidColorBrush(Colors.WhiteSmoke);

                DataRowView row = (DataRowView)dataGridRow.Item;
                var id_unsure_messages = row["id_unsure_messages"]; int id_unsure_messages_num = Convert.ToInt32(id_unsure_messages);
                var is_about_delivery = row["is_about_delivery"]; bool bool_Value = Convert.ToBoolean(is_about_delivery);

                e.Handled = true;

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                if (bool_Value == true)
                {
                    //add yes messages in unsure_messages table to archived_yes_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100
                    string cmd_archive_yes_string = "INSERT INTO archived_yes_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 1 and id_unsure_messages = " + id_unsure_messages_num.ToString();

                    MySqlCommand cmd_archive_yes = new MySqlCommand(cmd_archive_yes_string, main.myConn);
                    cmd_archive_yes.ExecuteNonQuery();

                    //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_yes_messages table
                    MySqlCommand cmd_update_archive_yes = new MySqlCommand("UPDATE archived_yes_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", main.myConn);
                    cmd_update_archive_yes.ExecuteNonQuery();

                }
                else
                {
                    //add NO messages in unsure_messages table to archived_no_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100

                    string cmd_archive_no_string = "INSERT INTO archived_no_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 0 and id_unsure_messages = " + id_unsure_messages_num.ToString();
                    MySqlCommand cmd_archive_no = new MySqlCommand(cmd_archive_no_string, main.myConn);
                    cmd_archive_no.ExecuteNonQuery();

                    //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_no_messages table 
                    MySqlCommand cmd_update_archive_no = new MySqlCommand("UPDATE archived_no_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", main.myConn);
                    cmd_update_archive_no.ExecuteNonQuery();

                }
                
                //archive yes/no, refresh unsure

                string cmd5_string = "DELETE FROM unsure_messages WHERE id_unsure_messages = " + id_unsure_messages_num.ToString();
                MySqlCommand cmd5 = new MySqlCommand(cmd5_string, main.myConn);

                //MySqlCommand cmd5 = new MySqlCommand("TRUNCATE TABLE unsure_messages ", myConn);
                cmd5.ExecuteNonQuery();

                MySqlCommand cmd_refresh_unsure = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", main.myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd_refresh_unsure);

                DataSet ds = new DataSet();
                adp3.Fill(ds, "LoadDataBinding_Unsure");
                main.Unsure_Messages.DG1.Items.Refresh();
                main.Unsure_Messages.DG1.DataContext = ds;

            }
            //if (main.count_user_click == 30)
            if (main.count_user_click == 10)
            {
                uiTimer1.Tick -= HideUpdatingLabel;
                uiTimer1.Stop();
                main.updating_label.Visibility = System.Windows.Visibility.Hidden;
            }
            else {
                if (uiTimer1.Enabled)
                {
                    uiTimer1.Tick -= HideUpdatingLabel;
                    uiTimer1.Stop();
                    Random rnd = new Random();
                    interval = rnd.Next(500, 1001);
                    main.updating_label.Visibility = System.Windows.Visibility.Visible;
                    uiTimer1.Interval = interval;
                    uiTimer1.Tick += HideUpdatingLabel;
                    uiTimer1.Start();

                }
                else
                {
                    Random rnd = new Random();
                    interval = rnd.Next(500, 1001);
                    main.updating_label.Visibility = System.Windows.Visibility.Visible;
                    uiTimer1.Interval = interval;
                    uiTimer1.Tick += HideUpdatingLabel;
                    uiTimer1.Start();
                }
            }
        }

        private void wrong_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;
            main.count_user_click++;
            if (main.current_task_type == "Aligned")
            {
                //if (main.count_user_click < 30)
                if (main.count_user_click < 10)
                    {
                    main.user_respond[main.count_user_click] = System.DateTime.Now;
                    write_subtasktime(main.user_respond[main.count_user_click], main.current_task_type, main.count_user_click, "User", "Deny_system", DG1.Items.Count);

                    if (main.count_user_click == main.count_trigger)
                    {
                        main.push_respond_interval[main.count_trigger] = main.user_respond[main.count_user_click] - main.system_push[main.count_trigger];
                        main.interval_ms[main.count_trigger] = Convert.ToInt32(main.push_respond_interval[main.count_trigger].TotalMilliseconds) + main.t0;
                    }
                    else
                    {
                        main.push_respond_interval[main.count_user_click] = main.user_respond[main.count_user_click] - main.system_push[main.count_user_click];
                        main.interval_ms[main.count_user_click] = Convert.ToInt32(main.push_respond_interval[main.count_user_click].TotalMilliseconds) + main.t0;
                    }

                    if (main.count_trigger == 1)
                    {
                        main.uiTimer_aligned.Interval = main.t0;
                        main.uiTimer_aligned.Tick += main.Aligned_Timer_PushMessages;
                        main.uiTimer_aligned.Start();
                        main.Aligned_timer_flag = 1;
                        //main.update_count_trigger_and_messages();
                    }
                }
                else
                {
                    main.user_respond[main.count_user_click] = System.DateTime.Now;
                    write_subtasktime(main.user_respond[main.count_user_click], main.current_task_type, main.count_user_click, "User", "Deny_system", DG1.Items.Count);
                    //end of task
                    main.Unsure_Messages.Visibility = main.Status_Bar.Visibility = System.Windows.Visibility.Hidden;
                    main.Start_Aligned_Timer.Visibility = main.Start_Random_Timer.Visibility = main.Start_Rhythmic_Timer.Visibility = main.Start_Manual.Visibility = main.Stop_Aligned_Timer.Visibility = main.Stop_Random_Timer.Visibility = main.Stop_Rhythmic_Timer.Visibility = main.Next_btn.Visibility = System.Windows.Visibility.Hidden;
                    main.Task_Ratings.Visibility = System.Windows.Visibility.Visible;

                }

            }
            else
            {
                write_subtasktime(System.DateTime.Now, main.current_task_type, main.count_user_click, "User", "Deny_system", DG1.Items.Count);

               // if (main.count_user_click == 30)
                if (main.count_user_click == 10)
                {
                    main.Unsure_Messages.Visibility = main.Status_Bar.Visibility = System.Windows.Visibility.Hidden;
                    main.Start_Aligned_Timer.Visibility = main.Start_Random_Timer.Visibility = main.Start_Rhythmic_Timer.Visibility = main.Start_Manual.Visibility = main.Stop_Aligned_Timer.Visibility = main.Stop_Random_Timer.Visibility = main.Stop_Rhythmic_Timer.Visibility = main.Next_btn.Visibility = System.Windows.Visibility.Hidden;
                    main.Task_Ratings.Visibility = System.Windows.Visibility.Visible;

                }

            }

            Button button = (Button)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(button) as DataGridRow;
            if (dataGridRow != null)
            {
                DataRowView row = (DataRowView)dataGridRow.Item;
                var id_unsure_messages = row["id_unsure_messages"]; int id_unsure_messages_num = Convert.ToInt32(id_unsure_messages);
                var is_about_delivery = row["is_about_delivery"]; bool bool_Value = Convert.ToBoolean(is_about_delivery);

                e.Handled = true;

                string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                MySqlConnection myConn = new MySqlConnection(myConnection);

                myConn.Open();

                if (bool_Value == true)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE unsure_messages SET is_about_delivery='0' WHERE id_unsure_messages='" + id_unsure_messages_num.ToString() + "'", myConn);
                    cmd.ExecuteNonQuery();

                    //add NO messages in unsure_messages table to archived_no_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100

                    string cmd_archive_no_string = "INSERT INTO archived_no_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 0 and id_unsure_messages = " + id_unsure_messages_num.ToString();
                    MySqlCommand cmd_archive_no = new MySqlCommand(cmd_archive_no_string, myConn);
                    cmd_archive_no.ExecuteNonQuery();

                    //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_no_messages table 
                    MySqlCommand cmd_update_archive_no = new MySqlCommand("UPDATE archived_no_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", myConn);
                    cmd_update_archive_no.ExecuteNonQuery();

                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE unsure_messages SET is_about_delivery='1' WHERE id_unsure_messages='" + id_unsure_messages_num.ToString() + "'", myConn);
                    cmd.ExecuteNonQuery();

                    //add yes messages in unsure_messages table to archived_yes_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100
                    string cmd_archive_yes_string = "INSERT INTO archived_yes_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 1 and id_unsure_messages = " + id_unsure_messages_num.ToString();
                    MySqlCommand cmd_archive_yes = new MySqlCommand(cmd_archive_yes_string, myConn);
                    cmd_archive_yes.ExecuteNonQuery();

                    //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_yes_messages table
                    MySqlCommand cmd_update_archive_yes = new MySqlCommand("UPDATE archived_yes_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", myConn);
                    cmd_update_archive_yes.ExecuteNonQuery();

                }

                //archive yes/no, refresh unsure

                //MySqlCommand cmd5 = new MySqlCommand("TRUNCATE TABLE unsure_messages ", myConn);
                string cmd5_string = "DELETE FROM unsure_messages WHERE id_unsure_messages = " + id_unsure_messages_num.ToString();
                MySqlCommand cmd5 = new MySqlCommand(cmd5_string, myConn);
                cmd5.ExecuteNonQuery();

                MySqlCommand cmd_refresh_unsure = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd_refresh_unsure);

                DataSet ds = new DataSet();
                adp3.Fill(ds, "LoadDataBinding_Unsure");
                
                main.Unsure_Messages.DG1.Items.Refresh();
                main.Unsure_Messages.DG1.DataContext = ds;
         
                myConn.Close();
            }
            //if (main.count_user_click == 30)
            if (main.count_user_click == 10)
             {
                uiTimer1.Tick -= HideUpdatingLabel;
                uiTimer1.Stop();
                main.updating_label.Visibility = System.Windows.Visibility.Hidden;
            }
            else {

                if (uiTimer1.Enabled)
                {
                    uiTimer1.Tick -= HideUpdatingLabel;
                    uiTimer1.Stop();
                    Random rnd = new Random();
                    interval = rnd.Next(500, 1001);
                    main.updating_label.Visibility = System.Windows.Visibility.Visible;
                    uiTimer1.Interval = interval;
                    uiTimer1.Tick += HideUpdatingLabel;
                    uiTimer1.Start();

                }
                else
                {
                    Random rnd = new Random();
                    interval = rnd.Next(500, 1001);
                    main.updating_label.Visibility = System.Windows.Visibility.Visible;
                    uiTimer1.Interval = interval;
                    uiTimer1.Tick += HideUpdatingLabel;
                    uiTimer1.Start();
                }
            }
        }

        public void HideUpdatingLabel(object sender, EventArgs e)
        {
            if (uiTimer1.Enabled)
            {
                var main = App.Current.MainWindow as MainWindow;

                //submit.IsEnabled = true;
                main.updating_label.Visibility = System.Windows.Visibility.Hidden;

            }
            uiTimer1.Tick -= HideUpdatingLabel;
            uiTimer1.Stop();
        }




        private void write_subtasktime(DateTime dt, string taskType, int taskNum, string initiator, string behaviour, int accumulated_messages )
        {
            var main = App.Current.MainWindow as MainWindow;


            var dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Environment.CurrentDirectory));

            string file = dir.Parent.FullName + @"\TXT\output.txt";

            StreamWriter sr;


            sr = File.AppendText(file);

            sr.WriteLine(dt.ToString("hh:mm:ss.fff") + "\t" + taskType + "\t" + taskNum + "\t" + initiator + "\t" + behaviour + "\t" + accumulated_messages.ToString());//将传入的字符串加上时间写入文本文件一行

            sr.Close();
        }

    }
}
