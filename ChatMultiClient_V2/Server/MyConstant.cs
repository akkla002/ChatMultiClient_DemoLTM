using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Server
{
    public class MyConstant
    {
        public const int PORT_SERVER = 62222;
        public const int HIGH_USER_BUTTON = 30;
        private static Color offLine_User = Color.Red;
        private static Color oNLINE_USER = Color.Green;

        public static Color OffLine_User
        {
            get
            {
                return offLine_User;
            }

            private set
            {
                offLine_User = value;
            }
        }

        public static Color ONLINE_USER
        {
            get
            {
                return oNLINE_USER;
            }

            private set
            {
                oNLINE_USER = value;
            }
        }
    }
}
