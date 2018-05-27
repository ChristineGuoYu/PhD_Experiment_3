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
using System.IO;

namespace Mixed_Initiative_Exp1_v0._1
{
    /// <summary>
    /// Interaction logic for TaskRatings.xaml
    /// </summary>
    public partial class TaskRatings : UserControl
    {
        public TaskRatings()
        {
            InitializeComponent();
            
        }

        public int s1 = 0;
        public int s2 = 0;
        public int s3 = 0;
        public int s4 = 0;
        public int s5 = 0;
        public int s6 = 0;
        public int s_tlx_1 = 0;
        public int s_tlx_2 = 0;
        public int s_tlx_3 = 0;
        public int s_tlx_4 = 0;
        public int s_tlx_5 = 0;
        public int s_tlx_6 = 0;
        public System.Windows.Forms.Timer uiTimer0 = new System.Windows.Forms.Timer();


        public int submit_flag = 0;


        private void sld1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s1 = 1;
            s1 = (int)(sld1.Value);

        }

        private void sld2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s2 = 1;
            s2 = (int)(sld2.Value);
        }

        private void sld3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s3 = 1;
            s3 = (int)(sld3.Value);
        }

        private void sld4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s4 = 1;
            s4 = (int)(sld4.Value);
        }

        private void sld5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s5 = 1;
            s5 = (int)(sld5.Value);
        }

        private void sld6_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s6 = 1;
            s6 = (int)(sld6.Value);
        }

        private void sld_tlx_1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_1 = 1;
            s_tlx_1 = (int)(sld_tlx_1.Value);

        }

        private void sld_tlx_2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_2 = 1;
            s_tlx_2 = (int)(sld_tlx_2.Value);
        }

        private void sld_tlx_3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_3 = 1;
            s_tlx_3 = (int)(sld_tlx_3.Value);
        }

        private void sld_tlx_4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_4 = 1;
            s_tlx_4 = (int)(sld_tlx_4.Value);
        }

        private void sld_tlx_5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_5 = 1;
            s_tlx_5 = (int)(sld_tlx_5.Value);
        }

        private void sld_tlx_6_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_tlx_6 = 1;
            s_tlx_6 = (int)(sld_tlx_6.Value);
        }



        public void EnableButton(object sender, EventArgs e)
        {
            if (uiTimer0.Enabled)
            {

                submit.IsEnabled = true;
                notice.Visibility = System.Windows.Visibility.Hidden;

            }
            uiTimer0.Tick -= EnableButton;
            uiTimer0.Stop();
        }


        private void submit_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;
            main.myConn.Close();
            if (submit_flag == 0)
            {
                // MessageBoxEx.Show("Check the ratings then submit again : )", "Just to make sure!");
                notice.Visibility = System.Windows.Visibility.Visible;
                submit.Content = "Submit";
                submit.IsEnabled = false;
                submit_flag = 1;
                uiTimer0.Interval = 1500;
                uiTimer0.Tick += EnableButton;
                uiTimer0.Start();
            }
            else if (submit_flag == 1)
            {
                notice.Visibility = this.Visibility = System.Windows.Visibility.Hidden;
                submit.Content = "Done";
                submit_flag = 0;

                write_txt(main.current_task_type, Convert.ToInt32(sld1.Value), "Rating-1", "Adaptation", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld2.Value), "Rating-2", "Control", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld3.Value), "Rating-3", "Intention", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld4.Value), "Rating-4", "Relaxation", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld5.Value), "Rating-5", "Smoothiness", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld6.Value), "Rating-6", "Confidence", System.DateTime.Now);

                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_1.Value), "Rating-1", "TLX-1", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_2.Value), "Rating-2", "TLX-2", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_3.Value), "Rating-3", "TLX-3", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_4.Value), "Rating-4", "TLX-4", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_5.Value), "Rating-5", "TLX-5", System.DateTime.Now);
                write_txt(main.current_task_type, Convert.ToInt32(sld_tlx_6.Value), "Rating-6", "TLX-5", System.DateTime.Now);


                sld1.Value = sld2.Value = sld3.Value = sld4.Value = sld5.Value = sld6.Value = 50;
                sld_tlx_1.Value = sld_tlx_2.Value = sld_tlx_3.Value = sld_tlx_4.Value = sld_tlx_5.Value = sld_tlx_6.Value = 11;


                main.count_trigger = 0; main.count_user_click = 0; //???


                if (main.taskFlag[1] == 1)
                {
                    main.Task1Btn.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    main.Task1Btn.Visibility = System.Windows.Visibility.Visible;
                }

                if (main.taskFlag[2] == 1)
                {
                    main.Task2Btn.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    main.Task2Btn.Visibility = System.Windows.Visibility.Visible;
                }
                if (main.taskFlag[3] == 1)
                {
                    main.Task3Btn.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    main.Task3Btn.Visibility = System.Windows.Visibility.Visible;
                }
                if (main.taskFlag[4] == 1)
                {
                    main.Task4Btn.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    main.Task4Btn.Visibility = System.Windows.Visibility.Visible;
                }





            }


        }

        private void write_txt(string taskType, int taskNum, string obj, string act, DateTime dt)
        {
            System.DateTime currentTime = System.DateTime.Now;

            var dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Environment.CurrentDirectory));

            string file = dir.Parent.FullName + @"\TXT\ratings.txt";

            StreamWriter sr;

            sr = File.AppendText(file);
            //DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.fff")
            sr.WriteLine(dt.ToString("hh:mm:ss.fff") + "\t" + taskType + "\t" + taskNum + "\t" + obj + "\t" + act);//将传入的字符串加上时间写入文本文件一行
            sr.Close();
        }


    }
}
