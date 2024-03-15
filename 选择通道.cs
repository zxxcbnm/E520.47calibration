using System;
using System.Windows.Forms;

namespace E520._47标定
{
    public partial class Select_channel : Form
    {
        public Select_channel()
        {
            InitializeComponent();
            foreach (string i in Form1.enable_channel1)
            {
                if (i == null) break;
                listBox1.Items.Add(i);

            }
            listBox1.SelectedIndex = 0;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) listBox1.SelectedIndex = 0;
            Form1.selected_channel = Convert.ToUInt16(listBox1.SelectedIndex);
            this.Close();
        }
    }
}
