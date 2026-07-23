using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.WForm;

public partial class frmLogin : Form
{
    private readonly IDispatcher _dispatcher;
    private bool _isAuthenticating;

    public LocalUserSessionDto? AuthenticatedUser { get; private set; }

    public frmLogin(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        InitializeComponent();
        Shown += frmLogin_Shown;
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private async void btnOk_Click(object sender, EventArgs e)
    {
        if (_isAuthenticating)
        {
            return;
        }

        lblErro.Visible = false;
        var selectedUser = cboUsuario.SelectedItem as LoginUserDto;
        if (selectedUser is null)
        {
            ShowValidation("Selecione um usuário.");
            cboUsuario.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtSenha.Text))
        {
            ShowValidation("Informe a senha.");
            txtSenha.Focus();
            return;
        }

        try
        {
            SetBusy(true);
            AuthenticatedUser = await _dispatcher.SendAsync<LocalUserSessionDto?>(
                new LoginLocalCommand(selectedUser.Id, txtSenha.Text));

            if (AuthenticatedUser is null)
            {
                txtSenha.Clear();
                ShowValidation("Usuário ou senha inválidos.");
                txtSenha.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception exception)
        {
            GlobalExceptionHandler.Handle(exception, "autenticação");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void frmLogin_Shown(object? sender, EventArgs e)
    {
        try
        {
            SetBusy(true);
            var users = await _dispatcher.QueryAsync(new GetActiveLoginUsersQuery());
            cboUsuario.DataSource = users;

            if (users.Count == 0)
            {
                ShowValidation("Nenhum usuário ativo foi encontrado.");
                btnOk.Enabled = false;
                return;
            }

            cboUsuario.SelectedIndex = 0;
            txtSenha.Focus();
        }
        catch (Exception exception)
        {
            GlobalExceptionHandler.Handle(exception, "carregamento dos usuários");
            btnOk.Enabled = false;
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool busy)
    {
        _isAuthenticating = busy;
        UseWaitCursor = busy;
        cboUsuario.Enabled = !busy;
        txtSenha.Enabled = !busy;
        btnOk.Enabled = !busy;
        btnCancelar.Enabled = !busy;
        btnOk.Text = busy ? "AGUARDE..." : "ENTRAR";
    }

    private void ShowValidation(string message)
    {
        lblErro.Text = message;
        lblErro.Visible = true;
    }
}
