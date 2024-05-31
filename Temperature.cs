using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class SENT_T : Form
    {
        public SENT_T()
        {
            InitializeComponent();
        }
        private static int T1_MODE;
        private static int T2_MODE;
        private static int TV_LIM;
        private static int TSP_LIM;
        private static int RT1_LIM;
        private static int RT2_LIM;
        private static int ERR_EN2_1;
        private static int ERR_EN4_3;
        private void SENT_T_Load(object sender, EventArgs e)
        {
            if(Form1.Temperature_Flag == false)
            {
                // 关联 ToolTip 控件和其他控件
                toolTip1.SetToolTip(btn_Save, "修改内容不会保存。");
                toolTip1.SetToolTip(groupBox1, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox2, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox3, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox4, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox5, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox6, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(cbx_T_path, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_TSP_L, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_TSP_H, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(cbx_TV, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_voter_L, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_voter_H, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(ckb_idio_off, "当前账户修改无效，请使用管理员登录。");
                btn_Save.Text = "退出";
            }

            tbx_T1_MODE.Text = "0x" + Form1.NVM_code[0x68];
            tbx_T2_MODE.Text = "0x" + Form1.NVM_code[0x6E];
            tbx_TV_LIM.Text = "0x" + Form1.NVM_code[0x45];
            tbx_TSP_LIM.Text = "0x" + Form1.NVM_code[0x4F];
            tbx_RT1_LIM.Text = "0x" + Form1.NVM_code[0x42];
            tbx_RT2_LIM.Text = "0x" + Form1.NVM_code[0x43];

            T1_MODE = Convert.ToInt32(Form1.NVM_code[0x68], 16);
            T2_MODE = Convert.ToInt32(Form1.NVM_code[0x6E], 16);
            TV_LIM = Convert.ToInt32(Form1.NVM_code[0x45], 16);
            TSP_LIM = Convert.ToInt32(Form1.NVM_code[0x4F], 16);
            RT1_LIM = Convert.ToInt32(Form1.NVM_code[0x42], 16);
            RT2_LIM = Convert.ToInt32(Form1.NVM_code[0x43], 16);
            ERR_EN2_1 = Convert.ToInt32(Form1.NVM_code[0x3F], 16);
            ERR_EN4_3 = Convert.ToInt32(Form1.NVM_code[0x40], 16);
            #region **将配置分配到各个选项**
            int a;
            //T1_MODE
            a = T1_MODE & 3;
            switch (a)
            {
                case 0: rbn_T1_gain1.Checked = true; break;
                case 2: rbn_T1_gain2.Checked = true; break;
                case 3: rbn_T1_gain4.Checked = true; break;
                default: break;
            }
            a = (T1_MODE >> 2) & 3;
            switch (a)
            {
                case 0:
                    rbn_T1_in.Checked = true;
                    rbn_T1_gain1.Enabled = true;
                    rbn_T1_gain2.Enabled = false;
                    rbn_T1_gain4.Enabled = false;
                    break;
                case 1:
                    rbn_T1_TSEN1.Checked = true;
                    rbn_T1_gain1.Enabled = true;
                    rbn_T1_gain2.Enabled = true;
                    rbn_T1_gain4.Enabled = true;
                    break;
                case 2:
                    rbn_T1_TSEN2.Checked = true;
                    rbn_T1_gain1.Enabled = true;
                    rbn_T1_gain2.Enabled = true;
                    rbn_T1_gain4.Enabled = true;
                    break;
                case 3:
                    rbn_T1_bridge.Checked = true;
                    rbn_T1_gain1.Enabled = false;
                    rbn_T1_gain2.Enabled = true;
                    rbn_T1_gain4.Enabled = true;
                    break;
                default: break;
            }
            a = (T1_MODE >> 7) & 1;
            if (a == 1) ckb_idio_off.Checked = true; else ckb_idio_off.Checked = false;

            //T2_MODE
            a = T2_MODE & 3;
            switch (a)
            {
                case 0: rbn_T2_gain1.Checked = true; break;
                case 2:
                    int b = (T2_MODE >> 4) & 1;
                    if (b == 0)
                        rbn_T2_gain2.Checked = true;
                    else rbn_T2_gain3.Checked = true;
                    break;
                case 3: rbn_T2_gain4.Checked = true; break;
                default: break;
            }
            a = (T2_MODE >> 2) & 3;
            switch (a)
            {
                case 0:
                    rbn_T2_in.Checked = true;
                    rbn_T2_gain1.Enabled = true;
                    rbn_T2_gain2.Enabled = false;
                    rbn_T2_gain3.Enabled = false;
                    rbn_T2_gain4.Enabled = false;
                    break;
                case 1:
                    rbn_T2_TSEN1.Checked = true;
                    rbn_T2_gain1.Enabled = true;
                    rbn_T2_gain2.Enabled = true;
                    rbn_T2_gain3.Enabled = true;
                    rbn_T2_gain4.Enabled = true;
                    break;
                case 2:
                    rbn_T2_TSEN2.Checked = true;
                    rbn_T2_gain1.Enabled = true;
                    rbn_T2_gain2.Enabled = true;
                    rbn_T2_gain3.Enabled = true;
                    rbn_T2_gain4.Enabled = true;
                    break;
                case 3:
                    rbn_T2_bridge.Checked = true;
                    rbn_T2_gain1.Enabled = false;
                    rbn_T2_gain2.Enabled = true;
                    rbn_T2_gain3.Enabled = false;
                    rbn_T2_gain4.Enabled = true;
                    break;
                default: break;
            }

            //TV_LIM
            a = TV_LIM & 0xFF;
            float temp = a;
            temp /= 4;
            if (a <= 127)
            {
                tbx_voter_L.Text = a.ToString("D");
                tbx_voter1.Text = "= +" + temp.ToString("f3") + "K";
            }
            else
            {
                a -= 256;
                tbx_voter_L.Text = a.ToString("D");
                tbx_voter1.Text = "= " + temp.ToString("f3") + "K";
            }
            a = (TV_LIM >> 8) & 0xFF;
            temp = a;
            temp /= 4;
            if (a <= 127)
            {
                tbx_voter_H.Text = a.ToString("D");
                tbx_voter2.Text = "= +" + temp.ToString("f3") + "K";
            }
            else
            {
                a -= 256;
                tbx_voter_H.Text = a.ToString("D");
                tbx_voter2.Text = "= " + temp.ToString("f3") + "K";
            }

            //TSP_LIM
            a = TSP_LIM & 0xFF;
            tbx_TSP_L.Text = a.ToString("D");
            temp = a;
            temp /= 256;
            temp *= 100;
            tbx_TSP1.Text = "= " + temp.ToString("f1") + " %FS";
            a = (TSP_LIM >> 8) & 0xFF;
            tbx_TSP_H.Text = a.ToString("D");
            temp = a;
            temp /= 256;
            temp *= 100;
            tbx_TSP2.Text = "= " + temp.ToString("f1") + " %FS";

            //RT1_LIM
            float temp1;
            int c = T1_MODE & 3;
            a = RT1_LIM & 0xFF;
            tbx_T1_min.Text = a.ToString("D");
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            if (c == 0) temp1 /= 192;
            else if (c == 2) temp1 /= 64;
            else if (c == 3) temp1 /= 512;
            tbx_T1_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";

            a = (RT1_LIM >> 8) & 0xFF;
            tbx_T1_max.Text = a.ToString("D");
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            if (c == 0) temp1 /= 192;
            else if (c == 2) temp1 /= 64;
            else if (c == 3) temp1 /= 512;
            tbx_T1_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";

            //RT2_LIM
            c = T2_MODE & 3;
            a = RT2_LIM & 0xFF;
            tbx_T2_min.Text = a.ToString("D");
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            if (c == 0) temp1 /= 192;
            else if (c == 2) temp1 /= 64;
            else if (c == 3) temp1 /= 512;
            tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";

            a = (RT2_LIM >> 8) & 0xFF;
            tbx_T2_max.Text = a.ToString("D");
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            if (c == 0) temp1 /= 192;
            else if (c == 2) temp1 /= 64;
            else if (c == 3) temp1 /= 512;
            tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";

            //ERR_EN2_1
            a = (ERR_EN2_1 >> 2) & 1;
            if (a == 1)
            {
                cbx_T_path.Checked = true;
                tbx_TSP_L.Enabled = true;
                tbx_TSP_H.Enabled = true;
            }
            else
            {
                cbx_T_path.Checked = false;
                tbx_TSP_L.Enabled = false;
                tbx_TSP_H.Enabled = false;
            }

            //ERR_EN2_1
            a = (ERR_EN4_3 >> 6) & 1;
            if (a == 1)
            {
                cbx_TV.Checked = true;
                tbx_voter_L.Enabled = true;
                tbx_voter_H.Enabled = true;
                tbx_voter1.Visible = true;
                tbx_voter2.Visible = true;
            }
            else
            {
                cbx_TV.Checked = false;
                tbx_voter_L.Enabled = false;
                tbx_voter_H.Enabled = false;
                tbx_voter1.Visible = false;
                tbx_voter2.Visible = false;
            }
            #endregion
        }

        private void cbx_T_path_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_T_path.Checked)
            {
                ERR_EN2_1 |= (1 << 2);
                tbx_TSP_L.Enabled = true;
                tbx_TSP_H.Enabled = true;
            }
            else
            {
                ERR_EN2_1 &= ~(1 << 2);
                tbx_TSP_L.Enabled = false;
                tbx_TSP_H.Enabled = false;
            }
        }

        private void cbx_TV_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_TV.Checked)
            {
                ERR_EN2_1 |= (1 << 6);
                tbx_voter_L.Enabled = true;
                tbx_voter_H.Enabled = true;
                tbx_voter1.Visible = true;
                tbx_voter2.Visible = true;
            }
            else
            {
                ERR_EN2_1 &= ~(1 << 6);
                tbx_voter_L.Enabled = false;
                tbx_voter_H.Enabled = false;
                tbx_voter1.Visible = false;
                tbx_voter2.Visible = false;
            }
        }

        private void tbx_TSP_L_TextChanged(object sender, EventArgs e)
        {
            if (tbx_TSP_L.Text == "") tbx_TSP_L.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_TSP_L.Text);
                if (a > 255) { a = 255; tbx_TSP_L.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a;
                temp /= 256;
                temp *= 100;
                tbx_TSP1.Text = "= " + temp.ToString("f1") + " %FS";
                TSP_LIM &= 0xFF00;
                TSP_LIM |= a;
                tbx_TSP_LIM.Text = "0x" + TSP_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void tbx_TSP_H_TextChanged(object sender, EventArgs e)
        {
            if (tbx_TSP_H.Text == "") tbx_TSP_H.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_TSP_H.Text);
                if (a > 255) { a = 255; tbx_TSP_H.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a;
                temp /= 256;
                temp *= 100;
                tbx_TSP2.Text = "= " + temp.ToString("f1") + " %FS";
                TSP_LIM &= 0x00FF;
                TSP_LIM |= (a << 8);
                tbx_TSP_LIM.Text = "0x" + TSP_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void tbx_voter_L_TextChanged(object sender, EventArgs e)
        {
            if (tbx_voter_L.Text == "") tbx_voter_L.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_voter_L.Text);
                if (a < -127) { a = -127; tbx_voter_L.Text = "-127"; }
                else if (a > 127) { a = 127; tbx_voter_L.Text = "127"; }
                float temp = a;
                temp /= 4;
                TV_LIM &= 0xFF00;
                if (temp >= 0)
                {
                    tbx_voter1.Text = "= +" + temp.ToString("f3") + "K";
                    TV_LIM |= a;
                }
                else
                {
                    tbx_voter1.Text = "= " + temp.ToString("f3") + "K";
                    TV_LIM |= (a + 256);
                }
                tbx_TV_LIM.Text = "0x" + TV_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围-127~127", "格式错误"); }

        }

        private void tbx_voter_H_TextChanged(object sender, EventArgs e)
        {
            if (tbx_voter_H.Text == "") tbx_voter_H.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_voter_H.Text);
                if (a < -127) { a = -127; tbx_voter_H.Text = "-127"; }
                else if (a > 127) { a = 127; tbx_voter_H.Text = "127"; }
                float temp = a;
                temp /= 4;
                TV_LIM &= 0x00FF;
                if (temp >= 0)
                {
                    tbx_voter2.Text = "= +" + temp.ToString("f3") + "K";
                    TV_LIM |= (a << 8);
                }
                else
                {
                    tbx_voter2.Text = "= " + temp.ToString("f3") + "K";
                    TV_LIM |= ((a + 256) << 8);
                }
                tbx_TV_LIM.Text = "0x" + TV_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围-127~127", "格式错误"); }
        }

        private void cbx_T1_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_T1_enable.Checked == true)
            {
                ERR_EN4_3 |= (1 << 4);
                tbx_T1_min.Enabled = true;
                tbx_T1_max.Enabled = true;
                tbx_T1_min_value.Visible = true;
                tbx_T1_max_value.Visible = true;
            }
            else
            {
                ERR_EN4_3 &= ~(1 << 4);
                tbx_T1_min.Enabled = false;
                tbx_T1_max.Enabled = false;
                tbx_T1_min_value.Visible = false;
                tbx_T1_max_value.Visible = false;
            }
        }

        private void cbx_T2_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_T2_enable.Checked == true)
            {
                ERR_EN4_3 |= (1 << 5);
                tbx_T2_min.Enabled = true;
                tbx_T2_max.Enabled = true;
                tbx_T2_min_value.Visible = true;
                tbx_T2_max_value.Visible = true;
            }
            else
            {
                ERR_EN4_3 &= ~(1 << 5);
                tbx_T2_min.Enabled = false;
                tbx_T2_max.Enabled = false;
                tbx_T2_min_value.Visible = false;
                tbx_T2_max_value.Visible = false;
            }
        }

        private void tbx_T1_min_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_min.Text == "") tbx_T1_min.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_T1_min.Text);
                if (a > 255) { a = 255; tbx_T1_min.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a, temp1 = a;
                temp /= 256;
                temp *= 100;
                if (rbn_T1_gain1.Checked) temp1 /= 192;
                else if (rbn_T1_gain2.Checked) temp1 /= 64;
                else if (rbn_T1_gain4.Checked) temp1 /= 512;
                tbx_T1_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
                RT1_LIM &= 0xFF00;
                RT1_LIM |= a;
                tbx_RT1_LIM.Text = "0x" + RT1_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void tbx_T1_max_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_max.Text == "") tbx_T1_max.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_T1_max.Text);
                if (a > 255) { a = 255; tbx_T1_max.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a, temp1 = a;
                temp /= 256;
                temp *= 100;
                if (rbn_T1_gain1.Checked) temp1 /= 192;
                else if (rbn_T1_gain2.Checked) temp1 /= 64;
                else if (rbn_T1_gain4.Checked) temp1 /= 512;
                tbx_T1_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
                RT1_LIM &= 0x00FF;
                RT1_LIM |= (a << 8);
                tbx_RT1_LIM.Text = "0x" + RT1_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void tbx_T2_min_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_min.Text == "") tbx_T2_min.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_T2_min.Text);
                if (a > 255) { a = 255; tbx_T2_min.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a, temp1 = a;
                temp /= 256;
                temp *= 100;
                if (rbn_T2_gain1.Checked) temp1 /= 192;
                else if (rbn_T2_gain2.Checked) temp1 /= 64;
                else if (rbn_T2_gain3.Checked) temp1 /= 64;
                else if (rbn_T2_gain4.Checked) temp1 /= 512;
                tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
                RT2_LIM &= 0xFF00;
                RT2_LIM |= a;
                tbx_RT2_LIM.Text = "0x" + RT2_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void tbx_T2_max_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_max.Text == "") tbx_T2_max.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_T2_max.Text);
                if (a > 255) { a = 255; tbx_T2_max.Text = "255"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                float temp = a, temp1 = a;
                temp /= 256;
                temp *= 100;
                if (rbn_T2_gain1.Checked) temp1 /= 192;
                else if (rbn_T2_gain2.Checked) temp1 /= 64;
                else if (rbn_T2_gain3.Checked) temp1 /= 64;
                else if (rbn_T2_gain4.Checked) temp1 /= 512;
                tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
                RT2_LIM &= 0x00FF;
                RT2_LIM |= (a << 8);
                tbx_RT2_LIM.Text = "0x" + RT2_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }
        }

        private void rbn_T1_in_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFFF3;
            rbn_T1_gain1.Enabled = true;
            rbn_T1_gain1.Checked = true;
            rbn_T1_gain2.Enabled = false;
            rbn_T1_gain4.Enabled = false;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
        }

        private void rbn_T1_TSEN1_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFFF3;
            T1_MODE |= 0x0004;
            rbn_T1_gain1.Enabled = true;
            rbn_T1_gain2.Enabled = true;
            rbn_T1_gain4.Enabled = true;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
        }

        private void rbn_T1_TSEN2_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFFF3;
            T1_MODE |= 0x0008;
            rbn_T1_gain1.Enabled = true;
            rbn_T1_gain2.Enabled = true;
            rbn_T1_gain4.Enabled = true;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
        }

        private void rbn_T1_bridge_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFFF3;
            T1_MODE |= 0x000C;
            rbn_T1_gain1.Enabled = false;
            rbn_T1_gain2.Enabled = true;
            rbn_T1_gain4.Enabled = true;
            rbn_T1_gain4.Checked = true;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
        }

        private void rbn_T2_in_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xFFF3;

            rbn_T2_gain1.Enabled = true;
            rbn_T2_gain1.Checked = true;
            rbn_T2_gain2.Enabled = false;
            rbn_T2_gain3.Enabled = false;
            rbn_T2_gain4.Enabled = false;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
        }

        private void rbn_T2_TSEN1_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xFFF3;
            T2_MODE |= 0x0004;
            rbn_T2_gain1.Enabled = true;
            rbn_T2_gain2.Enabled = true;
            rbn_T2_gain3.Enabled = true;
            rbn_T2_gain4.Enabled = true;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
        }

        private void rbn_T2_TSEN2_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xFFF3;
            T2_MODE |= 0x0008;
            rbn_T2_gain1.Enabled = true;
            rbn_T2_gain2.Enabled = true;
            rbn_T2_gain3.Enabled = true;
            rbn_T2_gain4.Enabled = true;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
        }

        private void rbn_T2_bridge_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xFFF3;
            T2_MODE |= 0x000C;
            rbn_T2_gain1.Enabled = false;
            rbn_T2_gain2.Enabled = true;
            rbn_T2_gain3.Enabled = false;
            rbn_T2_gain4.Enabled = true;
            rbn_T2_gain4.Checked = true;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
        }

        private void ckb_idio_off_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_idio_off.Checked)
            {
                T1_MODE &= 0xFF7F;
                T1_MODE |= 0x0080;
            }
            else T1_MODE &= 0xFF7F;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
        }

        private void rbn_T1_gain1_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFC;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
            tbx_TSP_L.Text = "97";
            tbx_TSP_H.Text = "111";
            int a = Convert.ToInt32(tbx_T1_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 192;
            tbx_T1_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T1_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 192;
            tbx_T1_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T1_gain2_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFC;
            T1_MODE |= 0x02;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
            tbx_TSP_L.Text = "49";
            tbx_TSP_H.Text = "63";
            int a = Convert.ToInt32(tbx_T1_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T1_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T1_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T1_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T1_gain4_Click(object sender, EventArgs e)
        {
            T1_MODE &= 0xFC;
            T1_MODE |= 0x03;
            tbx_T1_MODE.Text = "0x" + T1_MODE.ToString("X4");
            tbx_TSP_L.Text = "121";
            tbx_TSP_H.Text = "135";
            int a = Convert.ToInt32(tbx_T1_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 512;
            tbx_T1_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T1_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 512;
            tbx_T1_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T2_gain1_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xEC;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
            int a = Convert.ToInt32(tbx_T2_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 192;
            tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T2_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 192;
            tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T2_gain2_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xEC;
            T2_MODE |= 0x02;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
            int a = Convert.ToInt32(tbx_T2_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T2_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T2_gain3_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xEC;
            T2_MODE |= 0x12;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
            int a = Convert.ToInt32(tbx_T2_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T2_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 64;
            tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void rbn_T2_gain4_Click(object sender, EventArgs e)
        {
            T2_MODE &= 0xEC;
            T2_MODE |= 0x03;
            tbx_T2_MODE.Text = "0x" + T2_MODE.ToString("X4");
            int a = Convert.ToInt32(tbx_T2_min.Text);
            float temp = a, temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 512;
            tbx_T2_min_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
            a = Convert.ToInt32(tbx_T2_max.Text);
            temp = a; temp1 = a;
            temp /= 256;
            temp *= 100;
            temp1 /= 512;
            tbx_T2_max_value.Text = "= " + temp1.ToString("f3") + " V / " + temp.ToString("f1") + " %";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text == "退出")
            {
                Close();
                return;
            }
            Form1.NVM_code[0x3F] = ERR_EN2_1.ToString("X4");
            Form1.NVM_code[0x40] = ERR_EN4_3.ToString("X4");
            Form1.NVM_code[0x68] = T1_MODE.ToString("X4");
            Form1.NVM_code[0x6E] = T2_MODE.ToString("X4");
            Form1.NVM_code[0x45] = TV_LIM.ToString("X4");
            Form1.NVM_code[0x4F] = TSP_LIM.ToString("X4");
            Form1.NVM_code[0x42] = RT1_LIM.ToString("X4");
            Form1.NVM_code[0x43] = RT2_LIM.ToString("X4");
            Form1.CRC2();
            this.Close();
        }
    }
}
