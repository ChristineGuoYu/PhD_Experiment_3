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



namespace Mixed_Initiative_Exp1_v0._1
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        private void NewMessages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;

            //refresh the DataGrid of UnsureMessages

            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);


            myConn.Open();
            //  MessageBox.Show("Connected");

            MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, note, confidence, is_about_delivery FROM unsure_messages", myConn);
            MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

            DataSet ds = new DataSet();

            adp3.Fill(ds, "LoadDataBinding_Unsure");
            main.Unsure_Messages.DG1.Items.Refresh();
            main.Unsure_Messages.DG1.DataContext = ds;


            myConn.Close();


          //  main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Visible;
          //  main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;


        }

        /*
        private void NewMessages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;
            //refresh the DataGrid of NewMessages

            
            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            //  MessageBox.Show("Connected");
            string cmd_string = "SELECT id_all, time, content, tag, note, is_about_delivery_manually_set FROM all_messages WHERE is_processed=0 and " + main.current_selection_range; 
            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
           
            main.New_Messages.DG1.DataContext = ds;

            myConn.Close();
            
            

            main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Visible;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;

            
        }

            */

        /*

        private void UnsureMessages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;

            //refresh the DataGrid of UnsureMessages

            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);


            myConn.Open();
            //  MessageBox.Show("Connected");

            MySqlCommand cmd3 = new MySqlCommand("SELECT id_unsure_messages, time, content, confidence, is_about_delivery FROM unsure_messages", myConn);
            MySqlDataAdapter adp3 = new MySqlDataAdapter(cmd3);

            DataSet ds = new DataSet();

            adp3.Fill(ds, "LoadDataBinding_Unsure");
            main.Unsure_Messages.DG1.Items.Refresh();
            main.Unsure_Messages.DG1.DataContext = ds;


            myConn.Close();           


            main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Visible;
            main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;


        }

            */
        /*
        private void ManualMessages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;

            //refresh the DataGrid of ManualMessages

            string myConnection = "SERVER=localhost;DATABASE=test_database;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            //  MessageBox.Show("Connected");
            MySqlCommand cmd = new MySqlCommand("SELECT id_manual_messages, time, content, tag, note, is_about_delivery FROM manual_messages", myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding_Manual");
            main.Manual_Messages.DG1.Items.Refresh();
            main.Manual_Messages.DG1.DataContext = ds;

            myConn.Close();

            main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Visible;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;

        }
        */
        private void Archived_yes_Messages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;

            //refresh the DataGrid of ArchivedYesMessages
            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            //  MessageBox.Show("Connected");
            MySqlCommand cmd = new MySqlCommand("SELECT id_archived_yes, time, content, tag, note, is_about_delivery FROM archived_yes_messages", myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding_Archived_yes");
            
            main.Archived_yes_Messages.DG1.DataContext = ds;

            myConn.Close();
  
        //    main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
        //    main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Visible;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Archived_no_Messages_btn_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;

            //refresh the DataGrid of ArchivedNoMessages
            string myConnection = "SERVER=localhost;DATABASE=mixed_initiative_exp1_practice;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            //  MessageBox.Show("Connected");
            MySqlCommand cmd = new MySqlCommand("SELECT id_archived_no, time, content, tag, note, is_about_delivery FROM archived_no_messages", myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding_Archived_no");
            main.Archived_no_Messages.DG1.Items.Refresh();
            main.Archived_no_Messages.DG1.DataContext = ds;

            myConn.Close();
          
          //  main.New_Messages.Visibility = main.New_Messages_label.Visibility = main.New_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Unsure_Messages.Visibility = main.Unsure_Messages_label.Visibility = main.Unsure_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
         //   main.Manual_Messages.Visibility = main.Manual_Messages_label.Visibility = main.Manual_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_yes_Messages.Visibility = main.Archived_yes_Messages_label.Visibility = main.Archived_yes_Messages_num_label.Visibility = System.Windows.Visibility.Hidden;
            main.Archived_no_Messages.Visibility = main.Archived_no_Messages_label.Visibility = main.Archived_no_Messages_num_label.Visibility = System.Windows.Visibility.Visible;

        }
    }
}
