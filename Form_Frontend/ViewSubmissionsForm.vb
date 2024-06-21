Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Inherits Form

    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0
    Private client As New HttpClient()

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - View Submissions"
        Me.ClientSize = New Size(400, 400)

        ' Create Controls
        Dim lblName As New Label() With {.Text = "Name", .Location = New Point(10, 10), .Font = New Font("Arial", 10, FontStyle.Bold)}
        Me.Controls.Add(lblName)

        Dim txtName As New TextBox() With {.Location = New Point(150, 10), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 220}
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label() With {.Text = "Email", .Location = New Point(10, 50), .Font = New Font("Arial", 10, FontStyle.Bold)}
        Me.Controls.Add(lblEmail)

        Dim txtEmail As New TextBox() With {.Location = New Point(150, 50), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 220}
        Me.Controls.Add(txtEmail)

        Dim lblPhoneNum As New Label() With {.Text = "Phone Number", .Location = New Point(10, 90), .Font = New Font("Arial", 10, FontStyle.Bold)}
        Me.Controls.Add(lblPhoneNum)

        Dim txtPhoneNum As New TextBox() With {.Location = New Point(150, 90), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 220}
        Me.Controls.Add(txtPhoneNum)

        Dim lblGithubLink As New Label() With {.Text = "Github Link", .Location = New Point(10, 130), .Font = New Font("Arial", 10, FontStyle.Bold)}
        Me.Controls.Add(lblGithubLink)

        Dim txtGithubLink As New TextBox() With {.Location = New Point(150, 130), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 220}
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatchTime As New Label() With {.Text = "Stopwatch Time", .Location = New Point(10, 170), .Font = New Font("Arial", 10, FontStyle.Bold)}
        Me.Controls.Add(lblStopwatchTime)

        Dim txtStopwatchTime As New TextBox() With {.Location = New Point(150, 170), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 220}
        Me.Controls.Add(txtStopwatchTime)

        ' Navigation Buttons
        Dim btnPrevious As New Button() With {.Text = "PREVIOUS (CTRL + P)", .Location = New Point(10, 220), .Size = New Size(150, 30), .BackColor = Color.LightGray, .FlatStyle = FlatStyle.Flat}
        AddHandler btnPrevious.Click, AddressOf BtnPrevious_Click
        Me.Controls.Add(btnPrevious)

        Dim btnNext As New Button() With {.Text = "NEXT (CTRL + N)", .Location = New Point(180, 220), .Size = New Size(150, 30), .BackColor = Color.LightGray, .FlatStyle = FlatStyle.Flat}
        AddHandler btnNext.Click, AddressOf BtnNext_Click
        Me.Controls.Add(btnNext)

        ' Assign keyboard shortcuts
        Dim previousSubmissions As New MenuItem("Previous", New EventHandler(AddressOf BtnPrevious_Click))
        previousSubmissions.Shortcut = Shortcut.CtrlP
        Dim nextSubmissions As New MenuItem("Next", New EventHandler(AddressOf BtnNext_Click))
        nextSubmissions.Shortcut = Shortcut.CtrlN
        Dim menu As New MainMenu(New MenuItem() {previousSubmissions, nextSubmissions})
        Me.Menu = menu

        ' Fetch submissions from backend
        Await FetchSubmissionsAsync()

        ' Display first submission
        DisplaySubmission()
    End Sub

    Private Async Function FetchSubmissionsAsync() As Task
        Try
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/api/forms")
            response.EnsureSuccessStatusCode()
            Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(jsonResponse)
        Catch ex As Exception
            MessageBox.Show("Error fetching data: " & ex.Message)
            submissions = New List(Of Submission)()
        End Try
    End Function

    Private Sub DisplaySubmission()
        If submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            For Each ctrl As Control In Me.Controls
                If TypeOf ctrl Is TextBox Then
                    Select Case CType(ctrl, TextBox).Location.Y
                        Case 10
                            CType(ctrl, TextBox).Text = submission.Name
                        Case 50
                            CType(ctrl, TextBox).Text = submission.Email
                        Case 90
                            CType(ctrl, TextBox).Text = submission.Phone
                        Case 130
                            CType(ctrl, TextBox).Text = submission.Github
                        Case 170
                            CType(ctrl, TextBox).Text = submission.StopwatchTime
                    End Select
                End If
            Next
        End If
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ViewSubmissionsForm
        '
        Me.ClientSize = New System.Drawing.Size(400, 400)
        Me.Name = "ViewSubmissionsForm"
        Me.ResumeLayout(False)

    End Sub

    Private Sub ViewSubmissionsForm_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property Github As String
    Public Property StopwatchTime As String
End Class
