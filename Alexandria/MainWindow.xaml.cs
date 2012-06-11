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
using System.Threading;

namespace Alexandria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        AlexandriaMain alexandria = new AlexandriaMain();

        public MainWindow()
        {
            InitializeComponent();

            alexandria.startUp();
        }

        private void startTransfer_Btn_Click(object sender, RoutedEventArgs e)
        {
            alexandria.client.command.fileGet("SS intro.avi", alexandria.client.networkStream);
        }

        private void debug_getListButton_Click(object sender, RoutedEventArgs e)
        {
            string fileList = alexandria.client.command.List(alexandria.client.networkStream);
            debug_FileListBox.Text = fileList;
        }
    }
}
