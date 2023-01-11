Imports MySql.Data.MySqlClient
Public Class Rent

    Dim Con = New MySqlConnection("server=localhost;user id=root;database=carrentaldb;allowuservariables=True;")
    Private Sub fillcustomer()
        Con.open()
        Dim sql = "select * from `customertbl`"
        Dim cmd As New MySqlCommand(sql, Con)
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        CustIdCb.DataSource = Tbl
        CustIdCb.DisplayMember = "CustId"
        CustIdCb.ValueMember = "CustId"
        Con.close()
    End Sub
    Private Sub fillRegistration()
        Dim Status = "YES"
        Con.open()
        Dim sql = "SELECT * FROM `cartab` where Available='" & Status & "'"
        Dim cmd As New MySqlCommand(sql, Con)
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        RegNumCb.DataSource = Tbl
        RegNumCb.DisplayMember = "Regno"
        RegNumCb.ValueMember = "Regno"
        Con.close()
    End Sub
    Private Sub UpdateCar()
        Dim Status = "NO"
        Try
            Con.Open()
            Dim Query As String
            Dim cmd As New MySqlCommand
            Dim reader As MySqlDataReader
            Query = "update carrentaldb.cartab set Available = '" & Status & "' where Regno='" & RegNumCb.SelectedValue.ToString() & "'"
            cmd = New MySqlCommand(Query, Con)
            reader = cmd.ExecuteReader

            Con.Close()

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub GetCustName()
        Con.open()
        Dim sql = "select * from customertbl where CustId= " & CustIdCb.SelectedValue.ToString() & ""
        Dim cmd As New MySqlCommand(sql, Con)
        Dim dt As New DataTable
        Dim reader As MySqlDataReader
        reader = cmd.ExecuteReader
        While reader.Read
            CustnameTb.Text = reader(1).ToString()
        End While
        Con.close()

    End Sub

    Dim cost = 0
    Private Sub GetCarRate()
        Con.open()
        Dim sql = "select * from cartab where Regno= '" & RegNumCb.SelectedValue.ToString() & "'"
        Dim cmd As New MySqlCommand(sql, Con)
        Dim dt As New DataTable
        Dim reader As MySqlDataReader
        reader = cmd.ExecuteReader
        While reader.Read
            cost = Convert.ToInt32(reader(4).ToString())
        End While
        Con.close()

    End Sub
    Private Sub clear()
        CustnameTb.Text = ""
        FeesTb.Text = ""
        RegNumCb.SelectedIndex = -1
        CustIdCb.SelectedIndex = -1

    End Sub
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
        RentDgv.DataSource = ds.Tables(0)
        Con.close()

    End Sub
    Private Sub CalculateFees()
        'calculate the Number of days of the car will be rented
        Dim diff As System.TimeSpan = ReturnDate.Value.Date.Subtract(RentDate.Value.Date)
        'messagebox(diff.totaldays)
        Dim days = diff.TotalDays
        Dim fees = days * cost
        FeesTb.Text = fees
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CustnameTb.Text = "" Or RegNumCb.SelectedIndex = -1 Or FeesTb.Text = "" Then
            MessageBox.Show("           Missing Data          ")
        Else
            Try

                Con.Open()
                Dim Query As String
                Query = "insert into `renttbl`(`CarReg`,`CustId`,`CustName`,`RentDate`,`ReturnDate`,`Fees`)values('" & RegNumCb.SelectedValue.ToString() & "','" & CustIdCb.SelectedValue.ToString() & "','" & CustnameTb.Text & "','" & RentDate.Text & "','" & ReturnDate.Text & "','" & FeesTb.Text & "')"
                Dim cmd As MySqlCommand
                cmd = New MySqlCommand(Query, Con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("      Car Succesfully Rented         ")
                Con.Close()
                UpdateCar()
                clear()
                filldgv()
                fillRegistration()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Con.close()
            End Try
        End If

    End Sub
    Private Sub CustIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CustIdCb.SelectionChangeCommitted
        GetCustName()
    End Sub

    Private Sub Rent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        fillcustomer()
        fillRegistration()
        filldgv()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clear()
    End Sub

    Private Sub ReturnDate_ValueChanged(sender As Object, e As EventArgs) Handles ReturnDate.ValueChanged
        CalculateFees()
    End Sub

    Private Sub RegNumCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles RegNumCb.SelectionChangeCommitted
        GetCarRate()
    End Sub

    Private Sub RegNumCb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RegNumCb.SelectedIndexChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim obj = New cars
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim obj = New customers
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim obj = New Returncar
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PrintPreviewDialog1.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.RentDgv.Width, Me.RentDgv.Height)
        RentDgv.DrawToBitmap(bm, New Rectangle(0, 0, Me.RentDgv.Width, Me.RentDgv.Height))
        e.Graphics.DrawString(" Cars on Rent ", New Font("Arial", 25, FontStyle.Underline), Brushes.BlueViolet, 300, 40)
        e.Graphics.DrawImage(bm, 5, 100)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

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