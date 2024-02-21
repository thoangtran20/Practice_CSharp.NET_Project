namespace Calculator_Winform_App
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main));
            txtResult = new TextBox();
            btn7 = new Button();
            btn8 = new Button();
            btn9 = new Button();
            btnDivide = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnMultiple = new Button();
            btn6 = new Button();
            btn5 = new Button();
            btn4 = new Button();
            btnEqual = new Button();
            btnDash = new Button();
            btn3 = new Button();
            btn2 = new Button();
            btn1 = new Button();
            btn0 = new Button();
            btnSemi = new Button();
            btnAdd = new Button();
            lblCurentOperation = new Label();
            SuspendLayout();
            // 
            // txtResult
            // 
            txtResult.BackColor = Color.Sienna;
            txtResult.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtResult.ForeColor = SystemColors.InactiveBorder;
            txtResult.Location = new Point(111, 68);
            txtResult.Name = "txtResult";
            txtResult.RightToLeft = RightToLeft.No;
            txtResult.Size = new Size(259, 31);
            txtResult.TabIndex = 0;
            txtResult.Text = "0";
            txtResult.TextAlign = HorizontalAlignment.Right;
            // 
            // btn7
            // 
            btn7.BackColor = Color.LightCoral;
            btn7.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn7.ForeColor = SystemColors.ControlLightLight;
            btn7.Location = new Point(111, 105);
            btn7.Name = "btn7";
            btn7.Size = new Size(47, 43);
            btn7.TabIndex = 1;
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = false;
            btn7.Click += button_click;
            // 
            // btn8
            // 
            btn8.BackColor = Color.LightCoral;
            btn8.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn8.ForeColor = SystemColors.ControlLightLight;
            btn8.Location = new Point(164, 105);
            btn8.Name = "btn8";
            btn8.Size = new Size(47, 43);
            btn8.TabIndex = 2;
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = false;
            btn8.Click += button_click;
            // 
            // btn9
            // 
            btn9.BackColor = Color.LightCoral;
            btn9.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn9.ForeColor = SystemColors.ControlLightLight;
            btn9.Location = new Point(217, 105);
            btn9.Name = "btn9";
            btn9.Size = new Size(47, 43);
            btn9.TabIndex = 3;
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = false;
            btn9.Click += button_click;
            // 
            // btnDivide
            // 
            btnDivide.BackColor = Color.LightCoral;
            btnDivide.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnDivide.ForeColor = SystemColors.ControlLightLight;
            btnDivide.Location = new Point(270, 105);
            btnDivide.Name = "btnDivide";
            btnDivide.Size = new Size(47, 43);
            btnDivide.TabIndex = 4;
            btnDivide.Text = "/";
            btnDivide.UseVisualStyleBackColor = false;
            btnDivide.Click += operator_click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.LightCoral;
            btnClear.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnClear.ForeColor = SystemColors.ControlLightLight;
            btnClear.Location = new Point(323, 105);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(47, 43);
            btnClear.TabIndex = 5;
            btnClear.Text = "CE";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnDelete.ForeColor = SystemColors.ControlLightLight;
            btnDelete.Location = new Point(323, 154);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(47, 43);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "C";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnMultiple
            // 
            btnMultiple.BackColor = Color.LightCoral;
            btnMultiple.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnMultiple.ForeColor = SystemColors.ControlLightLight;
            btnMultiple.Location = new Point(270, 154);
            btnMultiple.Name = "btnMultiple";
            btnMultiple.Size = new Size(47, 43);
            btnMultiple.TabIndex = 9;
            btnMultiple.Text = "*";
            btnMultiple.UseVisualStyleBackColor = false;
            btnMultiple.Click += operator_click;
            // 
            // btn6
            // 
            btn6.BackColor = Color.LightCoral;
            btn6.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn6.ForeColor = SystemColors.ControlLightLight;
            btn6.Location = new Point(217, 154);
            btn6.Name = "btn6";
            btn6.Size = new Size(47, 43);
            btn6.TabIndex = 8;
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = false;
            btn6.Click += button_click;
            // 
            // btn5
            // 
            btn5.BackColor = Color.LightCoral;
            btn5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn5.ForeColor = SystemColors.ControlLightLight;
            btn5.Location = new Point(164, 154);
            btn5.Name = "btn5";
            btn5.Size = new Size(47, 43);
            btn5.TabIndex = 7;
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = false;
            btn5.Click += button_click;
            // 
            // btn4
            // 
            btn4.BackColor = Color.LightCoral;
            btn4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn4.ForeColor = SystemColors.ControlLightLight;
            btn4.Location = new Point(111, 154);
            btn4.Name = "btn4";
            btn4.Size = new Size(47, 43);
            btn4.TabIndex = 6;
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = false;
            btn4.Click += button_click;
            // 
            // btnEqual
            // 
            btnEqual.BackColor = Color.LightCoral;
            btnEqual.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnEqual.ForeColor = SystemColors.ControlLightLight;
            btnEqual.Location = new Point(323, 203);
            btnEqual.Name = "btnEqual";
            btnEqual.Size = new Size(47, 92);
            btnEqual.TabIndex = 15;
            btnEqual.Text = "=";
            btnEqual.UseVisualStyleBackColor = false;
            btnEqual.Click += btnEqual_Click;
            // 
            // btnDash
            // 
            btnDash.BackColor = Color.LightCoral;
            btnDash.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnDash.ForeColor = SystemColors.ControlLightLight;
            btnDash.Location = new Point(270, 203);
            btnDash.Name = "btnDash";
            btnDash.Size = new Size(47, 43);
            btnDash.TabIndex = 14;
            btnDash.Text = "-";
            btnDash.UseVisualStyleBackColor = false;
            btnDash.Click += operator_click;
            // 
            // btn3
            // 
            btn3.BackColor = Color.LightCoral;
            btn3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn3.ForeColor = SystemColors.ControlLightLight;
            btn3.Location = new Point(217, 203);
            btn3.Name = "btn3";
            btn3.Size = new Size(47, 43);
            btn3.TabIndex = 13;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = false;
            btn3.Click += button_click;
            // 
            // btn2
            // 
            btn2.BackColor = Color.LightCoral;
            btn2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn2.ForeColor = SystemColors.ControlLightLight;
            btn2.Location = new Point(164, 203);
            btn2.Name = "btn2";
            btn2.Size = new Size(47, 43);
            btn2.TabIndex = 12;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = false;
            btn2.Click += button_click;
            // 
            // btn1
            // 
            btn1.BackColor = Color.LightCoral;
            btn1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn1.ForeColor = SystemColors.ControlLightLight;
            btn1.Location = new Point(111, 203);
            btn1.Name = "btn1";
            btn1.Size = new Size(47, 43);
            btn1.TabIndex = 11;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += button_click;
            // 
            // btn0
            // 
            btn0.BackColor = Color.LightCoral;
            btn0.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btn0.ForeColor = SystemColors.ControlLightLight;
            btn0.Location = new Point(111, 252);
            btn0.Name = "btn0";
            btn0.Size = new Size(100, 43);
            btn0.TabIndex = 16;
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = false;
            btn0.Click += button_click;
            // 
            // btnSemi
            // 
            btnSemi.BackColor = Color.LightCoral;
            btnSemi.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSemi.ForeColor = SystemColors.ControlLightLight;
            btnSemi.Location = new Point(217, 252);
            btnSemi.Name = "btnSemi";
            btnSemi.Size = new Size(47, 43);
            btnSemi.TabIndex = 17;
            btnSemi.Text = ".";
            btnSemi.UseVisualStyleBackColor = false;
            btnSemi.Click += button_click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightCoral;
            btnAdd.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdd.ForeColor = SystemColors.ControlLightLight;
            btnAdd.Location = new Point(270, 252);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(47, 43);
            btnAdd.TabIndex = 18;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += operator_click;
            // 
            // lblCurentOperation
            // 
            lblCurentOperation.AutoSize = true;
            lblCurentOperation.BackColor = SystemColors.Window;
            lblCurentOperation.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblCurentOperation.ForeColor = SystemColors.ControlDark;
            lblCurentOperation.Location = new Point(111, 32);
            lblCurentOperation.Name = "lblCurentOperation";
            lblCurentOperation.Size = new Size(0, 25);
            lblCurentOperation.TabIndex = 19;
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 367);
            Controls.Add(lblCurentOperation);
            Controls.Add(btnAdd);
            Controls.Add(btnSemi);
            Controls.Add(btn0);
            Controls.Add(btnEqual);
            Controls.Add(btnDash);
            Controls.Add(btn3);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Controls.Add(btnDelete);
            Controls.Add(btnMultiple);
            Controls.Add(btn6);
            Controls.Add(btn5);
            Controls.Add(btn4);
            Controls.Add(btnClear);
            Controls.Add(btnDivide);
            Controls.Add(btn9);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(txtResult);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frm_Main";
            Text = "Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtResult;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btnDivide;
        private Button btnClear;
        private Button btnDelete;
        private Button btnMultiple;
        private Button btn6;
        private Button btn5;
        private Button btn4;
        private Button btnEqual;
        private Button btnDash;
        private Button btn3;
        private Button btn2;
        private Button btn1;
        private Button btn0;
        private Button btnSemi;
        private Button btnAdd;
        private Label lblCurentOperation;
    }
}