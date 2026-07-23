using xControlFin.Application.Features.Auth.Dtos;

namespace xControlFin.WForm;

public sealed class frmPrincipal : Form
{
    public frmPrincipal(LocalUserSessionDto user)
    {
        Text = "XControlFin";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 600);
        BackColor = Color.FromArgb(242, 246, 250);

        var header = new Panel
        {
            BackColor = Color.FromArgb(20, 35, 55),
            Dock = DockStyle.Top,
            Height = 76
        };
        var title = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.White,
            Location = new Point(24, 18),
            Text = "XControlFin"
        };
        var userLabel = new Label
        {
            AutoSize = true,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.FromArgb(190, 220, 235),
            Text = $"Olá, {user.Name}",
            Location = new Point(700, 28)
        };
        header.Controls.Add(title);
        header.Controls.Add(userLabel);
        header.Resize += (_, _) => userLabel.Left = header.ClientSize.Width - userLabel.Width - 24;

        var welcome = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 35, 55),
            Location = new Point(36, 118),
            Text = "Bem-vindo ao seu controle financeiro"
        };
        var description = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(80, 95, 110),
            Location = new Point(40, 164),
            Text = "Sua sessão local foi iniciada com segurança."
        };

        Controls.Add(description);
        Controls.Add(welcome);
        Controls.Add(header);
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
        SuspendLayout();
        //
        // frmPrincipal
        //
        ClientSize = new Size(284, 261);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "frmPrincipal";
        ResumeLayout(false);
    }
}