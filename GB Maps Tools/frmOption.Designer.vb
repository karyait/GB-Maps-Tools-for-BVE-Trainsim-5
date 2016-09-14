<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxBVEdir = New System.Windows.Forms.TextBox()
        Me.TextBoxGBImg = New System.Windows.Forms.TextBox()
        Me.ButtonBVEfile = New System.Windows.Forms.Button()
        Me.ButtonGBImg = New System.Windows.Forms.Button()
        Me.ButtonKO = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonGBImg)
        Me.GroupBox1.Controls.Add(Me.ButtonBVEfile)
        Me.GroupBox1.Controls.Add(Me.TextBoxGBImg)
        Me.GroupBox1.Controls.Add(Me.TextBoxBVEdir)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(438, 131)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(294, 153)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "BVE data folder :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "GB Maps Image Dir:"
        '
        'TextBoxBVEdir
        '
        Me.TextBoxBVEdir.Location = New System.Drawing.Point(114, 17)
        Me.TextBoxBVEdir.Name = "TextBoxBVEdir"
        Me.TextBoxBVEdir.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxBVEdir.TabIndex = 2
        '
        'TextBoxGBImg
        '
        Me.TextBoxGBImg.Location = New System.Drawing.Point(114, 43)
        Me.TextBoxGBImg.Name = "TextBoxGBImg"
        Me.TextBoxGBImg.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxGBImg.TabIndex = 3
        '
        'ButtonBVEfile
        '
        Me.ButtonBVEfile.Image = CType(resources.GetObject("ButtonBVEfile.Image"), System.Drawing.Image)
        Me.ButtonBVEfile.Location = New System.Drawing.Point(409, 15)
        Me.ButtonBVEfile.Name = "ButtonBVEfile"
        Me.ButtonBVEfile.Size = New System.Drawing.Size(23, 23)
        Me.ButtonBVEfile.TabIndex = 19
        Me.ButtonBVEfile.UseVisualStyleBackColor = True
        '
        'ButtonGBImg
        '
        Me.ButtonGBImg.Image = CType(resources.GetObject("ButtonGBImg.Image"), System.Drawing.Image)
        Me.ButtonGBImg.Location = New System.Drawing.Point(409, 41)
        Me.ButtonGBImg.Name = "ButtonGBImg"
        Me.ButtonGBImg.Size = New System.Drawing.Size(23, 23)
        Me.ButtonGBImg.TabIndex = 20
        Me.ButtonGBImg.UseVisualStyleBackColor = True
        '
        'ButtonKO
        '
        Me.ButtonKO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonKO.Location = New System.Drawing.Point(375, 153)
        Me.ButtonKO.Name = "ButtonKO"
        Me.ButtonKO.Size = New System.Drawing.Size(75, 23)
        Me.ButtonKO.TabIndex = 2
        Me.ButtonKO.Text = "Cancel"
        Me.ButtonKO.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 186)
        Me.Controls.Add(Me.ButtonKO)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOption"
        Me.Text = "Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents TextBoxGBImg As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxBVEdir As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonGBImg As System.Windows.Forms.Button
    Friend WithEvents ButtonBVEfile As System.Windows.Forms.Button
    Friend WithEvents ButtonKO As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
