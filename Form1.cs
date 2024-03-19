using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static E520._47标定.Calibration;


namespace E520._47标定
{

    public partial class Form1 : Form
    {
        public static SerialPort port = new SerialPort();

        public Form1()
        {
            InitializeComponent();
            DoubleBufferListView.DoubleBufferedListView(listview_status, true);
            DoubleBufferListView.DoubleBufferedListView(listView_sample, true);

            string[] portname = SerialPort.GetPortNames();   //Get the serial ports
            PortName.Items.AddRange(portname);   //Add to interface
            try { PortName.SelectedIndex = 0; }        //Preselect Items
            catch { }
        }
        public static string[] NVM_code = new string[]
        {   "0000","0000","0000","0000","0000","0000","0000","0000",//OEM[0~8]
            "5A51","A44F","2000","3082","5090","5191","6000","5292",//LOCK1,CRC1,MSG[0~5]
            "5393","5494","5595","5696","5797","6002","4002","49C9",//MSG[6~13]
            "4042","4003","4322","4144","6080","4390","0223","20C1",//MSG[14~21]
            "3009","4F38","1029","2000","3080","6000","112A","122B",//MSG[22~29]
            "132C","F000","F000","F000","F000","F000","F000","F000",//MSG[30~37]
            "F000","F000","F000","F000","0000","0000","0000","0000",//MSG[38~41],free,free,free,free
            "0000","0000","0060","4101","0010","0063","015D","0F3F",//DOFFS1/DGAIN1,DOFFS2/DGAIN2,I2C_CTRL,POFFS1/PGAIN1
                                                                    //POFFS2/PGAIN2,SENTCONF2/1,SENTCONF4/3,ERR_EN2/1
            "C83F","0366","E613","E613","0000","0000","808B","02D1",//ERR_EN4/3, P_DIFF_0,RT1_LIM,RT2_LIM,PV_LIM,TV_LIM,TNUM1_ADC,TNUM1_OUT
            "8147","07EF","0000","0000","0107","A06C","A16D","6F61",//PNUM1_ADC,PNUM1_OUT,PNUM2_ADC,PNUM2_OUT,PSP_DAC,PSP1_LIM,PSP2_LIM,TSP_LIM
            "07C9","0000","0000","0000","07A9","0000","0000","0000",//C1_00/01/02/03,C1_10/11/12/13
            "0000","0000","0000","0000","0200","0000","0000","0000",//C1_20/21/22/23,C2_00/01/02/03
            "0400","0000","0000","0000","0000","0000","0000","0000",//C2_10/11/12/13,C2_20/21/22/23
            "0000","1689","0000","0000","0000","0020","0000","1FF2",//T1_MODE,T1_Q0/1/2/3/4,T2_MODE,T2_Q0
            "F8DA","0000","0000","3E5F","5A52","33DD","6719","0001",//T2_Q1/2/3/4,LOCK2,CRC2,ID1/0,ID3/2
            "3065","0000","54DB","0A88","57DC","0000","0000","B87F"// A/F_TRIM,V_TRIM,M_TADC,N_TADC,OT_LIM,free,LOCK3,CRC3
        };
       
        public static string character = "";
        public static string users_name = "";
        public static bool Slow_channel_Flag = false;
        public static bool Sentconf_Flag = false;
        public static bool Diagnosis_Flag = false;
        public static bool Temperature_Flag = false;
        public static bool Pressure_Flag = false;
        public static string[] slow_ID_Value = new string[42];
        public static UInt16[] NVM_code1 = new UInt16[128];
        public static string[] enable_channel1 = new string[16];
        public static UInt16 selected_channel;
        bool serialIsOpen = false;  //ture放在了标定键，false放在了导入dfr键。写入NVM只判断，不更改。
        public static int reLen;
        UInt32 P1_sample_sum, P2_sample_sum, T1_sample_sum, T2_sample_sum;
        string ID_sample, PSP1_sample, PSP2_sample, PSP1_DAC, PSP2_DAC;
        UInt16 Sample_selected;
        double[,] aAdcDiode1 = new double[16, 4];
        string[,] Calibration_code = new string[15, 44];
        bool cabliration_falg = false;

        //set the delegate callback function
        public delegate bool WorkRun(byte[] reComm);
        public static WorkRun workRun = null;
        public bool WorkAndRun(byte[] reComm)
        {
            TimeOut.Enabled = false;
            if (workRun != null)
            {
                return workRun(reComm);
            }
            else
                return false;
        }
        private void TimeOut_Tick(object sender, EventArgs e)
        {
            if (PortName.SelectedIndex < PortName.Items.Count - 1)
                PortName.SelectedIndex++;
            serialIsOpen = false;
            TimeOut.Enabled = false;
            tbx_status.Text = "未找到串口，请检查";
        }
        List<byte> readBuf = new List<byte>();  //always triggered, when data is received
        int returnNum;
        int rxlen;
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            returnNum = reLen;
            //  if (serialIsOpen == false) { return; }// if port is closed, do not receive data
            rxlen = port.BytesToRead;                           // 读出接收到数据的长度
            if (rxlen < returnNum) { return; }
            byte[] rxbuf = new byte[rxlen];
            port.Read(rxbuf, 0, rxlen);                         // copy received data bytewise
            readBuf.AddRange(rxbuf);
            //Thread.Sleep(5);                                  //和放在开头功能一样的，等待接收完成。注释掉可以节省响应时间
            Invoke((EventHandler)delegate                      //跨线程调用匿名委托
            {
                if (reLen > 0)                                 // if there is data
                {
                    try
                    {
                        while (readBuf.Count >= returnNum)         // check, if complete command is received //relen表示预计接收到的字符数量
                        {                                           //循环等待，一旦接收到指定的长度，就把数据传送到WorkAndRun
                            if (readBuf[0] == '#')                  // check received command 如果首字符不是#，不接收
                            {
                                WorkAndRun(readBuf.ToArray());      //Call the corresponding function 将收到的数据装入线程中
                                                                    //readBuf.RemoveRange(0, returnNum);  //Remove used data
                                readBuf.Clear();
                            }
                            else { readBuf.RemoveAt(0); }               // remove first bit 首字符不是#时，删除。循环等待#出现
                        }
                    }
                    catch { return; }
                }
            });
        }
        string current_com;
        string[] portname;
        UInt16 cishu;
        bool thread_Flag = true;
        private void Form1_Load(object sender, EventArgs e)
        {           
            listView_sample.Items.Clear();           
            ThreadStart threadStart = new ThreadStart(() =>
            {
                int COM_count;
                while (thread_Flag)
                {
                    portname = SerialPort.GetPortNames();   //Get the serial ports
                    COM_count = portname.Length;
                    try
                    {
                        if ((portname.Length == 0) || ((portname.Length == 1) && (portname[0] == "COM1")))
                        {
                            PortName.Invoke(new Action(() =>
                            {
                                
                                COM_Port.Enabled = false;
                                serialIsOpen = false;
                                tbx_status.Text = "未找到串口，请检查";
                            }));
                            Thread.Sleep(1000);
                            continue;
                        }
                        if ((serialIsOpen == false) && (TimeOut.Enabled == false))
                        {
                            Invoke(new Action(() =>
                            {
                                if (serialIsOpen == false)
                                {
                                    if ((PortName.SelectedIndex >= PortName.Items.Count) || (PortName.Items.Count != COM_count))
                                    {
                                        PortName.Items.Clear();
                                        PortName.Items.AddRange(portname);
                                    }
                                    if (PortName.SelectedIndex < 0) PortName.SelectedIndex = 0;        //Preselect Items                               
                                    COM_Port.Text = portname[PortName.SelectedIndex];
                                    port.Close();
                                    port.PortName = portname[PortName.SelectedIndex];       //Initialize the serial port configuration, important settings
                                    port.BaudRate = 115200;
                                    port.DataBits = 8;
                                    port.StopBits = StopBits.One;
                                    port.Parity = Parity.None;
                                    try
                                    {
                                        port.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        thread_Flag = false;
                                        MessageBox.Show(ex.Message + "请检查后重新启动。");
                                        Close();
                                    }                       //Open serial port
                                    port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//Create a event handler for received data
                                    port.NewLine = ":";               //Send data, at last add a Carriage Return (\r) to show the end of command                                                                      
                                    TimeOut.Enabled = true;
                                    WriteComm(".LOGLEVEL 0", 15, OPENCOM);
                                }
                            }));
                        }
                        else if (TimeOut.Enabled == true) continue;
                        else
                        {
                            PortName.Invoke(new Action(() =>
                            {
                                COM_Port.Enabled = false;
                                foreach (string n in portname)
                                {
                                    if (n == current_com)
                                    {
                                        COM_Port.Enabled = true;
                                        tbx_status.Text = "等待开始";
                                    }
                                }
                            }));
                            if (COM_Port.Enabled == false)
                            {
                                serialIsOpen = false;
                                port.Close();
                                continue;
                            }
                            else Thread.Sleep(500);
                        }
                    }
                    catch { thread_Flag = false; Thread.CurrentThread.Abort(); }
                }
            });
            Thread thread = new Thread(threadStart);
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
            polynominal_p1p2.SelectedIndex = Properties.Settings.Default.p1p2_xuanze;
            polynominal_T1.SelectedIndex = Properties.Settings.Default.T1_xuanze;
            polynominal_T2.SelectedIndex = Properties.Settings.Default.T2_xuanze;
            CBX_MUX0.Checked = true;
            CBX_MUX1.Checked = true;
            CBX_MUX2.Checked = true;
            CBX_MUX3.Checked = true;
            CBX_MUX4.Checked = true;
            CBX_MUX5.Checked = true;
            CBX_MUX6.Checked = true;
            CBX_MUX7.Checked = true;
            Users_login users_Login = new Users_login();
            users_Login.Show();          
        }
        private void Administrator()
        { 
            group1.Enabled = true;
            btn_cdat_open.Enabled = true;
            btn_cdat_save.Enabled = true;
            tbx_PSPx_limit.Enabled = true;
            polynominal_p1p2.Enabled = true;
            polynominal_T1.Enabled = true;
            polynominal_T2.Enabled = true;
            btn_save_rdata.Enabled = true;
            btn_load_rdata.Enabled = true;
            btn_del_rdata.Enabled=true;
            btn_Sentslow.Enabled = true;
            btn_SentConf.Enabled = true;
            btn_DIAG.Enabled = true;
            btn_T.Enabled = true;
            button1.Enabled = true;
            btn_load_sfr.Enabled = true;
            btn_save_sfr.Enabled = true;
            btn_Write_NVM.Enabled = true;
            btn_READ_NVM.Enabled =true;
            btnCalibration.Enabled = true;
            btn_verify.Enabled = true;
            
            group2.Enabled = true;
            label32.Enabled = true;
            label33.Enabled = true;
            label34.Enabled = true;
            label35.Enabled = true;
            
            Slow_channel_Flag = true;
            Sentconf_Flag = true;
            Diagnosis_Flag = true;
            Temperature_Flag = true;
            Pressure_Flag = true;
        }
        private void production()
        {
            group1.Enabled = true;
            btn_cdat_open.Enabled = true;
            btn_cdat_save.Enabled = true;
            tbx_PSPx_limit.Enabled = true;
            polynominal_p1p2.Enabled = true;
            polynominal_T1.Enabled = true;
            polynominal_T2.Enabled = true;
            btn_save_rdata.Enabled = true;
            btn_load_rdata.Enabled = true;
            btn_del_rdata.Enabled = true;
            btn_Sentslow.Enabled = false;
            btn_SentConf.Enabled = false;
            btn_DIAG.Enabled = false;
            btn_T.Enabled = true;
            button1.Enabled = true;
            btn_load_sfr.Enabled = true;
            btn_save_sfr.Enabled = true;
            btn_Write_NVM.Enabled = true;
            btn_READ_NVM.Enabled = true;
            btnCalibration.Enabled = true;
            btn_verify.Enabled = true;
            
            group2.Enabled = true;
            label32.Enabled = true;
            label33.Enabled = true;
            label34.Enabled = true;
            label35.Enabled = true;
        }
        private void QC()
        {
            group1.Enabled = false;
            btn_cdat_open.Enabled = false;
            btn_cdat_save.Enabled = false;
            tbx_PSPx_limit.Enabled = false;
            polynominal_p1p2.Enabled = false;
            polynominal_T1.Enabled = false;
            polynominal_T2.Enabled = false;
            btn_save_rdata.Enabled = false;
            btn_load_rdata.Enabled = false;
            btn_del_rdata.Enabled = false;
            btn_Sentslow.Enabled = true;
            btn_SentConf.Enabled = true;
            btn_DIAG.Enabled = true;
            btn_T.Enabled = true;
            button1.Enabled = true;
            btn_load_sfr.Enabled = false;
            btn_save_sfr.Enabled = false;
            btn_Write_NVM.Enabled = false;
            btn_READ_NVM.Enabled = true;
            btnCalibration.Enabled = false;
            btn_verify.Enabled = true;
            group2.Enabled = false;
            toolTip1.SetToolTip(btn_Sentslow, "当前用户只可查看。");
            toolTip1.SetToolTip(btn_SentConf, "当前用户只可查看。");
            toolTip1.SetToolTip(btn_DIAG, "当前用户只可查看。");
            toolTip1.SetToolTip(btn_T, "当前用户只可查看。");
            toolTip1.SetToolTip(button1, "当前用户只可查看。");

        }
        private void WriteComm(string strComm, int reLength, WorkRun fun)       //写命令
        {       //write specific command to SIO
            try
            {
                port.Write(strComm + "\r");             // send command with carriage return (\r) to indicate end of command
                reLen = reLength;
                workRun = fun;                          //set corresponding callback function 标记发出的是哪个命令，方便接收到的数据对接
            }
            catch (Exception ex)
            {                                   // handle errors
                MessageBox.Show(ex.Message);
            }
        }
        bool write_flag = false;
        bool SETMUX_Flag = false;

