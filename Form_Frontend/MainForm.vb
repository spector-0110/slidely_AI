Imports System.Drawing
Imports System.Windows.Forms

Public Class MainForm
    Inherits Form

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - Slidely Form App"
        Me.ClientSize = New Size(450, 300)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the form background color
        Me.BackColor = Color.FromArgb(245, 245, 245) ' Light grey background

        ' Create "View Submissions" Button
        Dim btnViewSubmissions As New Button()
        btnViewSubmissions.Text = "VIEW SUBMISSIONS"
        btnViewSubmissions.Size = New Size(300, 50)
        btnViewSubmissions.Location = New Point(75, 50)
        btnViewSubmissions.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnViewSubmissions.BackColor = Color.FromArgb(52, 152, 219) ' Peter River Blue
        btnViewSubmissions.ForeColor = Color.White
        btnViewSubmissions.FlatStyle = FlatStyle.Flat
        btnViewSubmissions.FlatAppearance.BorderSize = 0
        btnViewSubmissions.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185) ' Darker blue on hover
        AddHandler btnViewSubmissions.Click, AddressOf BtnViewSubmissions_Click
        Me.Controls.Add(btnViewSubmissions)

        ' Create "Create Submission" Button
        Dim btnCreateSubmission As New Button()
        btnCreateSubmission.Text = "CREATE NEW SUBMISSION"
        btnCreateSubmission.Size = New Size(300, 50)
        btnCreateSubmission.Location = New Point(75, 120)
        btnCreateSubmission.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnCreateSubmission.BackColor = Color.FromArgb(46, 204, 113) ' Emerald Green
        btnCreateSubmission.ForeColor = Color.White
        btnCreateSubmission.FlatStyle = FlatStyle.Flat
        btnCreateSubmission.FlatAppearance.BorderSize = 0
        btnCreateSubmission.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96) ' Darker green on hover
        AddHandler btnCreateSubmission.Click, AddressOf BtnCreateSubmission_Click
        Me.Controls.Add(btnCreateSubmission)

        ' Assign keyboard shortcuts using a context menu strip (modern approach)
        Dim contextMenu As New ContextMenuStrip()
        Dim viewSubmissionsMenuItem As New ToolStripMenuItem("View Submissions (CTRL + V)", Nothing, AddressOf BtnViewSubmissions_Click)
        Dim createSubmissionMenuItem As New ToolStripMenuItem("Create Submission (CTRL + N)", Nothing, AddressOf BtnCreateSubmission_Click)
        contextMenu.Items.Add(viewSubmissionsMenuItem)
        contextMenu.Items.Add(createSubmissionMenuItem)
        Me.ContextMenuStrip = contextMenu

        ' Register keyboard shortcuts
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf MainForm_KeyDown
    End Sub

    Private Sub BtnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.Show()
    End Sub

    Private Sub BtnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createSubmissionForm As New CreateSubmissionForm()
        createSubmissionForm.Show()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.V Then
            BtnViewSubmissions_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            BtnCreateSubmission_Click(sender, e)
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'MainForm
        '
        Me.ClientSize = New System.Drawing.Size(450, 300)
        Me.Name = "MainForm"
        Me.ResumeLayout(False)

    End Sub
End Class
