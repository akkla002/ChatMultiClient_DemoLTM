using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Server.ServerSite
{
    internal class ListClientOnline
    {
        List<ClientConnecting> listClients;
        private Socket serverSocket;
        private Thread threadAccept = null;

        public delegate void SendMyObjectToServer(ChatContent obj);
        public SendMyObjectToServer SendMyObject = null;
        public ListClientOnline(Socket server)
        {
            listClients = new List<ClientConnecting>();
            serverSocket = server;
        }

        public int GetNumberOfClientOnline()
        {
            return listClients.Count;
        }
        #region Methods
        public void Start()
        {
            threadAccept = new Thread(new ThreadStart(Run));
            if (threadAccept != null)
                threadAccept.Start();
        }
        private void Run()
        {
            serverSocket.Listen(10);
            while (true)
            {
                Socket client = serverSocket.Accept();
                Console.WriteLine("Co client ket noi!");
                ClientConnecting tempClient = new ClientConnecting(client);
                tempClient.Online = AddOnline;
            }
        }
        private void AddOnline(ClientConnecting user)
        {
            //this method still bug
            SendNotification(user);


            listClients.Add(user);
            user.Disconnected = RemoveClientOnline;
            user.Pass = ProcessTranportObject;
            Logger.Write(user.User.UserName + " da dang nhap");
            if (listClients.Count > 19)
                threadAccept.Suspend();
        }
        private void RemoveClientOnline(ClientConnecting client)
        {
            //TheServer.Instance.ChangeStatus(client.User.UserName);
            if (this.listClients.Remove(client))
                Logger.Write("Co client tat!");
            if (!this.listClients.Remove(client))
                Logger.Write("Da remove client " + client.User.UserName);
            new Thread(() => {
                foreach (ClientConnecting item in listClients)
                {
                    item.SendUser(client.User);
                }
            }).Start();
            //SendAllUser(client, false);
            if (threadAccept.ThreadState == ThreadState.Suspended)
                threadAccept.Resume();
        }

        internal bool IsOnline(UserAccount userAccount)
        {
            foreach(ClientConnecting item in listClients)
            {
                if (item.User.UserName == userAccount.UserName)
                    return true;
            }
            return false;
        }

        internal void SendChatContent(ChatContent data)
        {
            Thread tempThread = new Thread(()=>
            {
                foreach (ClientConnecting item in listClients)
                {
                        //Check user
                    if (data.Receiver == "" || data.Sender == "" || data.Sender == item.User.UserName || data.Receiver == item.User.UserName)
                    {
                        item.SendChatContent(data);
                    }
                }
            });
            tempThread.Start();
        }
        private void SendNotification(ClientConnecting newClient)
        {
            new Thread(() => {
                if (listClients.Count > 0)
                    foreach (ClientConnecting item in listClients)
                    {
                        if (item == newClient)
                            continue;
                        //Send UserOnline to the new client
                        newClient.SendUser(item.User);

                        //Send new client to users online
                        item.SendUser(newClient.User);

                        Thread.Sleep(500);
                        //Logger.Write(item.User.UserName);
                    }
                SendHistoryChatContent(newClient);
            }).Start();
        }
        private void ProcessTranportObject(MyTransportObject obj)
        {
            if (obj.Obj is ChatContent)
                SendMyObject(obj.Obj as ChatContent);
            Logger.Write(obj.Obj.ToString());
        }
        internal void SendHistoryChatContent(ClientConnecting client)
        {
            client.SendListChatContent(client.User.GetHistoryChatContent());
        }

        public void Stop()
        {
            threadAccept.Abort();
            foreach(ClientConnecting client in listClients)
            {
                client.Close();
            }
        }
        #endregion
    }
}
