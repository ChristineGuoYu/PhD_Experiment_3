using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;using System.ComponentModel;
using System.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Timers;
using System.Windows.Media.Animation;


namespace Mixed_Initiative_Exp1_v0._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int task_type = 0;
        public int count_trigger = 0;
        public int count_messages = 0;
        public int count_user_click = 0;

        public int range_low = 1;
        public int range_high = 10;

        public int[,] range_num = new int[500, 2];
        public string current_selection_range="";
        public string from_table_range_string="";
        //two-dimension number array, '500' stores the counting of subtasks, '2' stores task_type (1,2,3,4) and the number of messages needed to be processed in the all_messages table

        public string current_task_type = "";
        public int[] taskFlag = new int[5];

        public System.Windows.Forms.Timer uiTimer_rhythmic = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer uiTimer_random = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer uiTimer_aligned = new System.Windows.Forms.Timer();

        public DateTime[] system_push = new DateTime[50];
        public DateTime[] user_respond = new DateTime[50];
        public TimeSpan[] push_respond_interval = new TimeSpan[50];
        public int[] interval_ms = new int[50];
        public int t0 = 1000;


        public int count_rhythmic_push = 0;
        public int count_random_push = 0;
        public int count_aligned_push = 0;
        public int count_user_pull = 0;


        public int Rhythmic_timer_flag = 0;
        public int Random_timer_flag = 0;
        public int Aligned_timer_flag = 0;


        public int[,] aperiodic_interval = new int[31, 2]; // 30: number of intervals. 2: [*, 0] is interval length, [*, 1] is the number of accumulated messages

        //public string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
        public MySqlConnection myConn = new MySqlConnection("SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;"); 

        //  public System.Windows.Forms.Timer uiTimer_btn_blink = new System.Windows.Forms.Timer();



        public MainWindow()
        {
            InitializeComponent();

            //MySqlConnection myConn = new MySqlConnection(myConnection);

        }

        public void ReadAperiodicInterval(int[,] aperiodic_interval)
        {
            current_task_type = "Arrhythmic";

            var dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Environment.CurrentDirectory));

            string file = dir.Parent.FullName + @"\TXT\intervals.txt";

            StreamReader sr = new StreamReader(file);

            string thisLine = sr.ReadLine();
            string[] thisLineData = thisLine.Split(new char[0]);
            // var lines = File.ReadAllLines(file).ToList();
            for (int i = 0; i < 30; i++)
            {        
                  aperiodic_interval[i, 0] = Int32.Parse(thisLineData[i]);                
            }
        }

        public string generate_selection_range()
         {
            string selection_range;        

            //Random rnd = new Random();
            // range_num[count_trigger, 1] = rnd.Next(5, 11);//generates a number between [5, 10] for this 'sub task' (number count_trigger th)
            //range_num[count_trigger, 1] = rnd.Next(1, 3);// first, try to generate a number between [1, 2] for this 'sub task' (number count_trigger th)

           // range_num[count_trigger, 1] = 1; //One message at a time

            range_low = range_high = count_messages + 1;

            /*
            range_low = count_messages + 1;

            if (count_messages + range_num[count_trigger, 1] > 20)  //If there are 20 new messages in total
            {
                range_high = 20;
            }
            else {

                range_high = count_messages + range_num[count_trigger, 1];
            }
            
            selection_range = "id_all >= " + range_low.ToString()  +" and id_all <= " + range_high.ToString(); */
            selection_range = "id_all = " + range_low.ToString();
            return selection_range;
        }

        public void generate_from_table_range()
        {
            //from_table_range_string = "from_table_id >= " + range_low.ToString() + " and from_table_id <= " + range_high.ToString() ;
            from_table_range_string = "from_table_id = " + range_low.ToString();

            return;

        }

        public void update_count_trigger_and_messages()
        {

            //count_messages = count_messages + range_num[count_trigger, 1];
            count_messages = count_messages + 1;
            count_trigger++;

        }

        /*
        private void notification_Click(object sender, RoutedEventArgs e)
        {
            Status_Bar.NoticeNew.Visibility = System.Windows.Visibility.Visible;
            Status_Bar.NoticeNew_Label.Visibility = System.Windows.Visibility.Visible;

            Status_Bar.NoticeArchived_yes.Visibility = System.Windows.Visibility.Visible;
            Status_Bar.NoticeArchived_yes_Label.Visibility = System.Windows.Visibility.Visible;

            Status_Bar.NoticeArchived_no.Visibility = System.Windows.Visibility.Visible;
            Status_Bar.NoticeArchived_no_Label.Visibility = System.Windows.Visibility.Visible;

            Status_Bar.NoticeUnsure.Visibility = System.Windows.Visibility.Visible;
            Status_Bar.NoticeUnsure_Label.Visibility = System.Windows.Visibility.Visible;

            Status_Bar.NoticeManual.Visibility = System.Windows.Visibility.Visible;
            Status_Bar.NoticeManual_Label.Visibility = System.Windows.Visibility.Visible;


        }

            */

        /*
       
        private void mySQL_cmd1_Click(object sender, RoutedEventArgs e)
        {
            //refresh the DataGrid of NewMessages

            string myConnection = "SERVER=localhost;DATABASE=test_database;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            //  MessageBox.Show("Connected");
            current_selection_range = generate_selection_range();
            generate_from_table_range();

            string cmd_string = "SELECT id_all, time, content, tag, note, is_about_delivery_manually_set FROM all_messages WHERE is_processed =0 and " + current_selection_range;
            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            New_Messages.DG1.Items.Refresh();
            New_Messages.DG1.DataContext = ds;

            update_count_trigger_and_messages();

            myConn.Close();
        }
        

        private void mySQL_cmd2_Click(object sender, RoutedEventArgs e)
        {
            //selected record with confidence (40, 90], show as UnsureMessages

            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();

            current_selection_range = generate_selection_range();
            generate_from_table_range();


            string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

          //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
          //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
           // cmd1.ExecuteNonQuery();


            string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
            MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
            cmd2.ExecuteNonQuery();


            MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, confidence, is_about_delivery FROM unsure_messages", myConn);
            MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

            DataSet ds = new DataSet();

            adp3.Fill(ds, "LoadDataBinding_Unsure");
            Unsure_Messages.DG1.Items.Refresh();
            Unsure_Messages.DG1.DataContext = ds;

            myConn.Close();

            update_count_trigger_and_messages();


        }

       
        /*

        private void mySQL_cmd3_Click(object sender, RoutedEventArgs e)
        {
            //selected record with confidence (0, 40], show as Manual Messages

            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();

            string cmd_string = "INSERT INTO manual_messages (from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 0 and confidence <= 40 and is_processed=0 and is_manually_tagged = 0 and " + current_selection_range;

            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

            // MySqlCommand cmd1 = new MySqlCommand("UPDATE manual_messages SET from_table='all_messages' WHERE from_table_id>=1", myConn);
            // cmd1.ExecuteNonQuery();

            string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 0 and confidence <= 40 and is_manually_tagged=0 and " + current_selection_range;
            MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
            cmd2.ExecuteNonQuery();


            MySqlCommand cmd3 = new MySqlCommand("SELECT id_manual_messages, time, content, confidence, is_about_delivery FROM manual_messages", myConn);
            MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

            DataSet ds = new DataSet();

            adp3.Fill(ds, "LoadDataBinding_Manual");
            Manual_Messages.DG1.Items.Refresh();
            Manual_Messages.DG1.DataContext = ds;

            myConn.Close();
        }

   
        private void mySQL_cmd4_Click(object sender, RoutedEventArgs e)
        {
       
            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            /////////////////////////////
            //automatically add yes messages in all_messages table with confidence [91,100) and is_manually_tagged = 0 to archived_yes_messages table

//            string cmd_yes_string = "INSERT INTO archived_yes_messages (from_table_id,  from_table, time, content, old_confidence, is_about_delivery, is_manually_tagged) SELECT id_all, table_name, time, content, confidence, is_about_delivery, is_manually_tagged FROM all_messages WHERE is_about_delivery = 1 and is_manually_tagged=0 and confidence >= 91 and is_processed=0 and " + current_selection_range;

 //           MySqlCommand cmd_yes = new MySqlCommand(cmd_yes_string, myConn);
 //           cmd_yes.ExecuteNonQuery();

            //add yes messages in all_messages table with any confidence and is_manually_tagged =1 to archived_yes_messages table
 //           string cmd1_yes_string = "INSERT INTO archived_yes_messages (from_table_id, time, content, old_confidence, is_about_delivery, is_manually_tagged, from_table) SELECT id_all, time, content, confidence, is_about_delivery_manually_set, is_manually_tagged, table_name FROM all_messages WHERE is_about_delivery_manually_set = 1 and is_manually_tagged=1 and is_processed=0 and " + current_selection_range;

//            MySqlCommand cmd1_yes = new MySqlCommand(cmd1_yes_string, myConn);
 //           cmd1_yes.ExecuteNonQuery();


            //set values (from_table='all_messages') for the messages that were from all_messages table in archived_yes_messages table 
            // MySqlCommand cmd1_yes2 = new MySqlCommand("UPDATE archived_yes_messages SET from_table='all_messages' WHERE from_table_id>=1 and old_confidence<=99 and old_confidence>90", myConn);
            //cmd1_yes2.ExecuteNonQuery();
            // MySqlCommand cmd1_yes3 = new MySqlCommand("UPDATE archived_yes_messages SET from_table='all_messages' WHERE from_table_id>=1 and old_confidence<=90 and is_manually_tagged=1", myConn);
            // cmd1_yes3.ExecuteNonQuery();

            //update the processed messages (is_processed='1') with confidence [91,100) in all_messages table
            string cmd2_yes_string = "UPDATE all_messages SET is_processed='1' WHERE confidence>90 and is_about_delivery = 1 and " + current_selection_range;
            MySqlCommand cmd2_yes = new MySqlCommand(cmd2_yes_string, myConn);
            cmd2_yes.ExecuteNonQuery();

            //update the processed messages (is_processed='1') with any confidence and is_manually_tagged = 1 in all_messages table
            //            string cmd2_yes2_string = "UPDATE all_messages SET is_processed='1' WHERE is_manually_tagged=1 and is_about_delivery_manually_set = 1 and " + current_selection_range;
            //            MySqlCommand cmd2_yes2 = new MySqlCommand(cmd2_yes2_string, myConn);
            //            cmd2_yes2.ExecuteNonQuery();


            //update the messages that were from the new_messages table and is_manually_tagged = 1 in the archived_yes_messages with new_confidence = 100
            //           MySqlCommand cmd2_yes3 = new MySqlCommand("UPDATE archived_yes_messages SET new_confidence='100' WHERE from_table = 'new_messages' and is_manually_tagged=1 ", myConn);
            //          cmd2_yes3.ExecuteNonQuery();



            //automatically add NO messages in all_messages table with confidence [91,100) and is_manually_tagged = 0 to archived_no_messages table
//            string cmd_no_string = "INSERT INTO archived_no_messages (from_table_id, time, content, old_confidence, is_about_delivery, from_table) SELECT id_all, time, content, confidence, is_about_delivery, table_name  FROM all_messages WHERE is_about_delivery = 0 and confidence >= 91 and is_manually_tagged=0 and is_processed=0 and " + current_selection_range;

//            MySqlCommand cmd_no = new MySqlCommand(cmd_no_string, myConn);
 //           MySqlDataAdapter adp_no = new MySqlDataAdapter(cmd_no);
//            cmd_no.ExecuteNonQuery();

            //add yes messages in all_messages table with any confidence and is_manually_tagged =1 to archived_no_messages table
 //           string cmd1_no_string = "INSERT INTO archived_no_messages (from_table_id, time, content, old_confidence, is_about_delivery, is_manually_tagged, from_table) SELECT id_all, time, content, confidence, is_about_delivery_manually_set, is_manually_tagged, table_name FROM all_messages WHERE is_about_delivery_manually_set = 0 and is_manually_tagged=1  and is_processed=0 and " + current_selection_range;

   //         MySqlCommand cmd1_no = new MySqlCommand(cmd1_no_string, myConn);
   //         cmd1_no.ExecuteNonQuery();


            //update the processed messages (is_processed='1') with confidence [91,100) in all_messages table
            string cmd2_no_string = "UPDATE all_messages SET is_processed='1' WHERE confidence>90 and is_about_delivery = 0 and " + current_selection_range;
            MySqlCommand cmd2_no = new MySqlCommand(cmd2_no_string, myConn);
            cmd2_no.ExecuteNonQuery();

            //update the processed messages (is_processed='1') with any confidence and is_manually_tagged = 1 in all_messages table
 //           string cmd2_no2_string = "UPDATE all_messages SET is_processed='1' WHERE is_manually_tagged=1 and is_about_delivery_manually_set = 0 and " + current_selection_range;
  //          MySqlCommand cmd2_no2 = new MySqlCommand(cmd2_no2_string, myConn);
  //          cmd2_no2.ExecuteNonQuery();


            //update the messages that were from the new_messages table and is_manually_tagged = 1 in the archived_no_messages with new_confidence = 100
 //           MySqlCommand cmd2_no3 = new MySqlCommand("UPDATE archived_no_messages SET new_confidence='100' WHERE from_table = 'new_messages' and is_manually_tagged=1 ", myConn);
  //          cmd2_no3.ExecuteNonQuery();






            /////////////////////////////
            //add yes messages in unsure_messages table to archived_yes_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100
            MySqlCommand cmd3_yes = new MySqlCommand("INSERT INTO archived_yes_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 1", myConn);
            cmd3_yes.ExecuteNonQuery();

            //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_yes_messages table 
            MySqlCommand cmd4_yes = new MySqlCommand("UPDATE archived_yes_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", myConn);
            cmd4_yes.ExecuteNonQuery();


            //add NO messages in unsure_messages table to archived_no_messages table, is_manually_tagged = 1, from_table = unsure_messages, update new_confidence = 100
            MySqlCommand cmd3_no = new MySqlCommand("INSERT INTO archived_no_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_unsure_messages, table_name, time, content, confidence, is_about_delivery FROM unsure_messages WHERE is_about_delivery = 0", myConn);
            cmd3_no.ExecuteNonQuery();

            //set values (from_table='unsure_messages', is_manually_tagged='1') for the messages that were from unsure_messages table in archived_no_messages table 
            MySqlCommand cmd4_no = new MySqlCommand("UPDATE archived_no_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=90 and old_confidence>40 and from_table='unsure_messages'", myConn);
            cmd4_no.ExecuteNonQuery();


            //clear unsure_messages table
            //            MySqlCommand cmd5_both = new MySqlCommand("TRUNCATE TABLE unsure_messages", myConn);
            //            cmd5_both.ExecuteNonQuery();


            //refresh unsure_messages table (delete those manually tagged ones, only display those is_manually_tagged=0 and is_about_delivery='NULL' messages)
            MySqlCommand cmd5 = new MySqlCommand("TRUNCATE TABLE unsure_messages ", myConn);
            cmd5.ExecuteNonQuery();




            /////////////////////////////
            //add yes messages in manual_messages table to archived_yes_messages table, is_manually_tagged = 1, from_table = manual_messages, update new_confidence = 100
            //            MySqlCommand cmd6_yes = new MySqlCommand("INSERT INTO archived_yes_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_manual_messages, table_name, time, content, confidence, is_about_delivery FROM manual_messages WHERE is_about_delivery = 1", myConn);
            //            cmd6_yes.ExecuteNonQuery();

            //set values (from_table='manual_messages', is_manually_tagged='1') for the messages that were from manual_messages table in archived_yes_messages table 
            //           MySqlCommand cmd7_yes = new MySqlCommand("UPDATE archived_yes_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=40 and from_table='manual_messages'", myConn);
            //           cmd7_yes.ExecuteNonQuery();


            //add NO messages in manual_messages table to archived_no_messages table, is_manually_tagged = 1, from_table = manual_messages, update new_confidence = 100
            //            MySqlCommand cmd6_no = new MySqlCommand("INSERT INTO archived_no_messages (from_table_id, from_table, time, content, old_confidence, is_about_delivery) SELECT id_manual_messages, table_name, time, content, confidence, is_about_delivery FROM manual_messages WHERE is_about_delivery = 0", myConn);
            //            cmd6_no.ExecuteNonQuery();

            //set values (from_table='manual_messages', is_manually_tagged='1') for the messages that were from manual_messages table in archived_no_messages table 
            //            MySqlCommand cmd7_no = new MySqlCommand("UPDATE archived_no_messages SET is_manually_tagged='1', new_confidence='100'  WHERE old_confidence<=40 and from_table='manual_messages'", myConn);
            //            cmd7_no.ExecuteNonQuery();

            //refresh manual_messages table (delete those manually tagged ones, only display those is_manually_tagged=0 and is_about_delivery='NULL' messages)
            //            MySqlCommand cmd8_both = new MySqlCommand("DELETE FROM manual_messages WHERE is_manually_tagged='1' ", myConn);
            //            cmd8_both.ExecuteNonQuery();


            //clear manual_messages table
            // MySqlCommand cmd8_both = new MySqlCommand("TRUNCATE TABLE manual_messages", myConn);
            //cmd8_both.ExecuteNonQuery();

            /////////////////////////////
            //display archived_yes_messages table data
            MySqlCommand cmd9_yes = new MySqlCommand("SELECT id_archived_yes, time, content, old_confidence, is_about_delivery FROM archived_yes_messages", myConn);
            MySqlDataAdapter adp9_yes = new MySqlDataAdapter(cmd9_yes);
            DataSet ds_yes = new DataSet();
            adp9_yes.Fill(ds_yes, "LoadDataBinding_Archived_yes");
            Archived_yes_Messages.DG1.Items.Refresh();
            Archived_yes_Messages.DG1.DataContext = ds_yes;

            //display archived_no_messages table data
            MySqlCommand cmd9_no = new MySqlCommand("SELECT id_archived_no, time, content, old_confidence, is_about_delivery FROM archived_no_messages", myConn);
            MySqlDataAdapter adp9_no = new MySqlDataAdapter(cmd9_no);
            DataSet ds_no = new DataSet();
            adp9_no.Fill(ds_no, "LoadDataBinding_Archived_no");
            Archived_no_Messages.DG1.Items.Refresh();
            Archived_no_Messages.DG1.DataContext = ds_no;


            myConn.Close();

        }

             private void mySQL_cmd5_Click(object sender, RoutedEventArgs e)
        {
           

        }
        */

        private void Start_Rhythmic_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (Rhythmic_timer_flag == 0)
            {
                Start_Rhythmic_Timer.IsEnabled = false;
                uiTimer_rhythmic.Interval = 4400;
                uiTimer_rhythmic.Tick += Rhythmic_Timer_PushMessages;
                uiTimer_rhythmic.Start();
                Rhythmic_timer_flag = 1;
                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Start_rhythmic_timer", Unsure_Messages.DG1.Items.Count);

            }
        }


        public void Rhythmic_Timer_PushMessages(object sender, EventArgs e)
        {
            // if (uiTimer_rhythmic.Enabled && count_trigger < 30)
            if (uiTimer_rhythmic.Enabled && count_trigger < 10)
            {  //same as mysql_cmd2_btn click event
               //selected record with confidence (40, 90], show as UnsureMessages

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                current_selection_range = generate_selection_range();
                generate_from_table_range();


                string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
                MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
                //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
                // cmd1.ExecuteNonQuery();


                string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
                MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
                cmd2.ExecuteNonQuery();


                MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

                DataSet ds = new DataSet();

                adp3.Fill(ds, "LoadDataBinding_Unsure");
                Unsure_Messages.DG1.Items.Refresh();
                Unsure_Messages.DG1.DataContext = ds;

                //myConn.Close();
                update_count_trigger_and_messages();
   
                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "System", "Push_messages", Unsure_Messages.DG1.Items.Count);
                  
                //Console.Beep(4200, 50);
                // BackgroundBeep.Beep();
            }

            //else if (uiTimer_rhythmic.Enabled && count_trigger == 30)
            else if (uiTimer_rhythmic.Enabled && count_trigger == 10)
            {
                uiTimer_rhythmic.Tick -= Rhythmic_Timer_PushMessages;
                uiTimer_rhythmic.Stop();
                Rhythmic_timer_flag = 0;
                //count_trigger = 0; count_user_click = 0;
            }
        }

        private void Stop_Rhythmic_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (Rhythmic_timer_flag == 1)
            {
                uiTimer_rhythmic.Tick -= Rhythmic_Timer_PushMessages;
                uiTimer_rhythmic.Stop();
                Rhythmic_timer_flag = 0;
                count_trigger = 0; count_user_click = 0;
            }
        }

        private void write_subtasktime(DateTime dt, string taskType, int taskNum, string initiator, string behaviour, int accumulated_messages)
        {
            var main = App.Current.MainWindow as MainWindow;

            var dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Environment.CurrentDirectory));

            string file = dir.Parent.FullName + @"\TXT\output.txt";

            StreamWriter sr;


            sr = File.AppendText(file);

            sr.WriteLine(dt.ToString("hh:mm:ss.fff") + "\t" + taskType + "\t" + taskNum + "\t" + initiator + "\t" + behaviour + "\t" + accumulated_messages.ToString());//将传入的字符串加上时间写入文本文件一行

            sr.Close();
        }

        private void Start_Random_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (Random_timer_flag == 0)
            {
                Start_Random_Timer.IsEnabled = false;
                uiTimer_random.Interval = aperiodic_interval[count_trigger , 0];
                uiTimer_random.Tick += Random_Timer_PushMessages;
                uiTimer_random.Start();
                Random_timer_flag = 1;
                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Start_random_timer", Unsure_Messages.DG1.Items.Count);
            }

        }

        public void Random_Timer_PushMessages(object sender, EventArgs e)
        {
            //if (uiTimer_random.Enabled && count_trigger < 30)
            if (uiTimer_random.Enabled && count_trigger < 10)
            {  //same as mysql_cmd2_btn click event
               //selected record with confidence (40, 90], show as UnsureMessages

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                current_selection_range = generate_selection_range();
                generate_from_table_range();


                string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
                MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
                //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
                // cmd1.ExecuteNonQuery();


                string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
                MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
                cmd2.ExecuteNonQuery();


                MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

                DataSet ds = new DataSet();

                adp3.Fill(ds, "LoadDataBinding_Unsure");
                Unsure_Messages.DG1.Items.Refresh();
                Unsure_Messages.DG1.DataContext = ds;

                //myConn.Close();
                update_count_trigger_and_messages();

                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "System", "Push_messages", Unsure_Messages.DG1.Items.Count);

                if (count_trigger <= 9)
                {
                    uiTimer_random.Interval = aperiodic_interval[count_trigger, 0];
                }
                else if (count_trigger == 10)
                {
                    uiTimer_random.Tick -= Random_Timer_PushMessages;
                    uiTimer_random.Stop();
                    Random_timer_flag = 0;
                    //count_trigger = 0; count_user_click = 0;
                }

            }         

        }


        private void Stop_Random_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (Random_timer_flag == 1)
            {
                uiTimer_random.Tick -= Random_Timer_PushMessages;
                uiTimer_random.Stop();
                Random_timer_flag = 0;
                count_trigger = 0; count_user_click = 0;

            }
        }


        private void Start_Manual_Click(object sender, RoutedEventArgs e)
        {
            Start_Manual.IsEnabled = false;
            Next_btn.Visibility = System.Windows.Visibility.Visible;
            //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            //MySqlConnection myConn = new MySqlConnection(myConnection);

            //myConn.Open();

            current_selection_range = generate_selection_range();
            generate_from_table_range();

            string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();

            //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
            //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
            // cmd1.ExecuteNonQuery();

            string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
            MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
            cmd2.ExecuteNonQuery();


            MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
            MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

            DataSet ds = new DataSet();

            adp3.Fill(ds, "LoadDataBinding_Unsure");
            Unsure_Messages.DG1.Items.Refresh();
            Unsure_Messages.DG1.DataContext = ds;

            //myConn.Close();

            write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Pull_messages", Unsure_Messages.DG1.Items.Count);

            update_count_trigger_and_messages();
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            // if (count_trigger < 30)
            if (count_trigger < 10)
            {
                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                current_selection_range = generate_selection_range();
                generate_from_table_range();

                string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
                MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
                //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
                // cmd1.ExecuteNonQuery();

                string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
                MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
                cmd2.ExecuteNonQuery();


                MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

                DataSet ds = new DataSet();

                adp3.Fill(ds, "LoadDataBinding_Unsure");
                Unsure_Messages.DG1.Items.Refresh();
                Unsure_Messages.DG1.DataContext = ds;
                //myConn.Close();
                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Pull_messages", Unsure_Messages.DG1.Items.Count);

                update_count_trigger_and_messages();
            }
            //if (count_trigger == 30)
            if (count_trigger == 10)
            {
                Next_btn.IsEnabled = false;
            }
        }

        private void Start_Aligned_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (Aligned_timer_flag == 0)
            {
                Start_Aligned_Timer.IsEnabled = false;
                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                current_selection_range = generate_selection_range();
                generate_from_table_range();

                string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
                MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
                //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
                // cmd1.ExecuteNonQuery();

                string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
                MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
                cmd2.ExecuteNonQuery();


                MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

                DataSet ds = new DataSet();

                adp3.Fill(ds, "LoadDataBinding_Unsure");
                Unsure_Messages.DG1.Items.Refresh();
                Unsure_Messages.DG1.DataContext = ds;


                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Start_aligned_mode", Unsure_Messages.DG1.Items.Count);
                update_count_trigger_and_messages();
                //myConn.Close();
                system_push[count_trigger] = System.DateTime.Now;

                //update_count_trigger_and_messages();
            }


        }

        public void Aligned_Timer_PushMessages(object sender, EventArgs e)
        {
            //if (uiTimer_aligned.Enabled && count_trigger < 30)
            if (uiTimer_aligned.Enabled && count_trigger < 10)
            {  //same as mysql_cmd2_btn click event
               //selected record with confidence (40, 90], show as UnsureMessages

                //string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
                //MySqlConnection myConn = new MySqlConnection(myConnection);

                //myConn.Open();

                current_selection_range = generate_selection_range();
                generate_from_table_range();


                string cmd_string = "INSERT INTO unsure_messages(from_table_id, from_table, time, content, confidence, is_about_delivery) SELECT id_all, table_name, time, content, confidence, is_about_delivery FROM all_messages WHERE confidence > 40 and confidence <= 90 and is_processed = 0 and " + current_selection_range;
                MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                //  string cmd1_string = "UPDATE unsure_messages SET from_table='all_messages' WHERE " + from_table_range_string;
                //  MySqlCommand cmd1 = new MySqlCommand(cmd1_string, myConn);
                // cmd1.ExecuteNonQuery();

                string cmd2_string = "UPDATE all_messages SET is_processed='1' WHERE confidence > 40 and confidence <= 90 and " + current_selection_range;
                MySqlCommand cmd2 = new MySqlCommand(cmd2_string, myConn);
                cmd2.ExecuteNonQuery();


                MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
                MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

                DataSet ds = new DataSet();

                adp3.Fill(ds, "LoadDataBinding_Unsure");
                Unsure_Messages.DG1.Items.Refresh();
                Unsure_Messages.DG1.DataContext = ds;

                //myConn.Close();

                write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "System", "Push_messages", Unsure_Messages.DG1.Items.Count);

                uiTimer_aligned.Interval = interval_ms[count_user_click];
                update_count_trigger_and_messages();
                system_push[count_trigger] = System.DateTime.Now;


                //Console.Beep(4200, 50);
                // BackgroundBeep.Beep();
            }

            //else if (count_trigger == 30)
            else if (count_trigger == 10)
            {
                uiTimer_aligned.Tick -= Aligned_Timer_PushMessages;
                uiTimer_aligned.Stop();
                Aligned_timer_flag = 0;
                //count_trigger = 0; count_user_click = 0;
            }
        }


        private void Stop_Aligned_Timer_Click(object sender, RoutedEventArgs e)
        {
            uiTimer_aligned.Tick -= Aligned_Timer_PushMessages;
            uiTimer_aligned.Stop();
            Aligned_timer_flag = 0;
            count_trigger = 0; count_user_click = 0;
        }

        private void Task1Btn_Click(object sender, RoutedEventArgs e)
        {
            Task1Btn.Visibility = Task2Btn.Visibility = Task3Btn.Visibility = Task4Btn.Visibility = Status_Bar.Visibility = System.Windows.Visibility.Hidden;
            Start_Rhythmic_Timer.Visibility = Stop_Rhythmic_Timer.Visibility =  Unsure_Messages.Visibility = System.Windows.Visibility.Visible;
            taskFlag[1] = 1;
            current_task_type = "Rhythmic";

            myConn.Open();

            count_trigger = 0;
            count_user_click = 0;

            write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Enter_rhythmic_mode", Unsure_Messages.DG1.Items.Count);


        }

        private void Task2Btn_Click(object sender, RoutedEventArgs e)
        {
            Task1Btn.Visibility = Task2Btn.Visibility = Task3Btn.Visibility = Task4Btn.Visibility = Status_Bar.Visibility = System.Windows.Visibility.Hidden;
            Start_Random_Timer.Visibility = Stop_Random_Timer.Visibility = Unsure_Messages.Visibility = System.Windows.Visibility.Visible;
            taskFlag[2] = 1;
            current_task_type = "Random";
            ReadAperiodicInterval(aperiodic_interval);

            myConn.Open();

            count_trigger = 0;
            count_user_click = 0;
            write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Enter_random_mode", Unsure_Messages.DG1.Items.Count);


        }

        private void Task3Btn_Click(object sender, RoutedEventArgs e)
        {
            Task1Btn.Visibility = Task2Btn.Visibility = Task3Btn.Visibility = Task4Btn.Visibility = Status_Bar.Visibility = System.Windows.Visibility.Hidden;
            Start_Manual.Visibility = System.Windows.Visibility.Visible;
            Unsure_Messages.Visibility = System.Windows.Visibility.Visible;
            taskFlag[3] = 1;
            current_task_type = "Manual";

            myConn.Open();

            count_trigger = 0;
            count_user_click = 0;
            write_subtasktime(System.DateTime.Now, current_task_type, count_trigger, "User", "Enter_manual_mode", Unsure_Messages.DG1.Items.Count);

        }

        private void Task4Btn_Click(object sender, RoutedEventArgs e)
        {
            Task1Btn.Visibility = Task2Btn.Visibility = Task3Btn.Visibility = Task4Btn.Visibility = Status_Bar.Visibility = System.Windows.Visibility.Hidden;
            Start_Aligned_Timer.Visibility = Stop_Aligned_Timer.Visibility = Unsure_Messages.Visibility = System.Windows.Visibility.Visible;
            taskFlag[4] = 1;
            current_task_type = "Aligned";

            myConn.Open();

            count_trigger = 0;
            count_user_click = 0;

            system_push[count_trigger] = System.DateTime.Now;
            write_subtasktime(system_push[count_trigger], current_task_type, count_trigger, "User", "Enter_aligned_mode", Unsure_Messages.DG1.Items.Count);

        }

        /*
        private void Correct_btn_Click(object sender, RoutedEventArgs e)
        {}

        private void Wrong_btn_Click(object sender, RoutedEventArgs e)
        {}
        */
    }
}
