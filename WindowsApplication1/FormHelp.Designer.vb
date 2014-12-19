<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHelp
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
        Me.WBHelp = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'WBHelp
        '
        Me.WBHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WBHelp.Location = New System.Drawing.Point(0, 0)
        Me.WBHelp.MinimumSize = New System.Drawing.Size(23, 20)
        Me.WBHelp.Name = "WBHelp"
        Me.WBHelp.Size = New System.Drawing.Size(331, 262)
        Me.WBHelp.TabIndex = 0
        '
        'FormHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 262)
        Me.Controls.Add(Me.WBHelp)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormHelp"
        Me.Text = "ASAS Manual"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WBHelp As System.Windows.Forms.WebBrowser
End Class
