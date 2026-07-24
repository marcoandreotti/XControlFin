using System.ComponentModel;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Dashboard.Commands;
using xControlFin.Application.Features.Dashboard.Dtos;
using xControlFin.Application.Features.Dashboard.Queries;
using xControlFin.Shared.Abstractions;

namespace xControlFin.WForm;

public partial class frmPrincipal : Form
{
    private readonly LocalUserSessionDto _user;
    private readonly IDispatcher _dispatcher;

    private readonly DashboardSettings _settings;

    public frmPrincipal(LocalUserSessionDto user, IDispatcher dispatcher)
    {
        _user = user;
        _dispatcher = dispatcher;
        _settings = DashboardSettingsStore.Load();

        InitializeComponent();
        BuildFilter();
        BuildGrid();
        Shown += async (_, _) => await LoadDashboardAsync();
        lblUser.Text = $"Olá, {_user.Name}";
    }

    private void BuildFilter()
    {
        nudDaysBack.Minimum = 0;
        nudDaysBack.Maximum = 365;
        nudDaysBack.Value = _settings.DaysBack;

        nudMonthsAhead.Minimum = 0;
        nudMonthsAhead.Maximum = 36;
        nudMonthsAhead.Value = _settings.MonthsAhead;

        //_refreshButton.SetBounds(244, 29, 112, 36);
        //_refreshButton.Text = "ATUALIZAR";
        //_refreshButton.BackColor = Color.FromArgb(30, 165, 211);
        //_refreshButton.ForeColor = Color.White;
        //_refreshButton.FlatStyle = FlatStyle.Flat;
        //_refreshButton.FlatAppearance.BorderSize = 0;
        //_refreshButton.Click += async (_, _) => await ApplyFilterAsync();
    }

    private async Task ApplyFilterAsync()
    {
        _settings.DaysBack = decimal.ToInt32(nudDaysBack.Value);
        _settings.MonthsAhead = decimal.ToInt32(nudMonthsAhead.Value);
        DashboardSettingsStore.Save(_settings);
        await LoadDashboardAsync();
    }

