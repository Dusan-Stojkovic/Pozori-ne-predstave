using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozorišne_predstave
{
    partial class PoTrupamaForm
    {
        private void TrupaCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrupaCombo.Text == "")
            {
                IzvrsiButton.Enabled = false;
            }
            else { IzvrsiButton.Enabled = true; }
        }
    }
}
