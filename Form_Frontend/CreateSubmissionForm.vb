Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Inherits Form

    Private stopwatch As New Stopwatch()
    Private client As New HttpClient()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - Create Submission"
        Me.ClientSize = New Size(350, 300)

        ' Create Controls
        Dim lblName As New Label() With {.Text = "Name", .Location = New Point(10, 10)}
        Me.Controls.Add(lblName)

        Dim txtName As New TextBox() With {.Location = New Point(150, 10)}
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label() With {.Text = "Email", .Location = New Point(10, 50)}
        Me.Controls.Add(lblEmail)

        Dim txtEmail As New TextBox() With {.Location = New Point(150, 50)}
        Me.Controls.Add(txtEmail)

        Dim lblPhoneNum As New Label() With {.Text = "Phone Num", .Location = New Point(10, 90)}
        Me.Controls.Add(lblPhoneNum)

        Dim txtPhoneNum As New TextBox() With {.Location = New Point(150, 90)}
        Me.Controls.Add(txtPhoneNum)

        Dim lblGithubLink As New Label() With {.Text = "Github Link For Task 2", .Location = New Point(10, 130)}
        Me.Controls.Add(lblGithubLink)

        Dim txtGithubLink As New TextBox() With {.Location = New Point(150, 130), .Size = New Size(180, 20)}
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatchTime As New Label() With {.Text = "", .Location = New Point(150, 170)}
        Me.Controls.Add(lblStopwatchTime)

        Dim btnToggleStopwatch As New Button() With {.Text = "TOGGLE STOPWATCH (CTRL + T)", .Location = New Point(10, 170), .Size = New Size(150, 30)}
        AddHandler btnToggleStopwatch.Click, AddressOf BtnToggleStopwatch_Click
        Me.Controls.Add(btnToggleStopwatch)

        Dim btnSubmit As New Button() With {.Text = "SUBMIT (CTRL + S)", .Location = New Point(10, 220), .Size = New Size(320, 30)}
        AddHandler btnSubmit.Click, AddressOf BtnSubmit_Click
        Me.Controls.Add(btnSubmit)

        ' Assign keyboard shortcuts
        Dim toggleStopwatch As New MenuItem("Toggle Stopwatch", New EventHandler(AddressOf BtnToggleStopwatch_Click))
        toggleStopwatch.Shortcut = Shortcut.CtrlT
        Dim submit As New MenuItem("Submit", New EventHandler(AddressOf BtnSubmit_Click))
        submit.Shortcut = Shortcut.CtrlS
        Dim menu As New MainMenu(New MenuItem() {toggleStopwatch, submit})
        Me.Menu = menu

        ' Timer for Stopwatch
        Dim timer As New Timer()
        timer.Interval = 1000
        AddHandler timer.Tick, Sub()
                                   lblStopwatchTime.Text = String.Format("{0:hh\:mm\:ss}", stopwatch.Elapsed)
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
        Dim txtName As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 10).First()
        Dim txtEmail As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 50).First()
        Dim txtPhoneNum As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 90).First()
        Dim txtGithubLink As TextBox = Me.Controls.OfType(Of TextBox)().Where(Function(txt) txt.Location.Y = 130).First()
        Dim lblStopwatchTime As Label = Me.Controls.OfType(Of Label)().Where(Function(lbl) lbl.Location.Y = 170).First()

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
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/api/forms", content)
            response.EnsureSuccessStatusCode()
            MessageBox.Show("Submission saved successfully!")
        Catch ex As Exception
            MessageBox.Show("Error submitting data: " & ex.Message)
        End Try

        Me.Close()
    End Sub
End Class
