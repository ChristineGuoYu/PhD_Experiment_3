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
    /// Interaction logic for Table_NewMessages.xaml
    /// </summary>
    /// 
    public class ComboBoxItemString_NewMessages
    {
        public string ValueString { get; set; }
    }
   


    public partial class Table_NewMessages : UserControl
    {

        public int flag_checked = 0;
        public int flag_unchecked = 0;

        public Table_NewMessages()
        {
            InitializeComponent();
            /*
            var main = App.Current.MainWindow as MainWindow;

            string myConnection = "SERVER=localhost;DATABASE=test_database;UID=Christine;PASSWORD=20150330;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            myConn.Open();
            // MessageBox.Show("Connected");
            string cmd_string = "SELECT id_all, time, content, tag, note, is_about_delivery_manually_set FROM all_messages WHERE " + main.current_selection_range;


            MySqlCommand cmd = new MySqlCommand(cmd_string, myConn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            DG1.DataContext = ds;

            myConn.Close();
            */
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
                var colValue = row["is_about_delivery_manually_set"];
                if (Convert.ToString(colValue) == "True")
                {
                    e.Row.Background = new SolidColorBrush(Colors.LightCyan);
                }
                else if (Convert.ToString(colValue) == "False")
                {
                    e.Row.Background = new SolidColorBrush(Colors.WhiteSmoke);
                }
                else
                {
                    e.Row.Background = new SolidColorBrush(Colors.White);
                }
            }
        }
        /*
        private void dg_RowPrePaint(object sender, DataGridRowStyleEventHandlerAgrs e)
        {
            if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Text) < Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Text))
            {
                DG1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Beige;
            }
        }
        */

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
                var id_all = row["id_all"]; int id_all_num = Convert.ToInt32(id_all);
                var is_about_delivery_manually_set = row["is_about_delivery_manually_set"]; bool bool_Value = Convert.ToBoolean(is_about_delivery_manually_set);

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


                string myConnection = "SERVER=localhost;DATABASE=test_database;UID=Christine;PASSWORD=20150330;";
                MySqlConnection myConn = new MySqlConnection(myConnection);

                myConn.Open();
                // MessageBox.Show("Connected");

                MySqlCommand cmd = new MySqlCommand("UPDATE all_messages SET is_about_delivery_manually_set='1', is_manually_tagged ='1', is_processed='0' WHERE id_all='" + id_all_num.ToString() + "'", myConn);
                cmd.ExecuteNonQuery();
                //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                myConn.Close();
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
                var id_all = row["id_all"]; int id_all_num = Convert.ToInt32(id_all);
                var is_about_delivery_manually_set = row["is_about_delivery_manually_set"]; bool bool_Value = Convert.ToBoolean(is_about_delivery_manually_set);

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

                string myConnection = "SERVER=localhost;DATABASE=test_database;UID=Christine;PASSWORD=20150330;";
                MySqlConnection myConn = new MySqlConnection(myConnection);

                myConn.Open();
                // MessageBox.Show("Connected");

                MySqlCommand cmd = new MySqlCommand("UPDATE all_messages SET is_about_delivery_manually_set='0', is_manually_tagged ='1', is_processed='0' WHERE id_all='" + id_all_num.ToString() + "'", myConn);
                cmd.ExecuteNonQuery();
                //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                myConn.Close();
            }
        }
    }
}
