namespace xControlFin.WForm;

internal sealed class MovementDateDialog : Form
{
    private readonly DateTimePicker _datePicker = new();

    public DateTime SelectedDate => _datePicker.Value.Date;

    public MovementDateDialog(
        string title,
        string instruction,
        DateTime initialDate,
        DateTime? minimumDate = null)
    {
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        BackColor = Color.White;
        ClientSize = new Size(410, 190);
        Font = new Font("Segoe UI", 10);

        var accent = new Panel
        {
            BackColor = Color.FromArgb(30, 165, 211),
            Dock = DockStyle.Top,
            Height = 6
        };
        var label = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(20, 35, 55),
            Location = new Point(24, 30),
            Text = instruction
        };

        _datePicker.Format = DateTimePickerFormat.Custom;
        _datePicker.CustomFormat = "dddd, dd 'de' MMMM 'de' yyyy";
        _datePicker.SetBounds(24, 69, 362, 32);
        _datePicker.Value = initialDate < DateTimePicker.MinimumDateTime
            ? DateTime.Today
            : initialDate;
        if (minimumDate.HasValue)
            _datePicker.MinDate = minimumDate.Value.Date;

        var cancel = CreateButton("CANCELAR", 176, Color.FromArgb(105, 115, 125));
        cancel.DialogResult = DialogResult.Cancel;
        var confirm = CreateButton("CONFIRMAR", 282, Color.FromArgb(30, 145, 95));
        confirm.DialogResult = DialogResult.OK;

        Controls.AddRange([accent, label, _datePicker, cancel, confirm]);
        AcceptButton = confirm;
        CancelButton = cancel;
    }

    private static Button CreateButton(string text, int left, Color color) => new()
    {
        Text = text,
        BackColor = color,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Location = new Point(left, 130),
        Size = new Size(104, 38),
        Cursor = Cursors.Hand
    };
}
