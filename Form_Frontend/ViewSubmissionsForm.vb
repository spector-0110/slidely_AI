Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Inherits Form

    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0
    Private client As New HttpClient()

    ' Controls
    Private txtId As TextBox
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhoneNum As TextBox
    Private txtGithubLink As TextBox
    Private txtStopwatchTime As TextBox
    Private txtHiddenId As TextBox ' Hidden field

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Vatsa Aditya, Slidely Task 2 - View Submissions"
        Me.ClientSize = New Size(500, 450)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 245, 245) ' Light grey background

        InitializeControls()

        ' Fetch submissions from backend
        Await FetchSubmissionsAsync()

        ' Display first submission
        DisplaySubmission()
    End Sub

    Private Sub InitializeControls()
        ' Create Controls
        Dim lblId As New Label() With {.Text = "ID", .Location = New Point(50, 30), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblId)

        txtId = New TextBox() With {.Location = New Point(200, 30), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtId)

        Dim lblName As New Label() With {.Text = "Name", .Location = New Point(50, 80), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblName)

        txtName = New TextBox() With {.Location = New Point(200, 80), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtName)

        Dim lblEmail As New Label() With {.Text = "Email", .Location = New Point(50, 130), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblEmail)

        txtEmail = New TextBox() With {.Location = New Point(200, 130), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtEmail)

        Dim lblPhoneNum As New Label() With {.Text = "Phone Number", .Location = New Point(50, 180), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblPhoneNum)

        txtPhoneNum = New TextBox() With {.Location = New Point(200, 180), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtPhoneNum)

        Dim lblGithubLink As New Label() With {.Text = "Github Link", .Location = New Point(50, 230), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblGithubLink)

        txtGithubLink = New TextBox() With {.Location = New Point(200, 230), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtGithubLink)

        Dim lblStopwatchTime As New Label() With {.Text = "Stopwatch Time", .Location = New Point(50, 280), .Font = New Font("Segoe UI", 12, FontStyle.Bold)}
        Me.Controls.Add(lblStopwatchTime)

        txtStopwatchTime = New TextBox() With {.Location = New Point(200, 280), .ReadOnly = True, .BackColor = Color.White, .BorderStyle = BorderStyle.FixedSingle, .Width = 250, .Font = New Font("Segoe UI", 12)}
        Me.Controls.Add(txtStopwatchTime)

        ' Hidden field for storing the submission Id
        txtHiddenId = New TextBox() With {.Visible = False}
        Me.Controls.Add(txtHiddenId)

        ' Navigation Buttons
        Dim btnPrevious As New Button() With {.Text = "PREVIOUS (CTRL + P)", .Location = New Point(50, 330), .Size = New Size(150, 30), .BackColor = Color.LightGray, .FlatStyle = FlatStyle.Flat}
        AddHandler btnPrevious.Click, AddressOf BtnPrevious_Click
        Me.Controls.Add(btnPrevious)

        Dim btnNext As New Button() With {.Text = "NEXT (CTRL + N)", .Location = New Point(250, 330), .Size = New Size(150, 30), .BackColor = Color.LightGray, .FlatStyle = FlatStyle.Flat}
        AddHandler btnNext.Click, AddressOf BtnNext_Click
        Me.Controls.Add(btnNext)

        ' Delete Button
        Dim btnDelete As New Button() With {.Text = "DELETE", .Location = New Point(150, 380), .Size = New Size(200, 40), .BackColor = Color.FromArgb(231, 76, 60), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        Me.Controls.Add(btnDelete)

        ' Assign keyboard shortcuts
        Dim previousSubmissions As New MenuItem("Previous", New EventHandler(AddressOf BtnPrevious_Click)) With {.Shortcut = Shortcut.CtrlP}
        Dim nextSubmissions As New MenuItem("Next", New EventHandler(AddressOf BtnNext_Click)) With {.Shortcut = Shortcut.CtrlN}
        Dim menu As New MainMenu(New MenuItem() {previousSubmissions, nextSubmissions})
        Me.Menu = menu
    End Sub

    Private Async Function FetchSubmissionsAsync() As Task
        Try
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/api/forms/read")
            response.EnsureSuccessStatusCode()
            Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(jsonResponse)
        Catch ex As Exception
            MessageBox.Show("Error fetching data: " & ex.Message)
            submissions = New List(Of Submission)()
        End Try
    End Function

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            txtId.Text = submission.Cuid
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhoneNum.Text = submission.Phone
            txtGithubLink.Text = submission.Github
            txtStopwatchTime.Text = submission.StopwatchTime
            txtHiddenId.Text = submission.Cuid ' Set the hidden field
        Else
            ' Clear the fields if no submissions are available
            txtId.Text = String.Empty
            txtName.Text = String.Empty
            txtEmail.Text = String.Empty
            txtPhoneNum.Text = String.Empty
            txtGithubLink.Text = String.Empty
            txtStopwatchTime.Text = String.Empty
            txtHiddenId.Text = String.Empty
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

    Private Async Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If submissions IsNot Nothing AndAlso submissions.Count > 0 AndAlso currentIndex < submissions.Count Then
            Dim submissionIdToDelete As String = submissions(currentIndex).Cuid   ' Get the submission Id from the hidden field

            ' Display the submission ID before deletion
            MessageBox.Show($"Deleting submission with ID: {submissionIdToDelete}")

            Try
                Dim response As HttpResponseMessage = Await client.DeleteAsync($"http://localhost:3000/api/forms/bycuid/{submissionIdToDelete}")
                response.EnsureSuccessStatusCode() ' Ensure HTTP success status

                ' If successful, show success message
                MessageBox.Show("Submission deleted successfully!")

                ' Remove the deleted submission from the list
                submissions.RemoveAt(currentIndex)

                ' Adjust currentIndex after deletion
                If currentIndex >= submissions.Count Then
                    currentIndex = Math.Max(0, submissions.Count - 1)
                End If

                ' Display the next or previous submission
                DisplaySubmission()
            Catch ex As HttpRequestException
                MessageBox.Show($"HTTP request error: {ex.Message}") ' Show specific HTTP request error
            Catch ex As Exception
                MessageBox.Show($"Error deleting submission: {ex.Message}") ' Show generic error message
            End Try
        Else
            MessageBox.Show("No submissions to delete or invalid current index.")
        End If
    End Sub


    Public Class Submission
        Public Property Cuid As String
        Public Property Name As String
        Public Property Email As String
        Public Property Phone As String
        Public Property Github As String
        Public Property StopwatchTime As String ' Assuming StopwatchTime is a TimeSpan
    End Class
End Class