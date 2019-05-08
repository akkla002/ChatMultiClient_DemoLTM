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
            
            rtxbAllContent.Text += data.ToString() + System.Environment.NewLine;
        }
        public void AddOrRemoveUser(UserAccount user)
        {
            foreach (UserAccount item in listUserOnline)
            {
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
            Button btnUser = new Button();
            btnUser.Location = new System.Drawing.Point(3, 3);
            btnUser.Name = user.UserName;
            btnUser.Size = new System.Drawing.Size(122, 32 * listUserOnline.Count);
            btnUser.Text = user.UserName;
            btnUser.Click += BtnUser_Click; this.Invoke((MethodInvoker)delegate
            {
                //perform on the UI thread
                this.pnUserOnline.Controls.Add(btnUser);
            });
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            txbReceiver.Text = (sender as Button).Text;
        }
    }
}
