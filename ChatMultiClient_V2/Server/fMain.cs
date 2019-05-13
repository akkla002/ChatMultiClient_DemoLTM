using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class fMain : Form
    {
        
        public fMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            ServerSite.TheServer.Instance.ShowChat = AddContentToRtbx;
            InitializeComponent();
            new Thread(()=> {
                while (true)
                {
                    ChangeUserOnline(ServerSite.TheServer.Instance.ListClient.GetNumberOfClientOnline());
                    Thread.Sleep(300);
                }
            }).Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txbContent.Text == "")
                return;
            ChatContent temp = new ChatContent();
            temp.Content = txbContent.Text;
            ServerSite.TheServer.Instance.SendChatContent(temp);
            AddContentToRtbx(temp);
            txbContent.Clear();
        }
        private void AddContentToRtbx(ChatContent data)
        {
            string s = "";
            if(data.Sender == "")
            {
                s += "Server to all: " + data.Content;
            }
            else
            {
                s += data.Sender + " to Server: " + data.Content;
            }
            rtxbAllContent.Text += s + System.Environment.NewLine;
        }
        

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ServerSite.TheServer.Instance.Stop();
                System.Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }
        public void ChangeUserOnline(int countUserOnline = 0)
        {
            lbNumUserOnline.Text = "Total users online: " + countUserOnline;
        }
    }
}
