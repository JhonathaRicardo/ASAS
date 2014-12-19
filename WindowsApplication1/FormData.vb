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
Imports System.IO

'FUNÇÃO PARA TEXTBOX NUMÉRICO
Module ValidaNumberBox
    Function NumberBox(ByVal keyAscii As Short) As Short
        If (InStr(Asc(8) & "0123456789,.", Chr(keyAscii)) = 0) Then
            NumberBox = 0
        Else
            NumberBox = keyAscii
        End If

    End Function
End Module
Public Class FormData
    'ARQUIVOS DE ENTRADA
    Dim DATAFILEIN As IO.StreamReader

    'ARQUIVOS DE SAIDA
    Dim DATAFILEOUT As IO.StreamWriter

    'VARIÁVEIS ALFANUMÉRICAS
    Dim PATHINDEX As String
    Dim N, C1, C2, LinhasL, LinhasN As Integer
    Dim TEXTLINE(9), TEXTLEVEL(4) As String
   

    Private Sub FormData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PATHINDEX = Application.StartupPath & "\DATABASE"
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)

        'LIMPANDO TABELA DE RESULTADOS
        LvResults.Columns.Clear()
        N = 0
    End Sub
    'ABRIR ARQUIVO DA BASE DE DADOS
    Private Sub ButtonImportFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonImportFile.Click

        OpenFileDialogDATA.InitialDirectory = PATHINDEX

        'ABILITANDO FUNÇÕES
        ButtonCancelAll.Enabled = False
        ButtonSaveAll.Enabled = False
        GroupBoxInsert.Enabled = False

        'LIMPANDO TABELA DE DADOS E TEXTBOXES
        LvResults.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox9.Clear()
        TextBox9.Clear()
        TextBox10.Clear()

        'ABRINDO ARQUIVO
        If OpenFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBoxOpenFile.Text = OpenFileDialogDATA.FileName
        Else
            TextBoxOpenFile.Text = ""
        End If

        'ABERTURA APENAS DOS ARQUIVOS DA BASE DE DADOS
        If (TextBoxOpenFile.Text.Contains("DATABASE\LINES\Lines")) Or (TextBoxOpenFile.Text.Contains("DATABASE\LEVELS\Levels")) Then
            'LIMPANDO TABELA
            LvResults.Clear()
            'ABILITAR GROUPBOX
            GroupBoxTable.Enabled = True
            'ABILITANDO FUNÇÕES
            ButtonCancelAll.Enabled = True
            ButtonSaveAll.Enabled = True

            DATAFILEIN = New IO.StreamReader(TextBoxOpenFile.Text)
            C2 = 0

            '______________________________________________________________________________________________
            'TABELA PARA OS ARQUIVOS DE LINHAS ESPECTRAIS
            If TextBoxOpenFile.Text.Contains("DATABASE\LINES\Lines") Then
                'SELECIONANDO RADIOBUTTON E DESABILITANDO TEXTBOX1
                N = 0
                RadioButtonLines.Checked = True

                LvResults.Columns.Add(" Number ", 80)
                LvResults.Columns.Add("λo (nm)", 90)
                LvResults.Columns.Add("λair (nm)", 90)
                LvResults.Columns.Add("RI (a.u.)", 100)
                LvResults.Columns.Add("A (1/s)", 100)
                LvResults.Columns.Add("Unc.A (+/- 1/s)", 120)
                LvResults.Columns.Add("El (1/cm)", 100)
                LvResults.Columns.Add("Jl", 80)
                LvResults.Columns.Add("Eu (1/cm)", 100)
                LvResults.Columns.Add("Ju", 80)


                'TEXTBOX UTILIZADOS NA TABELA DE LINHAS
                TextBox6.Visible = True
                Label6.Visible = True
                TextBox7.Visible = True
                Label7.Visible = True
                TextBox8.Visible = True
                Label8.Visible = True
                TextBox10.Visible = True
                Label10.Visible = True
                TextBox9.Visible = True
                Label9.Visible = True

                'MUDANÇA DOS TEXT DOS 3 PRIMIROS LABELS
                Label1.Text = "Number"
                Label2.Text = "λo"
                Label3.Text = "λair"
                Label4.Text = "RI (a.u.)"
                Label5.Text = "A (1/s)"

                'LEITURA DE DADOS DO ARQUIVO
                Do While DATAFILEIN.Peek <> -1

                    'WAITING CURSOR PARA ESPERAR CARREGAR
                    Me.Cursor = Cursors.WaitCursor

                    'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                    TEXTLINE = DATAFILEIN.ReadLine.Split(" ")
                    Dim LVItem As New ListViewItem

                    If TEXTLINE(0) <> "" Then
                        
                        'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                        Try
                            'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                            LVItem.Text = Replace(TEXTLINE(0), ",", ".")
                            LvResults.Items.Add(LVItem)
                            LVItem.SubItems.Add(Replace(TEXTLINE(1), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(2), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(3), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(4), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(5), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(6), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(7), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(8), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(9), ",", "."))
                            'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                        Catch ex As IndexOutOfRangeException
                        End Try

                        'AUMENTANDO RANGE DO ARRAY TEXTLINE EM CASOS ONDE LENGTH < 5
                        If TEXTLINE.Length < 10 Then
                            For i As Integer = TEXTLINE.Length To 10
                                LVItem.SubItems.Add("")
                            Next
                        End If

                        'NÚMERO DE TRANSIÇÕES
                        N = N + 1
                    End If
                Loop
                'NORMAL CURSOR PARA ESPERAR CARREGAR
                Me.Cursor = Cursors.Default

                'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                LabelNTrans.Text = N

                'EXCLUINDO ULTIMAS LINHAS DO LISTVIEW
            Else
                '______________________________________________________________________________________________
                'LIMPANDO TABELA E ABILITANDO TEXTBOX1
                N = 0
                RadioButtonLevels.Checked = True
                TextBox1.Enabled = True

                'TABELA PARA OS ARQUIVOS DE NÍVEIS DE ENERGIA
                LvResults.Columns.Add(" J ", 80)
                LvResults.Columns.Add("Energy Level (1/cm)", 150)
                LvResults.Columns.Add("Parity", 100)
                LvResults.Columns.Add("Life Time (ns)", 100)
                LvResults.Columns.Add("Unc. Life Time (+/- ns)", 150)

                'MUDANÇA DOS TEXT DOS 3 PRIMIROS LABELS
                Label1.Text = "J"
                Label2.Text = "Energy Level"
                Label3.Text = "Parity"
                Label4.Text = "Life Time"
                Label5.Text = "Unc. Life Time (+/- ns)"


                'TEXTBOX UTILIZADOS NA TABELA DE NIVEIS
                TextBox6.Visible = False
                Label6.Visible = False
                TextBox7.Visible = False
                Label7.Visible = False
                TextBox8.Visible = False
                Label8.Visible = False
                TextBox10.Visible = False
                Label10.Visible = False
                TextBox9.Visible = False
                Label9.Visible = False

                'LEITURA DE DADOS DO ARQUIVO
                Do While DATAFILEIN.Peek <> -1

                    'WAITING CURSOR PARA ESPERAR CARREGAR
                    Me.Cursor = Cursors.WaitCursor

                    'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                    TEXTLINE = DATAFILEIN.ReadLine.Split(" ")
                    Dim LVItem As New ListViewItem

                    If TEXTLINE(0) <> "" Then

                        'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                        Try
                            'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                            LVItem.Text = Replace(TEXTLINE(0), ",", ".")
                            LvResults.Items.Add(LVItem)
                            LVItem.SubItems.Add(Replace(TEXTLINE(1), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(2), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(3), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(4), ",", "."))

                            'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                        Catch ex As IndexOutOfRangeException
                        End Try

                        'AUMENTANDO RANGE DO ARRAY TEXTLINE EM CASOS ONDE LENGTH < 5
                        If TEXTLINE.Length < 5 Then
                            For i As Integer = TEXTLINE.Length To 5
                                LVItem.SubItems.Add("")
                            Next
                        End If

                        'NÚMERO DE TRANSIÇÕES
                        N = N + 1
                    End If
                Loop


                'WAITING CURSOR PARA ESPERAR CARREGAR
                Me.Cursor = Cursors.Default

                'NUMEROS DE LINHAS DO ARQUIVO DE NIVEIS
                LabelNTrans.Text = N

                'EXCLUINDO ULTIMAS LINHAS DO LISTVIEW
            End If
           

        ElseIf TextBoxOpenFile.Text = "" Then
            Return
        Else
            'PARA ARQUIVOS QUE NÃO ESTÃO NA BASE DE DADOS
            MessageBox.Show("The file can not be opened! Choose a file from the database.", "", MessageBoxButtons.OK)
            TextBoxOpenFile.Text = ""
            GroupBoxInsert.Enabled = False
            GroupBoxTable.Enabled = False
            Return
        End If

        'FECHANDO O ARQUIVO
        DATAFILEIN.Close()

    End Sub
    'SELEÇÃO DE ITENS NO LISTVIEW
    Private Sub LvResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvResults.SelectedIndexChanged
        'DESABILITANDO GROUPBOXINSERT
        GroupBoxInsert.Enabled = True

        'SELEÇÃO DE ITENS NA TABELA DE LINHAS
        If RadioButtonLines.Checked = True Then
            If LvResults.SelectedItems.Count > 0 Then
                TextBox1.Text = LvResults.SelectedItems(0).Text
                TextBox2.Text = LvResults.SelectedItems(0).SubItems(1).Text
                TextBox3.Text = LvResults.SelectedItems(0).SubItems(2).Text
                TextBox4.Text = LvResults.SelectedItems(0).SubItems(3).Text
                TextBox5.Text = LvResults.SelectedItems(0).SubItems(4).Text
                TextBox6.Text = LvResults.SelectedItems(0).SubItems(5).Text
                TextBox7.Text = LvResults.SelectedItems(0).SubItems(6).Text
                TextBox8.Text = LvResults.SelectedItems(0).SubItems(7).Text
                TextBox9.Text = LvResults.SelectedItems(0).SubItems(8).Text
                TextBox10.Text = LvResults.SelectedItems(0).SubItems(9).Text
            End If

        Else
            'SELEÇÃO DE ITENS NA TABELA DE NIVEIS
            If LvResults.SelectedItems.Count > 0 Then
                TextBox1.Text = LvResults.SelectedItems(0).Text
                TextBox2.Text = LvResults.SelectedItems(0).SubItems(1).Text
                TextBox3.Text = LvResults.SelectedItems(0).SubItems(2).Text
                TextBox4.Text = LvResults.SelectedItems(0).SubItems(3).Text
                TextBox5.Text = LvResults.SelectedItems(0).SubItems(4).Text
            End If
        End If

    End Sub

    Private Sub ButtonSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSalvar.Click
        'SELEÇÃO DE ITENS NA TABELA DE LINHAS
        If RadioButtonLines.Checked = True Then
            If LvResults.SelectedItems.Count > 0 Then
                LvResults.SelectedItems(0).Text = Replace(TextBox1.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(1).Text = Replace(TextBox2.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(2).Text = Replace(TextBox3.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(3).Text = Replace(TextBox4.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(4).Text = Replace(TextBox5.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(5).Text = Replace(TextBox6.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(6).Text = Replace(TextBox7.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(7).Text = Replace(TextBox8.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(8).Text = Replace(TextBox9.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(9).Text = Replace(TextBox10.Text, " ", "")
            End If
        Else
            'SELEÇÃO DE ITENS NA TABELA DE NIVEIS
            If LvResults.SelectedItems.Count > 0 Then
                LvResults.SelectedItems(0).Text = Replace(TextBox1.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(1).Text = Replace(TextBox2.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(2).Text = Replace(TextBox3.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(3).Text = Replace(TextBox4.Text, " ", "")
                LvResults.SelectedItems(0).SubItems(4).Text = Replace(TextBox5.Text, " ", "")
            End If
        End If
    End Sub
    'INCLUIR LINHA DE DADOS
    Private Sub ButtonIncludeLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonIncludeLine.Click

        GroupBoxInsert.Enabled = True
        N = N + 1

        Dim LVItem As New ListViewItem
        LvResults.Items.Add(LVItem)
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        LVItem.SubItems.Add("")
        
        'VERIFICANDO NÚMERO DA PROXIMA LINHA DE DADOS CASO O ARQUIVO FOR DE LINHAS
        If RadioButtonLines.Checked = True Then
            TextBox1.Text = N
            LvResults.Items(N - 1).Text = N
        End If

        LvResults.Items(N - 1).Selected = True
    End Sub
    'BOTÃO PARA FECHAR FORMDATA
    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub
    'SALVAR ALTERAÇÕES EM ARQUIVO
    Private Sub ButtonSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveAll.Click

        DATAFILEOUT = New IO.StreamWriter(TextBoxOpenFile.Text)

        Try
            If LvResults.Items.Count <= 0 Then Throw New Exception
            'LEITURA DAS LINHAS DO LISTVIEW LVRESULT
            For Index As Integer = 0 To LvResults.Items.Count - 1
                'CRIANDO NOVA LINHA DE DADOS
                DATAFILEOUT.Write(Environment.NewLine)
                '  percorre todas as colunhas coletando os itens e escreve no arquivo incluindo o TAB
                For SubIndex As Integer = 0 To LvResults.Items(Index).SubItems.Count - 1
                    DATAFILEOUT.Write(LvResults.Items(Index).SubItems(SubIndex).Text & " ")
                Next
            Next
            MsgBox("File successfully saved!")

        Catch ex As Exception
            MsgBox("Error saving the  text file!" & ex.Message)
        Finally
            DATAFILEOUT.Close()
        End Try

    End Sub
    'DELETAR LINHA
    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        'SELEÇÃO DE ITENS NA TABELA DE LINHAS
        If RadioButtonLines.Checked = True Then
            If LvResults.SelectedItems.Count > 0 Then
                LvResults.SelectedItems(0).Text = ""
                LvResults.SelectedItems(0).SubItems(1).Text = ""
                LvResults.SelectedItems(0).SubItems(2).Text = ""
                LvResults.SelectedItems(0).SubItems(3).Text = ""
                LvResults.SelectedItems(0).SubItems(4).Text = ""
                LvResults.SelectedItems(0).SubItems(5).Text = ""
                LvResults.SelectedItems(0).SubItems(6).Text = ""
                LvResults.SelectedItems(0).SubItems(7).Text = ""
                LvResults.SelectedItems(0).SubItems(8).Text = ""
                LvResults.SelectedItems(0).SubItems(9).Text = ""
            End If
        Else
            'SELEÇÃO DE ITENS NA TABELA DE NIVEIS
            If LvResults.SelectedItems.Count > 0 Then
                LvResults.SelectedItems(0).Text = ""
                LvResults.SelectedItems(0).SubItems(1).Text = ""
                LvResults.SelectedItems(0).SubItems(2).Text = ""
                LvResults.SelectedItems(0).SubItems(3).Text = ""
                LvResults.SelectedItems(0).SubItems(4).Text = ""
            End If
        End If
    End Sub
    'CANCELAR ALTERAÇÕES
    Private Sub ButtonCancelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancelAll.Click

        GroupBoxInsert.Enabled = False
        'LIMPANDO TABELA DE DADOS E TEXTBOXES
        LvResults.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox9.Clear()
        TextBox9.Clear()
        TextBox10.Clear()

        'ABERTURA APENAS DOS ARQUIVOS DA BASE DE DADOS
        If (TextBoxOpenFile.Text.Contains("DATABASE\LINES\Lines")) Or (TextBoxOpenFile.Text.Contains("DATABASE\LEVELS\Levels")) Then
            'LIMPANDO TABELA
            LvResults.Clear()
            'ABILITAR GROUPBOX
            GroupBoxTable.Enabled = True

            DATAFILEIN = New IO.StreamReader(TextBoxOpenFile.Text)
            C2 = 0

            '______________________________________________________________________________________________
            'TABELA PARA OS ARQUIVOS DE LINHAS ESPECTRAIS
            If TextBoxOpenFile.Text.Contains("DATABASE\LINES\Lines") Then
                'SELECIONANDO RADIOBUTTON E DESABILITANDO TEXTBOX1
                N = 0
                RadioButtonLines.Checked = True
                TextBox1.Enabled = False

                LvResults.Columns.Add(" Number ", 80)
                LvResults.Columns.Add("λo (nm)", 90)
                LvResults.Columns.Add("λair (nm)", 90)
                LvResults.Columns.Add("RI (a.u.)", 100)
                LvResults.Columns.Add("A (1/s)", 100)
                LvResults.Columns.Add("Unc.A (+/- 1/s)", 120)
                LvResults.Columns.Add("El (1/cm)", 100)
                LvResults.Columns.Add("Jl", 80)
                LvResults.Columns.Add("Eu (1/cm)", 100)
                LvResults.Columns.Add("Ju", 80)


                'TEXTBOX UTILIZADOS NA TABELA DE LINHAS
                TextBox6.Visible = True
                Label6.Visible = True
                TextBox7.Visible = True
                Label7.Visible = True
                TextBox8.Visible = True
                Label8.Visible = True
                TextBox10.Visible = True
                Label10.Visible = True
                TextBox9.Visible = True
                Label9.Visible = True

                'MUDANÇA DOS TEXT DOS 3 PRIMIROS LABELS
                Label1.Text = "Number"
                Label2.Text = "λo"
                Label3.Text = "λair"
                Label4.Text = "RI (a.u.)"
                Label5.Text = "A (1/s)"

                'LEITURA DE DADOS DO ARQUIVO
                Do While DATAFILEIN.Peek <> -1

                    'WAITING CURSOR PARA ESPERAR CARREGAR
                    Me.Cursor = Cursors.WaitCursor

                    'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                    TEXTLINE = DATAFILEIN.ReadLine.Split(" ")
                    Dim LVItem As New ListViewItem

                    If TEXTLINE(0) <> "" Then

                        'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                        Try
                            'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                            LVItem.Text = Replace(TEXTLINE(0), ",", ".")
                            LvResults.Items.Add(LVItem)
                            LVItem.SubItems.Add(Replace(TEXTLINE(1), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(2), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(3), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(4), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(5), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(6), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(7), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(8), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(9), ",", "."))
                            'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                        Catch ex As IndexOutOfRangeException
                        End Try

                        'AUMENTANDO RANGE DO ARRAY TEXTLINE EM CASOS ONDE LENGTH < 5
                        If TEXTLINE.Length < 10 Then
                            For i As Integer = TEXTLINE.Length To 10
                                LVItem.SubItems.Add("")
                            Next
                        End If

                        'NÚMERO DE TRANSIÇÕES
                        N = N + 1
                    End If
                Loop
                'NORMAL CURSOR PARA ESPERAR CARREGAR
                Me.Cursor = Cursors.Default

                'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                LabelNTrans.Text = N

                'EXCLUINDO ULTIMAS LINHAS DO LISTVIEW
            Else
                '______________________________________________________________________________________________
                'LIMPANDO TABELA E ABILITANDO TEXTBOX1
                N = 0
                RadioButtonLevels.Checked = True
                TextBox1.Enabled = True

                'TABELA PARA OS ARQUIVOS DE NÍVEIS DE ENERGIA
                LvResults.Columns.Add(" J ", 80)
                LvResults.Columns.Add("Energy Level (1/cm)", 150)
                LvResults.Columns.Add("Parity", 100)
                LvResults.Columns.Add("Life Time (ns)", 100)
                LvResults.Columns.Add("Unc. Life Time (+/- ns)", 150)

                'MUDANÇA DOS TEXT DOS 3 PRIMIROS LABELS
                Label1.Text = "J"
                Label2.Text = "Energy Level"
                Label3.Text = "Parity"
                Label4.Text = "Life Time"
                Label5.Text = "Unc. Life Time (+/- ns)"


                'TEXTBOX UTILIZADOS NA TABELA DE NIVEIS
                TextBox6.Visible = False
                Label6.Visible = False
                TextBox7.Visible = False
                Label7.Visible = False
                TextBox8.Visible = False
                Label8.Visible = False
                TextBox10.Visible = False
                Label10.Visible = False
                TextBox9.Visible = False
                Label9.Visible = False

                'LEITURA DE DADOS DO ARQUIVO
                Do While DATAFILEIN.Peek <> -1

                    'WAITING CURSOR PARA ESPERAR CARREGAR
                    Me.Cursor = Cursors.WaitCursor

                    'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                    TEXTLINE = DATAFILEIN.ReadLine.Split(" ")
                    Dim LVItem As New ListViewItem

                    If TEXTLINE(0) <> "" Then

                        'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                        Try
                            'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                            LVItem.Text = Replace(TEXTLINE(0), ",", ".")
                            LvResults.Items.Add(LVItem)
                            LVItem.SubItems.Add(Replace(TEXTLINE(1), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(2), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(3), ",", "."))
                            LVItem.SubItems.Add(Replace(TEXTLINE(4), ",", "."))

                            'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                        Catch ex As IndexOutOfRangeException
                        End Try

                        'AUMENTANDO RANGE DO ARRAY TEXTLINE EM CASOS ONDE LENGTH < 5
                        If TEXTLINE.Length < 5 Then
                            For i As Integer = TEXTLINE.Length To 5
                                LVItem.SubItems.Add("")
                            Next
                        End If

                        'NÚMERO DE TRANSIÇÕES
                        N = N + 1
                    End If
                Loop


                'WAITING CURSOR PARA ESPERAR CARREGAR
                Me.Cursor = Cursors.Default

                'NUMEROS DE LINHAS DO ARQUIVO DE NIVEIS
                LabelNTrans.Text = N

                'EXCLUINDO ULTIMAS LINHAS DO LISTVIEW
            End If


        ElseIf TextBoxOpenFile.Text = "" Then
            Return
        End If

        'FECHANDO O ARQUIVO
        DATAFILEIN.Close()
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub

    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub

    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub

    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class