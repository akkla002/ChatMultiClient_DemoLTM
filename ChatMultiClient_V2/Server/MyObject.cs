using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public abstract class MyObject
    {
        protected byte[] byteArr = null;
        protected int byteArrLength;

        public byte[] ByteArr
        {
            get
            {
                if (byteArr == null)
                {
                    ByteArr = toByte();
                }
                return byteArr;
            }

            protected set
            {
                byteArr = value;
            }
        }
        public int ByteArrLength
        {
            get
            {
                return byteArrLength;
            }

            protected set
            {
                byteArrLength = value;
            }
        }
        public MyObject() { }
        public MyObject(byte[] data, int size)
        {

        }
        protected abstract byte[] toByte();
        public abstract void ConfirmSetByte();
    }
}
