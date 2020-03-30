namespace DurakGameUI
{
    partial class frmDurak
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
            this.lblStatistics = new System.Windows.Forms.Label();
            this.lblNumOfGames = new System.Windows.Forms.Label();
            this.lblNumOfWins = new System.Windows.Forms.Label();
            this.lblNumOfLosses = new System.Windows.Forms.Label();
            this.lblNumOfDraws = new System.Windows.Forms.Label();
            this.lblGamesPlayed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPlayArea = new System.Windows.Forms.Panel();
            this.lblDeck = new System.Windows.Forms.Label();
            this.pnlPlayerHand = new System.Windows.Forms.Panel();
            this.lblYourHand = new System.Windows.Forms.Label();
            this.pnlCPUHand = new System.Windows.Forms.Panel();
            this.lblCPUHand = new System.Windows.Forms.Label();
            this.cdbDeck = new CardBox.CardBox();
            this.pnlPlayArea.SuspendLayout();
            this.pnlPlayerHand.SuspendLayout();
            this.pnlCPUHand.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatistics
            // 
            this.lblStatistics.BackColor = System.Drawing.Color.Yellow;
            this.lblStatistics.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistics.Location = new System.Drawing.Point(16, 695);
            this.lblStatistics.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(896, 36);
            this.lblStatistics.TabIndex = 3;
            this.lblStatistics.Text = "Statistics:";
            this.lblStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumOfGames
            // 
            this.lblNumOfGames.AutoSize = true;
            this.lblNumOfGames.Location = new System.Drawing.Point(91, 761);
            this.lblNumOfGames.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumOfGames.Name = "lblNumOfGames";
            this.lblNumOfGames.Size = new System.Drawing.Size(81, 17);
            this.lblNumOfGames.TabIndex = 4;
            this.lblNumOfGames.Text = "# of Games";
            // 
            // lblNumOfWins
            // 
            this.lblNumOfWins.AutoSize = true;
            this.lblNumOfWins.Location = new System.Drawing.Point(135, 796);
            this.lblNumOfWins.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumOfWins.Name = "lblNumOfWins";
            this.lblNumOfWins.Size = new System.Drawing.Size(36, 17);
            this.lblNumOfWins.TabIndex = 5;
            this.lblNumOfWins.Text = "Win:";
            // 
            // lblNumOfLosses
            // 
            this.lblNumOfLosses.AutoSize = true;
            this.lblNumOfLosses.Location = new System.Drawing.Point(311, 761);
            this.lblNumOfLosses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumOfLosses.Name = "lblNumOfLosses";
            this.lblNumOfLosses.Size = new System.Drawing.Size(42, 17);
            this.lblNumOfLosses.TabIndex = 6;
            this.lblNumOfLosses.Text = "Loss:";
            // 
            // lblNumOfDraws
            // 
            this.lblNumOfDraws.AutoSize = true;
            this.lblNumOfDraws.Location = new System.Drawing.Point(307, 796);
            this.lblNumOfDraws.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumOfDraws.Name = "lblNumOfDraws";
            this.lblNumOfDraws.Size = new System.Drawing.Size(44, 17);
            this.lblNumOfDraws.TabIndex = 7;
            this.lblNumOfDraws.Text = "Draw:";
            // 
            // lblGamesPlayed
            // 
            this.lblGamesPlayed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGamesPlayed.Location = new System.Drawing.Point(181, 756);
            this.lblGamesPlayed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGamesPlayed.Name = "lblGamesPlayed";
            this.lblGamesPlayed.Size = new System.Drawing.Size(82, 24);
            this.lblGamesPlayed.TabIndex = 8;
            this.lblGamesPlayed.Text = "0";
            this.lblGamesPlayed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(181, 791);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(361, 756);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(361, 791);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(631, 754);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(159, 28);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(631, 790);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(159, 28);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlPlayArea
            // 
            this.pnlPlayArea.BackColor = System.Drawing.Color.SaddleBrown;
            this.pnlPlayArea.BackgroundImage = global::DurakGameUI.Properties.Resources.placemat;
            this.pnlPlayArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPlayArea.Controls.Add(this.lblDeck);
            this.pnlPlayArea.Controls.Add(this.cdbDeck);
            this.pnlPlayArea.Location = new System.Drawing.Point(16, 198);
            this.pnlPlayArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPlayArea.Name = "pnlPlayArea";
            this.pnlPlayArea.Size = new System.Drawing.Size(896, 305);
            this.pnlPlayArea.TabIndex = 2;
            // 
            // lblDeck
            // 
            this.lblDeck.AutoSize = true;
            this.lblDeck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeck.Location = new System.Drawing.Point(797, 49);
            this.lblDeck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeck.Name = "lblDeck";
            this.lblDeck.Size = new System.Drawing.Size(48, 20);
            this.lblDeck.TabIndex = 2;
            this.lblDeck.Text = "Deck";
            // 
            // pnlPlayerHand
            // 
            this.pnlPlayerHand.BackColor = System.Drawing.Color.Teal;
            this.pnlPlayerHand.BackgroundImage = global::DurakGameUI.Properties.Resources.background;
            this.pnlPlayerHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPlayerHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerHand.Controls.Add(this.lblYourHand);
            this.pnlPlayerHand.Location = new System.Drawing.Point(16, 511);
            this.pnlPlayerHand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPlayerHand.Name = "pnlPlayerHand";
            this.pnlPlayerHand.Size = new System.Drawing.Size(895, 176);
            this.pnlPlayerHand.TabIndex = 1;
            // 
            // lblYourHand
            // 
            this.lblYourHand.AutoSize = true;
            this.lblYourHand.Location = new System.Drawing.Point(-3, 0);
            this.lblYourHand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYourHand.Name = "lblYourHand";
            this.lblYourHand.Size = new System.Drawing.Size(76, 17);
            this.lblYourHand.TabIndex = 0;
            this.lblYourHand.Text = "Your Hand";
            // 
            // pnlCPUHand
            // 
            this.pnlCPUHand.BackColor = System.Drawing.Color.IndianRed;
            this.pnlCPUHand.BackgroundImage = global::DurakGameUI.Properties.Resources.background;
            this.pnlCPUHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCPUHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCPUHand.Controls.Add(this.lblCPUHand);
            this.pnlCPUHand.Location = new System.Drawing.Point(16, 15);
            this.pnlCPUHand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCPUHand.Name = "pnlCPUHand";
            this.pnlCPUHand.Size = new System.Drawing.Size(895, 176);
            this.pnlCPUHand.TabIndex = 0;
            // 
            // lblCPUHand
            // 
            this.lblCPUHand.AutoSize = true;
            this.lblCPUHand.Location = new System.Drawing.Point(-3, 0);
            this.lblCPUHand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCPUHand.Name = "lblCPUHand";
            this.lblCPUHand.Size = new System.Drawing.Size(84, 17);
            this.lblCPUHand.TabIndex = 1;
            this.lblCPUHand.Text = "CPU\'s Hand";
            // 
            // cdbDeck
            // 
            this.cdbDeck.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.cdbDeck.FaceUp = false;
            this.cdbDeck.Location = new System.Drawing.Point(763, 71);
            this.cdbDeck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cdbDeck.Name = "cdbDeck";
            this.cdbDeck.Rank = CardLibrary.CardRank.Ace;
            this.cdbDeck.Size = new System.Drawing.Size(125, 157);
            this.cdbDeck.Suit = CardLibrary.CardSuit.Hearts;
            this.cdbDeck.TabIndex = 0;
            // 
            // frmDurak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DurakGameUI.Properties.Resources.table;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(928, 846);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGamesPlayed);
            this.Controls.Add(this.lblNumOfDraws);
            this.Controls.Add(this.lblNumOfLosses);
            this.Controls.Add(this.lblNumOfWins);
            this.Controls.Add(this.lblNumOfGames);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.pnlPlayArea);
            this.Controls.Add(this.pnlPlayerHand);
            this.Controls.Add(this.pnlCPUHand);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(946, 893);
            this.MinimumSize = new System.Drawing.Size(946, 893);
            this.Name = "frmDurak";
            this.Text = "Durak";
            this.pnlPlayArea.ResumeLayout(false);
            this.pnlPlayArea.PerformLayout();
            this.pnlPlayerHand.ResumeLayout(false);
            this.pnlPlayerHand.PerformLayout();
            this.pnlCPUHand.ResumeLayout(false);
            this.pnlCPUHand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCPUHand;
        private System.Windows.Forms.Label lblCPUHand;
        private System.Windows.Forms.Panel pnlPlayerHand;
        private System.Windows.Forms.Label lblYourHand;
        private System.Windows.Forms.Panel pnlPlayArea;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Label lblDeck;
        private CardBox.CardBox cdbDeck;
        private System.Windows.Forms.Label lblNumOfGames;
        private System.Windows.Forms.Label lblNumOfWins;
        private System.Windows.Forms.Label lblNumOfLosses;
        private System.Windows.Forms.Label lblNumOfDraws;
        private System.Windows.Forms.Label lblGamesPlayed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
    }
}

