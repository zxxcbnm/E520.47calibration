using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class Users_login : Form
    {
        public Users_login()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            //验证用户名和密码
            if (string.IsNullOrEmpty(Users_name.Text) || (string.IsNullOrEmpty(Users_password.Text)))
            {
                MessageBox.Show("请填写用户名和密码！");
                return;
            }


        }
    }
}
