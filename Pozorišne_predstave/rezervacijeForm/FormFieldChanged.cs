using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pozorišne_predstave
{
    partial class RezervacijeForm
    {
        private void UpisiRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (UpisiRadio.Checked == true)
            {
                IzbrisiRadio.Checked = false;
                Mode = (AppMode)0;
                TestUserAction(Mode);
            }
        }

        private void IzbrisiRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (IzbrisiRadio.Checked == true)
            {
                UpisiRadio.Checked = false;
                Mode = (AppMode)1;
                TestUserAction(Mode);
            }
        }

        private void sifraTextBox_TextChanged(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT RezervacijaID FROM Rezervacija " +
                        $"WHERE RezervacijaID = {Test_for_int(sifraTextBox.Text)};", conn);
                    using (OleDbDataReader TestInput = cmd.ExecuteReader())
                    {
                        if (TestInput.Read())       //We test input to know if we can make the reservation
                        {                           //If true just fill the form
                            OleDbCommand command = new OleDbCommand("SELECT KolikoSedista, KomadID " +
                                "FROM Stavke_Rezervacije " +
                                "INNER JOIN Rezervacija ON Rezervacija.RezervacijaID = Stavke_Rezervacije.RezervacijaID " +
                                $"WHERE Rezervacija.RezervacijaID = {TestInput.GetInt32(0)};", conn);
                            OleDbCommand command1 = new OleDbCommand("SELECT Pretplatnik.Ime, Pretplatnik.Prezime, Rezervacija.DatumRezervisanja " +
                                "FROM Pretplatnik INNER JOIN Rezervacija ON Pretplatnik.PretplatnikID = Rezervacija.PretplatnikID " +
                                $"WHERE(([Rezervacija].[RezervacijaID] = {TestInput.GetInt32(0)}));", conn);
                            using (OleDbDataReader PretplatnikReader = command1.ExecuteReader())
                            {
                                if (PretplatnikReader.Read())       //Must test for pretplatnik because new reservations can't specify one
                                {
                                    ImeLabel.Text = PretplatnikReader.GetString(0) + " " + PretplatnikReader.GetString(1);
                                    DateLabel.Text = PretplatnikReader.GetDateTime(2).ToShortDateString();
                                }
                                else
                                {
                                    ImeLabel.Text = "";
                                    DateLabel.Text = "";
                                }
                            }
                            using (OleDbDataReader InputReader = command.ExecuteReader())
                            {
                                if (InputReader.Read())
                                {
                                    UpisiRadio.Checked = false;
                                    IzbrisiRadio.Checked = true;
                                    BrSedistaTextBox.Text = InputReader.GetInt32(0).ToString();
                                    BrSedistaTextBox.Enabled = true;
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
                            //New input can't be deleted from database 
                            UpisiRadio.Checked = true;
                            IzbrisiRadio.Checked = false;
                            DateLabel.Text = "";
                            ImeLabel.Text = "";
                            BrSedistaTextBox.Text = "";
                            komadiComboBoxFill();
                            BrSedistaTextBox.Enabled = true;    //We can now edit reservations
                        }
                    }
                }
                catch (FormatException)
                {
                    Debug.Print("ID is not Int32. ");
                    //Reset all values of input output
                    sifraTextBox.Text = "";
                    ImeLabel.Text = "";
                    DateLabel.Text = "";
                    KomadiComboBox.Items.Clear();
                    KomadiComboBox.Text = "";
                    BrSedistaTextBox.Text = "";
                }
                catch (OverflowException)
                {
                    Debug.Print($"ID is greater than {int.MaxValue}");
                    //Reset all values of input output
                    sifraTextBox.Text = "";
                    ImeLabel.Text = "";
                    DateLabel.Text = "";
                    KomadiComboBox.Items.Clear();
                    KomadiComboBox.Text = "";
                    BrSedistaTextBox.Text = "";
                }
            }
        }

        private void BrSedistaTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BrSedistaTextBox.Text = Test_for_int(BrSedistaTextBox.Text);
                IzvrsiButton.Enabled = true;
            }
            catch (ArgumentNullException)
            {
                Debug.Print("Empty");
            }
            catch (FormatException)
            {
                Debug.Print("ID is not Int32. ");
                //Reset just the text box value
                BrSedistaTextBox.Text = "";
            }
            catch (OverflowException)
            {
                Debug.Print($"ID is greater than {int.MaxValue}");
                //Reset just the text box value
                BrSedistaTextBox.Text = "";
            }

        }
    }
}
