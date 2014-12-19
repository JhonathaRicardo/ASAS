<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormResultPT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormResultPT))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonPrint = New System.Windows.Forms.Button()
        Me.LvResults = New System.Windows.Forms.ListView()
        Me.LabelNTrans = New System.Windows.Forms.Label()
        Me.ButtonSalvar = New System.Windows.Forms.Button()
        Me.ButtonSair = New System.Windows.Forms.Button()
        Me.GroupBoxComand = New System.Windows.Forms.GroupBox()
        Me.ButtonPlot = New System.Windows.Forms.Button()
        Me.PrintDialogDATA = New System.Windows.Forms.PrintDialog()
        Me.SaveFileDialogDATA = New System.Windows.Forms.SaveFileDialog()
        Me.PrintDocumentDATA = New System.Drawing.Printing.PrintDocument()
        Me.GroupBoxDA = New System.Windows.Forms.GroupBox()
        Me.GroupBoxWaveAD = New System.Windows.Forms.GroupBox()
        Me.RadioButtonAirAD = New System.Windows.Forms.RadioButton()
        Me.RadioButtonVacAD = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDA2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDA1 = New System.Windows.Forms.RadioButton()
        Me.GroupBoxSS = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LabelnumLI = New System.Windows.Forms.Label()
        Me.LabelnumLS = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonAirSS = New System.Windows.Forms.RadioButton()
        Me.RadioButtonVacSS = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSS2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSS1 = New System.Windows.Forms.RadioButton()
        Me.Labelunit = New System.Windows.Forms.Label()
        Me.LabelFWHM = New System.Windows.Forms.Label()
        Me.TextBoxFWHM = New System.Windows.Forms.TextBox()
        Me.ButtonPlotSS = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBoxComand.SuspendLayout()
        Me.GroupBoxDA.SuspendLayout()
        Me.GroupBoxWaveAD.SuspendLayout()
        Me.GroupBoxSS.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ButtonPrint)
        Me.GroupBox1.Controls.Add(Me.LvResults)
        Me.GroupBox1.Controls.Add(Me.LabelNTrans)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1010, 390)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'ButtonPrint
        '
        Me.ButtonPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPrint.Location = New System.Drawing.Point(816, 342)
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
        Me.LvResults.Location = New System.Drawing.Point(34, 21)
        Me.LvResults.Name = "LvResults"
        Me.LvResults.Size = New System.Drawing.Size(945, 300)
        Me.LvResults.TabIndex = 21
        Me.LvResults.UseCompatibleStateImageBehavior = False
        '
        'LabelNTrans
        '
        Me.LabelNTrans.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.LabelNTrans.AutoSize = True
        Me.LabelNTrans.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNTrans.Location = New System.Drawing.Point(50, 342)
        Me.LabelNTrans.Name = "LabelNTrans"
        Me.LabelNTrans.Size = New System.Drawing.Size(55, 16)
        Me.LabelNTrans.TabIndex = 20
        Me.LabelNTrans.Text = "Label1"
        '
        'ButtonSalvar
        '
        Me.ButtonSalvar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSalvar.Location = New System.Drawing.Point(13, 19)
        Me.ButtonSalvar.Name = "ButtonSalvar"
        Me.ButtonSalvar.Size = New System.Drawing.Size(160, 87)
        Me.ButtonSalvar.TabIndex = 26
        Me.ButtonSalvar.Text = "Save"
        Me.ButtonSalvar.UseVisualStyleBackColor = True
        '
        'ButtonSair
        '
        Me.ButtonSair.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ButtonSair.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSair.Location = New System.Drawing.Point(863, 475)
        Me.ButtonSair.Name = "ButtonSair"
        Me.ButtonSair.Size = New System.Drawing.Size(159, 50)
        Me.ButtonSair.TabIndex = 25
        Me.ButtonSair.Text = "Exit"
        Me.ButtonSair.UseVisualStyleBackColor = True
        '
        'GroupBoxComand
        '
        Me.GroupBoxComand.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.GroupBoxComand.Controls.Add(Me.ButtonSalvar)
        Me.GroupBoxComand.Location = New System.Drawing.Point(12, 408)
        Me.GroupBoxComand.Name = "GroupBoxComand"
        Me.GroupBoxComand.Size = New System.Drawing.Size(185, 117)
        Me.GroupBoxComand.TabIndex = 29
        Me.GroupBoxComand.TabStop = False
        '
        'ButtonPlot
        '
        Me.ButtonPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPlot.Location = New System.Drawing.Point(149, 69)
        Me.ButtonPlot.Name = "ButtonPlot"
        Me.ButtonPlot.Size = New System.Drawing.Size(126, 38)
        Me.ButtonPlot.TabIndex = 30
        Me.ButtonPlot.Text = "Plot Diagram"
        Me.ButtonPlot.UseVisualStyleBackColor = True
        '
        'PrintDialogDATA
        '
        Me.PrintDialogDATA.UseEXDialog = True
        '
        'SaveFileDialogDATA
        '
        Me.SaveFileDialogDATA.DefaultExt = "txt"
        Me.SaveFileDialogDATA.Filter = "Text files|*.txt"
        '
        'PrintDocumentDATA
        '
        '
        'GroupBoxDA
        '
        Me.GroupBoxDA.Controls.Add(Me.GroupBoxWaveAD)
        Me.GroupBoxDA.Controls.Add(Me.RadioButtonDA2)
        Me.GroupBoxDA.Controls.Add(Me.RadioButtonDA1)
        Me.GroupBoxDA.Controls.Add(Me.ButtonPlot)
        Me.GroupBoxDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxDA.Location = New System.Drawing.Point(203, 408)
        Me.GroupBoxDA.Name = "GroupBoxDA"
        Me.GroupBoxDA.Size = New System.Drawing.Size(282, 117)
        Me.GroupBoxDA.TabIndex = 31
        Me.GroupBoxDA.TabStop = False
        Me.GroupBoxDA.Text = "Arrows Diagram"
        '
        'GroupBoxWaveAD
        '
        Me.GroupBoxWaveAD.Controls.Add(Me.RadioButtonAirAD)
        Me.GroupBoxWaveAD.Controls.Add(Me.RadioButtonVacAD)
        Me.GroupBoxWaveAD.Location = New System.Drawing.Point(6, 67)
        Me.GroupBoxWaveAD.Name = "GroupBoxWaveAD"
        Me.GroupBoxWaveAD.Size = New System.Drawing.Size(127, 44)
        Me.GroupBoxWaveAD.TabIndex = 33
        Me.GroupBoxWaveAD.TabStop = False
        Me.GroupBoxWaveAD.Text = "Wavelength in"
        '
        'RadioButtonAirAD
        '
        Me.RadioButtonAirAD.AutoSize = True
        Me.RadioButtonAirAD.Location = New System.Drawing.Point(82, 19)
        Me.RadioButtonAirAD.Name = "RadioButtonAirAD"
        Me.RadioButtonAirAD.Size = New System.Drawing.Size(40, 17)
        Me.RadioButtonAirAD.TabIndex = 1
        Me.RadioButtonAirAD.Text = "Air"
        Me.RadioButtonAirAD.UseVisualStyleBackColor = True
        '
        'RadioButtonVacAD
        '
        Me.RadioButtonVacAD.AutoSize = True
        Me.RadioButtonVacAD.Checked = True
        Me.RadioButtonVacAD.Location = New System.Drawing.Point(6, 19)
        Me.RadioButtonVacAD.Name = "RadioButtonVacAD"
        Me.RadioButtonVacAD.Size = New System.Drawing.Size(70, 17)
        Me.RadioButtonVacAD.TabIndex = 0
        Me.RadioButtonVacAD.TabStop = True
        Me.RadioButtonVacAD.Text = "Vacuum"
        Me.RadioButtonVacAD.UseVisualStyleBackColor = True
        '
        'RadioButtonDA2
        '
        Me.RadioButtonDA2.AutoSize = True
        Me.RadioButtonDA2.Checked = True
        Me.RadioButtonDA2.Location = New System.Drawing.Point(6, 41)
        Me.RadioButtonDA2.Name = "RadioButtonDA2"
        Me.RadioButtonDA2.Size = New System.Drawing.Size(156, 17)
        Me.RadioButtonDA2.TabIndex = 32
        Me.RadioButtonDA2.TabStop = True
        Me.RadioButtonDA2.Text = "All Possible Transitions"
        Me.RadioButtonDA2.UseVisualStyleBackColor = True
        '
        'RadioButtonDA1
        '
        Me.RadioButtonDA1.AutoSize = True
        Me.RadioButtonDA1.Location = New System.Drawing.Point(6, 23)
        Me.RadioButtonDA1.Name = "RadioButtonDA1"
        Me.RadioButtonDA1.Size = New System.Drawing.Size(180, 17)
        Me.RadioButtonDA1.TabIndex = 31
        Me.RadioButtonDA1.Text = "Known Possible Transitions"
        Me.RadioButtonDA1.UseVisualStyleBackColor = True
        '
        'GroupBoxSS
        '
        Me.GroupBoxSS.Controls.Add(Me.GroupBox3)
        Me.GroupBoxSS.Controls.Add(Me.GroupBox2)
        Me.GroupBoxSS.Controls.Add(Me.RadioButtonSS2)
        Me.GroupBoxSS.Controls.Add(Me.RadioButtonSS1)
        Me.GroupBoxSS.Controls.Add(Me.Labelunit)
        Me.GroupBoxSS.Controls.Add(Me.LabelFWHM)
        Me.GroupBoxSS.Controls.Add(Me.TextBoxFWHM)
        Me.GroupBoxSS.Controls.Add(Me.ButtonPlotSS)
        Me.GroupBoxSS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxSS.Location = New System.Drawing.Point(494, 408)
        Me.GroupBoxSS.Name = "GroupBoxSS"
        Me.GroupBoxSS.Size = New System.Drawing.Size(363, 117)
        Me.GroupBoxSS.TabIndex = 32
        Me.GroupBoxSS.TabStop = False
        Me.GroupBoxSS.Text = "Spectrum Simulation"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LabelnumLI)
        Me.GroupBox3.Controls.Add(Me.LabelnumLS)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(225, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(131, 42)
        Me.GroupBox3.TabIndex = 42
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Spectral Range"
        '
        'LabelnumLI
        '
        Me.LabelnumLI.AutoSize = True
        Me.LabelnumLI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelnumLI.Location = New System.Drawing.Point(4, 18)
        Me.LabelnumLI.Name = "LabelnumLI"
        Me.LabelnumLI.Size = New System.Drawing.Size(43, 13)
        Me.LabelnumLI.TabIndex = 36
        Me.LabelnumLI.Text = "000000"
        '
        'LabelnumLS
        '
        Me.LabelnumLS.AutoSize = True
        Me.LabelnumLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelnumLS.Location = New System.Drawing.Point(77, 18)
        Me.LabelnumLS.Name = "LabelnumLS"
        Me.LabelnumLS.Size = New System.Drawing.Size(43, 13)
        Me.LabelnumLS.TabIndex = 37
        Me.LabelnumLS.Text = "000000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(53, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "to"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButtonAirSS)
        Me.GroupBox2.Controls.Add(Me.RadioButtonVacSS)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(126, 44)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Wavelength in"
        '
        'RadioButtonAirSS
        '
        Me.RadioButtonAirSS.AutoSize = True
        Me.RadioButtonAirSS.Location = New System.Drawing.Point(82, 19)
        Me.RadioButtonAirSS.Name = "RadioButtonAirSS"
        Me.RadioButtonAirSS.Size = New System.Drawing.Size(40, 17)
        Me.RadioButtonAirSS.TabIndex = 1
        Me.RadioButtonAirSS.Text = "Air"
        Me.RadioButtonAirSS.UseVisualStyleBackColor = True
        '
        'RadioButtonVacSS
        '
        Me.RadioButtonVacSS.AutoSize = True
        Me.RadioButtonVacSS.Checked = True
        Me.RadioButtonVacSS.Location = New System.Drawing.Point(6, 19)
        Me.RadioButtonVacSS.Name = "RadioButtonVacSS"
        Me.RadioButtonVacSS.Size = New System.Drawing.Size(70, 17)
        Me.RadioButtonVacSS.TabIndex = 0
        Me.RadioButtonVacSS.TabStop = True
        Me.RadioButtonVacSS.Text = "Vacuum"
        Me.RadioButtonVacSS.UseVisualStyleBackColor = True
        '
        'RadioButtonSS2
        '
        Me.RadioButtonSS2.AutoSize = True
        Me.RadioButtonSS2.Checked = True
        Me.RadioButtonSS2.Location = New System.Drawing.Point(6, 41)
        Me.RadioButtonSS2.Name = "RadioButtonSS2"
        Me.RadioButtonSS2.Size = New System.Drawing.Size(156, 17)
        Me.RadioButtonSS2.TabIndex = 40
        Me.RadioButtonSS2.TabStop = True
        Me.RadioButtonSS2.Text = "All Possible Transitions"
        Me.RadioButtonSS2.UseVisualStyleBackColor = True
        '
        'RadioButtonSS1
        '
        Me.RadioButtonSS1.AutoSize = True
        Me.RadioButtonSS1.Location = New System.Drawing.Point(6, 23)
        Me.RadioButtonSS1.Name = "RadioButtonSS1"
        Me.RadioButtonSS1.Size = New System.Drawing.Size(180, 17)
        Me.RadioButtonSS1.TabIndex = 39
        Me.RadioButtonSS1.Text = "Known Possible Transitions"
        Me.RadioButtonSS1.UseVisualStyleBackColor = True
        '
        'Labelunit
        '
        Me.Labelunit.AutoSize = True
        Me.Labelunit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labelunit.Location = New System.Drawing.Point(185, 67)
        Me.Labelunit.Name = "Labelunit"
        Me.Labelunit.Size = New System.Drawing.Size(30, 13)
        Me.Labelunit.TabIndex = 33
        Me.Labelunit.Text = "( nm)"
        '
        'LabelFWHM
        '
        Me.LabelFWHM.AutoSize = True
        Me.LabelFWHM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelFWHM.Location = New System.Drawing.Point(141, 67)
        Me.LabelFWHM.Name = "LabelFWHM"
        Me.LabelFWHM.Size = New System.Drawing.Size(45, 13)
        Me.LabelFWHM.TabIndex = 32
        Me.LabelFWHM.Text = "FWHM"
        '
        'TextBoxFWHM
        '
        Me.TextBoxFWHM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxFWHM.Location = New System.Drawing.Point(144, 86)
        Me.TextBoxFWHM.Name = "TextBoxFWHM"
        Me.TextBoxFWHM.Size = New System.Drawing.Size(71, 20)
        Me.TextBoxFWHM.TabIndex = 31
        '
        'ButtonPlotSS
        '
        Me.ButtonPlotSS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonPlotSS.Location = New System.Drawing.Point(230, 69)
        Me.ButtonPlotSS.Name = "ButtonPlotSS"
        Me.ButtonPlotSS.Size = New System.Drawing.Size(126, 38)
        Me.ButtonPlotSS.TabIndex = 30
        Me.ButtonPlotSS.Text = "Plot Spectrum"
        Me.ButtonPlotSS.UseVisualStyleBackColor = True
        '
        'FormResultPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1034, 532)
        Me.Controls.Add(Me.GroupBoxSS)
        Me.Controls.Add(Me.GroupBoxDA)
        Me.Controls.Add(Me.GroupBoxComand)
        Me.Controls.Add(Me.ButtonSair)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormResultPT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormResultPT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBoxComand.ResumeLayout(False)
        Me.GroupBoxDA.ResumeLayout(False)
        Me.GroupBoxDA.PerformLayout()
        Me.GroupBoxWaveAD.ResumeLayout(False)
        Me.GroupBoxWaveAD.PerformLayout()
        Me.GroupBoxSS.ResumeLayout(False)
        Me.GroupBoxSS.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPrint As System.Windows.Forms.Button
    Friend WithEvents LabelNTrans As System.Windows.Forms.Label
    Friend WithEvents ButtonSalvar As System.Windows.Forms.Button
    Friend WithEvents ButtonSair As System.Windows.Forms.Button
    Friend WithEvents GroupBoxComand As System.Windows.Forms.GroupBox
    Friend WithEvents PrintDialogDATA As System.Windows.Forms.PrintDialog
    Friend WithEvents SaveFileDialogDATA As System.Windows.Forms.SaveFileDialog
    Friend WithEvents PrintDocumentDATA As System.Drawing.Printing.PrintDocument
    Friend WithEvents ButtonPlot As System.Windows.Forms.Button
    Friend WithEvents LvResults As System.Windows.Forms.ListView
    Friend WithEvents GroupBoxDA As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxSS As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonPlotSS As System.Windows.Forms.Button
    Friend WithEvents RadioButtonDA2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDA1 As System.Windows.Forms.RadioButton
    Friend WithEvents Labelunit As System.Windows.Forms.Label
    Friend WithEvents LabelFWHM As System.Windows.Forms.Label
    Friend WithEvents TextBoxFWHM As System.Windows.Forms.TextBox
    Friend WithEvents LabelnumLS As System.Windows.Forms.Label
    Friend WithEvents LabelnumLI As System.Windows.Forms.Label
    Friend WithEvents RadioButtonSS2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSS1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxWaveAD As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonVacAD As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAirAD As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonAirSS As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVacSS As System.Windows.Forms.RadioButton
End Class
