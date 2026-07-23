namespace xControlFin.WForm;

partial class frmLogin
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
        pictureBox1 = new PictureBox();
        btnOk = new Button();
        btnCancelar = new Button();
        label1 = new Label();
        txtSenha = new TextBox();
        label2 = new Label();
        cboUsuario = new ComboBox();
        lblErro = new Label();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(80, 12);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(472, 161);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.BackColor = Color.White;
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(30, 165, 211);
        btnOk.FlatAppearance.BorderSize = 2;
        btnOk.FlatAppearance.MouseDownBackColor = Color.Silver;
        btnOk.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
        btnOk.FlatStyle = FlatStyle.Flat;
        btnOk.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnOk.ForeColor = Color.FromArgb(20, 35, 55);
        btnOk.Location = new Point(529, 391);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(104, 47);
        btnOk.TabIndex = 2;
        btnOk.Text = "ENTRAR";
        btnOk.UseVisualStyleBackColor = false;
        btnOk.Click += this.btnOk_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancelar.BackColor = Color.White;
        btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(30, 165, 211);
        btnCancelar.FlatAppearance.BorderSize = 2;
        btnCancelar.FlatAppearance.MouseDownBackColor = Color.Silver;
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnCancelar.ForeColor = Color.FromArgb(20, 35, 55);
        btnCancelar.Location = new Point(419, 391);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(104, 47);
        btnCancelar.TabIndex = 3;
        btnCancelar.Text = "CANCELAR";
        btnCancelar.UseVisualStyleBackColor = false;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.ForeColor = Color.White;
        label1.Location = new Point(160, 200);
        label1.Name = "label1";
        label1.Size = new Size(47, 15);
        label1.TabIndex = 2;
        label1.Text = "Usuário";
        // 
        // txtSenha
        // 
        txtSenha.Font = new Font("Segoe UI", 12F);
        txtSenha.Location = new Point(160, 283);
        txtSenha.Name = "txtSenha";
        txtSenha.Size = new Size(298, 29);
        txtSenha.TabIndex = 1;
        txtSenha.UseSystemPasswordChar = true;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = Color.White;
        label2.Location = new Point(160, 265);
        label2.Name = "label2";
        label2.Size = new Size(39, 15);
        label2.TabIndex = 4;
        label2.Text = "Senha";
        // 
        // cboUsuario
        // 
        cboUsuario.Font = new Font("Segoe UI", 12F);
        cboUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
        cboUsuario.FormattingEnabled = true;
        cboUsuario.Location = new Point(160, 218);
        cboUsuario.Name = "cboUsuario";
        cboUsuario.Size = new Size(298, 29);
        cboUsuario.TabIndex = 0;
        // 
        // lblErro
        // 
        lblErro.ForeColor = Color.FromArgb(255, 170, 170);
        lblErro.Location = new Point(160, 325);
        lblErro.Name = "lblErro";
        lblErro.Size = new Size(298, 42);
        lblErro.TabIndex = 7;
        lblErro.TextAlign = ContentAlignment.MiddleCenter;
        lblErro.Visible = false;
        // 
        // frmLogin
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(20, 35, 55);
        CancelButton = btnCancelar;
        ClientSize = new Size(655, 450);
        Controls.Add(lblErro);
        Controls.Add(cboUsuario);
        Controls.Add(txtSenha);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(btnCancelar);
        Controls.Add(btnOk);
        Controls.Add(pictureBox1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MaximumSize = new Size(671, 489);
        MinimumSize = new Size(671, 489);
        Name = "frmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Login";
        AcceptButton = btnOk;
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private Button btnOk;
    private Button btnCancelar;
    private Label label1;
    private TextBox txtSenha;
    private Label label2;
    private ComboBox cboUsuario;
    private Label lblErro;
}
