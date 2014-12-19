<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormResultLT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormResultLT))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonPrint = New System.Windows.Forms.Button()
        Me.LvResults = New System.Windows.Forms.ListView()
        Me.LabelNTrans = New System.Windows.Forms.Label()
        Me.GroupBoxComand = New System.Windows.Forms.GroupBox()
        Me.ButtonSalvar = New System.Windows.Forms.Button()
        Me.ButtonSair = New System.Windows.Forms.Button()
        Me.PrintDialogDATA = New System.Windows.Forms.PrintDialog()
        Me.PrintDocumentDATA = New System.Drawing.Printing.PrintDocument()
        Me.SaveFileDialogDATA = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxComand.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ButtonPrint)
        Me.GroupBox1.Controls.Add(Me.LvResults)
        Me.GroupBox1.Controls.Add(Me.LabelNTrans)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(988, 306)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        '
        'ButtonPrint
        '
        Me.ButtonPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrint.Location = New System.Drawing.Point(799, 263)
        Me.ButtonPrint.Name = "ButtonPrint"
        Me.ButtonPrint.Size = New System.Drawing.Size(167, 37)
        Me.ButtonPrint.TabIndex = 14
        Me.ButtonPrint.Text = "Print"
        Me.ButtonPrint.UseVisualStyleBackColor = True
        '
        'LvResults
        '
        Me.LvResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.LvResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LvResults.FullRowSelect = True
        Me.LvResults.GridLines = True
        Me.LvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.LvResults.Location = New System.Drawing.Point(14, 19)
        Me.LvResults.Name = "LvResults"
        Me.LvResults.Size = New System.Drawing.Size(952, 236)
        Me.LvResults.TabIndex = 21
        Me.LvResults.UseCompatibleStateImageBehavior = False
        '
        'LabelNTrans
        '
        Me.LabelNTrans.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.LabelNTrans.AutoSize = True
        Me.LabelNTrans.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNTrans.Location = New System.Drawing.Point(11, 263)
        Me.LabelNTrans.Name = "LabelNTrans"
        Me.LabelNTrans.Size = New System.Drawing.Size(55, 16)
        Me.LabelNTrans.TabIndex = 20
        Me.LabelNTrans.Text = "Label1"
        '
        'GroupBoxComand
        '
        Me.GroupBoxComand.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBoxComand.Controls.Add(Me.ButtonSalvar)
        Me.GroupBoxComand.Location = New System.Drawing.Point(-3, 324)
        Me.GroupBoxComand.Name = "GroupBoxComand"
        Me.GroupBoxComand.Size = New System.Drawing.Size(210, 98)
        Me.GroupBoxComand.TabIndex = 31
        Me.GroupBoxComand.TabStop = False
        '
        'ButtonSalvar
        '
        Me.ButtonSalvar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSalvar.Location = New System.Drawing.Point(21, 19)
        Me.ButtonSalvar.Name = "ButtonSalvar"
        Me.ButtonSalvar.Size = New System.Drawing.Size(167, 69)
        Me.ButtonSalvar.TabIndex = 26
        Me.ButtonSalvar.Text = "Save"
        Me.ButtonSalvar.UseVisualStyleBackColor = True
        '
        'ButtonSair
        '
        Me.ButtonSair.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonSair.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSair.Location = New System.Drawing.Point(818, 362)
        Me.ButtonSair.Name = "ButtonSair"
        Me.ButtonSair.Size = New System.Drawing.Size(167, 55)
        Me.ButtonSair.TabIndex = 30
        Me.ButtonSair.Text = "Exit"
        Me.ButtonSair.UseVisualStyleBackColor = True
        '
        'PrintDialogDATA
        '
        Me.PrintDialogDATA.UseEXDialog = True
        '
        'PrintDocumentDATA
        '
        '
        'SaveFileDialogDATA
        '
        Me.SaveFileDialogDATA.DefaultExt = "txt"
        Me.SaveFileDialogDATA.Filter = "Text files|*.txt"
        '
        'FormResultLT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1014, 429)
        Me.Controls.Add(Me.GroupBoxComand)
        Me.Controls.Add(Me.ButtonSair)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormResultLT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxComand.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPrint As System.Windows.Forms.Button
    Friend WithEvents LvResults As System.Windows.Forms.ListView
    Friend WithEvents LabelNTrans As System.Windows.Forms.Label
    Friend WithEvents GroupBoxComand As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonSalvar As System.Windows.Forms.Button
    Friend WithEvents ButtonSair As System.Windows.Forms.Button
    Friend WithEvents PrintDialogDATA As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocumentDATA As System.Drawing.Printing.PrintDocument
    Friend WithEvents SaveFileDialogDATA As System.Windows.Forms.SaveFileDialog
End Class
