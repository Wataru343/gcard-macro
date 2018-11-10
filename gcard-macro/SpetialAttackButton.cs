using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcard_macro
{
    public class SpecialAttackButton
    {
        public enum AttackType
        {
            Combo30,
            Attack20,
            Attack10,
            BE1
        };

        public AttackType Type { get; set; }
        public bool Use { get; set; }
        public bool Normal { get; set; }
        public bool Mira { get; set; }
        public bool Boost { get; set; }
        virtual public string LargeButtonName
        {
            get
            {
                return "";
            }
        }

        virtual public string SmallButtonName
        {
            get
            {
                return "";
            }
        }
    }

    public class Combo30Button : SpecialAttackButton
    {
        public bool FirstAttack { get; set; }

        override public string LargeButtonName
        {
            get
            {
                return "s_c_1_sp.png";
            }
        }

        override public string SmallButtonName
        {
            get
            {
                return "s_m_c_1_sp.png";
            }
        }

        override public string ToString() { return "BEx1 1.2倍+30コンボ攻撃"; }
    }

    public class Attack20Button : SpecialAttackButton
    {
        public bool RequiredRatio { get; set; }

        override public string LargeButtonName
        {
            get
            {
                return "s_a_3_sp.png";
            }
        }

        override public string SmallButtonName
        {
            get
            {
                return "s_m_a_3_sp.png";
            }
        }

        override public string ToString() { return "BEx3 20倍攻撃"; }
    }

    public class Attack10Button : SpecialAttackButton
    {
        override public string LargeButtonName
        {
            get
            {
                return "m_5_sp.png";
            }
        }

        override public string SmallButtonName
        {
            get
            {
                return "s_m_5_sp.png";
            }
        }

        override public string ToString() { return "BEx5 10倍攻撃"; }
    }

    public class BE1Button : SpecialAttackButton
    {
        public bool RequiredRatio { get; set; }

        override public string LargeButtonName
        {
            get
            {
                return "b_l_3_sp.png";
            }
        }

        override public string SmallButtonName
        {
            get
            {
                return "s_b_l_3_sp.png";
            }
        }

        override public string ToString() { return "BEカプセルx1 20倍攻撃"; }
    }
}
