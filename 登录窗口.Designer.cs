namespace E520._47标定
{
    partial class Users_login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Users_name = new System.Windows.Forms.TextBox();
            this.Users_password = new System.Windows.Forms.TextBox();
            this.btn_Register = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码：";
            // 
            // Users_name
            // 
            this.Users_name.Location = new System.Drawing.Point(107, 28);
            this.Users_name.MaxLength = 6;
            this.Users_name.Name = "Users_name";
            this.Users_name.Size = new System.Drawing.Size(100, 25);
            this.Users_name.TabIndex = 1;
            this.Users_name.UseSystemPasswordChar = true;
            // 
            // Users_password
            // 
            this.Users_password.Location = new System.Drawing.Point(106, 62);
            this.Users_password.MaxLength = 6;
            this.Users_password.Name = "Users_password";
            this.Users_password.Size = new System.Drawing.Size(100, 25);
            this.Users_password.TabIndex = 1;
            this.Users_password.UseSystemPasswordChar = true;
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(47, 107);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(70, 30);
            this.btn_Register.TabIndex = 2;
            this.btn_Register.Text = "注册";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(136, 107);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(70, 30);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            // 
            // Users_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 161);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.Users_password);
            this.Controls.Add(this.Users_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Users_login";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Users_name;
        private System.Windows.Forms.TextBox Users_password;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Button btn_login;
    }
}