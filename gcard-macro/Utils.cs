using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gcard_macro
{
    class Utils
    {
        static public bool ValidDouble(TextBox textBox, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return false;

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                if (e.KeyChar == 22 || e.KeyChar == 3) return false;

            try
            {
                double.Parse(textBox.Text + e.KeyChar);
                return false;
            }
            catch
            {
                return true;
            }
        }

        static public bool ValidUlong(TextBox textBox, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back) return false;

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                if (e.KeyChar == 22 || e.KeyChar == 3) return false;

            try
            {
                ulong.Parse(textBox.Text + e.KeyChar);
                return false;
            }
            catch
            {
                return true;
            }
        }

        static public int CalcUseMiniCapsules(ulong enemyHP, ulong damageUsed1BE, double boost, double combo) => (int)(enemyHP / ((damageUsed1BE / 1.2) * boost * combo));
    }
}
