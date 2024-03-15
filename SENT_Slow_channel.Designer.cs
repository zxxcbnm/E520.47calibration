namespace E520._47标定
{
    partial class SENT_Slow_channel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Tb_data_code = new System.Windows.Forms.TextBox();
            this.Cbx_data_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_fix_data = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_insert_01 = new System.Windows.Forms.Button();
            this.btn_change_data = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.lbx_ID_value = new System.Windows.Forms.ListBox();
            this.lbx_NVM_data = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Tbx_OEM1 = new System.Windows.Forms.TextBox();
            this.Tbx_OEM2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Tbx_OEM3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Tbx_OEM4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Tbx_OEM5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Tbx_OEM6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Tbx_OEM7 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Tbx_OEM8 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(727, 570);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(103, 26);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.Text = "保存并退出";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(322, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "SENT慢通道设置";
            // 
            // Tb_data_code
            // 
            this.Tb_data_code.BackColor = System.Drawing.SystemColors.Control;
            this.Tb_data_code.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tb_data_code.Location = new System.Drawing.Point(21, 92);
            this.Tb_data_code.Name = "Tb_data_code";
            this.Tb_data_code.ReadOnly = true;
            this.Tb_data_code.Size = new System.Drawing.Size(100, 25);
            this.Tb_data_code.TabIndex = 3;
            // 
            // Cbx_data_type
            // 
            this.Cbx_data_type.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cbx_data_type.FormattingEnabled = true;
            this.Cbx_data_type.Items.AddRange(new object[] {
            "DEC",
            "S_TYPE",
            "CCODE",
            "MCODE",
            "SENT_REV",
            "PX1",
            "PX2",
            "PY1",
            "PY2",
            "MFU #1",
            "MFU #2",
            "MFU #3",
            "MFU #4",
            "SID #1",
            "SID #2",
            "SID #3",
            "SID #4",
            "P1",
            "P2",
            "T1",
            "T2",
            "OEM #1",
            "OEM #2",
            "OEM #3",
            "OEM #4",
            "OEM #5",
            "OEM #6",
            "OEM #7",
            "OEM #8",
            "other fix data"});
            this.Cbx_data_type.Location = new System.Drawing.Point(21, 40);
            this.Cbx_data_type.Name = "Cbx_data_type";
            this.Cbx_data_type.Size = new System.Drawing.Size(100, 25);
            this.Cbx_data_type.TabIndex = 4;
            this.Cbx_data_type.SelectedIndexChanged += new System.EventHandler(this.Cbx_data_type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据代码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 34);
            this.label4.TabIndex = 2;
            this.label4.Text = "ID 值\r\n(00~FF)";
            // 
            // tbx_ID
            // 
            this.tbx_ID.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_ID.Location = new System.Drawing.Point(21, 149);
            this.tbx_ID.Name = "tbx_ID";
            this.tbx_ID.Size = new System.Drawing.Size(100, 25);
            this.tbx_ID.TabIndex = 3;
            this.tbx_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(143, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 34);
            this.label5.TabIndex = 2;
            this.label5.Text = "设定值\r\n(000~FFF)";
            // 
            // tbx_fix_data
            // 
            this.tbx_fix_data.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_fix_data.Location = new System.Drawing.Point(21, 203);
            this.tbx_fix_data.Name = "tbx_fix_data";
            this.tbx_fix_data.Size = new System.Drawing.Size(100, 25);
            this.tbx_fix_data.TabIndex = 3;
            this.tbx_fix_data.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cbx_data_type);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Tb_data_code);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbx_ID);
            this.groupBox1.Controls.Add(this.tbx_fix_data);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 253);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "编辑数据";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(54, 353);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(148, 30);
            this.btn_add.TabIndex = 6;
            this.btn_add.Text = "增加/插入行";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_insert_01
            // 
            this.btn_insert_01.Location = new System.Drawing.Point(54, 417);
            this.btn_insert_01.Name = "btn_insert_01";
            this.btn_insert_01.Size = new System.Drawing.Size(148, 30);
            this.btn_insert_01.TabIndex = 6;
            this.btn_insert_01.Text = "插入行 ID=1(DEC)";
            this.btn_insert_01.UseVisualStyleBackColor = true;
            this.btn_insert_01.Click += new System.EventHandler(this.btn_insert_01_Click);
            // 
            // btn_change_data
            // 
            this.btn_change_data.Location = new System.Drawing.Point(54, 481);
            this.btn_change_data.Name = "btn_change_data";
            this.btn_change_data.Size = new System.Drawing.Size(148, 30);
            this.btn_change_data.TabIndex = 6;
            this.btn_change_data.Text = "更改数据";
            this.btn_change_data.UseVisualStyleBackColor = true;
            this.btn_change_data.Click += new System.EventHandler(this.btn_change_data_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(54, 545);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(148, 30);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "删除选定行";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // lbx_ID_value
            // 
            this.lbx_ID_value.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbx_ID_value.FormattingEnabled = true;
            this.lbx_ID_value.ItemHeight = 17;
            this.lbx_ID_value.Location = new System.Drawing.Point(267, 55);
            this.lbx_ID_value.Name = "lbx_ID_value";
            this.lbx_ID_value.Size = new System.Drawing.Size(178, 514);
            this.lbx_ID_value.TabIndex = 7;
            this.lbx_ID_value.SelectedIndexChanged += new System.EventHandler(this.lbx_ID_value_SelectedIndexChanged);
            // 
            // lbx_NVM_data
            // 
            this.lbx_NVM_data.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbx_NVM_data.FormattingEnabled = true;
            this.lbx_NVM_data.ItemHeight = 17;
            this.lbx_NVM_data.Location = new System.Drawing.Point(462, 55);
            this.lbx_NVM_data.Name = "lbx_NVM_data";
            this.lbx_NVM_data.Size = new System.Drawing.Size(148, 514);
            this.lbx_NVM_data.TabIndex = 8;
            this.lbx_NVM_data.SelectedIndexChanged += new System.EventHandler(this.lbx_NVM_data_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(268, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "ID,value";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(460, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "NVM data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(667, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "OEM #1";
            // 
            // Tbx_OEM1
            // 
            this.Tbx_OEM1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM1.Location = new System.Drawing.Point(734, 163);
            this.Tbx_OEM1.Name = "Tbx_OEM1";
            this.Tbx_OEM1.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM1.TabIndex = 3;
            this.Tbx_OEM1.Text = "000";
            // 
            // Tbx_OEM2
            // 
            this.Tbx_OEM2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM2.Location = new System.Drawing.Point(734, 197);
            this.Tbx_OEM2.Name = "Tbx_OEM2";
            this.Tbx_OEM2.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM2.TabIndex = 3;
            this.Tbx_OEM2.Text = "000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(667, 197);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "OEM #2";
            // 
            // Tbx_OEM3
            // 
            this.Tbx_OEM3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM3.Location = new System.Drawing.Point(733, 231);
            this.Tbx_OEM3.Name = "Tbx_OEM3";
            this.Tbx_OEM3.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM3.TabIndex = 3;
            this.Tbx_OEM3.Text = "000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(667, 234);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "OEM #3";
            // 
            // Tbx_OEM4
            // 
            this.Tbx_OEM4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM4.Location = new System.Drawing.Point(734, 265);
            this.Tbx_OEM4.Name = "Tbx_OEM4";
            this.Tbx_OEM4.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM4.TabIndex = 3;
            this.Tbx_OEM4.Text = "000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(667, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "OEM #4";
            // 
            // Tbx_OEM5
            // 
            this.Tbx_OEM5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM5.Location = new System.Drawing.Point(734, 299);
            this.Tbx_OEM5.Name = "Tbx_OEM5";
            this.Tbx_OEM5.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM5.TabIndex = 3;
            this.Tbx_OEM5.Text = "000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(667, 302);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 17);
            this.label12.TabIndex = 9;
            this.label12.Text = "OEM #5";
            // 
            // Tbx_OEM6
            // 
            this.Tbx_OEM6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM6.Location = new System.Drawing.Point(734, 333);
            this.Tbx_OEM6.Name = "Tbx_OEM6";
            this.Tbx_OEM6.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM6.TabIndex = 3;
            this.Tbx_OEM6.Text = "000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(667, 336);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 17);
            this.label13.TabIndex = 9;
            this.label13.Text = "OEM #6";
            // 
            // Tbx_OEM7
            // 
            this.Tbx_OEM7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM7.Location = new System.Drawing.Point(734, 367);
            this.Tbx_OEM7.Name = "Tbx_OEM7";
            this.Tbx_OEM7.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM7.TabIndex = 3;
            this.Tbx_OEM7.Text = "000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(667, 370);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 17);
            this.label14.TabIndex = 9;
            this.label14.Text = "OEM #7";
            // 
            // Tbx_OEM8
            // 
            this.Tbx_OEM8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tbx_OEM8.Location = new System.Drawing.Point(734, 401);
            this.Tbx_OEM8.Name = "Tbx_OEM8";
            this.Tbx_OEM8.Size = new System.Drawing.Size(72, 25);
            this.Tbx_OEM8.TabIndex = 3;
            this.Tbx_OEM8.Text = "000";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(667, 404);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 17);
            this.label15.TabIndex = 9;
            this.label15.Text = "OEM #8";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(686, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 17);
            this.label16.TabIndex = 9;
            this.label16.Text = "OEM零件号";
            // 
            // SENT_Slow_channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(842, 608);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbx_NVM_data);
            this.Controls.Add(this.Tbx_OEM8);
            this.Controls.Add(this.lbx_ID_value);
            this.Controls.Add(this.Tbx_OEM7);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.Tbx_OEM6);
            this.Controls.Add(this.btn_change_data);
            this.Controls.Add(this.Tbx_OEM5);
            this.Controls.Add(this.btn_insert_01);
            this.Controls.Add(this.Tbx_OEM4);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.Tbx_OEM3);
            this.Controls.Add(this.Tbx_OEM2);
            this.Controls.Add(this.Tbx_OEM1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SENT_Slow_channel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SENT_Slow_channel";
            this.Load += new System.EventHandler(this.SENT_Slow_channel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tb_data_code;
        private System.Windows.Forms.ComboBox Cbx_data_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_fix_data;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_insert_01;
        private System.Windows.Forms.Button btn_change_data;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.ListBox lbx_ID_value;
        private System.Windows.Forms.ListBox lbx_NVM_data;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Tbx_OEM2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Tbx_OEM3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Tbx_OEM4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Tbx_OEM5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Tbx_OEM6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Tbx_OEM7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Tbx_OEM8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox Tbx_OEM1;
    }
}