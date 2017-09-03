namespace BattleShips
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Grid = new System.Windows.Forms.PictureBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.Файл = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьСначалаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(0, -1);
            this.Grid.Margin = new System.Windows.Forms.Padding(0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(287, 268);
            this.Grid.TabIndex = 0;
            this.Grid.TabStop = false;
            this.Grid.DoubleClick += new System.EventHandler(this.Grid_DoubleClick);
            this.Grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseClick);
            this.Grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
            this.Grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            this.Grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseUp);
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.Transparent;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Файл});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(284, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // Файл
            // 
            this.Файл.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.начатьСначалаToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem});
            this.Файл.Name = "Файл";
            this.Файл.Size = new System.Drawing.Size(48, 20);
            this.Файл.Text = "Файл";
            // 
            // начатьСначалаToolStripMenuItem
            // 
            this.начатьСначалаToolStripMenuItem.Name = "начатьСначалаToolStripMenuItem";
            this.начатьСначалаToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.начатьСначалаToolStripMenuItem.Text = "Начать сначала";
            this.начатьСначалаToolStripMenuItem.Click += new System.EventHandler(this.начатьСначалаToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.Grid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Form1";
            this.Text = "BattleShips";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem Файл;
        private System.Windows.Forms.ToolStripMenuItem начатьСначалаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
    }
}

