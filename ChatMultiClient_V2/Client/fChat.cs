using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace Client
{

    public partial class fChat : Form
    {
        //private Server.UserAccount destination;

        public fChat()
        {
            ClientSite.Instance.AddOrRemove = AddOrRemoveUser;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //this.Destination = user;
        }
        List<UserAccount> listUserOnline = new List<UserAccount>();
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbContent.Text == "")
                return;
            ClientSite.Instance.SendChatContent(txbContent.Text,txbReceiver.Text);
            txbContent.Clear();
        }

        public void ShowChatContent(ChatContent data)
        {
            string s = "";
            if(data.Sender == "" && data.Receiver == "")
            {
                s += "Server to " + ClientSite.Instance.User.UserName + ": " + data.Content;
            }
            else
            {
                if (data.Sender == "")
                    s += "Server to " + data.Receiver + ": " + data.Content;
                else if (data.Receiver == "")
                    s += data.Sender + " to Server: " + data.Content;
                else s = data.ToString();
            }
            rtxbAllContent.Text += s + System.Environment.NewLine;
        }
        private void AddOrRemoveUser(UserAccount user)
        {

            //lbUserOnline.Text += user.UserName + System.Environment.NewLine;
            Logger.Write(user.UserName);
            return;


            foreach (UserAccount item in listUserOnline)
            {
                Logger.Write(item.UserName);
                if (item.UserName == user.UserName)
                {
                    listUserOnline.Remove(item);
                    foreach (Control ct in pnUserOnline.Controls)
                    {
                        if(ct is Button)
                        {
                            Button tempBt = ct as Button;
                            if (tempBt.Name == user.UserName)
                                pnUserOnline.Controls.Remove(ct);
                        }
                    }
                    return;
                }
            }
            Logger.Write("Dang adding");
            if (false)
            {
                Button btnUser = new Button();
                btnUser.Location = new System.Drawing.Point(3, 3);
                btnUser.Name = user.UserName;
                btnUser.Size = new System.Drawing.Size(122, 32 * listUserOnline.Count);
                btnUser.Text = user.UserName;
                btnUser.Click += new EventHandler((object sender, EventArgs e) => {
                    txbReceiver.Text = (sender as Button).Text;
                });
                this.Invoke((MethodInvoker)delegate
                {
                    //perform on the UI thread
                    pnUserOnline.Controls.Add(btnUser);
                    Logger.Write("Da add xong");
                });
            }
            else
            {
            }
            listUserOnline.Add(user);
        }
    }
}
