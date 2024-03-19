using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class SentConf : Form
    {
        public SentConf()
        {
            InitializeComponent();
        }
        private static int I2C_CTRL = 0x60;
        private static int SENTCONF1 = 0x63;
        private static int SENTCONF2 = 0x00;
        private static int SENTCONF3 = 0x5D;
        private static int SENTCONF4 = 0x01;
        private static int P_DIFF_0 = 0x0366;

        private void SentConf_Load(object sender, EventArgs e)
        {
            if (Form1.Sentconf_Flag == false)
            {
                // 关联 ToolTip 控件和其他控件
                toolTip1.SetToolTip(btn_Save, "修改内容不会保存。");
                toolTip1.SetToolTip(groupBox1, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox2, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox3, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox4, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox5, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox6, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox7, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(groupBox8, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_TICK, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_pp, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(cbx_pp, "当前账户修改无效，请使用管理员登录。");
                toolTip1.SetToolTip(tbx_p1_p2, "当前账户修改无效，请使用管理员登录。");
                btn_Save.Text = "退出";
            }
            rbx_I2C_CTRL.Text = "0x" + Form1.NVM_code[0x3A].Substring(2, 2);
            tbx_SENTCONF1.Text = "0x" + Form1.NVM_code[0x3D].Substring(2, 2);
            tbx_SENTCONF2.Text = "0x" + Form1.NVM_code[0x3D].Substring(0, 2);
            tbx_SENTCONF3.Text = "0x" + Form1.NVM_code[0x3E].Substring(2, 2);
            tbx_SENTCONF4.Text = "0x" + Form1.NVM_code[0x3E].Substring(0, 2);
            tbx_P_DIFF_0.Text = "0x" + Form1.NVM_code[0x41];
            I2C_CTRL = Convert.ToInt32(rbx_I2C_CTRL.Text, 16);
            SENTCONF1 = Convert.ToInt32(tbx_SENTCONF1.Text, 16);
            SENTCONF2 = Convert.ToInt32(tbx_SENTCONF2.Text, 16);
            SENTCONF3 = Convert.ToInt32(tbx_SENTCONF3.Text, 16);
            SENTCONF4 = Convert.ToInt32(tbx_SENTCONF4.Text, 16);
            P_DIFF_0 = Convert.ToInt32(tbx_P_DIFF_0.Text, 16);
            #region  **将读出的配置分配到各个选项**
            //获取配置数中某一位的值
            //(num >> (bit - 1)) & 1;
            int a;

            //I2C_CTRL
            a = (I2C_CTRL >> 6) & 1;
            if (a == 0) ckb_SCL.Checked = false; else ckb_SCL.Checked = true;
            a = (I2C_CTRL >> 5) & 1;
            if (a == 0) ckb_SDA.Checked = false; else ckb_SDA.Checked = true;
            a = I2C_CTRL & 3;
            switch (a)
            {
                case 0:
                    rbn_I2C_disable.Checked = true;
                    ckb_SCL.Enabled = true;
                    ckb_SDA.Enabled = true;
                    rbt_I2C1.Enabled = false;
                    rbt_I2C2.Enabled = false;
                    rbt_I2C3.Enabled = false;
                    rbt_I2C4.Enabled = false;
                    break;
                case 2:
                    rbn_I2C_FC1.Checked = true;
                    ckb_SCL.Enabled = false;
                    ckb_SDA.Enabled = false;
                    rbt_I2C1.Enabled = true;
                    rbt_I2C2.Enabled = true;
                    rbt_I2C3.Enabled = true;
                    rbt_I2C4.Enabled = true;
                    break;
                case 3:
                    rbn_I2C_FC12.Checked = true;
                    ckb_SCL.Enabled = false;
                    ckb_SDA.Enabled = false;
                    rbt_I2C1.Enabled = true;
                    rbt_I2C2.Enabled = true;
                    rbt_I2C3.Enabled = true;
                    rbt_I2C4.Enabled = true;
                    break;
                default: break;
            }

            //P_DIFF_0
            tbx_p1_p2.Text = P_DIFF_0.ToString("D");

            //SENTCONF4
            a = SENTCONF4 & 7;//设置快通道1信号
            switch (a)
            {
                case 1:
                    rbn_FC1_P1.Checked = true;
                    tbx_p1_p2.Visible = false;
                    lbl_p1_p2.Visible = false;
                    tbx_FC1.Text = "p1";
                    break;
                case 2:
                    rbn_FC1_P2.Checked = true;
                    tbx_p1_p2.Visible = false;
                    lbl_p1_p2.Visible = false;
                    tbx_FC1.Text = "p2";
                    break;
                case 3:
                    rbn_p1_p2.Checked = true;
                    tbx_p1_p2.Visible = true;
                    lbl_p1_p2.Visible = true;
                    tbx_FC1.Text = "p1-p2";
                    break;
                case 4:
                    rbn_FC1_4095.Checked = true;
                    tbx_p1_p2.Visible = false;
                    lbl_p1_p2.Visible = false;
                    tbx_FC1.Text = "4095";
                    break;
                default:
                    break;
            }
            a = (SENTCONF4 >> 3) & 7;//设置快通道2信号
            switch (a)
            {
                case 0:
                    rbn_always0.Checked = true;
                    tbx_FC2.Text = "0";
                    break;
                case 1:
                    rbn_T1.Checked = true;
                    tbx_FC2.Text = "T1";
                    break;
                case 2:
                    rbn_T2.Checked = true;
                    tbx_FC2.Text = "T2";
                    break;
                case 3:
                    rbn_FC2_P2.Checked = true;
                    tbx_FC2.Text = "p2";
                    break;
                case 7:
                    rbn_FC2_4095.Checked = true;
                    tbx_FC2.Text = "4095";
                    break;
                default: break;
            }
            //SENTCONF3
            tbx_pp.Text = SENTCONF3.ToString("D");
            int temp4 = SENTCONF1 & 0x1F;
            tbx_totalus.Text = (3 * SENTCONF3 + 3).ToString() + "节拍 / " + ((3 * SENTCONF3 + 3) * temp4).ToString() + "us";
            //SENTCONF2
            a = (SENTCONF2 >> 7) & 1;
            if (a == 0) rbn_slow_enhanced.Checked = true; else rbn_slow_short.Checked = true;
            a = (SENTCONF2 >> 6) & 1;
            if (a == 0) rbn_slow_8bit.Checked = true; else rbn_slow_4bit.Checked = true;
            a = (SENTCONF2 >> 5) & 1;
            if (a == 0)
            {
                cbx_pp.Checked = true;
                tbx_pp.ReadOnly = false;
            }
            else
            {
                cbx_pp.Checked = false;
                tbx_pp.ReadOnly = true;
            }
            //SENTCONF1
            a = SENTCONF1 & 0x1F;
            tbx_TICK.Text = a.ToString("D");
            a = SENTCONF1 >> 5;
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            switch (a)
            {
                case 0:
                    rbn_SENT_disable4.Checked = true;
                    tbx_TICK.ReadOnly = true;
                    cbx_pp.Enabled = false;
                    tbx_pp.ReadOnly = true;
                    tbx_FC1.Visible = false;
                    tbx_FC2.Visible = false;
                    label23.Visible = false;
                    pictureBox1.Visible = false;
                    tbx_totalus.Visible = false;
                    label_pplenth.Text = "范围：0~255";
                    label1.Text = "I2C配置";
                    break;
                case 1:
                    rbn_P_X.Checked = true;
                    if (pp_lenth < 78) pbx_error.Visible = true;
                    else pbx_error.Visible = false;
                    label_pplenth.Text = "范围：78~255";
                    label23.Text = "MSN,MidN,LSN";
                    if (((SENTCONF2 >> 5) & 1) == 0)
                        pictureBox1.Image = Properties.Resources.px_pp;
                    else pictureBox1.Image = Properties.Resources.px;
                    break;
                case 2:
                    rbn_P_S.Checked = true;
                    if (pp_lenth < 93) pbx_error.Visible = true;
                    else pbx_error.Visible = false;
                    label_pplenth.Text = "范围：93~255";
                    label23.Text = "counter MSN,LSN;inv(MSN-FC1)";
                    tbx_FC2.Text = "固定值";
                    rbn_always0.Enabled = false;
                    rbn_T1.Enabled = false;
                    rbn_T2.Enabled = false;
                    rbn_FC2_P2.Enabled = false;
                    rbn_FC2_4095.Enabled = false;
                    if (((SENTCONF2 >> 5) & 1) == 0)
                        pictureBox1.Image = Properties.Resources.ps_pp;
                    else pictureBox1.Image = Properties.Resources.ps;
                    break;
                case 3:
                    rbn_P_XH1.Checked = true;
                    if (pp_lenth < 93) pbx_error.Visible = true;
                    else pbx_error.Visible = false;
                    label_pplenth.Text = "范围：93~255";
                    label23.Text = "LSN,MidN,MSN";
                    if (((SENTCONF2 >> 5) & 1) == 0)
                        pictureBox1.Image = Properties.Resources.px_pp;
                    else pictureBox1.Image = Properties.Resources.px;
                    break;
                case 4:
                    rbn_P_H2.Checked = true;
                    if (pp_lenth < 66) pbx_error.Visible = true;
                    else pbx_error.Visible = false;
                    label_pplenth.Text = "范围：66~255";
                    label23.Visible = false;
                    tbx_FC2.Visible = false;
                    pictureBox1.Image = Properties.Resources.p_h2;
                    break;
                case 5:
                    rbn_P_H3.Checked = true;
                    if (pp_lenth < 66) pbx_error.Visible = true;
                    else pbx_error.Visible = false;
                    label_pplenth.Text = "范围：65~255";
                    label23.Visible = false;
                    tbx_FC2.Visible = false;
                    pictureBox1.Image = Properties.Resources.p_h3;
                    break;
                default: break;

            }
            #endregion
        }

        private void rbn_SENT_disable4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_SENT_disable4.Checked == true)
            {
                tbx_TICK.ReadOnly = true;
                cbx_pp.Enabled = false;
                tbx_pp.ReadOnly = true;
                tbx_FC1.Visible = false;
                tbx_FC2.Visible = false;
                label23.Visible = false;
                pictureBox1.Visible = false;
                tbx_totalus.Visible = false;
                SENTCONF1 &= 0x1F;
                label_pplenth.Text = "范围：0~255";
                label1.Text = "I2C配置";
                tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
            }
            else
            {
                tbx_TICK.ReadOnly = false;
                cbx_pp.Enabled = true;
                tbx_pp.ReadOnly = false;
                tbx_FC1.Visible = true;
                tbx_FC2.Visible = true;
                label23.Visible = true;
                pictureBox1.Visible = true;
                tbx_totalus.Visible = true;
                label1.Text = "SENT配置";
            }

        }

        private void rbn_P_X_Click(object sender, EventArgs e)
        {
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            if (pp_lenth < 78) pbx_error.Visible = true;
            else pbx_error.Visible = false;
            label_pplenth.Text = "范围：78~255";
            label23.Text = "MSN,MidN,LSN";
            if (rbn_always0.Checked == true) tbx_FC2.Text = "0";
            else if (rbn_T1.Checked == true) tbx_FC2.Text = "T1";
            else if (rbn_T2.Checked == true) tbx_FC2.Text = "T2";
            else if (rbn_FC2_P2.Checked == true) tbx_FC2.Text = "p2";
            else tbx_FC2.Text = "4095";
            label23.Visible = true;
            tbx_FC2.Visible = true;
            if (cbx_pp.Checked == true)
                pictureBox1.Image = Properties.Resources.px_pp;
            else pictureBox1.Image = Properties.Resources.px;
            rbn_always0.Enabled = true;
            rbn_T1.Enabled = true;
            rbn_T2.Enabled = true;
            rbn_FC2_P2.Enabled = true;
            rbn_FC2_4095.Enabled = true;
            SENTCONF1 &= 0x1F;
            SENTCONF1 |= 0x20;
            tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
        }

        private void rbn_P_S_Click(object sender, EventArgs e)
        {
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            if (pp_lenth < 93) pbx_error.Visible = true;
            else pbx_error.Visible = false;
            label_pplenth.Text = "范围：93~255";
            label23.Text = "counter MSN,LSN;inv(MSN-FC1)";
            tbx_FC2.Text = "固定值";
            label23.Visible = true;
            tbx_FC2.Visible = true;
            if (cbx_pp.Checked == true)
                pictureBox1.Image = Properties.Resources.ps_pp;
            else pictureBox1.Image = Properties.Resources.ps;
            rbn_always0.Enabled = false;
            rbn_T1.Enabled = false;
            rbn_T2.Enabled = false;
            rbn_FC2_P2.Enabled = false;
            rbn_FC2_4095.Enabled = false;
            SENTCONF1 &= 0x1F;
            SENTCONF1 |= 0x40;
            tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
        }

        private void rbn_P_XH1_Click(object sender, EventArgs e)
        {
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            if (pp_lenth < 93) pbx_error.Visible = true;
            else pbx_error.Visible = false;
            label_pplenth.Text = "范围：93~255";
            label23.Text = "LSN,MidN,MSN";
            if (rbn_always0.Checked == true) tbx_FC2.Text = "0";
            else if (rbn_T1.Checked == true) tbx_FC2.Text = "T1";
            else if (rbn_T2.Checked == true) tbx_FC2.Text = "T2";
            else if (rbn_FC2_P2.Checked == true) tbx_FC2.Text = "p2";
            else tbx_FC2.Text = "4095";
            label23.Visible = true;
            tbx_FC2.Visible = true;
            if (cbx_pp.Checked == true)
                pictureBox1.Image = Properties.Resources.px_pp;
            else pictureBox1.Image = Properties.Resources.px;
            rbn_always0.Enabled = true;
            rbn_T1.Enabled = true;
            rbn_T2.Enabled = true;
            rbn_FC2_P2.Enabled = true;
            rbn_FC2_4095.Enabled = true;
            SENTCONF1 &= 0x1F;
            SENTCONF1 |= 0x60;
            tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
        }

        private void rbn_P_H2_Click(object sender, EventArgs e)
        {
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            if (pp_lenth < 66) pbx_error.Visible = true;
            else pbx_error.Visible = false;
            label_pplenth.Text = "范围：66~255";
            label23.Visible = false;
            tbx_FC2.Visible = false;
            pictureBox1.Image = Properties.Resources.p_h2;
            rbn_always0.Enabled = false;
            rbn_T1.Enabled = false;
            rbn_T2.Enabled = false;
            rbn_FC2_P2.Enabled = false;
            rbn_FC2_4095.Enabled = false;
            SENTCONF1 &= 0x1F;
            SENTCONF1 |= 0x80;
            tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
        }

        private void rbn_P_H3_Click(object sender, EventArgs e)
        {
            int pp_lenth = Convert.ToInt32(tbx_pp.Text);
            if (pp_lenth < 66) pbx_error.Visible = true;
            else pbx_error.Visible = false;
            label_pplenth.Text = "范围：65~255";
            label23.Visible = false;
            tbx_FC2.Visible = false;
            pictureBox1.Image = Properties.Resources.p_h3;
            rbn_always0.Enabled = false;
            rbn_T1.Enabled = false;
            rbn_T2.Enabled = false;
            rbn_FC2_P2.Enabled = false;
            rbn_FC2_4095.Enabled = false;
            SENTCONF1 &= 0x1F;
            SENTCONF1 |= 0xA0;
            tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
        }

        private void tbx_TICK_TextChanged(object sender, EventArgs e)
        {
            if (tbx_TICK.Text == "") tbx_TICK.Text = "0";
            try
            {
                int temp1 = Convert.ToInt32(tbx_TICK.Text);
                if (temp1 > 31) { temp1 = 31; tbx_TICK.Text = "31"; }
                else if (temp1 == 0) { temp1 = 3; tbx_TICK.Text = "3"; }
                else if (temp1 < 0) { MessageBox.Show("请输入正确数字,范围0~31", "格式错误"); return; }
                SENTCONF1 &= 0xE0;
                SENTCONF1 |= temp1;
                tbx_SENTCONF1.Text = "0x" + SENTCONF1.ToString("X2");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~31", "格式错误"); }
        }

        private void rbn_slow_enhanced_Click(object sender, EventArgs e)
        {
            SENTCONF2 &= 0x7F;
            tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
        }

        private void rbn_slow_short_Click(object sender, EventArgs e)
        {
            SENTCONF2 &= 0x7F;
            SENTCONF2 |= 0x80;
            tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
        }

        private void rbn_slow_8bit_Click(object sender, EventArgs e)
        {
            SENTCONF2 &= 0xBF;
            tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
        }

        private void rbn_slow_4bit_Click(object sender, EventArgs e)
        {
            SENTCONF2 &= 0xBF;
            SENTCONF2 |= 0x40;
            tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
        }

        private void cbx_pp_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_pp.Checked == true)
            {
                tbx_pp.ReadOnly = false;
                SENTCONF2 &= 0xDF;
                tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
                int temp2 = Convert.ToInt32(tbx_pp.Text);
                tbx_SENTCONF3.Text = "0x" + temp2.ToString("X2");
                if ((rbn_P_X.Checked == true) || (rbn_P_XH1.Checked == true)) pictureBox1.Image = Properties.Resources.px_pp;
                if (rbn_P_S.Checked == true) pictureBox1.Image = Properties.Resources.ps_pp;

            }
            else
            {
                tbx_pp.ReadOnly = true;
                SENTCONF2 &= 0xDF;
                SENTCONF2 |= 0x20;
                tbx_SENTCONF2.Text = "0x" + SENTCONF2.ToString("X2");
                tbx_SENTCONF3.Text = "0x00";
                if ((rbn_P_X.Checked == true) || (rbn_P_XH1.Checked == true)) pictureBox1.Image = Properties.Resources.px;
                if (rbn_P_S.Checked == true) pictureBox1.Image = Properties.Resources.ps;

            }
        }

        private void tbx_pp_TextChanged(object sender, EventArgs e)
        {
            if (tbx_pp.Text == "") tbx_pp.Text = "0";
            try
            {
                int temp3 = Convert.ToInt32(tbx_pp.Text);
                if (temp3 > 255) { temp3 = 255; tbx_pp.Text = "255"; }
                else if (temp3 < 0) { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); return; }
                else if ((temp3 < 78) && (rbn_P_X.Checked == true)) pbx_error.Visible = true;
                else if ((temp3 < 93) && (rbn_P_S.Checked == true)) pbx_error.Visible = true;
                else if ((temp3 < 93) && (rbn_P_XH1.Checked == true)) pbx_error.Visible = true;
                else if ((temp3 < 66) && (rbn_P_H2.Checked == true)) pbx_error.Visible = true;
                else if ((temp3 < 65) && (rbn_P_H3.Checked == true)) pbx_error.Visible = true;
                else pbx_error.Visible = false;
                int temp4 = SENTCONF1 & 0x1F;
                tbx_totalus.Text = (3 * temp3 + 3).ToString() + "节拍 / " + ((3 * temp3 + 3) * temp4).ToString() + "us";
                SENTCONF3 = temp3;
                tbx_SENTCONF3.Text = "0x" + SENTCONF3.ToString("X2");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~255", "格式错误"); }

        }

        private void rbn_FC1_P1_Click(object sender, EventArgs e)
        {
            tbx_p1_p2.Visible = false;
            lbl_p1_p2.Visible = false;
            tbx_FC1.Text = "p1";
            SENTCONF4 &= 0xF8;
            SENTCONF4 |= 0x01;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_FC1_P2_Click(object sender, EventArgs e)
        {
            tbx_p1_p2.Visible = false;
            lbl_p1_p2.Visible = false;
            tbx_FC1.Text = "p2";
            SENTCONF4 &= 0xF8;
            SENTCONF4 |= 0x02;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_p1_p2_Click(object sender, EventArgs e)
        {
            tbx_p1_p2.Visible = true;
            lbl_p1_p2.Visible = true;
            tbx_FC1.Text = "p1-p2";
            SENTCONF4 &= 0xF8;
            SENTCONF4 |= 0x03;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
            P_DIFF_0 = Convert.ToInt32(tbx_p1_p2.Text);
            tbx_P_DIFF_0.Text = "0x" + P_DIFF_0.ToString("X4");
        }

        private void rbn_FC1_4095_Click(object sender, EventArgs e)
        {
            tbx_p1_p2.Visible = false;
            lbl_p1_p2.Visible = false;
            tbx_FC1.Text = "4095";
            SENTCONF4 &= 0xF8;
            SENTCONF4 |= 0x07;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void tbx_p1_p2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_p1_p2.Text == "") tbx_p1_p2.Text = "0";
            try
            {
                P_DIFF_0 = Convert.ToInt32(tbx_p1_p2.Text);
                if (P_DIFF_0 > 4095) { P_DIFF_0 = 4095; tbx_p1_p2.Text = "4095"; }
                else if (P_DIFF_0 < 0) { MessageBox.Show("请输入正确数字,范围0~4095", "格式错误"); return; }
                tbx_P_DIFF_0.Text = "0x" + P_DIFF_0.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~4095", "格式错误"); }
        }

        private void rbn_always0_Click(object sender, EventArgs e)
        {
            tbx_FC2.Text = "0";
            SENTCONF4 &= 0x07;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_T1_Click(object sender, EventArgs e)
        {
            tbx_FC2.Text = "T1";
            SENTCONF4 &= 0x07;
            SENTCONF4 |= 0x08;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_T2_Click(object sender, EventArgs e)
        {
            tbx_FC2.Text = "T2";
            SENTCONF4 &= 0x07;
            SENTCONF4 |= 0x10;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_FC2_P2_Click(object sender, EventArgs e)
        {
            tbx_FC2.Text = "p2";
            SENTCONF4 &= 0x07;
            SENTCONF4 |= 0x18;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_FC2_4095_Click(object sender, EventArgs e)
        {
            tbx_FC2.Text = "4095";
            SENTCONF4 &= 0x07;
            SENTCONF4 |= 0x38;
            tbx_SENTCONF4.Text = "0x" + SENTCONF4.ToString("X2");
        }

        private void rbn_I2C_disable_Click(object sender, EventArgs e)
        {
            ckb_SCL.Enabled = true;
            ckb_SDA.Enabled = true;
            rbt_I2C1.Enabled = false;
            rbt_I2C2.Enabled = false;
            rbt_I2C3.Enabled = false;
            rbt_I2C4.Enabled = false;
            I2C_CTRL &= 0x7C;
            rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
        }

        private void rbn_I2C_FC1_Click(object sender, EventArgs e)
        {
            ckb_SCL.Enabled = false;
            ckb_SDA.Enabled = false;
            rbt_I2C1.Enabled = true;
            rbt_I2C2.Enabled = true;
            rbt_I2C3.Enabled = true;
            rbt_I2C4.Enabled = true;
            I2C_CTRL &= 0x7C;
            I2C_CTRL |= 0x02;
            rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");

        }

        private void rbn_I2C_FC12_Click(object sender, EventArgs e)
        {
            ckb_SCL.Enabled = false;
            ckb_SDA.Enabled = false;
            rbt_I2C1.Enabled = true;
            rbt_I2C2.Enabled = true;
            rbt_I2C3.Enabled = true;
            rbt_I2C4.Enabled = true;
            I2C_CTRL &= 0x7C;
            I2C_CTRL |= 0x03;
            rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
        }

        private void ckb_SCL_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_SCL.Checked == true)
            {
                I2C_CTRL &= 0x3F;
                I2C_CTRL |= 0x40;
                rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
            }
            else
            {
                I2C_CTRL &= 0x3F;
                rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
            }
        }

        private void ckb_SDA_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_SDA.Checked == true)
            {
                I2C_CTRL &= 0x5F;
                I2C_CTRL |= 0x20;
                rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
            }
            else
            {
                I2C_CTRL &= 0x5F;
                rbx_I2C_CTRL.Text = "0x" + I2C_CTRL.ToString("X2");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text == "退出")
            {
                Close();
                return;
            }
            if (pbx_error.Visible == true)
            {
                MessageBox.Show("暂停脉冲(PP)设定值低于阈值，请重新输入", "保存失败");
                return;
            }
            Form1.NVM_code[0x3A] = I2C_CTRL.ToString("X4");
            Form1.NVM_code[0x3D] = SENTCONF2.ToString("X2") + SENTCONF1.ToString("X2");
            Form1.NVM_code[0x3E] = SENTCONF4.ToString("X2") + SENTCONF3.ToString("X2");
            Form1.NVM_code[0x41] = P_DIFF_0.ToString("X4");
            Form1.CRC2();
            this.Close();
        }
    }
}
