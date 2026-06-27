namespace Zombie_Shooter
{
    partial class end_game
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(191, 82);
            label1.Name = "label1";
            label1.Size = new Size(168, 50);
            label1.TabIndex = 0;
            label1.Text = "You Lost";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            label2.Location = new Point(209, 194);
            label2.Name = "label2";
            label2.Size = new Size(111, 30);
            label2.TabIndex = 0;
            label2.Text = "Your Kills:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            label3.Location = new Point(320, 194);
            label3.Name = "label3";
            label3.Size = new Size(22, 30);
            label3.TabIndex = 0;
            label3.Text = "?";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            button1.Location = new Point(181, 298);
            button1.Name = "button1";
            button1.Size = new Size(218, 44);
            button1.TabIndex = 1;
            button1.Text = "Restart Game";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // end_game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(555, 450);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "end_game";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "end_game";
            Load += end_game_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
    }
}