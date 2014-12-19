<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormGraph
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGraph))
        Me.ButtonDataSS = New System.Windows.Forms.Button()
        Me.ButtonGraphicSave = New System.Windows.Forms.Button()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SaveFileDialogDATA = New System.Windows.Forms.SaveFileDialog()
        Me.SaveFileDialogGRAPH = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonDataSS
        '
        Me.ButtonDataSS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDataSS.Location = New System.Drawing.Point(167, 14)
        Me.ButtonDataSS.Name = "ButtonDataSS"
        Me.ButtonDataSS.Size = New System.Drawing.Size(143, 59)
        Me.ButtonDataSS.TabIndex = 19
        Me.ButtonDataSS.Text = "Save Data Plot"
        Me.ButtonDataSS.UseVisualStyleBackColor = True
        '
        'ButtonGraphicSave
        '
        Me.ButtonGraphicSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonGraphicSave.Location = New System.Drawing.Point(7, 14)
        Me.ButtonGraphicSave.Name = "ButtonGraphicSave"
        Me.ButtonGraphicSave.Size = New System.Drawing.Size(143, 59)
        Me.ButtonGraphicSave.TabIndex = 18
        Me.ButtonGraphicSave.Text = "Save Plot"
        Me.ButtonGraphicSave.UseVisualStyleBackColor = True
        '
        'ButtonExit
        '
        Me.ButtonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonExit.Location = New System.Drawing.Point(6, 14)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(161, 59)
        Me.ButtonExit.TabIndex = 20
        Me.ButtonExit.Text = "Exit"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonDataSS)
        Me.GroupBox1.Controls.Add(Me.ButtonGraphicSave)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(317, 79)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ButtonExit)
        Me.GroupBox2.Location = New System.Drawing.Point(410, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(175, 78)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        '
        'SaveFileDialogDATA
        '
        Me.SaveFileDialogDATA.DefaultExt = "txt"
        Me.SaveFileDialogDATA.Filter = "Text files|*.txt"
        '
        'SaveFileDialogGRAPH
        '
        Me.SaveFileDialogGRAPH.DefaultExt = "JPEG"
        Me.SaveFileDialogGRAPH.Filter = "JPEG files|*.jpeg"
        '
        'FormGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(600, 95)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormGraph"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Options for Simulation"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonDataSS As System.Windows.Forms.Button
    Friend WithEvents ButtonGraphicSave As System.Windows.Forms.Button
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SaveFileDialogDATA As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SaveFileDialogGRAPH As System.Windows.Forms.SaveFileDialog
End Class
