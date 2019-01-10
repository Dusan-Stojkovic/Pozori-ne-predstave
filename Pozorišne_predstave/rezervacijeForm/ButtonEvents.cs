using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pozorišne_predstave
{
    partial class RezervacijeForm
    {
        public void IzadjiButton_Click(object sender, EventArgs e)
        {
            PozoristeForm f = new PozoristeForm();
            f.Show();
            this.Close();
        }

        public void IzvrsiButton_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    if (UpisiRadio.Checked == true)
                    {
                        //KomadiComboBox is only available to new reservations
                        if (KomadiComboBox.Enabled == true)
                        {
                            string[] temp = KomadiComboBox.SelectedItem.ToString().Split(' ');  //split to get KomadID
                                                                                                //No pretplatnik inserted
                            OleDbCommand cmd1 = new OleDbCommand("INSERT INTO Rezervacija (RezervacijaID, DatumRezervisanja) " +
                                $"VALUES ({sifraTextBox.Text}, #{DateTime.Now}#)", conn);       //Does not send correct date
                            cmd1.ExecuteNonQuery();
                            OleDbCommand cmd = new OleDbCommand("INSERT INTO Stavke_Rezervacije (RezervacijaID, RBR, KomadID, KolikoSedista) " +
                                $"VALUES ({sifraTextBox.Text}, {redniBrTextBox.Text}, {temp[0]}, " +
                                $"{BrSedistaTextBox.Text});", conn);
                            cmd.ExecuteNonQuery();
                            OleDbCommand cmd3 = new OleDbCommand("SELECT Datum FROM Predstava " +
                                $"WHERE KomadID = {temp[0]};", conn);
                            using (OleDbDataReader reader = cmd3.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var inputdate = reader.GetDateTime(0);
                                    OleDbCommand cmd2 = new OleDbCommand("UPDATE Stavke_Rezervacije " +
                                        $"SET Datum = #{inputdate}# " +
                                        $"WHERE RezervacijaID = {sifraTextBox.Text};", conn);
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Nova rezervacija uspesno uneta.");
                                    redniBrTextBox.Text = (int.Parse(redniBrTextBox.Text) + 1).ToString();

                                    KomadiComboBox.Enabled = false;
                                    BrSedistaTextBox.Enabled = false;
                                }
                            }
                        }
                    }
                    else if (IzbrisiRadio.Checked == true)
                    {
                        string inputnumber;
                        if (BrSedistaTextBox.Text == "")
                            inputnumber = "0";
                        else
                            inputnumber = BrSedistaTextBox.Text;

                        try
                        {
                            OleDbCommand command = new OleDbCommand($"UPDATE Stavke_Rezervacije SET KolikoSedista = {inputnumber} " +
                                    $"WHERE RezervacijaID = {sifraTextBox.Text};", conn);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Broj sedista rezervisan promenjen.");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
