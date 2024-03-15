using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static E520._47标定.Form1;

namespace E520._47标定
{
    public partial class verify : Form
    {
        public verify()
        {
            InitializeComponent();
            DoubleBufferListView.DoubleBufferedListView(listView_sample, true);
        }
        #region **传感器选择**
        private void CBX_MUX0_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX0.Checked == true)
            {
                enabled_channel.Items.Insert(0, "00");
            }
            else { enabled_channel.Items.RemoveAt(0); }
        }

        private void CBX_MUX1_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX1.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("01"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 1)
                    {
                        enabled_channel.Items.Insert(i, "01");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("01");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 1)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX2_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX2.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("02"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 2)
                    {
                        enabled_channel.Items.Insert(i, "02");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("02");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 2)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX3_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX3.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("03"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 3)
                    {
                        enabled_channel.Items.Insert(i, "03");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("03");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 3)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX4_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX4.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("04"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 4)
                    {
                        enabled_channel.Items.Insert(i, "04");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("04");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 4)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX5_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX5.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("05"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 5)
                    {
                        enabled_channel.Items.Insert(i, "05");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("05");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 5)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX6_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX6.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("06"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 6)
                    {
                        enabled_channel.Items.Insert(i, "06");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("06");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 6)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX7_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX7.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("07"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 7)
                    {
                        enabled_channel.Items.Insert(i, "07");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("07");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 7)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX8_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX8.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("08"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 8)
                    {
                        enabled_channel.Items.Insert(i, "08");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("08");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 8)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX9_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX9.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("09"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 9)
                    {
                        enabled_channel.Items.Insert(i, "09");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("09");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 9)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX10_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX10.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0A"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 10)
                    {
                        enabled_channel.Items.Insert(i, "0A");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0A");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 10)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX11_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX11.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0B"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 11)
                    {
                        enabled_channel.Items.Insert(i, "0B");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0B");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 11)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX12_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX12.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0C"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 12)
                    {
                        enabled_channel.Items.Insert(i, "0C");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0C");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 12)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX13_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX13.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0D"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 13)
                    {
                        enabled_channel.Items.Insert(i, "0D");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0D");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 13)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX14_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX14.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0E"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 14)
                    {
                        enabled_channel.Items.Insert(i, "0E");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0E");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 14)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CBX_MUX15_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX15.Checked == true)
            {
                if (enabled_channel.Items.Count == 0) { enabled_channel.Items.Add("0F"); return; }
                int a, b;
                b = enabled_channel.Items.Count;
                for (int i = 0; i < b; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a > 15)
                    {
                        enabled_channel.Items.Insert(i, "0F");
                        break;
                    }
                    if (i == b - 1) enabled_channel.Items.Add("0F");
                }
            }
            else
            {
                int a;
                for (int i = 0; i < enabled_channel.Items.Count; i++)
                {
                    a = Convert.ToInt16(enabled_channel.Items[i].ToString(), 16);
                    if (a == 15)
                    {
                        enabled_channel.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion
        int cishu;
        private void tbx_cishu_TextChanged(object sender, EventArgs e)
        {
            if (tbx_cishu.Text == "") tbx_cishu.Text = "4";
            try
            {
                cishu = Convert.ToUInt16(tbx_cishu.Text);
                if (cishu > 100) { cishu = 100; tbx_cishu.Text = "100"; }
                else if (cishu < 1) { cishu = 1; tbx_cishu.Text = "1"; }
            }
            catch { MessageBox.Show("请输入正确数字,范围1~100", "格式错误"); }
        }
        //List<byte> readBuf = new List<byte>();  //always triggered, when data is received
        //int reLen;
        private void WriteComm(string strComm, int reLength, WorkRun fun)       //写命令
        {       //write specific command to SIO
            try
            {
                port.Write(strComm + "\r");             // send command with carriage return (\r) to indicate end of command
                Form1.reLen = reLength;
                Form1.workRun = fun;                          //set corresponding callback function 标记发出的是哪个命令，方便接收到的数据对接
            }
            catch (Exception ex)
            {                                   // handle errors
                MessageBox.Show(ex.Message);
            }
            finally { }
        }
        private bool OPENCOM(byte[] strRet)         //ENACONF     #ENACONF+06 : //15
        {
            CBX_MUX0.Enabled = true; CBX_MUX1.Enabled = true; CBX_MUX2.Enabled = true; CBX_MUX3.Enabled = true;
            CBX_MUX4.Enabled = true; CBX_MUX5.Enabled = true; CBX_MUX6.Enabled = true;
            CBX_MUX7.Enabled = true; CBX_MUX8.Enabled = true; CBX_MUX9.Enabled = true;
            CBX_MUX10.Enabled = true; CBX_MUX11.Enabled = true; CBX_MUX12.Enabled = true;
            CBX_MUX13.Enabled = true; CBX_MUX14.Enabled = true; CBX_MUX15.Enabled = true;
            return false;
            //string str = Encoding.Default.GetString(strRet);                //encoding received bytes to ASCII

            //if (str.Contains("#POWEROFF"))
            //{
            //    return false;
            //}
            //else
            //{
            //    return false;
            //}
        }
        bool power_Flag = false;
        string ID_sample;
        int FC1, FC2, P_DIFF_0;
        UInt32 P1_sample_sum, P2_sample_sum, T1_sample_sum, T2_sample_sum;
        UInt32 P1_sent_sum, P2_sent_sum, T1_sent_sum, T2_sent_sum;
        double shiji1, shiji2, shiji3, shiji4;
        double c1 = Convert.ToDouble(Form1.verify_shuzu[1]);
        double c2 = Convert.ToDouble(Form1.verify_shuzu[5]);
        double a1 = (Convert.ToDouble(Form1.verify_shuzu[1]) - Convert.ToDouble(Form1.verify_shuzu[0])) /
                    (Convert.ToDouble(Form1.verify_shuzu[3]) - Convert.ToDouble(Form1.verify_shuzu[2]));
        double a2 = Convert.ToDouble(Form1.verify_shuzu[2]);
        double b1 = Convert.ToDouble(Form1.verify_shuzu[0]);
        double a3 = (Convert.ToDouble(Form1.verify_shuzu[5]) - Convert.ToDouble(Form1.verify_shuzu[4])) /
                    (Convert.ToDouble(Form1.verify_shuzu[7]) - Convert.ToDouble(Form1.verify_shuzu[6]));
        double a4 = Convert.ToDouble(Form1.verify_shuzu[6]);
        double b2 = Convert.ToDouble(Form1.verify_shuzu[4]);
        double a5 = (Convert.ToDouble(Form1.verify_shuzu[9]) - Convert.ToDouble(Form1.verify_shuzu[8])) /
                    (Convert.ToDouble(Form1.verify_shuzu[11]) - Convert.ToDouble(Form1.verify_shuzu[10]));
        double a6 = Convert.ToDouble(Form1.verify_shuzu[10]);



        double b3 = Convert.ToDouble(Form1.verify_shuzu[8]);
        double a7 = (Convert.ToDouble(Form1.verify_shuzu[13]) - Convert.ToDouble(Form1.verify_shuzu[12])) /
                    (Convert.ToDouble(Form1.verify_shuzu[15]) - Convert.ToDouble(Form1.verify_shuzu[14]));
        double a8 = Convert.ToDouble(Form1.verify_shuzu[14]);
        double b4 = Convert.ToDouble(Form1.verify_shuzu[12]);
        private void verify_Load(object sender, EventArgs e)
        {
            int sensor_xuanze = Convert.ToInt32(Form1.verify_shuzu[16]);
            int a;
            a = sensor_xuanze & 1;
            if (a == 1) CBX_MUX0.Checked = true; else CBX_MUX0.Checked = false;
            a = (sensor_xuanze >> 1) & 1;
            if (a == 1) CBX_MUX1.Checked = true; else CBX_MUX1.Checked = false;
            a = (sensor_xuanze >> 2) & 1;
            if (a == 1) CBX_MUX2.Checked = true; else CBX_MUX2.Checked = false;
            a = (sensor_xuanze >> 3) & 1;
            if (a == 1) CBX_MUX3.Checked = true; else CBX_MUX3.Checked = false;
            a = (sensor_xuanze >> 4) & 1;
            if (a == 1) CBX_MUX4.Checked = true; else CBX_MUX4.Checked = false;
            a = (sensor_xuanze >> 5) & 1;
            if (a == 1) CBX_MUX5.Checked = true; else CBX_MUX5.Checked = false;
            a = (sensor_xuanze >> 6) & 1;
            if (a == 1) CBX_MUX6.Checked = true; else CBX_MUX6.Checked = false;
            a = (sensor_xuanze >> 7) & 1;
            if (a == 1) CBX_MUX7.Checked = true; else CBX_MUX7.Checked = false;
            a = (sensor_xuanze >> 8) & 1;
            if (a == 1) CBX_MUX8.Checked = true; else CBX_MUX8.Checked = false;
            a = (sensor_xuanze >> 9) & 1;
            if (a == 1) CBX_MUX9.Checked = true; else CBX_MUX9.Checked = false;
            a = (sensor_xuanze >> 10) & 1;
            if (a == 1) CBX_MUX10.Checked = true; else CBX_MUX10.Checked = false;
            a = (sensor_xuanze >> 11) & 1;
            if (a == 1) CBX_MUX11.Checked = true; else CBX_MUX11.Checked = false;
            a = (sensor_xuanze >> 12) & 1;
            if (a == 1) CBX_MUX12.Checked = true; else CBX_MUX12.Checked = false;
            a = (sensor_xuanze >> 13) & 1;
            if (a == 1) CBX_MUX13.Checked = true; else CBX_MUX13.Checked = false;
            a = (sensor_xuanze >> 14) & 1;
            if (a == 1) CBX_MUX14.Checked = true; else CBX_MUX14.Checked = false;
            a = (sensor_xuanze >> 15) & 1;
            if (a == 1) CBX_MUX15.Checked = true; else CBX_MUX15.Checked = false;

            //int aa = Convert.ToInt32(Form1.verify_shuzu[17],16);
            //for (int y = 0; y < aa; y++)
            //{

            //    enabled_channel.Items.Add(Form1.verify_shuzu[18 + y]);
            //}
        }

        private bool Sample(byte[] strRet)         //写入NVM数据
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#POWEROFF"))
            {
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, Sample);
                return true;
            }
            else if (str.Contains("Neither"))
            {

                cishu = Convert.ToUInt16(tbx_cishu.Text);
                if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                {
                    enabled_channel.SelectedIndex++;
                    cishu = Convert.ToUInt16(tbx_cishu.Text);
                    WriteComm("POWEROFF", 15, Sample);
                }
                else
                {

                    WriteComm("POWEROFF", 15, OPENCOM);     //全部产品采样完毕
                }
                return true;
            }
            else if (str.Contains("#SETMUX "))
            {
                WriteComm("ENACONF", 15, Sample);
                return true;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("CCP 2 ca cb 6", 35, Sample);
                return true;
            }

            else if (str.Contains("#CCP 02 ca"))
            {
                ID_sample = str.Substring(21, 11);
                WriteComm("CCP 3 93 41 d5 4", 35, Sample);
                return true;
            }
            else if (str.Contains("#CCP 03 93 41 d5"))
            {
                P_DIFF_0 = Convert.ToInt32(str.Substring(24, 5).Remove(2, 1), 16);//p1-p2
                WriteComm("CCP 3 93 3e d2 4", 35, Sample);
                return true;
            }
            else if (str.Contains("#CCP 03 93 3e d2"))
            {
                string SENTCONF4 = str.Substring(24, 2);
                int conf4 = Convert.ToInt32(SENTCONF4, 16);
                FC1 = conf4 & 7;
                FC2 = (conf4 >> 3) & 7;
                WriteComm("CCP 3 c3 03 c7 6", 40, Sample);
                return true;
            }
            else if (str.Contains("#CCP 03 c3 03 c7"))
            {
                P1_sample_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                P2_sample_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 c3 02 c6 6", 40, Sample);
                return true;
            }

            else if (str.Contains("#CCP 03 c3 02 c6"))
            {
                P1_sent_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                P2_sent_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 cb 03 cf 6", 40, Sample);
                return true;
            }

            else if (str.Contains("#CCP 03 cb 03 cf"))
            {
                T1_sample_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                T2_sample_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 cb 02 ce 6", 40, Sample);
                return true;
            }

            else if (str.Contains("#CCP 03 cb 02 ce"))
            {
                T1_sent_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                T2_sent_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                cishu--;
                if (cishu == 0)                          //停止采样
                {
                    if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                    {
                        enabled_channel.SelectedIndex++;
                        cishu = Convert.ToUInt16(tbx_cishu.Text);
                        WriteComm("POWEROFF", 15, Sample);
                    }
                    else
                    {
                        WriteComm("POWEROFF", 15, OPENCOM);     //全部产品采样完毕
                    }

                    ListViewItem item1 = new ListViewItem();
                    uint n = Convert.ToUInt32(tbx_cishu.Text);

                    P1_sample_sum /= n;
                    P2_sample_sum /= n;
                    T1_sample_sum /= n;
                    T2_sample_sum /= n;
                    P1_sent_sum /= n;
                    P2_sent_sum /= n;
                    T1_sent_sum /= n;
                    T2_sent_sum /= n;
                    shiji1 = a1 * (P1_sent_sum - a2) + b1;
                    shiji2 = a3 * (P2_sent_sum - a4) + b2;
                    shiji3 = a5 * (T1_sent_sum - a6) + b3;
                    shiji4 = a7 * (T2_sent_sum - a8) + b4;

                    item1.SubItems.Add(c.ToString());
                    item1.SubItems.Add(ID_sample);
                    item1.SubItems.Add(P1_sample_sum.ToString());
                    item1.SubItems.Add(P2_sample_sum.ToString());
                    item1.SubItems.Add(T1_sample_sum.ToString());
                    item1.SubItems.Add(T2_sample_sum.ToString());
                    item1.SubItems.Add(P1_sent_sum.ToString());
                    item1.SubItems.Add(P2_sent_sum.ToString());
                    item1.SubItems.Add(T1_sent_sum.ToString());
                    item1.SubItems.Add(T2_sent_sum.ToString());
                    if (shiji1 <= c1) item1.SubItems.Add(shiji1.ToString("f2"));
                    else item1.SubItems.Add("-");
                    if (shiji2 <= c2) item1.SubItems.Add(shiji2.ToString("f2"));
                    else item1.SubItems.Add("-");
                    item1.SubItems.Add(shiji3.ToString("f2"));
                    item1.SubItems.Add(shiji4.ToString("f2"));
                    switch (FC1)
                    {
                        case 1: item1.SubItems.Add(P1_sent_sum.ToString()); break;
                        case 2: item1.SubItems.Add(P2_sent_sum.ToString()); break;
                        case 3: item1.SubItems.Add((P1_sent_sum - P2_sent_sum + P_DIFF_0).ToString()); break;
                        case 7: item1.SubItems.Add("4095"); break;
                        default: item1.SubItems.Add("-"); break;
                    }
                    switch (FC2)
                    {
                        case 0: item1.SubItems.Add("0"); break;
                        case 1: item1.SubItems.Add(T1_sent_sum.ToString()); break;
                        case 2: item1.SubItems.Add(T2_sent_sum.ToString()); break;
                        case 3: item1.SubItems.Add(P2_sent_sum.ToString()); break;
                        case 7: item1.SubItems.Add("4095"); break;
                        default: item1.SubItems.Add("-"); break;
                    }


                    listView_sample.Items.Add(item1);

                    P1_sample_sum = 0;
                    P2_sample_sum = 0;
                    T1_sample_sum = 0;
                    T2_sample_sum = 0;
                    P1_sent_sum = 0;
                    P2_sent_sum = 0;
                    T1_sent_sum = 0;
                    T2_sent_sum = 0;
                }
                else
                {
                    WriteComm("CCP 3 c3 03 c7 6", 40, Sample);//本产品再次采样
                }
                return true;
            }

            else
            {                                               // handle errors
                MessageBox.Show("数据通信失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ContinusRead(byte[] strRet)         //连续读
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#POWEROFF") && power_Flag == false)
            {
                //Thread.Sleep(100);
                WriteComm(".LOGLEVEL 0", 15, ContinusRead);
                return true;
            }
            else if (str.Contains("#.LOGLEVEL"))
            {
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, ContinusRead);
                return true;
            }
            else if (str.Contains("#SETMUX "))
            {
                WriteComm("ENACONF", 15, ContinusRead);
                return true;
            }
            else if (str.Contains("#ENACONF") && power_Flag == false)
            {
                power_Flag = true;
                WriteComm("POWEROFF", 15, ContinusRead);

                return true;
            }

            else if (str.Contains("#POWEROFF") && power_Flag)
            {
                WriteComm("ENACONF", 15, ContinusRead);
                Thread.Sleep(100);
                return true;
            }

            else if (str.Contains("#ENACONF") && power_Flag)
            {
                power_Flag = false;

                WriteComm("CCP 2 ca cb 6", 35, ContinusRead);
                return true;
            }

            else if (str.Contains("#CCP 02 ca"))
            {
                ID_sample = str.Substring(21, 11);
                WriteComm("CCP 3 93 41 d5 4", 35, ContinusRead);
                return true;
            }
            else if (str.Contains("#CCP 03 93 41 d5"))
            {
                P_DIFF_0 = Convert.ToInt32(str.Substring(24, 5).Remove(2, 1), 16);//p1-p2
                WriteComm("CCP 3 93 3e d2 4", 35, ContinusRead);
                return true;
            }
            else if (str.Contains("#CCP 03 93 3e d2"))
            {
                string SENTCONF4 = str.Substring(24, 2);
                int conf4 = Convert.ToInt32(SENTCONF4, 16);
                FC1 = conf4 & 7;
                FC2 = (conf4 >> 3) & 7;
                WriteComm("CCP 3 c3 03 c7 6", 40, ContinusRead);
                return true;
            }
            else if (str.Contains("#CCP 03 c3 03 c7"))
            {
                P1_sample_sum = Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                P2_sample_sum = Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 c3 02 c6 6", 40, ContinusRead);
                return true;
            }

            else if (str.Contains("#CCP 03 c3 02 c6"))
            {
                P1_sent_sum = Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                P2_sent_sum = Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 cb 03 cf 6", 40, ContinusRead);
                return true;
            }

            else if (str.Contains("#CCP 03 cb 03 cf"))
            {
                T1_sample_sum = Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                T2_sample_sum = Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 cb 02 ce 6", 40, ContinusRead);
                return true;
            }

            else if (str.Contains("#CCP 03 cb 02 ce"))
            {
                T1_sent_sum = Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                T2_sent_sum = Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);



                shiji1 = a1 * (P1_sent_sum - a2) + b1;
                shiji2 = a3 * (P2_sent_sum - a4) + b2;
                shiji3 = a5 * (T1_sent_sum - a6) + b3;
                shiji4 = a7 * (T2_sent_sum - a8) + b4;

                listView_sample.Items[c].SubItems[1].Text = c.ToString();
                listView_sample.Items[c].SubItems[2].Text = ID_sample;
                listView_sample.Items[c].SubItems[3].Text = P1_sample_sum.ToString();
                listView_sample.Items[c].SubItems[4].Text = P2_sample_sum.ToString();
                listView_sample.Items[c].SubItems[5].Text = T1_sample_sum.ToString();
                listView_sample.Items[c].SubItems[6].Text = T2_sample_sum.ToString();
                listView_sample.Items[c].SubItems[7].Text = P1_sent_sum.ToString();
                listView_sample.Items[c].SubItems[8].Text = P2_sent_sum.ToString();
                listView_sample.Items[c].SubItems[9].Text = T1_sent_sum.ToString();
                listView_sample.Items[c].SubItems[10].Text = T2_sent_sum.ToString();
                if (shiji1 <= c1) listView_sample.Items[c].SubItems[11].Text = shiji1.ToString("f2");
                else listView_sample.Items[c].SubItems[11].Text = "-";
                if (shiji2 <= c2) listView_sample.Items[c].SubItems[12].Text = shiji2.ToString("f2");
                else listView_sample.Items[c].SubItems[12].Text = "-";
                listView_sample.Items[c].SubItems[13].Text = shiji3.ToString("f2");
                listView_sample.Items[c].SubItems[14].Text = shiji4.ToString("f2");
                switch (FC1)
                {
                    case 1: listView_sample.Items[c].SubItems[15].Text = P1_sent_sum.ToString(); break;
                    case 2: listView_sample.Items[c].SubItems[15].Text = P2_sent_sum.ToString(); break;
                    case 3: listView_sample.Items[c].SubItems[15].Text = (P1_sent_sum - P2_sent_sum + P_DIFF_0).ToString(); break;
                    case 7: listView_sample.Items[c].SubItems[15].Text = "4095"; break;
                    default: listView_sample.Items[c].SubItems[15].Text = "-"; break;
                }
                switch (FC2)
                {
                    case 0: listView_sample.Items[c].SubItems[16].Text = "0"; break;
                    case 1: listView_sample.Items[c].SubItems[16].Text = T1_sent_sum.ToString(); break;
                    case 2: listView_sample.Items[c].SubItems[16].Text = T2_sent_sum.ToString(); break;
                    case 3: listView_sample.Items[c].SubItems[16].Text = P2_sent_sum.ToString(); break;
                    case 7: listView_sample.Items[c].SubItems[16].Text = "4095"; break;
                    default: listView_sample.Items[c].SubItems[16].Text = "-"; break;
                }
                if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                {
                    enabled_channel.SelectedIndex++;
                }
                else
                {
                    enabled_channel.SelectedIndex = 0;
                    cishu--;
                }
                if (cishu == 0) { WriteComm("POWEROFF", 15, OPENCOM); }
                else WriteComm("POWEROFF", 15, ContinusRead);

                return true;
            }
            else if (str.Contains("Neither"))
            {
                if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                {
                    enabled_channel.SelectedIndex++;
                    WriteComm("POWEROFF", 15, ContinusRead);
                }
                else
                {
                    WriteComm("POWEROFF", 15, OPENCOM);     //全部产品采样完毕
                }
                return true;
            }
            else
            {                                               // handle errors
                MessageBox.Show("数据通信失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        bool first_sample = true;
        private void btn_clear_Click(object sender, EventArgs e)
        {
            listView_sample.Items.Clear();
        }
        private void btn_sample1_Click(object sender, EventArgs e)
        {
            try
            {
                enabled_channel.SelectedIndex = 0;
            }
            catch { MessageBox.Show("请至少选择一个产品", "错误提示"); return; }
            cishu = Convert.ToUInt16(tbx_cishu.Text);
            if (first_sample)
            {
                listView_sample.Items.Clear();
                first_sample = false;
            }
            CBX_MUX0.Enabled = false; CBX_MUX1.Enabled = false; CBX_MUX2.Enabled = false; CBX_MUX3.Enabled = false;
            CBX_MUX4.Enabled = false; CBX_MUX5.Enabled = false; CBX_MUX6.Enabled = false;
            CBX_MUX7.Enabled = false; CBX_MUX8.Enabled = false; CBX_MUX9.Enabled = false;
            CBX_MUX10.Enabled = false; CBX_MUX11.Enabled = false; CBX_MUX12.Enabled = false;
            CBX_MUX13.Enabled = false; CBX_MUX14.Enabled = false; CBX_MUX15.Enabled = false;

            WriteComm("POWEROFF", 15, Sample);
        }
        private void btn_continuously_read_Click(object sender, EventArgs e)
        {
            if (btn_continuously_read.Text == "连续读")
            {
                btn_continuously_read.Text = "停止";
                try
                {
                    enabled_channel.SelectedIndex = 0;
                }
                catch { MessageBox.Show("请至少选择一个产品", "错误提示"); return; }
                cishu = 99999;
                listView_sample.Items.Clear();

                for (int j = 0; j < 16; j++)
                {
                    ListViewItem item2 = new ListViewItem();
                    for (int i = 0; i < 16; i++)
                    {
                        item2.SubItems.Add("");
                    }
                    listView_sample.Items.Add(item2);
                }
                CBX_MUX0.Enabled = false; CBX_MUX1.Enabled = false; CBX_MUX2.Enabled = false; CBX_MUX3.Enabled = false;
                CBX_MUX4.Enabled = false; CBX_MUX5.Enabled = false; CBX_MUX6.Enabled = false;
                CBX_MUX7.Enabled = false; CBX_MUX8.Enabled = false; CBX_MUX9.Enabled = false;
                CBX_MUX10.Enabled = false; CBX_MUX11.Enabled = false; CBX_MUX12.Enabled = false;
                CBX_MUX13.Enabled = false; CBX_MUX14.Enabled = false; CBX_MUX15.Enabled = false;
                WriteComm("POWEROFF", 15, ContinusRead);
            }

            else
            {
                btn_continuously_read.Text = "连续读";
                cishu = 0;
                WriteComm("POWEROFF", 15, OPENCOM);
            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
