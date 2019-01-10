using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Pozorišne_predstave
{
    public partial class PoTrupamaForm : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+@"..\..\..\Access\Pozorisne Predstave.accdb";

        public PoTrupamaForm()
        {
            InitializeComponent();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT NazivTrupe From Pozorisna_Trupa", conn);
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        TrupaCombo.Items.Add(reader.GetString(0));
                    }
                }

            }
        }

        private void IzadjiButton_Click(object sender, EventArgs e)
        {
            PozoristeForm f = new PozoristeForm();
            f.Show();
            this.Hide();
        }

        private void IzvrsiButton_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT GlumacID, Ime, Prezime " +
                    "FROM Glumac INNER JOIN Pozorisna_Trupa ON Pozorisna_Trupa.TrupaID = Glumac.TrupaID " +
                    $"WHERE Pozorisna_Trupa.NazivTrupe = '{TrupaCombo.SelectedItem.ToString()}' " +
                    "ORDER BY GlumacID;", conn);
                OleDbDataAdapter dataadapter = new OleDbDataAdapter(cmd.CommandText, connectionString);
                DataTable dt = new DataTable();
                dataadapter.Fill(dt);
                GlumacView.DataSource = dt;

                OleDbCommand cmd2 = new OleDbCommand("SELECT Pozorisni_Komad.Naziv, Pozorisni_Komad.[Trajanje/min], Pozorisni_Komad.BrojCinova AS Odigrano_puta " +
                    "FROM(Pozorisni_Komad INNER JOIN Predstava ON Predstava.KomadID = Pozorisni_Komad.KomadID) INNER JOIN Pozorisna_Trupa ON Pozorisna_Trupa.TrupaID = Predstava.TrupaID " +
                    $"WHERE Pozorisna_Trupa.NazivTrupe = '{TrupaCombo.SelectedItem.ToString()}' " +
                    "ORDER BY Pozorisni_Komad.Naziv;",conn);
                OleDbDataAdapter dataadapter2 = new OleDbDataAdapter(cmd2.CommandText, connectionString);
                DataTable dt2 = new DataTable();
                dataadapter2.Fill(dt2);
                KomadiView.DataSource = dt2;
            }
        }
    }
}
