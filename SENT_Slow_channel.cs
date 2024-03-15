using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class SENT_Slow_channel : Form
    {
        public SENT_Slow_channel()
        {
            InitializeComponent();
        }
        #region 编程大纲
        /**
         * 选择类型下拉列表时，更新下面3个文本框
         * 选中listbox_ID_data行时，更新3个文本框，同时与listbox_NVM_data联动
         * 插入行：
            * 检查行数是否溢出
            * 先把新增行插入到选定行下面，然后选定新增行
         *  listbox_NVM_data数据(fixed类型)：
            *  第二行是fixed 03并且第一行不是fixed类型时，4000
            *  如果上一行也是fixed，并且ID正好等于当前行ID-1，输出4000.
            *  如果不同时满足，分以下情况：
                *  如果fixed 03是第二行，
            *  如果fixed出现在第一行，2000+30ID，第二行开始方法通用
            *  else if fixed出现在第二行，且ID=3，4000
            *  else
         * 删除行：
            * 选定行是-1或最后一行，不执行
            * 先把选定行+1，然后删除选定行-1（向下删除）
            * 如果是倒数第二行，则反过来，先把选定行-1，然后删除选定行+1（向上删除）



        */
        #endregion

        int NVM_number = 14;
        private void Cbx_data_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Cbx_data_type.SelectedIndex)
            {
                case 0: Tb_data_code.Text = "DEC"; tbx_ID.Text = "01"; tbx_ID.ReadOnly = true; tbx_fix_data.Enabled = false; break;
                case 1: Tb_data_code.Text = "fixed"; tbx_ID.Text = "03"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 2: Tb_data_code.Text = "fixed"; tbx_ID.Text = "04"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 3: Tb_data_code.Text = "fixed"; tbx_ID.Text = "05"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 4: Tb_data_code.Text = "fixed"; tbx_ID.Text = "06"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 5: Tb_data_code.Text = "fixed"; tbx_ID.Text = "07"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 6: Tb_data_code.Text = "fixed"; tbx_ID.Text = "08"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 7: Tb_data_code.Text = "fixed"; tbx_ID.Text = "09"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 8: Tb_data_code.Text = "fixed"; tbx_ID.Text = "0A"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 9: Tb_data_code.Text = "fixed"; tbx_ID.Text = "80"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 10: Tb_data_code.Text = "fixed"; tbx_ID.Text = "81"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 11: Tb_data_code.Text = "fixed"; tbx_ID.Text = "82"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 12: Tb_data_code.Text = "fixed"; tbx_ID.Text = "83"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                case 13: Tb_data_code.Text = "SID #1"; tbx_ID.Text = "29"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 14: Tb_data_code.Text = "SID #2"; tbx_ID.Text = "2A"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 15: Tb_data_code.Text = "SID #3"; tbx_ID.Text = "2B"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 16: Tb_data_code.Text = "SID #4"; tbx_ID.Text = "2C"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 17: Tb_data_code.Text = "P1"; tbx_ID.Text = "1C"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 18: Tb_data_code.Text = "P2"; tbx_ID.Text = "1D"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 19: Tb_data_code.Text = "T1"; tbx_ID.Text = "23"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 20: Tb_data_code.Text = "T2"; tbx_ID.Text = "24"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 21: Tb_data_code.Text = "OEM #1"; tbx_ID.Text = "90"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 22: Tb_data_code.Text = "OEM #2"; tbx_ID.Text = "91"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 23: Tb_data_code.Text = "OEM #3"; tbx_ID.Text = "92"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 24: Tb_data_code.Text = "OEM #4"; tbx_ID.Text = "93"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 25: Tb_data_code.Text = "OEM #5"; tbx_ID.Text = "94"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 26: Tb_data_code.Text = "OEM #6"; tbx_ID.Text = "95"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 27: Tb_data_code.Text = "OEM #7"; tbx_ID.Text = "96"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 28: Tb_data_code.Text = "OEM #8"; tbx_ID.Text = "97"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = false; break;
                case 29: Tb_data_code.Text = "fixed"; tbx_ID.ReadOnly = false; tbx_fix_data.Enabled = true; break;
                default:
                    break;
            }
        }

        private void lbx_ID_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbx_NVM_data.SelectedIndex = lbx_ID_value.SelectedIndex;
            if (lbx_ID_value.SelectedIndex == (lbx_ID_value.Items.Count - 1))
            {
                lbx_ID_value.SelectedIndex = -1;
                return;
            }
            if (lbx_ID_value.SelectedIndex == -1) return;
            char[] separator = { ',' };           //分割字符串，分别填入不同文本框
            string[] splitstrings = new string[3];
            splitstrings = lbx_ID_value.Text.Split(separator);
            tbx_ID.Text = splitstrings[0];
            Tb_data_code.Text = splitstrings[1];
            if (splitstrings.Length > 2)
            {
                tbx_fix_data.Text = splitstrings[2];
                tbx_fix_data.Enabled = true;
            }
            else
            {
                tbx_fix_data.Text = "";
                tbx_fix_data.Enabled = false;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //检查是否溢出
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            if (NVM_number >= 44)
            {
                MessageBox.Show("超出NVM容量", "溢出提示");
                return;
            }
            lbx_ID_value.Items.RemoveAt(lbx_ID_value.Items.Count - 1);
            #region **设定ID和fixed值范围**
            //设定值位数修正
            if ((tbx_fix_data.Text.Length == 0) && (tbx_fix_data.Enabled == true)) tbx_fix_data.Text = "000";
            else if (tbx_fix_data.Text.Length == 1) tbx_fix_data.Text = "00" + tbx_fix_data.Text;
            else if (tbx_fix_data.Text.Length == 2) tbx_fix_data.Text = "0" + tbx_fix_data.Text;
            else if (tbx_fix_data.Text.Length > 3)
            {
                string temp1 = tbx_fix_data.Text;
                tbx_fix_data.Text = tbx_fix_data.Text.Substring(tbx_fix_data.Text.Length - 3);
                MessageBox.Show("输入值0x" + temp1 + "超出范围000~FFF\n已使用0x" + tbx_fix_data.Text + "代替", "格式错误");
            }
            else { }
            //ID值位数修正
            if (tbx_ID.Text.Length == 0) tbx_ID.Text = "00";
            else if (tbx_ID.Text.Length == 1) tbx_ID.Text = "0" + tbx_ID.Text;
            else if (tbx_ID.Text.Length > 2)
            {
                string temp2 = tbx_ID.Text;
                tbx_ID.Text = tbx_ID.Text.Substring(tbx_ID.Text.Length - 2);
                MessageBox.Show("输入值0x" + temp2 + "超出范围00~FF\n已使用0x" + tbx_ID.Text + "代替", "格式错误");
            }
            else { }
            #endregion

            //第一列表
            if (tbx_fix_data.Enabled == false)
            { lbx_ID_value.Items.Insert(lbx_ID_value.SelectedIndex + 1, tbx_ID.Text + "," + Tb_data_code.Text); }
            else { lbx_ID_value.Items.Insert(lbx_ID_value.SelectedIndex + 1, tbx_ID.Text + "," + Tb_data_code.Text + "," + tbx_fix_data.Text); }
            lbx_ID_value.Items.Add("（" + lbx_ID_value.Items.Count.ToString() + "条）");

            //第二列表

            string NVM_data = " ";
            switch (Tb_data_code.Text)
            {
                case "DEC":
                    try
                    {
                        if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(3, 2) == "fi")
                        {
                            string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(0, 2);
                            string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(9, 3);
                            int next_ID = Convert.ToInt32(next_ID_char, 16);

                            lbx_NVM_data.Items.Insert(lbx_NVM_data.SelectedIndex + 1, "60" + (next_ID - 1).ToString("X2"));
                            lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 2] = "4" + next_fixed_value;
                            if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(3, 2) == "DE")
                            { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "6000"; }
                        }
                        else { lbx_NVM_data.Items.Insert(lbx_NVM_data.SelectedIndex + 1, "6000"); }
                    }
                    catch { lbx_NVM_data.Items.Insert(lbx_NVM_data.SelectedIndex + 1, "6000"); }

                    lbx_ID_value.SelectedIndex += 1;
                    if (Cbx_data_type.SelectedIndex < 29) Cbx_data_type.SelectedIndex += 1;
                    lbx_NVM_data.Items.RemoveAt(lbx_NVM_data.Items.Count - 1);
                    #region **计算NVM寄存器数量**
                    NVM_number = lbx_NVM_data.Items.Count;
                    for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
                    {
                        if (lbx_NVM_data.Items[i].ToString().Length > 4)
                            NVM_number++;
                    }
                    #endregion
                    lbx_NVM_data.Items.Add("（" + NVM_number.ToString() + "条）");
                    return;
                //break;                                            //DEC
                case "fixed":                                                                   /*S_TYPE--MFU #4*/
                    {
                        string last_ID_char;
                        int last_ID = 0;
                        int current_ID = Convert.ToInt32(tbx_ID.Text, 16);

                        if (lbx_ID_value.SelectedIndex >= 0)    //如果要插入不是第一行，则获取上一行ID和类型
                        {
                            if (lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex].ToString().Substring(0, 2) == "60")
                                last_ID_char = lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex].ToString().Substring(2, 2);
                            else last_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex].ToString().Substring(0, 2);
                            last_ID = Convert.ToInt32(last_ID_char, 16);
                        }
                        if (lbx_ID_value.SelectedIndex == -1) { NVM_data = "2" + tbx_fix_data.Text + ",30" + tbx_ID.Text; }    //如果是插入到第一行，则2000
                        else if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex].ToString().Substring(3, 2) == "DE")             //检查当前行是不是DEC
                        {
                            NVM_data = "4" + tbx_fix_data.Text;
                            lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "60" + (current_ID - 1).ToString("X2");
                        }
                        else if (last_ID == (current_ID - 1)) NVM_data = "4" + tbx_fix_data.Text;
                        else { NVM_data = "2" + tbx_fix_data.Text + ",30" + tbx_ID.Text; }


                    }
                    break;
                case "SID #1": NVM_data = "10" + tbx_ID.Text; break;                              //SID #1
                case "SID #2": NVM_data = "11" + tbx_ID.Text; break;                              //SID #2
                case "SID #3": NVM_data = "11" + tbx_ID.Text; break;                              //SID #3
                case "SID #4": NVM_data = "11" + tbx_ID.Text; break;                              //SID #4
                case "P1": NVM_data = "00" + tbx_ID.Text; break;                               //P1
                case "P2": NVM_data = "01" + tbx_ID.Text; break;                               //P2
                case "T1": NVM_data = "02" + tbx_ID.Text; break;                               //T1
                case "T2": NVM_data = "03" + tbx_ID.Text; break;                               //T2
                case "OEM #1": NVM_data = "5090"; break;                                           //OEM #1
                case "OEM #2": NVM_data = "5191"; break;                                           //OEM #2
                case "OEM #3": NVM_data = "5292"; break;                                           //OEM #3
                case "OEM #4": NVM_data = "5393"; break;                                           //OEM #4
                case "OEM #5": NVM_data = "5494"; break;                                           //OEM #5
                case "OEM #6": NVM_data = "5595"; break;                                           //OEM #6
                case "OEM #7": NVM_data = "5696"; break;                                           //OEM #7
                case "OEM #8": NVM_data = "5797"; break;                                           //OEM #8
                default:
                    break;
            }
            lbx_NVM_data.Items.Insert(lbx_NVM_data.SelectedIndex + 1, NVM_data);
            //检查下一项是否要更改
            try
            {
                if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(3, 2) == "fi")
                {
                    string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(0, 2);
                    string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 2].ToString().Substring(9, 3);
                    int next_ID = Convert.ToInt32(next_ID_char, 16);
                    int current_ID = Convert.ToInt32(tbx_ID.Text, 16);
                    if (next_ID == (current_ID + 1)) lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 2] = "4" + next_fixed_value;
                    else { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 2] = "2" + next_fixed_value + ",30" + next_ID_char; }
                }
            }
            catch { }
            lbx_ID_value.SelectedIndex += 1;
            lbx_NVM_data.Items.RemoveAt(lbx_NVM_data.Items.Count - 1);
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            lbx_NVM_data.Items.Add("（" + NVM_number.ToString() + "条）");
            if (Cbx_data_type.SelectedIndex < 29) Cbx_data_type.SelectedIndex += 1;


        }

        private void btn_insert_01_Click(object sender, EventArgs e)
        {
            //检查是否溢出
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            if (NVM_number >= 44)
            {
                MessageBox.Show("超出NVM容量", "溢出提示");
                return;
            }

            //检查下一项是否要更改
            try
            {
                if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(3, 2) == "fi")
                {
                    string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(0, 2);
                    string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(9, 3);
                    int next_ID = Convert.ToInt32(next_ID_char, 16);
                    if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex].ToString().Substring(3, 3) == "DEC")
                    { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "6000"; }
                    lbx_NVM_data.Items.Insert(lbx_ID_value.SelectedIndex + 1, "60" + (next_ID - 1).ToString("X2"));
                    lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 2] = "4" + next_fixed_value;
                }
                else { lbx_NVM_data.Items.Insert(lbx_ID_value.SelectedIndex + 1, "6000"); }
            }
            catch { lbx_NVM_data.Items.Insert(lbx_ID_value.SelectedIndex + 1, "6000"); }
            lbx_ID_value.Items.Insert(lbx_ID_value.SelectedIndex + 1, "01,DEC");

            lbx_ID_value.SelectedIndex += 1;
            lbx_ID_value.Items.RemoveAt(lbx_ID_value.Items.Count - 1);
            lbx_NVM_data.Items.RemoveAt(lbx_NVM_data.Items.Count - 1);
            lbx_ID_value.Items.Add("（" + lbx_ID_value.Items.Count.ToString() + "条）");
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            lbx_NVM_data.Items.Add("（" + NVM_number.ToString() + "条）");
            if (Cbx_data_type.SelectedIndex < 29) Cbx_data_type.SelectedIndex += 1;

        }

        private void btn_change_data_Click(object sender, EventArgs e)
        {
            if (lbx_ID_value.SelectedIndex == -1) return;
            #region **设定ID和fixed值范围**
            //设定值位数修正
            if ((tbx_fix_data.Text.Length == 0) && (tbx_fix_data.Enabled == true)) tbx_fix_data.Text = "000";
            else if (tbx_fix_data.Text.Length == 1) tbx_fix_data.Text = "00" + tbx_fix_data.Text;
            else if (tbx_fix_data.Text.Length == 2) tbx_fix_data.Text = "0" + tbx_fix_data.Text;
            else if (tbx_fix_data.Text.Length > 3)
            {
                string temp1 = tbx_fix_data.Text;
                tbx_fix_data.Text = tbx_fix_data.Text.Substring(tbx_fix_data.Text.Length - 3);
                MessageBox.Show("输入值0x" + temp1 + "超出范围000~FFF\n已使用0x" + tbx_fix_data.Text + "代替", "");
            }
            else { }
            //ID值位数修正
            if (tbx_ID.Text.Length == 0) tbx_ID.Text = "00";
            else if (tbx_ID.Text.Length == 1) tbx_ID.Text = "0" + tbx_ID.Text;
            else if (tbx_ID.Text.Length > 2)
            {
                string temp2 = tbx_ID.Text;
                tbx_ID.Text = tbx_ID.Text.Substring(tbx_ID.Text.Length - 2);
                MessageBox.Show("输入值0x" + temp2 + "超出范围00~FF\n已使用0x" + tbx_ID.Text + "代替", "格式错误");
            }
            else { }
            #endregion
            switch (Tb_data_code.Text)
            {
                case "DEC":
                    try
                    {
                        if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(3, 2) == "fi")
                        {
                            string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(0, 2);
                            string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(9, 3);
                            int next_ID = Convert.ToInt32(next_ID_char, 16);

                            lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "60" + (next_ID - 1).ToString("X2");

                        }
                        else { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "6000"; }
                    }
                    catch { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "6000"; }
                    lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = "01,DEC";
                    break;

                case "fixed":
                    string last_ID_char;
                    int last_ID;
                    int current_ID = Convert.ToInt32(tbx_ID.Text, 16);
                    lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text + "," + tbx_fix_data.Text;
                    if (lbx_ID_value.SelectedIndex == 0) lbx_NVM_data.Items[0] = "2" + tbx_fix_data.Text + ",30" + tbx_ID.Text;
                    else
                    {
                        if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(3, 2) == "DE")
                        {
                            lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex - 1] = "60" + (current_ID - 1).ToString("X2");//DEC特殊处理
                            lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "4" + tbx_fix_data.Text;
                        }
                        else
                        {
                            last_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(0, 2);
                            last_ID = Convert.ToInt32(last_ID_char, 16);
                            if (last_ID == (current_ID - 1))
                                lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "4" + tbx_fix_data.Text;
                            else lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "2" + tbx_fix_data.Text + ",30" + tbx_ID.Text;
                        }
                    }
                    break;
                case "SID #1": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "10" + tbx_ID.Text; break;     //SID #1
                case "SID #2": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "11" + tbx_ID.Text; break;  //SID #2
                case "SID #3": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "11" + tbx_ID.Text; break;  //SID #3
                case "SID #4": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "11" + tbx_ID.Text; break;  //SID #4
                case "P1": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "00" + tbx_ID.Text; break;      //P1
                case "P2": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "01" + tbx_ID.Text; break;      //P2
                case "T1": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "02" + tbx_ID.Text; break;      //T1
                case "T2": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "03" + tbx_ID.Text; break;      //T2
                case "OEM #1": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5090"; break;              //OEM #1
                case "OEM #2": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5191"; break;              //OEM #2
                case "OEM #3": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5292"; break;              //OEM #3
                case "OEM #4": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5393"; break;              //OEM #4
                case "OEM #5": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5494"; break;              //OEM #5
                case "OEM #6": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5595"; break;              //OEM #6
                case "OEM #7": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5696"; break;              //OEM #7
                case "OEM #8": lbx_ID_value.Items[lbx_ID_value.SelectedIndex] = tbx_ID.Text + "," + Tb_data_code.Text; lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex] = "5797"; break;              //OEM #8
                default: break;

            }
            //检查下一项是否要更改
            try
            {
                if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(3, 2) == "fi")
                {

                    string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(0, 2);
                    string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(9, 3);
                    int next_ID = Convert.ToInt32(next_ID_char, 16);
                    int current_ID = Convert.ToInt32(tbx_ID.Text, 16);
                    if (Tb_data_code.Text == "DEC")
                    {
                        lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "4" + next_fixed_value;
                    }
                    else
                    {
                        if (next_ID == (current_ID + 1)) lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "4" + next_fixed_value;

                        else { lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "2" + next_fixed_value + ",30" + next_ID_char; }
                    }
                }
                if (Tb_data_code.Text == "fixed") return;
                if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(3, 2) == "DE")
                    lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex - 1] = "6000";

            }
            catch { }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (lbx_NVM_data.Items.Count == 1) { return; }
            if (lbx_NVM_data.SelectedIndex == -1) { return; }

            if (lbx_ID_value.SelectedIndex == (lbx_ID_value.Items.Count - 2))   //如果是倒数第二行（倒着删），不用考虑特殊情况
            {
                lbx_ID_value.SelectedIndex -= 1;
                lbx_ID_value.Items.RemoveAt(lbx_ID_value.Items.Count - 2);
                lbx_NVM_data.Items.RemoveAt(lbx_NVM_data.Items.Count - 2);
            }
            else if (lbx_ID_value.SelectedIndex == 0)                        //如果是从第一行开始删，只需考虑第二行是不是fixed
            {
                if (lbx_ID_value.Items[1].ToString().Substring(3, 2) == "fi")
                {
                    string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(0, 2);
                    string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(9, 3);
                    lbx_NVM_data.Items[1] = "2" + next_fixed_value + ",30" + next_ID_char;

                }
                lbx_ID_value.Items.RemoveAt(0);
                lbx_NVM_data.Items.RemoveAt(0);
                lbx_ID_value.SelectedIndex = 0;
            }
            else                                                        //从中间开始删
            {
                if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(3, 2) == "fi")
                {
                    string last_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(0, 2);
                    int last_ID = Convert.ToInt32(last_ID_char, 16);
                    string next_ID_char = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(0, 2);
                    string next_fixed_value = lbx_ID_value.Items[lbx_ID_value.SelectedIndex + 1].ToString().Substring(9, 3);
                    int next_ID = Convert.ToInt32(next_ID_char, 16);
                    if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(3, 2) == "DE")
                    {
                        lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex - 1] = "60" + (next_ID - 1).ToString("X2");
                        lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "4" + next_fixed_value;
                    }
                    else
                    {
                        if (last_ID == (next_ID - 1)) lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "4" + next_fixed_value;
                        else lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex + 1] = "2" + next_fixed_value + ",30" + next_ID_char;
                    }

                }
                else if (lbx_ID_value.Items[lbx_ID_value.SelectedIndex - 1].ToString().Substring(3, 2) == "DE")
                    lbx_NVM_data.Items[lbx_NVM_data.SelectedIndex - 1] = "6000";
                else { }
                lbx_ID_value.SelectedIndex += 1;
                lbx_ID_value.Items.RemoveAt(lbx_ID_value.SelectedIndex - 1);
                lbx_NVM_data.Items.RemoveAt(lbx_ID_value.SelectedIndex - 1);

            }
            //更新最后一条的数目
            lbx_ID_value.Items.RemoveAt(lbx_ID_value.Items.Count - 1);

            lbx_NVM_data.Items.RemoveAt(lbx_NVM_data.Items.Count - 1);
            lbx_ID_value.Items.Add("（" + lbx_ID_value.Items.Count.ToString() + "条）");
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            lbx_NVM_data.Items.Add("（" + NVM_number.ToString() + "条）");
        }


        private void lbx_NVM_data_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lbx_ID_value.SelectedIndex != lbx_NVM_data.SelectedIndex) lbx_ID_value.SelectedIndex = lbx_NVM_data.SelectedIndex;
            if (lbx_NVM_data.SelectedIndex == (lbx_NVM_data.Items.Count - 1))
            {
                lbx_NVM_data.SelectedIndex = -1;
                return;
            }
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            //保存OEM值
            //先检查OEM有没有特殊字符
            try
            {
                bool b = false;
                int a = Convert.ToInt32(Tbx_OEM1.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM2.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM3.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM4.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM5.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM6.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM7.Text, 16); if (a < 0) b = true;
                a = Convert.ToInt32(Tbx_OEM8.Text, 16); if (a < 0) b = true;
                if (b == true) { MessageBox.Show("OEM1~8中有负数，请检查后重新输入", "格式错误"); return; }
            }
            catch { MessageBox.Show("OEM1~8中有特殊字符，请检查后重新输入", "格式错误"); return; }
            //再检查OEM位数
            if (Tbx_OEM1.Text.Length == 0) Tbx_OEM1.Text = "000";
            else if (Tbx_OEM1.Text.Length == 1) Tbx_OEM1.Text = "00" + Tbx_OEM1.Text;
            else if (Tbx_OEM1.Text.Length == 2) Tbx_OEM1.Text = "0" + Tbx_OEM1.Text; else if (Tbx_OEM1.Text.Length > 3) Tbx_OEM1.Text = Tbx_OEM1.Text.Substring(Tbx_OEM1.Text.Length - 3, 3);
            if (Tbx_OEM2.Text.Length == 0) Tbx_OEM2.Text = "000";
            else if (Tbx_OEM2.Text.Length == 1) Tbx_OEM2.Text = "00" + Tbx_OEM2.Text;
            else if (Tbx_OEM2.Text.Length == 2) Tbx_OEM2.Text = "0" + Tbx_OEM2.Text; else if (Tbx_OEM2.Text.Length > 3) Tbx_OEM2.Text = Tbx_OEM2.Text.Substring(Tbx_OEM2.Text.Length - 3, 3);
            if (Tbx_OEM3.Text.Length == 0) Tbx_OEM3.Text = "000";
            else if (Tbx_OEM3.Text.Length == 1) Tbx_OEM3.Text = "00" + Tbx_OEM3.Text;
            else if (Tbx_OEM3.Text.Length == 2) Tbx_OEM3.Text = "0" + Tbx_OEM3.Text; else if (Tbx_OEM3.Text.Length > 3) Tbx_OEM3.Text = Tbx_OEM3.Text.Substring(Tbx_OEM3.Text.Length - 3, 3);
            if (Tbx_OEM4.Text.Length == 0) Tbx_OEM4.Text = "000";
            else if (Tbx_OEM4.Text.Length == 1) Tbx_OEM4.Text = "00" + Tbx_OEM4.Text;
            else if (Tbx_OEM4.Text.Length == 2) Tbx_OEM4.Text = "0" + Tbx_OEM4.Text; else if (Tbx_OEM4.Text.Length > 3) Tbx_OEM4.Text = Tbx_OEM4.Text.Substring(Tbx_OEM4.Text.Length - 3, 3);
            if (Tbx_OEM5.Text.Length == 0) Tbx_OEM5.Text = "000";
            else if (Tbx_OEM5.Text.Length == 1) Tbx_OEM5.Text = "00" + Tbx_OEM5.Text;
            else if (Tbx_OEM5.Text.Length == 2) Tbx_OEM5.Text = "0" + Tbx_OEM5.Text; else if (Tbx_OEM5.Text.Length > 3) Tbx_OEM5.Text = Tbx_OEM5.Text.Substring(Tbx_OEM5.Text.Length - 3, 3);
            if (Tbx_OEM6.Text.Length == 0) Tbx_OEM6.Text = "000";
            else if (Tbx_OEM6.Text.Length == 1) Tbx_OEM6.Text = "00" + Tbx_OEM6.Text;
            else if (Tbx_OEM6.Text.Length == 2) Tbx_OEM6.Text = "0" + Tbx_OEM6.Text; else if (Tbx_OEM6.Text.Length > 3) Tbx_OEM6.Text = Tbx_OEM6.Text.Substring(Tbx_OEM6.Text.Length - 3, 3);
            if (Tbx_OEM7.Text.Length == 0) Tbx_OEM7.Text = "000";
            else if (Tbx_OEM7.Text.Length == 1) Tbx_OEM7.Text = "00" + Tbx_OEM7.Text;
            else if (Tbx_OEM7.Text.Length == 2) Tbx_OEM7.Text = "0" + Tbx_OEM7.Text; else if (Tbx_OEM7.Text.Length > 3) Tbx_OEM7.Text = Tbx_OEM7.Text.Substring(Tbx_OEM7.Text.Length - 3, 3);
            if (Tbx_OEM8.Text.Length == 0) Tbx_OEM8.Text = "000";
            else if (Tbx_OEM8.Text.Length == 1) Tbx_OEM8.Text = "00" + Tbx_OEM8.Text;
            else if (Tbx_OEM8.Text.Length == 2) Tbx_OEM8.Text = "0" + Tbx_OEM8.Text; else if (Tbx_OEM8.Text.Length > 3) Tbx_OEM8.Text = Tbx_OEM8.Text.Substring(Tbx_OEM8.Text.Length - 3, 3);

            Form1.NVM_code[0] = "0" + Tbx_OEM1.Text;
            Form1.NVM_code[1] = "0" + Tbx_OEM2.Text;
            Form1.NVM_code[2] = "0" + Tbx_OEM3.Text;
            Form1.NVM_code[3] = "0" + Tbx_OEM4.Text;
            Form1.NVM_code[4] = "0" + Tbx_OEM5.Text;
            Form1.NVM_code[5] = "0" + Tbx_OEM6.Text;
            Form1.NVM_code[6] = "0" + Tbx_OEM7.Text;
            Form1.NVM_code[7] = "0" + Tbx_OEM8.Text;

            //保存NVM_data值
            int i, n = 0;
            for (i = 0; i < lbx_NVM_data.Items.Count - 1; i++)
            {
                Form1.NVM_code[i + n + 10] = lbx_NVM_data.Items[i].ToString().Substring(0, 4);

                if (lbx_NVM_data.Items[i].ToString().Length > 4)
                {
                    n++;
                    Form1.NVM_code[i + n + 10] = lbx_NVM_data.Items[i].ToString().Substring(5, 4);
                }

            }
            for (int a = i + n + 10; a < 42; a++)
            {
                Form1.NVM_code[a] = "F000";
            }
            //保存sfr中慢通道注释
            int c, d, m = 0;

            for (c = 0; c < lbx_ID_value.Items.Count - 1; c++)
            {
                d = lbx_NVM_data.Items[c].ToString().Length;
                Form1.slow_ID_Value[c + m] = lbx_ID_value.Items[c].ToString();
                if (d > 4)
                {
                    Form1.slow_ID_Value[c + m + 1] = "";
                    m++;
                }

            }
            if (c < 41)
            {
                for (c = lbx_ID_value.Items.Count - 1; c < 42; c++)
                {
                    Form1.slow_ID_Value[c] = "";
                }
            }

            lbx_ID_value.Items.Clear();
            lbx_NVM_data.Items.Clear();
            //Form1.CRC1();
            Form1.CRC2();
            this.Close();
        }

        private void SENT_Slow_channel_Load(object sender, EventArgs e)
        {
            ////读取OEM值 弃用
            //Tbx_OEM1.Text = Form1.NVM_code[0].ToString();
            //Tbx_OEM2.Text = Form1.NVM_code[1].ToString();
            //Tbx_OEM3.Text = Form1.NVM_code[2].ToString();
            //Tbx_OEM4.Text = Form1.NVM_code[3].ToString();
            //Tbx_OEM5.Text = Form1.NVM_code[4].ToString();
            //Tbx_OEM6.Text = Form1.NVM_code[5].ToString();
            //Tbx_OEM7.Text = Form1.NVM_code[6].ToString();
            //Tbx_OEM8.Text = Form1.NVM_code[7].ToString();

            //读取OEM值
            Tbx_OEM1.Text = Form1.NVM_code[0].ToString().Substring(1, 3);
            Tbx_OEM2.Text = Form1.NVM_code[1].ToString().Substring(1, 3);
            Tbx_OEM3.Text = Form1.NVM_code[2].ToString().Substring(1, 3);
            Tbx_OEM4.Text = Form1.NVM_code[3].ToString().Substring(1, 3);
            Tbx_OEM5.Text = Form1.NVM_code[4].ToString().Substring(1, 3);
            Tbx_OEM6.Text = Form1.NVM_code[5].ToString().Substring(1, 3);
            Tbx_OEM7.Text = Form1.NVM_code[6].ToString().Substring(1, 3);
            Tbx_OEM8.Text = Form1.NVM_code[7].ToString().Substring(1, 3);

            //读取NVM_data值
            int i, n = 0;

            for (i = 0; i < 43; i++)
            {
                if (Form1.NVM_code[i + n + 10].Substring(0, 2) == "F0")
                {

                    break;
                }

                else if (Form1.NVM_code[i + n + 10].Substring(0, 1) == "2")
                {
                    n++;
                    lbx_NVM_data.Items.Add(Form1.NVM_code[9 + i + n] + "," + Form1.NVM_code[10 + i + n]);
                }
                else
                {
                    lbx_NVM_data.Items.Add(Form1.NVM_code[10 + i + n]);
                }
            }
            #region **计算NVM寄存器数量**
            NVM_number = lbx_NVM_data.Items.Count;
            for (int j = 0; j < lbx_NVM_data.Items.Count; j++)
            {
                if (lbx_NVM_data.Items[j].ToString().Length > 4)
                    NVM_number++;
            }
            #endregion
            lbx_NVM_data.Items.Add("（" + NVM_number.ToString() + "条）");

            //对应编辑ID_value值
            for (i = 0; i < lbx_NVM_data.Items.Count; i++)
            {
                string NVM_Item_data = lbx_NVM_data.Items[i].ToString();
                string NVM_data_head = NVM_Item_data.Substring(0, 2);
                string NVM_data_end = NVM_Item_data.Substring(2, 2);
                switch (NVM_data_head)
                {
                    case "60": lbx_ID_value.Items.Add("01,DEC"); break;
                    case "10": lbx_ID_value.Items.Add(NVM_data_end + ",SID #1"); break;
                    case "11": lbx_ID_value.Items.Add(NVM_data_end + ",SID #2"); break;
                    case "12": lbx_ID_value.Items.Add(NVM_data_end + ",SID #3"); break;
                    case "13": lbx_ID_value.Items.Add(NVM_data_end + ",SID #4"); break;
                    case "00": lbx_ID_value.Items.Add(NVM_data_end + ",P1"); break;
                    case "01": lbx_ID_value.Items.Add(NVM_data_end + ",P2"); break;
                    case "02": lbx_ID_value.Items.Add(NVM_data_end + ",T1"); break;
                    case "03": lbx_ID_value.Items.Add(NVM_data_end + ",T2"); break;
                    case "50": lbx_ID_value.Items.Add("90,OEM #1"); break;
                    case "51": lbx_ID_value.Items.Add("91,OEM #2"); break;
                    case "52": lbx_ID_value.Items.Add("92,OEM #3"); break;
                    case "53": lbx_ID_value.Items.Add("93,OEM #4"); break;
                    case "54": lbx_ID_value.Items.Add("94,OEM #5"); break;
                    case "55": lbx_ID_value.Items.Add("95,OEM #6"); break;
                    case "56": lbx_ID_value.Items.Add("96,OEM #7"); break;
                    case "57": lbx_ID_value.Items.Add("97,OEM #8"); break;

                    default:
                        string head_1 = NVM_Item_data.Substring(0, 1);
                        string end_3 = NVM_Item_data.Substring(1, 3);
                        switch (head_1)
                        {
                            case "2":
                                string ID_2 = NVM_Item_data.Substring(NVM_Item_data.Length - 2, 2);
                                lbx_ID_value.Items.Add(ID_2 + ",fixed," + end_3); break;
                            case "4":
                                string ID_1_char = lbx_ID_value.Items[i - 1].ToString().Substring(0, 2);
                                if (lbx_ID_value.Items[i - 1].ToString() == "01,DEC")
                                    ID_1_char = lbx_NVM_data.Items[i - 1].ToString().Substring(2, 2);
                                int ID_1 = Convert.ToInt32(ID_1_char, 16);
                                lbx_ID_value.Items.Add((ID_1 + 1).ToString("X2") + ",fixed," + end_3);
                                break;
                            case "（":
                                lbx_ID_value.Items.Add("（" + i.ToString() + "条）");
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }
    }
}
