using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class Pressure : Form
    {
        public Pressure()
        {
            InitializeComponent();
        }
        private static int POFFS1_PGAIN1;
        private static int POFFS2_PGAIN2;
        private static int PV_LIM;
        private static int PSP_DAC;
        private static int PSP1_LIM;
        private static int PSP2_LIM;
        private static int ERR_EN2_1;
        private static byte[,] PSP_max = new byte[,]
        {
            {99,149,85,135,71,121,57,107,158,94,144,80,130,66,116,52,102,152,88,138,74,124,60,110,46,97,147,83,133,133,119,55 },
            {88,104,119,135,71,87,103,119,135,71,87,103,118,134,70,86,102,118,134,70,86,101,117,133,69,85,101,117,133,69,85,100 },
            {42,35,28,21,71,64,57,50,43,37,30,23,73,66,59,52,159,152,145,138,131,181,174,167,161,154,147,140,133,183,176,169 },
            {48,64,79,55,71,47,63,79,55,71,47,63,78,54,70,46,142,158,134,150,126,141,157,133,149,125,141,157,133,149,125,140},
            {71,64,85,78,71,64,86,79,72,65,87,80,73,66,87,80,131,124,117,138,131,124,117,139,132,125,118,140,133,126,119,140 },
            {71,64,85,78,71,64,86,79,72,65,87,80,73,66,87,80,131,124,117,138,131,124,117,139,132,125,118,140,133,126,119,140 },
            {88,84,79,75,91,87,83,79,75,91,87,83,78,74,90,86,122,118,114,130,126,121,117,113,129,125,121,117,113,129,125,120 },
            {85,92,85,92,86,93,86,93,86,94,87,94,87,94,87,95,116,109,117,110,117,110,117,110,118,111,118,111,118,112,119,112 },
            {88,94,89,95,91,87,93,89,95,91,97,93,88,94,90,96,112,108,114,110,116,111,107,113,109,115,111,117,113,109,115,110 },
            {203,156,92,92,93,93,93,93,93,94,94,94,94,94,95,95,109,109,109,110,110,110,110,110,111,111,111,111,111,112,112,48 }
        };

        private void Pressure_Load(object sender, EventArgs e)
        {
            tbx_POFFS1_PGAIN1.Text = "0x" + Form1.NVM_code[0x3B];
            tbx_POFFS2_PGAIN2.Text = "0x" + Form1.NVM_code[0x3C];
            tbx_PV_LIM.Text = "0x" + Form1.NVM_code[0x44];
            tbx_PSP_DAC.Text = "0x" + Form1.NVM_code[0x4C];
            tbx_PSP1_LIM.Text = "0x" + Form1.NVM_code[0x3D];
            tbx_PSP2_LIM.Text = "0x" + Form1.NVM_code[0x3E];

            POFFS1_PGAIN1 = Convert.ToInt32(Form1.NVM_code[0x3B], 16);
            POFFS2_PGAIN2 = Convert.ToInt32(Form1.NVM_code[0x3C], 16);
            PV_LIM = Convert.ToInt32(Form1.NVM_code[0x44], 16);
            PSP_DAC = Convert.ToInt32(Form1.NVM_code[0x4C], 16);
            PSP1_LIM = Convert.ToInt32(Form1.NVM_code[0x4D], 16);
            PSP2_LIM = Convert.ToInt32(Form1.NVM_code[0x4E], 16);
            ERR_EN2_1 = Convert.ToInt32(Form1.NVM_code[0x3F], 16);
            #region **将配置分配到各个选项**
            int a;
            //ERR_EN2_1
            a = (ERR_EN2_1 >> 7) & 1;
            if (a == 1) { cbx_PV_EN.Checked = true; tbx_PV.ReadOnly = false; lbl_PV.Visible = true; }
            else { cbx_PV_EN.Checked = false; tbx_PV.ReadOnly = true; lbl_PV.Visible = false; }
            a = (ERR_EN2_1 >> 1) & 1;
            if (a == 0)
            {
                cbx_PSP2_EN.Checked = false;
                tbx_PSP2_DAC.Enabled = false;
                tbx_PSP2_min.Enabled = false;
                tbx_PSP2_max.Enabled = false;
                lbl_PSP2_DAC.Visible = false;
                lbl_PSP2_min.Visible = false;
                lbl_PSP2_max.Visible = false;
                tbx_BIST2.Visible = false;
            }
            else
            {
                cbx_PSP2_EN.Checked = true;
                tbx_PSP2_DAC.Enabled = true;
                tbx_PSP2_min.Enabled = true;
                tbx_PSP2_max.Enabled = true;
                lbl_PSP2_DAC.Visible = true;
                lbl_PSP2_min.Visible = true;
                lbl_PSP2_max.Visible = true;
                tbx_BIST2.Visible = true;
            }
            a = ERR_EN2_1 & 1;
            if (a == 0)
            {
                cbx_PSP1_EN.Checked = false;
                tbx_PSP1_DAC.Enabled = false;
                tbx_PSP1_min.Enabled = false;
                tbx_PSP1_max.Enabled = false;
                lbl_PSP1_DAC.Visible = false;
                lbl_PSP1_min.Visible = false;
                lbl_PSP1_max.Visible = false;
                tbx_BIST1.Visible = false;
            }
            else
            {
                cbx_PSP1_EN.Checked = true;
                tbx_PSP1_DAC.Enabled = true;
                tbx_PSP1_min.Enabled = true;
                tbx_PSP1_max.Enabled = true;
                lbl_PSP1_DAC.Visible = true;
                lbl_PSP1_min.Visible = true;
                lbl_PSP1_max.Visible = true;
                tbx_BIST1.Visible = true;
            }
            //POFFS1_PGAIN1
            a = (POFFS1_PGAIN1 >> 7) & 1;
            if (a == 0) cbx_POL1.Checked = false;
            else cbx_POL1.Checked = true;

            a = (POFFS1_PGAIN1 >> 6) & 1;
            if (a == 0) cbx_PHALF1.Checked = false;
            else cbx_PHALF1.Checked = true;

            a = (POFFS1_PGAIN1 >> 4) & 3;
            cbb_EXLO_R.SelectedIndex = a;
            switch (a)
            {
                case 0: lbl_Ohm.Text = "87 Ohm"; lbl_R_BR.Text = "for 0.90k < R_BR < 4.0k or no T(Iex)"; break;
                case 1: lbl_Ohm.Text = "87 Ohm"; lbl_R_BR.Text = "for 0.90k < R_BR < 4.0k or no T(Iex)"; break;
                case 2: lbl_Ohm.Text = "174 Ohm"; lbl_R_BR.Text = "for 1.90k < R_BR < 8.0k"; break;
                case 3: lbl_Ohm.Text = "300 Ohm"; lbl_R_BR.Text = "for 3.60k < R_BR < 12.0k"; break;
                default: break;
            }
            a = POFFS1_PGAIN1 & 0xF;
            float ADC_min, ADC_max, ADC_standard, ADC_gain = 0;
            switch (a)
            {
                case 0: cbb_p1_gain.SelectedIndex = 9; lbl_p1_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; break;
                case 1: cbb_p1_gain.SelectedIndex = 8; lbl_p1_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; break;
                case 2: cbb_p1_gain.SelectedIndex = 7; lbl_p1_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; break;
                case 3: cbb_p1_gain.SelectedIndex = 6; lbl_p1_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; break;
                case 4: cbb_p1_gain.SelectedIndex = 5; lbl_p1_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; break;
                case 8: cbb_p1_gain.SelectedIndex = 4; lbl_p1_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; break;
                case 9: cbb_p1_gain.SelectedIndex = 3; lbl_p1_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; break;
                case 10: cbb_p1_gain.SelectedIndex = 2; lbl_p1_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; break;
                case 11: cbb_p1_gain.SelectedIndex = 1; lbl_p1_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; break;
                case 12: cbb_p1_gain.SelectedIndex = 0; lbl_p1_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; break;
                default: break;
            }
            a = (POFFS1_PGAIN1 >> 8) & 0x1F;
            if (a < 16) cbb_p1_offset.SelectedIndex = a + 16;
            else cbb_p1_offset.SelectedIndex = a - 16;
            ADC_standard = ADC_gain * ((a + 16) * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p1_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p1_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";
            //cbb_LPFC
            a = (POFFS1_PGAIN1 >> 13) & 7;
            cbb_LPFC.SelectedIndex = a;
            switch (a)
            {
                case 0: lbl_fc.Text = "无滤波器，不使用"; lbl_t.Text = "t_settling = -"; break;
                case 1: lbl_fc.Text = "f_c = 1092 Hz"; lbl_t.Text = "t_settling = <1 ms"; break;
                case 2: lbl_fc.Text = "f_c = 459 Hz"; lbl_t.Text = "t_settling = 1.0 ms"; break;
                case 3: lbl_fc.Text = "f_c = 213 Hz"; lbl_t.Text = "t_settling = 1.9 ms"; break;
                case 4: lbl_fc.Text = "f_c = 104 Hz"; lbl_t.Text = "t_settling = 3.4 ms"; break;
                case 5: lbl_fc.Text = "f_c = 51 Hz"; lbl_t.Text = "t_settling = 6.7 ms"; break;
                case 6: lbl_fc.Text = "f_c = 26 Hz"; lbl_t.Text = "t_settling = 13 ms"; break;
                case 7: lbl_fc.Text = "f_c = 13 Hz"; lbl_t.Text = "t_settling = 27 ms"; break;
                default: break;
            }

            //POFFS2_PGAIN2
            //cbx_POL2
            a = (POFFS2_PGAIN2 >> 7) & 1;
            if (a == 0) cbx_POL2.Checked = false;
            else cbx_POL2.Checked = true;
            //cbx_PHALF2
            a = (POFFS2_PGAIN2 >> 6) & 1;
            if (a == 0) cbx_PHALF2.Checked = false;
            else cbx_PHALF2.Checked = true;
            //cbx_p2_EN
            a = (POFFS2_PGAIN2 >> 4) & 1;
            if (a == 0)
            {
                cbx_p2_EN.Checked = false;
                cbb_p2_gain.Enabled = true;
                cbb_p2_offset.Enabled = true;
                cbx_POL2.Enabled = true;
                cbx_PHALF2.Enabled = true;
                lbl_p2_gain1.Visible = true;
                lbl_p2_offset1.Visible = true;
            }
            else
            {
                cbx_p2_EN.Checked = true;
                cbb_p2_gain.Enabled = false;
                cbb_p2_offset.Enabled = false;
                cbx_POL2.Enabled = false;
                cbx_PHALF2.Enabled = false;
                lbl_p2_gain1.Visible = false;
                lbl_p2_offset1.Visible = false;
            }
            //cbb_p2_gain
            a = POFFS2_PGAIN2 & 0xF;
            switch (a)
            {
                case 0: cbb_p2_gain.SelectedIndex = 9; lbl_p2_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; break;
                case 1: cbb_p2_gain.SelectedIndex = 8; lbl_p2_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; break;
                case 2: cbb_p2_gain.SelectedIndex = 7; lbl_p2_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; break;
                case 3: cbb_p2_gain.SelectedIndex = 6; lbl_p2_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; break;
                case 4: cbb_p2_gain.SelectedIndex = 5; lbl_p2_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; break;
                case 8: cbb_p2_gain.SelectedIndex = 4; lbl_p2_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; break;
                case 9: cbb_p2_gain.SelectedIndex = 3; lbl_p2_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; break;
                case 10: cbb_p2_gain.SelectedIndex = 2; lbl_p2_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; break;
                case 11: cbb_p2_gain.SelectedIndex = 1; lbl_p2_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; break;
                case 12: cbb_p2_gain.SelectedIndex = 0; lbl_p2_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; break;
                default: break;
            }
            //cbb_p2_offset
            a = (POFFS2_PGAIN2 >> 8) & 0x1F;
            if (a < 16) cbb_p2_offset.SelectedIndex = a + 16;
            else cbb_p2_offset.SelectedIndex = a - 16;
            ADC_standard = ADC_gain * ((a + 16) * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p2_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p2_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";


            //PV_LIM
            a = PV_LIM;
            float temp = a;
            temp /= 4096;
            temp *= 100;
            lbl_PV.Text = temp.ToString("f2") + " %FS";
            tbx_PV.Text = a.ToString();

            //PSP_DAC
            a = PSP_DAC & 0xFF;
            if (a > 127) a -= 255;
            tbx_PSP1_DAC.Text = a.ToString() + "（默认）";
            float b = a;
            b += 127;
            b *= 100;
            b /= 254;
            lbl_PSP1_DAC.Text = b.ToString("f1") + " %FS_DAC";
            b = a;
            b *= 312;
            b /= 100;
            tbx_BIST1.Text = b.ToString("f1") + " m/mV";

            a = (PSP_DAC >> 8) & 0xFF;
            if (a > 127) a -= 255;
            tbx_PSP2_DAC.Text = a.ToString() + "（默认）";
            b = a;
            b += 127;
            b *= 100;
            b /= 254;
            lbl_PSP2_DAC.Text = b.ToString("f1") + " %FS_DAC";
            b = a;
            b *= 312;
            b /= 100;
            tbx_BIST2.Text = b.ToString("f1") + " m/mV";

            //tbx_PSP1_LIM
            a = PSP1_LIM & 0xFF;
            tbx_PSP1_min.Text = a.ToString() + "（默认）";
            b = a;
            b /= 256;
            b *= 100;
            lbl_PSP1_min.Text = b.ToString("f1") + " %FS_PADC1";

            a = (PSP1_LIM >> 8) & 0xFF;
            tbx_PSP1_max.Text = a.ToString() + "（默认）";
            b = a;
            b /= 256;
            b *= 100;
            lbl_PSP1_max.Text = b.ToString("f1") + " %FS_PADC1";

            //tbx_PSP2_LIM
            a = PSP2_LIM & 0xFF;
            tbx_PSP2_min.Text = a.ToString() + "（默认）";
            b = a;
            b /= 256;
            b *= 100;
            lbl_PSP2_min.Text = b.ToString("f1") + " %FS_PADC1";

            a = (PSP2_LIM >> 8) & 0xFF;
            tbx_PSP2_max.Text = a.ToString() + "（默认）";
            b = a;
            b /= 256;
            b *= 100;
            lbl_PSP2_max.Text = b.ToString("f1") + " %FS_PADC1";
            #endregion
        }

        private void cbx_PV_EN_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PV_EN.Checked)
            {
                tbx_PV.ReadOnly = false;
                lbl_PV.Visible = true;
                ERR_EN2_1 |= (1 << 7);
            }
            else
            {
                tbx_PV.ReadOnly = true;
                lbl_PV.Visible = false;
                ERR_EN2_1 &= ~(1 << 7);
            }
        }

        private void tbx_PV_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PV.Text == "") tbx_PV.Text = "0";
            try
            {
                int a = Convert.ToInt32(tbx_PV.Text);
                if (a > 4095) { a = 4095; tbx_PV.Text = "4095"; }
                else if (a < 0) { MessageBox.Show("请输入正确数字,范围0~4095", "格式错误"); return; }
                float temp = a;
                temp /= 4096;
                temp *= 100;
                lbl_PV.Text = temp.ToString("f2") + " %FS";
                PV_LIM = a;
                tbx_PV_LIM.Text = "0x" + PV_LIM.ToString("X4");
            }
            catch { MessageBox.Show("请输入正确数字,范围0~4095", "格式错误"); }
        }

        private void cbb_p1_gain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_p1_gain.SelectedIndex == -1) return;
            double DAC, a = 0, b = 0;
            float ADC_min, ADC_max, ADC_standard, ADC_gain = 0;
            switch (cbb_p1_gain.Items[cbb_p1_gain.SelectedIndex])
            {
                case "0": lbl_p1_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; a = 9; b = 1; break;
                case "1": lbl_p1_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; a = 6.43; b = 1.3; break;
                case "2": lbl_p1_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; a = 4.52; b = 1; break;
                case "3": lbl_p1_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; a = 3.218; b = 1.35; break;
                case "4": lbl_p1_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "8": lbl_p1_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "9": lbl_p1_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; a = 1.6; b = 1.5; break;
                case "10": lbl_p1_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; a = 1.12; b = 1.5; break;
                case "11": lbl_p1_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; a = 0.798; b = 0.5; break;
                case "12": lbl_p1_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; a = 0.556; b = 0.5; break;
                default: break;
            }
            ADC_standard = ADC_gain * (cbb_p1_offset.SelectedIndex * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p1_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p1_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";
            //tbx_POFFS1_PGAIN1
            int c = Convert.ToInt32(cbb_p1_gain.Items[cbb_p1_gain.SelectedIndex]);
            POFFS1_PGAIN1 &= 0xFFF0;
            POFFS1_PGAIN1 |= c;
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");

            //tbx_PSP1_min/ax
            if (cbb_p1_offset.SelectedIndex == -1) return;
            byte shuzu = PSP_max[cbb_p1_gain.SelectedIndex, cbb_p1_offset.SelectedIndex];
            tbx_PSP1_min.Text = shuzu.ToString() + "(默认)";
            tbx_PSP1_max.Text = (shuzu + 52).ToString() + "(默认)";
            PSP1_LIM = ((shuzu + 52) << 8) + shuzu; ;
            tbx_PSP1_LIM.Text = "0x" + PSP1_LIM.ToString("X4");
            //tbx_PSP1_DAC
            if (cbb_p1_offset.SelectedIndex < 16)
                DAC = a * (cbb_p1_offset.SelectedIndex - 16) - b;
            else DAC = a * (cbb_p1_offset.SelectedIndex - 16) + b;
            if (DAC < -127) DAC = -127.0;
            else if (DAC > 127) DAC = 127.0;
            int d = DAC.ToString().IndexOf(".");
            if (d <= 0) tbx_PSP1_DAC.Text = DAC.ToString() + "(默认)"; //如果计算结果是整数
            else tbx_PSP1_DAC.Text = DAC.ToString().Substring(0, d) + "(默认)";
        }

        private void cbb_p1_offset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_p1_offset.SelectedIndex == -1) return;
            if (cbb_p1_gain.SelectedIndex == -1) return;
            lbl_p1_offset1.Text = ((cbb_p1_offset.SelectedIndex - 16) * 25).ToString() + " %FS";
            //
            double DAC, a = 0, b = 0;
            float ADC_min, ADC_max, ADC_standard, ADC_gain = 0;
            switch (cbb_p1_gain.Items[cbb_p1_gain.SelectedIndex])
            {
                case "0": lbl_p1_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; a = 9; b = 1; break;
                case "1": lbl_p1_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; a = 6.43; b = 1.3; break;
                case "2": lbl_p1_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; a = 4.52; b = 1; break;
                case "3": lbl_p1_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; a = 3.218; b = 1.35; break;
                case "4": lbl_p1_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "8": lbl_p1_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "9": lbl_p1_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; a = 1.6; b = 1.5; break;
                case "10": lbl_p1_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; a = 1.12; b = 1.5; break;
                case "11": lbl_p1_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; a = 0.798; b = 0.5; break;
                case "12": lbl_p1_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; a = 0.556; b = 0.5; break;
                default: break;
            }
            ADC_standard = ADC_gain * (cbb_p1_offset.SelectedIndex * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p1_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p1_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";
            //tbx_PSP1_DAC
            if (cbb_p1_offset.SelectedIndex < 16)
                DAC = a * (cbb_p1_offset.SelectedIndex - 16) - b;
            else DAC = a * (cbb_p1_offset.SelectedIndex - 16) + b;
            if (DAC < -127) DAC = -127.0;
            else if (DAC > 127) DAC = 127.0;
            int d = DAC.ToString().IndexOf(".");
            if (d <= 0) tbx_PSP1_DAC.Text = DAC.ToString() + "(默认)"; //如果计算结果是整数
            else tbx_PSP1_DAC.Text = DAC.ToString().Substring(0, d) + "(默认)";
            //tbx_POFFS1_PGAIN1
            POFFS1_PGAIN1 &= 0xE0FF;
            if (cbb_p1_offset.SelectedIndex < 16) POFFS1_PGAIN1 |= (cbb_p1_offset.SelectedIndex + 16) << 8;
            else POFFS1_PGAIN1 |= (cbb_p1_offset.SelectedIndex - 16) << 8;
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");
            //tbx_PSP1_min/ax
            //tbx_PSP1_min/ax
            byte shuzu = PSP_max[cbb_p1_gain.SelectedIndex, cbb_p1_offset.SelectedIndex];
            tbx_PSP1_min.Text = shuzu.ToString() + "(默认)";
            tbx_PSP1_max.Text = (shuzu + 52).ToString() + "(默认)";
            PSP1_LIM = ((shuzu + 52) << 8) + shuzu; ;
            tbx_PSP1_LIM.Text = "0x" + PSP1_LIM.ToString("X4");
        }

        private void cbx_POL1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_POL1.Checked) POFFS1_PGAIN1 |= 0x0080;
            else POFFS1_PGAIN1 &= 0xFF7F;
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");
        }

        private void cbx_PHALF1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PHALF1.Checked) POFFS1_PGAIN1 |= 0x0040;
            else POFFS1_PGAIN1 &= 0xFFBF;
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");
        }

        private void cbb_EXLO_R_SelectedIndexChanged(object sender, EventArgs e)
        {
            POFFS1_PGAIN1 &= 0xFFCF;
            switch (cbb_EXLO_R.SelectedIndex)
            {
                case 0: lbl_Ohm.Text = "87 Ohm"; lbl_R_BR.Text = "for 0.90k < R_BR < 4.0k or no T(Iex)"; break;
                case 1: lbl_Ohm.Text = "87 Ohm"; lbl_R_BR.Text = "for 0.90k < R_BR < 4.0k or no T(Iex)"; POFFS1_PGAIN1 |= 0x10; break;
                case 2: lbl_Ohm.Text = "174 Ohm"; lbl_R_BR.Text = "for 1.90k < R_BR < 8.0k"; POFFS1_PGAIN1 |= 0x20; break;
                case 3: lbl_Ohm.Text = "300 Ohm"; lbl_R_BR.Text = "for 3.60k < R_BR < 12.0k"; POFFS1_PGAIN1 |= 0x30; break;
                default: break;
            }
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");
        }

        private void tbx_PSP1_DAC_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PSP1_DAC.Text == "") tbx_PSP1_DAC.Text = "0";
            if (tbx_PSP1_DAC.Text == "-") return;
            string sb = "";
            foreach (char c in tbx_PSP1_DAC.Text)
            {
                if (c == '-')
                    sb += c;
                else if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    sb += c;
                else break;
            }
            try
            {
                int a = Convert.ToInt32(sb);
                if (a < -127) { a = -127; tbx_PSP1_DAC.Text = "-127"; }
                else if (a > 127) { a = 127; tbx_PSP1_DAC.Text = "127"; }
                float b = a;
                b += 127;
                b *= 100;
                b /= 254;
                lbl_PSP1_DAC.Text = b.ToString("f1") + " %FS_DAC";
                b = a;
                b *= 312;
                b /= 100;
                tbx_BIST1.Text = b.ToString("f1") + " m/mV";
                //PSP_DAC(p2/p1)
                PSP_DAC &= 0xFF00;
                if (a < 0) a += 256;
                PSP_DAC |= a;
                tbx_PSP_DAC.Text = "0x" + PSP_DAC.ToString("X4");

            }
            catch { }
            // catch { MessageBox.Show("请输入正确数字,范围-127~127", "格式错误"); }
        }

        private void tbx_PSP1_min_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PSP1_min.Text == "") tbx_PSP1_min.Text = "0";
            string sb = Regex.Replace(tbx_PSP1_min.Text, @"[^\d.\d]", "");
            int a = Convert.ToInt32(sb);
            float b = a;
            b /= 256;
            b *= 100;
            lbl_PSP1_min.Text = b.ToString("f1") + " %FS_PADC1";
            //PSP1_LIM
            PSP1_LIM &= 0xFF00;
            PSP1_LIM |= a;
            tbx_PSP1_LIM.Text = "0x" + PSP1_LIM.ToString("X4");
        }

        private void tbx_PSP1_max_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PSP1_max.Text == "") tbx_PSP1_max.Text = "0";
            string sb = Regex.Replace(tbx_PSP1_max.Text, @"[^\d.\d]", "");
            int a = Convert.ToInt32(sb);
            float b = a;
            b /= 256;
            b *= 100;
            lbl_PSP1_max.Text = b.ToString("f1") + " %FS_PADC1";
            //PSP1_LIM
            PSP1_LIM &= 0x00FF;
            PSP1_LIM |= (a << 8);
            tbx_PSP1_LIM.Text = "0x" + PSP1_LIM.ToString("X4");
        }

        private void cbb_p2_gain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_p2_gain.SelectedIndex == -1) return;
            double DAC, a = 0, b = 0;
            float ADC_min, ADC_max, ADC_standard, ADC_gain = 0;
            switch (cbb_p2_gain.Items[cbb_p2_gain.SelectedIndex])
            {
                case "0": lbl_p2_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; a = 9; b = 1; break;
                case "1": lbl_p2_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; a = 6.43; b = 1.3; break;
                case "2": lbl_p2_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; a = 4.52; b = 1; break;
                case "3": lbl_p2_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; a = 3.218; b = 1.35; break;
                case "4": lbl_p2_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "8": lbl_p2_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "9": lbl_p2_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; a = 1.6; b = 1.5; break;
                case "10": lbl_p2_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; a = 1.12; b = 1.5; break;
                case "11": lbl_p2_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; a = 0.798; b = 0.5; break;
                case "12": lbl_p2_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; a = 0.556; b = 0.5; break;
                default: break;
            }
            ADC_standard = ADC_gain * (cbb_p2_offset.SelectedIndex * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p2_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p2_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";
            //tbx_POFFS2_PGAIN2
            int c = Convert.ToInt32(cbb_p2_gain.Items[cbb_p2_gain.SelectedIndex]);
            POFFS2_PGAIN2 &= 0xFFF0;
            POFFS2_PGAIN2 |= c;
            tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");
            //tbx_PSP2_DAC
            if (cbb_p2_offset.SelectedIndex < 16)
                DAC = a * (cbb_p2_offset.SelectedIndex - 16) - b;
            else DAC = a * (cbb_p2_offset.SelectedIndex - 16) + b;
            if (DAC < -127) DAC = -127.0;
            else if (DAC > 127) DAC = 127.0;
            int d = DAC.ToString().IndexOf(".");
            if (d <= 0) tbx_PSP2_DAC.Text = DAC.ToString() + "(默认)"; //如果计算结果是整数
            else tbx_PSP2_DAC.Text = DAC.ToString().Substring(0, d) + "(默认)";
            //tbx_PSP2_min/ax
            if (cbb_p2_offset.SelectedIndex == -1) return;
            byte shuzu = PSP_max[cbb_p2_gain.SelectedIndex, cbb_p2_offset.SelectedIndex];
            tbx_PSP2_min.Text = shuzu.ToString() + "(默认)";
            tbx_PSP2_max.Text = (shuzu + 52).ToString() + "(默认)";
            PSP2_LIM = ((shuzu + 52) << 8) + shuzu; ;
            tbx_PSP2_LIM.Text = "0x" + PSP2_LIM.ToString("X4");
        }

        private void cbb_p2_offset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_p2_offset.SelectedIndex == -1) return;
            if (cbb_p2_gain.SelectedIndex == -1) return;
            lbl_p2_offset1.Text = ((cbb_p2_offset.SelectedIndex - 16) * 25).ToString() + " %FS";
            //
            double DAC, a = 0, b = 0;
            float ADC_min, ADC_max, ADC_standard, ADC_gain = 0;
            switch (cbb_p2_gain.Items[cbb_p2_gain.SelectedIndex])
            {
                case "0": lbl_p2_gain1.Text = "112 mV/V, 500kOhm"; ADC_gain = 112; a = 9; b = 1; break;
                case "1": lbl_p2_gain1.Text = "80 mV/V, 360kOhm"; ADC_gain = 80; a = 6.43; b = 1.3; break;
                case "2": lbl_p2_gain1.Text = "56 mV/V, 250kOhm"; ADC_gain = 56; a = 4.52; b = 1; break;
                case "3": lbl_p2_gain1.Text = "40 mV/V, 180kOhm"; ADC_gain = 40; a = 3.218; b = 1.35; break;
                case "4": lbl_p2_gain1.Text = "28 mV/V, 125kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "8": lbl_p2_gain1.Text = "28 mV/V, 500kOhm"; ADC_gain = 28; a = 2.26; b = 1.3; break;
                case "9": lbl_p2_gain1.Text = "20 mV/V, 360kOhm"; ADC_gain = 20; a = 1.6; b = 1.5; break;
                case "10": lbl_p2_gain1.Text = "14 mV/V, 250kOhm"; ADC_gain = 14; a = 1.12; b = 1.5; break;
                case "11": lbl_p2_gain1.Text = "10 mV/V, 180kOhm"; ADC_gain = 10; a = 0.798; b = 0.5; break;
                case "12": lbl_p2_gain1.Text = "7 mV/V, 125kOhm"; ADC_gain = 7; a = 0.556; b = 0.5; break;
                default: break;
            }
            ADC_standard = ADC_gain * (cbb_p2_offset.SelectedIndex * 25 - 400);
            ADC_standard /= 100;
            ADC_max = ADC_standard + ADC_gain / 2;
            ADC_min = ADC_standard - ADC_gain / 2;
            tbx_p2_ADC_max.Text = ADC_max.ToString("f1") + " mV/V";
            tbx_p2_ADC_min.Text = ADC_min.ToString("f1") + " mV/V";
            //tbx_PSP2_DAC
            if (cbb_p2_offset.SelectedIndex < 16)
                DAC = a * (cbb_p2_offset.SelectedIndex - 16) - b;
            else DAC = a * (cbb_p2_offset.SelectedIndex - 16) + b;
            if (DAC < -127) DAC = -127.0;
            else if (DAC > 127) DAC = 127.0;
            int d = DAC.ToString().IndexOf(".");
            if (d <= 0) tbx_PSP2_DAC.Text = DAC.ToString() + "(默认)"; //如果计算结果是整数
            else tbx_PSP2_DAC.Text = DAC.ToString().Substring(0, d) + "(默认)";
            //tbx_POFFS2_PGAIN2
            POFFS2_PGAIN2 &= 0xE0FF;
            if (cbb_p2_offset.SelectedIndex < 16) POFFS2_PGAIN2 |= (cbb_p2_offset.SelectedIndex + 16) << 8;
            else POFFS2_PGAIN2 |= (cbb_p2_offset.SelectedIndex - 16) << 8;
            tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");
            //tbx_PSP2_min/ax
            byte shuzu = PSP_max[cbb_p2_gain.SelectedIndex, cbb_p2_offset.SelectedIndex];
            tbx_PSP2_min.Text = shuzu.ToString() + "(默认)";
            tbx_PSP2_max.Text = (shuzu + 52).ToString() + "(默认)";
            PSP2_LIM = ((shuzu + 52) << 8) + shuzu; ;
            tbx_PSP2_LIM.Text = "0x" + PSP2_LIM.ToString("X4");
        }

        private void cbx_POL2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_POL2.Checked) POFFS2_PGAIN2 |= 0x0080;
            else POFFS2_PGAIN2 &= 0xFF7F;
            tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");
        }

        private void cbx_PHALF2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PHALF2.Checked) POFFS2_PGAIN2 |= 0x0040;
            else POFFS2_PGAIN2 &= 0xFFBF;
            tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");
        }

        private void cbx_PSP2_EN_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PSP2_EN.Checked)
            {
                tbx_PSP2_DAC.Enabled = true;
                tbx_PSP2_min.Enabled = true;
                tbx_PSP2_max.Enabled = true;
                lbl_PSP2_DAC.Visible = true;
                lbl_PSP2_min.Visible = true;
                lbl_PSP2_max.Visible = true;
                tbx_BIST2.Visible = true;
                ERR_EN2_1 |= (1 << 1);
            }
            else
            {
                tbx_PSP2_DAC.Enabled = false;
                tbx_PSP2_min.Enabled = false;
                tbx_PSP2_max.Enabled = false;
                lbl_PSP2_DAC.Visible = false;
                lbl_PSP2_min.Visible = false;
                lbl_PSP2_max.Visible = false;
                tbx_BIST2.Visible = false;
                ERR_EN2_1 &= ~(1 << 1);
            }
        }

        private void cbx_PSP1_EN_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_PSP1_EN.Checked)
            {
                tbx_PSP1_DAC.Enabled = true;
                tbx_PSP1_min.Enabled = true;
                tbx_PSP1_max.Enabled = true;
                lbl_PSP1_DAC.Visible = true;
                lbl_PSP1_min.Visible = true;
                lbl_PSP1_max.Visible = true;
                tbx_BIST1.Visible = true;
                ERR_EN2_1 |= 1;
            }
            else
            {
                tbx_PSP1_DAC.Enabled = false;
                tbx_PSP1_min.Enabled = false;
                tbx_PSP1_max.Enabled = false;
                lbl_PSP1_DAC.Visible = false;
                lbl_PSP1_min.Visible = false;
                lbl_PSP1_max.Visible = false;
                tbx_BIST1.Visible = false;
                ERR_EN2_1 &= ~1;
            }
        }

        private void tbx_PSP2_DAC_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PSP2_DAC.Text == "") tbx_PSP2_DAC.Text = "0";
            if (tbx_PSP2_DAC.Text == "-") return;
            string sb = "";
            foreach (char c in tbx_PSP2_DAC.Text)
            {
                if (c == '-')
                    sb += c;
                else if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    sb += c;
                else break;
            }
            try
            {
                int a = Convert.ToInt32(sb);
                if (a < -127) { a = -127; tbx_PSP2_DAC.Text = "-127"; }
                else if (a > 127) { a = 127; tbx_PSP2_DAC.Text = "127"; }
                float b = a;
                b += 127;
                b *= 100;
                b /= 254;
                lbl_PSP2_DAC.Text = b.ToString("f1") + " %FS_DAC";
                b = a;
                b *= 312;
                b /= 100;
                tbx_BIST2.Text = b.ToString("f1") + " m/mV";
                //PSP_DAC(p2/p1)
                PSP_DAC &= 0x00FF;
                if (a < 0) a += 256;
                PSP_DAC |= (a << 8);
                tbx_PSP_DAC.Text = "0x" + PSP_DAC.ToString("X4");

            }
            catch { }
            //catch { MessageBox.Show("请输入正确数字,范围-127~127", "格式错误"); }
        }

        private void cbb_LPFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LPFC.SelectedIndex == -1) return;
            switch (cbb_LPFC.SelectedIndex)
            {
                case 0: lbl_fc.Text = "无滤波器，不使用"; lbl_t.Text = "t_settling = -"; break;
                case 1: lbl_fc.Text = "f_c = 1092 Hz"; lbl_t.Text = "t_settling = <1 ms"; break;
                case 2: lbl_fc.Text = "f_c = 459 Hz"; lbl_t.Text = "t_settling = 1.0 ms"; break;
                case 3: lbl_fc.Text = "f_c = 213 Hz"; lbl_t.Text = "t_settling = 1.9 ms"; break;
                case 4: lbl_fc.Text = "f_c = 104 Hz"; lbl_t.Text = "t_settling = 3.4 ms"; break;
                case 5: lbl_fc.Text = "f_c = 51 Hz"; lbl_t.Text = "t_settling = 6.7 ms"; break;
                case 6: lbl_fc.Text = "f_c = 26 Hz"; lbl_t.Text = "t_settling = 13 ms"; break;
                case 7: lbl_fc.Text = "f_c = 13 Hz"; lbl_t.Text = "t_settling = 27 ms"; break;
                default: break;
            }
            POFFS1_PGAIN1 &= 0x1FFF;
            POFFS1_PGAIN1 |= (cbb_LPFC.SelectedIndex << 13);
            tbx_POFFS1_PGAIN1.Text = "0x" + POFFS1_PGAIN1.ToString("X4");
        }

        private void cbx_p2_EN_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_p2_EN.Checked)
            {
                cbb_p2_gain.Enabled = false;
                cbb_p2_offset.Enabled = false;
                cbx_POL2.Enabled = false;
                cbx_PHALF2.Enabled = false;
                cbx_PSP2_EN.Enabled = false;
                tbx_PSP2_DAC.Enabled = false;
                tbx_PSP2_min.Enabled = false;
                tbx_PSP2_max.Enabled = false;
                lbl_p2_gain1.Visible = false;
                lbl_p2_offset1.Visible = false;
                lbl_PSP2_DAC.Visible = false;
                lbl_PSP2_min.Visible = false;
                lbl_PSP2_max.Visible = false;
                tbx_p2_ADC_max.Visible = false;
                tbx_p2_ADC_min.Visible = false;
                tbx_BIST2.Visible = false;
                POFFS2_PGAIN2 |= 0x10;
                tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");

            }
            else
            {
                cbb_p2_gain.Enabled = true;
                cbb_p2_offset.Enabled = true;
                cbx_POL2.Enabled = true;
                cbx_PHALF2.Enabled = true;
                cbx_PSP2_EN.Enabled = true;
                tbx_PSP2_DAC.Enabled = true;
                tbx_PSP2_min.Enabled = true;
                tbx_PSP2_max.Enabled = true;
                lbl_p2_gain1.Visible = true;
                lbl_p2_offset1.Visible = true;
                lbl_PSP2_DAC.Visible = true;
                lbl_PSP2_min.Visible = true;
                lbl_PSP2_max.Visible = true;
                tbx_p2_ADC_max.Visible = true;
                tbx_p2_ADC_min.Visible = true;
                tbx_BIST2.Visible = true;
                POFFS2_PGAIN2 &= 0xFFEF;
                tbx_POFFS2_PGAIN2.Text = "0x" + POFFS2_PGAIN2.ToString("X4");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Form1.NVM_code[0x3F] = ERR_EN2_1.ToString("X4");
            Form1.NVM_code[0x3B] = POFFS1_PGAIN1.ToString("X4");
            Form1.NVM_code[0x3C] = POFFS2_PGAIN2.ToString("X4");
            Form1.NVM_code[0x44] = PV_LIM.ToString("X4");
            Form1.NVM_code[0x4C] = PSP_DAC.ToString("X4");
            Form1.NVM_code[0x4D] = PSP1_LIM.ToString("X4");
            Form1.NVM_code[0x4E] = PSP2_LIM.ToString("X4");
            Form1.CRC2();
            this.Close();
        }

        private void tbx_BIST1_TextChanged(object sender, EventArgs e)
        {
            //warning
            float b, p1_ADC_max, p1_ADC_min, limit_min, limit_max;
            b = Convert.ToSingle(tbx_BIST1.Text.Substring(0, tbx_BIST1.TextLength - 5));
            p1_ADC_min = Convert.ToSingle(tbx_p1_ADC_min.Text.Substring(0, tbx_p1_ADC_min.TextLength - 5));
            p1_ADC_max = Convert.ToSingle(tbx_p1_ADC_max.Text.Substring(0, tbx_p1_ADC_max.TextLength - 5));
            limit_min = (p1_ADC_max - p1_ADC_min) / 10;
            limit_max = limit_min * 9 + p1_ADC_min;
            limit_min += p1_ADC_min;
            if ((b < limit_min) || (b > limit_max)) lbl_warning1.Visible = true;
            else lbl_warning1.Visible = false;
        }

        private void tbx_BIST2_TextChanged(object sender, EventArgs e)
        {
            //warning
            float b, p2_ADC_max, p2_ADC_min, limit_min, limit_max;
            b = Convert.ToSingle(tbx_BIST2.Text.Substring(0, tbx_BIST2.TextLength - 5));
            p2_ADC_min = Convert.ToSingle(tbx_p2_ADC_min.Text.Substring(0, tbx_p2_ADC_min.TextLength - 5));
            p2_ADC_max = Convert.ToSingle(tbx_p2_ADC_max.Text.Substring(0, tbx_p2_ADC_max.TextLength - 5));
            limit_min = (p2_ADC_max - p2_ADC_min) / 10;
            limit_max = limit_min * 9 + p2_ADC_min;
            limit_min += p2_ADC_min;
            if ((b < limit_min) || (b > limit_max)) lbl_warning2.Visible = true;
            else lbl_warning2.Visible = false;
        }
    }
}
