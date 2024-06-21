Imports System.Net.Http
Imports System.Text.Json
Imports System.Diagnostics
Imports System.Threading.Tasks

Public Class CreateSubmissionForm
    Inherits Form

    Private TxtName As TextBox
    Private TxtEmail As TextBox
    Private TxtPhone As TextBox
    Private TxtGithub As TextBox
    Private BtnSubmit As Button
    Private BtnStartStopStopwatch As Button
    Private stopwatch As Stopwatch

    Public Sub New()
        Me.Text = "Reena Prajapat, Slidely Task 2 - Create Submission"
        Me.Size = New Size(500, 300)
        Me.BackColor = Color.LightGreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Padding = New Padding(10)

        ' Initialize controls
        TxtName = New TextBox() With {.Location = New Point(20, 20), .Size = New Size(350, 20), .PlaceholderText = "Name"}
        TxtEmail = New TextBox() With {.Location = New Point(20, 60), .Size = New Size(350, 20), .PlaceholderText = "Email"}
        TxtPhone = New TextBox() With {.Location = New Point(20, 100), .Size = New Size(350, 20), .PlaceholderText = "Phone"}
        TxtGithub = New TextBox() With {.Location = New Point(20, 140), .Size = New Size(350, 20), .PlaceholderText = "GitHub"}
        BtnSubmit = New Button() With {.Text = "Submit", .Location = New Point(20, 180), .Size = New Size(200, 40), .BackColor = Color.SkyBlue, .ForeColor = Color.White}
        BtnStartStopStopwatch = New Button() With {.Text = "Toggle Stopwatch(CTRL + T)", .Location = New Point(200, 180), .Size = New Size(300, 40), .BackColor = Color.Yellow}

        ' Add controls to form
        Me.Controls.Add(TxtName)
        Me.Controls.Add(TxtEmail)
        Me.Controls.Add(TxtPhone)
        Me.Controls.Add(TxtGithub)
        Me.Controls.Add(BtnSubmit)
        Me.Controls.Add(BtnStartStopStopwatch)

        ' Initialize stopwatch
        stopwatch = New Stopwatch()

        ' Add event handlers
        AddHandler BtnSubmit.Click, AddressOf BtnSubmit_Click
        AddHandler BtnStartStopStopwatch.Click, AddressOf BtnStartStopStopwatch_Click
    End Sub

    Private Sub BtnStartStopStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            BtnStartStopStopwatch.Text = "Resume Stopwatch"
        Else
            stopwatch.Start()
            BtnStartStopStopwatch.Text = "Pause Stopwatch"

        End If

    End Sub

    Private Async Sub BtnSubmit_Click(sender As Object, e As EventArgs)
        Dim submission As New Dictionary(Of String, String) From {
            {"name", TxtName.Text},
            {"email", TxtEmail.Text},
            {"phone", TxtPhone.Text},
            {"github_link", TxtGithub.Text},
            {"stopwatch_time", stopwatch.Elapsed.ToString()}
        }

        Dim json As String = JsonSerializer.Serialize(submission)
        Using httpClient As New HttpClient()
            Dim content As New StringContent(json, System.Text.Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await httpClient.PostAsync("http://localhost:3000/submit", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!")
            Else
                MessageBox.Show("Error: " & response.ReasonPhrase)
            End If
        End Using
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then
            BtnSubmit.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.T) Then
            BtnStartStopStopwatch.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function



End Class


