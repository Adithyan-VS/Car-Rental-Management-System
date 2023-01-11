Public Class Adminlogin


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim log = New Login()
        log.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        System.Windows.Forms.Application.Exit()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If PasswordTb.Text = "" Then
            MessageBox.Show(" Enter Password ")
        ElseIf PasswordTb.Text = "12345678" Then
            Dim emp = New Employees()
            emp.Show()
            Me.Hide()
        Else
            MessageBox.Show(" Incorrect Password ")
        End If
    End Sub


End Class