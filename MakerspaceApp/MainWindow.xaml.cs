using GS.SCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MakerspaceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected static Boolean result;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, EventArgs e)
        {

            App.cardProtocol(); //if everything is good
            result = App.isValid();
            if (result)
            {




                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {

                    UserInventory ui = new UserInventory();
                    ui.ShowDialog();
                }
                else
                {

                    App.recordTran();

                }


            }







        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
