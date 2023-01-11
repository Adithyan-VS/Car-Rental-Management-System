Imports MySql.Data.MySqlClient
Public Class AdminCarView
    Dim MysqlConn As MySqlConnection
    Dim COMMAND As MySqlCommand
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
            CarsDgv.DataSource = bSource
            SDA.Update(dbDataset)

            MysqlConn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            MysqlConn.Close()
        Finally
            MysqlConn.Dispose()
        End Try
    End Sub



    Private Sub AdminCarView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgv()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim log = New Employees
        log.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        Dim Ask As MsgBoxResult = MsgBox("Are you sure you want to Log Out?", MsgBoxStyle.OkCancel)
        If Ask = MsgBoxResult.Ok Then
            Dim obj = New Login
            obj.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.CarsDgv.Width, Me.CarsDgv.Height)
        CarsDgv.DrawToBitmap(bm, New Rectangle(0, 0, Me.CarsDgv.Width, Me.CarsDgv.Height))
        e.Graphics.DrawString(" List Of Cars ", New Font("Arial", 25, FontStyle.Underline), Brushes.BlueViolet, 300, 40)
        e.Graphics.DrawImage(bm, 5, 100)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PrintPreviewDialog1.Show()
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