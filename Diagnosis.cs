using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class Diagnosis : Form
    {
        public Diagnosis()
        {
            InitializeComponent();
        }
        private static int ERR_EN2_1;
        private static int ERR_EN4_3;
        private void Diagnosis_Load(object sender, EventArgs e)
        {
            tbx_ERR_EN2_1.Text = "0x" + Form1.NVM_code[0x3F];
            tbx_ERR_EN4_3.Text = "0x" + Form1.NVM_code[0x40];
            ERR_EN2_1 = Convert.ToInt32(Form1.NVM_code[0x3F], 16);
            ERR_EN4_3 = Convert.ToInt32(Form1.NVM_code[0x40], 16);
            #region ** 将配置分配到各个选项中**
            int a;
            //ERR_EN1 第一组
            a = (ERR_EN2_1 >> 7) & 1;
            if (a == 1) cbx_PV.Checked = true; else cbx_PV.Checked = false;
            a = (ERR_EN2_1 >> 6) & 1;
            if (a == 1) cbx_TV.Checked = true; else cbx_TV.Checked = false;
            a = (ERR_EN2_1 >> 5) & 1;
            if (a == 1) cbx_V5H.Checked = true; else cbx_V5H.Checked = false;
            a = (ERR_EN2_1 >> 4) & 1;
            if (a == 1) cbx_V5L.Checked = true; else cbx_V5L.Checked = false;
            a = (ERR_EN2_1 >> 3) & 1;
            if (a == 1) cbx_CAP.Checked = true; else cbx_CAP.Checked = false;
            a = (ERR_EN2_1 >> 2) & 1;
            if (a == 1) cbx_TSP.Checked = true; else cbx_TSP.Checked = false;
            a = (ERR_EN2_1 >> 1) & 1;
            if (a == 1) cbx_PSP2.Checked = true; else cbx_PSP2.Checked = false;
            a = ERR_EN2_1 & 1;
            if (a == 1) cbx_PSP1.Checked = true; else cbx_PSP1.Checked = false;

            //ERR_EN2 第二组
            a = (ERR_EN2_1 >> 15) & 1;
            if (a == 1) cbx_S42.Checked = true; else cbx_S42.Checked = false;
            a = (ERR_EN2_1 >> 14) & 1;
            if (a == 1) cbx_S32.Checked = true; else cbx_S32.Checked = false;
            a = (ERR_EN2_1 >> 13) & 1;
            if (a == 1) cbx_S22.Checked = true; else cbx_S22.Checked = false;
            a = (ERR_EN2_1 >> 12) & 1;
            if (a == 1) cbx_S02.Checked = true; else cbx_S02.Checked = false;
            a = (ERR_EN2_1 >> 11) & 1;
            if (a == 1) cbx_S41.Checked = true; else cbx_S41.Checked = false;
            a = (ERR_EN2_1 >> 10) & 1;
            if (a == 1) cbx_S31.Checked = true; else cbx_S31.Checked = false;
            a = (ERR_EN2_1 >> 9) & 1;
            if (a == 1) cbx_S21.Checked = true; else cbx_S21.Checked = false;
            a = (ERR_EN2_1 >> 8) & 1;
            if (a == 1) cbx_S01.Checked = true; else cbx_S01.Checked = false;

            //ERR_EN3 第三组
            a = (ERR_EN4_3 >> 6) & 1;
            if (a == 1) cbx_NOP.Checked = true; else cbx_NOP.Checked = false;
            a = (ERR_EN4_3 >> 5) & 1;
            if (a == 1) cbx_RT2.Checked = true; else cbx_RT2.Checked = false;
            a = (ERR_EN4_3 >> 4) & 1;
            if (a == 1) cbx_RT1.Checked = true; else cbx_RT1.Checked = false;
            a = (ERR_EN4_3 >> 3) & 1;
            if (a == 1) cbx_T2.Checked = true; else cbx_T2.Checked = false;
            a = (ERR_EN4_3 >> 2) & 1;
            if (a == 1) cbx_T1.Checked = true; else cbx_T1.Checked = false;
            a = (ERR_EN4_3 >> 1) & 1;
            if (a == 1) cbx_P2.Checked = true; else cbx_P2.Checked = false;
            a = ERR_EN4_3 & 1;
            if (a == 1) cbx_P1.Checked = true; else cbx_P1.Checked = false;

            //ERR_EN4 第四组
            a = (ERR_EN4_3 >> 15) & 1;
            if (a == 1) cbx_OT.Checked = true; else cbx_OT.Checked = false;
            a = (ERR_EN4_3 >> 14) & 1;
            if (a == 1) cbx_VSM.Checked = true; else cbx_VSM.Checked = false;
            a = (ERR_EN4_3 >> 11) & 1;
            if (a == 1) cbx_S1.Checked = true; else cbx_S1.Checked = false;
            a = (ERR_EN4_3 >> 10) & 1;
            if (a == 1) cbx_TC1.Checked = true; else cbx_TC1.Checked = false;
            a = (ERR_EN4_3 >> 9) & 1;
            if (a == 1) cbx_PC2.Checked = true; else cbx_PC2.Checked = false;
            a = (ERR_EN4_3 >> 8) & 1;
            if (a == 1) cbx_PC1.Checked = true; else cbx_PC1.Checked = false;
            #endregion
        }

        private void cbx_V5H_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_V5H.Checked) ERR_EN2_1 |= (1 << 5); else ERR_EN2_1 &= ~(1 << 5);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_V5L_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_V5L.Checked) ERR_EN2_1 |= (1 << 4); else ERR_EN2_1 &= ~(1 << 4);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_CAP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_CAP.Checked) ERR_EN2_1 |= (1 << 3); else ERR_EN2_1 &= ~(1 << 3);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S42_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S42.Checked) ERR_EN2_1 |= (1 << 15); else ERR_EN2_1 &= ~(1 << 15);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S32_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S32.Checked) ERR_EN2_1 |= (1 << 14); else ERR_EN2_1 &= ~(1 << 14);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S22_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S22.Checked) ERR_EN2_1 |= (1 << 13); else ERR_EN2_1 &= ~(1 << 13);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S02_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S02.Checked) ERR_EN2_1 |= (1 << 12); else ERR_EN2_1 &= ~(1 << 12);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S41_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S41.Checked) ERR_EN2_1 |= (1 << 11); else ERR_EN2_1 &= ~(1 << 11);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S31_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S31.Checked) ERR_EN2_1 |= (1 << 10); else ERR_EN2_1 &= ~(1 << 10);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S21_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S21.Checked) ERR_EN2_1 |= (1 << 9); else ERR_EN2_1 &= ~(1 << 9);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_S01_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S01.Checked) ERR_EN2_1 |= (1 << 8); else ERR_EN2_1 &= ~(1 << 8);
            tbx_ERR_EN2_1.Text = "0x" + ERR_EN2_1.ToString("X4");
        }

        private void cbx_NOP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_NOP.Checked) ERR_EN4_3 |= (1 << 6); else ERR_EN4_3 &= ~(1 << 6);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_T2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_T2.Checked) ERR_EN4_3 |= (1 << 3); else ERR_EN4_3 &= ~(1 << 3);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_T1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_T1.Checked) ERR_EN4_3 |= (1 << 2); else ERR_EN4_3 &= ~(1 << 2);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_P2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_P2.Checked) ERR_EN4_3 |= (1 << 1); else ERR_EN4_3 &= ~(1 << 1);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_P1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_P1.Checked) ERR_EN4_3 |= 1; else ERR_EN4_3 &= ~1;
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_OT_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_OT.Checked) ERR_EN4_3 |= (1 << 15); else ERR_EN4_3 &= ~(1 << 15);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_VSM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_VSM.Checked) ERR_EN4_3 |= (1 << 14); else ERR_EN4_3 &= ~(1 << 14);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_S1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_S1.Checked) ERR_EN4_3 |= (1 << 11); else ERR_EN4_3 &= ~(1 << 11);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_TC1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_TC1.Checked) ERR_EN4_3 |= (1 << 10); else ERR_EN4_3 &= ~(1 << 10);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_PC2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PC2.Checked) ERR_EN4_3 |= (1 << 9); else ERR_EN4_3 &= ~(1 << 9);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void cbx_PC1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PC1.Checked) ERR_EN4_3 |= (1 << 8); else ERR_EN4_3 &= ~(1 << 8);
            tbx_ERR_EN4_3.Text = "0x" + ERR_EN4_3.ToString("X4");
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Form1.NVM_code[0x3F] = ERR_EN2_1.ToString("X4");
            Form1.NVM_code[0x40] = ERR_EN4_3.ToString("X4");
            Form1.CRC2();
            this.Close();
        }
    }
}