        private bool OPENCOM(byte[] strRet)         //ENACONF     #ENACONF+06 : //15
        {
            string str = Encoding.Default.GetString(strRet);                //encoding received bytes to ASCII

            if (str.Contains("#.LOGLEVEL"))
            {
                current_com = port.PortName;
                serialIsOpen = true;
                WriteComm("SETMUX 00", 16, OPENCOM);
                return true;

            }
            else if (str.Contains("#SETMUX 00") && (SETMUX_Flag == false))
            {
                SETMUX_Flag = true;
                WriteComm("SETMUX 10", 50, OPENCOM);
                return false;
            }
            else if (str.Contains("#SETMUX 10"))
            {
                WriteComm("SETMUX 00", 16, OPENCOM);
                return false;
            }
            else if (str.Contains("#SETMUX 00") && (SETMUX_Flag == true))
            {
                SETMUX_Flag = false;
                WriteComm("ENACONF", 15, OPENCOM);
                return false;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("POWEROFF", 15, OPENCOM);//9
                return true;
            }
            else if (str.Contains("#POWEROFF"))
            {
                tbx_status.Text = "等待开始";
                return false;
            }
            else
            {
                PortName.SelectedIndex++;
                if (PortName.SelectedIndex > PortName.Items.Count - 1)
                    PortName.SelectedIndex = 0;
                serialIsOpen = false;
                return false;
            }
        }
        private bool Enaconf(byte[] strRet)         //ENACONF     #ENACONF+06 : //15
        {       // establish connection, get device information and read EEPROM
            string str = Encoding.Default.GetString(strRet);                //encoding received bytes to ASCII

            if (str.Contains("#POWEROFF"))
            {
                WriteComm("ENACONF", 9, Enaconf);
                return true;
            }        //device is shut down
            else if (str.Contains("#ENACONF"))
            {
                if (write_flag) MessageBox.Show("NVM已成功写入!", "Success");
                write_flag = false;
                return false;
            }
            else
            {          //handle errors
                return false;
            }
        }

