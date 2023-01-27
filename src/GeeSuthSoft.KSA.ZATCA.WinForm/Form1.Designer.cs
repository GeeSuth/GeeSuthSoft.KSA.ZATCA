namespace GeeSuthSoft.KSA.ZATCA.WinForm
{
    partial class Form1
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
            this.btn_show = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_new = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_base64 = new System.Windows.Forms.TextBox();
            this.linkOpenInBroswer = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pic_new)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_show
            // 
            this.btn_show.Location = new System.Drawing.Point(12, 11);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(776, 28);
            this.btn_show.TabIndex = 0;
            this.btn_show.Text = "Generate";
            this.btn_show.UseVisualStyleBackColor = true;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Image :";
            // 
            // pic_new
            // 
            this.pic_new.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_new.Location = new System.Drawing.Point(12, 61);
            this.pic_new.Name = "pic_new";
            this.pic_new.Size = new System.Drawing.Size(300, 217);
            this.pic_new.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_new.TabIndex = 4;
            this.pic_new.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Base64 :";
            // 
            // txt_base64
            // 
            this.txt_base64.Location = new System.Drawing.Point(318, 61);
            this.txt_base64.Multiline = true;
            this.txt_base64.Name = "txt_base64";
            this.txt_base64.ReadOnly = true;
            this.txt_base64.Size = new System.Drawing.Size(470, 217);
            this.txt_base64.TabIndex = 6;
            // 
            // linkOpenInBroswer
            // 
            this.linkOpenInBroswer.AutoSize = true;
            this.linkOpenInBroswer.Location = new System.Drawing.Point(12, 298);
            this.linkOpenInBroswer.Name = "linkOpenInBroswer";
            this.linkOpenInBroswer.Size = new System.Drawing.Size(86, 13);
            this.linkOpenInBroswer.TabIndex = 7;
            this.linkOpenInBroswer.TabStop = true;
            this.linkOpenInBroswer.Text = "Open In Browser";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.linkOpenInBroswer);
            this.Controls.Add(this.txt_base64);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pic_new);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_show);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic_new)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_new;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_base64;
        private System.Windows.Forms.LinkLabel linkOpenInBroswer;
    }
}

