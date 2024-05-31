using OlymmpicManagementSystem.Util;
using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Users_name.Text) || (string.IsNullOrEmpty(Users_password.Text)) || (string.IsNullOrEmpty(password2.Text)))
            {
                MessageBox.Show("请填写用户名和密码！");
                return;
            }
            if (string.IsNullOrEmpty(ad_password.Text))
            {
                MessageBox.Show("请输入管理员密码！", "提示");
                return;
            }
            if (ad_password.Text != "lsl123")
            {
                MessageBox.Show("管理员密码不正确，请重新输入！", "错误提示");
                return;
            }
            if (Users_password.Text == password2.Text)
            {
                string name = Users_name.Text;
                string password = Users_password.Text;

                int row = SqlHelper.ExecuteNonQuery("insert into User_password values('" + name + "','" + password + "','" + cbb_type.SelectedItem.ToString() + "');");
                if (row > 0)
                    MessageBox.Show("注册成功！", "提示");
                Close();
            }
            else
            {
                MessageBox.Show("2次密码不匹配，请重新输入。", "错误提示");
                return;
            }


        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Users_login users_Login = new Users_login();
            users_Login.Show();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
