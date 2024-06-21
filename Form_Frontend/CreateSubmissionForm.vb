Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Diagnostics
Imports System.Linq

Public Class CreateSubmissionForm
    Inherits Form

    Private stopwatch As New Stopwatch()
    Private client As New HttpClient()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - Create Submission"
        Me.ClientSize = New Size(500, 400)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Set the form background color
        Me.BackColor = Color.FromArgb(245, 245, 245) ' Light grey background

        ' Create and add controls with modern design
        Dim lblName As New Label() With {
            .Text = "Name",
            .Location = New Point(50, 30),
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(lblName)

        Dim txtName As New TextBox() With {
            .Location = New Point(250, 30),
            .Width = 200,
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label() With {
            .Text = "Email",
            .Location = New Point(50, 80),
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(lblEmail)

        Dim txtEmail As New TextBox() With {
            .Location = New Point(250, 80),
            .Width = 200,
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(txtEmail)

        Dim lblPhoneNum As New Label() With {
            .Text = "Phone Number",
            .Location = New Point(50, 130),
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(lblPhoneNum)

        Dim txtPhoneNum As New TextBox() With {
            .Location = New Point(250, 130),
            .Width = 200,
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(txtPhoneNum)

        Dim lblGithubLink As New Label() With {
            .Text = "Github Link for Task 2",
            .Location = New Point(50, 180),
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(lblGithubLink)

        Dim txtGithubLink As New TextBox() With {
            .Location = New Point(250, 180),
            .Width = 200,
            .Font = New Font("Segoe UI", 12, FontStyle.Regular)
        }
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatchTime As New Label() With {
            .Text = "Stopwatch Time: 00:00:00",
            .Location = New Point(250, 230),
            .Font = New Font("Segoe UI", 12, FontStyle.Regular),
            .AutoSize = True
        }
        Me.Controls.Add(lblStopwatchTime)

        Dim btnToggleStopwatch As New Button() With {
            .Text = "TOGGLE STOPWATCH",
            .Location = New Point(50, 230),
            .Size = New Size(180, 40),
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .BackColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        btnToggleStopwatch.FlatAppearance.BorderSize = 0
        btnToggleStopwatch.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185)
        AddHandler btnToggleStopwatch.Click, AddressOf BtnToggleStopwatch_Click
        Me.Controls.Add(btnToggleStopwatch)

        Dim btnSubmit As New Button() With {
            .Text = "SUBMIT",
            .Location = New Point(50, 290),
            .Size = New Size(400, 50),
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .BackColor = Color.FromArgb(46, 204, 113),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        btnSubmit.FlatAppearance.BorderSize = 0
        btnSubmit.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96)
        AddHandler btnSubmit.Click, AddressOf BtnSubmit_Click
        Me.Controls.Add(btnSubmit)

        ' Assign keyboard shortcuts
        Dim toggleStopwatch As New MenuItem("Toggle Stopwatch", New EventHandler(AddressOf BtnToggleStopwatch_Click)) With {.Shortcut = Shortcut.CtrlT}
        Dim submit As New MenuItem("Submit", New EventHandler(AddressOf BtnSubmit_Click)) With {.Shortcut = Shortcut.CtrlS}
        Dim menu As New MainMenu(New MenuItem() {toggleStopwatch, submit})
        Me.Menu = menu

        ' Timer for Stopwatch
        Dim timer As New Timer() With {.Interval = 1000}
        AddHandler timer.Tick, Sub()
                                   lblStopwatchTime.Text = "Stopwatch Time: " & String.Format("{0:hh\:mm\:ss}", stopwatch.Elapsed)
                               End Sub
        timer.Start()
    End Sub

    Private Sub BtnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
    End Sub

    Private Async Sub BtnSubmit_Click(sender As Object, e As EventArgs)
        ' Collect data and submit
        Dim txtName As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 30).First()
        Dim txtEmail As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 80).First()
        Dim txtPhoneNum As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 130).First()
        Dim txtGithubLink As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 180).First()

        Dim submission As New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhoneNum.Text,
            .github = txtGithubLink.Text,
            .stopwatchTime = stopwatch.Elapsed.TotalSeconds ' Send the elapsed time in seconds
        }

        ' Convert the submission to JSON
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        ' Send the POST request
        Try
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/api/forms/submit", content)
            response.EnsureSuccessStatusCode()
            MessageBox.Show("Submission saved successfully!")
        Catch ex As Exception
            MessageBox.Show("Error submitting data: " & ex.Message)
        End Try

        Me.Close()
    End Sub
End Class
