Public Class MainForm
    Inherits Form

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - Slidely Form App"
        Me.ClientSize = New Size(450, 300)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' Set the form background color
        Me.BackColor = Color.FromArgb(240, 240, 240)

        ' Create "View Submissions" Button
        Dim btnViewSubmissions As New Button()
        btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
        btnViewSubmissions.Size = New Size(300, 40)
        btnViewSubmissions.Location = New Point(25, 25)
        btnViewSubmissions.Font = New Font("Arial", 10, FontStyle.Bold)
        btnViewSubmissions.BackColor = Color.FromArgb(100, 149, 237) ' Cornflower Blue
        btnViewSubmissions.ForeColor = Color.White
        btnViewSubmissions.FlatStyle = FlatStyle.Flat
        btnViewSubmissions.FlatAppearance.BorderSize = 0
        AddHandler btnViewSubmissions.Click, AddressOf BtnViewSubmissions_Click
        Me.Controls.Add(btnViewSubmissions)

        ' Create "Create Submission" Button
        Dim btnCreateSubmission As New Button()
        btnCreateSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        btnCreateSubmission.Size = New Size(300, 40)
        btnCreateSubmission.Location = New Point(25, 85)
        btnCreateSubmission.Font = New Font("Arial", 10, FontStyle.Bold)
        btnCreateSubmission.BackColor = Color.FromArgb(60, 179, 113) ' Medium Sea Green
        btnCreateSubmission.ForeColor = Color.White
        btnCreateSubmission.FlatStyle = FlatStyle.Flat
        btnCreateSubmission.FlatAppearance.BorderSize = 0
        AddHandler btnCreateSubmission.Click, AddressOf BtnCreateSubmission_Click
        Me.Controls.Add(btnCreateSubmission)

        ' Assign keyboard shortcuts
        Dim viewSubmissions As New MenuItem("View Submissions", New EventHandler(AddressOf BtnViewSubmissions_Click))
        viewSubmissions.Shortcut = Shortcut.CtrlV
        Dim createSubmission As New MenuItem("Create Submission", New EventHandler(AddressOf BtnCreateSubmission_Click))
        createSubmission.Shortcut = Shortcut.CtrlN
        Dim menu As New MainMenu(New MenuItem() {viewSubmissions, createSubmission})
        Me.Menu = menu
    End Sub

    Private Sub BtnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.Show()
    End Sub

    Private Sub BtnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createSubmissionForm As New CreateSubmissionForm()
        createSubmissionForm.Show()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(800, 350)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
End Class