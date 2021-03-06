﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server;
using System.Windows.Forms;

namespace Client
{
    public class ClientSite
    {
        private static ClientSite instance = null;
        public static ClientSite Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientSite();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }


        public UserAccount User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public List<Form> ListFormShow
        {
            get
            {
                return listFormShow;
            }

            set
            {
                listFormShow = value;
            }
        }

        public List<UserAccount> ListUserOnline
        {
            get
            {
                return listUserOnline;
            }

            private set
            {
                listUserOnline = value;
            }
        }

        private List<UserAccount> listUserOnline;
        IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse(Server.MyConstant.SERVER_IP), Server.MyConstant.PORT_SERVER);
        Socket clientSocket;
        DataTransmission dataTrans;
        UserAccount user;
        List<Form> listFormShow;

        public delegate void AddOrRemoveOnlineUser(UserAccount user);
        public AddOrRemoveOnlineUser AddOrRemove = null;

        Thread threadListen;

        private ClientSite()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataTrans = new DataTransmission(clientSocket);
            User = new UserAccount();
            ListUserOnline = new List<UserAccount>();
            ListUserOnline.Add(new UserAccount() {NickName = "Server" });
            threadListen = new Thread(StartReceiveData);

            ListFormShow = new List<Form>();
        }
        public bool Connect()
        {
            try
            {
                clientSocket.Connect(ipepServer);
            }
            catch (SocketException)
            {
                Server.Logger.Write("Ket noi that bai!");
                return false;
            }
            //threadListen.Start();
            return true;
        }
        public bool Login()
        {
            dataTrans.SendData(new MyTransportObject(this.User));
            MyTransportObject receiveData = dataTrans.ReceiveData();
            UserAccount receiveUser = receiveData.Obj as UserAccount;
            if (receiveUser.UserName == "")
                return false;
            this.User = receiveUser;
            threadListen.Start();
            return true;
        }
        public void Finish()
        {
            threadListen.Abort();
            clientSocket.Close();
            Server.Logger.Write("Da ngat ket noi!");
            Instance = null;
        }
        public void StartReceiveData()
        {
            Thread.Sleep(100);
            Server.MyTransportObject buffIn;
            while (true)
            {
                buffIn = dataTrans.ReceiveData();
                if (buffIn == null)
                {
                    MessageBox.Show("Ngắt kết nối đến server!", "Thông báo!", MessageBoxButtons.OK);
                    Form temp = this.ListFormShow[0];
                    temp.Close();
                    this.ListFormShow.Remove(temp);
                    clientSocket.Close();
                    Finish();
                    //Application.Exit();
                    break;
                }
                ProcessObject(buffIn.Obj);
            }
        }
        public void SendChatContent(string data, string destination)
        {
            ChatContent chatContent = new ChatContent(User.UserName, destination, data);
            dataTrans.SendData(new MyTransportObject(chatContent));
        }
        public void ProcessObject(Server.MyObject obj)
        {
            //if(obj is Server.Database.ChatContent)
            if (obj is Server.ChatContent)
            {
                ChatContent temp = obj as ChatContent;
                fChat item = ListFormShow[0] as fChat;
                if ("" == temp.Receiver && temp.Sender == "")
                    temp.Receiver = User.UserName;
                item.ShowChatContent(temp);
            }
            else if (obj is UserAccount)
            {
                RemoveOrAddUserOnline(obj as UserAccount);
            }
        }
        private void RemoveOrAddUserOnline(UserAccount inputUser)
        {
            (ListFormShow[0] as fChat).AddOrRemoveOnlineUser(inputUser);
            foreach (UserAccount item in ListUserOnline)
            {
                if(item.UserName == inputUser.UserName)
                {
                    ListUserOnline.Remove(item);
                    return;
                }
            }
            ListUserOnline.Add(inputUser);
            return;
        }
    }
}
