Imports MySql.Data.MySqlClient

Public Class Returncar

    Dim Con = New MySqlConnection("server=localhost;user id=root;database=carrentaldb;allowuservariables=True;")

    Private Sub filldgv()

        Con.Open()
        Dim sql = "select * from `renttbl`"
        Dim cmd = New MySqlCommand(sql, Con)
        Dim adapter As MySqlDataAdapter
        adapter = New MySqlDataAdapter(cmd)
        Dim builder As MySqlCommandBuilder
        builder = New MySqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        RentalsDgv.DataSource = ds.Tables(0)
        Con.close()
    End Sub

    Private Sub filldgvreturn()

        Con.Open()
        Dim sql = "select * from `returntbl`"
        Dim cmd = New MySqlCommand(sql, Con)
        Dim adapter As MySqlDataAdapter
        adapter = New MySqlDataAdapter(cmd)
        Dim builder As MySqlCommandBuilder
        builder = New MySqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ReturnDgv.DataSource = ds.Tables(0)
        Con.close()
    End Sub

    Dim Delay = 0
    Private Sub CalculateDelay()
        'calculate the Delay 
        'Dim diff As System.TimeSpan = Convert.ToDateTime(ReturnDate.Text).Subtract(DateTime.Today.Date)
        Dim diff As System.TimeSpan = DateTime.Today.Date.Subtract(Convert.ToDateTime(ReturnDate.Text))
        'messagebox(diff.totaldays)
        Dim days = diff.TotalDays
        If days < 0 Then
            days = 0
            DelayTb.Text = "No Delay "
        Else
            DelayTb.Text = days

        End If
        Dim Fine = days * 500
        '500 is the penalty per day

        'FineTb.Text = fine
    End Sub
    Private Sub Delete()
        Try
            Con.Open()
            Dim Query As String
            Query = "DELETE FROM carrentaldb.renttbl where Rid = '" & key & "'"
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(Query, Con)
            cmd.ExecuteNonQuery()

            'MessageBox.Show(" Car Succesfully Deleted ")
            Con.Close()
            'clear()
            filldgv()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CalculateFine()
        'calculate the fine Due to delay

        If DelayTb.Text = "No Delay " Then
            FineTb.Text = "No Fine "
        Else
            Dim Days = Convert.ToInt32(DelayTb.Text)
            Dim Fine = Days * 500
            FineTb.Text = "Rs " + Convert.ToString(Fine)
        End If
        'FineTb.Text = fine
    End Sub

    Private Sub Returncar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        filldgv()
        filldgvreturn()
    End Sub

    Dim MyReturn As DateTime
    Dim key = 0

    Private Sub RentalsDgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles RentalsDgv.CellMouseClick
        Dim row As DataGridViewRow = RentalsDgv.Rows(e.RowIndex)
        RegNumTb.Text = row.Cells(1).Value.ToString
        CustName.Text = row.Cells(3).Value.ToString
        ReturnDate.Text = row.Cells(5).Value.ToString
        MyReturn = row.Cells(5).Value
        If RegNumTb.Text = "" Then
            key = 0
        Else
            key = row.Cells(0).Value.ToString
        End If
    End Sub

    Private Sub ReturnDate_TextChanged(sender As Object, e As EventArgs) Handles ReturnDate.TextChanged
        CalculateDelay()
    End Sub

    Private Sub DelayTb_TextChanged(sender As Object, e As EventArgs) Handles DelayTb.TextChanged
        CalculateFine()
    End Sub

    Private Sub clear()
        RegNumTb.Text = ""
        CustName.Text = ""
        ReturnDate.Text = ""
        DelayTb.Text = ""
        FineTb.Text = ""
        key = 0
    End Sub
    Private Sub UpdateCar()
        Dim Status = "YES"
        Try
            Con.Open()
            Dim Query As String
            Dim cmd As New MySqlCommand
            Dim reader As MySqlDataReader
            Query = "update carrentaldb.cartab set Available = '" & Status & "' where Regno='" & RegNumTb.Text & "'"
            cmd = New MySqlCommand(Query, Con)
            reader = cmd.ExecuteReader

            Con.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CustName.Text = "" Or DelayTb.Text = "" Then
            MessageBox.Show("           Select The Car To Return          ")
        Else
            Dim Delay, Fine
            If DelayTb.Text = "No Delay" Then
                Delay = 0
            Else
                Delay = DelayTb.Text
            End If
            If FineTb.Text = "No Fine" Then
                Fine = 0
            Else
                Fine = FineTb.Text
            End If
            Try
                Con.Open()
                Dim Query As String
                Query = "insert into `returntbl` (`CarReg`,`CustName`,`ReturnDate`,`Delay`,`Fine`)values('" & RegNumTb.Text & "','" & CustName.Text & "','" & MyReturn & "','" & Delay & "','" & Fine & "')"
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(Query, Con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("     Car Succesfully Returned         ")
                Con.Close()
                UpdateCar()
                Delete()
                filldgvreturn()
                clear()
            Catch ex As Exception
                Con.close()
            End Try
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim cr = New cars
        cr.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim obj = New customers
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim obj = New Rent
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub LogOut_Click(sender As Object, e As EventArgs) Handles LogOut.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub
End Class