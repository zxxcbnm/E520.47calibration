namespace E520._47标定
{
    partial class verify
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listView_sample = new System.Windows.Forms.ListView();
            this.kong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xulie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID_1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p1_s = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p2_s = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T1_s = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T2_s = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p1_err = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p2_err = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T1_err = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.T2_err = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FC11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FC22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.enabled_channel = new System.Windows.Forms.ComboBox();
            this.CBX_MUX0 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX1 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX2 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX3 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX4 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX5 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX6 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX7 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX8 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX9 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX10 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX11 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX12 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX13 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX14 = new System.Windows.Forms.CheckBox();
            this.CBX_MUX15 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_cishu = new System.Windows.Forms.TextBox();
            this.btn_sample1 = new System.Windows.Forms.Button();
            this.btn_continuously_read = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::E520._47标定.Properties.Resources.BM;
            this.pictureBox2.Location = new System.Drawing.Point(863, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(158, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(946, 570);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 26);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "退出";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(425, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 21;
            this.label1.Text = "校准后测试";
            // 
            // listView_sample
            // 
            this.listView_sample.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.kong,
            this.xulie,
            this.ID_1,
            this.p1,
            this.p2,
            this.T1,
            this.T2,
            this.p1_s,
            this.p2_s,
            this.T1_s,
            this.T2_s,
            this.p1_err,
            this.p2_err,
            this.T1_err,
            this.T2_err,
            this.FC11,
            this.FC22});
            this.listView_sample.GridLines = true;
            this.listView_sample.HideSelection = false;
            this.listView_sample.Location = new System.Drawing.Point(94, 81);
            this.listView_sample.Name = "listView_sample";
            this.listView_sample.Size = new System.Drawing.Size(804, 391);
            this.listView_sample.TabIndex = 37;
            this.listView_sample.UseCompatibleStateImageBehavior = false;
            this.listView_sample.View = System.Windows.Forms.View.Details;
            // 
            // kong
            // 
            this.kong.Width = 1;
            // 
            // xulie
            // 
            this.xulie.Text = "序";
            this.xulie.Width = 35;
            // 
            // ID_1
            // 
            this.ID_1.Text = "ID";
            this.ID_1.Width = 85;
            // 
            // p1
            // 
            this.p1.Text = "p1";
            this.p1.Width = 52;
            // 
            // p2
            // 
            this.p2.Text = "p2";
            this.p2.Width = 52;
            // 
            // T1
            // 
            this.T1.Text = "T1";
            this.T1.Width = 52;
            // 
            // T2
            // 
            this.T2.Text = "T2";
            this.T2.Width = 52;
            // 
            // p1_s
            // 
            this.p1_s.Text = "p1";
            this.p1_s.Width = 45;
            // 
            // p2_s
            // 
            this.p2_s.Text = "p2";
            this.p2_s.Width = 45;
            // 
            // T1_s
            // 
            this.T1_s.Text = "T1";
            this.T1_s.Width = 45;
            // 
            // T2_s
            // 
            this.T2_s.Text = "T2";
            this.T2_s.Width = 45;
            // 
            // p1_err
            // 
            this.p1_err.Text = "p1";
            this.p1_err.Width = 45;
            // 
            // p2_err
            // 
            this.p2_err.Text = "p2";
            this.p2_err.Width = 45;
            // 
            // T1_err
            // 
            this.T1_err.Text = "T1";
            this.T1_err.Width = 45;
            // 
            // T2_err
            // 
            this.T2_err.Text = "T2";
            this.T2_err.Width = 45;
            // 
            // FC11
            // 
            this.FC11.Text = "FC1";
            this.FC11.Width = 52;
            // 
            // FC22
            // 
            this.FC22.Text = "FC2";
            this.FC22.Width = 52;
            // 
            // enabled_channel
            // 
            this.enabled_channel.FormattingEnabled = true;
            this.enabled_channel.Location = new System.Drawing.Point(153, 526);
            this.enabled_channel.Name = "enabled_channel";
            this.enabled_channel.Size = new System.Drawing.Size(96, 25);
            this.enabled_channel.TabIndex = 54;
            this.enabled_channel.Visible = false;
            // 
            // CBX_MUX0
            // 
            this.CBX_MUX0.AutoSize = true;
            this.CBX_MUX0.Location = new System.Drawing.Point(33, 104);
            this.CBX_MUX0.Name = "CBX_MUX0";
            this.CBX_MUX0.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX0.TabIndex = 52;
            this.CBX_MUX0.Text = "0";
            this.CBX_MUX0.UseVisualStyleBackColor = true;
            this.CBX_MUX0.CheckedChanged += new System.EventHandler(this.CBX_MUX0_CheckedChanged);
            // 
            // CBX_MUX1
            // 
            this.CBX_MUX1.AutoSize = true;
            this.CBX_MUX1.Location = new System.Drawing.Point(33, 125);
            this.CBX_MUX1.Name = "CBX_MUX1";
            this.CBX_MUX1.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX1.TabIndex = 51;
            this.CBX_MUX1.Text = "1";
            this.CBX_MUX1.UseVisualStyleBackColor = true;
            this.CBX_MUX1.CheckedChanged += new System.EventHandler(this.CBX_MUX1_CheckedChanged);
            // 
            // CBX_MUX2
            // 
            this.CBX_MUX2.AutoSize = true;
            this.CBX_MUX2.Location = new System.Drawing.Point(33, 146);
            this.CBX_MUX2.Name = "CBX_MUX2";
            this.CBX_MUX2.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX2.TabIndex = 50;
            this.CBX_MUX2.Text = "2";
            this.CBX_MUX2.UseVisualStyleBackColor = true;
            this.CBX_MUX2.CheckedChanged += new System.EventHandler(this.CBX_MUX2_CheckedChanged);
            // 
            // CBX_MUX3
            // 
            this.CBX_MUX3.AutoSize = true;
            this.CBX_MUX3.Location = new System.Drawing.Point(33, 167);
            this.CBX_MUX3.Name = "CBX_MUX3";
            this.CBX_MUX3.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX3.TabIndex = 49;
            this.CBX_MUX3.Text = "3";
            this.CBX_MUX3.UseVisualStyleBackColor = true;
            this.CBX_MUX3.CheckedChanged += new System.EventHandler(this.CBX_MUX3_CheckedChanged);
            // 
            // CBX_MUX4
            // 
            this.CBX_MUX4.AutoSize = true;
            this.CBX_MUX4.Location = new System.Drawing.Point(33, 188);
            this.CBX_MUX4.Name = "CBX_MUX4";
            this.CBX_MUX4.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX4.TabIndex = 48;
            this.CBX_MUX4.Text = "4";
            this.CBX_MUX4.UseVisualStyleBackColor = true;
            this.CBX_MUX4.CheckedChanged += new System.EventHandler(this.CBX_MUX4_CheckedChanged);
            // 
            // CBX_MUX5
            // 
            this.CBX_MUX5.AutoSize = true;
            this.CBX_MUX5.Location = new System.Drawing.Point(33, 209);
            this.CBX_MUX5.Name = "CBX_MUX5";
            this.CBX_MUX5.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX5.TabIndex = 47;
            this.CBX_MUX5.Text = "5";
            this.CBX_MUX5.UseVisualStyleBackColor = true;
            this.CBX_MUX5.CheckedChanged += new System.EventHandler(this.CBX_MUX5_CheckedChanged);
            // 
            // CBX_MUX6
            // 
            this.CBX_MUX6.AutoSize = true;
            this.CBX_MUX6.Location = new System.Drawing.Point(33, 230);
            this.CBX_MUX6.Name = "CBX_MUX6";
            this.CBX_MUX6.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX6.TabIndex = 53;
            this.CBX_MUX6.Text = "6";
            this.CBX_MUX6.UseVisualStyleBackColor = true;
            this.CBX_MUX6.CheckedChanged += new System.EventHandler(this.CBX_MUX6_CheckedChanged);
            // 
            // CBX_MUX7
            // 
            this.CBX_MUX7.AutoSize = true;
            this.CBX_MUX7.Location = new System.Drawing.Point(33, 251);
            this.CBX_MUX7.Name = "CBX_MUX7";
            this.CBX_MUX7.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX7.TabIndex = 38;
            this.CBX_MUX7.Text = "7";
            this.CBX_MUX7.UseVisualStyleBackColor = true;
            this.CBX_MUX7.CheckedChanged += new System.EventHandler(this.CBX_MUX7_CheckedChanged);
            // 
            // CBX_MUX8
            // 
            this.CBX_MUX8.AutoSize = true;
            this.CBX_MUX8.Location = new System.Drawing.Point(33, 272);
            this.CBX_MUX8.Name = "CBX_MUX8";
            this.CBX_MUX8.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX8.TabIndex = 44;
            this.CBX_MUX8.Text = "8";
            this.CBX_MUX8.UseVisualStyleBackColor = true;
            this.CBX_MUX8.CheckedChanged += new System.EventHandler(this.CBX_MUX8_CheckedChanged);
            // 
            // CBX_MUX9
            // 
            this.CBX_MUX9.AutoSize = true;
            this.CBX_MUX9.Location = new System.Drawing.Point(33, 293);
            this.CBX_MUX9.Name = "CBX_MUX9";
            this.CBX_MUX9.Size = new System.Drawing.Size(38, 21);
            this.CBX_MUX9.TabIndex = 43;
            this.CBX_MUX9.Text = "9";
            this.CBX_MUX9.UseVisualStyleBackColor = true;
            this.CBX_MUX9.CheckedChanged += new System.EventHandler(this.CBX_MUX9_CheckedChanged);
            // 
            // CBX_MUX10
            // 
            this.CBX_MUX10.AutoSize = true;
            this.CBX_MUX10.Location = new System.Drawing.Point(33, 314);
            this.CBX_MUX10.Name = "CBX_MUX10";
            this.CBX_MUX10.Size = new System.Drawing.Size(46, 21);
            this.CBX_MUX10.TabIndex = 42;
            this.CBX_MUX10.Text = "10";
            this.CBX_MUX10.UseVisualStyleBackColor = true;
            this.CBX_MUX10.CheckedChanged += new System.EventHandler(this.CBX_MUX10_CheckedChanged);
            // 
            // CBX_MUX11
            // 
            this.CBX_MUX11.AutoSize = true;
            this.CBX_MUX11.Location = new System.Drawing.Point(33, 335);
            this.CBX_MUX11.Name = "CBX_MUX11";
            this.CBX_MUX11.Size = new System.Drawing.Size(45, 21);
            this.CBX_MUX11.TabIndex = 41;
            this.CBX_MUX11.Text = "11";
            this.CBX_MUX11.UseVisualStyleBackColor = true;
            this.CBX_MUX11.CheckedChanged += new System.EventHandler(this.CBX_MUX11_CheckedChanged);
            // 
            // CBX_MUX12
            // 
            this.CBX_MUX12.AutoSize = true;
            this.CBX_MUX12.Location = new System.Drawing.Point(33, 356);
            this.CBX_MUX12.Name = "CBX_MUX12";
            this.CBX_MUX12.Size = new System.Drawing.Size(46, 21);
            this.CBX_MUX12.TabIndex = 40;
            this.CBX_MUX12.Text = "12";
            this.CBX_MUX12.UseVisualStyleBackColor = true;
            this.CBX_MUX12.CheckedChanged += new System.EventHandler(this.CBX_MUX12_CheckedChanged);
            // 
            // CBX_MUX13
            // 
            this.CBX_MUX13.AutoSize = true;
            this.CBX_MUX13.Location = new System.Drawing.Point(33, 377);
            this.CBX_MUX13.Name = "CBX_MUX13";
            this.CBX_MUX13.Size = new System.Drawing.Size(46, 21);
            this.CBX_MUX13.TabIndex = 39;
            this.CBX_MUX13.Text = "13";
            this.CBX_MUX13.UseVisualStyleBackColor = true;
            this.CBX_MUX13.CheckedChanged += new System.EventHandler(this.CBX_MUX13_CheckedChanged);
            // 
            // CBX_MUX14
            // 
            this.CBX_MUX14.AutoSize = true;
            this.CBX_MUX14.Location = new System.Drawing.Point(33, 398);
            this.CBX_MUX14.Name = "CBX_MUX14";
            this.CBX_MUX14.Size = new System.Drawing.Size(46, 21);
            this.CBX_MUX14.TabIndex = 45;
            this.CBX_MUX14.Text = "14";
            this.CBX_MUX14.UseVisualStyleBackColor = true;
            this.CBX_MUX14.CheckedChanged += new System.EventHandler(this.CBX_MUX14_CheckedChanged);
            // 
            // CBX_MUX15
            // 
            this.CBX_MUX15.AutoSize = true;
            this.CBX_MUX15.Location = new System.Drawing.Point(33, 419);
            this.CBX_MUX15.Name = "CBX_MUX15";
            this.CBX_MUX15.Size = new System.Drawing.Size(46, 21);
            this.CBX_MUX15.TabIndex = 46;
            this.CBX_MUX15.Text = "15";
            this.CBX_MUX15.UseVisualStyleBackColor = true;
            this.CBX_MUX15.CheckedChanged += new System.EventHandler(this.CBX_MUX15_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(769, 490);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 58;
            this.label7.Text = "次";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(639, 490);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 17);
            this.label6.TabIndex = 57;
            this.label6.Text = "采样次数：";
            // 
            // tbx_cishu
            // 
            this.tbx_cishu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_cishu.Location = new System.Drawing.Point(715, 485);
            this.tbx_cishu.Name = "tbx_cishu";
            this.tbx_cishu.Size = new System.Drawing.Size(48, 25);
            this.tbx_cishu.TabIndex = 56;
            this.tbx_cishu.Text = "4";
            this.tbx_cishu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbx_cishu.TextChanged += new System.EventHandler(this.tbx_cishu_TextChanged);
            // 
            // btn_sample1
            // 
            this.btn_sample1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sample1.Location = new System.Drawing.Point(684, 526);
            this.btn_sample1.Name = "btn_sample1";
            this.btn_sample1.Size = new System.Drawing.Size(93, 35);
            this.btn_sample1.TabIndex = 55;
            this.btn_sample1.Text = "读取";
            this.btn_sample1.UseVisualStyleBackColor = true;
            this.btn_sample1.Click += new System.EventHandler(this.btn_sample1_Click);
            // 
            // btn_continuously_read
            // 
            this.btn_continuously_read.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_continuously_read.Location = new System.Drawing.Point(793, 526);
            this.btn_continuously_read.Name = "btn_continuously_read";
            this.btn_continuously_read.Size = new System.Drawing.Size(93, 35);
            this.btn_continuously_read.TabIndex = 55;
            this.btn_continuously_read.Text = "连续读";
            this.btn_continuously_read.UseVisualStyleBackColor = true;
            this.btn_continuously_read.Click += new System.EventHandler(this.btn_continuously_read_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 59;
            this.label2.Text = "采样值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "sent值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(602, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "实际值：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(790, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 59;
            this.label5.Text = "快通道：";
            // 
            // btn_clear
            // 
            this.btn_clear.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_clear.Location = new System.Drawing.Point(571, 526);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(93, 35);
            this.btn_clear.TabIndex = 55;
            this.btn_clear.Text = "清屏";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // verify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 608);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbx_cishu);
            this.Controls.Add(this.btn_continuously_read);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_sample1);
            this.Controls.Add(this.enabled_channel);
            this.Controls.Add(this.CBX_MUX0);
            this.Controls.Add(this.CBX_MUX1);
            this.Controls.Add(this.CBX_MUX2);
            this.Controls.Add(this.CBX_MUX3);
            this.Controls.Add(this.CBX_MUX4);
            this.Controls.Add(this.CBX_MUX5);
            this.Controls.Add(this.CBX_MUX6);
            this.Controls.Add(this.CBX_MUX7);
            this.Controls.Add(this.CBX_MUX8);
            this.Controls.Add(this.CBX_MUX9);
            this.Controls.Add(this.CBX_MUX10);
            this.Controls.Add(this.CBX_MUX11);
            this.Controls.Add(this.CBX_MUX12);
            this.Controls.Add(this.CBX_MUX13);
            this.Controls.Add(this.CBX_MUX14);
            this.Controls.Add(this.CBX_MUX15);
            this.Controls.Add(this.listView_sample);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "verify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "verify";
            this.Load += new System.EventHandler(this.verify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView_sample;
        private System.Windows.Forms.ColumnHeader kong;
        private System.Windows.Forms.ColumnHeader xulie;
        private System.Windows.Forms.ColumnHeader ID_1;
        private System.Windows.Forms.ColumnHeader p1;
        private System.Windows.Forms.ColumnHeader p2;
        private System.Windows.Forms.ColumnHeader T1;
        private System.Windows.Forms.ColumnHeader T2;
        private System.Windows.Forms.ColumnHeader p1_s;
        private System.Windows.Forms.ColumnHeader p2_s;
        private System.Windows.Forms.ColumnHeader T1_s;
        private System.Windows.Forms.ColumnHeader T2_s;
        private System.Windows.Forms.ColumnHeader p1_err;
        private System.Windows.Forms.ColumnHeader p2_err;
        private System.Windows.Forms.ColumnHeader T1_err;
        private System.Windows.Forms.ColumnHeader T2_err;
        private System.Windows.Forms.ComboBox enabled_channel;
        private System.Windows.Forms.CheckBox CBX_MUX0;
        private System.Windows.Forms.CheckBox CBX_MUX1;
        private System.Windows.Forms.CheckBox CBX_MUX2;
        private System.Windows.Forms.CheckBox CBX_MUX3;
        private System.Windows.Forms.CheckBox CBX_MUX4;
        private System.Windows.Forms.CheckBox CBX_MUX5;
        private System.Windows.Forms.CheckBox CBX_MUX6;
        private System.Windows.Forms.CheckBox CBX_MUX7;
        private System.Windows.Forms.CheckBox CBX_MUX8;
        private System.Windows.Forms.CheckBox CBX_MUX9;
        private System.Windows.Forms.CheckBox CBX_MUX10;
        private System.Windows.Forms.CheckBox CBX_MUX11;
        private System.Windows.Forms.CheckBox CBX_MUX12;
        private System.Windows.Forms.CheckBox CBX_MUX13;
        private System.Windows.Forms.CheckBox CBX_MUX14;
        private System.Windows.Forms.CheckBox CBX_MUX15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbx_cishu;
        private System.Windows.Forms.Button btn_sample1;
        private System.Windows.Forms.Button btn_continuously_read;
        private System.Windows.Forms.ColumnHeader FC11;
        private System.Windows.Forms.ColumnHeader FC22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_clear;
    }
}