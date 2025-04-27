namespace Demand_Paging
{
    partial class Cover
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
            panel1 = new Panel();
            button1 = new Button();
            closeBtn = new Button();
            coverPic = new PictureBox();
            continueBtn = new Krypton.Toolkit.KryptonButton();
            groupLbl = new Label();
            titleLbl2 = new Label();
            titleLbl1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)coverPic).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(15, 10, 45);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(closeBtn);
            panel1.Controls.Add(coverPic);
            panel1.Controls.Add(continueBtn);
            panel1.Controls.Add(groupLbl);
            panel1.Controls.Add(titleLbl2);
            panel1.Controls.Add(titleLbl1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1159, 646);
            panel1.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources.minimizeButton;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(142, 139, 166);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(142, 139, 166);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(1071, 0);
            button1.Name = "button1";
            button1.Size = new Size(46, 41);
            button1.TabIndex = 8;
            button1.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.Transparent;
            closeBtn.BackgroundImage = Properties.Resources.closeButton;
            closeBtn.BackgroundImageLayout = ImageLayout.Center;
            closeBtn.Cursor = Cursors.Hand;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 0, 0);
            closeBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.Location = new Point(1113, 0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(46, 41);
            closeBtn.TabIndex = 7;
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // coverPic
            // 
            coverPic.BackgroundImage = Properties.Resources.cover_illus;
            coverPic.BackgroundImageLayout = ImageLayout.Zoom;
            coverPic.Location = new Point(548, 85);
            coverPic.Name = "coverPic";
            coverPic.Size = new Size(611, 486);
            coverPic.TabIndex = 5;
            coverPic.TabStop = false;
            // 
            // continueBtn
            // 
            continueBtn.Cursor = Cursors.Hand;
            continueBtn.Location = new Point(155, 472);
            continueBtn.Name = "continueBtn";
            continueBtn.OverrideDefault.Back.Color1 = Color.FromArgb(39, 150, 255);
            continueBtn.OverrideDefault.Back.Color2 = Color.FromArgb(39, 150, 255);
            continueBtn.OverrideDefault.Border.Color1 = Color.FromArgb(39, 150, 255);
            continueBtn.OverrideDefault.Border.Color2 = Color.FromArgb(39, 150, 255);
            continueBtn.OverrideDefault.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            continueBtn.OverrideDefault.Border.Rounding = 20F;
            continueBtn.OverrideDefault.Border.Width = 1;
            continueBtn.OverrideDefault.Content.ShortText.Color1 = Color.White;
            continueBtn.OverrideDefault.Content.ShortText.Color2 = Color.White;
            continueBtn.OverrideDefault.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            continueBtn.Size = new Size(226, 42);
            continueBtn.StateCommon.Back.Color1 = Color.FromArgb(39, 150, 255);
            continueBtn.StateCommon.Back.Color2 = Color.FromArgb(39, 150, 255);
            continueBtn.StateCommon.Border.Color1 = Color.FromArgb(39, 150, 255);
            continueBtn.StateCommon.Border.Color2 = Color.FromArgb(39, 150, 255);
            continueBtn.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            continueBtn.StateCommon.Border.Rounding = 20F;
            continueBtn.StateCommon.Border.Width = 1;
            continueBtn.StateCommon.Content.ShortText.Color1 = Color.White;
            continueBtn.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            continueBtn.TabIndex = 4;
            continueBtn.Values.DropDownArrowColor = Color.Empty;
            continueBtn.Values.Text = "Continue";
            // 
            // groupLbl
            // 
            groupLbl.AutoSize = true;
            groupLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupLbl.ForeColor = Color.White;
            groupLbl.Location = new Point(74, 320);
            groupLbl.Name = "groupLbl";
            groupLbl.Size = new Size(440, 42);
            groupLbl.TabIndex = 2;
            groupLbl.Text = "Group 1, consisting of Emil Yordan, Marju Faller, Luke Umpad, \r\nand Mark Abella";
            // 
            // titleLbl2
            // 
            titleLbl2.AutoSize = true;
            titleLbl2.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLbl2.ForeColor = Color.FromArgb(0, 129, 252);
            titleLbl2.Location = new Point(65, 222);
            titleLbl2.Name = "titleLbl2";
            titleLbl2.Size = new Size(482, 65);
            titleLbl2.TabIndex = 1;
            titleLbl2.Text = "Memory Allocation";
            // 
            // titleLbl1
            // 
            titleLbl1.AutoSize = true;
            titleLbl1.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLbl1.ForeColor = Color.White;
            titleLbl1.Location = new Point(65, 157);
            titleLbl1.Name = "titleLbl1";
            titleLbl1.Size = new Size(410, 65);
            titleLbl1.TabIndex = 0;
            titleLbl1.Text = "Demand-Paging";
            // 
            // Cover
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1159, 646);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Cover";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += Cover_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)coverPic).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label titleLbl1;
        private Label titleLbl2;
        private Label groupLbl;
        private Krypton.Toolkit.KryptonButton continueBtn;
        private PictureBox coverPic;
        private Button closeBtn;
        private Button button1;
    }
}
