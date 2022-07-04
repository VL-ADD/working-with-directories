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
//using System.Windows.Shapes;
using System.IO;

namespace lab5_sharp
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static string DirName ;
      //   DirectoryInfo di1 = new DirectoryInfo("C:\\");
        string DirName = "C:\\";
        List<DirectoryInfo> rr = new List<DirectoryInfo>();
        //DirectoryInfo[] rr;
        int index;
        string[] buttonlist;
        //MenuItem h = new MenuItem();
        //string[] files = Directory.GetFiles(DirName/*,  SearchOption.AllDirectories*/);
        private void ALL(object sender, EventArgs e)
        {
            spisok.Items.Clear();
            work();
        }

        private void FILTR(object sender,EventArgs e)
        {
            spisok.Items.Clear();
            sort();
        }
       
        private void qw(object sender,EventArgs e)
        {
            
        }
      
        private void work()////////////////////////////////////////////////////
        {
            try
            {
                string[] files = Directory.GetFiles(DirName);
                rr.Clear();
                int r = files.Length;
                DirectoryInfo di1 = new DirectoryInfo(DirName);
                di1.Create();
                buttonlist = files;
                //rr = di1.GetDirectories();

                foreach (var i in di1.GetDirectories())
                {
                    spisok.Items.Add(i);
                    rr.Add(i);
                }
                for(int i = 0;i<r;i++)
                {
                    spisok.Items.Add(files[i]);///////////////////////////////
                    //buttonlist[i] = files[i];
                }
               
            }
            catch (Exception)
            {
                
            }
        }
        private void sort()
        {
            try
            {
                rr.Clear();
                DirectoryInfo di1 = new DirectoryInfo(DirName);
                di1.Create();
               // rr = di1.GetDirectories();
                foreach (var i in di1.GetDirectories())
                {
                    if(i.CreationTime > date.DisplayDate)
                    {
                        spisok.Items.Add(i);
                    }
                    rr.Add(i);
                   
                }
                string[] files = Directory.GetFiles(DirName/*,  SearchOption.AllDirectories*/);
                int r = files.Length;
                buttonlist = files;
                for (int i = 0; i < r; i++)
                {
                    DateTime creation = File.GetCreationTime(files[i]);
                    if (creation > date.DisplayDate)
                    spisok.Items.Add(files[i]);
                    //buttonlist[i] = files[i];
                }
            }
            catch (Exception)
            {

            }
        }
        private void erer(object sender,EventArgs e)
        {
            MenuItem name = (MenuItem)sender;
            DirName = Convert.ToString( name.Header);
        }


        public MainWindow()
        {
            InitializeComponent();
           
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach( var ee in allDrives)
            {
                MenuItem h = new MenuItem();
                h.Header = Convert.ToString( ee);
                h.Click += new RoutedEventHandler(erer);
                discks.Items.Add(h);
            }
        }

        private void spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                index = spisok.SelectedIndex;
                if (index < rr.Count)
                {
                    textbox1.Text = rr[index].FullName;
                    string dat = rr[index].FullName;
                    textbox2.Text = Convert.ToString(rr[index].Attributes);
                   
                    textbox3.Text = Convert.ToString(rr[index].CreationTime);
                }
                else
                {
                    int newindex = index - rr.Count + 1;
                    textbox1.Text = buttonlist[newindex];
                    FileInfo fileInf = new FileInfo(buttonlist[newindex]);
                    textbox2.Text = fileInf.Length + "байт";
                    textbox3.Text =Convert.ToString( fileInf.CreationTime);
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";

            }
            catch (Exception)
            {
                spisok.Items.Clear();
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
            }

        }
    }
}