    private async Task LoadDashboardAsync()
    {
        var startDate = DateTime.Today.AddDays(-_settings.DaysBack);
        var endDate = DateTime.Today.AddMonths(_settings.MonthsAhead);
        lblPeriodo.Text = $"Exibindo de {startDate:dd/MM/yyyy} até {endDate:dd/MM/yyyy}";

        try
        {
            SetBusy(true);
            var dashboard = await _dispatcher.QueryAsync(
                new GetDashboardQuery(_user.UserId, startDate, endDate, DateTime.Today));
            //RenderBalances(dashboard);
            gridLanc.DataSource = new BindingList<DashboardReleaseDto>(dashboard.Releases);
            gridLanc.ClearSelection();
            UpdateActionState();
        }
        catch (Exception exception)
        {
            GlobalExceptionHandler.Handle(exception, "carregamento do dashboard");
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void BuildGrid()
    {
        AddGridColumn("PaymentDate", "Data", 90, "dd/MM/yyyy");
        AddGridColumn("Institution", "Conta", 150);
        AddGridColumn("CostCenter", "Centro de custo", 150);
        AddGridColumn("Historic", "Histórico", 300);
        AddGridColumn("Status", "Situação", 100);
        AddGridColumn("Value", "Valor", 110, "C2");
        gridLanc.CellFormatting += Grid_CellFormatting;
        gridLanc.SelectionChanged += (_, _) => UpdateActionState();
    }

    private void AddGridColumn(string property, string header, int fillWeight, string? format = null)
    {
        gridLanc.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = property,
            HeaderText = header,
            FillWeight = fillWeight,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = format,
                Alignment = property == "Value"
                    ? DataGridViewContentAlignment.MiddleRight
                    : DataGridViewContentAlignment.MiddleLeft
            }
        });
    }

    private static void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid ||
            grid.Rows[e.RowIndex].DataBoundItem is not DashboardReleaseDto item)
        {
            return;
        }

        if (grid.Columns[e.ColumnIndex].DataPropertyName == "Status")
        {
            e.CellStyle.ForeColor = item.Realized
                ? Color.FromArgb(35, 135, 85)
                : Color.FromArgb(190, 125, 35);
            e.CellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        }
    }

    private void SetBusy(bool busy)
    {
        UseWaitCursor = busy;
        //_refreshButton.Enabled = !busy;
        nudDaysBack.Enabled = !busy;
        nudMonthsAhead.Enabled = !busy;
        if (busy)
        {
            //_effectuateButton.Enabled = false;
            //_changeDateButton.Enabled = false;
            //_reverseButton.Enabled = false;
        }
        else
        {
            UpdateActionState();
        }
    }

    private List<DashboardReleaseDto> GetSelectedMovements()
    {
        return gridLanc.SelectedRows
            .Cast<DataGridViewRow>()
            .Select(row => row.DataBoundItem)
            .OfType<DashboardReleaseDto>()
            .Distinct()
            .ToList();
    }

    private async Task ChangeSelectedDatesAsync()
    {
        var selected = GetSelectedMovements();
        if (selected.Count == 0)
            return;

        using var dialog = new MovementDateDialog(
            "Alterar datas",
            "Informe a nova data dos lançamentos:",
            selected.Min(item => item.PaymentDate));
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        if (!ConfirmOperation(
                $"Alterar a data de {selected.Count} lançamento(ões) para {dialog.SelectedDate:dd/MM/yyyy}?"))
            return;

        var items = selected.Select(ToSelection).ToList();
        await ExecuteMovementCommandAsync(
            () => _dispatcher.SendAsync(
                new ChangeDashboardMovementDatesCommand(items, dialog.SelectedDate)),
            "alteração das datas");
    }

    private void UpdateActionState()
    {
        var selected = GetSelectedMovements();
        //_selectionLabel.Text = selected.Count == 0
        //    ? "Selecione um ou mais lançamentos"
        //    : $"{selected.Count} lançamento(ões) selecionado(s)";
        //_effectuateButton.Enabled = selected.Count > 0 && selected.All(item => !item.Realized);
        //_changeDateButton.Enabled = selected.Count > 0;
        //_reverseButton.Enabled = selected.Count > 0 &&
        //                         selected.All(item => item.Realized && item.ReleaseId.HasValue);
    }

    private async Task ReverseSelectedAsync()
    {
        var selected = GetSelectedMovements();
        if (selected.Count == 0 ||
            selected.Any(item => !item.Realized || !item.ReleaseId.HasValue))
            return;

        if (!ConfirmOperation(
                $"Estornar {selected.Count} lançamento(ões)? Eles voltarão para a situação prevista."))
            return;

        var releaseIds = selected.Select(item => item.ReleaseId!.Value).ToList();
        await ExecuteMovementCommandAsync(
            () => _dispatcher.SendAsync(new ReverseDashboardMovementsCommand(releaseIds)),
            "estorno dos lançamentos");
    }

    private async Task ExecuteMovementCommandAsync(Func<Task> operation, string operationName)
    {
        try
        {
            SetBusy(true);
            await operation();
            await LoadDashboardAsync();
        }
        catch (Exception exception)
        {
            GlobalExceptionHandler.Handle(exception, operationName);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private static DashboardMovementSelection ToSelection(DashboardReleaseDto movement) =>
        new(movement.ReleaseId, movement.PlanningId, movement.ScheduledDate);

    private bool ConfirmOperation(string message) =>
        MessageBox.Show(
            this,
            message,
            "Confirmar operação",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    private static void ConfigureActionButton(
        Button button,
        string text,
        int left,
        Color background)
    {
        button.SetBounds(left, 12, 128, 38);
        button.Text = text;
        button.BackColor = background;
        button.ForeColor = Color.White;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        button.Cursor = Cursors.Hand;
    }

    private async void btnAtualizar_Click(object sender, EventArgs e)
    {
        await ApplyFilterAsync();
    }
}