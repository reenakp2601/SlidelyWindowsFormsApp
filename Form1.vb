Public Class Form1

    Inherits Form

    Private BtnViewSubmissions As Button
    Private BtnCreateNewSubmission As Button

    Public Sub New()
        Me.Text = "Reena Prajapat, Slidely Task 2 - Slidely Form App"
        Me.Size = New Size(500, 300)
        Me.BackColor = Color.LightGreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.Padding = New Padding(10)

        BtnViewSubmissions = New Button() With {
            .Text = "View Submissions(CTRL + V)",
            .Location = New Point(50, 50),
            .Size = New Size(400, 40),
            .Font = New Font("Arial", 12, FontStyle.Regular),
            .BackColor = Color.Yellow
        }
        AddHandler BtnViewSubmissions.Click, AddressOf BtnViewSubmissions_Click

        BtnCreateNewSubmission = New Button() With {
            .Text = "Create New Submission(CTRL + N",
            .Location = New Point(50, 120),
            .Size = New Size(400, 40),
            .Font = New Font("Arial", 12, FontStyle.Regular),
            .BackColor = Color.SkyBlue,
            .ForeColor = Color.White
        }
        AddHandler BtnCreateNewSubmission.Click, AddressOf BtnCreateNewSubmission_Click

        Me.Controls.Add(BtnViewSubmissions)
        Me.Controls.Add(BtnCreateNewSubmission)
    End Sub

    Private Sub BtnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub BtnCreateNewSubmission_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.V) Then
            BtnViewSubmissions.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            BtnCreateNewSubmission.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class
