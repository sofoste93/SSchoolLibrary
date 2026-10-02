using SchoolLibrary;
using System.Drawing.Imaging;

namespace SchoolFormApp;

/// <summary>
/// A small Windows front end for the reusable SchoolLibrary domain project.
/// The controls are created in code so learners can follow the complete layout
/// without depending on generated designer files.
/// </summary>
public sealed class MainForm : Form
{
    private static readonly Color Paper = Color.FromArgb(244, 237, 218);
    private static readonly Color Card = Color.FromArgb(255, 251, 239);
    private static readonly Color Ink = Color.FromArgb(49, 43, 36);
    private static readonly Color Oxblood = Color.FromArgb(105, 48, 45);
    private static readonly Color Forest = Color.FromArgb(54, 79, 62);
    private static readonly Color Gold = Color.FromArgb(190, 153, 75);
    private static readonly Color Muted = Color.FromArgb(112, 102, 86);

    private readonly Dictionary<string, TextBox> schoolFields = new();
    private readonly TextBox studentName = CreateTextBox();
    private readonly ComboBox subject = new();
    private readonly NumericUpDown[] scores = new NumericUpDown[3];
    private readonly Label resultAverage = new();
    private readonly Label resultGrade = new();
    private readonly Label resultComment = new();
    private readonly DataGridView register = new();
    private readonly Label status = new();

    public MainForm(bool showDemoData = false)
    {
        Text = "SSchoolLibrary · Academic register";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(980, 680);
        ClientSize = new Size(1080, 700);
        BackColor = Paper;
        ForeColor = Ink;
        Font = new Font("Segoe UI", 9.5f);
        AutoScaleMode = AutoScaleMode.Dpi;

        Controls.Add(BuildLayout());

        if (showDemoData)
            PopulateDemoData();
    }

