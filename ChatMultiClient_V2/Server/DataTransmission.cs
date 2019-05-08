using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

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
            byte[] size = new byte[4];
            int byteReceive;
            byte[] data;
            try
            {
                destination.Receive(size, 0, 4, SocketFlags.None);
                byteReceive = BitConverter.ToInt32(size, 0);
                data = new byte[byteReceive+1];
                destination.Receive(data, 0, byteReceive+1, SocketFlags.None);
            }
            catch (SocketException e)
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
    }
}
