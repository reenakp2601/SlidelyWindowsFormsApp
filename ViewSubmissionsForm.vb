Imports System.Net.Http
Imports System.Text.Json
Imports System.Threading.Tasks

Public Class ViewSubmissionsForm
    Inherits Form

    Private TxtName As TextBox
    Private TxtEmail As TextBox
    Private TxtPhone As TextBox
    Private TxtGithub As TextBox
    Private TxtStopwatchTime As TextBox
    Private BtnPrevious As Button
    Private BtnNext As Button
    Private currentIndex As Integer = 0

    Public Sub New()
        Me.Text = "Reena Prajapat, Slidely Task 2 - View Submissions"
        Me.Size = New Size(500, 400)
        Me.BackColor = Color.LightGreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Padding = New Padding(10)

        ' Initialize controls
        TxtName = New TextBox() With {.Location = New Point(20, 20), .Size = New Size(350, 20), .ReadOnly = True, .PlaceholderText = "Name"}
        TxtEmail = New TextBox() With {.Location = New Point(20, 60), .Size = New Size(350, 20), .ReadOnly = True, .PlaceholderText = "Email"}
        TxtPhone = New TextBox() With {.Location = New Point(20, 100), .Size = New Size(350, 20), .ReadOnly = True, .PlaceholderText = "Phone"}
        TxtGithub = New TextBox() With {.Location = New Point(20, 140), .Size = New Size(350, 20), .ReadOnly = True, .PlaceholderText = "GitHub"}
        TxtStopwatchTime = New TextBox() With {.Location = New Point(20, 180), .Size = New Size(350, 20), .ReadOnly = True, .PlaceholderText = "Stopwatch Time"}
        BtnPrevious = New Button() With {.Text = "Previous(CTRL +P)", .Location = New Point(20, 220), .Size = New Size(200, 40), .BackColor = Color.Yellow}
        BtnNext = New Button() With {.Text = "Next(CTRL + N)", .Location = New Point(200, 220), .Size = New Size(200, 40), .BackColor = Color.SkyBlue, .ForeColor = Color.White}

        ' Add controls to form
        Me.Controls.Add(TxtName)
        Me.Controls.Add(TxtEmail)
        Me.Controls.Add(TxtPhone)
        Me.Controls.Add(TxtGithub)
        Me.Controls.Add(TxtStopwatchTime)
        Me.Controls.Add(BtnPrevious)
        Me.Controls.Add(BtnNext)

        ' Add event handlers
        AddHandler BtnPrevious.Click, AddressOf BtnPrevious_Click
        AddHandler BtnNext.Click, AddressOf BtnNext_Click

        ' Load the first submission
        LoadSubmission(currentIndex)
    End Sub

    Private Async Function LoadSubmission(index As Integer) As Task
        Using httpClient As New HttpClient()
            Dim response As HttpResponseMessage = Await httpClient.GetAsync("http://localhost:3000/read?index=" & index)

            If response.IsSuccessStatusCode Then
                Dim submission As String = Await response.Content.ReadAsStringAsync()
                Dim submissionData As Dictionary(Of String, String) = JsonSerializer.Deserialize(Of Dictionary(Of String, String))(submission)

                TxtName.Text = submissionData("name")
                TxtEmail.Text = submissionData("email")
                TxtPhone.Text = submissionData("phone")
                TxtGithub.Text = submissionData("github_link")
                TxtStopwatchTime.Text = submissionData("stopwatch_time")
            Else
                MessageBox.Show("Error: " & response.ReasonPhrase)
            End If
        End Using
    End Function

    Private Async Sub BtnNext_Click(sender As Object, e As EventArgs)
        currentIndex += 1
        Await LoadSubmission(currentIndex)
    End Sub

    Private Async Sub BtnPrevious_Click(sender As Object, e As EventArgs)
        currentIndex -= 1
        If currentIndex < 0 Then currentIndex = 0
        Await LoadSubmission(currentIndex)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.P) Then
            BtnPrevious.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            BtnNext.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class


