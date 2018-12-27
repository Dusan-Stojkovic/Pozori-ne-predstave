using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;         //It is used for debuging and output is displayed on the terminal

namespace Pozorišne_predstave
{
    public partial class RezervacijeForm : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"..\..\..\Access\Pozorisne Predstave.accdb";

        public RezervacijeForm()
        {
            InitializeComponent();
            UpisiRadio.Checked = true;
            KomadiComboBox.Enabled = false;
        }

        private void IzadjiButton_Click(object sender, EventArgs e)
        {
            PozoristeForm f = new PozoristeForm();
            f.Show();
            this.Close();
        }

        Func<string, string> Test_for_ID = x =>  //Int input test 
        {
            try
            {
                if (int.Parse(x) >= 1)      //We try to parse to test for null or int <= 1 and non input value 
                {
                    return x;
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            catch (OverflowException)
            {
                throw new OverflowException();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
        };

        private void sifraTextBox_TextChanged(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                try
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT RezervacijaID FROM Rezervacija " +
                        $"WHERE RezervacijaID = {Test_for_ID(sifraTextBox.Text)};", conn);      //Main reason for try catch    line 26
                    using (OleDbDataReader TestInput = cmd.ExecuteReader())
                    {
                        if (TestInput.Read())
                        {
                            OleDbCommand command = new OleDbCommand("SELECT Pretplatnik.Ime, Pretplatnik.Prezime, Rezervacija.DatumRezervisanja, Stavke_Rezervacije.RBR " +
                                "FROM (Pretplatnik INNER JOIN Rezervacija ON Pretplatnik.PretplatnikID = Rezervacija.PretplatnikID) " +
                                "INNER JOIN Stavke_Rezervacije ON Stavke_Rezervacije.RezervacijaID = Rezervacija.RezervacijaID " +
                                $"WHERE Rezervacija.RezervacijaID = {TestInput.GetInt32(0)};", conn);
                            using (OleDbDataReader InputReader = command.ExecuteReader())
                            {
                                if (InputReader.Read())
                                {
                                    DateLabel.Text = InputReader.GetDateTime(2).ToShortDateString();
                                    ImeLabel.Text = InputReader.GetString(0) + " " + InputReader.GetString(1);
                                    redniBrTextBox.Text = InputReader.GetInt32(3).ToString();
                                    komadiComboBoxFill(TestInput.GetInt32(0));
                                }
                                else
                                {
                                    MessageBox.Show("InputReader failed.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nepoznata šifra. Upisi novu rezervaciju: ");
                            UpisiRadio.Checked = true;
                            /*OleDbCommand commad1 = new OleDbCommand("SELECT COUNT(RBR) FROM Stavke_Rezervacije " +
                                $"WHERE KomadID = (SELECT KomadID FROM Stavke_Rezervacije WHERE RezervacijaID = {TestInput.GetInt32(0) + 1});", conn);*/

                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    Debug.Print($"Empty");
                }
                catch (FormatException)
                {
                    Debug.Print("ID is not Int32. ");
                    //Reset all values of input output
                    sifraTextBox.Text = "";
                    ImeLabel.Text = "";
                    SifraLabel.Text = "";
                    KomadiComboBox.Items.Clear();
                    BrSedistaLabel.Text = "";


                }
                catch (OverflowException)
                {
                    Debug.Print($"ID is greater than {int.MaxValue}");
                    //Reset all values of input output
                    sifraTextBox.Text = "";
                    ImeLabel.Text = "";
                    SifraLabel.Text = "";
                    KomadiComboBox.Items.Clear();
                    BrSedistaLabel.Text = "";
                }
            }
        }

        private void komadiComboBoxFill(int RezervacijaID = 0)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd;
                if (RezervacijaID != 0)
                {
                    cmd = new OleDbCommand("SELECT Pozorisni_Komad.KomadID, Naziv FROM Pozorisni_Komad " +
                        "INNER JOIN Stavke_Rezervacije ON Stavke_Rezervacije.KomadID = Pozorisni_Komad.KomadID " +
                        $"WHERE Stavke_Rezervacije.RezervacijaID = {RezervacijaID};", conn);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader.GetInt32(0).ToString() + " " + reader.GetString(1);
                            KomadiComboBox.Items.Add(temp);
                            KomadiComboBox.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    cmd = new OleDbCommand("SELECT Pozorisni_Komad.KomadID, Naziv FROM Pozorisni_Komad", conn);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            KomadiComboBox.Items.Add(reader.GetInt16(0).ToString() + reader.GetString(1));
                    }
                }
            }
        }

        private void UpisiRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (IzbrisiRadio.Checked == true && UpisiRadio.Checked == true) IzbrisiRadio.Checked = false;
        }

        private void IzbrisiRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (UpisiRadio.Checked == true && IzbrisiRadio.Checked == true) UpisiRadio.Checked = false;
        }

        private void IzvrsiButton_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {

            }
        }
    }
}
