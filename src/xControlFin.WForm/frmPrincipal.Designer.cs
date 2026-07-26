namespace xControlFin.WForm;

partial class frmPrincipal
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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
        gridLanc = new DataGridView();
        pictureBox1 = new PictureBox();
        panel1 = new Panel();
        lblPeriodo = new Label();
        lblUser = new Label();
        btnEstornar = new Button();
        btnAlterarData = new Button();
        btnEfetivar = new Button();
        nudDaysBack = new NumericUpDown();
        nudMonthsAhead = new NumericUpDown();
        label2 = new Label();
        label1 = new Label();
        label3 = new Label();
        lblRealizado = new Label();
        lblPrevisto = new Label();
        label6 = new Label();
        lblTotal = new Label();
        label8 = new Label();
        btnAtualizar = new Button();
        cboAccount = new ComboBox();
        label4 = new Label();
        lblMsg = new Label();
        ((System.ComponentModel.ISupportInitialize)gridLanc).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudDaysBack).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudMonthsAhead).BeginInit();
        SuspendLayout();
        // 
        // gridLanc
        // 
        gridLanc.AllowUserToAddRows = false;
        gridLanc.AllowUserToDeleteRows = false;
        gridLanc.AllowUserToResizeColumns = false;
        gridLanc.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 249, 252);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        gridLanc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        gridLanc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gridLanc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridLanc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        gridLanc.BackgroundColor = Color.White;
        gridLanc.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(20, 35, 55);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        gridLanc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        gridLanc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = SystemColors.Window;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        gridLanc.DefaultCellStyle = dataGridViewCellStyle3;
        gridLanc.EnableHeadersVisualStyles = false;
        gridLanc.Location = new Point(0, 128);
        gridLanc.Name = "gridLanc";
        gridLanc.ReadOnly = true;
        gridLanc.RowHeadersVisible = false;
        gridLanc.RowTemplate.Resizable = DataGridViewTriState.True;
        gridLanc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridLanc.Size = new Size(1086, 622);
        gridLanc.TabIndex = 2;
        // 
        // pictureBox1
        // 
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(3, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(187, 52);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panel1.BackColor = Color.FromArgb(20, 35, 55);
        panel1.Controls.Add(lblPeriodo);
        panel1.Controls.Add(lblUser);
        panel1.Controls.Add(pictureBox1);
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(1086, 57);
        panel1.TabIndex = 1;
        // 
        // lblPeriodo
        // 
        lblPeriodo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPeriodo.Font = new Font("Segoe UI", 7F);
        lblPeriodo.ForeColor = Color.FromArgb(178, 178, 178);
        lblPeriodo.Location = new Point(650, 37);
        lblPeriodo.Name = "lblPeriodo";
        lblPeriodo.Size = new Size(422, 21);
        lblPeriodo.TabIndex = 2;
        lblPeriodo.Text = "Período e Msg";
        lblPeriodo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblUser
        // 
        lblUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblUser.Font = new Font("Segoe UI", 12F);
        lblUser.ForeColor = Color.FromArgb(252, 114, 0);
        lblUser.Location = new Point(669, 16);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(403, 21);
        lblUser.TabIndex = 1;
        lblUser.Text = "Olá";
        lblUser.TextAlign = ContentAlignment.MiddleRight;
        // 
        // btnEstornar
        // 
        btnEstornar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEstornar.BackColor = Color.FromArgb(20, 35, 55);
        btnEstornar.Enabled = false;
        btnEstornar.FlatAppearance.BorderColor = Color.FromArgb(205, 95, 70);
        btnEstornar.FlatAppearance.BorderSize = 2;
        btnEstornar.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 183, 229);
        btnEstornar.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 51, 75);
        btnEstornar.FlatStyle = FlatStyle.Flat;
        btnEstornar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnEstornar.ForeColor = Color.White;
        btnEstornar.Location = new Point(649, 92);
        btnEstornar.Name = "btnEstornar";
        btnEstornar.Size = new Size(150, 33);
        btnEstornar.TabIndex = 3;
        btnEstornar.Text = "↶  ESTORNAR";
        btnEstornar.UseVisualStyleBackColor = false;
        btnEstornar.Click += btnEstornar_Click;
        // 
        // btnAlterarData
        // 
        btnAlterarData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAlterarData.BackColor = Color.FromArgb(20, 35, 55);
        btnAlterarData.Enabled = false;
        btnAlterarData.FlatAppearance.BorderColor = Color.FromArgb(30, 125, 180);
        btnAlterarData.FlatAppearance.BorderSize = 2;
        btnAlterarData.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 183, 229);
        btnAlterarData.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 51, 75);
        btnAlterarData.FlatStyle = FlatStyle.Flat;
        btnAlterarData.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAlterarData.ForeColor = Color.White;
        btnAlterarData.Location = new Point(498, 92);
        btnAlterarData.Name = "btnAlterarData";
        btnAlterarData.Size = new Size(150, 33);
        btnAlterarData.TabIndex = 3;
        btnAlterarData.Text = "◷  ALTERAR DATA";
        btnAlterarData.UseVisualStyleBackColor = false;
        btnAlterarData.Click += btnAlterarData_Click;
        // 
        // btnEfetivar
        // 
        btnEfetivar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEfetivar.BackColor = Color.FromArgb(20, 35, 55);
        btnEfetivar.Enabled = false;
        btnEfetivar.FlatAppearance.BorderColor = Color.FromArgb(35, 145, 90);
        btnEfetivar.FlatAppearance.BorderSize = 2;
        btnEfetivar.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 183, 229);
        btnEfetivar.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 51, 75);
        btnEfetivar.FlatStyle = FlatStyle.Flat;
        btnEfetivar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnEfetivar.ForeColor = Color.White;
        btnEfetivar.Location = new Point(649, 58);
        btnEfetivar.Name = "btnEfetivar";
        btnEfetivar.Size = new Size(150, 33);
        btnEfetivar.TabIndex = 3;
        btnEfetivar.Text = "✓  EFETIVAR";
        btnEfetivar.UseVisualStyleBackColor = false;
        btnEfetivar.Click += btnEfetivar_Click;
        // 
        // nudDaysBack
        // 
        nudDaysBack.Font = new Font("Segoe UI", 8F);
        nudDaysBack.Location = new Point(8, 77);
        nudDaysBack.Name = "nudDaysBack";
        nudDaysBack.Size = new Size(82, 22);
        nudDaysBack.TabIndex = 3;
        nudDaysBack.TextAlign = HorizontalAlignment.Right;
        // 
        // nudMonthsAhead
        // 
        nudMonthsAhead.Font = new Font("Segoe UI", 8F);
        nudMonthsAhead.Location = new Point(96, 76);
        nudMonthsAhead.Name = "nudMonthsAhead";
        nudMonthsAhead.Size = new Size(82, 22);
        nudMonthsAhead.TabIndex = 6;
        nudMonthsAhead.TextAlign = HorizontalAlignment.Right;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.Font = new Font("Segoe UI", 8F);
        label2.ForeColor = Color.Black;
        label2.ImageAlign = ContentAlignment.MiddleRight;
        label2.Location = new Point(96, 60);
        label2.Name = "label2";
        label2.Size = new Size(82, 13);
        label2.TabIndex = 6;
        label2.Text = "Meses à frente";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.BackColor = Color.Transparent;
        label1.Font = new Font("Segoe UI", 8F);
        label1.ForeColor = Color.Black;
        label1.ImageAlign = ContentAlignment.MiddleRight;
        label1.Location = new Point(6, 61);
        label1.Name = "label1";
        label1.Size = new Size(84, 13);
        label1.TabIndex = 6;
        label1.Text = "Dias anteriores";
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 8F);
        label3.Location = new Point(845, 73);
        label3.Name = "label3";
        label3.Size = new Size(60, 13);
        label3.TabIndex = 7;
        label3.Text = "Realizado:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblRealizado
        // 
        lblRealizado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblRealizado.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblRealizado.ForeColor = Color.DimGray;
        lblRealizado.Location = new Point(911, 73);
        lblRealizado.Margin = new Padding(3, 0, 4, 0);
        lblRealizado.Name = "lblRealizado";
        lblRealizado.Size = new Size(161, 13);
        lblRealizado.TabIndex = 7;
        lblRealizado.Text = "0,00";
        lblRealizado.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblPrevisto
        // 
        lblPrevisto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPrevisto.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblPrevisto.ForeColor = Color.DimGray;
        lblPrevisto.Location = new Point(911, 89);
        lblPrevisto.Margin = new Padding(3, 0, 4, 0);
        lblPrevisto.Name = "lblPrevisto";
        lblPrevisto.Size = new Size(161, 13);
        lblPrevisto.TabIndex = 8;
        lblPrevisto.Text = "0,00";
        lblPrevisto.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label6
        // 
        label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 8F);
        label6.Location = new Point(855, 89);
        label6.Name = "label6";
        label6.Size = new Size(50, 13);
        label6.TabIndex = 9;
        label6.Text = "Previsto:";
        label6.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotal.ForeColor = Color.DimGray;
        lblTotal.Location = new Point(911, 102);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(161, 20);
        lblTotal.TabIndex = 10;
        lblTotal.Text = "0,00";
        lblTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // label8
        // 
        label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label8.AutoSize = true;
        label8.Font = new Font("Segoe UI", 8F);
        label8.Location = new Point(871, 107);
        label8.Name = "label8";
        label8.Size = new Size(34, 13);
        label8.TabIndex = 11;
        label8.Text = "Total:";
        label8.TextAlign = ContentAlignment.MiddleRight;
        // 
        // btnAtualizar
        // 
        btnAtualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAtualizar.BackColor = Color.FromArgb(20, 35, 55);
        btnAtualizar.FlatAppearance.BorderColor = Color.FromArgb(41, 50, 63);
        btnAtualizar.FlatAppearance.BorderSize = 2;
        btnAtualizar.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 183, 229);
        btnAtualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 51, 75);
        btnAtualizar.FlatStyle = FlatStyle.Flat;
        btnAtualizar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAtualizar.ForeColor = Color.White;
        btnAtualizar.Location = new Point(498, 58);
        btnAtualizar.Name = "btnAtualizar";
        btnAtualizar.Size = new Size(150, 33);
        btnAtualizar.TabIndex = 3;
        btnAtualizar.Text = "◊ ATUALIZAR";
        btnAtualizar.UseVisualStyleBackColor = false;
        btnAtualizar.Click += btnAtualizar_Click;
        // 
        // cboAccount
        // 
        cboAccount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboAccount.Cursor = Cursors.Hand;
        cboAccount.DisplayMember = "InstitutionName";
        cboAccount.FormattingEnabled = true;
        cboAccount.Location = new Point(184, 74);
        cboAccount.Name = "cboAccount";
        cboAccount.Size = new Size(306, 25);
        cboAccount.TabIndex = 12;
        cboAccount.ValueMember = "InstitutionId";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.BackColor = Color.Transparent;
        label4.Font = new Font("Segoe UI", 8F);
        label4.ForeColor = Color.Black;
        label4.ImageAlign = ContentAlignment.MiddleRight;
        label4.Location = new Point(184, 60);
        label4.Name = "label4";
        label4.Size = new Size(38, 13);
        label4.TabIndex = 6;
        label4.Text = "Conta";
        // 
        // lblMsg
        // 
        lblMsg.BackColor = Color.Transparent;
        lblMsg.Font = new Font("Segoe UI", 10F);
        lblMsg.ForeColor = Color.FromArgb(178, 178, 178);
        lblMsg.Location = new Point(8, 102);
        lblMsg.Name = "lblMsg";
        lblMsg.Size = new Size(482, 23);
        lblMsg.TabIndex = 6;
        lblMsg.Text = "...";
        lblMsg.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // frmPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 246, 250);
        ClientSize = new Size(1084, 749);
        Controls.Add(cboAccount);
        Controls.Add(btnEstornar);
        Controls.Add(lblTotal);
        Controls.Add(btnAlterarData);
        Controls.Add(label8);
        Controls.Add(btnAtualizar);
        Controls.Add(btnEfetivar);
        Controls.Add(lblPrevisto);
        Controls.Add(label6);
        Controls.Add(lblRealizado);
        Controls.Add(label3);
        Controls.Add(label1);
        Controls.Add(lblMsg);
        Controls.Add(label4);
        Controls.Add(label2);
        Controls.Add(nudMonthsAhead);
        Controls.Add(nudDaysBack);
        Controls.Add(gridLanc);
        Controls.Add(panel1);
        Font = new Font("Segoe UI", 10F);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MinimumSize = new Size(1100, 788);
        Name = "frmPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "XControlFin • Dashboard";
        ((System.ComponentModel.ISupportInitialize)gridLanc).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudDaysBack).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudMonthsAhead).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private Panel panel1;
    private Label lblUser;
    private DataGridView gridLanc;
    private NumericUpDown nudDaysBack;
    private NumericUpDown nudMonthsAhead;
    private Label label2;
    private Label label1;
    private Label lblPeriodo;
    private Label label3;
    private Label label4;
    private Label lblMsg;
    private Label label6;
    private Label lblRealizado;
    private Label lblPrevisto;
    private Label lblTotal;
    private Label label8;
    private Button btnEfetivar;
    private Button btnEstornar;
    private Button btnAlterarData;
    private Button btnAtualizar;
    private ComboBox cboAccount;
}
