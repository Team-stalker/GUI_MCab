namespace Compress
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LinkA = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pathCmp = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PathOut = new System.Windows.Forms.PictureBox();
            this.LinkB = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.who = new System.Windows.Forms.ToolStripMenuItem();
            this.help = new System.Windows.Forms.ToolStripMenuItem();
            this.StartCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.Thread = new System.ComponentModel.BackgroundWorker();
            this.INFO = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathCmp)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathOut)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LinkA
            // 
            this.LinkA.Location = new System.Drawing.Point(9, 21);
            this.LinkA.Name = "LinkA";
            this.LinkA.Size = new System.Drawing.Size(252, 20);
            this.LinkA.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pathCmp);
            this.groupBox1.Controls.Add(this.LinkA);
            this.groupBox1.Location = new System.Drawing.Point(9, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите файл для сжатия:";
            // 
            // pathCmp
            // 
            this.pathCmp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pathCmp.BackgroundImage")));
            this.pathCmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pathCmp.Location = new System.Drawing.Point(267, 21);
            this.pathCmp.Name = "pathCmp";
            this.pathCmp.Size = new System.Drawing.Size(20, 20);
            this.pathCmp.TabIndex = 3;
            this.pathCmp.TabStop = false;
            this.pathCmp.Click += new System.EventHandler(this.pathCmp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PathOut);
            this.groupBox2.Controls.Add(this.LinkB);
            this.groupBox2.Location = new System.Drawing.Point(9, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 53);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выберите файл для распаковки:";
            // 
            // PathOut
            // 
            this.PathOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PathOut.BackgroundImage")));
            this.PathOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PathOut.Location = new System.Drawing.Point(267, 21);
            this.PathOut.Name = "PathOut";
            this.PathOut.Size = new System.Drawing.Size(20, 20);
            this.PathOut.TabIndex = 3;
            this.PathOut.TabStop = false;
            this.PathOut.Click += new System.EventHandler(this.PathOut_Click);
            // 
            // LinkB
            // 
            this.LinkB.Location = new System.Drawing.Point(9, 21);
            this.LinkB.Name = "LinkB";
            this.LinkB.Size = new System.Drawing.Size(252, 20);
            this.LinkB.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.StartCmd,
            this.INFO});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(318, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.who,
            this.help});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(65, 20);
            this.file.Text = "Справка";
            // 
            // who
            // 
            this.who.Name = "who";
            this.who.Size = new System.Drawing.Size(149, 22);
            this.who.Text = "О программе";
            this.who.Click += new System.EventHandler(this.who_Click);
            // 
            // help
            // 
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(149, 22);
            this.help.Text = "Помощь";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // StartCmd
            // 
            this.StartCmd.Name = "StartCmd";
            this.StartCmd.Size = new System.Drawing.Size(50, 20);
            this.StartCmd.Text = "Старт";
            this.StartCmd.Click += new System.EventHandler(this.StartCmd_Click);
            // 
            // Thread
            // 
            this.Thread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Thread_DoWork);
            this.Thread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Thread_RunWorkerCompleted);
            // 
            // INFO
            // 
            this.INFO.Name = "INFO";
            this.INFO.Size = new System.Drawing.Size(40, 20);
            this.INFO.Text = "info";
            this.INFO.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 144);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(334, 183);
            this.MinimumSize = new System.Drawing.Size(334, 183);
            this.Name = "Form1";
            this.Text = "GUI Make .cab";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathCmp)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PathOut)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LinkA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pathCmp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox PathOut;
        private System.Windows.Forms.TextBox LinkB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem StartCmd;
        private System.Windows.Forms.ToolStripMenuItem who;
        private System.Windows.Forms.ToolStripMenuItem help;
        private System.ComponentModel.BackgroundWorker Thread;
        private System.Windows.Forms.ToolStripMenuItem INFO;
    }
}

