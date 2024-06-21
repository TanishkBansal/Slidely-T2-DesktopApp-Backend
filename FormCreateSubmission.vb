Imports System.Diagnostics
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class FormCreateSubmission
    Private stopwatch As Stopwatch = New Stopwatch()
    Private isRunning As Boolean = False
    Private timer As Timer
    Private isEdit As Boolean = False
    Private editIndex As Integer = -1

    ' Constructor for creating new submission
    Public Sub New()
        InitializeComponent()
        InitializeForm()
    End Sub

    ' Constructor for editing existing submission
    Public Sub New(submission As Submission, index As Integer)
        InitializeComponent()
        InitializeForm()

        ' Set form fields with the existing submission data
        txtName.Text = submission.Name
        txtEmail.Text = submission.Email
        txtPhoneNumber.Text = submission.PhoneNumber
        txtGitHub.Text = submission.GitHubRepo
        txtStopwatch.Text = submission.StopwatchTime

        isEdit = True
        editIndex = index
    End Sub

    Private Sub InitializeForm()
        Me.KeyPreview = True
        txtStopwatch.Text = "00:00:00"
        timer = New Timer()
        AddHandler timer.Tick, AddressOf UpdateStopwatch
        timer.Interval = 1000 ' Update every second
    End Sub

    Private Sub btnStopwatch_Click(sender As Object, e As EventArgs) Handles btnStopwatch.Click
        If isRunning Then
            stopwatch.Stop()
            timer.Stop()
            btnStopwatch.Text = "Resume"
        Else
            stopwatch.Start()
            timer.Start()
            btnStopwatch.Text = "Pause"
        End If
        isRunning = Not isRunning
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim newSubmission As New Submission(txtName.Text, txtEmail.Text, txtPhoneNumber.Text, txtGitHub.Text, txtStopwatch.Text)
        If isEdit Then
            Await UpdateSubmissionAsync(editIndex, newSubmission)
        Else
            Await SaveSubmissionAsync(newSubmission)
        End If
        MessageBox.Show("Submission Saved!")
        ResetStopwatch()
        Me.Close()
    End Sub

    Private Sub FormCreateSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            btnStopwatch.PerformClick()
        End If
    End Sub

    Private Sub UpdateStopwatch(sender As Object, e As EventArgs)
        txtStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub ResetStopwatch()
        stopwatch.Reset()
        txtStopwatch.Text = "00:00:00"
        btnStopwatch.Text = "Start"
        isRunning = False
        timer.Stop()
    End Sub

    Private Async Function SaveSubmissionAsync(submission As Submission) As Task
        Dim settings As New JsonSerializerSettings With {
            .ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }
        Dim json As String = JsonConvert.SerializeObject(submission, settings)

        Using client As New HttpClient()
            Dim content As New StringContent(json, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:5000/api/submit", content)
            response.EnsureSuccessStatusCode()
        End Using
    End Function

    Private Async Function UpdateSubmissionAsync(index As Integer, submission As Submission) As Task
        Dim jsonData As String = JsonConvert.SerializeObject(submission)
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Accept.Add(New Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            Dim response As HttpResponseMessage = Await client.PutAsync($"http://localhost:3000/api/update/{index}", content)
            response.EnsureSuccessStatusCode()
        End Using
    End Function
End Class

Public Class Me1
    Public Property Name As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property GitHubRepo As String
    Public Property StopwatchTime As String

    Public Sub New(name As String, email As String, phoneNumber As String, gitHubRepo As String, stopwatchTime As String)
        Me.Name = name
        Me.Email = email
        Me.PhoneNumber = phoneNumber
        Me.GitHubRepo = gitHubRepo
        Me.StopwatchTime = stopwatchTime
    End Sub

    ' Update the static method to update a submission
    Public Shared Sub UpdateSubmission(index As Integer, submission As Submission)
        ' Logic to update submission in the backend
        ' This method needs to be implemented if the backend supports update functionality
    End Sub

    ' Save the submission
    Public Shared Sub SaveSubmission(submission As Submission)
        ' This method can be removed or used if saving is required within the application
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Return MyBase.Equals(obj)
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Overrides Function GetHashCode() As Integer
        Return MyBase.GetHashCode()
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
End Class
