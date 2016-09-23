Imports System
Imports System.IO
Imports System.Threading
Imports System.Text
Imports System.Xml
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Drawing.Image

Public Class Main
    Friend bvedir, gbIdir, currDir As String
    Friend newData As Boolean
    Friend saved As Boolean

    Public Enum filetype
        x = 0
        img = 1
        wav = 2
    End Enum

    Private Sub MainWin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title


        If File.Exists("5config.xml") = True Then
            Try
                Dim xCfile As XDocument = XDocument.Load("5config.xml")
                textBoxBVEdataDir.Text = xCfile.<dir>.<bve>.Value
                textBoxGBimgDir.Text = xCfile.<dir>.<gbimg>.Value
                bvedir = xCfile.<dir>.<bve>.Value
                gbIdir = xCfile.<dir>.<gbimg>.Value
                OpenFileDialog2.InitialDirectory = gbIdir
                OpenFileDialog1.InitialDirectory = bvedir
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        currDir = ""
    End Sub

    Private Sub TextBoxaudiofile_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxaudiofile.TextChanged
        Try
            Dim basedir = bvedir.Substring(0, bvedir.LastIndexOf("\")) 'bvedir.ToLower.Replace("\object", "")
            Dim fullpath As String = basedir & "\" & TextBoxaudiofile.Text
            If File.Exists(fullpath) Then
                PictureBoxaudiorun.Visible = True
                My.Computer.Audio.Stop()
                My.Computer.Audio.Play(fullpath)
            Else
                PictureBoxaudiorun.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Buttonbrowseaudiofile_Click(sender As System.Object, e As System.EventArgs) Handles Buttonbrowseaudiofile.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim filename = "sounds\" & My.Computer.FileSystem.GetFileInfo(OpenFileDialog1.FileName).Name
            TextBoxaudiofile.Text = filename
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Buttonaudioadd_Click(sender As System.Object, e As System.EventArgs) Handles Buttonaudioadd.Click
        If TextBoxaudioname.Text <> "" And TextBoxaudiotitle.Text <> "" And TextBoxaudiofile.Text <> "" And ComboBoxaudiotype.Text <> "" Then
            DataGridViewaudio.Rows.Add(New String() {DataGridViewaudio.RowCount - 1, TextBoxaudioname.Text,
                                                     TextBoxaudiotitle.Text, ComboBoxaudiotype.Text, TextBoxaudiofile.Text})
        End If
    End Sub

    Private Sub ButtonRailTypeAdd_Click(sender As System.Object, e As System.EventArgs) Handles buttonNewRail.Click
        If textBoxRailName.Text <> "" And textBoxRailTitle.Text <> "" And textBoxRailSleeper1.Text <> "" And
            comboBoxRailType.Text <> "" Then
            DataGridViewRailType.Rows.Add(New String() {DataGridViewRailType.RowCount - 1, textBoxRailName.Text,
            textBoxRailTitle.Text, textBoxRailImage.Text, comboBoxRailType.Text, comboBoxRailGauge.Text,
            textBoxRailSleeper1.Text, textBoxRailLeft1.Text, textBoxRailRight1.Text, textBoxRailSleeper2.Text,
            textBoxRailLeft2.Text, textBoxRailRight2.Text, textBoxRailSleeper3.Text, textBoxRailLeft3.Text,
            textBoxRailRight3.Text, textBoxRailSleeper4.Text, textBoxRailLeft4.Text, textBoxRailRight4.Text,
            textBoxRailSleeper5.Text, textBoxRailLeft5.Text, textBoxRailRight5.Text, NumericUpDownRailCycle.Value})
        End If
    End Sub

    Private Sub buttonBrowseRailSleeper1_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRailSleeper1.Click
        UpdateXFileField(textBoxRailSleeper1, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailImage_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRailImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Try
                textBoxRailImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
                PictureBoxRailTypeImg.Image = Nothing
                PictureBoxRailTypeImg.Image = Image.FromFile(OpenFileDialog2.FileName)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Buttonbvestrfile_Click(sender As System.Object, e As System.EventArgs) Handles Buttonbvestrfile.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxBVEstrfile.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Buttonbvestrimg_Click(sender As System.Object, e As System.EventArgs) Handles Buttonbvestrimg.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxBVEstrimg.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxBVEstrimg.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonBVEstrAdd_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBVEstrAdd.Click
        If ComboBoxBVEstrtype.Text <> "" And TextBoxBVEstrname.Text <> "" And TextBoxBVEstrtitle.Text <> "" And TextBoxBVEstrfile.Text <> "" Then
            DataGridViewBVEstr.Rows.Add(New String() {DataGridViewBVEstr.RowCount - 1, TextBoxBVEstrname.Text,
            TextBoxBVEstrtitle.Text, ComboBoxBVEstrtype.Text, TextBoxBVEstrimg.Text, TextBoxBVEstrfile.Text,
            TextBoxbveWallLfile.Text, TextBoxbveWallRfile.Text, NumericUpDownBveX.Value})
        End If
    End Sub

    Private Sub Buttonbvefobjadd_Click(sender As System.Object, e As System.EventArgs) Handles Buttonbvefobjadd.Click
        If ComboBoxbvefobjotype.Text <> "" And TextBoxbvefobjname.Text <> "" And TextBoxbvefobjtitle.Text <> "" And TextBoxbvefobjfile.Text <> "" Then
            DataGridViewBVEfobj.Rows.Add(New String() {DataGridViewBVEfobj.RowCount - 1, TextBoxbvefobjname.Text,
                                         TextBoxbvefobjtitle.Text, ComboBoxbvefobjotype.Text, TextBoxbvefobjimg.Text, TextBoxbvefobjfile.Text})
        End If
    End Sub

    Private Sub ButtonbveFOfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbveFOfile.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxbvefobjfile.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonbveFOimg_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbveFOimg.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxbvefobjimg.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxbvefobjimg.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub DataGridViewaudio_Click(sender As Object, e As System.EventArgs) Handles DataGridViewaudio.Click
        Try
            Dim irow As Integer = DataGridViewaudio.CurrentRow.Index
            Dim basedir = bvedir.Substring(0, bvedir.LastIndexOf("\"))
            Dim fullpath As String = basedir & "\" & DataGridViewaudio.Item(4, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxaudiorun.Visible = True
                My.Computer.Audio.Stop()
                My.Computer.Audio.Play(fullpath)
            Else
                PictureBoxaudiorun.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBVEfobj_Click(sender As Object, e As System.EventArgs) Handles DataGridViewBVEfobj.Click
        Try
            Dim irow As Integer = DataGridViewBVEfobj.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewBVEfobj.Item(4, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxbvefobjimg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxbvefobjimg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBVEstr_Click(sender As Object, e As System.EventArgs) Handles DataGridViewBVEstr.Click
        Try
            Dim irow As Integer = DataGridViewBVEstr.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewBVEstr.Item(4, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxBVEstrimg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxBVEstrimg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewRailType_Click(sender As Object, e As System.EventArgs) Handles DataGridViewRailType.Click
        Try
            Dim irow As Integer = DataGridViewRailType.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewRailType.Item(6, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxRailTypeImg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxRailTypeImg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ButtonRetriveTrainFolder_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRetriveTrainFolder.Click
        Try
            Dim basedir = TextBoxTrainDir.Text
            Dim subdirsList = Directory.EnumerateDirectories(basedir)
            Dim bil = 0
            For Each trainDir In subdirsList
                Dim dirName = trainDir.Substring(trainDir.LastIndexOf("\") + 1)
                Dim imgFile = dirName & "\" & "train.bmp"
                If File.Exists(basedir & "\" & dirName & "\train.dat") Then
                    DataGridViewTrainDir.Rows.Add(New String() {bil, dirName, dirName, imgFile, dirName})
                    bil += 1
                End If
            Next
        Catch eIo As IOException
            MessageBox.Show(eIo.Message)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ButtonBrowseTrainDir_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseTrainDir.Click
        FolderBrowserDialog1.Description = "Please select BVE train folder ..."
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTrainDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ButtonTunnelBrowseImage_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseTunnelImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxTunnelPicture.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonAddTunnel_Click(sender As System.Object, e As System.EventArgs) Handles buttonAddNewTunnel.Click
        If textBoxTunnelName.Text <> "" And textBoxTunnelTitle.Text <> "" And textBoxTunnelEntrance.Text <> "" Then
            DataGridViewTunnel.Rows.Add(New String() {DataGridViewTunnel.RowCount - 1, textBoxTunnelName.Text,
                textBoxTunnelTitle.Text, textBoxTunnelImage.Text,
                textBoxTunnelEntrance.Text, textBoxTunnelExitStructure.Text,
                textBoxTunnelWallLeft.Text, textBoxTunnelWallRight.Text,
                NumericUpDownTunnelWallCycle.Value})
        End If
    End Sub

    Private Sub TextBoxTunnelInMid1Left_Click(sender As System.Object, e As System.EventArgs) Handles textBoxTunnelWallLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelWallLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub


    Private Sub TextBoxTunnelInMid1Right_Click(sender As System.Object, e As System.EventArgs) Handles textBoxTunnelWallRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelWallRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxTunnelEntrance_Click(sender As System.Object, e As System.EventArgs) Handles textBoxTunnelEntrance.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelEntrance.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxTunnelExit_Click(sender As System.Object, e As System.EventArgs) Handles textBoxTunnelExitStructure.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelExitStructure.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonBrowseBridgeImageFile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseBridgeImageFile.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxBridgeImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxBridge.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonFOBrowseImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseOverPassImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxOverPassImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxFO.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonBrowseCutImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseHillCutImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxHillCutImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxHillCut.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonDikeBrowseImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseDikeImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxDikeImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxDike.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonBrowseRCImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRCImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxRCImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxRC.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub ButtonBrowsePformImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePlatformImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxPlatform.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub TextBoxBridgeFileLeft_Click(sender As System.Object, e As System.EventArgs) Handles TextBoxBridgeLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxBridgeLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxBridgeFileRight_Click(sender As System.Object, e As System.EventArgs) Handles TextBoxBridgeRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxBridgeRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxBridgePier_Click(sender As System.Object, e As System.EventArgs) Handles textBoxBridgePier.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxBridgePier.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonNewBridge_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewBridge.Click
        If TextBoxBridgeName.Text <> "" And TextBoxBridgeTitle.Text <> "" Then
            DataGridViewBridge.Rows.Add(New String() {DataGridViewBridge.RowCount - 1, TextBoxBridgeName.Text,
                TextBoxBridgeTitle.Text, TextBoxBridgeImage.Text,
                TextBoxBridgeLeft.Text, TextBoxBridgeRight.Text, NumericUpDownBridgeWallCycle.Value,
                textBoxBridgePier.Text, NumericUpDownBridgePierCycle.Value})
        End If
    End Sub

    Private Sub TextBoxFOWallLeft_Click(sender As System.Object, e As System.EventArgs) Handles textBoxOverPassWallLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxOverPassWallLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxFOWallRight_Click(sender As System.Object, e As System.EventArgs) Handles textBoxOverPassWallRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxOverPassWallRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxFObeamunder_Click(sender As System.Object, e As System.EventArgs) Handles textBoxOverPassPier.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxOverPassPier.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonNewOverpass_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewOverpass.Click
        If textBoxOverPassName.Text <> "" And textBoxOverPassTitle.Text <> "" Then
            DataGridViewFlyOver.Rows.Add(New String() {DataGridViewFlyOver.RowCount - 1,
                textBoxOverPassName.Text, textBoxOverPassTitle.Text, textBoxOverPassImage.Text,
                textBoxOverPassWallLeft.Text, textBoxOverPassWallRight.Text, NumericUpDownOverpassWallCycle.Value,
                textBoxOverPassPier.Text, NumericUpDownOverpassPierCycle.Value})
        End If
    End Sub

    Private Sub TextBoxCutL5m_Click(sender As System.Object, e As System.EventArgs) Handles textBoxHillCutLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxHillCutLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxCutR5m_Click(sender As System.Object, e As System.EventArgs) Handles textBoxHillCutRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxHillCutRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonAddCut_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewHillCut.Click
        If textBoxHillCutName.Text <> "" And textBoxHillCutTitle.Text <> "" Then
            DataGridViewCut.Rows.Add(New String() {DataGridViewCut.RowCount - 1,
                textBoxHillCutName.Text, textBoxHillCutTitle.Text, textBoxHillCutImage.Text,
                textBoxHillCutLeft.Text, textBoxHillCutRight.Text, NumericUpDownHillCutCycle.Value})
        End If
    End Sub

    Private Sub TextBoxDikeLeft_Click(sender As System.Object, e As System.EventArgs) Handles textBoxDikeLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxDikeLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxDikeRight_Click(sender As System.Object, e As System.EventArgs) Handles textBoxDikeRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxDikeRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonNewDike_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewDike.Click
        If textBoxDikeName.Text <> "" And textBoxDikeTitle.Text <> "" Then
            DataGridViewDike.Rows.Add(New String() {DataGridViewDike.RowCount - 1,
            textBoxDikeName.Text, textBoxDikeTitle.Text, textBoxDikeImage.Text,
            textBoxDikeLeft.Text, textBoxDikeRight.Text, NumericUpDownDikeCycle.Value})
        End If
    End Sub

    Private Sub TextBoxRCLeft_Click(sender As System.Object, e As System.EventArgs) Handles textBoxRCgateLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxRCgateLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxRCcross_Click(sender As System.Object, e As System.EventArgs) Handles textBoxRCIntersection.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxRCIntersection.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxRCRight_Click(sender As System.Object, e As System.EventArgs) Handles textBoxRCgateRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxRCgateRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonAddRC_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewRC.Click
        If textBoxRCName.Text <> "" And textBoxRCTitle.Text <> "" Then
            DataGridViewRC.Rows.Add(New String() {DataGridViewRC.RowCount - 1,
                textBoxRCName.Text, textBoxRCTitle.Text, textBoxRCImage.Text,
                textBoxRCgateLeft.Text, textBoxRCIntersection.Text, textBoxRCgateRight.Text,
                textBoxRCSound.Text})
        End If
    End Sub

    Private Sub TextBoxPformFormL_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformFormCL_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformMiddleLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformMiddleLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformFormCR_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformMiddleRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformMiddleRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformFormR_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformRoofL_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformRoofLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformRoofLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformRoofCL_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformRoofMiddleLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformRoofMiddleLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformRoofCR_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformRoofMiddleRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformRoofMiddleRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxPformRoofR_Click(sender As System.Object, e As System.EventArgs) Handles textBoxPlatformRoofRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPlatformRoofRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonAddPlatform_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewPlatform.Click
        If textBoxPlatformName.Text <> "" And textBoxPlatformTitle.Text <> "" Then
            DataGridViewPlatform.Rows.Add(New String() {DataGridViewPlatform.RowCount - 1,
             textBoxPlatformName.Text, textBoxPlatformTitle.Text, textBoxPlatformImage.Text,
              textBoxPlatformLeft.Text, textBoxPlatformMiddleLeft.Text,
              textBoxPlatformMiddleRight.Text, textBoxPlatformRight.Text,
              NumericUpDownPlatformCycle.Value,
              textBoxPlatformRoofLeft.Text, textBoxPlatformRoofMiddleLeft.Text,
              textBoxPlatformRoofMiddleRight.Text, textBoxPlatformRoofRight.Text,
              NumericUpDownPlatformRoofCycle.Value})
        End If
    End Sub



    Private Sub DataGridViewTrainDir_Click(sender As Object, e As System.EventArgs) Handles DataGridViewTrainDir.Click
        Try
            Dim irow As Integer = DataGridViewTrainDir.CurrentRow.Index
            Dim basedir = bvedir.Substring(0, bvedir.LastIndexOf("\")) & "\trains" 'bvedir.Replace("Railway\Object", "") & "train"

            Dim fullpath As String = basedir.ToLower & "\" & DataGridViewTrainDir.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxTrainDir.Image = Image.FromFile(fullpath)
            Else
                PictureBoxTrainDir.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewTunnel_Click(sender As Object, e As System.EventArgs) Handles DataGridViewTunnel.Click
        Try
            Dim irow As Integer = DataGridViewTunnel.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewTunnel.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxTunnelPicture.Image = Image.FromFile(fullpath)
            Else
                PictureBoxTunnelPicture.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBridge_Click(sender As Object, e As System.EventArgs) Handles DataGridViewBridge.Click
        Try
            Dim irow As Integer = DataGridViewBridge.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewBridge.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxBridge.Image = Image.FromFile(fullpath)
            Else
                PictureBoxBridge.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewFlyOver_Click(sender As Object, e As System.EventArgs) Handles DataGridViewFlyOver.Click
        Try
            Dim irow As Integer = DataGridViewFlyOver.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewFlyOver.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxFO.Image = Image.FromFile(fullpath)
            Else
                PictureBoxFO.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewCut_Click(sender As Object, e As System.EventArgs) Handles DataGridViewCut.Click
        Try
            Dim irow As Integer = DataGridViewCut.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewCut.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxHillCut.Image = Image.FromFile(fullpath)
            Else
                PictureBoxHillCut.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewDike_Click(sender As Object, e As System.EventArgs) Handles DataGridViewDike.Click
        Try
            Dim irow As Integer = DataGridViewDike.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewDike.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxDike.Image = Image.FromFile(fullpath)
            Else
                PictureBoxDike.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewRC_Click(sender As Object, e As System.EventArgs) Handles DataGridViewRC.Click
        Try
            Dim irow As Integer = DataGridViewRC.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewRC.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxRC.Image = Image.FromFile(fullpath)
            Else
                PictureBoxRC.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewPlatform_Click(sender As Object, e As System.EventArgs) Handles DataGridViewPlatform.Click
        Try
            Dim irow As Integer = DataGridViewPlatform.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewPlatform.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxPlatform.Image = Image.FromFile(fullpath)
            Else
                PictureBoxPlatform.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonNewPole_Click(sender As System.Object, e As System.EventArgs) Handles buttonNewPole.Click
        If textBoxPoleName.Text <> "" And textBoxPoleTitle.Text <> "" Then
            DataGridViewPole.Rows.Add(New String() {DataGridViewPole.RowCount - 1, textBoxPoleName.Text,
                textBoxPoleTitle.Text, textBoxPoleImage.Text, textBoxPoleStructureLeft.Text,
                textBoxPoleStructureRight.Text, textBoxOverHeadWire.Text, NumericUpDownPoleCycle.Value})
        End If
    End Sub

    Private Sub ButtonBrowsePoleCSV_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePoleStructureLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPoleStructureLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonBrowsePoleImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePoleImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxPoleImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxPole.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub DataGridViewPole_Click(sender As Object, e As System.EventArgs) Handles DataGridViewPole.Click
        Try
            Dim irow As Integer = DataGridViewPole.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewPole.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxPole.Image = Image.FromFile(fullpath)
            Else
                PictureBoxPole.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonNewCrack_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewCrack.Click
        If textBoxCrackName.Text <> "" And textBoxCrackTitle.Text <> "" Then
            DataGridViewCrack.Rows.Add(New String() {DataGridViewCrack.RowCount - 1,
                textBoxCrackName.Text, textBoxCrackTitle.Text, textBoxCrackImage.Text,
                textBoxCrackLeft.Text, textBoxCrackRight.Text, NumericUpDownCrackCycle.Value})
        End If
    End Sub

    Private Sub ButtonBrowseCrackImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseCrackImage.Click
        OpenFileDialog2.InitialDirectory = gbIdir
        If OpenFileDialog2.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxCrackImage.Text = OpenFileDialog2.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")
            PictureBoxCrack.Image = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub TextBoxCrackLeftcsv_Click(sender As System.Object, e As System.EventArgs) Handles textBoxCrackLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxCrackLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub TextBoxCrackRightcsv_Click(sender As System.Object, e As System.EventArgs) Handles textBoxCrackRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxCrackRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub DataGridViewCrack_Click(sender As Object, e As System.EventArgs) Handles DataGridViewCrack.Click
        Try
            Dim irow As Integer = DataGridViewCrack.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewCrack.Item(3, irow).Value
            If File.Exists(fullpath) Then
                PictureBoxCrack.Image = Image.FromFile(fullpath)
            Else
                PictureBoxCrack.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ComboBoxBVEstrtype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxBVEstrtype.SelectedIndexChanged
        If ComboBoxBVEstrtype.SelectedItem = "Wall" Then
            GroupBoxbvestrwall.Enabled = True
        Else
            GroupBoxbvestrwall.Enabled = False
        End If
    End Sub

    Private Sub ButtonbveWallLfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbveWallLfile.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxbveWallLfile.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonbveWallRfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbveWallRfile.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            TextBoxbveWallRfile.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub buttonBrowseTunnelWallLeft_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelWallLeft.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelWallLeft.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub buttonBrowseTunnelWallRight_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelWallRight.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelWallRight.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub buttonBrowseTunnelEntrance_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelEntrance.Click
        OpenFileDialog1.InitialDirectory = currDir
        If OpenFileDialog1.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            textBoxTunnelEntrance.Text = OpenFileDialog1.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
            currDir = My.Computer.FileSystem.GetParentPath(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub buttonBrowseTunnelExitStructure_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelExitStructure.Click
        UpdateXFileField(textBoxTunnelExitStructure, filetype.x, Nothing)
    End Sub

    Private Sub btnBgL_Click(sender As Object, e As EventArgs) Handles buttonBrowseBridgeLeft.Click
        UpdateXFileField(TextBoxBridgeLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnBgR_Click(sender As Object, e As EventArgs) Handles buttonBrowseBridgeRight.Click
        UpdateXFileField(TextBoxBridgeRight, filetype.x, Nothing)
    End Sub

    Private Sub btnBgPr_Click(sender As Object, e As EventArgs) Handles buttonBrowseBridgePier.Click
        UpdateXFileField(textBoxBridgePier, filetype.x, Nothing)
    End Sub

    Private Sub btnFOL_Click(sender As Object, e As EventArgs) Handles buttonBrowseOverPassWallLeft.Click
        UpdateXFileField(textBoxOverPassWallLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnFOR_Click(sender As Object, e As EventArgs) Handles buttonBrowseOverPassWallRight.Click
        UpdateXFileField(textBoxOverPassWallRight, filetype.x, Nothing)
    End Sub

    Private Sub btnFOB_Click(sender As Object, e As EventArgs) Handles buttonBrowseOverPassPier.Click
        UpdateXFileField(textBoxOverPassPier, filetype.x, Nothing)
    End Sub

    Private Sub btnHCL_Click(sender As Object, e As EventArgs) Handles buttonBrowseHillCutLeft.Click
        UpdateXFileField(textBoxHillCutLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnHCR_Click(sender As Object, e As EventArgs) Handles buttonBrowseHillCutRight.Click
        UpdateXFileField(textBoxHillCutRight, filetype.x, Nothing)
    End Sub

    Private Sub btnDL_Click(sender As Object, e As EventArgs) Handles buttonBrowseDikeLeft.Click
        UpdateXFileField(textBoxDikeLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnDR_Click(sender As Object, e As EventArgs) Handles buttonBrowseDikeRight.Click
        UpdateXFileField(textBoxDikeRight, filetype.x, Nothing)
    End Sub

    Private Sub btnRCTL_Click(sender As Object, e As EventArgs) Handles buttonBrowseRCgateLeft.Click
        UpdateXFileField(textBoxRCgateLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnRCC_Click(sender As Object, e As EventArgs) Handles buttonBrowseRCIntersection.Click
        UpdateXFileField(textBoxRCIntersection, filetype.x, Nothing)
    End Sub

    Private Sub btnRCTR_Click(sender As Object, e As EventArgs) Handles buttonBrowseRCgateRight.Click
        UpdateXFileField(textBoxRCgateRight, filetype.x, Nothing)
    End Sub

    Private Sub btnRCDS_Click(sender As Object, e As EventArgs) Handles buttonBrowseRCSound.Click
        UpdateXFileField(textBoxRCSound, filetype.wav, Nothing)
    End Sub


    Private Sub btnpFL_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformLeft.Click
        UpdateXFileField(textBoxPlatformLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnpFLC_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformMiddleLeft.Click
        UpdateXFileField(textBoxPlatformMiddleLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnpFRC_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformMiddleRight.Click
        UpdateXFileField(textBoxPlatformMiddleRight, filetype.x, Nothing)
    End Sub

    Private Sub btnpFR_Click(sender As Object, e As EventArgs) Handles buttonbrowsePlatformRight.Click
        UpdateXFileField(textBoxPlatformRight, filetype.x, Nothing)
    End Sub

    Private Sub btnpRL_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformRoofLeft.Click
        UpdateXFileField(textBoxPlatformRoofLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnpRLC_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformRoofMiddleLeft.Click
        UpdateXFileField(textBoxPlatformRoofMiddleLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnpRRC_Click(sender As Object, e As EventArgs) Handles buttonBrowsePlatformRoofMiddleRight.Click
        UpdateXFileField(textBoxPlatformRoofMiddleRight, filetype.x, Nothing)
    End Sub

    Private Sub btnpRR_Click(sender As Object, e As EventArgs) Handles buttonBrowsermRoofRight.Click
        UpdateXFileField(textBoxPlatformRoofRight, filetype.x, Nothing)
    End Sub

    Private Sub btnCrL_Click(sender As Object, e As EventArgs) Handles buttonBrowseCrackLeft.Click
        UpdateXFileField(textBoxCrackLeft, filetype.x, Nothing)
    End Sub

    Private Sub btnCrR_Click(sender As Object, e As EventArgs) Handles buttonBrowseCrackRight.Click
        UpdateXFileField(textBoxCrackRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailSleeper2_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailSleeper2.Click
        UpdateXFileField(textBoxRailSleeper2, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailSleeper3_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailSleeper3.Click
        UpdateXFileField(textBoxRailSleeper3, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailSleeper4_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailSleeper4.Click
        UpdateXFileField(textBoxRailSleeper4, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailSleeper5_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailSleeper5.Click
        UpdateXFileField(textBoxRailSleeper5, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailLeft2_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft2.Click
        UpdateXFileField(textBoxRailLeft2, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailLeft3_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft3.Click
        UpdateXFileField(textBoxRailLeft3, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailLeft4_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft4.Click
        UpdateXFileField(textBoxRailLeft4, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailLeft5_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft5.Click
        UpdateXFileField(textBoxRailLeft5, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailRight2_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight2.Click
        UpdateXFileField(textBoxRailRight2, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailRight3_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight3.Click
        UpdateXFileField(textBoxRailRight3, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailRight4_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight4.Click
        UpdateXFileField(textBoxRailRight4, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailRight5_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight5.Click
        UpdateXFileField(textBoxRailRight5, filetype.x, Nothing)
    End Sub

    Private Sub ButtonBVEDir_Click(sender As Object, e As EventArgs) Handles buttonBrowseBVEDataDir.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            textBoxBVEdataDir.Text = FolderBrowserDialog1.SelectedPath
            bvedir = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub buttonOpenXML_Click(sender As Object, e As EventArgs) Handles buttonOpenXML.Click
        Dim basedir As String
        If gbIdir = "" Then
            MessageBox.Show("Please set image dir first")

        Else
            basedir = gbIdir.ToLower.Replace("\images", "")
            If basedir <> "" Then OpenFileDialog3.InitialDirectory = basedir
        End If
        If OpenFileDialog3.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim filename As String = OpenFileDialog3.FileName
            Dim teks As String = My.Computer.FileSystem.ReadAllText(filename)
            Dim arrRow As String() = teks.Split(vbCrLf)
            For Each drow As String In arrRow
                Dim dd As String() = drow.Split(",")
                Select Case dd(0).Trim()
                    Case "rail"
                        'DataGridViewRailType
                        Dim dty As String() = dd(4).Split("_")
                        DataGridViewRailType.Rows.Add(New String() {dd(1), dd(2), dd(3), dty(0), dty(1), dd(6), dd(5), dd(7), dd(8), dd(9), dd(10), dd(11), dd(12), dd(13), dd(14), dd(15)})
                    Case "bvestr"
                        'DataGridViewBVEstr
                        DataGridViewBVEstr.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                    Case "fobj"
                        'DataGridViewBVEfobj
                        DataGridViewBVEfobj.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6)})
                    Case "wav"
                        'DataGridViewaudio
                        DataGridViewaudio.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5)})

                        '//***************
                    Case "traindir"
                        DataGridViewTrainDir.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5)})
                    Case "tunnel"
                        DataGridViewTunnel.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10), dd(11), dd(12), dd(13), dd(14), dd(15), dd(16)})
                    Case "bridge"
                        DataGridViewBridge.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10), dd(11)})
                    Case "fo"
                        DataGridViewFlyOver.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                    Case "cut"
                        DataGridViewCut.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                    Case "dike"
                        DataGridViewDike.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                    Case "rc"
                        DataGridViewRC.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8)})
                    Case "pform"
                        DataGridViewPlatform.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10), dd(11), dd(12), dd(13)})
                    Case "poles"
                        DataGridViewPole.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6)})
                    Case "cracks"
                        DataGridViewCrack.Rows.Add(New String() {dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                    Case Else

                End Select
            Next
        End If
    End Sub

    Private Sub buttonGBImageDir_Click(sender As Object, e As EventArgs) Handles buttonGBImageDir.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            textBoxGBimgDir.Text = FolderBrowserDialog1.SelectedPath
            gbIdir = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub buttonNewXML_Click(sender As Object, e As EventArgs) Handles buttonNewXML.Click
        DataGridViewaudio.Rows.Clear()
        DataGridViewBVEfobj.Rows.Clear()
        DataGridViewBVEstr.Rows.Clear()
        DataGridViewRailType.Rows.Clear()
        'DataGridViewGBMstr.Rows.Clear()
        DataGridViewCrack.Rows.Clear()
        DataGridViewPole.Rows.Clear()
        DataGridViewTrainDir.Rows.Clear()
        DataGridViewTunnel.Rows.Clear()
        DataGridViewBridge.Rows.Clear()
        DataGridViewFlyOver.Rows.Clear()
        DataGridViewCut.Rows.Clear()
        DataGridViewDike.Rows.Clear()
        DataGridViewRC.Rows.Clear()
        DataGridViewPlatform.Rows.Clear()
    End Sub

    Private Sub ButtonRailL_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft1.Click
        UpdateXFileField(textBoxRailLeft1, filetype.x, Nothing)
    End Sub

    Private Sub ButtonRailR_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight1.Click
        UpdateXFileField(textBoxRailRight1, filetype.x, Nothing)
    End Sub

    Private Sub ButtonSaveXML_Click(sender As Object, e As EventArgs) Handles ButtonSaveXML.Click
        Dim basedir = gbIdir.ToLower.Replace("\images", "")
        If SaveFileDialog1.InitialDirectory = "" Then SaveFileDialog1.InitialDirectory = basedir & "\data"
        SaveFileDialog1.Filter = "GB Maps Data|*.txt|All files|*.*"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim txt As New StringBuilder
            Dim filename As String = SaveFileDialog1.FileName

            For ro = 0 To DataGridViewRailType.RowCount - 1
                If DataGridViewRailType.Item(0, ro).Value <> "" And DataGridViewRailType.Item(1, ro).Value <> "" And DataGridViewRailType.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "rail," & DataGridViewRailType.Item(0, ro).Value & "," & DataGridViewRailType.Item(1, ro).Value & "," &
                        DataGridViewRailType.Item(2, ro).Value & "," & DataGridViewRailType.Item(3, ro).Value & "_" & DataGridViewRailType.Item(4, ro).Value & "," &
                        DataGridViewRailType.Item(6, ro).Value & "," & DataGridViewRailType.Item(5, ro).Value & "," & DataGridViewRailType.Item(7, ro).Value & "," &
                        DataGridViewRailType.Item(8, ro).Value & "," & DataGridViewRailType.Item(9, ro).Value & "," & DataGridViewRailType.Item(10, ro).Value & "," &
                        DataGridViewRailType.Item(11, ro).Value & "," & DataGridViewRailType.Item(12, ro).Value & "," & DataGridViewRailType.Item(13, ro).Value & "," &
                        DataGridViewRailType.Item(14, ro).Value & "," & DataGridViewRailType.Item(15, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewBVEstr.RowCount - 1
                If DataGridViewBVEstr.Item(0, ro).Value <> "" And DataGridViewBVEstr.Item(1, ro).Value <> "" And DataGridViewBVEstr.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "bvestr," & DataGridViewBVEstr.Item(0, ro).Value & "," & DataGridViewBVEstr.Item(1, ro).Value & "," &
                        DataGridViewBVEstr.Item(2, ro).Value & "," & DataGridViewBVEstr.Item(3, ro).Value & "," &
                        DataGridViewBVEstr.Item(4, ro).Value & "," & DataGridViewBVEstr.Item(5, ro).Value & "," &
                        DataGridViewBVEstr.Item(6, ro).Value & "," & DataGridViewBVEstr.Item(7, ro).Value & "," &
                        DataGridViewBVEstr.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewBVEfobj.RowCount - 1
                If DataGridViewBVEfobj.Item(0, ro).Value <> "" And DataGridViewBVEfobj.Item(1, ro).Value <> "" And DataGridViewBVEfobj.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "fobj," & DataGridViewBVEfobj.Item(0, ro).Value & "," & DataGridViewBVEfobj.Item(1, ro).Value & "," &
                        DataGridViewBVEfobj.Item(2, ro).Value & "," & DataGridViewBVEfobj.Item(3, ro).Value & "," &
                        DataGridViewBVEfobj.Item(4, ro).Value & "," & DataGridViewBVEfobj.Item(5, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next


            For ro = 0 To DataGridViewaudio.RowCount - 1
                If DataGridViewaudio.Item(0, ro).Value <> "" And DataGridViewaudio.Item(1, ro).Value <> "" And DataGridViewaudio.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "wav," & DataGridViewaudio.Item(0, ro).Value & "," & DataGridViewaudio.Item(1, ro).Value & "," &
                        DataGridViewaudio.Item(2, ro).Value & "," & DataGridViewaudio.Item(3, ro).Value & "," &
                        DataGridViewaudio.Item(4, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            ' // *******************************

            For ro = 0 To DataGridViewTrainDir.RowCount - 1
                If DataGridViewTrainDir.Item(0, ro).Value <> "" And DataGridViewTrainDir.Item(1, ro).Value <> "" And DataGridViewTrainDir.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "traindir," & DataGridViewTrainDir.Item(0, ro).Value & "," & DataGridViewTrainDir.Item(1, ro).Value & "," &
                        DataGridViewTrainDir.Item(2, ro).Value & "," & DataGridViewTrainDir.Item(3, ro).Value & "," &
                        DataGridViewTrainDir.Item(4, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewTunnel.RowCount - 1
                If DataGridViewTunnel.Item(0, ro).Value <> "" And DataGridViewTunnel.Item(1, ro).Value <> "" And DataGridViewTunnel.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "tunnel," & DataGridViewTunnel.Item(0, ro).Value & "," & DataGridViewTunnel.Item(1, ro).Value & "," &
                        DataGridViewTunnel.Item(2, ro).Value & "," & DataGridViewTunnel.Item(3, ro).Value & "," &
                        DataGridViewTunnel.Item(4, ro).Value & "," & DataGridViewTunnel.Item(5, ro).Value & "," &
                        DataGridViewTunnel.Item(6, ro).Value & "," & DataGridViewTunnel.Item(7, ro).Value & "," &
                        DataGridViewTunnel.Item(8, ro).Value & "," & DataGridViewTunnel.Item(9, ro).Value & "," &
                        DataGridViewTunnel.Item(10, ro).Value & "," & DataGridViewTunnel.Item(11, ro).Value & "," &
                        DataGridViewTunnel.Item(12, ro).Value & "," & DataGridViewTunnel.Item(13, ro).Value & "," &
                        DataGridViewTunnel.Item(14, ro).Value & "," & DataGridViewTunnel.Item(15, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewBridge.RowCount - 1
                If DataGridViewBridge.Item(0, ro).Value <> "" And DataGridViewBridge.Item(1, ro).Value <> "" And DataGridViewBridge.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "bridge," & DataGridViewBridge.Item(0, ro).Value & "," & DataGridViewBridge.Item(1, ro).Value & "," &
                        DataGridViewBridge.Item(2, ro).Value & "," & DataGridViewBridge.Item(3, ro).Value & "," &
                        DataGridViewBridge.Item(4, ro).Value & "," & DataGridViewBridge.Item(5, ro).Value & "," &
                        DataGridViewBridge.Item(6, ro).Value & "," & DataGridViewBridge.Item(7, ro).Value & "," &
                        DataGridViewBridge.Item(8, ro).Value & "," & DataGridViewBridge.Item(9, ro).Value & "," &
                        DataGridViewBridge.Item(10, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewFlyOver.RowCount - 1
                If DataGridViewFlyOver.Item(0, ro).Value <> "" And DataGridViewFlyOver.Item(1, ro).Value <> "" And DataGridViewFlyOver.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "fo," & DataGridViewFlyOver.Item(0, ro).Value & "," & DataGridViewFlyOver.Item(1, ro).Value & "," &
                        DataGridViewFlyOver.Item(2, ro).Value & "," & DataGridViewFlyOver.Item(3, ro).Value & "," &
                        DataGridViewFlyOver.Item(4, ro).Value & "," & DataGridViewFlyOver.Item(5, ro).Value & "," &
                        DataGridViewFlyOver.Item(6, ro).Value & "," & DataGridViewFlyOver.Item(7, ro).Value & "," &
                        DataGridViewFlyOver.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewCut.RowCount - 1
                If DataGridViewCut.Item(0, ro).Value <> "" And DataGridViewCut.Item(1, ro).Value <> "" And DataGridViewCut.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "cut," & DataGridViewCut.Item(0, ro).Value & "," & DataGridViewCut.Item(1, ro).Value & "," &
                        DataGridViewCut.Item(2, ro).Value & "," & DataGridViewCut.Item(3, ro).Value & "," &
                        DataGridViewCut.Item(4, ro).Value & "," & DataGridViewCut.Item(5, ro).Value & "," &
                        DataGridViewCut.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewDike.RowCount - 1
                If DataGridViewDike.Item(0, ro).Value <> "" And DataGridViewDike.Item(1, ro).Value <> "" And DataGridViewDike.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "dike," & DataGridViewDike.Item(0, ro).Value & "," & DataGridViewDike.Item(1, ro).Value & "," &
                        DataGridViewDike.Item(2, ro).Value & "," & DataGridViewDike.Item(3, ro).Value & "," &
                        DataGridViewDike.Item(4, ro).Value & "," & DataGridViewDike.Item(5, ro).Value & "," &
                        DataGridViewDike.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewRC.RowCount - 1
                If DataGridViewRC.Item(0, ro).Value <> "" And DataGridViewRC.Item(1, ro).Value <> "" And DataGridViewRC.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "rc," & DataGridViewRC.Item(0, ro).Value & "," & DataGridViewRC.Item(1, ro).Value & "," &
                        DataGridViewRC.Item(2, ro).Value & "," & DataGridViewRC.Item(3, ro).Value & "," &
                        DataGridViewRC.Item(4, ro).Value & "," & DataGridViewRC.Item(5, ro).Value & "," &
                        DataGridViewRC.Item(6, ro).Value & "," & DataGridViewRC.Item(7, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewPlatform.RowCount - 1
                If DataGridViewPlatform.Item(0, ro).Value <> "" And DataGridViewPlatform.Item(1, ro).Value <> "" And DataGridViewPlatform.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "pform," & DataGridViewPlatform.Item(0, ro).Value & "," & DataGridViewPlatform.Item(1, ro).Value & "," &
                        DataGridViewPlatform.Item(2, ro).Value & "," & DataGridViewPlatform.Item(3, ro).Value & "," &
                        DataGridViewPlatform.Item(4, ro).Value & "," & DataGridViewPlatform.Item(5, ro).Value & "," &
                        DataGridViewPlatform.Item(6, ro).Value & "," & DataGridViewPlatform.Item(7, ro).Value & "," &
                        DataGridViewPlatform.Item(8, ro).Value & "," & DataGridViewPlatform.Item(9, ro).Value & "," &
                        DataGridViewPlatform.Item(10, ro).Value & "," & DataGridViewPlatform.Item(11, ro).Value & "," &
                        DataGridViewPlatform.Item(12, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewPole.RowCount - 1
                If DataGridViewPole.Item(0, ro).Value <> "" And DataGridViewPole.Item(1, ro).Value <> "" And DataGridViewPole.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "poles," & DataGridViewPole.Item(0, ro).Value & "," & DataGridViewPole.Item(1, ro).Value & "," &
                        DataGridViewPole.Item(2, ro).Value & "," & DataGridViewPole.Item(3, ro).Value & "," &
                        DataGridViewPole.Item(4, ro).Value & "," & DataGridViewPole.Item(5, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            For ro = 0 To DataGridViewCrack.RowCount - 1
                If DataGridViewCrack.Item(0, ro).Value <> "" And DataGridViewCrack.Item(1, ro).Value <> "" And DataGridViewCrack.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "cracks," & DataGridViewCrack.Item(0, ro).Value & "," & DataGridViewCrack.Item(1, ro).Value & "," &
                        DataGridViewCrack.Item(2, ro).Value & "," & DataGridViewCrack.Item(3, ro).Value & "," &
                        DataGridViewCrack.Item(4, ro).Value & "," & DataGridViewCrack.Item(5, ro).Value & "," &
                        DataGridViewCrack.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next
            Try
                File.WriteAllText(filename, txt.ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub ButtonGenerateGBMapsJS_Click(sender As Object, e As EventArgs) Handles ButtonGenerateGBMapsJS.Click
        Dim basedir = gbIdir.ToLower.Replace("\images", "")
        If SaveFileDialog1.InitialDirectory = "" Then SaveFileDialog1.InitialDirectory = basedir & "\script"
        SaveFileDialog1.Filter = "Javascript file|*.js|All files|*.*"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim scriptfile = SaveFileDialog1.FileName 'basedir & "\script\gbm-objects.js"
            Dim txt As New StringBuilder
            'txt.AppendLine("GB Maps - ギビマップ Tools,2.0.0,gauge,object title")
            txt.AppendLine("// This file is created with GB Maps - ギビマップ Tools v2.2.0. If necessary, you can create your own file reference. ")
            txt.AppendLine("// Fail ini dicipta dengan GB Maps - ギビマップ Tools v2.2.0. Jika perlu, anda boleh membuat rujukan fail anda sendiri. ")
            txt.AppendLine("var gbmdatatool = 'GB Maps - ギビマップ Tools';")
            txt.AppendLine("var gbmdataversion = '2.2.0';")
            'txt.AppendLine("var gbmdatagauge = '1067';")
            'txt.AppendLine("var bverailobjArr = [];")
            ''txt.AppendLine("var bvegbmapOArr = [];")
            'txt.AppendLine("var bvebveStrOjArr = [];")
            'txt.AppendLine("var bvefreeObjArr = [];")
            'txt.AppendLine("var bvetrainObjArr = [];")
            'txt.AppendLine("var bveaudioObjArr = [];")
            'txt.AppendLine("var bvetrainDirArr = [];")
            'txt.AppendLine("var bvetunnelObjArr = [];")
            'txt.AppendLine("var bveplatformObjArr = [];")
            'txt.AppendLine("var bvecutObjArr = [];")
            'txt.AppendLine("var bvedikeObjArr = [];")
            'txt.AppendLine("var bveFOObjArr = [];")
            'txt.AppendLine("var bvebridgeObjArr = [];")
            'txt.AppendLine("var bveRCObjArr = [];")
            'txt.AppendLine("var bveUGObjArr = [];")
            'txt.AppendLine("var bvepoleObjArr = [];")
            'txt.AppendLine("var bvecrackObjArr = [];")
            txt.AppendLine("var ttxt ='';")

            txt.AppendLine()

            For ro = 0 To DataGridViewRailType.RowCount - 1
                If DataGridViewRailType.Item(0, ro).Value <> "" And DataGridViewRailType.Item(1, ro).Value <> "" And
                        DataGridViewRailType.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewRailType.Item(0, ro).Value & "','" &
                        DataGridViewRailType.Item(1, ro).Value & "','" & DataGridViewRailType.Item(2, ro).Value &
                        "','" & DataGridViewRailType.Item(3, ro).Value & "','" & DataGridViewRailType.Item(4, ro).Value &
                        "','" & DataGridViewRailType.Item(6, ro).Value & "','" & DataGridViewRailType.Item(5, ro).Value &
                        "','" & DataGridViewRailType.Item(7, ro).Value & "','" & DataGridViewRailType.Item(8, ro).Value &
                        "','" & DataGridViewRailType.Item(9, ro).Value & "','" & DataGridViewRailType.Item(10, ro).Value &
                        "','" & DataGridViewRailType.Item(11, ro).Value & "','" & DataGridViewRailType.Item(12, ro).Value &
                        "','" & DataGridViewRailType.Item(13, ro).Value & "','" & DataGridViewRailType.Item(14, ro).Value &
                        "','" & DataGridViewRailType.Item(15, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bverailobjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewBVEstr.RowCount - 1
                If DataGridViewBVEstr.Item(0, ro).Value <> "" And DataGridViewBVEstr.Item(1, ro).Value <> "" And DataGridViewBVEstr.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewBVEstr.Item(0, ro).Value & "','" & DataGridViewBVEstr.Item(1, ro).Value & "','" &
                        DataGridViewBVEstr.Item(2, ro).Value & "','" & DataGridViewBVEstr.Item(3, ro).Value & "','" &
                        DataGridViewBVEstr.Item(4, ro).Value & "','" & DataGridViewBVEstr.Item(5, ro).Value & "','" &
                        DataGridViewBVEstr.Item(6, ro).Value & "','" & DataGridViewBVEstr.Item(7, ro).Value & "','" &
                        DataGridViewBVEstr.Item(8, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvebveStrOjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewBVEfobj.RowCount - 1
                If DataGridViewBVEfobj.Item(0, ro).Value <> "" And DataGridViewBVEfobj.Item(1, ro).Value <> "" And DataGridViewBVEfobj.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewBVEfobj.Item(0, ro).Value & "','" & DataGridViewBVEfobj.Item(1, ro).Value & "','" &
                        DataGridViewBVEfobj.Item(2, ro).Value & "','" & DataGridViewBVEfobj.Item(3, ro).Value & "','" &
                        DataGridViewBVEfobj.Item(4, ro).Value & "','" & DataGridViewBVEfobj.Item(5, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvefreeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")

                End If
            Next

            For ro = 0 To DataGridViewaudio.RowCount - 1
                If DataGridViewaudio.Item(0, ro).Value <> "" And DataGridViewaudio.Item(1, ro).Value <> "" And DataGridViewaudio.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewaudio.Item(0, ro).Value & "','" & DataGridViewaudio.Item(1, ro).Value & "','" &
                        DataGridViewaudio.Item(2, ro).Value & "','" & DataGridViewaudio.Item(3, ro).Value & "','" &
                        DataGridViewaudio.Item(4, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveaudioObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '// *****************************
            For ro = 0 To DataGridViewTrainDir.RowCount - 1
                If DataGridViewTrainDir.Item(0, ro).Value <> "" And DataGridViewTrainDir.Item(1, ro).Value <> "" And DataGridViewTrainDir.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewTrainDir.Item(0, ro).Value & "','" & DataGridViewTrainDir.Item(1, ro).Value & "','" &
                        DataGridViewTrainDir.Item(2, ro).Value & "','" & DataGridViewTrainDir.Item(3, ro).Value & "','" &
                        DataGridViewTrainDir.Item(4, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvetrainDirArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewTunnel.RowCount - 1
                If DataGridViewTunnel.Item(0, ro).Value <> "" And DataGridViewTunnel.Item(1, ro).Value <> "" And DataGridViewTunnel.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewTunnel.Item(0, ro).Value & "','" & DataGridViewTunnel.Item(1, ro).Value & "','" &
                        DataGridViewTunnel.Item(2, ro).Value & "','" & DataGridViewTunnel.Item(3, ro).Value & "','" &
                        DataGridViewTunnel.Item(4, ro).Value & "','" & DataGridViewTunnel.Item(5, ro).Value & "','" &
                        DataGridViewTunnel.Item(6, ro).Value & "','" & DataGridViewTunnel.Item(7, ro).Value & "','" &
                        DataGridViewTunnel.Item(8, ro).Value & "','" & DataGridViewTunnel.Item(9, ro).Value & "','" &
                        DataGridViewTunnel.Item(10, ro).Value & "','" & DataGridViewTunnel.Item(11, ro).Value & "','" &
                        DataGridViewTunnel.Item(12, ro).Value & "','" & DataGridViewTunnel.Item(13, ro).Value & "','" &
                        DataGridViewTunnel.Item(14, ro).Value & "','" & DataGridViewTunnel.Item(15, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvetunnelObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewBridge.RowCount - 1
                If DataGridViewBridge.Item(0, ro).Value <> "" And DataGridViewBridge.Item(1, ro).Value <> "" And DataGridViewBridge.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewBridge.Item(0, ro).Value & "','" & DataGridViewBridge.Item(1, ro).Value & "','" &
                        DataGridViewBridge.Item(2, ro).Value & "','" & DataGridViewBridge.Item(3, ro).Value & "','" &
                        DataGridViewBridge.Item(4, ro).Value & "','" & DataGridViewBridge.Item(5, ro).Value & "','" &
                        DataGridViewBridge.Item(6, ro).Value & "','" & DataGridViewBridge.Item(7, ro).Value & "','" &
                        DataGridViewBridge.Item(8, ro).Value & "','" & DataGridViewBridge.Item(9, ro).Value & "','" &
                        DataGridViewBridge.Item(10, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvebridgeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewFlyOver.RowCount - 1
                If DataGridViewFlyOver.Item(0, ro).Value <> "" And DataGridViewFlyOver.Item(1, ro).Value <> "" And DataGridViewFlyOver.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewFlyOver.Item(0, ro).Value & "','" & DataGridViewFlyOver.Item(1, ro).Value & "','" &
                        DataGridViewFlyOver.Item(2, ro).Value & "','" & DataGridViewFlyOver.Item(3, ro).Value & "','" &
                        DataGridViewFlyOver.Item(4, ro).Value & "','" & DataGridViewFlyOver.Item(5, ro).Value & "','" &
                        DataGridViewFlyOver.Item(6, ro).Value & "','" & DataGridViewFlyOver.Item(7, ro).Value & "','" &
                        DataGridViewFlyOver.Item(8, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveFOObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewCut.RowCount - 1
                If DataGridViewCut.Item(0, ro).Value <> "" And DataGridViewCut.Item(1, ro).Value <> "" And DataGridViewCut.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewCut.Item(0, ro).Value & "','" & DataGridViewCut.Item(1, ro).Value & "','" &
                        DataGridViewCut.Item(2, ro).Value & "','" & DataGridViewCut.Item(3, ro).Value & "','" &
                        DataGridViewCut.Item(4, ro).Value & "','" & DataGridViewCut.Item(5, ro).Value & "','" &
                        DataGridViewCut.Item(6, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvecutObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewDike.RowCount - 1
                If DataGridViewDike.Item(0, ro).Value <> "" And DataGridViewDike.Item(1, ro).Value <> "" And DataGridViewDike.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewDike.Item(0, ro).Value & "','" & DataGridViewDike.Item(1, ro).Value & "','" &
                        DataGridViewDike.Item(2, ro).Value & "','" & DataGridViewDike.Item(3, ro).Value & "','" &
                        DataGridViewDike.Item(4, ro).Value & "','" & DataGridViewDike.Item(5, ro).Value & "','" &
                        DataGridViewDike.Item(6, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvedikeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewRC.RowCount - 1
                If DataGridViewRC.Item(0, ro).Value <> "" And DataGridViewRC.Item(1, ro).Value <> "" And DataGridViewRC.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewRC.Item(0, ro).Value & "','" & DataGridViewRC.Item(1, ro).Value & "','" &
                        DataGridViewRC.Item(2, ro).Value & "','" & DataGridViewRC.Item(3, ro).Value & "','" &
                        DataGridViewRC.Item(4, ro).Value & "','" & DataGridViewRC.Item(5, ro).Value & "','" &
                        DataGridViewRC.Item(6, ro).Value & "','" & DataGridViewRC.Item(7, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveRCObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewPlatform.RowCount - 1
                If DataGridViewPlatform.Item(0, ro).Value <> "" And DataGridViewPlatform.Item(1, ro).Value <> "" And DataGridViewPlatform.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewPlatform.Item(0, ro).Value & "','" & DataGridViewPlatform.Item(1, ro).Value & "','" &
                        DataGridViewPlatform.Item(2, ro).Value & "','" & DataGridViewPlatform.Item(3, ro).Value & "','" &
                        DataGridViewPlatform.Item(4, ro).Value & "','" & DataGridViewPlatform.Item(5, ro).Value & "','" &
                        DataGridViewPlatform.Item(6, ro).Value & "','" & DataGridViewPlatform.Item(7, ro).Value & "','" &
                        DataGridViewPlatform.Item(8, ro).Value & "','" & DataGridViewPlatform.Item(9, ro).Value & "','" &
                        DataGridViewPlatform.Item(10, ro).Value & "','" & DataGridViewPlatform.Item(11, ro).Value & "','" &
                        DataGridViewPlatform.Item(12, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveplatformObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewPole.RowCount - 1
                If DataGridViewPole.Item(0, ro).Value <> "" And DataGridViewPole.Item(1, ro).Value <> "" And DataGridViewPole.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewPole.Item(0, ro).Value & "','" & DataGridViewPole.Item(1, ro).Value & "','" &
                        DataGridViewPole.Item(2, ro).Value & "','" & DataGridViewPole.Item(3, ro).Value & "','" &
                        DataGridViewPole.Item(4, ro).Value & "','" & DataGridViewPole.Item(5, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvepoleObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            For ro = 0 To DataGridViewCrack.RowCount - 1
                If DataGridViewCrack.Item(0, ro).Value <> "" And DataGridViewCrack.Item(1, ro).Value <> "" And DataGridViewCrack.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewCrack.Item(0, ro).Value & "','" & DataGridViewCrack.Item(1, ro).Value & "','" &
                        DataGridViewCrack.Item(2, ro).Value & "','" & DataGridViewCrack.Item(3, ro).Value & "','" &
                        DataGridViewCrack.Item(4, ro).Value & "','" & DataGridViewCrack.Item(5, ro).Value & "','" &
                        DataGridViewCrack.Item(6, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvecrackObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            Try
                File.WriteAllText(scriptfile, txt.ToString)
                MessageBox.Show("Script file ('" & scriptfile & "') saved succesfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub PictureBoxBrowseBVEDir_Click(sender As Object, e As EventArgs) Handles PictureBoxBrowseBVEDir.Click
        DialogBrowseBVEDirHelp.Show()
    End Sub

    Private Sub PictureBoxPoleBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxPoleBVESyntax.Click
        FormPoleBVESyntax.Show()
    End Sub

    Private Sub PictureBoxTunnelBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxTunnelBVESyntax.Click
        FormTunnelBVESyntax.Show()
    End Sub

    Private Sub PictureBoxBridgeBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxBridgeBVESyntax.Click
        FormBridgeBVESyntax.Show()
    End Sub

    Private Sub PictureBoxOverpassBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxOverpassBVESyntax.Click
        FormOverpassBVESyntax.Show()
    End Sub

    Private Sub PictureBoxHillCutBVEsyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxHillCutBVEsyntax.Click
        FormHillCutBVESyntax.Show()
    End Sub

    Private Sub PictureBoxDikeBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxDikeBVESyntax.Click
        FormDikeBVEsyntax.Show()
    End Sub

    Private Sub PictureBoxRCBVEsyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxRCBVEsyntax.Click
        FormRCBVEsyntax.Show()
    End Sub

    Private Sub PictureBoxPlatformTip_Click(sender As Object, e As EventArgs)
        FormPlatformTip.Show()
    End Sub

    Private Sub PictureBoxPlatformBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxPlatformBVESyntax.Click
        FormPlatformBVESyntax.Show()
    End Sub

    Private Sub PictureBoxCrackTip_Click(sender As Object, e As EventArgs)
        FormCrackTip.Show()
    End Sub

    Private Sub PictureBoxCrackBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxCrackBVESyntax.Click
        FormCrackBVESyntax.Show()
    End Sub

    Private Sub ButtonRailTip_Click(sender As Object, e As EventArgs) Handles ButtonRailTip.Click
        FormRailPicHelp.Show()
    End Sub

    Private Sub ButtonPoleTip_Click(sender As Object, e As EventArgs) Handles ButtonPoleTip.Click
        FormPoleTip.Show()
    End Sub

    Private Sub ButtonTunnelTip_Click(sender As Object, e As EventArgs) Handles ButtonTunnelTip.Click
        FormTunnelTip.Show()
    End Sub

    Private Sub ButtonBridgeTip_Click(sender As Object, e As EventArgs) Handles ButtonBridgeTip.Click
        FormBridgeTip.Show()
    End Sub

    Private Sub ButtonOverpassTip_Click(sender As Object, e As EventArgs) Handles ButtonOverpassTip.Click
        FormOverpassTip.Show()
    End Sub

    Private Sub ButtonDikeTip_Click(sender As Object, e As EventArgs) Handles ButtonDikeTip.Click
        FormDikeTip.Show()
    End Sub

    Private Sub ButtonRCTip_Click(sender As Object, e As EventArgs) Handles ButtonRCTip.Click
        FormRCTip.Show()
    End Sub

    Private Sub ButtonPlatformTip_Click(sender As Object, e As EventArgs) Handles ButtonPlatformTip.Click
        FormPlatformTip.Show()
    End Sub

    Private Sub ButtonNewUG_Click(sender As Object, e As EventArgs) Handles ButtonNewUG.Click
        If textBoxUGName.Text <> "" And textBoxUGTitle.Text <> "" Then
            DataGridViewUG.Rows.Add(New String() {DataGridViewUG.RowCount - 1,
             textBoxUGName.Text, textBoxUGTitle.Text, textBoxUGImage.Text,
              textBoxUGGroundLeft.Text, textBoxUGGroundRight.Text, NumericUpDownUGgroundCycle.Value,
              textBoxUGoWallLeft.Text, textBoxUGoWallRight.Text, NumericUpDownUGoWallCycle.Value,
              textBoxUGEntrance.Text, textBoxUGiWallLeft.Text,
              textBoxUGiWallRight.Text, NumericUpDownUGiWallCycle.Value,
              textBoxUGExit.Text})
        End If
    End Sub

    Private Sub PictureBoxBrowseImgDir_Click(sender As Object, e As EventArgs) Handles PictureBoxBrowseImgDir.Click
        DialogBrowseGBMapsImageDirHelp.Show()
    End Sub

    Public Sub UpdateXFileField(TB As TextBox, file As filetype, PB As PictureBox)
        If bvedir Is Nothing And file = filetype.x Then
            MessageBox.Show("Please set default bve folder in step 1, first.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If gbIdir Is Nothing And file = filetype.img Then
            MessageBox.Show("Please set default image folder in step 1, first.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim FileDialog As New OpenFileDialog
        With FileDialog
            .AddExtension = True
            .CheckFileExists = True
            .CheckPathExists = True

            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True
            .DereferenceLinks = True
            Select Case file
                Case filetype.x
                    .Filter = "DirectX 3D object (*.x)|*.x"
                    If IO.Directory.Exists(currDir) Then .InitialDirectory = currDir
                Case filetype.img
                    .Filter = "Image Files (*.gif, *.jpg, *.png)|*.gif;*.jpg;*.png"
                    If IO.Directory.Exists(gbIdir) Then .InitialDirectory = gbIdir
                Case filetype.wav
                    .Filter = "Sound Files (*.wav)|*.wav"
                Case Else
                    .Filter = "All files|*.*"
            End Select

        End With
        If FileDialog.ShowDialog = DialogResult.OK Then
            Select Case file
                Case filetype.x
                    TB.Text = FileDialog.FileName.ToLower.Replace(bvedir.ToLower & "\", "")
                    currDir = My.Computer.FileSystem.GetParentPath(FileDialog.FileName)
                Case filetype.img
                    Try
                        TB.Text = FileDialog.FileName.ToLower.Replace(gbIdir.ToLower & "\", "")

                        PB.Image = Nothing
                        PB.Image = Image.FromFile(FileDialog.FileName)

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                Case filetype.wav
                    Dim filename = "sounds\" & My.Computer.FileSystem.GetFileInfo(FileDialog.FileName).Name
                    TB.Text = filename
                Case Else
                    TB.Text = FileDialog.FileName.ToLower
            End Select

        End If
    End Sub

End Class
