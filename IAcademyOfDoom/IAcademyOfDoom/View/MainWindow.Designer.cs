namespace IAcademyOfDoom.View
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.endPrepButton = new System.Windows.Forms.Button();
            this.nextInAssaultButton = new System.Windows.Forms.Button();
            this.botnumTextLabel = new System.Windows.Forms.Label();
            this.numberOfBotlingsContentLabel = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Location = new System.Drawing.Point(34, 492);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(765, 134);
            this.outputListBox.TabIndex = 0;
            // 
            // endPrepButton
            // 
            this.endPrepButton.Location = new System.Drawing.Point(833, 492);
            this.endPrepButton.Name = "endPrepButton";
            this.endPrepButton.Size = new System.Drawing.Size(162, 23);
            this.endPrepButton.TabIndex = 1;
            this.endPrepButton.Text = "End preparations";
            this.endPrepButton.UseVisualStyleBackColor = true;
            this.endPrepButton.Click += new System.EventHandler(this.EndPrepButton_Click);
            // 
            // nextInAssaultButton
            // 
            this.nextInAssaultButton.Enabled = false;
            this.nextInAssaultButton.Location = new System.Drawing.Point(833, 521);
            this.nextInAssaultButton.Name = "nextInAssaultButton";
            this.nextInAssaultButton.Size = new System.Drawing.Size(162, 23);
            this.nextInAssaultButton.TabIndex = 2;
            this.nextInAssaultButton.Text = "Assault: next";
            this.nextInAssaultButton.UseVisualStyleBackColor = true;
            this.nextInAssaultButton.Click += new System.EventHandler(this.NextInAssaultButton_Click);
            // 
            // botnumTextLabel
            // 
            this.botnumTextLabel.AutoSize = true;
            this.botnumTextLabel.Location = new System.Drawing.Point(34, 23);
            this.botnumTextLabel.Name = "botnumTextLabel";
            this.botnumTextLabel.Size = new System.Drawing.Size(98, 13);
            this.botnumTextLabel.TabIndex = 3;
            this.botnumTextLabel.Text = "Number of botlings:";
            // 
            // numberOfBotlingsContentLabel
            // 
            this.numberOfBotlingsContentLabel.AutoSize = true;
            this.numberOfBotlingsContentLabel.Location = new System.Drawing.Point(138, 23);
            this.numberOfBotlingsContentLabel.Name = "numberOfBotlingsContentLabel";
            this.numberOfBotlingsContentLabel.Size = new System.Drawing.Size(16, 13);
            this.numberOfBotlingsContentLabel.TabIndex = 4;
            this.numberOfBotlingsContentLabel.Text = "   ";
            // 
            // quitButton
            // 
            this.quitButton.Enabled = false;
            this.quitButton.Location = new System.Drawing.Point(833, 603);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(162, 23);
            this.quitButton.TabIndex = 5;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Visible = false;
            this.quitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new System.Drawing.Point(204, 23);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(28, 13);
            this.playerNameLabel.TabIndex = 6;
            this.playerNameLabel.Text = "       ";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 687);
            this.Controls.Add(this.playerNameLabel);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.numberOfBotlingsContentLabel);
            this.Controls.Add(this.botnumTextLabel);
            this.Controls.Add(this.nextInAssaultButton);
            this.Controls.Add(this.endPrepButton);
            this.Controls.Add(this.outputListBox);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Button endPrepButton;
        private System.Windows.Forms.Button nextInAssaultButton;
        private System.Windows.Forms.Label botnumTextLabel;
        private System.Windows.Forms.Label numberOfBotlingsContentLabel;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Label playerNameLabel;
    }
}

