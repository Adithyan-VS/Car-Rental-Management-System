Imports MySql.Data.MySqlClient
Public Class Login
    Dim Con = New MySqlConnection("server=localhost;user id=root;database=carrentaldb;allowuservariables=True;")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If UsernameTb.Text = "" Then
            MessageBox.Show("Enter the Username")
        ElseIf PasswordTb.Text = "" Then
            MessageBox.Show("Enter the Password")
        Else
            Con.open()
            Dim query = "Select * from employeetbl where EmpName='" & UsernameTb.Text & "' and EmpPass = '" & PasswordTb.Text & "'"
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(query, Con)
            Dim da As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim ds As DataSet = New DataSet()
            da.Fill(ds)
            Dim a As Integer
            a = ds.tables(0).Rows.Count
            If a = 0 Then
                MessageBox.Show("Wrong Username or Password")
            Else
                Dim cr = New cars

                cr.Show()
                Me.Hide()
            End If

            Con.close()
        End If


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim Adm = New Adminlogin
        Adm.Show()
        Me.Hide()
    End Sub


End Class