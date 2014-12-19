'/* 
' ASAS Software - Analysis and simulation of Atomic Spectra
'   Copyright (C) 2014  Jhonatha Ricardo dos Santos, Maria Esther Sbampato, Luiz Felipe Nardin Barreta, Marcelo G. Destro
'
'   This program is free software: you can redistribute it and/or modify
'  it under the terms of the GNU Affero General Public License as
' published by the Free Software Foundation, either version 3 of the
'License, or (at your option) any later version.
'
'   This program is distributed in the hope that it will be useful,
'  but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU Affero General Public License for more details.
'
'   You should have received a copy of the GNU Affero General Public License
'  along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*/
Imports System.IO.IOException
Public Class FormResultLT
    Dim PATHINDEX As String
    Dim NLINHAS As Integer
    'ARQUIVOS DE SAIDA
    Dim DATAFILEOUT As IO.StreamWriter
    'QUANDO FORM FOR FECHADO
    Private Sub FormResultLT_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormMain.ButtonCancel.Enabled = True
        FormMain.Enabled = True
    End Sub

    'CARREGANDO FORMRESULTLT
    Private Sub FormResultLT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvResults.View = View.Details
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)
    End Sub

    Private Sub ButtonSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSalvar.Click
        SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\POSSIBLE TRANSITIONS"
        SaveFileDialogDATA.FileName = Me.Text

        If SaveFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DATAFILEOUT = New IO.StreamWriter(SaveFileDialogDATA.FileName)

            Try
                If LvResults.Items.Count <= 0 Then Throw New Exception
                'LEITURA DAS LINHAS DO LISTVIEW LVRESULT
                For Index As Integer = 0 To LvResults.Items.Count - 1
                    If Index = 0 Then
                        For ColIndex As Integer = 0 To LvResults.Items(Index).SubItems.Count - 1
                            DATAFILEOUT.Write(LvResults.Columns(ColIndex).Text & Chr(9))
                        Next
                        DATAFILEOUT.Write(Environment.NewLine)
                    End If

                    'CRIANDO NOVA LINHA DE DADOS
                    DATAFILEOUT.Write(Environment.NewLine)
                    '  percorre todas as colunhas coletando os itens e escreve no arquivo incluindo o TAB
                    For SubIndex As Integer = 0 To LvResults.Items(Index).SubItems.Count - 1
                        DATAFILEOUT.Write(LvResults.Items(Index).SubItems(SubIndex).Text & Chr(9))
                    Next
                Next

            Catch ex As Exception
                MsgBox("Error saving the  text file!" & ex.Message)
            Finally
                DATAFILEOUT.Close()
            End Try

        End If
    End Sub
    Private Sub ButtonSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSair.Click
        Me.Close()
    End Sub
    Private Sub PrintDocumentDATA_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocumentDATA.PrintPage
        Dim lINHAS As Integer = 0

        e.Graphics.DrawString("ASAS Software", New Font("Microsoft Sans Serif", 7), Brushes.Black, 7, 2)
        e.Graphics.DrawString(Me.Text, New Font("Microsoft Sans Serif", 7), Brushes.Black, 300, 2)
        e.Graphics.DrawString(Date.Now, New Font("Microsoft Sans Serif", 7), Brushes.Black, 725, 2)
        e.Graphics.DrawString(Me.Text, New Font("Arial Black", 9), Brushes.Blue, 55, 50)
        e.Graphics.DrawString(LabelNTrans.Text, New Font("Arial Black", 10), Brushes.Black, 60, 1100)

        If LvResults.Items.Count <= 0 Then Throw New Exception
        ' percorre todas as linhas
        For Index As Integer = NLINHAS To LvResults.Items.Count - 1

            '  percorre todas as colunhas coletando os itens e escreve no arquivo incluindo o TAB
            For SubIndex As Integer = 0 To LvResults.Items(Index).SubItems.Count - 1
                If Index = NLINHAS Then
                    e.Graphics.DrawRectangle(Pens.Black, 55 + SubIndex * 80, 75, 80, 20)
                    e.Graphics.DrawRectangle(Pens.Black, 55 + SubIndex * 80, 75, 80, 1000)
                    e.Graphics.DrawString(LvResults.Columns(SubIndex).Text, New Font("Arial Black", 10), Brushes.Black, 60 + (SubIndex) * 80, 75)
                End If
                e.Graphics.DrawString(LvResults.Items(Index).SubItems(SubIndex).Text, New Font("Microsoft Sans Serif", 10), Brushes.Black, 60 + SubIndex * 80, 100 + lINHAS * 20)
            Next
            lINHAS = lINHAS + 1
            If Index = 47 + NLINHAS Then
                e.HasMorePages = True
                NLINHAS = Index + 1
                Exit Sub
            End If
        Next
    End Sub
    Private Sub ButtonPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPrint.Click
        NLINHAS = 0
        PrintDialogDATA.Document = PrintDocumentDATA
        If PrintDialogDATA.ShowDialog() = DialogResult.OK Then
            PrintDocumentDATA.Print()
        End If
    End Sub

End Class