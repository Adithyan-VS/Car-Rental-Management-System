Imports MySql.Data.MySqlClient
Public Class cars
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (RegNumTb.Text = "" Or BrandCb.SelectedIndex = -1 Or ModelTb.Text = "" Or PriceTb.Text = "" Or ColorTb.Text = "" Or AvailableCb.SelectedIndex = -1) Then
            MessageBox.Show("Details cannot be Empty")
        Else
            MysqlConn = New MySqlConnection
            MysqlConn.ConnectionString = "server=localhost;user id=root;database=carrentaldb"
            Dim READER As MySqlDataReader

            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "insert into `cartab`(`Regno`,`Brand`,`Model`,`Price`,`Color`,`Available`)values('" & RegNumTb.Text & "','" & BrandCb.SelectedItem & "','" & ModelTb.Text & "','" & PriceTb.Text & "','" & ColorTb.Text & "','" & AvailableCb.SelectedItem & "')"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader

                MessageBox.Show(" Car Saved Succesfully ")
                MysqlConn.Close()
                Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        filldgv()
    End Sub
    Private Sub Clear()
        RegNumTb.Text = ""
        BrandCb.SelectedIndex = -1
        ModelTb.Text = ""
        PriceTb.Text = ""
        ColorTb.Text = ""
        AvailableCb.SelectedIndex = -1
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
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
            query = "SELECT * FROM `cartab`"
            COMMAND = New MySqlCommand(query, MysqlConn)
            SDA.SelectCommand = COMMAND
            SDA.Fill(dbDataset)
            bSource.DataSource = dbDataset
            CarDgv.DataSource = bSource
            SDA.Update(dbDataset)

            MysqlConn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MysqlConn.Close()
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub

    Private Sub cars_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgv()


        Dim log = New Login()
    End Sub

    Private Sub CarDgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles CarDgv.CellMouseClick
        Dim row As DataGridViewRow = CarDgv.Rows(e.RowIndex)
        RegNumTb.Text = row.Cells(1).Value.ToString
        BrandCb.SelectedItem = row.Cells(2).Value.ToString
        ModelTb.Text = row.Cells(3).Value.ToString
        PriceTb.Text = row.Cells(4).Value.ToString
        ColorTb.Text = row.Cells(5).Value.ToString
        AvailableCb.SelectedItem = row.Cells(6).Value.ToString
        If RegNumTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)

        End If
    End Sub
    Dim key = 0
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Or RegNumTb.Text = "" Or BrandCb.SelectedIndex = -1 Or ModelTb.Text = "" Or PriceTb.Text = "" Or ColorTb.Text = "" Or AvailableCb.SelectedIndex = -1 Then
            MessageBox.Show("Car not Selected")

        Else
            Dim READER As MySqlDataReader
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "DELETE FROM carrentaldb.cartab where Cid = '" & key & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show(" Car Succesfully Deleted ")
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
        If RegNumTb.Text = "" Or BrandCb.SelectedIndex = -1 Or ModelTb.Text = "" Or PriceTb.Text = "" Or ColorTb.Text = "" Or AvailableCb.SelectedIndex = -1 Then
            MessageBox.Show("Car Not Selected")

        Else
            Dim READER As MySqlDataReader
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "update carrentaldb.cartab set RegNo = '" & RegNumTb.Text & "',Brand='" & BrandCb.SelectedItem.ToString() & "',model='" & ModelTb.Text & "',Price='" & PriceTb.Text & "',color= '" & ColorTb.Text & "',Available='" & AvailableCb.SelectedItem.ToString.ToString() & "' where Cid=" & key & ""
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show(" Car Succesfully Updated ")
                MysqlConn.Close()
                Clear()
                filldgv()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                MysqlConn.Close()
            End Try
        End If
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Ret = New Returncar
        Ret.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim Obj = New customers
        Obj.Show()
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

    Private Sub PriceTb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTb.KeyPress
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