namespace GeeSuthSoft.KSA.ZATCA.WinFormTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pic = new PictureBox();
            btn_loadQR = new Button();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // pic
            // 
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Location = new Point(41, 24);
            pic.Name = "pic";
            pic.Size = new Size(308, 264);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            // 
            // btn_loadQR
            // 
            btn_loadQR.Location = new Point(41, 294);
            btn_loadQR.Name = "btn_loadQR";
            btn_loadQR.Size = new Size(101, 23);
            btn_loadQR.TabIndex = 1;
            btn_loadQR.Text = "Load Test QR";
            btn_loadQR.UseVisualStyleBackColor = true;
            btn_loadQR.Click += btn_loadQR_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_loadQR);
            Controls.Add(pic);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pic;
        private Button btn_loadQR;
    }
}
