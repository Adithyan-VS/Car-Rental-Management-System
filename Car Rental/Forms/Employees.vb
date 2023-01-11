Imports MySql.Data.MySqlClient
Public Class Employees
    Dim Con = New MySqlConnection("server=localhost;user id=root;database=carrentaldb;allowuservariables=True;")
    Private Sub filldgv()
        Con.open()
        Dim sql = "select * from employeetbl "
        Dim cmd = New MySqlCommand(sql, Con)
        Dim adapter As New MySqlDataAdapter
        adapter = New MySqlDataAdapter(cmd)
        Dim builder As MySqlCommandBuilder
        builder = New MySqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        EmployeeDgv.DataSource = ds.Tables(0)
        Con.close()
    End Sub
    Private Sub clear()
        EmpName.Text = ""
        EmpPassTb.Text = ""
        key = 0

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If EmpName.Text = "" Or EmpPassTb.Text = "" Then
            MessageBox.Show("Missing Data")
        Else
            Try
                Con.open
                Dim query = "insert into employeetbl (`EmpName`,`EmpPass`)values('" & EmpName.Text & "','" & EmpPassTb.Text & "')"
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Employee Successfully Saved")
                Con.close()
                clear()
                filldgv()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Employees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgv()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clear()
    End Sub
    Dim key = 0
    Private Sub EmployeeDgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles EmployeeDgv.CellMouseClick
        Dim row As DataGridViewRow = EmployeeDgv.Rows(e.RowIndex)
        EmpName.Text = row.Cells(1).Value.ToString
        EmpPassTb.Text = row.Cells(2).Value.ToString


        If EmpName.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Then
            MessageBox.Show("Select the Employee")

        Else

            Try
                Con.Open()
                Dim Query = "DELETE FROM carrentaldb.employeetbl where EmpId = '" & key & "'"
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(Query, Con)
                cmd.ExecuteNonQuery()
                MessageBox.Show(" Employee Deleted Successfully ")
                Con.Close()
                clear()
                filldgv()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If EmpName.Text = "" Or EmpPassTb.Text = "" Then
            MessageBox.Show("   Missing Information    ")

        Else

            Try
                Con.Open()
                Dim Query = "update carrentaldb.employeetbl set EmpName = '" & EmpName.Text & "',EmpPass='" & EmpPassTb.Text & "' where EmpId =" & key & ""
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(Query, Con)
                cmd.ExecuteNonQuery()
                MessageBox.Show(" Employee Successfully Updated  ")
                Con.Close()
                clear()
                filldgv()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim log = New AdminCarView
        log.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub
End Class