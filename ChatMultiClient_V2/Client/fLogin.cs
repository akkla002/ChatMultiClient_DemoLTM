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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            //ClientSite client = new ClientSite();
            Server.Logger.Write("Da vao duoc!");
        }

        public bool Login()
        {
            UserAccount user = new UserAccount();
            user.UserName = txbUserName.Text;
            user.Password = txbPassWord.Text;
            ClientSite.Instance.User = user;
            if (ClientSite.Instance.Connect())
            {
                return ClientSite.Instance.Login();
            }
            else
            {
                MessageBox.Show("Lỗi kết nối!");
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(Login()==true)
            {
                
                Server.Logger.Write("Dang nhap thanh cong");
                //Server.Logger.Write(ClientSite.Instance.User.UserName);
                this.Hide();
                fChat f = new fChat();
                ClientSite.Instance.ListFormShow.Add(f);
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
                Server.Logger.Write("Can't connect!");
            }
            ClientSite.Instance.Finish();
        }
    }
}
