Imports MySql.Data.MySqlClient
Public Class customers

    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=localhost;user id=root;database=carrentaldb"
        Dim READER As MySqlDataReader
        If CustNameTb.Text = "" Or CustAddressTb.Text = "" Or CustPhoneTb.Text = "" Or AadhaarTb.Text = "" Then
            MessageBox.Show("Details cannot be Empty")
        Else
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "insert into `customertbl`(`CustName`,`CustAdd`,`CustPhone`,`AadhaarNum`)values('" & CustNameTb.Text & "','" & CustAddressTb.Text & "','" & CustPhoneTb.Text & "','" & AadhaarTb.Text & "')"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader

                MessageBox.Show(" Customer Added Succesfully ")
                MysqlConn.Close()
                Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        filldgv()

    End Sub

    Private Sub filldgv()
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=localhost;user id=root;database=carrentaldb;allowuservariables=True"
        Dim SDA As New MySqlDataAdapter
        Dim dbDataset As New DataTable
        Dim bSource As New BindingSource
        Try
            MysqlConn.Open()
            Dim query As String
            query = "SELECT * FROM `customertbl`"
            COMMAND = New MySqlCommand(query, MysqlConn)
            SDA.SelectCommand = COMMAND
            SDA.Fill(dbDataset)
            bSource.DataSource = dbDataset
            CustomerDgv.DataSource = bSource
            SDA.Update(dbDataset)

            MysqlConn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub Clear()
        CustNameTb.Text = ""
        CustAddressTb.Text = ""
        CustPhoneTb.Text = ""
        AadhaarTb.Text = ""
    End Sub

    Private Sub customers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgv()


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
    End Sub
    Dim key = 0
    Private Sub CustomerDgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles CustomerDgv.CellMouseClick
        Dim row As DataGridViewRow = CustomerDgv.Rows(e.RowIndex)
        CustNameTb.Text = row.Cells(1).Value.ToString
        CustAddressTb.Text = row.Cells(2).Value.ToString
        CustPhoneTb.Text = row.Cells(3).Value.ToString
        AadhaarTb.Text = row.Cells(4).Value.ToString

        If CustNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Or CustNameTb.Text = "" Or CustAddressTb.Text = "" Or CustPhoneTb.Text = "" Or AadhaarTb.Text = "" Then
            MessageBox.Show("Customer not Selected")

        Else
            Dim READER As MySqlDataReader
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "DELETE FROM carrentaldb.customertbl where CustId = '" & key & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show(" Customer Deleted Successfully ")
                MysqlConn.Close()
                Clear()
                filldgv()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                MysqlConn.Close()
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CustNameTb.Text = "" Or CustAddressTb.Text = "" Or CustPhoneTb.Text = "" Or AadhaarTb.Text = "" Then
            MessageBox.Show("Customer Not Selected")

        Else
            Dim READER As MySqlDataReader
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "update carrentaldb.customertbl set CustName = '" & CustNameTb.Text & "',CustAdd='" & CustAddressTb.Text & "',CustPhone='" & CustPhoneTb.Text & "',AadhaarNum='" & AadhaarTb.Text & "' where CustId=" & key & ""
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show(" Customer Edited Successfully ")
                MysqlConn.Close()
                Clear()
                filldgv()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                MysqlConn.Close()
            End Try
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim obj = New cars
        obj.Show()
        Me.Hide()
        Dim cr = New cars

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim obj = New Rent
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

    Private Sub CustPhoneTb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CustPhoneTb.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = Chr(Keys.Delete) And Not e.KeyChar = Chr(Keys.Back) And Not e.KeyChar = Chr(Keys.Space) Then
            e.Handled = True
        End If
    End Sub

    Private Sub AadhaarTb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles AadhaarTb.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = Chr(Keys.Delete) And Not e.KeyChar = Chr(Keys.Back) And Not e.KeyChar = Chr(Keys.Space) Then
            e.Handled = True
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