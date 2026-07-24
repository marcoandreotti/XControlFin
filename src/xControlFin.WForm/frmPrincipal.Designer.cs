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
        nudDaysBack = new NumericUpDown();
        nudMonthsAhead = new NumericUpDown();
        label2 = new Label();
        label1 = new Label();
        btnAtualizar = new Button();
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
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 8F);
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
        pictureBox1.Size = new Size(206, 62);
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
        panel1.Size = new Size(1086, 70);
        panel1.TabIndex = 1;
        // 
        // lblPeriodo
        // 
        lblPeriodo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPeriodo.Font = new Font("Segoe UI", 7F);
        lblPeriodo.ForeColor = Color.Gray;
        lblPeriodo.Location = new Point(650, 49);
        lblPeriodo.Name = "lblPeriodo";
        lblPeriodo.Size = new Size(422, 21);
        lblPeriodo.TabIndex = 2;
        lblPeriodo.Text = "Olá";
        lblPeriodo.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblUser
        // 
        lblUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblUser.Font = new Font("Segoe UI", 12F);
        lblUser.ForeColor = Color.FromArgb(252, 114, 0);
        lblUser.Location = new Point(669, 26);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(403, 21);
        lblUser.TabIndex = 1;
        lblUser.Text = "Olá";
        lblUser.TextAlign = ContentAlignment.MiddleRight;
        // 
        // nudDaysBack
        // 
        nudDaysBack.Location = new Point(3, 97);
        nudDaysBack.Name = "nudDaysBack";
        nudDaysBack.Size = new Size(84, 25);
        nudDaysBack.TabIndex = 3;
        // 
        // nudMonthsAhead
        // 
        nudMonthsAhead.Location = new Point(93, 97);
        nudMonthsAhead.Name = "nudMonthsAhead";
        nudMonthsAhead.Size = new Size(82, 25);
        nudMonthsAhead.TabIndex = 6;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.BackColor = Color.Transparent;
        label2.Font = new Font("Segoe UI", 8F);
        label2.ForeColor = Color.Black;
        label2.Location = new Point(93, 81);
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
        label1.Location = new Point(3, 81);
        label1.Name = "label1";
        label1.Size = new Size(84, 13);
        label1.TabIndex = 6;
        label1.Text = "Dias anteriores";
        // 
        // btnAtualizar
        // 
        btnAtualizar.BackColor = Color.White;
        btnAtualizar.FlatAppearance.BorderColor = Color.FromArgb(30, 165, 211);
        btnAtualizar.FlatAppearance.BorderSize = 2;
        btnAtualizar.FlatAppearance.MouseDownBackColor = Color.Silver;
        btnAtualizar.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
        btnAtualizar.FlatStyle = FlatStyle.Flat;
        btnAtualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnAtualizar.ForeColor = Color.FromArgb(20, 35, 55);
        btnAtualizar.Location = new Point(181, 76);
        btnAtualizar.Name = "btnAtualizar";
        btnAtualizar.Size = new Size(110, 47);
        btnAtualizar.TabIndex = 6;
        btnAtualizar.Text = "ATUALIZAR";
        btnAtualizar.UseVisualStyleBackColor = false;
        btnAtualizar.Click += btnAtualizar_Click;
        // 
        // frmPrincipal
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 246, 250);
        ClientSize = new Size(1084, 749);
        Controls.Add(btnAtualizar);
        Controls.Add(label1);
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
    private Button btnAtualizar;
    private Label lblPeriodo;
}