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
            this.PicQr = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicQr)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_show
            // 
            this.btn_show.Location = new System.Drawing.Point(232, 31);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(300, 28);
            this.btn_show.TabIndex = 0;
            this.btn_show.Text = "Show Qr Code";
            this.btn_show.UseVisualStyleBackColor = true;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // PicQr
            // 
            this.PicQr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicQr.Location = new System.Drawing.Point(232, 74);
            this.PicQr.Name = "PicQr";
            this.PicQr.Size = new System.Drawing.Size(300, 217);
            this.PicQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicQr.TabIndex = 1;
            this.PicQr.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PicQr);
            this.Controls.Add(this.btn_show);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PicQr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.PictureBox PicQr;
    }
}

