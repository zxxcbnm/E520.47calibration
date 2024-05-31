using OlymmpicManagementSystem.Util;
using System;
using System.Data;
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
            Register register = new Register();
            Hide();
            register.Show();
        }
        private void Confirm_login()
        {
            //验证用户名和密码
            if (string.IsNullOrEmpty(Users_name.Text) || (string.IsNullOrEmpty(Users_password.Text)))
            {
                MessageBox.Show("请填写用户名和密码！");
                return;
            }
            string name = Users_name.Text;
            string password = Users_password.Text;

            DataTable dataTable = SqlHelper.ExecuteTable("select * from User_password where name = '" + name + "' and password = '" + password + "'");
            if (dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                Form1.character = dataRow["job"].ToString();
                Form1.users_name = name;
                //MessageBox.Show(Form1.character, "提示");
                Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误", "提示");
            }
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            Confirm_login();
        }
        private void Users_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }
        private void Users_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Confirm_login();
            }
        }
        private void Users_login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Form1.character == "")
                Form1.character = "未登录";
        }
    }
}