    private Control BuildLayout()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            BackColor = Paper,
            Padding = Padding.Empty
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        layout.Controls.Add(BuildHeader(), 0, 0);
        layout.Controls.Add(BuildWorkspace(), 0, 1);
        layout.Controls.Add(BuildFooter(), 0, 2);
        return layout;
    }

    private Control BuildHeader()
    {
        var header = new Panel { Dock = DockStyle.Fill, BackColor = Oxblood, Padding = new Padding(30, 16, 30, 12) };
        var title = new Label
        {
            Text = "SCHOOL LIBRARY",
            ForeColor = Color.White,
            Font = new Font("Georgia", 22, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(28, 14)
        };
        var subtitle = new Label
        {
            Text = "ACADEMIC REGISTER  ·  RESTORED EDITION 2026",
            ForeColor = Color.FromArgb(231, 212, 180),
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(31, 57)
        };
        var seal = new Label
        {
            Text = "SFS\n№ 02",
            ForeColor = Color.FromArgb(246, 226, 174),
            Font = new Font("Georgia", 11, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            BorderStyle = BorderStyle.FixedSingle,
            Size = new Size(66, 55),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            Location = new Point(ClientSize.Width - 98, 18)
        };
        header.Controls.Add(title);
        header.Controls.Add(subtitle);
        header.Controls.Add(seal);
        header.Resize += (_, _) => seal.Left = header.ClientSize.Width - seal.Width - 30;
        return header;
    }

    private Control BuildWorkspace()
    {
        var workspace = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(22, 20, 22, 18),
            BackColor = Paper
        };
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37));
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63));
        workspace.Controls.Add(BuildSchoolCard(), 0, 0);
        workspace.Controls.Add(BuildGradebookCard(), 1, 0);
        return workspace;
    }

    private Control BuildSchoolCard()
    {
        var card = CreateCard(new Padding(22));
        card.Margin = new Padding(0, 0, 14, 0);

        var content = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 61));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        content.Controls.Add(CreateSectionHeading("I  ·  SCHOOL PROFILE", "The institution shown on the register"), 0, 0);

        var fields = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, AutoScroll = true };
        AddSchoolField(fields, "Name", "name");
        AddSchoolField(fields, "Address", "address");
        AddSchoolField(fields, "City", "city");
        AddSchoolField(fields, "Region / State", "region");
        AddSchoolField(fields, "Postal code", "postal");
        AddSchoolField(fields, "Phone", "phone");
        AddSchoolField(fields, "Social handle", "social");
        content.Controls.Add(fields, 0, 1);

        var save = CreateButton("VALIDATE PROFILE", Forest);
        save.Dock = DockStyle.Fill;
        save.Margin = new Padding(0, 7, 0, 5);
        save.Click += (_, _) => ValidateSchool();
        content.Controls.Add(save, 0, 2);

        var hint = new Label
        {
            Text = "Required: school name and city",
            ForeColor = Muted,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(Font, FontStyle.Italic)
        };
        content.Controls.Add(hint, 0, 3);
        card.Controls.Add(content);
        return card;
    }

    private Control BuildGradebookCard()
    {
        var card = CreateCard(new Padding(22));
        card.Margin = new Padding(0);

        var content = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 61));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 154));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 82));
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        content.Controls.Add(CreateSectionHeading("II  ·  STUDENT GRADEBOOK", "Three scores become one clear evaluation"), 0, 0);
        content.Controls.Add(BuildEvaluationForm(), 0, 1);
        content.Controls.Add(BuildResultStrip(), 0, 2);
        content.Controls.Add(BuildRegister(), 0, 3);
        card.Controls.Add(content);
        return card;
    }

    private Control BuildEvaluationForm()
    {
        var form = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4, Padding = new Padding(0, 2, 0, 6) };
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        form.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
        form.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        form.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        form.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        form.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

        form.Controls.Add(CreateFieldLabel("Student name"), 0, 0);
        form.Controls.Add(CreateFieldLabel("Subject"), 1, 0);
        form.Controls.Add(CreateFieldLabel("Score 1"), 2, 0);
        form.Controls.Add(CreateFieldLabel("Score 2"), 3, 0);
        studentName.Dock = DockStyle.Fill;
        studentName.Margin = new Padding(0, 0, 9, 3);
        form.Controls.Add(studentName, 0, 1);

        subject.DropDownStyle = ComboBoxStyle.DropDownList;
        subject.Items.AddRange(new object[] { "Mathematics", "English", "Science", "History", "Arts" });
        subject.SelectedIndex = 0;
        subject.Dock = DockStyle.Fill;
        subject.Margin = new Padding(0, 0, 9, 3);
        form.Controls.Add(subject, 1, 1);

        for (var index = 0; index < scores.Length; index++)
        {
            scores[index] = CreateScoreInput(75 + index * 5);
            scores[index].Margin = new Padding(0, 0, index == 2 ? 0 : 9, 3);
        }
        form.Controls.Add(scores[0], 2, 1);
        form.Controls.Add(scores[1], 3, 1);

        form.Controls.Add(CreateFieldLabel("Score 3"), 0, 2);
        form.Controls.Add(scores[2], 0, 3);

        var evaluate = CreateButton("CALCULATE & ADD", Oxblood);
        evaluate.Dock = DockStyle.Fill;
        evaluate.Margin = new Padding(10, 3, 0, 0);
        evaluate.Click += (_, _) => EvaluateStudent();
        form.SetColumnSpan(evaluate, 3);
        form.Controls.Add(evaluate, 1, 3);
        return form;
    }

    private Control BuildResultStrip()
    {
        var strip = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            BackColor = Color.FromArgb(235, 225, 197),
            Padding = new Padding(16, 8, 16, 8),
            Margin = new Padding(0, 4, 0, 10)
        };
        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86));
        strip.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        resultAverage.Text = "—";
        resultAverage.Font = new Font("Georgia", 18, FontStyle.Bold);
        resultAverage.ForeColor = Forest;
        resultAverage.Dock = DockStyle.Fill;
        resultAverage.TextAlign = ContentAlignment.MiddleLeft;
        resultGrade.Text = "—";
        resultGrade.Font = new Font("Georgia", 23, FontStyle.Bold);
        resultGrade.ForeColor = Oxblood;
        resultGrade.Dock = DockStyle.Fill;
        resultGrade.TextAlign = ContentAlignment.MiddleCenter;
        resultComment.Text = "Ready for the first evaluation";
        resultComment.ForeColor = Ink;
        resultComment.Dock = DockStyle.Fill;
        resultComment.TextAlign = ContentAlignment.MiddleLeft;
        strip.Controls.Add(resultAverage, 0, 0);
        strip.Controls.Add(resultGrade, 1, 0);
        strip.Controls.Add(resultComment, 2, 0);
        return strip;
    }

    private Control BuildRegister()
    {
        register.Dock = DockStyle.Fill;
        register.AllowUserToAddRows = false;
        register.AllowUserToDeleteRows = false;
        register.AllowUserToResizeRows = false;
        register.ReadOnly = true;
        register.RowHeadersVisible = false;
        register.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        register.BackgroundColor = Card;
        register.BorderStyle = BorderStyle.None;
        register.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        register.GridColor = Color.FromArgb(220, 209, 183);
        register.EnableHeadersVisualStyles = false;
        register.ColumnHeadersDefaultCellStyle.BackColor = Forest;
        register.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        register.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
        register.ColumnHeadersHeight = 34;
        register.DefaultCellStyle.BackColor = Card;
        register.DefaultCellStyle.ForeColor = Ink;
        register.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 210, 178);
        register.DefaultCellStyle.SelectionForeColor = Ink;
        register.RowTemplate.Height = 32;
        register.Columns.Add("student", "STUDENT");
        register.Columns.Add("subject", "SUBJECT");
        register.Columns.Add("average", "AVERAGE");
        register.Columns.Add("grade", "GRADE");
        register.Columns[2].FillWeight = 65;
        register.Columns[3].FillWeight = 55;
        return register;
    }

    private Control BuildFooter()
    {
        var footer = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(224, 213, 187) };
        status.Text = "READY  ·  Data stays on this computer during the session";
        status.ForeColor = Forest;
        status.Dock = DockStyle.Fill;
        status.Padding = new Padding(24, 0, 0, 0);
        status.TextAlign = ContentAlignment.MiddleLeft;
        status.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        footer.Controls.Add(status);
        return footer;
    }

    private void AddSchoolField(TableLayoutPanel fields, string label, string key)
    {
        fields.RowCount += 2;
        fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
        fields.RowStyles.Add(new RowStyle(SizeType.Absolute, 37));
        fields.Controls.Add(CreateFieldLabel(label), 0, fields.RowCount - 2);
        var input = CreateTextBox();
        input.Dock = DockStyle.Fill;
        input.Margin = new Padding(0, 0, 4, 6);
        fields.Controls.Add(input, 0, fields.RowCount - 1);
        schoolFields[key] = input;
    }

    private void ValidateSchool()
    {
        var school = new School
        {
            Name = schoolFields["name"].Text,
            Address = schoolFields["address"].Text,
            City = schoolFields["city"].Text,
            Region = schoolFields["region"].Text,
            PostalCode = schoolFields["postal"].Text,
            PhoneNumber = schoolFields["phone"].Text,
            SocialHandle = schoolFields["social"].Text
        };
        var errors = school.Validate();
        SetStatus(errors.Count == 0
            ? $"PROFILE VALID  ·  {school.Name.Trim()}  ·  {school.FormattedAddress}"
            : $"CHECK PROFILE  ·  {errors[0]}", errors.Count == 0);
    }

    private void EvaluateStudent()
    {
        try
        {
            var evaluation = GradeService.Evaluate(studentName.Text, subject.Text, scores.Select(score => score.Value));
            resultAverage.Text = $"{evaluation.Average:0.0}%";
            resultGrade.Text = evaluation.Grade;
            resultComment.Text = evaluation.Comment;
            register.Rows.Insert(0, evaluation.StudentName, evaluation.Subject, $"{evaluation.Average:0.0}%", evaluation.Grade);
            SetStatus($"ADDED  ·  {evaluation.StudentName}'s evaluation is in the register", true);
            studentName.SelectAll();
            studentName.Focus();
        }
        catch (ArgumentException exception)
        {
            SetStatus($"CHECK ENTRY  ·  {exception.Message.Split(Environment.NewLine)[0]}", false);
            studentName.Focus();
        }
    }

    private void SetStatus(string message, bool success)
    {
        status.Text = message.ToUpperInvariant();
        status.ForeColor = success ? Forest : Oxblood;
    }

    private void PopulateDemoData()
    {
        schoolFields["name"].Text = "Northstar Academy";
        schoolFields["address"].Text = "12 Observatory Lane";
        schoolFields["city"].Text = "Berlin";
        schoolFields["region"].Text = "BE";
        schoolFields["postal"].Text = "10115";
        schoolFields["phone"].Text = "+49 30 555 0192";
        schoolFields["social"].Text = "@northstar";
        studentName.Text = "Amelia Stone";
        subject.SelectedItem = "Science";
        scores[0].Value = 88;
        scores[1].Value = 94;
        scores[2].Value = 91;
        ValidateSchool();
        EvaluateStudent();
    }

    public void RenderScreenshot(string filePath)
    {
        StartPosition = FormStartPosition.Manual;
        Location = new Point(-20000, -20000);
        ShowInTaskbar = false;
        Show();
        Application.DoEvents();
        PerformLayout();

        var absolutePath = Path.GetFullPath(filePath);
        Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);
        using var bitmap = new Bitmap(Width, Height);
        DrawToBitmap(bitmap, new Rectangle(Point.Empty, Size));
        bitmap.Save(absolutePath, ImageFormat.Png);
        Close();
    }

    private static Panel CreateCard(Padding padding) => new()
    {
        Dock = DockStyle.Fill,
        BackColor = Card,
        Padding = padding,
        BorderStyle = BorderStyle.FixedSingle
    };

    private static Control CreateSectionHeading(string title, string subtitle)
    {
        var panel = new Panel { Dock = DockStyle.Fill };
        panel.Controls.Add(new Label
        {
            Text = subtitle,
            ForeColor = Muted,
            Font = new Font("Segoe UI", 8.5f),
            AutoSize = true,
            Location = new Point(1, 31)
        });
        panel.Controls.Add(new Label
        {
            Text = title,
            ForeColor = Oxblood,
            Font = new Font("Georgia", 13, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(0, 3)
        });
        return panel;
    }

    private static Label CreateFieldLabel(string text) => new()
    {
        Text = text.ToUpperInvariant(),
        ForeColor = Muted,
        Dock = DockStyle.Fill,
        TextAlign = ContentAlignment.BottomLeft,
        Font = new Font("Segoe UI", 8, FontStyle.Bold)
    };

    private static TextBox CreateTextBox() => new()
    {
        BorderStyle = BorderStyle.FixedSingle,
        BackColor = Color.White,
        ForeColor = Ink,
        Font = new Font("Segoe UI", 10)
    };

    private static NumericUpDown CreateScoreInput(decimal value) => new()
    {
        Minimum = 0,
        Maximum = 100,
        DecimalPlaces = 0,
        Value = value,
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        ForeColor = Ink,
        Font = new Font("Segoe UI", 10)
    };

    private static Button CreateButton(string text, Color color) => new()
    {
        Text = text,
        BackColor = color,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand,
        Font = new Font("Segoe UI", 9, FontStyle.Bold),
        UseVisualStyleBackColor = false
    };
}
