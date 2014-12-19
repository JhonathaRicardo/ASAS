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
Public Class FormResultPT
    Dim PATHINDEX As String
    Dim NPOINTS, NLINHAS, NLINHAS2, c1, ctcolor, ctcolor2, ctindex, clevel As Integer
    Dim FWHMstr, TEXTLINE(4), COMANDOGNU As String
    Dim DeltaL, FWHM, LIMINF, LIMSUP, LEVEL, IRMax, LEVELMax, LEVELMin As Double

    'VETORES DE ENTRADA
    Dim IRSS(250000), LAMBDASS(250000), Lvac(250000), Lair(250000), IR(250000), Ei(250000), Ef(250000) As Double
    'ARQUIVOS DE SAIDA
    Dim DATAFILEOUTEXE2, DATAFILEOUTEXE, DATAFILEOUT, DATAFILEOUT2, DATAFILEOUT3 As IO.StreamWriter
    Dim DATAFILEIN As IO.StreamReader


    Private Sub FormResultPT_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'HABILITANDO FUNÇOES APÓS FECHAMENTO DO PROGRAMA
        FormMain.ButtonCancel.Enabled = True
        FormMain.Enabled = True

    End Sub

    'CARREGANDO FORMRESULTPT
    Private Sub FormResultPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormMain.Enabled = False
        LvResults.View = View.Details
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)

        'DEFININDO LIMITES DOS GRAFICOS
        'PARA AS FUNÇÕES 2 E 3
        If FormMain.CheckBox2fot.Checked = True Then
            If (Val(FormMain.TextBoxLimI.Text) < Val(FormMain.TextBoxLimI2.Text)) Then
                LIMINF = Val(FormMain.TextBoxLimI.Text)
            Else
                LIMINF = Val(FormMain.TextBoxLimI2.Text)
            End If

            If Val(FormMain.TextBoxLimS.Text) < Val(FormMain.TextBoxLimS2.Text) Then
                LIMSUP = Val(FormMain.TextBoxLimS2.Text)
            Else
                LIMSUP = Val(FormMain.TextBoxLimS.Text)
            End If
        Else
            LIMINF = Val(FormMain.TextBoxLimI.Text)
            LIMSUP = Val(FormMain.TextBoxLimS.Text)
        End If
        'VALOR DE MAIOR INTENSIDADE PARA OS ESPECTROS SIMULADOS E NIVEIS PARA O DIAGRAMA
        IRMax = 0
        LEVELMax = 0
        LEVELMin = 1000000

        'LENDO ARQUIVO DATAGRAGH
        DATAFILEIN = New IO.StreamReader(PATHINDEX & "\DATAGRAPH.txt")
        Do While DATAFILEIN.Peek <> -1
            'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
            TEXTLINE = DATAFILEIN.ReadLine.Split(" ")

            If TEXTLINE(0) <> "" Then
                'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                Try
                    'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                    Lvac(c1) = Val(TEXTLINE(0))
                    Lair(c1) = Val(TEXTLINE(1))
                    Ei(c1) = Val(TEXTLINE(2))
                    Ef(c1) = Val(TEXTLINE(3))
                    IR(c1) = Val(TEXTLINE(4))

                    'ATRIBUINDO VALOR DE IRMAX
                    If (IR(c1) > IRMax) Then
                        IRMax = IR(c1)
                    End If

                    'ATRIBUINDO VALOR MAXIMO E MÍNIMO DE ENERGIA PARA O DIAGRAMA
                    If (Ei(c1) > LEVELMax) Then LEVELMax = Ei(c1)
                    If (Ef(c1) > LEVELMax) Then LEVELMax = Ef(c1)
                    If (Ei(c1) < LEVELMin) Then LEVELMin = Ei(c1)
                    If (Ef(c1) < LEVELMin) Then LEVELMin = Ef(c1)

                    c1 = c1 + 1

                    'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                Catch ex As IndexOutOfRangeException
                End Try

            End If
        Loop

        NLINHAS2 = c1 - 1
        c1 = 0

        'PARA REGIÕES SEM TRANSIÇÕES CONHECIDAS DEFINIMOS IRMAX = 10
        If (IRMax = 0) Then
            IRMax = 100
        End If

        IRMax = IRMax / 5

        'FECHANDO ARQUIVO DE ENTRADA
        DATAFILEIN.Close()
    End Sub
    'FECHANDO FORM
    Private Sub ButtonSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSair.Click
        'FECHAMENTO DO GNUPLOT E FORMGRAPH
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)
        FormGraph.Close()

        Me.Close()
    End Sub
    'SALVAR DADOS DE TABELA
    Private Sub ButtonSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSalvar.Click
        SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\TRANSITIONS FROM A LEVEL"
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
                MsgBox("Error saving the text file!" & ex.Message)
            Finally
                DATAFILEOUT.Close()
            End Try

        End If
    End Sub
    'BOTÃO DE IMPRESSÃO
    Private Sub ButtonPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPrint.Click
        NLINHAS = 0
        PrintDialogDATA.Document = PrintDocumentDATA
        If PrintDialogDATA.ShowDialog() = DialogResult.OK Then
            PrintDocumentDATA.Print()
        End If
    End Sub
    'DOCUMENTO DE IMPRESSÃO
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
    'ABRINDO DIAGRAMA PELO GNUPLOT 
    Private Sub ButtonPlot_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPlot.Click
        'VARIÁVEL DE COMFIRMAÇÃO

        'FECHAMENTO DO GNUPLOT E FORMGRAPH
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)
        FormGraph.Close()

        'CONFIGURAÇÃO FORMGRAPH
        FormGraph.ButtonDataSS.Enabled = False

        'ABRINDO, ESCREVENDO CABEÇALHO E FECHANDO O ARQUIVO DE EXECUÇÃO DO GNUPLOT
        DATAFILEOUTEXE2 = New IO.StreamWriter("DATAGRAPHEXE.gnu", False)
        DATAFILEOUTEXE2.WriteLine("reset")
        DATAFILEOUTEXE2.WriteLine("set terminal jpeg size 800,500")
        DATAFILEOUTEXE2.WriteLine("set output 'GraphicSS.jpeg'")
        DATAFILEOUTEXE2.WriteLine("set yrange[" & Replace((LEVELMin - LEVELMin * 0.005).ToString("0.00"), ",", ".") & ":" & Replace((LEVELMax + LEVELMax * 0.005).ToString("0.00"), ",", ".") & "]")
        DATAFILEOUTEXE2.WriteLine("set xrange[" & Replace((LIMINF - 15).ToString("0.00"), ",", ".") & ":" & Replace((LIMSUP + 15).ToString("0.00"), ",", ".") & "]")
        DATAFILEOUTEXE2.WriteLine("set grid")
        DATAFILEOUTEXE2.WriteLine("set title 'Arrows Diagram'")
        If RadioButtonVacAD.Checked = True Then
            DATAFILEOUTEXE2.WriteLine("set xlabel 'Wavelength in Vacuum (nm)'")
        ElseIf RadioButtonAirAD.Checked = True Then
            DATAFILEOUTEXE2.WriteLine("set xlabel 'Wavelength in Air (nm)'")
        End If

        DATAFILEOUTEXE2.WriteLine("set ylabel 'Energy Level (1/cm)'")
        If (FormMain.CheckBox2fot.Checked = True) And (FormMain.RadioButtonAbs.Checked = True) Then
            DATAFILEOUTEXE2.WriteLine("set label 'SECOND PHOTON' at " & Replace((LIMINF - 14).ToString("0.0000"), ",", ".") & "," & Replace(((LEVELMax + Ef(1)) / 2).ToString("0.0000"), ",", "."))
            DATAFILEOUTEXE2.WriteLine("set label 'FIRST PHOTON - From " & Replace((Ei(0)).ToString("0.0000"), ",", ".") & " [1/cm]' at " & Replace((LIMINF - 14).ToString("0.00"), ",", ".") & "," & Replace(((LEVELMin + Ef(1)) / 2).ToString("0.00"), ",", "."))
        ElseIf (FormMain.RadioButtonAbs.Checked = True) Then
            DATAFILEOUTEXE2.WriteLine("set label 'FIRST PHOTON - From " & Replace((Ei(0)).ToString("0.0000"), ",", ".") & " [1/cm]' at " & Replace((LIMINF - 14).ToString("0.00"), ",", ".") & "," & Replace(((LEVELMin + Ef(1)) / 2).ToString("0.00"), ",", "."))
        End If
        COMANDOGNU = "plot " & Replace(Ei(0).ToString("0.0000"), ",", ".") & " lc 0 lw 2"

        'ABRINDO ARQUIVO DE LEITURA DATAGRAPH
        LEVEL = -1 'VALOR INICIAL PARA LEVEL
        ctcolor = 1 'VALOR INICIAL DE CORES DE SETA GNUPLOT
        ctcolor2 = 1
        clevel = 0
        ctindex = 0 'VALOR DO INDEX DO GNUPLOT

        DATAFILEOUT2 = New IO.StreamWriter(PATHINDEX & "\DATAGRAPH1.txt")

        'REESCREVENDO O ARQUIVO PARA CONSTRUÇÃO DO DIAGRAMA COM OU SEM LINHAS DESCONHECIDAS NA LITERATURA
        If RadioButtonDA1.Checked = True Then
            'OPÇÃO PARA O DIAGRAMA APENAS DAS LINHAS CONHECIDA
            For i As Integer = 0 To NLINHAS2
                'CRIANDO SETAS PARA PRIMEIRA TRANSIÇÃO

                If (Ei(i) = Ei(0)) And (IR(i) <> 0) Then
                    'PULAR 2 LINHAS PARA LEITURA DO INDEX GNUPLOT
                    DATAFILEOUT2.WriteLine("")
                    DATAFILEOUT2.WriteLine("")
                    If RadioButtonVacAD.Checked = True Then
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))
                        'DATAFILEOUTEXE2.WriteLine("replot 'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))
                    Else
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($2):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))

                    End If

                    'PARA DIAGRAMA DE PRIMEIRO PASSO
                    If FormMain.CheckBox2fot.Checked = False Then
                        COMANDOGNU = COMANDOGNU & ", " & (Replace(Ef(i).ToString("0.0000"), ",", ".") & " lc " & (ctcolor))

                    End If

                    'MUDANÇA DE COR DE SETAS
                    ctcolor = ctcolor + 1
                    ctindex = ctindex + 1
                    'DADOS PARA O ARQUIVO DE GRAFICOS: LVAC, LAIR, EI, EF E IR.
                    DATAFILEOUT2.WriteLine(Replace(Lvac(i).ToString("0.000000"), ",", ".") & " " & Replace(Lair(i).ToString("0.000000"), ",", ".") & " " & Replace(Ei(i).ToString("0.0000"), ",", ".") & " " & Replace(Ef(i).ToString("0.0000"), ",", ".") & " " & Replace(IR(i).ToString("0.00"), ",", "."))

                    'CRIANDO SETAS PARA SEGUNDA TRANSIÇÃO
                ElseIf (Ei(i) <> Ei(0)) And (Ei(i) <> LEVEL) And (IR(i) <> 0) Then
                    LEVEL = Ei(i)

                    'CONFIGURANDO ARQUIVO PARA LAMBDA NO VACUO OU NO AR
                    If RadioButtonVacAD.Checked = True Then
                        COMANDOGNU = COMANDOGNU & ", " & (" 'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor2))
                    Else
                        COMANDOGNU = COMANDOGNU & ", " & (" 'DATAGRAPH1.txt' index " & ctindex & " using ($2):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor2))
                    End If

                    'PULAR 2 LINHAS PARA LEITURA DO INDEX GNUPLOT
                    DATAFILEOUT2.WriteLine("")
                    DATAFILEOUT2.WriteLine("")

                    COMANDOGNU = COMANDOGNU & ", " & (Replace(Ei(i).ToString("0.0000"), ",", ".") & " lc " & (ctcolor2))

                    'MUDANÇA DE COR DE SETAS
                    ctcolor2 = ctcolor2 + 1
                    ctindex = ctindex + 1


                    'DADOS PARA O ARQUIVO DE GRAFICOS: LVAC, LAIR, EI, EF E IR.
                    DATAFILEOUT2.WriteLine(Replace(Lvac(i).ToString("0.000000"), ",", ".") & " " & Replace(Lair(i).ToString("0.000000"), ",", ".") & " " & Replace(Ei(i).ToString("0.0000"), ",", ".") & " " & Replace(Ef(i).ToString("0.0000"), ",", ".") & " " & Replace(IR(i).ToString("0.00"), ",", "."))

                End If

            Next
        Else

            'OPÇÃO PARA O DIAGRAMA PARA TODAS AS POSSIVEIS LINHAS
            For i As Integer = 0 To NLINHAS2
                'CRIANDO SETAS PARA PRIMEIRA TRANSIÇÃO
                If (Ei(i) = Ei(0)) Then
                    'PULAR 2 LINHAS PARA LEITURA DO INDEX GNUPLOT
                    DATAFILEOUT2.WriteLine("")
                    DATAFILEOUT2.WriteLine("")

                    If RadioButtonVacAD.Checked = True Then
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))
                        'DATAFILEOUTEXE2.WriteLine("replot 'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))
                    Else
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($2):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor))

                    End If

                    COMANDOGNU = COMANDOGNU & ", " & (Replace(Ef(i).ToString("0.0000"), ",", ".") & " lc " & (ctcolor))

                    'MUDANÇA DE COR DE SETAS
                    ctcolor = ctcolor + 1
                    ctindex = ctindex + 1

                    'CRIANDO SETAS PARA SEGUNDA TRANSIÇÃO
                ElseIf (Ei(i) <> Ei(0)) And (Ei(i) <> LEVEL) Then
                    LEVEL = Ei(i)

                    'CONFIGURANDO ARQUIVO PARA LAMBDA NO VACUO OU NO AR
                    If RadioButtonVacAD.Checked = True Then
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($1):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor2))
                    Else
                        COMANDOGNU = COMANDOGNU & ", " & ("'DATAGRAPH1.txt' index " & ctindex & " using ($2):($3):(0):($4-$3) t '' with vector lt 1 lw 1 lc " & (ctcolor2))
                    End If

                    'PULAR 2 LINHAS PARA LEITURA DO INDEX GNUPLOT
                    DATAFILEOUT2.WriteLine("")
                    DATAFILEOUT2.WriteLine("")

                    'MUDANÇA DE COR DE SETAS
                    ctcolor2 = ctcolor2 + 1
                    ctindex = ctindex + 1


                End If

                'DADOS PARA O ARQUIVO DE GRAFICOS: LVAC, LAIR, EI, EF E IR.
                DATAFILEOUT2.WriteLine(Replace(Lvac(i).ToString("0.000000"), ",", ".") & " " & Replace(Lair(i).ToString("0.000000"), ",", ".") & " " & Replace(Ei(i).ToString("0.0000"), ",", ".") & " " & Replace(Ef(i).ToString("0.0000"), ",", ".") & " " & Replace(IR(i).ToString("0.00"), ",", "."))
            Next
        End If

        DATAFILEOUTEXE2.WriteLine(COMANDOGNU)
        DATAFILEOUTEXE2.WriteLine("set terminal windows font 'Arial,12'")
        DATAFILEOUTEXE2.WriteLine("replot")
        'FECHANDO ARQUIVO
        COMANDOGNU = ""
        DATAFILEOUT2.Close()
        DATAFILEOUTEXE2.Close()

        'EXECUTANDO GNUPLOT
        Shell("cmd /k echo load 'DATAGRAPHEXE.gnu'|pgnuplot -persist", AppWinStyle.Hide)
        FormGraph.Text = "OPTIONS - Arrows Diagram - " & Me.Text
        FormGraph.Show()

    End Sub

    'CONSTRUÇÃO DO ESPECTRO SIMULADO
    Private Sub ButtonPlotSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPlotSS.Click
        'FECHAMENTO DO GNUPLOT E FORMGRAPH
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)
        FormGraph.Close()

        'CONFIGURAÇÃO FORMGRAPH
        FormGraph.ButtonDataSS.Enabled = True

        'MUDANDO TIPO CURSOR
        Me.Cursor = Cursors.AppStarting

        'ABRINDO ARQUIVO
        DATAFILEOUT3 = New IO.StreamWriter(PATHINDEX & "\DATAGRAPH1.txt")

        'ORGANIZAR PARÂMETROS DE ENTRADA
        FWHMstr = Trim(TextBoxFWHM.Text)
        FWHMstr = Replace(FWHMstr, ",", ".")
        TextBoxFWHM.Text = FWHMstr

        FWHM = Val(FWHMstr)

        If (FWHM = 0) Or (LabelFWHM.Text = "") Then
            'DESABILITANDO FWHM=0
            MessageBox.Show("Invalid value of FHWM!", "", MessageBoxButtons.OK)
            Me.Cursor = Cursors.Default
            DATAFILEOUT3.Close()
            Return
        End If


        'CALCULO NO NÚMERO DE PONTOS DO ESPECTRO
        'OBS: ESTE NUMERO NÃO PODE EXCEDER 250000 PONTOS
        NPOINTS = (LIMSUP - LIMINF + 2) / (FWHM / 10)

        If (NPOINTS >= 250000) Then
            FWHM = FWHM * (LIMSUP - LIMINF) / 100
            NPOINTS = 249990
        End If

        LAMBDASS(1) = LIMINF

        For i As Integer = 2 To (NPOINTS)
            LAMBDASS(i) = LAMBDASS(i - 1) + FWHM / 10
        Next

        'DIFERENCIANDO AS SIMULAÇÕES PARA AS LINHAS CONHECIDAS OU TODAS AS LINHAS

        'FOR - TODAS AS LINHAS
        If RadioButtonSS1.Checked = False Then

            For c3 As Integer = 1 To (NPOINTS)
                IRSS(c3) = 0
                If RadioButtonAirSS.Checked = True Then
                    'ESPECTRO PARA LAMBDA NO AR
                    For c2 As Integer = NLINHAS2 To 0 Step -1
                        DeltaL = LAMBDASS(c3) - Lair(c2)
                        IRSS(c3) = IRSS(c3) + ((IR(c2) + IRMax) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2))
                    Next
                Else
                    'ESPECTRO PARA LAMBDA NO VÁCUO
                    For c2 As Integer = NLINHAS2 To 0 Step -1
                        DeltaL = LAMBDASS(c3) - Lvac(c2)
                        IRSS(c3) = IRSS(c3) + ((IR(c2) + IRMax) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2))
                    Next
                End If
                DATAFILEOUT3.WriteLine(Replace(LAMBDASS(c3).ToString("0.0000"), ",", ".") & " " & Replace(IRSS(c3).ToString("0.00"), ",", "."))
            Next

            'FOR - LINHAS CONHECIDAS 
        Else
            For c3 As Integer = 1 To (NPOINTS)
                IRSS(c3) = 0
                If RadioButtonAirSS.Checked = True Then
                    'ESPECTRO PARA LAMBDA NO AR
                    For c2 As Integer = NLINHAS2 To 0 Step -1
                        DeltaL = LAMBDASS(c3) - Lair(c2)
                        IRSS(c3) = IRSS(c3) + ((IR(c2) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2)))
                    Next
                Else
                    'ESPECTRO PARA LAMBDA NO VÁCUO
                    For c2 As Integer = NLINHAS2 To 0 Step -1
                        DeltaL = LAMBDASS(c3) - Lvac(c2)
                        IRSS(c3) = IRSS(c3) + (IR(c2) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2))
                    Next
                End If
                DATAFILEOUT3.WriteLine(Replace(LAMBDASS(c3).ToString("0.0000"), ",", ".") & " " & Replace(IRSS(c3).ToString("0.00"), ",", "."))
            Next
        End If
        'CRIAÇÃO DO ARQUIVO DE COMANDO DO GNUPLOT
        DATAFILEOUTEXE = New IO.StreamWriter(PATHINDEX & "\DATAGRAPHEXE.gnu")
        DATAFILEOUTEXE.WriteLine("reset")
        DATAFILEOUTEXE.WriteLine("set terminal jpeg size 800,500")
        DATAFILEOUTEXE.WriteLine("set output 'GraphicSS.jpeg'")
        DATAFILEOUTEXE.WriteLine("set xrange[" & Replace((LIMINF).ToString("0.00"), ",", ".") & ":" & Replace((LIMSUP).ToString("0.00"), ",", ".") & "]")
        DATAFILEOUTEXE.WriteLine("set grid")
        DATAFILEOUTEXE.WriteLine("set title 'Simulated Spectrum - " & Me.Text)

        If RadioButtonAirSS.Checked = True Then
            'ESPECTRO PARA LAMBDA NO AR
            DATAFILEOUTEXE.WriteLine("set xlabel 'Wavelength in air (nm)'")
        Else
            'ESPECTRO PARA LAMBDA NO VÁCUO
            DATAFILEOUTEXE.WriteLine("set xlabel 'Wavelength in vacuum (nm)'")
        End If

        DATAFILEOUTEXE.WriteLine("set ylabel 'Relative Intensity (a.u.)'")
        DATAFILEOUTEXE.WriteLine("plot 'DATAGRAPH1.txt' using ($1):($2) t 'Simulated Spectra' with line lc 1 lw 2")

        DATAFILEOUTEXE.WriteLine("set terminal windows font 'Arial,12'")
        DATAFILEOUTEXE.WriteLine("replot")

        'FECHANDO ARQUIVOS
        DATAFILEOUT3.Close()
        DATAFILEOUTEXE.Close()

        'MUDANDO TIPO CURSOR
        Me.Cursor = Cursors.Default

        'ABRINDO DIAGRAMA PELO GNUPLOT 
        Shell("cmd /k echo load 'DATAGRAPHEXE.gnu'|pgnuplot -persist", AppWinStyle.Hide)
        FormGraph.Text = "OPTIONS - Simulated Spectrum - " & Me.Text
        FormGraph.Show()

    End Sub

End Class