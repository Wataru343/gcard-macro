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

        static public double CalcRequiredRatio(ulong enemyHP, ulong damageUsed1BE, double boost, double combo) => enemyHP / (damageUsed1BE / 1.2 * boost * combo);

        static public ulong ToUlong(string val)
        {
            try
            {
                return Convert.ToUInt64(val);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch(OverflowException)
            {
                return ulong.MaxValue;
            }
        }

        static public long ToLong(string val)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return long.MaxValue;
            }
        }

        static public int ToInt(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return int.MaxValue;
            }
        }

        static public uint ToUInt(string val)
        {
            try
            {
                return Convert.ToUInt32(val);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return uint.MaxValue;
            }
        }

        static public double ToDouble(string val)
        {
            try
            {
                return Convert.ToDouble(val);
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (OverflowException)
            {
                return double.MaxValue;
            }
        }
    }
}
