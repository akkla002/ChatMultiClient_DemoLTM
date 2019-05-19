using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Server
{
    public class ChatContent : MyObject
    {
        private string sender = "";
        private string receiver = "";
        private string content = "";
        
        #region Constructor
        public ChatContent()
        {
        }
        public ChatContent(string sender, string receiver, string content)
        {
            Sender = sender;
            Receiver = receiver;
            Content = content;
        }
        public ChatContent(byte[] data, int size) : base(data, size)
        {
            ByteArr = new byte[size];
            Buffer.BlockCopy(data, 4, ByteArr, 0, size);
            int place = 0;
            int theSize;
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                Sender = Encoding.UTF8.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            else
                Sender = "";
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                Receiver = Encoding.UTF8.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            else
                Receiver = "";
            theSize = BitConverter.ToInt32(ByteArr, place);
            place += 4;
            if (theSize > 0)
            {
                Content = Encoding.UTF8.GetString(ByteArr, place, theSize);
                place += theSize;
            }
            if (place != size)
                throw new Exception("The user not same size");
            ByteArrLength = size;
        }

        internal ChatContent(DataRow data)
        {
            Sender = data["sender"].ToString();
            Receiver = data["receiver"].ToString();
            Content = data["content"].ToString();
        }
        #endregion

        #region Properties
        public string Sender
        {
            get
            {
                return sender;
            }

            set
            {
                sender = value;
            }
        }

        public string Receiver
        {
            get
            {
                return receiver;
            }

            set
            {
                receiver = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        #endregion

        #region Methods
        public override void ConfirmSetByte()
        {
            //throw new NotImplementedException();
            ByteArr = toByte();

        }
        private void SetDefaltValue()
        {
            if (sender == null)
                sender = "";
            if (receiver == null)
                receiver = "";
            if (content == null)
                content = "";

        }
        protected override byte[] toByte()
        {
            SetDefaltValue();
            byte[] data = new byte[1024];
            int place = 0;
            Buffer.BlockCopy(BitConverter.GetBytes(Sender.Length), 0, data, place, 4);
            place += 4;
            if (Sender.Length > 0)
            {
                Buffer.BlockCopy(Encoding.UTF8.GetBytes(Sender), 0, data, place, Sender.Length);
                place += Sender.Length;
            }
            Buffer.BlockCopy(BitConverter.GetBytes(Receiver.Length), 0, data, place, 4);
            place += 4;
            if (Receiver.Length > 0)
            {
                Buffer.BlockCopy(Encoding.UTF8.GetBytes(Receiver), 0, data, place, Receiver.Length);
                place += Receiver.Length;
            }
            Buffer.BlockCopy(BitConverter.GetBytes(Content.Length), 0, data, place, 4);
            place += 4;
            if (Content.Length > 0)
            {
                Buffer.BlockCopy(Encoding.UTF8.GetBytes(Content), 0, data, place, Content.Length);
                place += Content.Length;
            }
            ByteArrLength = place;
            return data;
        }

        public override string ToString()
        {
            return string.Format("{0} to {1}: {2}", Sender, Receiver, Content);
            //return toStr01();
            //return base.ToString();
        }
        private string toStr01()
        {
            string s = "";
            
            if (Sender == "")
                s += "Server to ";
            else s += Sender + " to ";
            if (Receiver == "" && Sender == "")
                s += "All";
            else if(Receiver=="") s += Receiver;
            s += ": " + Content;
            //s = Sender + ": " + Content;
            return s;
        }

        internal bool Save()
        {
            string query = "exec usp_create_chatContent @sender , @receiver , @content";
            int resuilt = DataProvider.Instance.ExecuteNonQuery(query, new object[] { Sender,Receiver,Content });
            if (resuilt > 0)
                return true;
            else return false;
        }

        #endregion
    }
}
