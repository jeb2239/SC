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

namespace MakerspaceApp
{
    /// <summary>
    /// Interaction logic for UserInventory.xaml
    /// </summary>
    public partial class UserInventory : Window
    {
        public UserInventory()
        {
            InitializeComponent();
            loadCatalogue(data);
        }

     
        private void submitClick(object sender, RoutedEventArgs e)
        {
           List<string> listOfItems = new List<string>();
           List<int> numbers = new List<int>();

           foreach (ComboBox box in mainPanel.Children.OfType<ComboBox>())
           {
             string nameSelected  =  box.SelectedItem.ToString();
               listOfItems.Add(nameSelected.Trim());
           }

           foreach (TextBox tb in mainPanel.Children.OfType<TextBox>())
           {
               string numba = tb.Text;
               numbers.Add(int.Parse(numba.Trim()));

           }

          for (int i = 0; i < listOfItems.ToArray().Length; i++)
          {
              App.subtractFromInventory(listOfItems[i], numbers[i]);
              App.recordTran(listOfItems[i], numbers[i]);
          }
           
        }

        private void addNewClick(object sender, RoutedEventArgs e)
        {
            addNewCBox();
        }

     public ComboBox loadCatalogue(ComboBox data)
        {
            MakerspaceDataDataContext db = new MakerspaceDataDataContext();
         
            var query = from e in db.Inventories
                    select e;
            var listItems=query.ToList();
            List<string> ItemIDs=new List<string>();
            foreach( var i in listItems){
              ItemIDs.Add(  i.ItemID);
            };
            ItemIDs.Sort();
            data.ItemsSource = ItemIDs;
            data.SelectedIndex = 0;
            return data;
            
            
        }

     public void addNewCBox()
     {
         ComboBox cBox = new ComboBox( );
         cBox.AllowDrop = true;
         cBox.Margin = new Thickness(0, 0, 488, 0);
         cBox.Height = 23;
         TextBox box = new TextBox();
         box.Margin = new Thickness(0, 0, 488, 0);
         box.Height = 23;
         cBox = loadCatalogue(cBox);
         mainPanel.Children.Add(cBox);
         mainPanel.Children.Add(box);
         mainPanel.UpdateLayout();

     }

    }
}
