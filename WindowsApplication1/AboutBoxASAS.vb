Public NotInheritable Class AboutBoxASAS

    Private Sub AboutBoxASAS_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'HABILITANDO O FORM PRINCIPAL QUANDO O ABOUT É FECHADO
        FormMain.Enabled = True
    End Sub

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim PATHINDEX As String
        Dim REFIN As IO.StreamReader
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)
        REFIN = New IO.StreamReader(PATHINDEX & "\Reference.txt")
        TextBoxRef.Text = REFIN.ReadToEnd

        FormMain.Enabled = False

        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'ABRINDO MANUAL
        'LOCALIZANDO PATH PARA UTILIZAÇÃO DO BANCO DE DADOS
        Dim PATHINDEX As String
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)
        FormHelp.WBHelp.Navigate(PATHINDEX & "\ASAS MANUAL.pdf")
        FormHelp.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("mailto:esthersbam@hmail.com")
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("mailto:jhonatharicardo@hmail.com")
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        System.Diagnostics.Process.Start("mailto:felipe.barreta@hmail.com")
    End Sub
End Class
