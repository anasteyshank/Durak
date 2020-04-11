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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDurak));
            this.lblStatistics = new System.Windows.Forms.Label();
            this.lblNumOfGames = new System.Windows.Forms.Label();
            this.lblNumOfWins = new System.Windows.Forms.Label();
            this.lblNumOfLosses = new System.Windows.Forms.Label();
            this.lblNumOfDraws = new System.Windows.Forms.Label();
            this.lblGamesPlayedCount = new System.Windows.Forms.Label();
            this.lblWinsCount = new System.Windows.Forms.Label();
            this.lblLossCount = new System.Windows.Forms.Label();
            this.lblDrawCount = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPlayArea = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.pbDeck = new MyCardBox.CardBox();
            this.pbTrump = new MyCardBox.CardBox();
            this.lblDeck = new System.Windows.Forms.Label();
            this.pnlPlayerHand = new System.Windows.Forms.Panel();
            this.lblPlayerHand = new System.Windows.Forms.Label();
            this.pnlCPUHand = new System.Windows.Forms.Panel();
            this.lblCPUHand = new System.Windows.Forms.Label();
            this.btnTake = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.GameToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.pnlPlayArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatistics
            // 
            this.lblStatistics.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatistics.BackColor = System.Drawing.Color.Yellow;
            this.lblStatistics.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatistics.Location = new System.Drawing.Point(17, 691);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(954, 29);
            this.lblStatistics.TabIndex = 3;
            this.lblStatistics.Text = "Statistics:";
            this.lblStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumOfGames
            // 
            this.lblNumOfGames.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNumOfGames.AutoSize = true;
            this.lblNumOfGames.Location = new System.Drawing.Point(179, 734);
            this.lblNumOfGames.Name = "lblNumOfGames";
            this.lblNumOfGames.Size = new System.Drawing.Size(62, 13);
            this.lblNumOfGames.TabIndex = 4;
            this.lblNumOfGames.Text = "# of Games";
            // 
            // lblNumOfWins
            // 
            this.lblNumOfWins.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNumOfWins.AutoSize = true;
            this.lblNumOfWins.Location = new System.Drawing.Point(213, 762);
            this.lblNumOfWins.Name = "lblNumOfWins";
            this.lblNumOfWins.Size = new System.Drawing.Size(29, 13);
            this.lblNumOfWins.TabIndex = 5;
            this.lblNumOfWins.Text = "Win:";
            // 
            // lblNumOfLosses
            // 
            this.lblNumOfLosses.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNumOfLosses.AutoSize = true;
            this.lblNumOfLosses.Location = new System.Drawing.Point(345, 734);
            this.lblNumOfLosses.Name = "lblNumOfLosses";
            this.lblNumOfLosses.Size = new System.Drawing.Size(32, 13);
            this.lblNumOfLosses.TabIndex = 6;
            this.lblNumOfLosses.Text = "Loss:";
            // 
            // lblNumOfDraws
            // 
            this.lblNumOfDraws.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNumOfDraws.AutoSize = true;
            this.lblNumOfDraws.Location = new System.Drawing.Point(341, 762);
            this.lblNumOfDraws.Name = "lblNumOfDraws";
            this.lblNumOfDraws.Size = new System.Drawing.Size(35, 13);
            this.lblNumOfDraws.TabIndex = 7;
            this.lblNumOfDraws.Text = "Draw:";
            // 
            // lblGamesPlayedCount
            // 
            this.lblGamesPlayedCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGamesPlayedCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGamesPlayedCount.Location = new System.Drawing.Point(248, 728);
            this.lblGamesPlayedCount.Name = "lblGamesPlayedCount";
            this.lblGamesPlayedCount.Size = new System.Drawing.Size(62, 20);
            this.lblGamesPlayedCount.TabIndex = 8;
            this.lblGamesPlayedCount.Text = "0";
            this.lblGamesPlayedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameToolTips.SetToolTip(this.lblGamesPlayedCount, "Number of games played");
            // 
            // lblWinsCount
            // 
            this.lblWinsCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWinsCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWinsCount.Location = new System.Drawing.Point(248, 758);
            this.lblWinsCount.Name = "lblWinsCount";
            this.lblWinsCount.Size = new System.Drawing.Size(62, 20);
            this.lblWinsCount.TabIndex = 9;
            this.lblWinsCount.Text = "0";
            this.lblWinsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameToolTips.SetToolTip(this.lblWinsCount, "Number of times you won");
            // 
            // lblLossCount
            // 
            this.lblLossCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLossCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLossCount.Location = new System.Drawing.Point(383, 728);
            this.lblLossCount.Name = "lblLossCount";
            this.lblLossCount.Size = new System.Drawing.Size(62, 20);
            this.lblLossCount.TabIndex = 10;
            this.lblLossCount.Text = "0";
            this.lblLossCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameToolTips.SetToolTip(this.lblLossCount, "Number of times you lost");
            // 
            // lblDrawCount
            // 
            this.lblDrawCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDrawCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDrawCount.Location = new System.Drawing.Point(383, 758);
            this.lblDrawCount.Name = "lblDrawCount";
            this.lblDrawCount.Size = new System.Drawing.Size(62, 20);
            this.lblDrawCount.TabIndex = 11;
            this.lblDrawCount.Text = "0";
            this.lblDrawCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GameToolTips.SetToolTip(this.lblDrawCount, "Number of draws");
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.Location = new System.Drawing.Point(728, 727);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(119, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.GameToolTips.SetToolTip(this.btnReset, "Click to reset a game");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.Location = new System.Drawing.Point(728, 756);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.GameToolTips.SetToolTip(this.btnExit, "Click to exit the application");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlPlayArea
            // 
            this.pnlPlayArea.AllowDrop = true;
            this.pnlPlayArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlPlayArea.BackColor = System.Drawing.Color.SaddleBrown;
            this.pnlPlayArea.BackgroundImage = global::DurakGameUI.Properties.Resources.placemat;
            this.pnlPlayArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPlayArea.Controls.Add(this.lblResult);
            this.pnlPlayArea.Controls.Add(this.pbDeck);
            this.pnlPlayArea.Controls.Add(this.pbTrump);
            this.pnlPlayArea.Controls.Add(this.lblDeck);
            this.pnlPlayArea.Location = new System.Drawing.Point(17, 240);
            this.pnlPlayArea.Name = "pnlPlayArea";
            this.pnlPlayArea.Size = new System.Drawing.Size(954, 248);
            this.pnlPlayArea.TabIndex = 2;
            this.pnlPlayArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlPlayArea_DragDrop);
            this.pnlPlayArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Snow;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(231, 67);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(449, 74);
            this.lblResult.TabIndex = 5;
            // 
            // pbDeck
            // 
            this.pbDeck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbDeck.BackColor = System.Drawing.Color.Transparent;
            this.pbDeck.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.pbDeck.FaceUp = false;
            this.pbDeck.Location = new System.Drawing.Point(838, 50);
            this.pbDeck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbDeck.Name = "pbDeck";
            this.pbDeck.Rank = CardLibrary.CardRank.Ace;
            this.pbDeck.Size = new System.Drawing.Size(94, 128);
            this.pbDeck.Suit = CardLibrary.CardSuit.Hearts;
            this.pbDeck.TabIndex = 4;
            this.GameToolTips.SetToolTip(this.pbDeck, "A deck of cards");
            // 
            // pbTrump
            // 
            this.pbTrump.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbTrump.BackColor = System.Drawing.Color.Transparent;
            this.pbTrump.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.pbTrump.FaceUp = false;
            this.pbTrump.Location = new System.Drawing.Point(783, 50);
            this.pbTrump.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbTrump.Name = "pbTrump";
            this.pbTrump.Rank = CardLibrary.CardRank.Ace;
            this.pbTrump.Size = new System.Drawing.Size(94, 128);
            this.pbTrump.Suit = CardLibrary.CardSuit.Hearts;
            this.pbTrump.TabIndex = 3;
            this.GameToolTips.SetToolTip(this.pbTrump, "Trump Suit");
            // 
            // lblDeck
            // 
            this.lblDeck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDeck.AutoSize = true;
            this.lblDeck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeck.Location = new System.Drawing.Point(868, 17);
            this.lblDeck.Name = "lblDeck";
            this.lblDeck.Size = new System.Drawing.Size(40, 16);
            this.lblDeck.TabIndex = 2;
            this.lblDeck.Text = "Deck";
            // 
            // pnlPlayerHand
            // 
            this.pnlPlayerHand.AllowDrop = true;
            this.pnlPlayerHand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlPlayerHand.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayerHand.BackgroundImage = global::DurakGameUI.Properties.Resources.background;
            this.pnlPlayerHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPlayerHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlayerHand.Location = new System.Drawing.Point(17, 514);
            this.pnlPlayerHand.Name = "pnlPlayerHand";
            this.pnlPlayerHand.Size = new System.Drawing.Size(954, 173);
            this.pnlPlayerHand.TabIndex = 1;
            this.pnlPlayerHand.DragDrop += new System.Windows.Forms.DragEventHandler(this.Panel_DragDrop);
            this.pnlPlayerHand.DragEnter += new System.Windows.Forms.DragEventHandler(this.Panel_DragEnter);
            // 
            // lblPlayerHand
            // 
            this.lblPlayerHand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlayerHand.AutoSize = true;
            this.lblPlayerHand.BackColor = System.Drawing.Color.Teal;
            this.lblPlayerHand.Location = new System.Drawing.Point(23, 500);
            this.lblPlayerHand.Name = "lblPlayerHand";
            this.lblPlayerHand.Size = new System.Drawing.Size(58, 13);
            this.lblPlayerHand.TabIndex = 0;
            this.lblPlayerHand.Text = "Your Hand";
            // 
            // pnlCPUHand
            // 
            this.pnlCPUHand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCPUHand.BackColor = System.Drawing.Color.Transparent;
            this.pnlCPUHand.BackgroundImage = global::DurakGameUI.Properties.Resources.background;
            this.pnlCPUHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCPUHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCPUHand.Location = new System.Drawing.Point(17, 60);
            this.pnlCPUHand.Name = "pnlCPUHand";
            this.pnlCPUHand.Size = new System.Drawing.Size(954, 173);
            this.pnlCPUHand.TabIndex = 0;
            // 
            // lblCPUHand
            // 
            this.lblCPUHand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCPUHand.AutoSize = true;
            this.lblCPUHand.BackColor = System.Drawing.Color.IndianRed;
            this.lblCPUHand.Location = new System.Drawing.Point(23, 44);
            this.lblCPUHand.Name = "lblCPUHand";
            this.lblCPUHand.Size = new System.Drawing.Size(65, 13);
            this.lblCPUHand.TabIndex = 1;
            this.lblCPUHand.Text = "CPU\'s Hand";
            // 
            // btnTake
            // 
            this.btnTake.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTake.Location = new System.Drawing.Point(852, 491);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(119, 23);
            this.btnTake.TabIndex = 1;
            this.btnTake.Text = "Take";
            this.GameToolTips.SetToolTip(this.btnTake, "Click when you want to take cards");
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnPickUp_Click);
            // 
            // btnReady
            // 
            this.btnReady.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReady.Location = new System.Drawing.Point(728, 491);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(119, 23);
            this.btnReady.TabIndex = 0;
            this.btnReady.Text = "Ready";
            this.GameToolTips.SetToolTip(this.btnReady, "Click when you want  to stop attacking");
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // frmDurak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DurakGameUI.Properties.Resources.table;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(995, 797);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.btnTake);
            this.Controls.Add(this.lblPlayerHand);
            this.Controls.Add(this.lblCPUHand);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblDrawCount);
            this.Controls.Add(this.lblLossCount);
            this.Controls.Add(this.lblWinsCount);
            this.Controls.Add(this.lblGamesPlayedCount);
            this.Controls.Add(this.lblNumOfDraws);
            this.Controls.Add(this.lblNumOfLosses);
            this.Controls.Add(this.lblNumOfWins);
            this.Controls.Add(this.lblNumOfGames);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.pnlPlayArea);
            this.Controls.Add(this.pnlPlayerHand);
            this.Controls.Add(this.pnlCPUHand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1011, 822);
            this.Name = "frmDurak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durak";
            this.Load += new System.EventHandler(this.frmDurak_Load);
            this.pnlPlayArea.ResumeLayout(false);
            this.pnlPlayArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCPUHand;
        private System.Windows.Forms.Label lblCPUHand;
        private System.Windows.Forms.Panel pnlPlayerHand;
        private System.Windows.Forms.Label lblPlayerHand;
        private System.Windows.Forms.Panel pnlPlayArea;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Label lblDeck;
        private System.Windows.Forms.Label lblNumOfGames;
        private System.Windows.Forms.Label lblNumOfWins;
        private System.Windows.Forms.Label lblNumOfLosses;
        private System.Windows.Forms.Label lblNumOfDraws;
        private System.Windows.Forms.Label lblGamesPlayedCount;
        private System.Windows.Forms.Label lblWinsCount;
        private System.Windows.Forms.Label lblLossCount;
        private System.Windows.Forms.Label lblDrawCount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private MyCardBox.CardBox pbTrump;
        private MyCardBox.CardBox pbDeck;
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ToolTip GameToolTips;
    }
}

