namespace Book_of_the_Dead
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.ControlBox = new System.Windows.Forms.TextBox();
            this.Player1Label = new System.Windows.Forms.Label();
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.Player2Label = new System.Windows.Forms.Label();
            this.MonsterLabel = new System.Windows.Forms.Label();
            this.Player3Label = new System.Windows.Forms.Label();
            this.Player4Label = new System.Windows.Forms.Label();
            this.PowerUpTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ControlBox
            // 
            this.ControlBox.Location = new System.Drawing.Point(-24, -24);
            this.ControlBox.Name = "ControlBox";
            this.ControlBox.ReadOnly = true;
            this.ControlBox.Size = new System.Drawing.Size(0, 20);
            this.ControlBox.TabIndex = 0;
            this.ControlBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlBox_KeyDown);
            this.ControlBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ControlBox_KeyUp);
            // 
            // Player1Label
            // 
            this.Player1Label.BackColor = System.Drawing.Color.Transparent;
            this.Player1Label.Image = ((System.Drawing.Image)(resources.GetObject("Player1Label.Image")));
            this.Player1Label.Location = new System.Drawing.Point(293, 173);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(16, 16);
            this.Player1Label.TabIndex = 3;
            this.Player1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player1Label.Visible = false;
            // 
            // MoveTimer
            // 
            this.MoveTimer.Enabled = true;
            this.MoveTimer.Interval = 1;
            this.MoveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // Player2Label
            // 
            this.Player2Label.BackColor = System.Drawing.Color.Transparent;
            this.Player2Label.Image = ((System.Drawing.Image)(resources.GetObject("Player2Label.Image")));
            this.Player2Label.Location = new System.Drawing.Point(595, 399);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(16, 16);
            this.Player2Label.TabIndex = 4;
            this.Player2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player2Label.Visible = false;
            // 
            // MonsterLabel
            // 
            this.MonsterLabel.BackColor = System.Drawing.Color.Red;
            this.MonsterLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MonsterLabel.Image = ((System.Drawing.Image)(resources.GetObject("MonsterLabel.Image")));
            this.MonsterLabel.Location = new System.Drawing.Point(474, 284);
            this.MonsterLabel.Name = "MonsterLabel";
            this.MonsterLabel.Size = new System.Drawing.Size(32, 32);
            this.MonsterLabel.TabIndex = 5;
            this.MonsterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Player3Label
            // 
            this.Player3Label.BackColor = System.Drawing.Color.Transparent;
            this.Player3Label.Image = ((System.Drawing.Image)(resources.GetObject("Player3Label.Image")));
            this.Player3Label.Location = new System.Drawing.Point(595, 173);
            this.Player3Label.Name = "Player3Label";
            this.Player3Label.Size = new System.Drawing.Size(16, 16);
            this.Player3Label.TabIndex = 6;
            this.Player3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player3Label.Visible = false;
            // 
            // Player4Label
            // 
            this.Player4Label.BackColor = System.Drawing.Color.Transparent;
            this.Player4Label.Image = ((System.Drawing.Image)(resources.GetObject("Player4Label.Image")));
            this.Player4Label.Location = new System.Drawing.Point(293, 399);
            this.Player4Label.Name = "Player4Label";
            this.Player4Label.Size = new System.Drawing.Size(16, 16);
            this.Player4Label.TabIndex = 7;
            this.Player4Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Player4Label.Visible = false;
            // 
            // PowerUpTimer
            // 
            this.PowerUpTimer.Enabled = true;
            this.PowerUpTimer.Interval = 1000;
            this.PowerUpTimer.Tick += new System.EventHandler(this.PowerUpTimer_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(940, 757);
            this.Controls.Add(this.Player4Label);
            this.Controls.Add(this.Player3Label);
            this.Controls.Add(this.MonsterLabel);
            this.Controls.Add(this.Player2Label);
            this.Controls.Add(this.Player1Label);
            this.Controls.Add(this.ControlBox);
            this.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(960, 800);
            this.MinimumSize = new System.Drawing.Size(960, 800);
            this.Name = "Form2";
            this.Text = "Tuat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ControlBox;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Timer MoveTimer;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Label MonsterLabel;
        private System.Windows.Forms.Label Player3Label;
        private System.Windows.Forms.Label Player4Label;
        private System.Windows.Forms.Timer PowerUpTimer;
    }
}