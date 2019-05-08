using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Server.ServerSite
{
    internal class ClientConnecting
    {
        private UserAccount user = new UserAccount();

        public delegate void SetUserOnline(ClientConnecting client);
        public SetUserOnline Online = null;
        public delegate void SetDisconnect(ClientConnecting socket);
        public SetDisconnect Disconnected = null;
        public delegate void PassToHigher(MyTransportObject obj);
        public PassToHigher Pass = null;
        private DataTransmission clientDataTrans;

        public UserAccount User
        {
            get
            {
                return user;
            }

            private set
            {
                user = value;
            }
        }

        public ClientConnecting(Socket clientSk)
        {
            this.clientDataTrans = new DataTransmission(clientSk);
            Thread threadListen = new Thread(new ThreadStart(Listen));
            threadListen.Start();
        }
        private void Listen()
        {
            MyTransportObject buffIn;

            //CheckLogin
            if (CheckLogin())
            {
                Logger.Write(User.UserName);
                Online(this);
            }
            else
                return;
            //Listen from client
            while (true)
            {
                buffIn = clientDataTrans.ReceiveData();
                if (buffIn == null )
                {
                    if(Disconnected != null)
                        Disconnected(this);
                    Logger.Write("Ngat ket noi tren class ClientConnecting");
                    break;
                }
                Pass(buffIn);
            }
        }
        internal void SendChatContent(ChatContent data)
        {
            clientDataTrans.SendData(new MyTransportObject(data));
        }
        private bool CheckLogin()
        {
            MyTransportObject dataReceive = clientDataTrans.ReceiveData();
            UserAccount tempUser = dataReceive.Obj as UserAccount;
            if(tempUser.CheckExisted())
            {
                this.User = tempUser;
                SendUser(tempUser);
                return true;
            }
            SendUser(tempUser);
            return false;
        }
        internal void SendUser(UserAccount user)
        {
            clientDataTrans.SendData(new MyTransportObject(user));
        }
    }
}
