using System;
using System.Data.OleDb;
using System.Diagnostics;

namespace Pozorišne_predstave
{
    partial class RezervacijeForm
    {
        private enum AppMode
        {
            Upisi = 0,
            Izbrisi = 1
        }

        public Func<string, string> Test_for_int = x =>  //Int input test 
        {
            try
            {
                if (int.Parse(x) >= 1)      //We try to parse to test for null or int <= 1 and non input value 
                {
                    return x;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex) { throw ex; }
        };

        public void komadiComboBoxFill(int RezervacijaID = 0)
        {
            KomadiComboBox.Items.Clear();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd;
                //If RezervacijaID is 0 then add all plays available to KomadiComboBox
                if (RezervacijaID != 0)
                {
                    cmd = new OleDbCommand("SELECT Pozorisni_Komad.KomadID, Naziv FROM Pozorisni_Komad " +
                        "INNER JOIN Stavke_Rezervacije ON Stavke_Rezervacije.KomadID = Pozorisni_Komad.KomadID " +
                        $"WHERE Stavke_Rezervacije.RezervacijaID = {RezervacijaID};", conn);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader.GetInt32(0).ToString() + " " + reader.GetString(1);        //The display format for KomadiComboBox
                            KomadiComboBox.Items.Add(temp);
                            KomadiComboBox.SelectedIndex = 0;
                            KomadiComboBox.Enabled = false;
                        }
                    }
                }
                else
                {
                    cmd = new OleDbCommand("SELECT Pozorisni_Komad.KomadID, Naziv FROM Pozorisni_Komad", conn);
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string temp = reader.GetInt32(0).ToString() + " " + reader.GetString(1);
                            KomadiComboBox.Items.Add(temp);
                        }
                        KomadiComboBox.SelectedIndex = 0;
                        KomadiComboBox.Enabled = true;      //Now the user can choose a new play for the rezervation
                    }
                }
            }
        }

        private void TestUserAction(AppMode mode)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    //Calibrate form for the change of actions
                    OleDbCommand cmd = new OleDbCommand("SELECT RezervacijaID FROM Rezervacija " +
                        $"WHERE RezervacijaID = {Test_for_int(sifraTextBox.Text)};", conn);

                    using (OleDbDataReader TestInput = cmd.ExecuteReader())
                    {
                        if (TestInput.Read())      //old ID
                        {
                            if (mode == 0)
                            {
                                KomadiComboBox.Enabled = false;
                                BrSedistaTextBox.Enabled = false;
                            }
                            else
                            {
                                KomadiComboBox.Enabled = false;
                                BrSedistaTextBox.Enabled = true;
                            }
                        }
                        else                       //New ID
                        {
                            if (mode == 0)
                            {
                                KomadiComboBox.Enabled = true;
                                BrSedistaTextBox.Enabled = true;
                            }
                            else
                            {
                                KomadiComboBox.Enabled = false;
                                BrSedistaTextBox.Enabled = false;
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    Debug.Print("IN TestUserInput: ID is not Int32. ");
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
                    Debug.Print($"IN TestUserInput: ID is greater than {int.MaxValue}");
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
    }
}
