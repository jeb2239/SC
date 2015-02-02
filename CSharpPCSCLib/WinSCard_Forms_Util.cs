
using System.Windows.Forms;

namespace GS.SCard
{
    public partial class WinSCard
    {
        //generic form
        public void AddReaders(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Enabled = false;

            WinSCard pcscReader = new WinSCard();
            pcscReader.EstablishContext();
            pcscReader.ListReaders();
            
            if (pcscReader.ReaderNames != null)
            {
                comboBox.Items.AddRange( pcscReader.ReaderNames );
                comboBox.SelectedIndex = 0;

                if (pcscReader.ReaderNames.Length > 1)
                {
                    comboBox.Enabled = true;
                }
            }
            else
            {
                comboBox.Items.Add( "No PC/SC Reader found!" );
            }
            pcscReader.ReleaseContext();
        } 
    }
}
