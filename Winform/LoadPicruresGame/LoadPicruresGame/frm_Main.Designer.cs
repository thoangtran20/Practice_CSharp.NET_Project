namespace LoadPicturesGame
{
    partial class frm_Main
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
            components = new System.ComponentModel.Container();
            btnRestart = new Button();
            lblStatus = new Label();
            lblTimeLeft = new Label();
            GameTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // btnRestart
            // 
            btnRestart.BackColor = Color.Coral;
            btnRestart.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRestart.ForeColor = SystemColors.ButtonHighlight;
            btnRestart.Location = new Point(299, 32);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(113, 40);
            btnRestart.TabIndex = 0;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(299, 104);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(47, 20);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "status";
            // 
            // lblTimeLeft
            // 
            lblTimeLeft.AutoSize = true;
            lblTimeLeft.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblTimeLeft.Location = new Point(299, 153);
            lblTimeLeft.Name = "lblTimeLeft";
            lblTimeLeft.Size = new Size(122, 25);
            lblTimeLeft.TabIndex = 2;
            lblTimeLeft.Text = "Time Left: 30";
            // 
            // GameTimer
            // 
            GameTimer.Interval = 1000;
            GameTimer.Tick += GameTimer_Tick;
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTimeLeft);
            Controls.Add(lblStatus);
            Controls.Add(btnRestart);
            Name = "frm_Main";
            Text = "Picture Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRestart;
        private Label lblStatus;
        private Label lblTimeLeft;
        private System.Windows.Forms.Timer GameTimer;
    }
}