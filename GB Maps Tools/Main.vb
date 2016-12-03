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
    Friend onStartup As Boolean = True
    Friend kamus As New Dictionary(Of String, String)

    Public Enum filetype
        x = 0
        img = 1
        wav = 2
    End Enum

    Private Sub MainWin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = My.Application.Info.Title
        LabelAboutTitle.Text = My.Application.Info.Title
        LabelAboutVersion.Text = My.Application.Info.Version.ToString
        LabelAboutDescription.Text = My.Application.Info.Description
        LabelAboutCopyRight.Text = My.Application.Info.Copyright

        With kamus
            .Add("txtMsgErrorTitle", "Error")
            .Add("txtMsgInfoTitle", "Info")
            .Add("txtMsgWarningTitle", "Warning")
            .Add("txtBVEtraindir", "Please select BVE train folder ...")
            .Add("txtbuttonOpenCSVimgdir1st", "Please set image dir first")
            .Add("txtbuttonOpenCSVfilter", "GB Maps Data|*.csv|All files|*.*")
            .Add("txtbuttonOpenCSVerror1", "Sorry! invalid format or older data version not supported.")
            .Add("txtbuttonOpenCSVerror2", "unknown error.")
            .Add("txtButtonSaveTXTfilter", "GB Maps Data|*.csv|All files|*.*")
            .Add("txtButtonGenerateGBMapsJSfilter", "Javascript file|*.js|All files|*.*")
            .Add("txtButtonGenerateGBMapsJSSaved", "Script file saved succesfully.")
            .Add("txtUpdateXFileFieldErrorbvedir", "Please set default bve folder in step 1, first.")
            .Add("txtUpdateXFileFieldErrorimgdir", "Please set default image folder in step 1, first.")
            .Add("txtUpdateXFileFieldFilterX", "DirectX 3D object (*.x)|*.x")
            .Add("txtUpdateXFileFieldFilterImg", "Image Files (*.gif, *.jpg, *.png)|*.gif;*.jpg;*.png")
            .Add("txtUpdateXFileFieldFilterSnd", "Sound Files (*.wav)|*.wav")
            .Add("txtUpdateXFileFieldFilterAll", "All files|*.*")
            .Add("txtTabControl1Errorbvedir", "BVE data folder not exist")
            .Add("txtTabControl1Errorimgdir", "GB Maps image folder not exist")
            .Add("txtTabControl1RefSaved", "Reference saved")
            .Add("txtButtonSaveTXTSaved", "CSV file saved succesfully.")
        End With

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
        If File.Exists("lang.xml") = True Then
            Try
                Dim xLfile As XDocument = XDocument.Load("lang.xml")
                For Each lang In From element In xLfile.<language>.<lang>
                    ComboBoxLanguage.Items.Add(lang.@name.Replace("_", " "))
                    If lang.@select = "true" Then
                        ComboBoxLanguage.SelectedItem = lang.@name.Replace("_", " ")

                    End If
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        currDir = ""
        saved = True
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
        UpdateXFileField(TextBoxaudiofile, filetype.wav, Nothing)
    End Sub

    Private Sub Buttonaudioadd_Click(sender As System.Object, e As System.EventArgs) Handles Buttonaudioadd.Click
        If TextBoxaudioname.Text <> "" And TextBoxaudiotitle.Text <> "" And TextBoxaudiofile.Text <> "" And ComboBoxaudiotype.Text <> "" Then
            DataGridViewSound.Rows.Add(New String() {DataGridViewSound.RowCount - 1, TextBoxaudioname.Text,
                                                     TextBoxaudiotitle.Text, ComboBoxaudiotype.Text, TextBoxaudiofile.Text})
        End If
    End Sub

    Private Sub ButtonRailTypeAdd_Click(sender As System.Object, e As System.EventArgs) Handles buttonNewRail.Click
        If textBoxRailName.Text <> "" And textBoxRailTitle.Text <> "" And textBoxRailSleeper1.Text <> "" And
            comboBoxRailType.Text <> "" Then
            DataGridViewRail.Rows.Add(New String() {DataGridViewRail.RowCount - 1, textBoxRailName.Text,
            textBoxRailTitle.Text, textBoxRailImage.Text, comboBoxRailType.Text, comboBoxRailGauge.Text,
            textBoxRailSleeper1.Text, textBoxRailLeft1.Text, textBoxRailRight1.Text, textBoxRailSleeper2.Text,
            textBoxRailLeft2.Text, textBoxRailRight2.Text, textBoxRailSleeper3.Text, textBoxRailLeft3.Text,
            textBoxRailRight3.Text, textBoxRailSleeper4.Text, textBoxRailLeft4.Text, textBoxRailRight4.Text,
            textBoxRailSleeper5.Text, textBoxRailLeft5.Text, textBoxRailRight5.Text, NumericUpDownRailCycle.Value})

            textBoxRailSleeper1.Text = ""
            textBoxRailLeft1.Text = ""
            textBoxRailRight1.Text = ""
            textBoxRailSleeper2.Text = ""
            textBoxRailLeft2.Text = ""
            textBoxRailRight2.Text = ""
            textBoxRailSleeper3.Text = ""
            textBoxRailLeft3.Text = ""
            textBoxRailRight3.Text = ""
            textBoxRailSleeper4.Text = ""
            textBoxRailLeft4.Text = ""
            textBoxRailRight4.Text = ""
            textBoxRailSleeper5.Text = ""
            textBoxRailLeft5.Text = ""
            textBoxRailRight5.Text = ""

        End If
    End Sub

    Private Sub buttonBrowseRailSleeper1_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRailSleeper1.Click
        UpdateXFileField(textBoxRailSleeper1, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseRailImage_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRailImage.Click
        UpdateXFileField(textBoxRailImage, filetype.img, PictureBoxRailTypeImg)
    End Sub

    Private Sub Buttonbvestrfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseBVEfile.Click
        UpdateXFileField(TextBoxBVEfile, filetype.x, Nothing)
    End Sub

    Private Sub Buttonbvestrimg_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseEtcBVEImage.Click
        UpdateXFileField(TextBoxetcBVEImage, filetype.img, PictureBoxBVEstrimg)
    End Sub

    Private Sub ButtonBVEstrAdd_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewBVE.Click
        If ComboBoxBVEType.Text <> "" And TextBoxBVEstrname.Text <> "" And TextBoxBVEstrtitle.Text <> "" And TextBoxBVEfile.Text <> "" Then
            DataGridViewEtc.Rows.Add(New String() {DataGridViewEtc.RowCount - 1,
            TextBoxBVEstrname.Text, TextBoxBVEstrtitle.Text, TextBoxetcBVEImage.Text,
            ComboBoxBVEType.Text, TextBoxBVEfile.Text,
            textBoxBVELeft.Text, TextBoxBVERight.Text,
            NumericUpDownBveX.Value})
            TextBoxBVEfile.Text = ""
            textBoxBVELeft.Text = ""
            TextBoxBVERight.Text = ""

        End If
    End Sub

    Private Sub Buttonbvefobjadd_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewFreeObject.Click
        If ComboBoxFreeObjectType.Text <> "" And TextBoxbvefobjname.Text <> "" And TextBoxbvefobjtitle.Text <> "" And textBoxFreeObjectFile.Text <> "" Then
            DataGridViewFreeObject.Rows.Add(New String() {DataGridViewFreeObject.RowCount - 1,
            TextBoxbvefobjname.Text, TextBoxbvefobjtitle.Text, textBoxFreeObjectImage.Text,
            ComboBoxFreeObjectType.Text, textBoxFreeObjectFile.Text})

            textBoxFreeObjectFile.Text = ""

        End If
    End Sub

    Private Sub ButtonbveFOfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseFreeObjectFile.Click
        UpdateXFileField(textBoxFreeObjectFile, filetype.x, Nothing)
    End Sub

    Private Sub ButtonbveFOimg_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseFreeObjectImage.Click
        UpdateXFileField(textBoxFreeObjectImage, filetype.img, PictureBoxbvefobjimg)
    End Sub

    Private Sub DataGridViewaudio_Click(sender As Object, e As System.EventArgs) Handles DataGridViewSound.Click
        Try
            Dim irow As Integer = DataGridViewSound.CurrentRow.Index
            Dim basedir = bvedir.Substring(0, bvedir.LastIndexOf("\"))
            Dim fullpath As String = basedir & "\" & DataGridViewSound.Item(4, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxaudiorun.Visible = True
                My.Computer.Audio.Stop()
                My.Computer.Audio.Play(fullpath)
            Else
                PictureBoxaudiorun.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBVEfobj_Click(sender As Object, e As System.EventArgs) Handles DataGridViewFreeObject.Click
        Try
            Dim irow As Integer = DataGridViewFreeObject.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewFreeObject.Item(4, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxbvefobjimg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxbvefobjimg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBVEstr_Click(sender As Object, e As System.EventArgs) Handles DataGridViewEtc.Click
        Try
            Dim irow As Integer = DataGridViewEtc.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewEtc.Item(4, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxBVEstrimg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxBVEstrimg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewRail_Click(sender As Object, e As System.EventArgs) Handles DataGridViewRail.Click
        Try
            Dim irow As Integer = DataGridViewRail.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewRail.Item(3, irow).Value.ToString.Trim
            'MsgBox(fullpath)
            If File.Exists(fullpath) Then
                PictureBoxRailTypeImg.Image = Image.FromFile(fullpath)
            Else
                PictureBoxRailTypeImg.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    DataGridViewTrain.Rows.Add(New String() {bil, dirName, dirName, imgFile, dirName})
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
        FolderBrowserDialog1.Description = kamus.Item("txtBVEtraindir")
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBoxTrainDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ButtonTunnelBrowseImage_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseTunnelImage.Click
        UpdateXFileField(textBoxTunnelImage, filetype.img, PictureBoxTunnelPicture)
    End Sub

    Private Sub buttonNewTunnel_Click(sender As System.Object, e As System.EventArgs) Handles buttonNewTunnel.Click
        If textBoxTunnelName.Text <> "" And textBoxTunnelTitle.Text <> "" And textBoxTunnelEntrance.Text <> "" Then
            DataGridViewTunnel.Rows.Add(New String() {DataGridViewTunnel.RowCount - 1, textBoxTunnelName.Text,
                textBoxTunnelTitle.Text, textBoxTunnelImage.Text,
                textBoxTunnelEntrance.Text, textBoxTunnelExitStructure.Text,
                textBoxTunnelWallLeft.Text, textBoxTunnelWallRight.Text,
                NumericUpDownTunnelWallCycle.Value})
            textBoxTunnelEntrance.Text = ""
            textBoxTunnelExitStructure.Text = ""
            textBoxTunnelWallLeft.Text = ""
            textBoxTunnelWallRight.Text = ""
        End If
    End Sub

    Private Sub ButtonBrowseBridgeImageFile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowseBridgeImageFile.Click
        UpdateXFileField(TextBoxBridgeImage, filetype.img, PictureBoxBridge)
    End Sub

    Private Sub ButtonFOBrowseImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseOverPassImage.Click
        UpdateXFileField(textBoxOverPassImage, filetype.img, PictureBoxFO)
    End Sub

    Private Sub ButtonBrowseCutImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseHillCutImage.Click
        UpdateXFileField(textBoxHillCutImage, filetype.img, PictureBoxHillCut)
    End Sub

    Private Sub ButtonDikeBrowseImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseDikeImage.Click
        UpdateXFileField(textBoxDikeImage, filetype.img, PictureBoxDike)
    End Sub

    Private Sub ButtonBrowseRCImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseRCImage.Click
        UpdateXFileField(textBoxRCImage, filetype.img, PictureBoxRC)
    End Sub

    Private Sub ButtonBrowsePformImgFile_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePlatformImage.Click
        UpdateXFileField(textBoxPlatformImage, filetype.img, PictureBoxPlatform)
    End Sub

    Private Sub ButtonNewBridge_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewBridge.Click
        If TextBoxBridgeName.Text <> "" And TextBoxBridgeTitle.Text <> "" Then
            DataGridViewBridge.Rows.Add(New String() {DataGridViewBridge.RowCount - 1, TextBoxBridgeName.Text,
                TextBoxBridgeTitle.Text, TextBoxBridgeImage.Text,
                TextBoxBridgeLeft.Text, TextBoxBridgeRight.Text, NumericUpDownBridgeWallCycle.Value,
                textBoxBridgePier.Text, NumericUpDownBridgePierCycle.Value})
            TextBoxBridgeLeft.Text = ""
            TextBoxBridgeRight.Text = ""
            textBoxBridgePier.Text = ""
        End If
    End Sub

    Private Sub ButtonNewOverpass_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewOverpass.Click
        If textBoxOverPassName.Text <> "" And textBoxOverPassTitle.Text <> "" Then
            DataGridViewOverpass.Rows.Add(New String() {DataGridViewOverpass.RowCount - 1,
                textBoxOverPassName.Text, textBoxOverPassTitle.Text, textBoxOverPassImage.Text,
                textBoxOverPassWallLeft.Text, textBoxOverPassWallRight.Text, NumericUpDownOverpassWallCycle.Value,
                textBoxOverPassPier.Text, NumericUpDownOverpassPierCycle.Value})
            textBoxOverPassWallLeft.Text = ""
            textBoxOverPassWallRight.Text = ""
            textBoxOverPassPier.Text = ""
        End If
    End Sub

    Private Sub ButtonAddCut_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewHillCut.Click
        If textBoxHillCutName.Text <> "" And textBoxHillCutTitle.Text <> "" Then
            DataGridViewHillCut.Rows.Add(New String() {DataGridViewHillCut.RowCount - 1,
                textBoxHillCutName.Text, textBoxHillCutTitle.Text, textBoxHillCutImage.Text,
                textBoxHillCutLeft.Text, textBoxHillCutRight.Text, NumericUpDownHillCutCycle.Value})
            textBoxHillCutLeft.Text = ""
            textBoxHillCutRight.Text = ""
        End If
    End Sub


    Private Sub ButtonNewDike_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewDike.Click
        If textBoxDikeName.Text <> "" And textBoxDikeTitle.Text <> "" Then
            DataGridViewDike.Rows.Add(New String() {DataGridViewDike.RowCount - 1,
            textBoxDikeName.Text, textBoxDikeTitle.Text, textBoxDikeImage.Text,
            textBoxDikeLeft.Text, textBoxDikeRight.Text, NumericUpDownDikeCycle.Value})
            textBoxDikeLeft.Text = ""
            textBoxDikeRight.Text = ""
        End If
    End Sub

    Private Sub ButtonAddRC_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewRC.Click
        If textBoxRCName.Text <> "" And textBoxRCTitle.Text <> "" Then
            DataGridViewRC.Rows.Add(New String() {DataGridViewRC.RowCount - 1,
                textBoxRCName.Text, textBoxRCTitle.Text, textBoxRCImage.Text,
                textBoxRCgateLeft.Text, textBoxRCIntersection.Text, textBoxRCgateRight.Text,
                textBoxRCSound.Text})
            textBoxRCgateLeft.Text = ""
            textBoxRCIntersection.Text = ""
            textBoxRCgateRight.Text = ""
            textBoxRCSound.Text = ""

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
            textBoxPlatformLeft.Text = ""
            textBoxPlatformMiddleLeft.Text = ""
            textBoxPlatformMiddleRight.Text = ""
            textBoxPlatformRight.Text = ""
            textBoxPlatformRoofLeft.Text = ""
            textBoxPlatformRoofMiddleLeft.Text = ""
            textBoxPlatformRoofMiddleRight.Text = ""
            textBoxPlatformRoofRight.Text = ""

        End If
    End Sub

    Private Sub DataGridViewTrainDir_Click(sender As Object, e As System.EventArgs) Handles DataGridViewTrain.Click
        Try
            Dim irow As Integer = DataGridViewTrain.CurrentRow.Index
            Dim basedir = bvedir.Substring(0, bvedir.LastIndexOf("\")) & "\trains" 'bvedir.Replace("Railway\Object", "") & "train"

            Dim fullpath As String = basedir.ToLower & "\" & DataGridViewTrain.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxTrainDir.Image = Image.FromFile(fullpath)
            Else
                PictureBoxTrainDir.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewTunnel_Click(sender As Object, e As System.EventArgs) Handles DataGridViewTunnel.Click
        Try
            Dim irow As Integer = DataGridViewTunnel.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewTunnel.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxTunnelPicture.Image = Image.FromFile(fullpath)
            Else
                PictureBoxTunnelPicture.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewBridge_Click(sender As Object, e As System.EventArgs) Handles DataGridViewBridge.Click
        Try
            Dim irow As Integer = DataGridViewBridge.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewBridge.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxBridge.Image = Image.FromFile(fullpath)
            Else
                PictureBoxBridge.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewFlyOver_Click(sender As Object, e As System.EventArgs) Handles DataGridViewOverpass.Click
        Try
            Dim irow As Integer = DataGridViewOverpass.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewOverpass.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxFO.Image = Image.FromFile(fullpath)
            Else
                PictureBoxFO.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewCut_Click(sender As Object, e As System.EventArgs) Handles DataGridViewHillCut.Click
        Try
            Dim irow As Integer = DataGridViewHillCut.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewHillCut.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxHillCut.Image = Image.FromFile(fullpath)
            Else
                PictureBoxHillCut.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewDike_Click(sender As Object, e As System.EventArgs) Handles DataGridViewDike.Click
        Try
            Dim irow As Integer = DataGridViewDike.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewDike.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxDike.Image = Image.FromFile(fullpath)
            Else
                PictureBoxDike.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewRC_Click(sender As Object, e As System.EventArgs) Handles DataGridViewRC.Click
        Try
            Dim irow As Integer = DataGridViewRC.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewRC.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxRC.Image = Image.FromFile(fullpath)
            Else
                PictureBoxRC.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridViewPlatform_Click(sender As Object, e As System.EventArgs) Handles DataGridViewPlatform.Click
        Try
            Dim irow As Integer = DataGridViewPlatform.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewPlatform.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxPlatform.Image = Image.FromFile(fullpath)
            Else
                PictureBoxPlatform.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub buttonNewPole_Click(sender As System.Object, e As System.EventArgs) Handles buttonNewPole.Click
        If textBoxPoleName.Text <> "" And textBoxPoleTitle.Text <> "" Then
            DataGridViewPole.Rows.Add(New String() {DataGridViewPole.RowCount - 1, textBoxPoleName.Text,
                textBoxPoleTitle.Text, textBoxPoleImage.Text, textBoxPoleStructureLeft.Text,
                textBoxPoleStructureRight.Text, textBoxOverHeadWire.Text, NumericUpDownPoleCycle.Value})
            textBoxPoleStructureLeft.Text = ""
            textBoxPoleStructureRight.Text = ""
            textBoxOverHeadWire.Text = ""
        End If
    End Sub

    Private Sub ButtonBrowsePoleCSV_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePoleStructureLeft.Click
        UpdateXFileField(textBoxPoleStructureLeft, filetype.x, Nothing)
    End Sub

    Private Sub ButtonBrowsePoleImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowsePoleImage.Click
        UpdateXFileField(textBoxPoleImage, filetype.img, PictureBoxPole)
    End Sub

    Private Sub DataGridViewPole_Click(sender As Object, e As System.EventArgs) Handles DataGridViewPole.Click
        Try
            Dim irow As Integer = DataGridViewPole.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewPole.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxPole.Image = Image.FromFile(fullpath)
            Else
                PictureBoxPole.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonNewCrack_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNewCrack.Click
        If textBoxCrackName.Text <> "" And textBoxCrackTitle.Text <> "" Then
            DataGridViewCrack.Rows.Add(New String() {DataGridViewCrack.RowCount - 1,
                textBoxCrackName.Text, textBoxCrackTitle.Text, textBoxCrackImage.Text,
               textBoxCrackLeft.Text, textBoxCrackRight.Text, NumericUpDownCrackCycle.Value})
            textBoxCrackLeft.Text = ""
            textBoxCrackRight.Text = ""
        End If
    End Sub

    Private Sub ButtonBrowseCrackImg_Click(sender As System.Object, e As System.EventArgs) Handles buttonBrowseCrackImage.Click
        UpdateXFileField(textBoxCrackImage, filetype.img, PictureBoxCrack)
    End Sub

    Private Sub TextBoxCrackLeftcsv_Click(sender As System.Object, e As System.EventArgs) Handles textBoxCrackLeft.Click
        UpdateXFileField(textBoxCrackLeft, filetype.x, Nothing)
    End Sub

    Private Sub TextBoxCrackRightcsv_Click(sender As System.Object, e As System.EventArgs) Handles textBoxCrackRight.Click
        UpdateXFileField(textBoxCrackRight, filetype.x, Nothing)
    End Sub

    Private Sub DataGridViewCrack_Click(sender As Object, e As System.EventArgs) Handles DataGridViewCrack.Click
        Try
            Dim irow As Integer = DataGridViewCrack.CurrentRow.Index
            Dim fullpath As String = gbIdir.ToLower & "\" & DataGridViewCrack.Item(3, irow).Value.ToString.Trim
            If File.Exists(fullpath) Then
                PictureBoxCrack.Image = Image.FromFile(fullpath)
            Else
                PictureBoxCrack.Image = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ComboBoxBVEstrtype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxBVEType.SelectedIndexChanged
        If ComboBoxBVEType.SelectedItem = "Wall" Then
            GroupBoxS16_Txt08.Enabled = True
        Else
            GroupBoxS16_Txt08.Enabled = False
        End If
    End Sub

    Private Sub ButtonbveWallLfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbrowseBVELeft.Click
        UpdateXFileField(textBoxBVELeft, filetype.x, Nothing)
    End Sub

    Private Sub ButtonbveWallRfile_Click(sender As System.Object, e As System.EventArgs) Handles ButtonbrowseBVERight.Click
        UpdateXFileField(TextBoxBVERight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseTunnelWallLeft_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelWallLeft.Click
        UpdateXFileField(textBoxTunnelWallLeft, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseTunnelWallRight_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelWallRight.Click
        UpdateXFileField(textBoxTunnelWallRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseTunnelEntrance_Click(sender As Object, e As EventArgs) Handles buttonBrowseTunnelEntrance.Click
        UpdateXFileField(textBoxTunnelEntrance, filetype.x, Nothing)
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

    Private Sub buttonOpenCSV_Click(sender As Object, e As EventArgs) Handles buttonOpenCSV.Click
        Dim basedir As String
        If gbIdir = "" Then
            MessageBox.Show(kamus.item("txtbuttonOpenCSVimgdir1st"))
            Exit Sub
            Else
            basedir = gbIdir.ToLower.Replace("\images", "")
            If basedir <> "" Then OpenFileDialog3.InitialDirectory = basedir
            End If

            OpenFileDialog3.Filter = kamus.Item("txtbuttonOpenCSVfilter")

        If OpenFileDialog3.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim filename As String = OpenFileDialog3.FileName
            Dim teks As String = My.Computer.FileSystem.ReadAllText(filename)
            Dim arrRow As String() = teks.Split(vbLf)

            '# version check
            Dim vercek = arrRow(0).Split("_")
            If vercek.Count < 3 Then
                MessageBox.Show(kamus.item("txtbuttonOpenCSVerror1"), kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            '# "gbmapstools_v_2.2"
            If vercek(0) <> "gbmapstools" And vercek(1) <> "v" And (Convert.ToDouble(vercek(2)) >= 2.2) Then
                MessageBox.Show(kamus.item("txtbuttonOpenCSVerror2"), kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            For Each drow As String In arrRow

                Dim dd As String() = drow.Split(", ")

                For i = 0 To dd.Count - 1
                    dd(i) = dd(i).Trim
                Next
                Select Case dd(0)
                    Case "rail"
                        '#2 "rail, "
                        Try
                            DataGridViewRail.Rows.Add(New String() {
                               dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10),
                               dd(11), dd(12), dd(13), dd(14), dd(15), dd(16), dd(17), dd(18), dd(19), dd(20),
                               dd(21), dd(22)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                    Case "pole"
                        '#3 "pole,"
                        Try
                            DataGridViewPole.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "train"
                        '#4 "train, "
                        Try
                            DataGridViewTrain.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "sound"
                        '#5 "sound,"
                        Try
                            DataGridViewSound.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "tunnel"
                        '#6 "tunnel, "
                        Try
                            DataGridViewTunnel.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "bridge"
                        '#7 "bridge,"
                        Try
                            DataGridViewBridge.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "overpass"
                        '#8 "overpass, "
                        Try
                            DataGridViewOverpass.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "hillcut"
                        '#9 "hillcut,"
                        Try
                            DataGridViewHillCut.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "dike"
                        '#10 "dike, "
                        Try
                            DataGridViewDike.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "rc"
                        '#11 "rc,"
                        Try
                            DataGridViewRC.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "pform"
                        '#12 "pform, "
                        Try
                            DataGridViewPlatform.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10),
                            dd(11), dd(12), dd(13), dd(14)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "cracks"
                        '#13 "cracks,"
                        Try
                            DataGridViewCrack.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "ug"
                        '#14 "ug, "
                        Try
                            DataGridViewUG.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9), dd(10),
                            dd(11), dd(12), dd(13), dd(14), dd(15)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try


                    Case "fobj"
                        '#15 "fobj,"
                        Try
                            DataGridViewFreeObject.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    Case "etc"
                        '#16 "etc, "
                        Try
                            DataGridViewEtc.Rows.Add(New String() {
                            dd(1), dd(2), dd(3), dd(4), dd(5), dd(6), dd(7), dd(8), dd(9)})
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, kamus.Item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                        '//***************
                    Case Else

                End Select

            Next

            TabControl1.SelectedTab = Step_2

        End If
    End Sub

    Private Sub buttonGBImageDir_Click(sender As Object, e As EventArgs) Handles buttonGBImageDir.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            textBoxGBimgDir.Text = FolderBrowserDialog1.SelectedPath
            gbIdir = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub buttonNewXML_Click(sender As Object, e As EventArgs) Handles buttonNewXML.Click
        DataGridViewRail.Rows.Clear()  '#2
        DataGridViewPole.Rows.Clear()  '#3
        DataGridViewTrain.Rows.Clear()  '#4
        DataGridViewSound.Rows.Clear()  '#5
        DataGridViewTunnel.Rows.Clear()  '#6
        DataGridViewBridge.Rows.Clear()  '#7
        DataGridViewOverpass.Rows.Clear()  '#8
        DataGridViewHillCut.Rows.Clear()  '#9
        DataGridViewDike.Rows.Clear()  '#10
        DataGridViewRC.Rows.Clear()  '#11
        DataGridViewPlatform.Rows.Clear()  '#12
        DataGridViewCrack.Rows.Clear()  '#13
        DataGridViewUG.Rows.Clear()  '#14
        DataGridViewFreeObject.Rows.Clear()  '#15
        DataGridViewEtc.Rows.Clear()  '#16
    End Sub

    Private Sub ButtonRailL_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailLeft1.Click
        UpdateXFileField(textBoxRailLeft1, filetype.x, Nothing)
    End Sub

    Private Sub ButtonRailR_Click(sender As Object, e As EventArgs) Handles buttonBrowseRailRight1.Click
        UpdateXFileField(textBoxRailRight1, filetype.x, Nothing)
    End Sub

    Private Sub ButtonSaveTXT_Click(sender As Object, e As EventArgs) Handles ButtonSaveTXT.Click
        Dim basedir = gbIdir.ToLower.Replace("\images", "")
        If SaveFileDialog1.InitialDirectory = "" Then SaveFileDialog1.InitialDirectory = basedir & "\data"
        SaveFileDialog1.Filter = kamus.Item("txtButtonSaveTXTfilter")
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim txt As New StringBuilder
            Dim filename As String = SaveFileDialog1.FileName
            txt.AppendLine("gbmapstools_v_2.2")
            '#2 Rail (22 item = 0 - 21)
            For ro = 0 To DataGridViewRail.RowCount - 1
                If DataGridViewRail.Item(0, ro).Value <> "" And DataGridViewRail.Item(1, ro).Value <> "" And
                    DataGridViewRail.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "rail, " & DataGridViewRail.Item(0, ro).Value & ", " &
                        DataGridViewRail.Item(1, ro).Value & ", " & DataGridViewRail.Item(2, ro).Value &
                        ", " & DataGridViewRail.Item(3, ro).Value & ", " & DataGridViewRail.Item(4, ro).Value &
                        ", " & DataGridViewRail.Item(5, ro).Value & ", " & DataGridViewRail.Item(6, ro).Value &
                        ", " & DataGridViewRail.Item(7, ro).Value & ", " & DataGridViewRail.Item(8, ro).Value &
                        ", " & DataGridViewRail.Item(9, ro).Value & ", " & DataGridViewRail.Item(10, ro).Value &
                        ", " & DataGridViewRail.Item(11, ro).Value & ", " & DataGridViewRail.Item(12, ro).Value &
                        ", " & DataGridViewRail.Item(13, ro).Value & ", " & DataGridViewRail.Item(14, ro).Value &
                        ", " & DataGridViewRail.Item(15, ro).Value & ", " & DataGridViewRail.Item(16, ro).Value &
                        ", " & DataGridViewRail.Item(17, ro).Value & ", " & DataGridViewRail.Item(18, ro).Value &
                        ", " & DataGridViewRail.Item(19, ro).Value & ", " & DataGridViewRail.Item(20, ro).Value &
                        ", " & DataGridViewRail.Item(21, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#3 Pole (8 item = 0 - 7)
            For ro = 0 To DataGridViewPole.RowCount - 1
                If DataGridViewPole.Item(0, ro).Value <> "" And DataGridViewPole.Item(1, ro).Value <> "" And
                    DataGridViewPole.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "pole, " & DataGridViewPole.Item(0, ro).Value &
                        ", " & DataGridViewPole.Item(1, ro).Value & ", " & DataGridViewPole.Item(2, ro).Value &
                        ", " & DataGridViewPole.Item(3, ro).Value & ", " & DataGridViewPole.Item(4, ro).Value &
                        ", " & DataGridViewPole.Item(5, ro).Value & ", " & DataGridViewPole.Item(6, ro).Value &
                        ", " & DataGridViewPole.Item(7, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#4 Train (5 item = 0 - 4)
            For ro = 0 To DataGridViewTrain.RowCount - 1
                If DataGridViewTrain.Item(0, ro).Value <> "" And DataGridViewTrain.Item(1, ro).Value <> "" And
                    DataGridViewTrain.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "train, " & DataGridViewTrain.Item(0, ro).Value &
                        ", " & DataGridViewTrain.Item(1, ro).Value & ", " & DataGridViewTrain.Item(2, ro).Value &
                        ", " & DataGridViewTrain.Item(3, ro).Value & ", " & DataGridViewTrain.Item(4, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#5 Sound (5 item = 0 - 4)
            For ro = 0 To DataGridViewSound.RowCount - 1
                If DataGridViewSound.Item(0, ro).Value <> "" And DataGridViewSound.Item(1, ro).Value <> "" And
                    DataGridViewSound.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "sound, " & DataGridViewSound.Item(0, ro).Value &
                        ", " & DataGridViewSound.Item(1, ro).Value & ", " & DataGridViewSound.Item(2, ro).Value &
                        ", " & DataGridViewSound.Item(3, ro).Value & ", " & DataGridViewSound.Item(4, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#6 Tunnel (9 item = 0 - 8)
            For ro = 0 To DataGridViewTunnel.RowCount - 1
                If DataGridViewTunnel.Item(0, ro).Value <> "" And DataGridViewTunnel.Item(1, ro).Value <> "" And
                    DataGridViewTunnel.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "tunnel, " & DataGridViewTunnel.Item(0, ro).Value &
                        ", " & DataGridViewTunnel.Item(1, ro).Value & ", " & DataGridViewTunnel.Item(2, ro).Value &
                        ", " & DataGridViewTunnel.Item(3, ro).Value & ", " & DataGridViewTunnel.Item(4, ro).Value &
                        ", " & DataGridViewTunnel.Item(5, ro).Value & ", " & DataGridViewTunnel.Item(6, ro).Value &
                        ", " & DataGridViewTunnel.Item(7, ro).Value & ", " & DataGridViewTunnel.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#7 Bridge (9 item = 0 - 8)
            For ro = 0 To DataGridViewBridge.RowCount - 1
                If DataGridViewBridge.Item(0, ro).Value <> "" And DataGridViewBridge.Item(1, ro).Value <> "" And
                    DataGridViewBridge.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "bridge, " & DataGridViewBridge.Item(0, ro).Value &
                        ", " & DataGridViewBridge.Item(1, ro).Value & ", " & DataGridViewBridge.Item(2, ro).Value &
                        ", " & DataGridViewBridge.Item(3, ro).Value & ", " & DataGridViewBridge.Item(4, ro).Value &
                        ", " & DataGridViewBridge.Item(5, ro).Value & ", " & DataGridViewBridge.Item(6, ro).Value &
                        ", " & DataGridViewBridge.Item(7, ro).Value & ", " & DataGridViewBridge.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#8 Overpass (9 item = 0 - 8)
            For ro = 0 To DataGridViewOverpass.RowCount - 1
                If DataGridViewOverpass.Item(0, ro).Value <> "" And DataGridViewOverpass.Item(1, ro).Value <> "" And
                    DataGridViewOverpass.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "overpass, " & DataGridViewOverpass.Item(0, ro).Value &
                        ", " & DataGridViewOverpass.Item(1, ro).Value & ", " & DataGridViewOverpass.Item(2, ro).Value &
                        ", " & DataGridViewOverpass.Item(3, ro).Value & ", " & DataGridViewOverpass.Item(4, ro).Value &
                        ", " & DataGridViewOverpass.Item(5, ro).Value & ", " & DataGridViewOverpass.Item(6, ro).Value &
                        ", " & DataGridViewOverpass.Item(7, ro).Value & ", " & DataGridViewOverpass.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#9 Hillcut (7 item = 0 - 6)
            For ro = 0 To DataGridViewHillCut.RowCount - 1
                If DataGridViewHillCut.Item(0, ro).Value <> "" And DataGridViewHillCut.Item(1, ro).Value <> "" And
                    DataGridViewHillCut.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "hillcut, " & DataGridViewHillCut.Item(0, ro).Value &
                        ", " & DataGridViewHillCut.Item(1, ro).Value & ", " & DataGridViewHillCut.Item(2, ro).Value &
                        ", " & DataGridViewHillCut.Item(3, ro).Value & ", " & DataGridViewHillCut.Item(4, ro).Value &
                        ", " & DataGridViewHillCut.Item(5, ro).Value & ", " & DataGridViewHillCut.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#10 Dike (7 item = 0 - 6)
            For ro = 0 To DataGridViewDike.RowCount - 1
                If DataGridViewDike.Item(0, ro).Value <> "" And DataGridViewDike.Item(1, ro).Value <> "" And
                    DataGridViewDike.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "dike, " & DataGridViewDike.Item(0, ro).Value &
                        ", " & DataGridViewDike.Item(1, ro).Value & ", " & DataGridViewDike.Item(2, ro).Value &
                        ", " & DataGridViewDike.Item(3, ro).Value & ", " & DataGridViewDike.Item(4, ro).Value &
                        ", " & DataGridViewDike.Item(5, ro).Value & ", " & DataGridViewDike.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#11 Road Crossing (8 item = 0 - 7)
            For ro = 0 To DataGridViewRC.RowCount - 1
                If DataGridViewRC.Item(0, ro).Value <> "" And DataGridViewRC.Item(1, ro).Value <> "" And
                    DataGridViewRC.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "rc, " & DataGridViewRC.Item(0, ro).Value &
                        ", " & DataGridViewRC.Item(1, ro).Value & ", " & DataGridViewRC.Item(2, ro).Value &
                        ", " & DataGridViewRC.Item(3, ro).Value & ", " & DataGridViewRC.Item(4, ro).Value &
                        ", " & DataGridViewRC.Item(5, ro).Value & ", " & DataGridViewRC.Item(6, ro).Value &
                        ", " & DataGridViewRC.Item(7, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#12 Platform (14 item = 0 - 13)
            For ro = 0 To DataGridViewPlatform.RowCount - 1
                If DataGridViewPlatform.Item(0, ro).Value <> "" And DataGridViewPlatform.Item(1, ro).Value <> "" And
                    DataGridViewPlatform.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "pform, " & DataGridViewPlatform.Item(0, ro).Value &
                        ", " & DataGridViewPlatform.Item(1, ro).Value & ", " & DataGridViewPlatform.Item(2, ro).Value &
                        ", " & DataGridViewPlatform.Item(3, ro).Value & ", " & DataGridViewPlatform.Item(4, ro).Value &
                        ", " & DataGridViewPlatform.Item(5, ro).Value & ", " & DataGridViewPlatform.Item(6, ro).Value &
                        ", " & DataGridViewPlatform.Item(7, ro).Value & ", " & DataGridViewPlatform.Item(8, ro).Value &
                        ", " & DataGridViewPlatform.Item(9, ro).Value & ", " & DataGridViewPlatform.Item(10, ro).Value &
                        ", " & DataGridViewPlatform.Item(11, ro).Value & ", " & DataGridViewPlatform.Item(12, ro).Value &
                        "," & DataGridViewPlatform.Item(13, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#13 Crack (7 item = 0 - 6)
            For ro = 0 To DataGridViewCrack.RowCount - 1
                If DataGridViewCrack.Item(0, ro).Value <> "" And DataGridViewCrack.Item(1, ro).Value <> "" And
                    DataGridViewCrack.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "cracks, " & DataGridViewCrack.Item(0, ro).Value &
                        ", " & DataGridViewCrack.Item(1, ro).Value & ", " & DataGridViewCrack.Item(2, ro).Value &
                        ", " & DataGridViewCrack.Item(3, ro).Value & ", " & DataGridViewCrack.Item(4, ro).Value &
                        ", " & DataGridViewCrack.Item(5, ro).Value & ", " & DataGridViewCrack.Item(6, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#14 Underground (15 item = 0 - 14)
            For ro = 0 To DataGridViewUG.RowCount - 1
                If DataGridViewUG.Item(0, ro).Value <> "" And DataGridViewUG.Item(1, ro).Value <> "" And
                    DataGridViewUG.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ug, " & DataGridViewUG.Item(0, ro).Value &
                        ", " & DataGridViewUG.Item(1, ro).Value & ", " & DataGridViewUG.Item(2, ro).Value &
                        ", " & DataGridViewUG.Item(3, ro).Value & ", " & DataGridViewUG.Item(4, ro).Value &
                        ", " & DataGridViewUG.Item(5, ro).Value & ", " & DataGridViewUG.Item(6, ro).Value &
                        ", " & DataGridViewUG.Item(7, ro).Value & ", " & DataGridViewUG.Item(8, ro).Value &
                        ", " & DataGridViewUG.Item(9, ro).Value & ", " & DataGridViewUG.Item(10, ro).Value &
                        ", " & DataGridViewUG.Item(11, ro).Value & ", " & DataGridViewUG.Item(12, ro).Value &
                        ", " & DataGridViewUG.Item(13, ro).Value & ", " & DataGridViewUG.Item(14, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#15 Free objects  (6 item = 0 - 5)
            For ro = 0 To DataGridViewFreeObject.RowCount - 1
                If DataGridViewFreeObject.Item(0, ro).Value <> "" And DataGridViewFreeObject.Item(1, ro).Value <> "" And
                    DataGridViewFreeObject.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "fobj, " & DataGridViewFreeObject.Item(0, ro).Value &
                        ", " & DataGridViewFreeObject.Item(1, ro).Value & ", " & DataGridViewFreeObject.Item(2, ro).Value &
                        ", " & DataGridViewFreeObject.Item(3, ro).Value & ", " & DataGridViewFreeObject.Item(4, ro).Value &
                        ", " & DataGridViewFreeObject.Item(5, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next

            '#16 Etc (9 item = 0 - 8)
            For ro = 0 To DataGridViewEtc.RowCount - 1
                If DataGridViewEtc.Item(0, ro).Value <> "" And DataGridViewEtc.Item(1, ro).Value <> "" And
                    DataGridViewEtc.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "etc, " & DataGridViewEtc.Item(0, ro).Value &
                        ", " & DataGridViewEtc.Item(1, ro).Value & ", " & DataGridViewEtc.Item(2, ro).Value &
                        ", " & DataGridViewEtc.Item(3, ro).Value & ", " & DataGridViewEtc.Item(4, ro).Value &
                        ", " & DataGridViewEtc.Item(5, ro).Value & ", " & DataGridViewEtc.Item(6, ro).Value &
                        ", " & DataGridViewEtc.Item(7, ro).Value & ", " & DataGridViewEtc.Item(8, ro).Value
                    txt.AppendLine(ttxt)
                End If
            Next


            ' // *******************************

            Try
                File.WriteAllText(filename, txt.ToString)
                MessageBox.Show(kamus.Item("txtButtonSaveTXTSaved"),
                                kamus.Item("txtMsgInfoTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub ButtonGenerateGBMapsJS_Click(sender As Object, e As EventArgs) Handles ButtonGenerateGBMapsJS.Click
        Dim basedir = gbIdir.ToLower.Replace(" \ images", "")
        If SaveFileDialog1.InitialDirectory = "" Then SaveFileDialog1.InitialDirectory = basedir & "\script"
        SaveFileDialog1.Filter = kamus.item("txtButtonGenerateGBMapsJSfilter")
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim scriptfile = SaveFileDialog1.FileName
            Dim txt As New StringBuilder
            'txt.AppendLine("GB Maps - ギビマップ Tools,2.0.0,gauge,object title")
            txt.AppendLine("// This file is created with GB Maps - ギビマップ Tools v2.3.0. If necessary, you can create your own file reference. ")
            txt.AppendLine("// Fail ini dicipta dengan GB Maps - ギビマップ Tools v2.3.0. Jika perlu, anda boleh membuat rujukan fail anda sendiri. ")
            txt.AppendLine("var gbmdatatool = 'GB Maps - ギビマップ Tools';")
            txt.AppendLine("var gbmdataversion = '2.3.0';")

            txt.AppendLine("var ttxt ='';")

            txt.AppendLine()

            '#2 Rail (22 item = 0 - 21)
            For ro = 0 To DataGridViewRail.RowCount - 1
                If DataGridViewRail.Item(0, ro).Value <> "" And DataGridViewRail.Item(1, ro).Value <> "" And
                        DataGridViewRail.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewRail.Item(0, ro).Value &
                        "','" & DataGridViewRail.Item(1, ro).Value & "','" & DataGridViewRail.Item(2, ro).Value &
                        "','" & DataGridViewRail.Item(3, ro).Value & "','" & DataGridViewRail.Item(4, ro).Value &
                        "','" & DataGridViewRail.Item(5, ro).Value & "','" & DataGridViewRail.Item(6, ro).Value &
                        "','" & DataGridViewRail.Item(7, ro).Value & "','" & DataGridViewRail.Item(8, ro).Value &
                        "','" & DataGridViewRail.Item(9, ro).Value & "','" & DataGridViewRail.Item(10, ro).Value &
                        "','" & DataGridViewRail.Item(11, ro).Value & "','" & DataGridViewRail.Item(12, ro).Value &
                        "','" & DataGridViewRail.Item(13, ro).Value & "','" & DataGridViewRail.Item(14, ro).Value &
                        "','" & DataGridViewRail.Item(15, ro).Value & "','" & DataGridViewRail.Item(16, ro).Value &
                        "','" & DataGridViewRail.Item(17, ro).Value & "','" & DataGridViewRail.Item(18, ro).Value &
                        "','" & DataGridViewRail.Item(19, ro).Value & "','" & DataGridViewRail.Item(20, ro).Value &
                        "','" & DataGridViewRail.Item(21, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bverailobjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#3 Pole (8 item = 0 - 7)
            For ro = 0 To DataGridViewPole.RowCount - 1
                If DataGridViewPole.Item(0, ro).Value <> "" And DataGridViewPole.Item(1, ro).Value <> "" _
                    And DataGridViewPole.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewPole.Item(0, ro).Value &
                        "','" & DataGridViewPole.Item(1, ro).Value & "','" & DataGridViewPole.Item(2, ro).Value &
                        "','" & DataGridViewPole.Item(3, ro).Value & "','" & DataGridViewPole.Item(4, ro).Value &
                        "','" & DataGridViewPole.Item(5, ro).Value & "','" & DataGridViewPole.Item(6, ro).Value &
                        "','" & DataGridViewPole.Item(7, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvepoleObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#4 Train (5 item = 0 - 4)
            For ro = 0 To DataGridViewTrain.RowCount - 1
                If DataGridViewTrain.Item(0, ro).Value <> "" And DataGridViewTrain.Item(1, ro).Value <> "" And
                    DataGridViewTrain.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewTrain.Item(0, ro).Value &
                        "','" & DataGridViewTrain.Item(1, ro).Value & "','" & DataGridViewTrain.Item(2, ro).Value &
                        "','" & DataGridViewTrain.Item(3, ro).Value & "','" & DataGridViewTrain.Item(4, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvetrainDirArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#5 Sound (5 item = 0 - 4)
            For ro = 0 To DataGridViewSound.RowCount - 1
                If DataGridViewSound.Item(0, ro).Value <> "" And DataGridViewSound.Item(1, ro).Value <> "" And
                    DataGridViewSound.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewSound.Item(0, ro).Value &
                        "','" & DataGridViewSound.Item(1, ro).Value & "','" & DataGridViewSound.Item(2, ro).Value &
                        "','" & DataGridViewSound.Item(3, ro).Value & "','" & DataGridViewSound.Item(4, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveaudioObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#6 Tunnel (9 item = 0 - 8)
            For ro = 0 To DataGridViewTunnel.RowCount - 1
                If DataGridViewTunnel.Item(0, ro).Value <> "" And DataGridViewTunnel.Item(1, ro).Value <> "" And
                    DataGridViewTunnel.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewTunnel.Item(0, ro).Value &
                        "','" & DataGridViewTunnel.Item(1, ro).Value & "','" & DataGridViewTunnel.Item(2, ro).Value &
                        "','" & DataGridViewTunnel.Item(3, ro).Value & "','" & DataGridViewTunnel.Item(4, ro).Value &
                        "','" & DataGridViewTunnel.Item(5, ro).Value & "','" & DataGridViewTunnel.Item(6, ro).Value &
                        "','" & DataGridViewTunnel.Item(7, ro).Value & "','" & DataGridViewTunnel.Item(8, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvetunnelObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#7 Bridge (9 item = 0 - 8)
            For ro = 0 To DataGridViewBridge.RowCount - 1
                If DataGridViewBridge.Item(0, ro).Value <> "" And DataGridViewBridge.Item(1, ro).Value <> "" And
                    DataGridViewBridge.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewBridge.Item(0, ro).Value &
                        "','" & DataGridViewBridge.Item(1, ro).Value & "','" & DataGridViewBridge.Item(2, ro).Value &
                        "','" & DataGridViewBridge.Item(3, ro).Value & "','" & DataGridViewBridge.Item(4, ro).Value &
                        "','" & DataGridViewBridge.Item(5, ro).Value & "','" & DataGridViewBridge.Item(6, ro).Value &
                        "','" & DataGridViewBridge.Item(7, ro).Value & "','" & DataGridViewBridge.Item(8, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvebridgeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#8 Overpass (9 item = 0 - 8)
            For ro = 0 To DataGridViewOverpass.RowCount - 1
                If DataGridViewOverpass.Item(0, ro).Value <> "" And DataGridViewOverpass.Item(1, ro).Value <> "" And
                    DataGridViewOverpass.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewOverpass.Item(0, ro).Value &
                        "','" & DataGridViewOverpass.Item(1, ro).Value & "','" & DataGridViewOverpass.Item(2, ro).Value &
                        "','" & DataGridViewOverpass.Item(3, ro).Value & "','" & DataGridViewOverpass.Item(4, ro).Value &
                        "','" & DataGridViewOverpass.Item(5, ro).Value & "','" & DataGridViewOverpass.Item(6, ro).Value &
                        "','" & DataGridViewOverpass.Item(7, ro).Value & "','" & DataGridViewOverpass.Item(8, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveFOObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#9 Hillcut (7 item = 0 - 6)
            For ro = 0 To DataGridViewHillCut.RowCount - 1
                If DataGridViewHillCut.Item(0, ro).Value <> "" And DataGridViewHillCut.Item(1, ro).Value <> "" And
                    DataGridViewHillCut.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewHillCut.Item(0, ro).Value &
                        "','" & DataGridViewHillCut.Item(1, ro).Value & "','" & DataGridViewHillCut.Item(2, ro).Value &
                        "','" & DataGridViewHillCut.Item(3, ro).Value & "','" & DataGridViewHillCut.Item(4, ro).Value &
                        "','" & DataGridViewHillCut.Item(5, ro).Value & "','" & DataGridViewHillCut.Item(6, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvecutObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#10 Dike (7 item = 0 - 6)
            For ro = 0 To DataGridViewDike.RowCount - 1
                If DataGridViewDike.Item(0, ro).Value <> "" And DataGridViewDike.Item(1, ro).Value <> "" And
                    DataGridViewDike.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewDike.Item(0, ro).Value &
                        "','" & DataGridViewDike.Item(1, ro).Value & "','" & DataGridViewDike.Item(2, ro).Value &
                        "','" & DataGridViewDike.Item(3, ro).Value & "','" & DataGridViewDike.Item(4, ro).Value &
                        "','" & DataGridViewDike.Item(5, ro).Value & "','" & DataGridViewDike.Item(6, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvedikeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#11 Road Crossing (8 item = 0 - 7)
            For ro = 0 To DataGridViewRC.RowCount - 1
                If DataGridViewRC.Item(0, ro).Value <> "" And DataGridViewRC.Item(1, ro).Value <> "" And
                    DataGridViewRC.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewRC.Item(0, ro).Value &
                        "','" & DataGridViewRC.Item(1, ro).Value & "','" & DataGridViewRC.Item(2, ro).Value &
                        "','" & DataGridViewRC.Item(3, ro).Value & "','" & DataGridViewRC.Item(4, ro).Value &
                        "','" & DataGridViewRC.Item(5, ro).Value & "','" & DataGridViewRC.Item(6, ro).Value &
                        "','" & DataGridViewRC.Item(7, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveRCObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#12 Platform (14 item = 0 - 13)
            For ro = 0 To DataGridViewPlatform.RowCount - 1
                If DataGridViewPlatform.Item(0, ro).Value <> "" And DataGridViewPlatform.Item(1, ro).Value <> "" And
                    DataGridViewPlatform.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewPlatform.Item(0, ro).Value &
                        "','" & DataGridViewPlatform.Item(1, ro).Value & "','" & DataGridViewPlatform.Item(2, ro).Value &
                        "','" & DataGridViewPlatform.Item(3, ro).Value & "','" & DataGridViewPlatform.Item(4, ro).Value &
                        "','" & DataGridViewPlatform.Item(5, ro).Value & "','" & DataGridViewPlatform.Item(6, ro).Value &
                        "','" & DataGridViewPlatform.Item(7, ro).Value & "','" & DataGridViewPlatform.Item(8, ro).Value &
                        "','" & DataGridViewPlatform.Item(9, ro).Value & "','" & DataGridViewPlatform.Item(10, ro).Value &
                        "','" & DataGridViewPlatform.Item(11, ro).Value & "','" & DataGridViewPlatform.Item(12, ro).Value &
                        "','" & DataGridViewPlatform.Item(13, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveplatformObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#13 Crack (7 item = 0 - 6)
            For ro = 0 To DataGridViewCrack.RowCount - 1
                If DataGridViewCrack.Item(0, ro).Value <> "" And DataGridViewCrack.Item(1, ro).Value <> "" And
                    DataGridViewCrack.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewCrack.Item(0, ro).Value &
                        "','" & DataGridViewCrack.Item(1, ro).Value & "','" & DataGridViewCrack.Item(2, ro).Value &
                        "','" & DataGridViewCrack.Item(3, ro).Value & "','" & DataGridViewCrack.Item(4, ro).Value &
                        "','" & DataGridViewCrack.Item(5, ro).Value & "','" & DataGridViewCrack.Item(6, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvecrackObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#14 UG (15 item = 0 - 14)
            For ro = 0 To DataGridViewUG.RowCount - 1
                If DataGridViewUG.Item(0, ro).Value <> "" And DataGridViewUG.Item(1, ro).Value <> "" And
                    DataGridViewUG.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewUG.Item(0, ro).Value &
                        "','" & DataGridViewUG.Item(1, ro).Value & "','" & DataGridViewUG.Item(2, ro).Value &
                        "','" & DataGridViewUG.Item(3, ro).Value & "','" & DataGridViewUG.Item(4, ro).Value &
                        "','" & DataGridViewUG.Item(5, ro).Value & "','" & DataGridViewUG.Item(6, ro).Value &
                        "','" & DataGridViewUG.Item(7, ro).Value & "','" & DataGridViewUG.Item(8, ro).Value &
                        "','" & DataGridViewUG.Item(9, ro).Value & "','" & DataGridViewUG.Item(10, ro).Value &
                        "','" & DataGridViewUG.Item(11, ro).Value & "','" & DataGridViewUG.Item(12, ro).Value &
                        "','" & DataGridViewUG.Item(13, ro).Value & "','" & DataGridViewUG.Item(14, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bveUGObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            '#15 Free objects  (6 item = 0 - 5)
            For ro = 0 To DataGridViewFreeObject.RowCount - 1
                If DataGridViewFreeObject.Item(0, ro).Value <> "" And DataGridViewFreeObject.Item(1, ro).Value <> "" And
                    DataGridViewFreeObject.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewFreeObject.Item(0, ro).Value &
                        "','" & DataGridViewFreeObject.Item(1, ro).Value & "','" & DataGridViewFreeObject.Item(2, ro).Value &
                        "','" & DataGridViewFreeObject.Item(3, ro).Value & "','" & DataGridViewFreeObject.Item(4, ro).Value &
                        "','" & DataGridViewFreeObject.Item(5, ro).Value & "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvefreeObjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")

                End If
            Next

            '#16 Etc (9 item = 0 - 8)
            For ro = 0 To DataGridViewEtc.RowCount - 1
                If DataGridViewEtc.Item(0, ro).Value <> "" And DataGridViewEtc.Item(1, ro).Value <> "" And
                    DataGridViewEtc.Item(2, ro).Value <> "" Then
                    Dim ttxt As String = "ttxt = ['" & DataGridViewEtc.Item(0, ro).Value &
                        "','" & DataGridViewEtc.Item(1, ro).Value & "','" & DataGridViewEtc.Item(2, ro).Value &
                        "','" & DataGridViewEtc.Item(3, ro).Value & "','" & DataGridViewEtc.Item(4, ro).Value &
                        "','" & DataGridViewEtc.Item(5, ro).Value & "','" & DataGridViewEtc.Item(6, ro).Value &
                        "','" & DataGridViewEtc.Item(7, ro).Value & "','" & DataGridViewEtc.Item(8, ro).Value &
                        "'];"
                    ttxt = ttxt.Replace("\", "/")
                    txt.AppendLine(ttxt)
                    txt.AppendLine("bvebveStrOjArr.push(ttxt);")
                    txt.AppendLine("ttxt = [];")
                End If
            Next

            Try
                File.WriteAllText(scriptfile, txt.ToString)
                MessageBox.Show(kamus.Item("txtButtonGenerateGBMapsJSSaved"), kamus.item("txtMsgInfoTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
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

    Private Sub PictureBoxPlatformBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxPlatformBVESyntax.Click
        FormPlatformBVESyntax.Show()
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
            textBoxUGGroundLeft.Text = ""
            textBoxUGGroundRight.Text = ""
            textBoxUGoWallLeft.Text = ""
            textBoxUGoWallRight.Text = ""
            textBoxUGEntrance.Text = ""
            textBoxUGiWallLeft.Text = ""
            textBoxUGiWallRight.Text = ""
            textBoxUGExit.Text = ""

        End If
    End Sub

    Private Sub ButtonCrackTip_Click(sender As Object, e As EventArgs) Handles ButtonCrackTip.Click
        FormCrackTip.Show()
    End Sub

    Private Sub ButtonUGGroundTip_Click(sender As Object, e As EventArgs) Handles ButtonUGGroundTip.Click
        FormUGsplitground.Show()
    End Sub

    Private Sub ButtonUGoWallTip_Click(sender As Object, e As EventArgs) Handles ButtonUGoWallTip.Click
        FormUGoWallTip.Show()
    End Sub

    Private Sub ButtonUGentranceTip_Click(sender As Object, e As EventArgs) Handles ButtonUGentranceTip.Click
        FormUGEntrance.Show()
    End Sub

    Private Sub ButtonUGExitTip_Click(sender As Object, e As EventArgs) Handles ButtonUGExitTip.Click
        FormUGExit.Show()
    End Sub

    Private Sub ButtonUGiWallTip_Click(sender As Object, e As EventArgs) Handles ButtonUGiWallTip.Click
        FormUGiWallTip.Show()
    End Sub

    Private Sub PictureBoxUGbveSyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxUGbveSyntax.Click
        FormUGbveSyntax.Show()
    End Sub

    Private Sub ButtonBrowseBVEDirTip_Click(sender As Object, e As EventArgs) Handles ButtonBrowseBVEDirTip.Click
        DialogBrowseBVEDirHelp.Show()
    End Sub

    Private Sub PictureBoxRailBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxRailBVESyntax.Click
        FormRailBVESyntaxEx.Show()
    End Sub

    Private Sub buttonBrowsePoleStructureRight_Click(sender As Object, e As EventArgs) Handles buttonBrowsePoleStructureRight.Click
        UpdateXFileField(textBoxPoleStructureRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseOverHeadWire_Click(sender As Object, e As EventArgs) Handles buttonBrowseOverHeadWire.Click
        UpdateXFileField(textBoxOverHeadWire, filetype.x, Nothing)
    End Sub

    Private Sub ButtonTrainFolderTip_Click(sender As Object, e As EventArgs) Handles ButtonTrainFolderTip.Click
        FormTrainDirTip.Show()
    End Sub

    Private Sub ButtonSoundTip_Click(sender As Object, e As EventArgs) Handles ButtonSoundTip.Click
        FormSoundTip.Show()
    End Sub

    Private Sub ButtonHillCutTip_Click(sender As Object, e As EventArgs) Handles ButtonHillCutTip.Click
        FormHillCutTip.Show()
    End Sub

    Private Sub buttonBrowseUGImage_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGImage.Click
        UpdateXFileField(textBoxUGImage, filetype.img, PictureBoxUG)
    End Sub

    Private Sub buttonBrowseUGGroundLeft_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGGroundLeft.Click
        UpdateXFileField(textBoxUGGroundLeft, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGGroundRight_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGGroundRight.Click
        UpdateXFileField(textBoxUGGroundRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGoWallLeft_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGoWallLeft.Click
        UpdateXFileField(textBoxUGoWallLeft, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGoWallRight_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGoWallRight.Click
        UpdateXFileField(textBoxUGoWallRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGEntrance_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGEntrance.Click
        UpdateXFileField(textBoxUGEntrance, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGiWallLeft_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGiWallLeft.Click
        UpdateXFileField(textBoxUGiWallLeft, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGiWallRight_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGiWallRight.Click
        UpdateXFileField(textBoxUGiWallRight, filetype.x, Nothing)
    End Sub

    Private Sub buttonBrowseUGExit_Click(sender As Object, e As EventArgs) Handles buttonBrowseUGExit.Click
        UpdateXFileField(textBoxUGExit, filetype.x, Nothing)
    End Sub

    Private Sub ButtonFreeObjectTip_Click(sender As Object, e As EventArgs) Handles ButtonFreeObjectTip.Click
        FormFreeObjectTip.Show()
    End Sub

    Private Sub PictureBoxFreeObjectSyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxFreeObjectSyntax.Click
        FormFreeObjectSyntax.Show()
    End Sub

    Private Sub PictureBoxetcBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxetcBVESyntax.Click
        FormEtcBVESyntax.Show()
    End Sub

    Private Sub ButtonEtcBVEImage_Click(sender As Object, e As EventArgs) Handles ButtonEtcBVEImage.Click
        FormetcBVEtip.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        '# generate controls text list
        Dim xdata As XElement = <list></list>

        For Each teks In kamus
            xdata.Add(<control>
                          <name><%= teks.Key %></name>
                          <type><%= "kamus" %></type>
                          <text><%= teks.Value %></text>
                      </control>)
        Next

        For Each t As Type In Me.GetType().Assembly.GetTypes()
            If t.BaseType.Name = "Form" Then
                Dim fom = CType(Activator.CreateInstance(t), Form)
                If fom.Name <> "Main" Then
                    xdata.Add(<control>
                                  <name><%= fom.Name %></name>
                                  <type><%= "Form" %></type>
                                  <text><%= fom.Text %></text>
                              </control>)
                End If
            End If
        Next

        For Each page As Control In TabControl1.Controls
            xdata.Add(<control>
                          <name><%= page.Name %></name>
                          <type><%= TypeName(page) %></type>
                          <text><%= page.Text %></text>
                      </control>)
        Next

        '#metod3
        For Each ctrl In GetAll(TabControl1, GetType(Label))
            If ctrl IsNot Nothing Then
                If ctrl.Name.Contains("About") Then
                    Continue For
                End If
                If ctrl.Name = "LinkLabel1" Then
                    Continue For
                End If
                xdata.Add(<control>
                              <name><%= ctrl.Name %></name>
                              <type><%= TypeName(ctrl) %></type>
                              <text><%= ctrl.Text %></text>
                          </control>)
            End If
        Next

        For Each ctrl In GetAll(TabControl1, GetType(Button))
            If ctrl IsNot Nothing Then
                If ctrl.Name.Contains("About") Then
                    Continue For
                End If
                If ctrl.Name = "LinkLabel1" Then
                    Continue For
                End If
                xdata.Add(<control>
                              <name><%= ctrl.Name %></name>
                              <type><%= TypeName(ctrl) %></type>
                              <text><%= ctrl.Text %></text>
                          </control>)
            End If
        Next

        For Each ctrl In GetAll(TabControl1, GetType(GroupBox))
            If ctrl IsNot Nothing Then
                If ctrl.Name.Contains("About") Then
                    Continue For
                End If
                If ctrl.Name = "LinkLabel1" Then
                    Continue For
                End If
                xdata.Add(<control>
                              <name><%= ctrl.Name %></name>
                              <type><%= TypeName(ctrl) %></type>
                              <text><%= ctrl.Text %></text>
                          </control>)
            End If
        Next

        Try
            xdata.Save("controls_text.xml")
            MessageBox.Show("Control text list saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, kamus.Item("txtMsgWarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ButtonBrowseImgDirTip_Click(sender As Object, e As EventArgs) Handles ButtonBrowseImgDirTip.Click
        DialogBrowseGBMapsImageDirHelp.Show()
    End Sub

    Public Sub UpdateXFileField(TB As TextBox, file As filetype, PB As PictureBox)
        If bvedir Is Nothing And file = filetype.x Then
            MessageBox.Show(kamus.Item("txtUpdateXFileFieldErrorbvedir"),
                            kamus.Item("txtMsgWarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If gbIdir Is Nothing And file = filetype.img Then
            MessageBox.Show(kamus.Item("txtUpdateXFileFieldErrorimgdir"),
                            kamus.Item("txtMsgWarningTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                    .Filter = kamus.item("txtUpdateXFileFieldFilterX")
                    If IO.Directory.Exists(currDir) Then
                        .InitialDirectory = currDir
                    Else
                        .InitialDirectory = bvedir
                    End If
                Case filetype.img
                    .Filter = kamus.Item("txtUpdateXFileFieldFilterImg")
                    If IO.Directory.Exists(gbIdir) Then .InitialDirectory = gbIdir
                Case filetype.wav
                    .Filter = kamus.Item("txtUpdateXFileFieldFilterSnd")
                Case Else
                    .Filter = kamus.Item("txtUpdateXFileFieldFilterAll")
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

    Private Sub textBoxBVEdataDir_TextChanged(sender As Object, e As EventArgs) Handles textBoxBVEdataDir.TextChanged
        saved = False
    End Sub

    Private Sub textBoxGBimgDir_TextChanged(sender As Object, e As EventArgs) Handles textBoxGBimgDir.TextChanged
        saved = False
    End Sub

    Private Sub ComboBoxLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxLanguage.SelectedIndexChanged
        If File.Exists("lang.xml") = True And onStartup = False Then

            'Dim frmList As New List(Of Form)
            'For Each t As Type In Me.GetType().Assembly.GetTypes()
            '    If t.BaseType.Name = "Form" Then
            '        Dim fom = CType(Activator.CreateInstance(t), Form)
            '        If fom.Name <> "Main" Then
            '            frmList.Add(fom)
            '        End If
            '    End If
            'Next

            'Try
            Dim xLfile As XDocument = XDocument.Load("lang.xml")
            For Each lang In From element In xLfile.<language>.<lang>
                If lang.@name.Replace("_", " ") = ComboBoxLanguage.SelectedItem Then

                    Dim xUpdLang = XElement.Load(lang.@file)

                    For Each kontrol In From element In xUpdLang.Elements
                        If kontrol.<type>.Value = "kamus" Then
                            kamus(kontrol.<name>.Value) = kontrol.<text>.Value

                        ElseIf kontrol.<type>.Value = "Form" Then
                            'For Each fom In frmList
                            '    If fom.Name = kontrol.<name>.Value Then
                            '        'fom.Text = kontrol.<text>.Value
                            '        DirectCast(fom, Form).Text = kontrol.<text>.Value

                            '        'MsgBox(fom.Name & "(" & fom.Name.Length &
                            '        '       ")  <|>  " & kontrol.<name>.Value &
                            '        '       "(" & kontrol.<name>.Value.Length &
                            '        '       ") = " & vbCrLf & (fom.Name = kontrol.<name>.Value))
                            '        'MsgBox(fom.Text)
                            '        Exit For
                            '    End If
                            'Next
                            Select Case True
                                Case DialogBrowseBVEDirHelp.Name = kontrol.<name>.Value
                                    DialogBrowseBVEDirHelp.Text = kontrol.<text>.Value
                                Case DialogBrowseGBMapsImageDirHelp.Name = kontrol.<name>.Value
                                    DialogBrowseGBMapsImageDirHelp.Text = kontrol.<text>.Value
                                Case FormBridgeBVESyntax.Name = kontrol.<name>.Value
                                    FormBridgeBVESyntax.Text = kontrol.<text>.Value
                                Case FormBridgeTip.Name = kontrol.<name>.Value
                                    FormBridgeTip.Text = kontrol.<text>.Value
                                Case FormBVESoundSyntax.Name = kontrol.<name>.Value
                                    FormBVESoundSyntax.Text = kontrol.<text>.Value
                                Case FormBVETrainSyntax.Name = kontrol.<name>.Value
                                    FormBVETrainSyntax.Text = kontrol.<text>.Value
                                Case FormCrackTip.Name = kontrol.<name>.Value
                                    FormCrackTip.Text = kontrol.<text>.Value
                                Case FormDikeBVEsyntax.Name = kontrol.<name>.Value
                                    FormDikeBVEsyntax.Text = kontrol.<text>.Value
                                Case FormDikeTip.Name = kontrol.<name>.Value
                                    FormDikeTip.Text = kontrol.<text>.Value
                                Case FormEtcBVESyntax.Name = kontrol.<name>.Value
                                    FormEtcBVESyntax.Text = kontrol.<text>.Value
                                Case FormetcBVEtip.Name = kontrol.<name>.Value
                                    FormetcBVEtip.Text = kontrol.<text>.Value
                                Case FormFreeObjectSyntax.Name = kontrol.<name>.Value
                                    FormFreeObjectSyntax.Text = kontrol.<text>.Value
                                Case FormFreeObjectTip.Name = kontrol.<name>.Value
                                    FormFreeObjectTip.Text = kontrol.<text>.Value
                                Case FormHillCutBVESyntax.Name = kontrol.<name>.Value
                                    FormHillCutBVESyntax.Text = kontrol.<text>.Value
                                Case FormHillCutTip.Name = kontrol.<name>.Value
                                    FormHillCutTip.Text = kontrol.<text>.Value
                                Case FormOverpassBVESyntax.Name = kontrol.<name>.Value
                                    FormOverpassBVESyntax.Text = kontrol.<text>.Value
                                Case FormOverpassTip.Name = kontrol.<name>.Value
                                    FormOverpassTip.Text = kontrol.<text>.Value
                                Case FormPlatformBVESyntax.Name = kontrol.<name>.Value
                                    FormPlatformBVESyntax.Text = kontrol.<text>.Value
                                Case FormPlatformTip.Name = kontrol.<name>.Value
                                    FormPlatformTip.Text = kontrol.<text>.Value
                                Case FormPoleBVESyntax.Name = kontrol.<name>.Value
                                    FormPoleBVESyntax.Text = kontrol.<text>.Value
                                Case FormPoleTip.Name = kontrol.<name>.Value
                                    FormPoleTip.Text = kontrol.<text>.Value
                                Case FormRailBVESyntaxEx.Name = kontrol.<name>.Value
                                    FormRailBVESyntaxEx.Text = kontrol.<text>.Value
                                Case FormRailPicHelp.Name = kontrol.<name>.Value
                                    FormRailPicHelp.Text = kontrol.<text>.Value
                                Case FormRCBVEsyntax.Name = kontrol.<name>.Value
                                    FormRCBVEsyntax.Text = kontrol.<text>.Value
                                Case FormRCTip.Name = kontrol.<name>.Value
                                    FormRCTip.Text = kontrol.<text>.Value
                                Case FormSoundTip.Name = kontrol.<name>.Value
                                    FormSoundTip.Text = kontrol.<text>.Value
                                Case FormTrainDirTip.Name = kontrol.<name>.Value
                                    FormTrainDirTip.Text = kontrol.<text>.Value
                                Case FormTunnelBVESyntax.Name = kontrol.<name>.Value
                                    FormTunnelBVESyntax.Text = kontrol.<text>.Value
                                Case FormTunnelTip.Name = kontrol.<name>.Value
                                    FormTunnelTip.Text = kontrol.<text>.Value
                                Case FormCrackBVESyntax.Name = kontrol.<name>.Value
                                    FormCrackBVESyntax.Text = kontrol.<text>.Value
                                Case FormUGbveSyntax.Name = kontrol.<name>.Value
                                    FormUGbveSyntax.Text = kontrol.<text>.Value
                                Case FormUGEntrance.Name = kontrol.<name>.Value
                                    FormUGEntrance.Text = kontrol.<text>.Value
                                Case FormUGExit.Name = kontrol.<name>.Value
                                    FormUGExit.Text = kontrol.<text>.Value
                                Case FormUGiWallTip.Name = kontrol.<name>.Value
                                    FormUGiWallTip.Text = kontrol.<text>.Value
                                Case FormUGsplitground.Name = kontrol.<name>.Value
                                    FormUGsplitground.Text = kontrol.<text>.Value
                                Case FormUGoWallTip.Name = kontrol.<name>.Value
                                    FormUGoWallTip.Text = kontrol.<text>.Value
                            End Select
                        Else
                            DirectCast(TabControl1.Controls.Find(kontrol.<name>.Value, True)(0), Control).Text =
                                            kontrol.<text>.Value
                        End If

                    Next

                    lang.@select = "true"
                Else
                    If lang.@select = "true" Then lang.@select = "false"
                End If
            Next

            xLfile.Save("lang.xml")
                    'Catch ex As Exception
                    '    MessageBox.Show(ex.Message)
                    'End Try
                End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If saved = False And TabControl1.SelectedIndex <> 0 Then
            Dim notexist = ""
            If Directory.Exists(textBoxBVEdataDir.Text) = False Then
                notexist &= kamus.Item("txtTabControl1Errorbvedir") & vbCrLf
            End If
            If Directory.Exists(textBoxGBimgDir.Text) = False Then
                notexist &= kamus.Item("txtTabControl1Errorimgdir")
            End If
            If notexist <> "" Then
                MessageBox.Show(notexist, kamus.item("txtMsgErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedTab = Step_1
                If Directory.Exists(textBoxBVEdataDir.Text) = False Then
                    textBoxBVEdataDir.Focus()
                Else
                    textBoxGBimgDir.Focus()
                End If
                Exit Sub
            End If

            Try
                bvedir = textBoxBVEdataDir.Text
                gbIdir = textBoxGBimgDir.Text
                Dim xCfile As XElement =
                        <dir>
                            <bve><%= textBoxBVEdataDir.Text %></bve>
                            <gbimg><%= textBoxGBimgDir.Text %></gbimg>
                        </dir>
                xCfile.Save("5config.xml")
                MessageBox.Show(kamus.Item("txtTabControl1RefSaved"), kamus.Item("txtMsgInfoTitle"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                saved = True

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub PictureBoxTrainBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxTrainBVESyntax.Click
        FormBVETrainSyntax.Show()
    End Sub

    Private Sub PictureBoxSoundBVESyntax_Click(sender As Object, e As EventArgs) Handles PictureBoxSoundBVESyntax.Click
        FormBVESoundSyntax.Show()
    End Sub

    Private Sub TabControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles TabControl1.KeyDown
        If e.KeyCode = Keys.F9 AndAlso e.Modifiers = Keys.Control Then
            If LinkLabel1.Visible = True Then
                LinkLabel1.Visible = False
            Else
                LinkLabel1.Visible = True
            End If
        End If
    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        onStartup = False
        If ComboBoxLanguage.SelectedIndex <> 0 Then
            ComboBoxLanguage_SelectedIndexChanged(sender, New System.EventArgs())
        End If
    End Sub

    Public Function GetAll(control As Control, type As Type) As IEnumerable(Of Control)
        '# original code by PsychoCoder [http://stackoverflow.com/questions/3419159/how-to-get-all-child-controls-of-a-windows-forms-form-of-a-specific-type-button]
        Dim controls = control.Controls.Cast(Of Control)()

        Return controls.SelectMany(Function(ctrl) GetAll(ctrl, type)).Concat(controls).Where(Function(c) c.[GetType]() = type)
    End Function

End Class
