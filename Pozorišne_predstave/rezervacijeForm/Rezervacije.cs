using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;         //It is used for debuging and output is displayed on the terminal

namespace Pozorišne_predstave
{
    public partial class RezervacijeForm : Form
    {
        //These are the default params of the form:
        private static int RBR = 1;     //RedniBrojRezervacije
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"..\..\..\Access\Pozorisne Predstave.accdb";
        AppMode Mode;               //Set to Upisi mode

        public RezervacijeForm()
        {
            InitializeComponent(); 
            UpisiRadio.Checked = true;
            Mode = 0;
            KomadiComboBox.Enabled = false;
            redniBrTextBox.Text = RBR.ToString();
            BrSedistaTextBox.Enabled = false;
            IzvrsiButton.Enabled = false;
        }
    }
}