        private bool check_DUT(byte[] strRet)
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#.LOGLEVEL"))
            {
                current_com = port.PortName;
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, check_DUT);
                return true;
            }
            else if (str.Contains("Neither"))
            {
                listview_status.Items[c].SubItems[1].Text = "通信失败";
                listview_status.Items[c].BackColor = Color.Red;

                cishu = Convert.ToUInt16(tbx_cishu.Text);
                if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                {
                    enabled_channel.SelectedIndex++;
                    WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, check_DUT);
                }
                else
                {
                    WriteComm("POWEROFF", 15, OPENCOM);     //全部产品采样完毕
                }
                return true;
            }
            else if (str.Contains("#SETMUX "))
            {
                WriteComm("ENACONF", 14, check_DUT);
                return true;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("CCP 2 ca cb 6", 38, check_DUT);
                return true;
            }

            else if (str.Contains("#CCP 02 ca"))
            {
                ID_sample = str.Substring(21, 11).Replace(" ", "");
                listview_status.Items[c].SubItems[1].Text = ID_sample;
                listview_status.Items[c].BackColor = Color.White;
                if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)    //更换下一个产品
                {
                    enabled_channel.SelectedIndex++;
                    WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, check_DUT);
                }
                else
                {
                    WriteComm("POWEROFF", 15, OPENCOM);     //全部产品采样完毕
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        UInt16 j = 0, addr = 0x00;
        string[] strCom1 = new string[0x80];
        string item1_ID_value = "";
        private bool WriteFullEEPROM(byte[] strRet)         //写入NVM数据
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#CCP 05"))       //开始写NVM
            {
                if (j < 0x75)
                {
                    j++;
                    WriteComm(strCom1[j], 32, WriteFullEEPROM);//32
                }
                else
                {
                    if (enabled_channel.SelectedIndex < enabled_channel.Items.Count - 1)
                    {
                        enabled_channel.SelectedIndex++;
                        if (cabliration_falg)
                        {
                            NVM_code[0x4D] = Calibration_code[enabled_channel.SelectedIndex, 7];
                            NVM_code[0x4E] = Calibration_code[enabled_channel.SelectedIndex, 8];

                            if (ckb_calibrate_P1.Checked)
                            {
                                NVM_code[0x48] = Calibration_code[enabled_channel.SelectedIndex, 2];
                                NVM_code[0x49] = Calibration_code[enabled_channel.SelectedIndex, 3];
                                for (int y = 0; y < 12; y++)
                                {
                                    NVM_code[0x50 + y] = Calibration_code[enabled_channel.SelectedIndex, y + 10];
                                }
                            }
                            if (ckb_calibrate_P2.Checked)
                            {
                                NVM_code[0x4A] = Calibration_code[enabled_channel.SelectedIndex, 4];
                                NVM_code[0x4B] = Calibration_code[enabled_channel.SelectedIndex, 5];
                                for (int y = 0; y < 12; y++)
                                {
                                    NVM_code[0x5C + y] = Calibration_code[enabled_channel.SelectedIndex, y + 22];
                                }
                            }
                            if (ckb_calibrate_T1.Checked)
                            {
                                NVM_code[0x46] = Calibration_code[enabled_channel.SelectedIndex, 0];
                                NVM_code[0x47] = Calibration_code[enabled_channel.SelectedIndex, 1];
                                for (int y = 0; y < 5; y++)
                                {
                                    NVM_code[0x69 + y] = Calibration_code[enabled_channel.SelectedIndex, y + 34];
                                }
                            }
                            if (ckb_calibrate_T2.Checked)
                            {
                                for (int y = 0; y < 5; y++)
                                {
                                    NVM_code[0x6F + y] = Calibration_code[enabled_channel.SelectedIndex, y + 39];
                                }
                            }
                            CRC2();
                            string strCom;
                            UInt16[] st = new UInt16[5];
                            UInt16 nCrc;
                            int iAddr;
                            for (j = 0x46; j <= 0x75; j++)
                            {
                                st[0] = 0x9D;
                                st[1] = j;
                                st[2] = Convert.ToUInt16(NVM_code[j].Substring(0, 2), 16);
                                st[3] = Convert.ToUInt16(NVM_code[j].Substring(2, 2), 16);
                                nCrc = 0xFF;
                                for (iAddr = 0; iAddr < 4; iAddr++)
                                {
                                    nCrc = CRC_calc(st[iAddr], nCrc);
                                }
                                st[4] = nCrc;
                                strCom = j.ToString("X2") + " " + NVM_code[j].Substring(0, 2) + " " + NVM_code[j].Substring(2, 2)
                                    + " " + st[4].ToString("X2") + " 1";
                                strCom = "CCP 5 9d " + strCom.ToLower();
                                strCom1[j] = strCom;
                            }
                        }
                        j = 0;
                        WriteComm("POWEROFF", 15, WriteFullEEPROM);
                    }
                    else
                    {

                        WriteComm("POWEROFF", 15, OPENCOM);
                    }
                    listview_status.Items[c].SubItems[2].Text = "写入成功";
                    j = 0;
                }
                return true;
            }
            else if (str.Contains("#POWEROFF"))
            {
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, WriteFullEEPROM);
                tbx_status.Text = " " + c.ToString("D") + " -- 正在写入NVM数据";
                //标定数据添加在这里

                return true;
            }
            else if (str.Contains("#SETMUX "))
            {
                WriteComm("ENACONF", 15, WriteFullEEPROM);
                return true;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("CCP 2 da db 6", 38, WriteFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 da"))
            {
                WriteComm("CCP 2 ca cb 6", 38, WriteFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 ca"))
            {
                item1_ID_value = str.Substring(21, 11).Replace(" ", "");
                listview_status.Items[c].SubItems[1].Text = item1_ID_value;
                WriteComm("CCP 2 ea eb 4", 32, WriteFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 ea"))
            {
                WriteComm(strCom1[0], 28, WriteFullEEPROM);
                return true;
            }

            else
            {                                               // handle errors
                MessageBox.Show("数据通信失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ReadFullEEPROM(byte[] strRet)         //写入NVM数据
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#CCP 03"))       //开始写NVM
            {
                NVM_code[j] = str.Substring(24, 5).Remove(2, 1);

                if (j < 0x7F)
                {
                    j++;
                    WriteComm(strCom1[j], 35, ReadFullEEPROM);//32
                }
                else
                {
                    WriteComm("POWEROFF", 15, OPENCOM);
                    listview_status.Items[c].SubItems[2].Text = "读取成功";
                    j = 0;
                    listView1.Items.Clear();
                    for (int j = 0; j <= 0x7F; j++)
                        listView1.Items.Add(NVM_code[j]);
                }
                return true;
            }
            else if (str.Contains("#POWEROFF"))
            {
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, ReadFullEEPROM);
                tbx_status.Text = " " + c.ToString("D") + " -- 正在读取NVM数据";
                return true;
            }
            else if (str.Contains("#SETMUX "))
            {
                WriteComm("ENACONF", 14, ReadFullEEPROM);
                return true;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("CCP 2 da db 6", 20, ReadFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 da"))
            {
                WriteComm("CCP 2 ca cb 6", 38, ReadFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 ca"))
            {
                item1_ID_value = str.Substring(21, 11).Replace(" ", "");
                listview_status.Items[c].SubItems[1].Text = item1_ID_value;
                WriteComm("CCP 2 ea eb 4", 20, ReadFullEEPROM);
                return true;
            }
            else if (str.Contains("#CCP 02 ea"))
            {
                WriteComm(strCom1[0], 35, ReadFullEEPROM);
                return true;
            }

            else
            {                                               // handle errors
                MessageBox.Show("数据通信失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool Sample(byte[] strRet)         //写入NVM数据
        {
            string str = Encoding.Default.GetString(strRet);
            int c = Convert.ToInt16(enabled_channel.SelectedItem.ToString(), 16);
            if (str.Contains("#POWEROFF"))
            {
                WriteComm("SETMUX " + enabled_channel.SelectedItem, 15, Sample);
                tbx_status.Text = " " + c.ToString("D") + " -- 正在采样";
                return true;
            }
            else if (str.Contains("Neither"))
            {
                listview_status.Items[c].SubItems[2].Text = "通信失败";
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
                WriteComm("ENACONF", 14, Sample);
                return true;
            }
            else if (str.Contains("#ENACONF"))
            {
                WriteComm("CCP 2 ca cb 6", 38, Sample);
                return true;
            }

            else if (str.Contains("#CCP 02 ca"))
            {
                ID_sample = str.Substring(21, 11).Replace(" ", "");
                listview_status.Items[c].SubItems[1].Text = ID_sample;
                WriteComm("CCP 3 93 4c e0 4", 35, Sample);
                return true;
            }
            else if (str.Contains("#CCP 03 93 4c e0"))
            {
                PSP1_DAC = str.Substring(27, 2);
                PSP2_DAC = str.Substring(24, 2);
                string strCom;
                UInt16[] st = new UInt16[4];
                UInt16 nCrc;
                int iAddr;
                st[0] = 0xB4;
                st[1] = 0x00;
                st[2] = Convert.ToUInt16(PSP1_DAC, 16);
                nCrc = 0xFF;
                for (iAddr = 0; iAddr < 3; iAddr++)
                {
                    nCrc = CRC_calc(st[iAddr], nCrc);
                }
                st[3] = nCrc;
                strCom = "CCP 4 b4 00 " + PSP1_DAC + " " + st[3].ToString("X2").ToLower() + " 6";
                WriteComm(strCom, 40, Sample);
                return true;
            }
            else if (str.Contains("#CCP 04 b4 00"))
            {
                PSP1_sample = str.Substring(27, 5).Remove(2, 1);
                string strCom;
                UInt16[] st = new UInt16[4];
                UInt16 nCrc;
                int iAddr;
                st[0] = 0xB4;
                st[1] = 0x01;
                st[2] = Convert.ToUInt16(PSP2_DAC, 16);
                nCrc = 0xFF;
                for (iAddr = 0; iAddr < 3; iAddr++)
                {
                    nCrc = CRC_calc(st[iAddr], nCrc);
                }
                st[3] = nCrc;
                strCom = "CCP 4 b4 01 " + PSP2_DAC + " " + st[3].ToString("X2").ToLower() + " 6";
                WriteComm(strCom, 40, Sample);
                return true;
            }
            else if (str.Contains("#CCP 04 b4 01"))
            {
                PSP2_sample = str.Substring(27, 5).Remove(2, 1);
                WriteComm("CCP 3 c3 03 c7 6", 40, Sample);
                return true;
            }
            else if (str.Contains("#CCP 03 c3 03 c7"))
            {
                P1_sample_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                P2_sample_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
                WriteComm("CCP 3 cb 03 cf 6", 40, Sample);
                return true;
            }

            else if (str.Contains("#CCP 03 cb 03 cf"))
            {
                T1_sample_sum += Convert.ToUInt32(str.Substring(24, 5).Remove(2, 1), 16);
                T2_sample_sum += Convert.ToUInt32(str.Substring(30, 5).Remove(2, 1), 16);
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
                    listview_status.Items[c].SubItems[2].Text = "采样完成";
                    ListViewItem item1 = new ListViewItem();
                    uint n = Convert.ToUInt32(tbx_cishu.Text);
                    uint a = Convert.ToUInt32(PSP1_sample, 16);
                    uint b = Convert.ToUInt32(PSP2_sample, 16);
                    P1_sample_sum /= n;
                    P2_sample_sum /= n;
                    T1_sample_sum /= n;
                    T2_sample_sum /= n;
                    item1.SubItems.Add(c.ToString());
                    item1.SubItems.Add(ID_sample);
                    switch (Sample_selected)
                    {
                        case 0:
                            item1.SubItems.Add(tbx_qiya1.Text);
                            item1.SubItems.Add(tbx_qiya1.Text);
                            item1.SubItems.Add(tbx_diwen.Text);
                            item1.SubItems.Add(tbx_diwen.Text);
                            break;
                        case 1:
                            item1.SubItems.Add(tbx_qiya2.Text);
                            item1.SubItems.Add(tbx_qiya2.Text);
                            item1.SubItems.Add(tbx_changwen.Text);
                            item1.SubItems.Add(tbx_changwen.Text);
                            break;
                        case 2:
                            item1.SubItems.Add(tbx_qiya3.Text);
                            item1.SubItems.Add(tbx_qiya3.Text);
                            item1.SubItems.Add(tbx_gaowen.Text);
                            item1.SubItems.Add(tbx_gaowen.Text);
                            break;
                        default: break;
                    }
                    item1.SubItems.Add(P1_sample_sum.ToString());
                    item1.SubItems.Add(P2_sample_sum.ToString());
                    item1.SubItems.Add(T1_sample_sum.ToString());
                    item1.SubItems.Add(T2_sample_sum.ToString());
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add(a.ToString());
                    item1.SubItems.Add(b.ToString());
                    item1.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                    item1.SubItems.Add(DateTime.Now.ToString("hh:mm:ss"));
                    listView_sample.Items.Add(item1);
                    listView_sample.Items[listView_sample.Items.Count - 1].EnsureVisible();
                    P1_sample_sum = 0;
                    P2_sample_sum = 0;
                    T1_sample_sum = 0;
                    T2_sample_sum = 0;
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
        //CRC
        public static UInt16 CRC_calc(UInt16 data, UInt16 sig)
        {           //calculate CRC checksum
            UInt16 bit15 = (UInt16)((sig >> 15) & 1);
            UInt16 bit14 = (UInt16)((sig >> 14) & 1);
            UInt16 bit12 = (UInt16)((sig >> 12) & 1);
            UInt16 bit3 = (UInt16)((sig >> 3) & 1);
            UInt16 fb = (UInt16)(bit15 ^ bit14 ^ bit12 ^ bit3);
            sig = (UInt16)(((sig << 1) ^ data ^ fb) & 0xFFFF);
            return sig;
        }

        public static void CRC1()
        {
            for (int i = 0; i < 9; i++)
            {
                NVM_code1[i] = Convert.ToUInt16(NVM_code[i], 16);
            }
            UInt16 nCrc;
            int iAddr;

            nCrc = 0xFFFF;
            for (iAddr = 0; iAddr < 9; iAddr++)
            {
                nCrc = CRC_calc(NVM_code1[iAddr], nCrc);
            }
            NVM_code1[0x09] = nCrc;
            NVM_code[0x09] = NVM_code1[0x09].ToString("X4");

        }
        public static void CRC2()
        {
            for (int i = 0x0A; i < 0x75; i++)
            {
                NVM_code1[i] = Convert.ToUInt16(NVM_code[i], 16);
            }
            UInt16 nCrc;
            int iAddr;

            nCrc = 0xFFFF;
            for (iAddr = 0x0A; iAddr < 0x75; iAddr++)
            {
                nCrc = CRC_calc(NVM_code1[iAddr], nCrc);
            }
            NVM_code1[0x75] = nCrc;
            NVM_code[0x75] = NVM_code1[0x75].ToString("X4");

        }
        private void CRC3()
        {
            for (int i = 0x76; i < 0x7F; i++)
            {
                NVM_code1[i] = Convert.ToUInt16(NVM_code[i], 16);
            }
            UInt16 nCrc;
            int iAddr;

            nCrc = 0xFFFF;
            for (iAddr = 0x76; iAddr < 0x7F; iAddr++)
            {
                nCrc = CRC_calc(NVM_code1[iAddr], nCrc);
            }
            NVM_code1[0x7F] = nCrc;
            NVM_code[0x7F] = NVM_code1[0x7F].ToString("X4");
            //listView1.Items.Clear();
            //for (int j = 0; j <= 0x7F; j++)
            //    listView1.Items.Add(NVM_code[j]);
        }

        // diode
        double[] aAdcDiode = new double[30]; //温度*压力，最多采9次
        double[] aSentDiode = new double[30];
        double[] aAdcDiode2 = new double[30]; //温度*压力，最多采9次
        double[] aSentDiode2 = new double[30];

        double[] aAdcP2 = new double[30];
        double[] aSentP2 = new double[30];

        // NTC
        //double[] aAdcNTC = new double[30];
        //double[] aSentNTC = new double[30];

        // pressure sensor
        double[] aAdcP = new double[30];
        double[] aSentP = new double[30];
        //double[] aSentT = new double[30];
        int T1_jie = 0x13;
        int T2_jie = 0x13;
        int P1P2_jie = 0x33;

        private double SENT2T(double dSent)
        {
            double a, b;
            a = (Convert.ToDouble(tbx_T1_Y2.Text) - Convert.ToDouble(tbx_T1_Y1.Text)) / (Convert.ToDouble(tbx_T1_X2.Text) - Convert.ToDouble(tbx_T1_X1.Text));
            b = Convert.ToDouble(tbx_T1_Y2.Text) - a * Convert.ToDouble(tbx_T1_X2.Text);
            return (dSent - b) / a;
        }
        private double SENT2T2(double dSent)
        {
            double a, b;
            a = (Convert.ToDouble(tbx_T2_Y2.Text) - Convert.ToDouble(tbx_T2_Y1.Text)) / (Convert.ToDouble(tbx_T2_X2.Text) - Convert.ToDouble(tbx_T2_X1.Text));
            b = Convert.ToDouble(tbx_T2_Y2.Text) - a * Convert.ToDouble(tbx_T2_X2.Text);
            return (dSent - b) / a;
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            if (enabled_channel.Items.Count == 0)
            {
                MessageBox.Show("请至少选择一个产品", "缺少目标"); return;
            }
            int iCnt = 0, iIdx, temp_count = 0;
            string st = "";
            int[] xuhao_jihe = new int[30];
            double a, b, c, d, ee, f, g, h;
            int T1_mode = Convert.ToInt32(NVM_code[0x68]) & 3;
            int T2_mode = Convert.ToInt32(NVM_code[0x6E]) & 3;
            bool T1_diode_Flag, T2_diode_Flag, T1_TSEN_Flag, T2_TSEN_Flag;
            ushort changwen_xu;
            double[] wendu = new double[4];
            int[] PSP1_Value = new int[30];
            int[] PSP2_Value = new int[30];
            double PSP1_changwen, PSP2_changwen;
            ArrayList current_IDs = new ArrayList();
            for (int i = 0; i < enabled_channel.Items.Count; i++)
            {
                ushort xuhao = Convert.ToUInt16(enabled_channel.Items[i].ToString(), 16);
                current_IDs.Add(listview_status.Items[xuhao].SubItems[1].Text);
            }
            #region **各种异常检查**           
            for (int i = 0; i < listView_sample.Items.Count; i++)
            {
                if (current_IDs[0].ToString() == listView_sample.Items[i].SubItems[2].Text)
                {
                    iCnt++;
                    if (st != listView_sample.Items[i].SubItems[5].Text)
                    {
                        st = listView_sample.Items[i].SubItems[5].Text;
                        wendu[temp_count] = Convert.ToDouble(st);
                        temp_count++;
                    }
                }
            }
            //判断常温位置
            if ((wendu[0] < wendu[1]) && (wendu[0] > wendu[2])) changwen_xu = 0;
            else if ((wendu[0] < wendu[2]) && (wendu[0] > wendu[1])) changwen_xu = 0;
            else if ((wendu[1] < wendu[2]) && (wendu[1] > wendu[0])) changwen_xu = 1;
            else if ((wendu[1] < wendu[0]) && (wendu[1] > wendu[2])) changwen_xu = 1;
            else if ((wendu[2] < wendu[1]) && (wendu[2] > wendu[0])) changwen_xu = 2;
            else if ((wendu[2] < wendu[0]) && (wendu[2] > wendu[1])) changwen_xu = 2;
            else changwen_xu = 0;

            if (iCnt < 2) return;
            //检查采样数是否符合标定阶数要求
            if (ckb_calibrate_T1.Checked)
            {
                switch (T1_jie)
                {
                    case 0x11: break;
                    case 0x13: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    case 0x17: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    case 0x1F: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    default: break;
                }
            }
            if (ckb_calibrate_T2.Checked)
            {
                switch (T2_jie)
                {
                    case 0x11: break;
                    case 0x13: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    case 0x17: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    case 0x1F: if (temp_count <= 1) { MessageBox.Show("只采集了1个温度,至少需要2个！", "错误提示"); return; } break;
                    default: break;
                }
            }
            if (ckb_calibrate_P1.Checked || ckb_calibrate_P2.Checked)
            {
                if (ckb_calibrate_T1.Checked == false)      //标定压力时（P1、P2至少一个），T1必须勾选
                {
                    MessageBox.Show("标定压力时，必须勾选T1！", "错误提示");
                    return;
                }
                switch (P1P2_jie)
                {
                    case 0x11: break;
                    case 0x33: if (iCnt < 4) { MessageBox.Show("只采集了" + iCnt.ToString() + "组压力信息，至少需要4组！", "错误提示"); return; } break;
                    case 0x77: if (iCnt < 6) { MessageBox.Show("只采集了" + iCnt.ToString() + "组压力信息，至少需要6组！", "错误提示"); return; } break;
                    case 0x111: if (iCnt < 3) { MessageBox.Show("只采集了" + iCnt.ToString() + "组压力信息，至少需要3组！", "错误提示"); return; } break;
                    case 0x377: if (iCnt < 9) { MessageBox.Show("只采集了" + iCnt.ToString() + "组压力信息，至少需要9组！", "错误提示"); return; } break;
                    case 0xFFF: if (iCnt < 12) { MessageBox.Show("只采集了" + iCnt.ToString() + "组压力信息，至少需要12组！", "错误提示"); return; } break;
                    default: break;
                }
            }
            iCnt = 0;
            #endregion
            //判断T1、T2模式

            switch (T1_mode)
            {
                case 0: T1_diode_Flag = true; T1_TSEN_Flag = false; break;
                case 1: T1_diode_Flag = false; T1_TSEN_Flag = true; break;
                case 2: T1_diode_Flag = false; T1_TSEN_Flag = true; break;
                default: T1_diode_Flag = false; T1_TSEN_Flag = false; break;
            }
            switch (T2_mode)
            {
                case 0: T2_diode_Flag = true; T2_TSEN_Flag = false; break;
                case 1: T2_diode_Flag = false; T2_TSEN_Flag = true; break;
                case 2: T2_diode_Flag = false; T2_TSEN_Flag = true; break;
                default: T2_diode_Flag = false; T2_TSEN_Flag = false; break;
            }
            a = (Convert.ToDouble(tbx_T1_Y2.Text) - Convert.ToDouble(tbx_T1_Y1.Text)) / (Convert.ToDouble(tbx_T1_X2.Text) - Convert.ToDouble(tbx_T1_X1.Text));
            b = Convert.ToDouble(tbx_T1_Y2.Text) - a * Convert.ToDouble(tbx_T1_X2.Text);
            c = (Convert.ToDouble(tbx_P1_Y2.Text) - Convert.ToDouble(tbx_P1_Y1.Text)) / (Convert.ToDouble(tbx_P1_X2.Text) - Convert.ToDouble(tbx_P1_X1.Text));
            d = Convert.ToDouble(tbx_P1_Y2.Text) - c * Convert.ToDouble(tbx_P1_X2.Text);

            ee = (Convert.ToDouble(tbx_T2_Y2.Text) - Convert.ToDouble(tbx_T2_Y1.Text)) / (Convert.ToDouble(tbx_T2_X2.Text) - Convert.ToDouble(tbx_T2_X1.Text));
            f = Convert.ToDouble(tbx_T2_Y2.Text) - ee * Convert.ToDouble(tbx_T2_X2.Text);
            g = (Convert.ToDouble(tbx_P2_Y2.Text) - Convert.ToDouble(tbx_P2_Y1.Text)) / (Convert.ToDouble(tbx_P2_X2.Text) - Convert.ToDouble(tbx_P2_X1.Text));
            h = Convert.ToDouble(tbx_P2_Y2.Text) - g * Convert.ToDouble(tbx_P2_X2.Text);
            enabled_channel.SelectedIndex = -1;
            for (int k = 0; k < current_IDs.Count; k++)
            {
                for (int i = 0; i < listView_sample.Items.Count; i++)
                {
                    try
                    {
                        if (current_IDs[k].ToString() == listView_sample.Items[i].SubItems[2].Text)
                        {
                            xuhao_jihe[iCnt] = i;
                            aAdcDiode[iCnt] = Convert.ToDouble(listView_sample.Items[i].SubItems[9].Text) / 65536; //采集该终端所有温度ADC (* 0.0000169 - 0.2134)
                            aSentDiode[iCnt] = a * Convert.ToDouble(listView_sample.Items[i].SubItems[5].Text) + b;   //采集该终端所有温度sent
                            aAdcDiode2[iCnt] = Convert.ToDouble(listView_sample.Items[i].SubItems[10].Text) / 65536; //采集该终端所有温度ADC
                            aSentDiode2[iCnt] = ee * Convert.ToDouble(listView_sample.Items[i].SubItems[6].Text) + f;

                            aAdcP[iCnt] = Convert.ToDouble(listView_sample.Items[i].SubItems[7].Text);          //P1
                            aSentP[iCnt] = c * Convert.ToDouble(listView_sample.Items[i].SubItems[3].Text) + d;
                            aAdcP2[iCnt] = Convert.ToDouble(listView_sample.Items[i].SubItems[8].Text);         //P2
                            aSentP2[iCnt] = g * Convert.ToDouble(listView_sample.Items[i].SubItems[4].Text) + h;
                            PSP1_Value[iCnt] = Convert.ToInt32(listView_sample.Items[i].SubItems[15].Text);
                            PSP2_Value[iCnt] = Convert.ToInt32(listView_sample.Items[i].SubItems[16].Text);
                            iCnt++;                         //一共采了几次，算上所有目标温度和温度采样值                           
                        }
                    }
                    catch { continue; }
                }
                if (iCnt == 0)
                {
                    MessageBox.Show("未找到ID:" + current_IDs[k].ToString() + "对应的采样数据！", "数据缺失");
                }
                PSP1_changwen = 0; PSP2_changwen = 0;
                for (int p = 0; p < 3; p++)
                {
                    PSP1_changwen += PSP1_Value[changwen_xu + p];
                    PSP2_changwen += PSP2_Value[changwen_xu + p];
                }
                PSP1_changwen /= 768; PSP2_changwen /= 768; // /3/65536*256算出均值并转换成8位
                double PSPx_limit = Convert.ToDouble(tbx_PSPx_limit.Text);
                PSPx_limit /= 100;
                int temp_high = (int)(PSP1_changwen * (1 + PSPx_limit));
                int temp_low = (int)(PSP1_changwen * (1 - PSPx_limit));
                Calibration_code[k, 7] = ((temp_high << 8) + temp_low).ToString("X4");
                temp_high = (int)(PSP2_changwen * (1 + PSPx_limit));
                temp_low = (int)(PSP2_changwen * (1 - PSPx_limit));
                Calibration_code[k, 8] = ((temp_high << 8) + temp_low).ToString("X4");
                unsafe
                {
                    int nDescr;
                    ushort nSelect, nVal, nBase;
                    double dVal;
                    double SENT2T_1, SENT2T_2, SENT2T_3;
                    SSP_CFG_INF0 xCfg;
                    SSP_CAL_INF0 xCal;
                    SSP_OBS_LIST xObs;

                    SSP_OpenLog("calibration.log", 10, 0);
                    nDescr = SSP_GetDescriptor(520, 47, 2, 1);

                    // initialize data structures
                    SSP_CFG_Init(&xCfg, nDescr);
                    SSP_CAL_Init(&xCal, nDescr);

                    //------------------------------------------------------------------------ 
                    // calibrate diode at T1
                    //------------------------------------------------------------------------ 
                    if (ckb_calibrate_T1.Checked && T1_diode_Flag)
                    {
                        // set T1 mode to internal diode
                        SSP_CFG_SetNvmByName(&xCfg, "T1_MODE", 0x0000); //Convert.ToUInt16(NVM_code[0x68],16)

                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T1_ADC", iIdx, aAdcDiode[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "T1_DSP", iIdx, aSentDiode[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "T1_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, T1_jie);

                        for (iIdx = 0; iIdx <= 4; iIdx++)
                        {
                            SSP_CFG_GetNvmByNameOffs(&xCfg, "T1_Q0", iIdx, &nVal);
                            Calibration_code[k, iIdx + 34] = nVal.ToString("X4");
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);

                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "T1_DSP", iIdx, &dVal);
                            SENT2T_1 = SENT2T(dVal * (1 << 12));
                            SENT2T_2 = SENT2T(aSentDiode[iIdx]);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[13].Text = (SENT2T_1 - SENT2T_2).ToString("F1");
                            if (iIdx == 1)
                            {
                                int x1 = Convert.ToInt32(listView_sample.Items[1].SubItems[9].Text);
                                int x2 = Convert.ToInt32(dVal * (1 << 12));
                                Calibration_code[k, 0] = x1.ToString("X4");
                                Calibration_code[k, 1] = x2.ToString("X4");
                            }
                        }
                        SSP_ObsFree(&xObs);
                    }
                    if (ckb_calibrate_T1.Checked && T1_TSEN_Flag)
                    {
                        // set T1 mode to TSEN
                        SSP_CFG_SetNvmByName(&xCfg, "T1_MODE", 0x001A);

                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T1_ADC", iIdx, aAdcDiode[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "T1_DSP", iIdx, aSentDiode[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "T1_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, T1_jie & 0xF);

                        for (iIdx = 0; iIdx <= 4; iIdx++)
                        {
                            SSP_CFG_GetNvmByNameOffs(&xCfg, "T1_Q0", iIdx, &nVal);
                            Calibration_code[k, iIdx + 34] = nVal.ToString("X4");
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);

                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "T1_DSP", iIdx, &dVal);
                            SENT2T_1 = SENT2T(dVal * (1 << 12));
                            SENT2T_2 = SENT2T(aSentDiode[iIdx]);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[13].Text = (SENT2T_1 - SENT2T_2).ToString("F1");
                        }
                        SSP_ObsFree(&xObs);
                    }
                    //------------------------------------------------------------------------ 
                    // calibrate diode at T2
                    //------------------------------------------------------------------------ 
                    if (ckb_calibrate_T2.Checked && T2_diode_Flag)
                    {
                        // set T2 mode to internal diode
                        SSP_CFG_SetNvmByName(&xCfg, "T2_MODE", 0x0000);

                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T2_ADC", iIdx, aAdcDiode2[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "T2_DSP", iIdx, aSentDiode2[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "T2_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, T2_jie);

                        // print calibration results
                        for (iIdx = 0; iIdx <= 4; iIdx++)
                        {
                            SSP_CFG_GetNvmByNameOffs(&xCfg, "T2_Q0", iIdx, &nVal);
                            Calibration_code[k, iIdx + 39] = nVal.ToString("X4");
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);

                        // print simulation results
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "T2_DSP", iIdx, &dVal);
                            SENT2T_1 = SENT2T2(dVal * (1 << 12));
                            SENT2T_2 = SENT2T2(aSentDiode2[iIdx]);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[14].Text = (SENT2T_1 - SENT2T_2).ToString("F1");
                        }
                        SSP_ObsFree(&xObs);
                    }
                    if (ckb_calibrate_T2.Checked && T2_TSEN_Flag)
                    {
                        // set T2 mode to TSEN
                        SSP_CFG_SetNvmByName(&xCfg, "T2_MODE", 0x001A);

                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T2_ADC", iIdx, aAdcDiode2[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "T2_DSP", iIdx, aSentDiode2[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "T2_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, T2_jie & 0xF);

                        // print calibration results
                        for (iIdx = 0; iIdx <= 4; iIdx++)
                        {
                            SSP_CFG_GetNvmByNameOffs(&xCfg, "T2_Q0", iIdx, &nVal);
                            Calibration_code[k, iIdx + 39] = nVal.ToString("X4");
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);

                        // print simulation results
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "T2_DSP", iIdx, &dVal);
                            SENT2T_1 = SENT2T2(dVal * (1 << 12));
                            SENT2T_2 = SENT2T2(aSentDiode2[iIdx]);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[14].Text = (SENT2T_1 - SENT2T_2).ToString("F1");
                        }
                        SSP_ObsFree(&xObs);
                    }
                    //------------------------------------------------------------------------ 
                    // calibrate pressure sensor (p1)
                    //------------------------------------------------------------------------ 
                    if (ckb_calibrate_P1.Checked)
                    {
                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T1_ADC", iIdx, aAdcDiode[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "P1_ADC", iIdx, aAdcP[iIdx] / (1 << 16));
                            SSP_ObsSetDataByName(&xObs, "P1_DSP", iIdx, aSentP[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "P1_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, P1P2_jie);
                        SSP_CFG_GetNvmIdx(&xCfg, "C1_00", &nBase);

                        for (iIdx = 0; iIdx < 12; iIdx++)
                        {
                            SSP_CFG_GetNvmByIdx(&xCfg, nBase + iIdx, &nVal);
                            Calibration_code[k, iIdx + 10] = nVal.ToString("X4");    //标定值记录下来
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "P1_DSP", iIdx, &dVal);
                            SENT2T_3 = dVal * (1 << 12);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[11].Text = ((SENT2T_3 - aSentP[iIdx]) / c).ToString("F2");
                            if (iIdx == 1)
                            {
                                int z1 = Convert.ToInt32(listView_sample.Items[1].SubItems[7].Text);
                                int z2 = Convert.ToInt32(SENT2T_3);
                                Calibration_code[k, 2] = z1.ToString("X4");
                                Calibration_code[k, 3] = z2.ToString("X4");
                            }
                        }
                        SSP_ObsFree(&xObs);
                    }
                    //------------------------------------------------------------------------ 
                    // calibrate pressure sensor (p2)
                    //------------------------------------------------------------------------ 
                    if (ckb_calibrate_P2.Checked)
                    {
                        // observation list
                        SSP_ObsInit(&xObs, &xCal, iCnt);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsSetDataByName(&xObs, "T1_ADC", iIdx, aAdcDiode[iIdx]);
                            SSP_ObsSetDataByName(&xObs, "P2_ADC", iIdx, aAdcP2[iIdx] / (1 << 16));
                            SSP_ObsSetDataByName(&xObs, "P2_DSP", iIdx, aSentP2[iIdx] / (1 << 12));
                        }
                        // calibrate
                        SSP_CAL_GetObsType(&xCal, "P2_DSP", &nSelect);
                        SSP_CAL_Calibrate(&xCal, &xCfg, nSelect, &xObs, P1P2_jie);
                        SSP_CFG_GetNvmIdx(&xCfg, "C2_00", &nBase);

                        for (iIdx = 0; iIdx < 12; iIdx++)
                        {
                            SSP_CFG_GetNvmByIdx(&xCfg, nBase + iIdx, &nVal);
                            Calibration_code[k, iIdx + 22] = nVal.ToString("X4");    //标定值记录下来
                        }

                        // simulate
                        SSP_CAL_Simulate(&xCal, &xCfg, nSelect, &xObs);
                        for (iIdx = 0; iIdx < iCnt; iIdx++)
                        {
                            SSP_ObsGetDataByName(&xObs, "P2_DSP", iIdx, &dVal);
                            SENT2T_3 = dVal * (1 << 12);
                            listView_sample.Items[xuhao_jihe[iIdx]].SubItems[12].Text = ((SENT2T_3 - aSentP2[iIdx]) / c).ToString("F2");
                            if (iIdx == 1)
                            {
                                int z3 = Convert.ToInt32(listView_sample.Items[1].SubItems[8].Text);
                                int z4 = Convert.ToInt32(SENT2T_3);
                                Calibration_code[k, 4] = z3.ToString("X4");
                                Calibration_code[k, 5] = z4.ToString("X4");
                            }
                        }
                        SSP_ObsFree(&xObs);
                    }
                    // free data structures
                    SSP_CFG_Free(&xCfg);
                    SSP_CAL_Free(&xCal);

                    SSP_CloseLog();
                }
                iCnt = 0;

            }
            cabliration_falg = true;
            tbx_status.Text = "标定计算完成";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SENT_Slow_channel sENT_Slow_Channel = new SENT_Slow_channel();
            sENT_Slow_Channel.Show();
        }

        private void btn_SentConf_Click(object sender, EventArgs e)
        {
            SentConf sentConf = new SentConf();
            sentConf.Show();
        }

        private void btn_DIAG_Click(object sender, EventArgs e)
        {
            Diagnosis diag = new Diagnosis();
            diag.Show();
        }

        private void btn_T_Click(object sender, EventArgs e)
        {
            SENT_T SENT_T = new SENT_T();
            SENT_T.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Pressure pressure = new Pressure();
            pressure.Show();
        }

        private void btn_load_sfr_Click(object sender, EventArgs e)
        {
            openSfr.InitialDirectory = Properties.Settings.Default.Openpath;
            if (openSfr.ShowDialog() == DialogResult.OK)
            {
                import_model.Text = Path.GetFileNameWithoutExtension(openSfr.FileName);
                Properties.Settings.Default.Openpath = openSfr.InitialDirectory;
                FileStream myFileStream = new FileStream(openSfr.FileName, FileMode.Open, FileAccess.Read);
                StreamReader myStreamReader = new StreamReader(myFileStream);
                myStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                int i = 0;
                string line;
                while ((line = myStreamReader.ReadLine()) != null)
                {
                    NVM_code[i] = line.Substring(0, 4);
                    i++;
                }
                myStreamReader.Close();
                cabliration_falg = false;
                //listView1.Items.Clear();
                //for (int j = 0; j <= 0x7F; j++)
                //    listView1.Items.Add(NVM_code[j]);
                //MessageBox.Show("导入成功", "提示");
                tbx_status.Text = "sfr导入成功";
            }
        }

        private void btn_save_sfr_Click(object sender, EventArgs e)
        {
            saveSfr.InitialDirectory = Properties.Settings.Default.Openpath;
            if (saveSfr.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Openpath = saveSfr.FileName;
                FileStream myFileStream = new FileStream(saveSfr.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter myStreamWriter = new StreamWriter(myFileStream);
                myStreamWriter.Flush();
                #region **写入NVM数据**
                myStreamWriter.WriteLine(NVM_code[0] + " # 00  OEM[0]");
                myStreamWriter.WriteLine(NVM_code[1] + " # 01  OEM[1]");
                myStreamWriter.WriteLine(NVM_code[2] + " # 02  OEM[2]");
                myStreamWriter.WriteLine(NVM_code[3] + " # 03  OEM[3]");
                myStreamWriter.WriteLine(NVM_code[4] + " # 04  OEM[4]");
                myStreamWriter.WriteLine(NVM_code[5] + " # 05  OEM[5]");
                myStreamWriter.WriteLine(NVM_code[6] + " # 06  OEM[6]");
                myStreamWriter.WriteLine(NVM_code[7] + " # 07  OEM[7]");
                myStreamWriter.WriteLine(NVM_code[8] + " # 08  LOCK1");
                myStreamWriter.WriteLine(NVM_code[9] + " # 09  CRC1");
                myStreamWriter.WriteLine(NVM_code[0x0A] + " # 0A  MSG[0]    " + slow_ID_Value[0]);
                myStreamWriter.WriteLine(NVM_code[0x0B] + " # 0B  MSG[1]    " + slow_ID_Value[1]);
                myStreamWriter.WriteLine(NVM_code[0x0C] + " # 0C  MSG[2]    " + slow_ID_Value[2]);
                myStreamWriter.WriteLine(NVM_code[0x0D] + " # 0D  MSG[3]    " + slow_ID_Value[3]);
                myStreamWriter.WriteLine(NVM_code[0x0E] + " # 0E  MSG[4]    " + slow_ID_Value[4]);
                myStreamWriter.WriteLine(NVM_code[0x0F] + " # 0F  MSG[5]    " + slow_ID_Value[5]);
                myStreamWriter.WriteLine(NVM_code[0x10] + " # 10  MSG[6]    " + slow_ID_Value[6]);
                myStreamWriter.WriteLine(NVM_code[0x11] + " # 11  MSG[7]    " + slow_ID_Value[7]);
                myStreamWriter.WriteLine(NVM_code[0x12] + " # 12  MSG[8]    " + slow_ID_Value[8]);
                myStreamWriter.WriteLine(NVM_code[0x13] + " # 13  MSG[9]    " + slow_ID_Value[9]);
                myStreamWriter.WriteLine(NVM_code[0x14] + " # 14  MSG[10]   " + slow_ID_Value[10]);
                myStreamWriter.WriteLine(NVM_code[0x15] + " # 15  MSG[11]   " + slow_ID_Value[11]);
                myStreamWriter.WriteLine(NVM_code[0x16] + " # 16  MSG[12]   " + slow_ID_Value[12]);
                myStreamWriter.WriteLine(NVM_code[0x17] + " # 17  MSG[13]   " + slow_ID_Value[13]);
                myStreamWriter.WriteLine(NVM_code[0x18] + " # 18  MSG[14]   " + slow_ID_Value[14]);
                myStreamWriter.WriteLine(NVM_code[0x19] + " # 19  MSG[15]   " + slow_ID_Value[15]);
                myStreamWriter.WriteLine(NVM_code[0x1A] + " # 1A  MSG[16]   " + slow_ID_Value[16]);
                myStreamWriter.WriteLine(NVM_code[0x1B] + " # 1B  MSG[17]   " + slow_ID_Value[17]);
                myStreamWriter.WriteLine(NVM_code[0x1C] + " # 1C  MSG[18]   " + slow_ID_Value[18]);
                myStreamWriter.WriteLine(NVM_code[0x1D] + " # 1D  MSG[19]   " + slow_ID_Value[19]);
                myStreamWriter.WriteLine(NVM_code[0x1E] + " # 1E  MSG[20]   " + slow_ID_Value[20]);
                myStreamWriter.WriteLine(NVM_code[0x1F] + " # 1F  MSG[21]   " + slow_ID_Value[21]);
                myStreamWriter.WriteLine(NVM_code[0x20] + " # 20  MSG[22]   " + slow_ID_Value[22]);
                myStreamWriter.WriteLine(NVM_code[0x21] + " # 21  MSG[23]   " + slow_ID_Value[23]);
                myStreamWriter.WriteLine(NVM_code[0x22] + " # 22  MSG[24]   " + slow_ID_Value[24]);
                myStreamWriter.WriteLine(NVM_code[0x23] + " # 23  MSG[25]   " + slow_ID_Value[25]);
                myStreamWriter.WriteLine(NVM_code[0x24] + " # 24  MSG[26]   " + slow_ID_Value[26]);
                myStreamWriter.WriteLine(NVM_code[0x25] + " # 25  MSG[27]   " + slow_ID_Value[27]);
                myStreamWriter.WriteLine(NVM_code[0x26] + " # 26  MSG[28]   " + slow_ID_Value[28]);
                myStreamWriter.WriteLine(NVM_code[0x27] + " # 27  MSG[29]   " + slow_ID_Value[29]);
                myStreamWriter.WriteLine(NVM_code[0x28] + " # 28  MSG[30]   " + slow_ID_Value[30]);
                myStreamWriter.WriteLine(NVM_code[0x29] + " # 29  MSG[31]   " + slow_ID_Value[31]);
                myStreamWriter.WriteLine(NVM_code[0x2A] + " # 2A  MSG[32]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x2B] + " # 2B  MSG[33]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x2C] + " # 2C  MSG[34]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x2D] + " # 2D  MSG[35]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x2E] + " # 2E  MSG[36]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x2F] + " # 2F  MSG[37]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x30] + " # 30  MSG[38]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x31] + " # 31  MSG[39]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x32] + " # 32  MSG[40]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x33] + " # 33  MSG[41]   " + slow_ID_Value[32]);
                myStreamWriter.WriteLine(NVM_code[0x34] + " # 34  (free)");
                myStreamWriter.WriteLine(NVM_code[0x35] + " # 35  (free)");
                myStreamWriter.WriteLine(NVM_code[0x36] + " # 36  (free)");
                myStreamWriter.WriteLine(NVM_code[0x37] + " # 37  (free)");
                myStreamWriter.WriteLine(NVM_code[0x38] + " # 38  DOFFS1/DGAIN1");
                myStreamWriter.WriteLine(NVM_code[0x39] + " # 39  DOFFS2/DGAIN2");
                myStreamWriter.WriteLine(NVM_code[0x3A] + " # 3A  I2C_CTRL");
                myStreamWriter.WriteLine(NVM_code[0x3B] + " # 3B  POFFS1/PGAIN1");
                myStreamWriter.WriteLine(NVM_code[0x3C] + " # 3C  POFFS2/PGAIN2");
                myStreamWriter.WriteLine(NVM_code[0x3D] + " # 3D  SENTCONF2/1");
                myStreamWriter.WriteLine(NVM_code[0x3E] + " # 3E  SENTCONF4/3");
                myStreamWriter.WriteLine(NVM_code[0x3F] + " # 3F  ERR_EN2/1");
                myStreamWriter.WriteLine(NVM_code[0x40] + " # 40  ERR_EN4/3");
                myStreamWriter.WriteLine(NVM_code[0x41] + " # 41  P_DIFF_0");
                myStreamWriter.WriteLine(NVM_code[0x42] + " # 42  RT1_LIM");
                myStreamWriter.WriteLine(NVM_code[0x43] + " # 43  RT2_LIM");
                myStreamWriter.WriteLine(NVM_code[0x44] + " # 44  PV_LIM");
                myStreamWriter.WriteLine(NVM_code[0x45] + " # 45  TV_LIM");
                myStreamWriter.WriteLine(NVM_code[0x46] + " # 46  TNUM1_ADC");
                myStreamWriter.WriteLine(NVM_code[0x47] + " # 47  TNUM1_OUT");
                myStreamWriter.WriteLine(NVM_code[0x48] + " # 48  PNUM1_ADC");
                myStreamWriter.WriteLine(NVM_code[0x49] + " # 49  PNUM1_OUT");
                myStreamWriter.WriteLine(NVM_code[0x4A] + " # 4A  PNUM2_ADC");
                myStreamWriter.WriteLine(NVM_code[0x4B] + " # 4B  PNUM2_OUT");
                myStreamWriter.WriteLine(NVM_code[0x4C] + " # 4C  PSP_DAC");
                myStreamWriter.WriteLine(NVM_code[0x4D] + " # 4D  PSP1_LIM");
                myStreamWriter.WriteLine(NVM_code[0x4E] + " # 4E  PSP2_LIM");
                myStreamWriter.WriteLine(NVM_code[0x4F] + " # 4F  TSP_LIM");
                myStreamWriter.WriteLine(NVM_code[0x50] + " # 50  C1_00");
                myStreamWriter.WriteLine(NVM_code[0x51] + " # 51  C1_01");
                myStreamWriter.WriteLine(NVM_code[0x52] + " # 52  C1_02");
                myStreamWriter.WriteLine(NVM_code[0x53] + " # 53  C1_03");
                myStreamWriter.WriteLine(NVM_code[0x54] + " # 54  C1_10");
                myStreamWriter.WriteLine(NVM_code[0x55] + " # 55  C1_11");
                myStreamWriter.WriteLine(NVM_code[0x56] + " # 56  C1_12");
                myStreamWriter.WriteLine(NVM_code[0x57] + " # 57  C1_13");
                myStreamWriter.WriteLine(NVM_code[0x58] + " # 58  C1_20");
                myStreamWriter.WriteLine(NVM_code[0x59] + " # 59  C1_21");
                myStreamWriter.WriteLine(NVM_code[0x5A] + " # 5A  C1_22");
                myStreamWriter.WriteLine(NVM_code[0x5B] + " # 5B  C1_23");
                myStreamWriter.WriteLine(NVM_code[0x5C] + " # 5C  C2_00");
                myStreamWriter.WriteLine(NVM_code[0x5D] + " # 5D  C2_01");
                myStreamWriter.WriteLine(NVM_code[0x5E] + " # 5E  C2_02");
                myStreamWriter.WriteLine(NVM_code[0x5F] + " # 5F  C2_03");
                myStreamWriter.WriteLine(NVM_code[0x60] + " # 60  C2_10");
                myStreamWriter.WriteLine(NVM_code[0x61] + " # 61  C2_11");
                myStreamWriter.WriteLine(NVM_code[0x62] + " # 62  C2_12");
                myStreamWriter.WriteLine(NVM_code[0x63] + " # 63  C2_13");
                myStreamWriter.WriteLine(NVM_code[0x64] + " # 64  C2_20");
                myStreamWriter.WriteLine(NVM_code[0x65] + " # 65  C2_21");
                myStreamWriter.WriteLine(NVM_code[0x66] + " # 66  C2_22");
                myStreamWriter.WriteLine(NVM_code[0x67] + " # 67  C2_23");
                myStreamWriter.WriteLine(NVM_code[0x68] + " # 68  T1_MODE");
                myStreamWriter.WriteLine(NVM_code[0x69] + " # 69  T1_Q0");
                myStreamWriter.WriteLine(NVM_code[0x6A] + " # 6A  T1_Q1");
                myStreamWriter.WriteLine(NVM_code[0x6B] + " # 6B  T1_Q2");
                myStreamWriter.WriteLine(NVM_code[0x6C] + " # 6C  T1_Q3");
                myStreamWriter.WriteLine(NVM_code[0x6D] + " # 6D  T1_Q4");
                myStreamWriter.WriteLine(NVM_code[0x6E] + " # 6E  T2_MODE");
                myStreamWriter.WriteLine(NVM_code[0x6F] + " # 6F  T2_Q0");
                myStreamWriter.WriteLine(NVM_code[0x70] + " # 70  T2_Q1");
                myStreamWriter.WriteLine(NVM_code[0x71] + " # 71  T2_Q2");
                myStreamWriter.WriteLine(NVM_code[0x72] + " # 72  T2_Q3");
                myStreamWriter.WriteLine(NVM_code[0x73] + " # 73  T2_Q4");
                myStreamWriter.WriteLine(NVM_code[0x74] + " # 74  LOCK2");
                myStreamWriter.WriteLine(NVM_code[0x75] + " # 75  CRC2");
                myStreamWriter.WriteLine(NVM_code[0x76] + " # 76  ID1/0");
                myStreamWriter.WriteLine(NVM_code[0x77] + " # 77  ID3/2");
                myStreamWriter.WriteLine(NVM_code[0x78] + " # 78  A/F_TRIM");
                myStreamWriter.WriteLine(NVM_code[0x79] + " # 79  V_TRIM");
                myStreamWriter.WriteLine(NVM_code[0x7A] + " # 7A  M_TADC");
                myStreamWriter.WriteLine(NVM_code[0x7B] + " # 7B  N_TADC");
                myStreamWriter.WriteLine(NVM_code[0x7C] + " # 7C  OT_LIM");
                myStreamWriter.WriteLine(NVM_code[0x7D] + " # 7D  (free)");
                myStreamWriter.WriteLine(NVM_code[0x7E] + " # 7E  LOCK3");
                myStreamWriter.WriteLine(NVM_code[0x7F] + " # 7F  CRC3");
                #endregion
                myStreamWriter.Flush();
                myStreamWriter.Close();
                tbx_status.Text = "sfr导出成功";
            }
        }

        private void btn_cdat_save_Click(object sender, EventArgs e)
        {
            saveCdat.InitialDirectory = Properties.Settings.Default.Openpath;
            if (saveCdat.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Cdatpath = saveCdat.InitialDirectory;
                FileStream myFileStream = new FileStream(saveCdat.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter myStreamWriter = new StreamWriter(myFileStream);
                myStreamWriter.Flush();
                #region  ***保存标定参数***
                string p1p2 = "";
                switch (polynominal_p1p2.SelectedIndex)
                {
                    case 0: p1p2 = "1"; break;
                    case 1: p1p2 = "2"; break;
                    case 2: p1p2 = "3"; break;
                    case 3: p1p2 = "4"; break;
                    case 4: p1p2 = "5"; break;
                    case 5: p1p2 = "7"; break;
                    default:break;
                }
                myStreamWriter.WriteLine(" //--------------------------------------------");
                myStreamWriter.WriteLine("// E520.47标定参数  " + DateTime.Now.ToString("F"));
                myStreamWriter.WriteLine("//---------------------------------------------");
                if(ckb_calibrate_P1.Checked)
                {
                    myStreamWriter.Write("{0,10}", "1");
                    myStreamWriter.WriteLine(" // 标定P1");
                }
                else
                {
                    myStreamWriter.Write("{0,10}", "0");
                    myStreamWriter.WriteLine(" // 不标定P1");
                }
                myStreamWriter.Write("{0,10}", tbx_P1_X1.Text); myStreamWriter.WriteLine(" // " + "P1_X1[气压]");
                myStreamWriter.Write("{0,10}", tbx_P1_X2.Text); myStreamWriter.WriteLine(" // " + "P1_X2[气压]");
                myStreamWriter.Write("{0,10}", tbx_P1_Y1.Text); myStreamWriter.WriteLine(" // " + "P1_Y1[SENT]");
                myStreamWriter.Write("{0,10}", tbx_P1_Y2.Text); myStreamWriter.WriteLine(" // " + "P1_Y2[SENT]");
                myStreamWriter.Write("{0,10}", p1p2); myStreamWriter.WriteLine(" // " + polynominal_p1p2.SelectedItem.ToString());
                myStreamWriter.WriteLine("//");

                if (ckb_calibrate_P2.Checked)
                {
                    myStreamWriter.Write("{0,10}", "1");
                    myStreamWriter.WriteLine(" // 标定P2");
                }
                else
                {
                    myStreamWriter.Write("{0,10}", "0");
                    myStreamWriter.WriteLine(" // 不标定P2");
                }
                myStreamWriter.Write("{0,10}", tbx_P2_X1.Text); myStreamWriter.WriteLine(" // " + "P2_X1[气压]");
                myStreamWriter.Write("{0,10}", tbx_P2_X2.Text); myStreamWriter.WriteLine(" // " + "P2_X2[气压]");
                myStreamWriter.Write("{0,10}", tbx_P2_Y1.Text); myStreamWriter.WriteLine(" // " + "P2_Y1[SENT]");
                myStreamWriter.Write("{0,10}", tbx_P2_Y2.Text); myStreamWriter.WriteLine(" // " + "P2_Y2[SENT]");
                myStreamWriter.Write("{0,10}", p1p2); myStreamWriter.WriteLine(" // " + polynominal_p1p2.SelectedItem.ToString());
                myStreamWriter.WriteLine("//");

                if (ckb_calibrate_T1.Checked)
                {
                    myStreamWriter.Write("{0,10}", "1");
                    myStreamWriter.WriteLine(" // 标定T1");
                }
                else
                {
                    myStreamWriter.Write("{0,10}", "0");
                    myStreamWriter.WriteLine(" // 不标定T1");
                }
                myStreamWriter.Write("{0,10}", tbx_T1_X1.Text); myStreamWriter.WriteLine(" // " + "T1_X1[℃]");
                myStreamWriter.Write("{0,10}", tbx_T1_X2.Text); myStreamWriter.WriteLine(" // " + "T1_X2[℃]");
                myStreamWriter.Write("{0,10}", tbx_T1_Y1.Text); myStreamWriter.WriteLine(" // " + "T1_Y1[SENT]");
                myStreamWriter.Write("{0,10}", tbx_T1_Y2.Text); myStreamWriter.WriteLine(" // " + "T1_Y2[SENT]");
                myStreamWriter.Write("{0,10}", polynominal_T1.SelectedIndex.ToString());
                myStreamWriter.WriteLine(" // " + polynominal_T1.SelectedItem.ToString());
                myStreamWriter.WriteLine("//");

                if (ckb_calibrate_T2.Checked)
                {
                    myStreamWriter.Write("{0,10}", "1");
                    myStreamWriter.WriteLine(" // 标定T2");
                }
                else
                {
                    myStreamWriter.Write("{0,10}", "0");
                    myStreamWriter.WriteLine(" // 不标定T2");
                }
                myStreamWriter.Write("{0,10}", tbx_T2_X1.Text); myStreamWriter.WriteLine(" // " + "T2_X1[℃]");
                myStreamWriter.Write("{0,10}", tbx_T2_X2.Text); myStreamWriter.WriteLine(" // " + "T2_X2[℃]");
                myStreamWriter.Write("{0,10}", tbx_T2_Y1.Text); myStreamWriter.WriteLine(" // " + "T2_Y1[SENT]");
                myStreamWriter.Write("{0,10}", tbx_T2_Y2.Text); myStreamWriter.WriteLine(" // " + "T2_Y2[SENT]");
                myStreamWriter.Write("{0,10}", polynominal_T2.SelectedIndex.ToString());
                myStreamWriter.WriteLine(" // " + polynominal_T2.SelectedItem.ToString());
                myStreamWriter.WriteLine("//");

                myStreamWriter.Write("{0,10}", tbx_PSPx_limit.Text); myStreamWriter.WriteLine(" // PSPx 限制+-% FS");
                myStreamWriter.WriteLine("// 结束");
                #endregion
                myStreamWriter.Flush();
                myStreamWriter.Close();
                tbx_status.Text = "标定参数导出成功";
            }
        }
        private void btn_cdat_open_Click(object sender, EventArgs e)
        {
            openCdat.InitialDirectory = Properties.Settings.Default.Openpath;
            if(openCdat.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Cdatpath = openCdat.InitialDirectory;
                FileStream myFileStream = new FileStream(openCdat.FileName, FileMode.Open, FileAccess.Read);
                StreamReader myStreamReader = new StreamReader(myFileStream);
                myStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                #region  ***读取标定参数***
                string line;
                line = myStreamReader.ReadLine();
                line = myStreamReader.ReadLine();
                line = myStreamReader.ReadLine();
                //第1组
                line = myStreamReader.ReadLine();
                if (line.Substring(0, 10).Contains("0"))
                    ckb_calibrate_P1.Checked = false;
                else if (line.Substring(0, 10).Contains("1"))
                    ckb_calibrate_P1.Checked = true;
                line = myStreamReader.ReadLine();
                tbx_P1_X1.Text = line.Substring(0, 10).Replace(" ","");
                line = myStreamReader.ReadLine();
                tbx_P1_X2.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_P1_Y1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_P1_Y2.Text = line.Substring(0, 10).Replace(" ", "");
                //标定选择 P1/P2             
                line = myStreamReader.ReadLine().Substring(0, 10).Replace(" ", "");
                if (line.Contains("1"))
                {
                    polynominal_p1p2.SelectedIndex = 0;
                }
                else if (line.Contains("2"))
                {
                    polynominal_p1p2.SelectedIndex = 1;
                }
                else if (line.Contains("3"))
                {
                    polynominal_p1p2.SelectedIndex = 2;
                }
                else if (line.Contains("4"))
                {
                    polynominal_p1p2.SelectedIndex = 3;
                }
                else if (line.Contains("5"))
                {
                    polynominal_p1p2.SelectedIndex = 4;
                }
                else if (line.Contains("7"))
                {
                    polynominal_p1p2.SelectedIndex = 5;
                }
                line = myStreamReader.ReadLine();
                //第2组
                line = myStreamReader.ReadLine();
                if (line.Substring(0, 10).Contains("0"))
                    ckb_calibrate_P2.Checked = false;
                else if (line.Substring(0, 10).Contains("1"))
                    ckb_calibrate_P2.Checked = true;
                line = myStreamReader.ReadLine();
                tbx_P2_X1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_P2_X2.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_P2_Y1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_P2_Y2.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                line = myStreamReader.ReadLine();//忽略p1p2选择
                //第3组
                line = myStreamReader.ReadLine();
                if (line.Substring(0, 10).Contains("0"))
                    ckb_calibrate_T1.Checked = false;
                else if (line.Substring(0, 10).Contains("1"))
                    ckb_calibrate_T1.Checked = true;
                line = myStreamReader.ReadLine();
                tbx_T1_X1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T1_X2.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T1_Y1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T1_Y2.Text = line.Substring(0, 10).Replace(" ", "");
                //标定选择 T1
                line = myStreamReader.ReadLine().Substring(0, 10).Replace(" ", "");
                if (line.Contains("0"))
                {
                    polynominal_T1.SelectedIndex = 0;
                }
                else if (line.Contains("1"))
                {
                    polynominal_T1.SelectedIndex = 1;
                }
                else if (line.Contains("2"))
                {
                    polynominal_T1.SelectedIndex = 2;
                }
                else if (line.Contains("3"))
                {
                    polynominal_T1.SelectedIndex = 3;
                }
                else if (line.Contains("4"))
                {
                    polynominal_T1.SelectedIndex = 4;
                }
                line = myStreamReader.ReadLine();
                //第4组
                line = myStreamReader.ReadLine();
                if (line.Substring(0, 10).Contains("0"))
                    ckb_calibrate_T2.Checked = false;
                else if (line.Substring(0, 10).Contains("1"))
                    ckb_calibrate_T2.Checked = true;
                line = myStreamReader.ReadLine();
                tbx_T2_X1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T2_X2.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T2_Y1.Text = line.Substring(0, 10).Replace(" ", "");
                line = myStreamReader.ReadLine();
                tbx_T2_Y2.Text = line.Substring(0, 10).Replace(" ", "");
                //标定选择 T2
                line = myStreamReader.ReadLine().Substring(0, 10).Replace(" ", "");
                if (line.Contains("仅TADC值"))
                {
                    polynominal_T2.SelectedIndex = 0;
                }
                else if (line.Contains("T = 常数"))
                {
                    polynominal_T2.SelectedIndex = 1;
                }
                else if (line.Contains("一阶多项式"))
                {
                    polynominal_T2.SelectedIndex = 2;
                }
                else if (line.Contains("二阶多项式"))
                {
                    polynominal_T2.SelectedIndex = 3;
                }
                else if (line.Contains("三阶多项式"))
                {
                    polynominal_T2.SelectedIndex = 4;
                }
                line = myStreamReader.ReadLine();
                //限制+-% FS
                line = myStreamReader.ReadLine();
                tbx_PSPx_limit.Text = line.Substring(0, 10).Replace(" ", "");
                tbx_status.Text = "标定参数导入成功";
#endregion
                myStreamReader.Close();
            }
        }

        int sensor_xuanze;
        #region **传感器选择**
        private void CBX_MUX0_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX0.Checked == true)
            {
                enabled_channel.Items.Insert(0, "00");
                sensor_xuanze |= 1;
            }
            else { enabled_channel.Items.RemoveAt(0); sensor_xuanze &= ~1; }
        }

        private void CBX_MUX1_CheckedChanged(object sender, EventArgs e)
        {
            if (CBX_MUX1.Checked == true)
            {
                sensor_xuanze |= 1 << 1;
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
                sensor_xuanze &= ~(1 << 1);
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
                sensor_xuanze |= 1 << 2;
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
                sensor_xuanze &= ~(1 << 2);
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
                sensor_xuanze |= 1 << 3;
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
                sensor_xuanze &= ~(1 << 3);
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
                sensor_xuanze |= 1 << 4;
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
                sensor_xuanze &= ~(1 << 4);
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
                sensor_xuanze |= 1 << 5;
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
                sensor_xuanze &= ~(1 << 5);
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
                sensor_xuanze |= 1 << 6;
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
                sensor_xuanze &= ~(1 << 6);
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
                sensor_xuanze |= 1 << 7;
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
                sensor_xuanze &= ~(1 << 7);
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
                sensor_xuanze |= 1 << 8;
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
                sensor_xuanze &= ~(1 << 8);
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
                sensor_xuanze |= 1 << 9;
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
                sensor_xuanze &= ~(1 << 9);
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
                sensor_xuanze |= 1 << 10;
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
                sensor_xuanze &= ~(1 << 10);
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
                sensor_xuanze |= 1 << 11;
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
                sensor_xuanze &= ~(1 << 11);
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
                sensor_xuanze |= 1 << 12;
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
                sensor_xuanze &= ~(1 << 12);
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
                sensor_xuanze |= 1 << 13;
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
                sensor_xuanze &= ~(1 << 13);
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
                sensor_xuanze |= 1 << 14;
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
                sensor_xuanze &= ~(1 << 14);
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
                sensor_xuanze |= 1 << 15;
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
                sensor_xuanze &= ~(1 << 15);
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

        private void btn_Write_NVM_Click(object sender, EventArgs e)
        {
            if (enabled_channel.Items.Count == 0)
            {
                MessageBox.Show("请至少选择一个产品", "缺少目标"); return;
            }
            for (int i = 0; i < 16; i++)    //清除状态
            {
                listview_status.Items[i].BackColor = Color.White;
                listview_status.Items[i].SubItems[2].Text = "";
            }
            string strCom;
            UInt16[] st = new UInt16[5];
            UInt16 nCrc;
            int iAddr;

            enabled_channel.SelectedIndex = 0;
            if (cabliration_falg)
            {
                NVM_code[0x4D] = Calibration_code[enabled_channel.SelectedIndex, 7];
                NVM_code[0x4E] = Calibration_code[enabled_channel.SelectedIndex, 8];
                if (ckb_calibrate_P1.Checked)
                {
                    NVM_code[0x48] = Calibration_code[enabled_channel.SelectedIndex, 2];
                    NVM_code[0x49] = Calibration_code[enabled_channel.SelectedIndex, 3];
                    for (int y = 0; y < 12; y++)
                    {
                        NVM_code[0x50 + y] = Calibration_code[enabled_channel.SelectedIndex, y + 10];
                    }
                }
                if (ckb_calibrate_P2.Checked)
                {
                    NVM_code[0x4A] = Calibration_code[enabled_channel.SelectedIndex, 4];
                    NVM_code[0x4B] = Calibration_code[enabled_channel.SelectedIndex, 5];
                    for (int y = 0; y < 12; y++)
                    {
                        NVM_code[0x5C + y] = Calibration_code[enabled_channel.SelectedIndex, y + 22];
                    }
                }
                if (ckb_calibrate_T1.Checked)
                {
                    NVM_code[0x46] = Calibration_code[enabled_channel.SelectedIndex, 0];
                    NVM_code[0x47] = Calibration_code[enabled_channel.SelectedIndex, 1];
                    for (int y = 0; y < 5; y++)
                    {
                        NVM_code[0x69 + y] = Calibration_code[enabled_channel.SelectedIndex, y + 34];
                    }
                }
                if (ckb_calibrate_T2.Checked)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        NVM_code[0x6F + y] = Calibration_code[enabled_channel.SelectedIndex, y + 39];
                    }
                }
                CRC2();
            }
            for (j = 0; j <= 0x75; j++)
            {

                st[0] = 0x9D;
                st[1] = addr;
                st[2] = Convert.ToUInt16(NVM_code[j].Substring(0, 2), 16);
                st[3] = Convert.ToUInt16(NVM_code[j].Substring(2, 2), 16);
                nCrc = 0xFF;
                for (iAddr = 0; iAddr < 4; iAddr++)
                {
                    nCrc = CRC_calc(st[iAddr], nCrc);
                }
                st[4] = nCrc;
                strCom = "CCP 5 9d " + addr.ToString("X2") + " " + NVM_code[j].Substring(0, 2) + " " + NVM_code[j].Substring(2, 2)
                    + " " + st[4].ToString("X2") + " 1";
                strCom = strCom.ToLower();
                strCom1[j] = strCom;
                addr++;
            }
            j = 0;
            addr = 0;

            WriteComm("POWEROFF", 15, WriteFullEEPROM);   //15 30           
        }

        private void btn_READ_NVM_Click(object sender, EventArgs e)
        {
            if (enabled_channel.Items.Count == 0)
            {
                MessageBox.Show("请至少选择一个产品", "缺少目标"); return;
            }
            for (int i = 0; i < 16; i++)    //清除状态
            {
                listview_status.Items[i].BackColor = Color.White;
                listview_status.Items[i].SubItems[2].Text = "";
            }
            for (int i = 0; i < enabled_channel.Items.Count; i++)
            {
                enable_channel1[i] = "#" + enabled_channel.Items[i].ToString();
            }
            Select_channel select_Channel = new Select_channel();
            select_Channel.ShowDialog();
            enabled_channel.SelectedIndex = selected_channel;

            string strCom;
            int[] st = new int[3];
            for (j = 0; j <= 0x7F; j++)
            {
                st[0] = 0x93;
                st[1] = addr;
                st[2] = (st[0] + st[1] + 1) & 0xff;
                strCom = "CCP 3 93 " + addr.ToString("X2") + " " + st[2].ToString("X2") + " 4";
                strCom = strCom.ToLower();
                strCom1[j] = strCom;
                addr++;
            }
            j = 0;
            addr = 0;
            WriteComm("POWEROFF", 15, ReadFullEEPROM);
        }

        private void ckb_calibrate_P1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_calibrate_P1.Checked)
            {
                tbx_P1_X1.Enabled = true;
                tbx_P1_X2.Enabled = true;
                tbx_P1_Y1.Enabled = true;
                tbx_P1_Y2.Enabled = true;
            }
            else
            {
                tbx_P1_X1.Enabled = false;
                tbx_P1_X2.Enabled = false;
                tbx_P1_Y1.Enabled = false;
                tbx_P1_Y2.Enabled = false;
            }
        }

        private void ckb_calibrate_P2_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_calibrate_P2.Checked)
            {
                tbx_P2_X1.Enabled = true;
                tbx_P2_X2.Enabled = true;
                tbx_P2_Y1.Enabled = true;
                tbx_P2_Y2.Enabled = true;
            }
            else
            {
                tbx_P2_X1.Enabled = false;
                tbx_P2_X2.Enabled = false;
                tbx_P2_Y1.Enabled = false;
                tbx_P2_Y2.Enabled = false;
            }
        }

        private void ckb_calibrate_T1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_calibrate_T1.Checked)
            {
                tbx_T1_X1.Enabled = true;
                tbx_T1_X2.Enabled = true;
                tbx_T1_Y1.Enabled = true;
                tbx_T1_Y2.Enabled = true;
                polynominal_T1.Enabled = true;
            }
            else
            {
                tbx_T1_X1.Enabled = false;
                tbx_T1_X2.Enabled = false;
                tbx_T1_Y1.Enabled = false;
                tbx_T1_Y2.Enabled = false;
                polynominal_T1.Enabled = false;
            }
        }

        private void ckb_calibrate_T2_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_calibrate_T2.Checked)
            {
                tbx_T2_X1.Enabled = true;
                tbx_T2_X2.Enabled = true;
                tbx_T2_Y1.Enabled = true;
                tbx_T2_Y2.Enabled = true;
                polynominal_T2.Enabled = true;
            }
            else
            {
                tbx_T2_X1.Enabled = false;
                tbx_T2_X2.Enabled = false;
                tbx_T2_Y1.Enabled = false;
                tbx_T2_Y2.Enabled = false;
                polynominal_T2.Enabled = false;
            }
        }
        #region   **6个框采样**
        private void tbx_qiya1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_qiya1.Text == "") return;
            if (tbx_qiya1.Text == "-") return;
            double n;
            if (double.TryParse(tbx_qiya1.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }
        private void tbx_qiya2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_qiya2.Text == "") return;
            if (tbx_qiya2.Text == "-") return;
            double n;
            if (double.TryParse(tbx_qiya2.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }
        private void tbx_qiya3_TextChanged(object sender, EventArgs e)
        {
            if (tbx_qiya3.Text == "") return;
            if (tbx_qiya3.Text == "-") return;
            double n;
            if (double.TryParse(tbx_qiya3.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }
        private void tbx_diwen_TextChanged(object sender, EventArgs e)
        {
            if (tbx_diwen.Text == "-") return;
            if (tbx_diwen.Text == "") tbx_diwen.Text = "0";
            try
            {
                double a = Convert.ToDouble(tbx_diwen.Text);
                if (a > 120) { a = 120; tbx_diwen.Text = "120"; }
                else if (a < -40) { a = -40; tbx_diwen.Text = "-40"; }
            }
            catch { MessageBox.Show("请输入正确数字,范围-40~120", "格式错误"); }

        }
        private void tbx_changwen_TextChanged(object sender, EventArgs e)
        {
            if (tbx_changwen.Text == "-") return;
            if (tbx_changwen.Text == "") tbx_changwen.Text = "0";
            try
            {
                double a = Convert.ToDouble(tbx_changwen.Text);
                if (a > 120) { a = 120; tbx_changwen.Text = "120"; }
                else if (a < -40) { a = -40; tbx_changwen.Text = "-40"; }
            }
            catch { MessageBox.Show("请输入正确数字,范围-40~120", "格式错误"); }
        }
        private void tbx_gaowen_TextChanged(object sender, EventArgs e)
        {
            if (tbx_gaowen.Text == "-") return;
            if (tbx_gaowen.Text == "") tbx_gaowen.Text = "0";
            try
            {
                double a = Convert.ToDouble(tbx_gaowen.Text);
                if (a > 120) { a = 120; tbx_gaowen.Text = "120"; }
                else if (a < -40) { a = -40; tbx_gaowen.Text = "-40"; }
            }
            catch { MessageBox.Show("请输入正确数字,范围-40~120", "格式错误"); }
        }
        private void tbx_qiya1_Click(object sender, EventArgs e)
        {
            Sample_selected = 0;
            //tbx_qiya1.BackColor = Color.Green;
        }
        private void tbx_qiya2_Click(object sender, EventArgs e)
        {
            Sample_selected = 1;
            //tbx_qiya2.BackColor = Color.Green;
        }
        private void tbx_diwen_Click(object sender, EventArgs e)
        {
            Sample_selected = 0;
        }
        private void tbx_changwen_Click(object sender, EventArgs e)
        {
            Sample_selected = 1;
        }
        private void tbx_qiya3_Click(object sender, EventArgs e)
        {
            Sample_selected = 2;
        }
        private void tbx_gaowen_Click(object sender, EventArgs e)
        {
            Sample_selected = 2;
        }
        #endregion

        #region  **16个框标定**
        private void tbx_P1_X1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P1_X1.Text == "") return;
            if (tbx_P1_X1.Text == "-") return;
            double n;
            if (double.TryParse(tbx_P1_X1.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_P1_X2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P1_X2.Text == "") return;
            if (tbx_P1_X2.Text == "-") return;
            double n;
            if (double.TryParse(tbx_P1_X2.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_P1_Y1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P1_Y1.Text == "") tbx_P1_Y1.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_P1_Y1.Text);
                if (a > 65535) tbx_P1_Y1.Text = "65535";
                else if (a < 0) tbx_P1_Y1.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }
        private void tbx_P1_Y2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P1_Y2.Text == "") tbx_P1_Y2.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_P1_Y2.Text);
                if (a > 65535) tbx_P1_Y2.Text = "65535";
                else if (a < 0) tbx_P1_Y2.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }
        private void tbx_P2_X1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P2_X1.Text == "-") return;
            if (tbx_P2_X1.Text == "") return;
            double n;
            if (double.TryParse(tbx_P2_X1.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }

        }
        private void tbx_P2_X2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P2_X2.Text == "-") return;
            if (tbx_P2_X2.Text == "") return;
            double n;
            if (double.TryParse(tbx_P2_X2.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_P2_Y1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P2_Y1.Text == "") tbx_P2_Y1.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_P2_Y1.Text);
                if (a > 65535) tbx_P2_Y1.Text = "65535";
                else if (a < 0) tbx_P2_Y1.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }
        private void tbx_P2_Y2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_P2_Y2.Text == "") tbx_P2_Y2.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_P2_Y2.Text);
                if (a > 65535) tbx_P2_Y2.Text = "65535";
                else if (a < 0) tbx_P2_Y2.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }

        private void tbx_T1_X1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_X1.Text == "-") return;
            if (tbx_T1_X1.Text == "") return;
            double n;
            if (double.TryParse(tbx_T1_X1.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_T1_X2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_X2.Text == "-") return;
            if (tbx_T1_X2.Text == "") return;
            double n;
            if (double.TryParse(tbx_T1_X2.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_T1_Y1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_Y1.Text == "") tbx_T1_Y1.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_T1_Y1.Text);
                if (a > 65535) tbx_T1_Y1.Text = "65535";
                else if (a < 0) tbx_T1_Y1.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }

        private void tbx_T1_Y2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T1_Y2.Text == "") tbx_T1_Y2.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_T1_Y2.Text);
                if (a > 65535) tbx_T1_Y2.Text = "65535";
                else if (a < 0) tbx_T1_Y2.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }

        private void tbx_T2_X1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_X1.Text == "-") return;
            if (tbx_T2_X1.Text == "") return;
            double n;
            if (double.TryParse(tbx_T2_X1.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_T2_X2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_X2.Text == "-") return;
            if (tbx_T2_X2.Text == "") return;
            double n;
            if (double.TryParse(tbx_T2_X2.Text, out n)) return;
            else { MessageBox.Show("请输入数字", "格式错误"); }
        }

        private void tbx_T2_Y1_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_Y1.Text == "") tbx_T2_Y1.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_T2_Y1.Text);
                if (a > 65535) tbx_T2_Y1.Text = "65535";
                else if (a < 0) tbx_T2_Y1.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }

        private void polynominal_T1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.T1_xuanze = polynominal_T1.SelectedIndex;
            switch (polynominal_T1.SelectedIndex)       //缺少0
            {
                case 1: T1_jie = 0x11; break;
                case 2: T1_jie = 0x13; break;
                case 3: T1_jie = 0x17; break;
                case 4: T1_jie = 0x1F; break;
                default: break;
            }

        }

        private void polynominal_p1p2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (polynominal_p1p2.SelectedIndex)
            {
                case 0: P1P2_jie = 0x11; break;
                case 1: P1P2_jie = 0x33; break;
                case 2: P1P2_jie = 0x77; break;
                case 3: P1P2_jie = 0x111; break;
                case 4: P1P2_jie = 0x377; break;
                case 5: P1P2_jie = 0xFFF; break;
                default: break;
            }
            Properties.Settings.Default.p1p2_xuanze = polynominal_p1p2.SelectedIndex;
        }

        private void polynominal_T2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.T2_xuanze = polynominal_T2.SelectedIndex;
            switch (polynominal_T2.SelectedIndex)       //缺少0
            {
                case 1: T2_jie = 0x11; break;
                case 2: T2_jie = 0x13; break;
                case 3: T2_jie = 0x17; break;
                case 4: T2_jie = 0x1F; break;
                default: break;
            }
        }

        private void tbx_T2_Y2_TextChanged(object sender, EventArgs e)
        {
            if (tbx_T2_Y2.Text == "") tbx_T2_Y2.Text = "1";
            try
            {
                uint a = Convert.ToUInt32(tbx_T2_Y2.Text);
                if (a > 65535) tbx_T2_Y2.Text = "65535";
                else if (a < 0) tbx_T2_Y2.Text = "0";
            }
            catch { MessageBox.Show("请输入正确数字,范围0~65535", "格式错误"); }
        }
        #endregion

        private void btn_save_rdata_Click(object sender, EventArgs e)
        {
            if (listView_sample.Items.Count == 0)
            {
                MessageBox.Show("请先采样", "提示"); return;
            }
            SaveRdata.InitialDirectory = Properties.Settings.Default.Savepath;
            SaveRdata.FileName = DateTime.Now.ToString("yy.MM.dd") + "+" + import_model.Text + ".rdata";
            StreamWriter myStreamWriter;
            StreamReader myStreamReader;
            if (File.Exists(SaveRdata.InitialDirectory))
            {
                FileStream myFileStream = new FileStream(SaveRdata.InitialDirectory, FileMode.Open, FileAccess.ReadWrite);
                myStreamReader = new StreamReader(myFileStream);
                myStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);

                string line;
                while ((line = myStreamReader.ReadLine()) != null)
                {
                    if (line.Contains(listView_sample.Items[0].SubItems[2].Text))
                    {
                        MessageBox.Show("文件中已有对应ID数据，请勿重复保存。\n" + SaveRdata.InitialDirectory, "提示");
                        myStreamReader.Close();
                        return;
                    }
                }
                myStreamReader.Close();

                myStreamWriter = new StreamWriter(SaveRdata.InitialDirectory, true);
                myStreamWriter.Flush();
                for (int i = 0; i < listView_sample.Items.Count; i++)
                {
                    myStreamWriter.Write("{0,-2}", listView_sample.Items[i].SubItems[1].Text);
                    myStreamWriter.Write("{0,10}", listView_sample.Items[i].SubItems[2].Text);
                    myStreamWriter.Write("{0,8}", listView_sample.Items[i].SubItems[3].Text);
                    myStreamWriter.Write("{0,8}", listView_sample.Items[i].SubItems[4].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[5].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[6].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[7].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[8].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[9].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[10].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[15].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[16].Text);
                    myStreamWriter.Write("{0,12}", listView_sample.Items[i].SubItems[17].Text);
                    myStreamWriter.Write("{0,10}", listView_sample.Items[i].SubItems[18].Text);
                    myStreamWriter.WriteLine();
                }
                myStreamWriter.Flush();
                myStreamWriter.Close();
                tbx_status.Text = "采样数据导出成功";
                MessageBox.Show("采样数据导出成功", "提示");
            }
            else if (SaveRdata.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Savepath = SaveRdata.FileName;

                //FileStream myFileStream = new FileStream(SaveRdata.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                myStreamWriter = new StreamWriter(SaveRdata.FileName, true);
                myStreamWriter.Flush();
                for (int i = 0; i < listView_sample.Items.Count; i++)
                {
                    myStreamWriter.Write("{0,-2}", listView_sample.Items[i].SubItems[1].Text);
                    myStreamWriter.Write("{0,10}", listView_sample.Items[i].SubItems[2].Text);
                    myStreamWriter.Write("{0,8}", listView_sample.Items[i].SubItems[3].Text);
                    myStreamWriter.Write("{0,8}", listView_sample.Items[i].SubItems[4].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[5].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[6].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[7].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[8].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[9].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[10].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[15].Text);
                    myStreamWriter.Write("{0,7}", listView_sample.Items[i].SubItems[16].Text);
                    myStreamWriter.Write("{0,12}", listView_sample.Items[i].SubItems[17].Text);
                    myStreamWriter.Write("{0,10}", listView_sample.Items[i].SubItems[18].Text);
                    myStreamWriter.WriteLine();
                }
                myStreamWriter.Flush();
                myStreamWriter.Close();
                tbx_status.Text = "采样数据导出成功";
                MessageBox.Show("采样数据导出成功", "提示");
            }
            //else { myStreamWriter = new StreamWriter(@"D:\\"); }

            /*
            try
            {
                enabled_channel.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("请至少选择一个产品", "缺少目标"); return;
            }
            if(listView_sample.Items.Count == 0)
            {
                MessageBox.Show("没有采样数据，请先采样", "缺少数据"); return;
            }
            
            for (int i = 0; i < listView_sample.Items.Count; i++)
            {
                double x = Convert.ToDouble(listView_sample.Items[i].SubItems[3].Text) * 10;
                int y = (int)x;
                rdata[i, 0] = y.ToString("X4"); //设置p1*10
                x = Convert.ToDouble(listView_sample.Items[i].SubItems[4].Text) * 10;
                y = (int)x;
                rdata[i, 1] = y.ToString("X4"); //设置p2*10
                x = (Convert.ToDouble(listView_sample.Items[i].SubItems[5].Text) + 40) * 10;
                y = (int)x;
                rdata[i, 2] = y.ToString("X4"); //设置t1*10
                x = (Convert.ToDouble(listView_sample.Items[i].SubItems[6].Text) + 40) * 10;
                y = (int)x;
                rdata[i, 3] = y.ToString("X4"); //设置t2*10
                rdata[i, 4] = Convert.ToUInt16(listView_sample.Items[i].SubItems[7].Text).ToString("X4");    //采样p1
                rdata[i, 5] = Convert.ToUInt16(listView_sample.Items[i].SubItems[8].Text).ToString("X4");    //采样p2
                rdata[i, 6] = Convert.ToUInt16(listView_sample.Items[i].SubItems[9].Text).ToString("X4");    //采样t1
                rdata[i, 7] = Convert.ToUInt16(listView_sample.Items[i].SubItems[10].Text).ToString("X4");    //采样t2
                rdata[i, 8] = Convert.ToUInt16(listView_sample.Items[i].SubItems[1].Text).ToString("X4");    //序号
                rdata[i, 9] = Convert.ToUInt16(listView_sample.Items[i].SubItems[15].Text).ToString("X4");  //PSP1
                rdata[i, 10] = Convert.ToUInt16(listView_sample.Items[i].SubItems[16].Text).ToString("X4");  //PSP2

            }
            string strCom;
            UInt16[] st = new UInt16[5];
            UInt16 nCrc;
            int iAddr;
            
            for(int i = 0; i < 8; i++)
            {
                NVM_code[i] = rdata[enabled_channel.SelectedIndex,i];
            }
            NVM_code[0x34] = rdata[enabled_channel.SelectedIndex,8];
            NVM_code[0x35] = rdata[enabled_channel.SelectedIndex, 9];
            NVM_code[0x36] = rdata[enabled_channel.SelectedIndex, 10];
            CRC1();
            CRC2();
            //0x00~0x09
            for (j = 0; j <= 9; j++)
            {
                st[0] = 0x9D;
                st[1] = addr;
                st[2] = Convert.ToUInt16(NVM_code[j].Substring(0, 2), 16);
                st[3] = Convert.ToUInt16(NVM_code[j].Substring(2, 2), 16);
                nCrc = 0xFF;
                for (iAddr = 0; iAddr < 4; iAddr++)
                {
                    nCrc = CRC_calc(st[iAddr], nCrc);
                }
                st[4] = nCrc;
                strCom = "CCP 5 9d " + addr.ToString("X2") + " " + NVM_code[j].Substring(0, 2) + " " + NVM_code[j].Substring(2, 2)
                    + " " + st[4].ToString("X2") + " 1";
                strCom = strCom.ToLower();
                strCom1[j] = strCom;
                addr++;
            }
            //0x34~0x36
            addr = 0x34;
            for (j = 0; j <= 3; j++)
            {
                st[0] = 0x9D;
                st[1] = addr;
                st[2] = Convert.ToUInt16(NVM_code[j + 0x34].Substring(0, 2), 16);
                st[3] = Convert.ToUInt16(NVM_code[j + 0x34].Substring(2, 2), 16);
                nCrc = 0xFF;
                for (iAddr = 0; iAddr < 4; iAddr++)
                {
                    nCrc = CRC_calc(st[iAddr], nCrc);
                }
                st[4] = nCrc;
                strCom = "CCP 5 9d " + addr.ToString("X2") + " " + NVM_code[j + 0x34].Substring(0, 2) + " " + NVM_code[j + 0x34].Substring(2, 2)
                    + " " + st[4].ToString("X2") + " 1";
                strCom = strCom.ToLower();
                strCom1[j + 10] = strCom;
                addr++;
            }
            //0x75 CRC2
            st[0] = 0x9D;
            st[1] = 0x75;
            st[2] = Convert.ToUInt16(NVM_code[0x75].Substring(0, 2), 16);
            st[3] = Convert.ToUInt16(NVM_code[0x75].Substring(2, 2), 16);
            nCrc = 0xFF;
            for (iAddr = 0; iAddr < 4; iAddr++)
            {
                nCrc = CRC_calc(st[iAddr], nCrc);
            }
            st[4] = nCrc;
            strCom = "CCP 5 9d 75 " + NVM_code[0x75].Substring(0, 2) + " " + NVM_code[0x75].Substring(2, 2)
                + " " + st[4].ToString("X2") + " 1";
            strCom = strCom.ToLower();
            strCom1[13] = strCom;
            j = 0;
            addr = 0;
            WriteComm("POWEROFF", 15, SaveRdata);   //15 30
            */
        }

        private void tbx_PSPx_limit_TextChanged(object sender, EventArgs e)
        {
            if (tbx_PSPx_limit.Text == "") tbx_PSPx_limit.Text = "3";
            try
            {
                uint a = Convert.ToUInt32(tbx_PSPx_limit.Text);
                if (a > 50) tbx_PSPx_limit.Text = "50";
                else if (a < 3) tbx_PSPx_limit.Text = "3";
            }
            catch { MessageBox.Show("请输入正确数字,范围3~50", "格式错误"); }
        }

        private void btn_load_rdata_Click(object sender, EventArgs e)
        {
            string Filename = Properties.Settings.Default.Savepath;
            openRdata.InitialDirectory = Properties.Settings.Default.Savepath;
            StreamReader myStreamReader;
            if (File.Exists(Filename))
            {
                FileStream myFileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                myStreamReader = new StreamReader(myFileStream);
            }
            else if (openRdata.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Savepath = openRdata.InitialDirectory;
                FileStream myFileStream = new FileStream(openRdata.FileName, FileMode.Open, FileAccess.Read);
                myStreamReader = new StreamReader(myFileStream);
            }
            else
            {
                myStreamReader = new StreamReader(@"d:\\");
            }
            myStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            string line;
            while ((line = myStreamReader.ReadLine()) != null)
            {

                var st = new Regex("[\\s]+").Replace(line, " ");
                string[] rdata = st.Split(' ');
                if (rdata.Length < 10)//14
                {
                    MessageBox.Show("文件中数据缺失"); return;
                }
                if ((rdata[1] == listview_status.Items[0].SubItems[1].Text) || (rdata[1] == listview_status.Items[1].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[2].SubItems[1].Text) || (rdata[1] == listview_status.Items[3].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[4].SubItems[1].Text) || (rdata[1] == listview_status.Items[5].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[6].SubItems[1].Text) || (rdata[1] == listview_status.Items[7].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[8].SubItems[1].Text) || (rdata[1] == listview_status.Items[9].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[10].SubItems[1].Text) || (rdata[1] == listview_status.Items[11].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[12].SubItems[1].Text) || (rdata[1] == listview_status.Items[13].SubItems[1].Text)
                    || (rdata[1] == listview_status.Items[14].SubItems[1].Text) || (rdata[1] == listview_status.Items[15].SubItems[1].Text))
                {
                    ListViewItem item1 = new ListViewItem();
                    item1.SubItems.Add(rdata[0]);
                    item1.SubItems.Add(rdata[1]);
                    item1.SubItems.Add(rdata[2]);
                    item1.SubItems.Add(rdata[3]);
                    item1.SubItems.Add(rdata[4]);
                    item1.SubItems.Add(rdata[5]);
                    item1.SubItems.Add(rdata[6]);
                    item1.SubItems.Add(rdata[7]);
                    item1.SubItems.Add(rdata[8]);
                    item1.SubItems.Add(rdata[9]);
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add(rdata[10]);
                    item1.SubItems.Add(rdata[11]);
                    //item1.SubItems.Add(rdata[12]);
                    //item1.SubItems.Add(rdata[13]);
                    listView_sample.Items.Add(item1);
                }
            }
            myStreamReader.Close();
            tbx_status.Text = "采样数据导入成功";
        }

        private void btn_jiancha_Click(object sender, EventArgs e)
        {
            if (enabled_channel.Items.Count == 0)
            {
                MessageBox.Show("请至少选择一个产品", "缺少目标"); return;
            }
            enabled_channel.SelectedIndex = -1;
            for (int i = 0; i < 16; i++)
            {
                listview_status.Items[i].BackColor = Color.White;
                listview_status.Items[i].SubItems[1].Text = "";
                listview_status.Items[i].SubItems[2].Text = "";
            }
            enabled_channel.SelectedIndex = 0;
            WriteComm(".LOGLEVEL 0", 15, check_DUT);
        }

        private void Timer_user_Tick(object sender, EventArgs e)
        {
            if(character == "管理员")
            {
                Administrator();
                User_type.Text = users_name + "   " + character;
                Timer_user.Enabled = false;
            }
            else if (character == "标定人员")
            {
                production();
                User_type.Text = users_name + "   " + character;
                Timer_user.Enabled = false;
            }
            else if (character == "测试人员")
            {
                QC();
                User_type.Text = users_name + "   " + character;
                Timer_user.Enabled = false;
            }
            else if (character == "质检员")
            {
                QC();
                User_type.Text = users_name + "   " + character;
                Timer_user.Enabled = false;
            }
            else if(character =="未登录")
            {
               Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Date_Time.Text = DateTime.Now.ToString("g");
        }

        //bool first_sample = true;
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

        string wendu_yali_zuhe = "";

        private void btn_sample1_Click(object sender, EventArgs e)
        {
            string temp = "";
            switch (Sample_selected)
            {
                case 0: temp = tbx_qiya1.Text + tbx_diwen.Text; break;
                case 1: temp = tbx_qiya2.Text + tbx_changwen.Text; break;
                case 2: temp = tbx_qiya3.Text + tbx_gaowen.Text; break;
                default: break;
            }

            if (wendu_yali_zuhe == temp)
            {
                int a = listView_sample.Items.Count;
                if (a != 0)
                {
                    int b = enabled_channel.Items.Count;
                    int c = a % b;
                    if (c == 0)
                    {
                        for (int i = 0; i < b; i++)
                        {
                            listView_sample.Items.RemoveAt(a - b);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < c; i++)
                        {
                            listView_sample.Items.RemoveAt(a - c);
                        }
                    }
                }
            }
            try
            {
                enabled_channel.SelectedIndex = 0;
                Array.Clear(aAdcDiode1, 0, aAdcDiode1.Length);
            }
            catch { MessageBox.Show("请至少选择一个产品", "错误提示"); return; }
            cishu = Convert.ToUInt16(tbx_cishu.Text);
            switch (Sample_selected)
            {
                case 0: wendu_yali_zuhe = tbx_qiya1.Text + tbx_diwen.Text; break;
                case 1: wendu_yali_zuhe = tbx_qiya2.Text + tbx_changwen.Text; break;
                case 2: wendu_yali_zuhe = tbx_qiya3.Text + tbx_gaowen.Text; break;
                default: break;
            }
            for (int i = 0; i < 16; i++)    //清除状态
            {
                listview_status.Items[i].BackColor = Color.White;
                listview_status.Items[i].SubItems[2].Text = "";
            }
            WriteComm("POWEROFF", 15, Sample);
        }

        private void btn_del_rdata_Click(object sender, EventArgs e)
        {
            listView_sample.Items.Clear();
            Array.Clear(aAdcDiode1, 0, aAdcDiode1.Length);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CRC1();
            CRC2();
            CRC3();
            listView1.Items.Clear();
            for (int j = 0; j <= 0x7F; j++)
                listView1.Items.Add(NVM_code[j]);
        }

        public static string[] verify_shuzu = new string[17];
        private void btn_verify_Click(object sender, EventArgs e)
        {
            verify_shuzu[0] = tbx_P1_X1.Text;
            verify_shuzu[1] = tbx_P1_X2.Text;
            verify_shuzu[2] = tbx_P1_Y1.Text;
            verify_shuzu[3] = tbx_P1_Y2.Text;
            verify_shuzu[4] = tbx_P2_X1.Text;
            verify_shuzu[5] = tbx_P2_X2.Text;
            verify_shuzu[6] = tbx_P2_Y1.Text;
            verify_shuzu[7] = tbx_P2_Y2.Text;
            verify_shuzu[8] = tbx_T1_X1.Text;
            verify_shuzu[9] = tbx_T1_X2.Text;
            verify_shuzu[10] = tbx_T1_Y1.Text;
            verify_shuzu[11] = tbx_T1_Y2.Text;
            verify_shuzu[12] = tbx_T2_X1.Text;
            verify_shuzu[13] = tbx_T2_X2.Text;
            verify_shuzu[14] = tbx_T2_Y1.Text;
            verify_shuzu[15] = tbx_T2_Y2.Text;
            verify_shuzu[16] = sensor_xuanze.ToString();           
            verify verify1 = new verify();
            verify1.Show();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

    }


}
