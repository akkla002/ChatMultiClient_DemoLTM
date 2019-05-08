using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Server
{
    public class UserAccount : MyObject
    {
        private string userName = "";
        private string password = "";
        private string nickName = "";


        public string NickName
        {
            get
            {
                return nickName;
            }

            set
            {
                nickName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public UserAccount()
        {
        }

        public UserAccount(byte[] data, int size) : base(data, size)
        {
            ByteArr = new byte[1024];
            Buffer.BlockCopy(data, 4, this.ByteArr, 0, size);
            int place = 0;
            int theSize;
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                UserName = Encoding.ASCII.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                Password = Encoding.ASCII.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                NickName = Encoding.ASCII.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            if (place != size)
                throw new Exception("place != size");
            ByteArrLength = size;
        }

        public override void ConfirmSetByte()
        {
            ByteArr = toByte();
        }

        protected override byte[] toByte()
        {
            byte[] data = new byte[1024];
            int place = 0;
            Buffer.BlockCopy(BitConverter.GetBytes(UserName.Length), 0, data, place, 4);
            place += 4;
            if (UserName.Length > 0)
            {
                Buffer.BlockCopy(Encoding.ASCII.GetBytes(UserName), 0, data, place, UserName.Length);
                place += UserName.Length;
            }
            Buffer.BlockCopy(BitConverter.GetBytes(Password.Length), 0, data, place, 4);
            place += 4;
            if (Password.Length > 0)
            {
                Buffer.BlockCopy(Encoding.ASCII.GetBytes(Password), 0, data, place, Password.Length);
                place += Password.Length;
            }
            Buffer.BlockCopy(BitConverter.GetBytes(NickName.Length), 0, data, place, 4);
            place += 4;
            if (NickName.Length > 0)
            {
                Buffer.BlockCopy(Encoding.ASCII.GetBytes(NickName), 0, data, place, NickName.Length);
                place += NickName.Length;
            }
            ByteArrLength = place;
            return data;
        }

        internal bool CheckExisted()
        {
            string query = "exec usp_checkUser @userName , @password";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { UserName, Password });
            if (data.Rows.Count > 0)
            {
                NickName = data.Rows[0]["nickName"].ToString();
                Password = "";
                return true;
            }
            UserName = "";
            Password = "";
            NickName = "temp";
            return false;
        }
    }
}
