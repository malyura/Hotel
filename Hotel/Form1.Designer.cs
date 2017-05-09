namespace Hotel
{
    partial class HotelForm
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
            this.dataGridResidents = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridBooking = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.butBooking = new System.Windows.Forms.Button();
            this.butAddResFromBooking = new System.Windows.Forms.Button();
            this.butAddResident = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butDelBooking = new System.Windows.Forms.Button();
            this.butEditResident = new System.Windows.Forms.Button();
            this.butDelRes = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButRooms = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButClients = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButExit = new System.Windows.Forms.ToolStripButton();
            this.graphic1 = new Hotel.Graphic();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResidents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBooking)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridResidents
            // 
            this.dataGridResidents.AllowUserToAddRows = false;
            this.dataGridResidents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResidents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column3,
            this.Column7});
            this.dataGridResidents.Location = new System.Drawing.Point(10, 110);
            this.dataGridResidents.Name = "dataGridResidents";
            this.dataGridResidents.RowHeadersVisible = false;
            this.dataGridResidents.RowTemplate.Height = 24;
            this.dataGridResidents.Size = new System.Drawing.Size(731, 278);
            this.dataGridResidents.TabIndex = 0;
            this.dataGridResidents.MouseEnter += new System.EventHandler(this.dataGridResidents_MouseEnter);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Номер";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 45;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Категория";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Заезд";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Выезд";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Оплата, руб.";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Дата оплаты";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Клиент";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridBooking
            // 
            this.dataGridBooking.AllowUserToAddRows = false;
            this.dataGridBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBooking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dataGridBooking.Location = new System.Drawing.Point(747, 110);
            this.dataGridBooking.Name = "dataGridBooking";
            this.dataGridBooking.RowHeadersVisible = false;
            this.dataGridBooking.RowTemplate.Height = 24;
            this.dataGridBooking.Size = new System.Drawing.Size(551, 278);
            this.dataGridBooking.TabIndex = 1;
            this.dataGridBooking.MouseEnter += new System.EventHandler(this.dataGridBooking_MouseEnter);
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Номер";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 45;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "ФИО";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 150;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Заезд";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 70;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Выезд";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 70;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Дата записи";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 70;
            // 
            // butBooking
            // 
            this.butBooking.Location = new System.Drawing.Point(1050, 469);
            this.butBooking.Name = "butBooking";
            this.butBooking.Size = new System.Drawing.Size(248, 64);
            this.butBooking.TabIndex = 2;
            this.butBooking.Text = "Добавить бронь";
            this.butBooking.UseVisualStyleBackColor = true;
            this.butBooking.Click += new System.EventHandler(this.butBooking_Click);
            this.butBooking.MouseEnter += new System.EventHandler(this.butBooking_MouseEnter);
            // 
            // butAddResFromBooking
            // 
            this.butAddResFromBooking.Location = new System.Drawing.Point(1050, 394);
            this.butAddResFromBooking.Name = "butAddResFromBooking";
            this.butAddResFromBooking.Size = new System.Drawing.Size(248, 69);
            this.butAddResFromBooking.TabIndex = 3;
            this.butAddResFromBooking.Text = "Заселение по предварительному бронированию";
            this.butAddResFromBooking.UseVisualStyleBackColor = true;
            this.butAddResFromBooking.Click += new System.EventHandler(this.butAddResFromBooking_Click);
            this.butAddResFromBooking.MouseEnter += new System.EventHandler(this.butAddResFromBooking_MouseEnter);
            // 
            // butAddResident
            // 
            this.butAddResident.Location = new System.Drawing.Point(10, 393);
            this.butAddResident.Name = "butAddResident";
            this.butAddResident.Size = new System.Drawing.Size(245, 65);
            this.butAddResident.TabIndex = 5;
            this.butAddResident.Text = "Заселение без предварительного бронирования";
            this.butAddResident.UseVisualStyleBackColor = true;
            this.butAddResident.Click += new System.EventHandler(this.butAddResident_Click);
            this.butAddResident.MouseEnter += new System.EventHandler(this.butAddResident_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "АКТУАЛЬНЫЕ ЗАПИСИ О ПРОЖИВАНИИ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(875, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "АКТУАЛЬНЫЕ ЗАПИСИ О БРОНИРОВАНИИ";
            // 
            // butDelBooking
            // 
            this.butDelBooking.Location = new System.Drawing.Point(1050, 539);
            this.butDelBooking.Name = "butDelBooking";
            this.butDelBooking.Size = new System.Drawing.Size(248, 64);
            this.butDelBooking.TabIndex = 12;
            this.butDelBooking.Text = "Удалить бронь";
            this.butDelBooking.UseVisualStyleBackColor = true;
            this.butDelBooking.Click += new System.EventHandler(this.butDelBooking_Click);
            this.butDelBooking.MouseEnter += new System.EventHandler(this.butDelBooking_MouseEnter);
            // 
            // butEditResident
            // 
            this.butEditResident.Location = new System.Drawing.Point(10, 464);
            this.butEditResident.Name = "butEditResident";
            this.butEditResident.Size = new System.Drawing.Size(245, 69);
            this.butEditResident.TabIndex = 13;
            this.butEditResident.Text = "Редактировать запись о проживании";
            this.butEditResident.UseVisualStyleBackColor = true;
            this.butEditResident.Click += new System.EventHandler(this.butEditResident_Click);
            this.butEditResident.MouseEnter += new System.EventHandler(this.butEditResident_MouseEnter);
            // 
            // butDelRes
            // 
            this.butDelRes.Location = new System.Drawing.Point(10, 539);
            this.butDelRes.Name = "butDelRes";
            this.butDelRes.Size = new System.Drawing.Size(245, 66);
            this.butDelRes.TabIndex = 15;
            this.butDelRes.Text = "Удалить запись о проживании";
            this.butDelRes.UseVisualStyleBackColor = true;
            this.butDelRes.Click += new System.EventHandler(this.butDelRes_Click);
            this.butDelRes.MouseEnter += new System.EventHandler(this.butDelRes_MouseEnter);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 763);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1307, 25);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(189, 20);
            this.toolStripStatusLabel1.Text = "Приложение \"Гостиница\"";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1307, 28);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportResToolStripMenuItem,
            this.ImportResToolStripMenuItem});
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.xMLToolStripMenuItem.Text = "Экспорт записей";
            this.xMLToolStripMenuItem.MouseEnter += new System.EventHandler(this.xMLToolStripMenuItem_MouseEnter);
            // 
            // ExportResToolStripMenuItem
            // 
            this.ExportResToolStripMenuItem.Name = "ExportResToolStripMenuItem";
            this.ExportResToolStripMenuItem.Size = new System.Drawing.Size(358, 26);
            this.ExportResToolStripMenuItem.Text = "Экспорт записей о проживании в Excel";
            this.ExportResToolStripMenuItem.Click += new System.EventHandler(this.ExportResToolStripMenuItem_Click);
            this.ExportResToolStripMenuItem.MouseEnter += new System.EventHandler(this.ExportResToolStripMenuItem_MouseEnter);
            // 
            // ImportResToolStripMenuItem
            // 
            this.ImportResToolStripMenuItem.Name = "ImportResToolStripMenuItem";
            this.ImportResToolStripMenuItem.Size = new System.Drawing.Size(358, 26);
            this.ImportResToolStripMenuItem.Text = "Просмотр записей о проживании";
            this.ImportResToolStripMenuItem.Click += new System.EventHandler(this.ImportResToolStripMenuItem_Click);
            this.ImportResToolStripMenuItem.MouseEnter += new System.EventHandler(this.ImportResToolStripMenuItem_MouseEnter);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.оПрограммеToolStripMenuItem.Text = "О программе...";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButRooms,
            this.toolStripSeparator1,
            this.toolStripButClients,
            this.toolStripSeparator2,
            this.toolStripButExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1307, 50);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButRooms
            // 
            this.toolStripButRooms.AutoSize = false;
            this.toolStripButRooms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButRooms.Image = global::Hotel.Properties.Resources.room;
            this.toolStripButRooms.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButRooms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButRooms.Name = "toolStripButRooms";
            this.toolStripButRooms.Size = new System.Drawing.Size(83, 47);
            this.toolStripButRooms.Text = "Номерной фонд";
            this.toolStripButRooms.Click += new System.EventHandler(this.toolStripButRooms_Click);
            this.toolStripButRooms.MouseEnter += new System.EventHandler(this.toolStripButton2_MouseEnter);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButClients
            // 
            this.toolStripButClients.AutoSize = false;
            this.toolStripButClients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButClients.Image = global::Hotel.Properties.Resources.cl;
            this.toolStripButClients.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButClients.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButClients.Name = "toolStripButClients";
            this.toolStripButClients.Size = new System.Drawing.Size(55, 47);
            this.toolStripButClients.Text = "Клиенты";
            this.toolStripButClients.Click += new System.EventHandler(this.toolStripButClients_Click);
            this.toolStripButClients.MouseEnter += new System.EventHandler(this.toolStripButton3_MouseEnter);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButExit
            // 
            this.toolStripButExit.AutoSize = false;
            this.toolStripButExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButExit.Image = global::Hotel.Properties.Resources.exit;
            this.toolStripButExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButExit.Name = "toolStripButExit";
            this.toolStripButExit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButExit.Size = new System.Drawing.Size(56, 47);
            this.toolStripButExit.Text = "Выход";
            this.toolStripButExit.Click += new System.EventHandler(this.toolStripButExit_Click);
            this.toolStripButExit.MouseEnter += new System.EventHandler(this.toolStripButExit_MouseEnter);
            // 
            // graphic1
            // 
            this.graphic1.Location = new System.Drawing.Point(264, 394);
            this.graphic1.Name = "graphic1";
            this.graphic1.Size = new System.Drawing.Size(783, 361);
            this.graphic1.TabIndex = 9;
            this.graphic1.y = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            this.graphic1.YearChanged += new System.EventHandler<Hotel.SumInfoEventArgs>(this.graphic1_YearChanged);
            this.graphic1.Load += new System.EventHandler(this.graphic1_Load);
            this.graphic1.Click += new System.EventHandler(this.graphic1_Click);
            this.graphic1.MouseEnter += new System.EventHandler(this.graphic1_MouseEnter);
            // 
            // HotelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 788);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.butDelRes);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.butEditResident);
            this.Controls.Add(this.butDelBooking);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphic1);
            this.Controls.Add(this.butAddResident);
            this.Controls.Add(this.butAddResFromBooking);
            this.Controls.Add(this.butBooking);
            this.Controls.Add(this.dataGridBooking);
            this.Controls.Add(this.dataGridResidents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "HotelForm";
            this.Text = "Гостиница";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResidents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBooking)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridResidents;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridBooking;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.Button butBooking;
        private System.Windows.Forms.Button butAddResFromBooking;
        private System.Windows.Forms.Button butAddResident;
        private Graphic graphic1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butDelBooking;
        private System.Windows.Forms.Button butEditResident;
        private System.Windows.Forms.Button butDelRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButRooms;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButClients;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportResToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

