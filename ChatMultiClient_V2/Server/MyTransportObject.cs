using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class MyTransportObject
    {
        private TypeMyObject type;
        MyObject obj;

        public TypeMyObject Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public MyObject Obj
        {
            get
            {
                return obj;
            }

            set
            {
                obj = value;
            }
        }

        public MyTransportObject(byte[] input, int size)
        {

            int intType = BitConverter.ToInt32(input, 0);
            this.Type = (TypeMyObject)intType;
            switch (Type)
            {
                case TypeMyObject.Account:
                    Obj = new UserAccount(input, size - 4);
                    break;
                case TypeMyObject.ChatContent:
                    Obj = new ChatContent(input, size - 4);
                    break;
                default:
                    throw new Exception(intType.ToString());
                    break;
            }
        }
        public MyTransportObject(MyObject obj)
        {
            this.Obj = obj;
            SetType();
        }
        public byte[] ToByte()
        {
            byte[] data = new byte[Obj.ByteArrLength + 4];
            int place = 0;
            int temp = (int)Type;
            Buffer.BlockCopy(BitConverter.GetBytes(temp), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(Obj.ByteArr, 0, data, place, Obj.ByteArrLength);
            return data;
        }
        private void SetType()
        {
            if (Obj is UserAccount)
                Type = TypeMyObject.Account;
            else if (Obj is ChatContent)
                Type = TypeMyObject.ChatContent;
            else
                throw new Exception("Loi");
            
        }
        public enum TypeMyObject
        {
            Account = 30,
            ChatContent = 42,
        }
    }
}
