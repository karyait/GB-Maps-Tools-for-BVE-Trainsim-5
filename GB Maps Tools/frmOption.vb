Imports System.IO
Imports System.Text
Imports System.Xml

Public Class frmOption

    Private Sub ButtonBVEfile_Click(sender As Object, e As EventArgs) Handles ButtonBVEfile.Click
        If Main.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxBVEdir.Text = Main.FolderBrowserDialog1.SelectedPath
            Main.bvedir = Main.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ButtonGBImg_Click(sender As Object, e As EventArgs) Handles ButtonGBImg.Click
        If Main.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxGBImg.Text = Main.FolderBrowserDialog1.SelectedPath
            Main.gbIdir = Main.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If Directory.Exists(TextBoxBVEdir.Text) = False Then
            ErrorProvider1.SetError(TextBoxBVEdir, "Sorry! directory not exists.")
            Exit Sub
        Else
            If ErrorProvider1.GetError(TextBoxBVEdir) IsNot Nothing Then
                ErrorProvider1.SetError(TextBoxBVEdir, vbNullString)
            End If
        End If
        If Directory.Exists(TextBoxGBImg.Text) = False Then
            ErrorProvider1.SetError(TextBoxGBImg, "Sorry! directory not exists.")
            Exit Sub
        Else
            If ErrorProvider1.GetError(TextBoxGBImg) IsNot Nothing Then
                ErrorProvider1.SetError(TextBoxGBImg, vbNullString)
            End If
        End If
        If File.Exists("5config.xml") = False Then
            Try
                Dim xCfile As XDocument =
                    <?xml version="1.0" encoding="utf-8"?>
                    <dir>
                        <bve><%= TextBoxBVEdir.Text %></bve>
                        <gbimg><%= TextBoxGBImg.Text %></gbimg>
                    </dir>
                xCfile.Save("5config.xml")
                Main.TSTBBVEOdir.Text = TextBoxBVEdir.Text
                Main.TBSTBGBIdir.Text = TextBoxGBImg.Text
                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Else
            Try
                Dim xCfile As XDocument = XDocument.Load("5config.xml")
                xCfile.<dir>.<bve>.Value = TextBoxBVEdir.Text
                xCfile.<dir>.<gbimg>.Value = TextBoxGBImg.Text
                xCfile.Save("5config.xml")
                Main.TSTBBVEOdir.Text = TextBoxBVEdir.Text
                Main.TBSTBGBIdir.Text = TextBoxGBImg.Text
                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub ButtonKO_Click(sender As Object, e As EventArgs) Handles ButtonKO.Click
        Close()
    End Sub

    Private Sub frmOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists("5config.xml") = True Then
            Try
                Dim xCfile As XDocument = XDocument.Load("5config.xml")
                TextBoxBVEdir.Text = xCfile.<dir>.<bve>.Value
                TextBoxGBImg.Text = xCfile.<dir>.<gbimg>.Value
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
End Class