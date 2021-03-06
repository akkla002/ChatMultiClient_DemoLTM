﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server

{
    public class DataTransmission
    {
        private Socket destination;
        public DataTransmission(Socket des)
        {
            destination = des;
        }
        public MyTransportObject ReceiveData()
        {
            NetworkStream nw = new NetworkStream(destination);

            byte[] size = new byte[4];
            int byteReceive;
            byte[] data;
            try
            {
                byteReceive = nw.Read(size, 0, 4);
                //destination.Receive(size, 0, 4, SocketFlags.None);
                byteReceive = BitConverter.ToInt32(size, 0);
                if (byteReceive > 1024)
                    throw new Exception("Loi");
                data = new byte[1024];
                byteReceive = nw.Read(data, 0, byteReceive);
                //destination.Receive(data, 0, byteReceive+1, SocketFlags.None);
            }
            catch (Exception e)
            {
                return null;
            }
            if (byteReceive == 0)
            {
                throw new Exception("Nhan bi loi!");
                return null;
            }
            //Logger.ShowByteArr(data,byteReceive);
            MyTransportObject result = new MyTransportObject(data, byteReceive);
            return result;
        }
        public void SendData(MyTransportObject data)
        {
            data.Obj.ConfirmSetByte();
            byte[] packetSize = new byte[4];
            int pkSize = data.Obj.ByteArrLength + 4;
            packetSize = BitConverter.GetBytes(pkSize);
            if (pkSize > 1024)
                throw new Exception("Loi!");
            //Logger.ShowByteArr(data.ToByte(),pkSize);
            //Logger.ShowByteArr(data.Obj.ByteArr, data.Obj.LengthOfByteArr);
            try
            {
                destination.Send(packetSize, 0, 4, SocketFlags.None);
                destination.Send(data.ToByte(), 0, pkSize, SocketFlags.None);
            }
            catch (SocketException e)
            {
                Logger.Write("Loi gui du lieu!");
            }
        }

        internal void Close()
        {
            this.destination.Close();
            //throw new NotImplementedException();
        }
    }
}
