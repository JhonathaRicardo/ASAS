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

Imports System.IO
Imports System.IO.IOException
Imports System.Collections
Imports System

Public Class FormResultTE
    Dim PATHINDEX, PATHINDEX2, PATHFILE, PATHLEVELS, TYPESPECTRA As String
    Dim LEVELFILEIN, INTFILEIN As IO.StreamReader
    Dim TEXTLEVEL(4), TEXTLINE(3), COMANDOGNUDIST, COMANDOGNUBOLTZ As String
    ' VARIÁVEL fi ESTA RELACIONADA AO PARAMETRO NO ln DA FUNÇÃO DA TEMPERATURA
    Dim Jn1(250000), En1(250000), LifeTime1(250000), DesvTime1(250000) As Double
    Dim LEVELp(250000), PEtotal(250000), P(250000), CoordX(250000), CoordY(250000), coordYmod(250000), RI(250000), Ji(250000), Jf(250000), Ei(250000), Ef(250000), Lambda(250000), Aif(250000), Bif(250000), DesvB(250000), DesvA(250000), Intensity(250000), UncIntensity(250000), fi(250000), Desvfi(250000) As Double
    Dim Ech, Pch, Pmaior, Z, SQtot, SQexp, R, R2, AddE, AddE2, MedDesvB, MedDesvA, MedDesvI, MedX, MedY, DesvT, denA, A, B, T As Double
    Dim NumberLine(250000), Parity1(250000) As Integer
    Dim C1, LinhasE, LinhasFILE, N, Np, NPoints, ctindex, ctcolor As Integer


    'ARQUIVOS DE SAIDA PARA O GRAFICO DE BOLTZMANN
    'ARQUIVOS DE SAIDA PARA OS GRAFICOS DE SETAS
    Dim DATAFILEOUT, DATAFILEOUT2, DATAFILEOUTEXE As IO.StreamWriter
    'Dim DATAFILEOUT As StreamWriter = File.CreateText("DATAGRAPH.txt")
    'Dim DATAFILEOUTEXE As StreamWriter = File.CreateText("DATAGRAPHEXE.gnu")

    'CONSTANTES
    Dim h As Double = 6.626068E-34 'CONST. DE PLANCK
    Dim c As Double = 29979245800.0 'VELOCIDADE DA LUZ NO VÁCUO
    Dim KB As Double = 1.3806503E-23 ' CONST. DE BOLTZMANN


    Private Sub FormResultTE_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'HABILITANDO FUNÇOES APÓS FECHAMENTO DO PROGRAMA
        FormMain.ButtonCancel.Enabled = True
        FormMain.Enabled = True
    End Sub

    'CARREGANDO FORMRESULT TE
    Private Sub FormResultTE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        FormMain.Enabled = False
        LvResults.View = View.Details
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)
        OpenFileDialogDATA.InitialDirectory = PATHINDEX

        'LEITURA DA FUNÇÃO DOS NÍVEIS DE ENERGIA PARA CALCULO DA FUNÇÃO DE PARTIÇÃO TOTAL
        PATHLEVELS = PATHINDEX & "\DATABASE\LEVELS\Levels " & FormMain.TextBoxAtom.Text & ".txt"
        LEVELFILEIN = New IO.StreamReader(PATHLEVELS)

        c1 = 0

        'LE O ARQUIVO ATÉ ACABAR SUAS LINHAS
        Do While LEVELFILEIN.Peek <> -1
            'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
            TEXTLEVEL = LEVELFILEIN.ReadLine.Split(" ")

            If TEXTLEVEL(0) <> "" Then
                'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                Try
                    'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                    Jn1(c1) = Val(TEXTLEVEL(0))
                    En1(c1) = Val(TEXTLEVEL(1))
                    Parity1(c1) = Val(TEXTLEVEL(2))
                    LifeTime1(c1) = Val(TEXTLEVEL(3))
                    DesvTime1(c1) = Val(TEXTLEVEL(4))
                    c1 = c1 + 1
                    'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                Catch ex As IndexOutOfRangeException
                End Try
            End If
        Loop

        'NUMEROS DE LINHAS DO ARQUIVO DE NÍVEIS DE ENERGIA
        LinhasE = C1 - 1


        c1 = 0
        If PATHINDEX2 <> "" Then
            System.IO.Directory.SetCurrentDirectory(PATHINDEX2)
            OpenFileDialogDATA.InitialDirectory = PATHINDEX2
        End If

        'ZERANDO VARIÁVEIS
        NPoints = 0
        N = Val(LabelNTrans.Text) - 1
        A = 0
        B = 0
        T = 0
        MedDesvA = 0
        MedDesvI = 0
        Pmaior = 0

    End Sub
    'SELECIONANDO ITEM E SUBITENS PARA EDIÇÃO
    Private Sub LvResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LvResults.SelectedIndexChanged
        'DESABILITANDO GROUPBOXINSERT
        GroupBoxInsert.Enabled = True

        If LvResults.SelectedItems.Count > 0 Then
            TextBoxNumber.Text = LvResults.SelectedItems(0).Text
            TextBoxLambda.Text = LvResults.SelectedItems(0).SubItems(1).Text
            TextBoxRI.Text = LvResults.SelectedItems(0).SubItems(2).Text
            TextBoxAif.Text = LvResults.SelectedItems(0).SubItems(3).Text
            TextBoxDesvA.Text = LvResults.SelectedItems(0).SubItems(4).Text
            TextBoxEi.Text = LvResults.SelectedItems(0).SubItems(5).Text
            TextBoxJi.Text = LvResults.SelectedItems(0).SubItems(6).Text
            TextBoxEs.Text = LvResults.SelectedItems(0).SubItems(7).Text
            TextBoxJs.Text = LvResults.SelectedItems(0).SubItems(8).Text


            If LvResults.SelectedItems(0).SubItems(4).Text = "" Then
                TextBoxInt.Clear()
            Else
                TextBoxInt.Text = LvResults.SelectedItems(0).SubItems(9).Text
            End If

            If LvResults.SelectedItems(0).SubItems(5).Text = "" Then
                TextBoxDesvInt.Clear()
            Else
                TextBoxDesvInt.Text = LvResults.SelectedItems(0).SubItems(10).Text
            End If
        End If
    End Sub
    'IMPORTANDO INTENSIDADES APARTIR DE ARQUIVOS
    Private Sub ButtonImportFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonImportFile.Click
        Dim c1, c2 As Integer

        'DESABILITANDO GROUPBOXINSERT
        GroupBoxInsert.Enabled = True
        PATHINDEX2 = ""

        'ABRINDO ARQUIVO
        If OpenFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PATHFILE = OpenFileDialogDATA.FileName
            OpenFileDialogDATA.InitialDirectory = Replace(OpenFileDialogDATA.FileName, OpenFileDialogDATA.SafeFileName, "")
            PATHINDEX2 = OpenFileDialogDATA.InitialDirectory
            'ABRINDO ARQUIVO
            TextBoxOpenFile.Text = OpenFileDialogDATA.FileName

        INTFILEIN = New IO.StreamReader(PATHFILE)
        Do While INTFILEIN.Peek <> -1
            'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
            TEXTLINE = INTFILEIN.ReadLine.Split(" ")

            If TEXTLINE(0) <> "" Then
                'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                Try
                    'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                    NumberLine(c1) = Val(Replace(TEXTLINE(0), ",", "."))
                    Intensity(c1) = Val(Replace(TEXTLINE(1), ",", "."))
                    UncIntensity(c1) = Val(Replace(TEXTLINE(2), ",", "."))
                    c1 = c1 + 1
                    'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                Catch ex As IndexOutOfRangeException
                End Try

            End If
        Loop

        LinhasFILE = c1 - 1

        INTFILEIN.Close()
        ' VERIFICANDO A EXISTENCIA DAS LINHAS NA TABELA
        For c1 = 0 To LinhasFILE

            For c2 = 0 To N
                If LvResults.Items(c2).Text = NumberLine(c1) Then
                    LvResults.Items(c2).SubItems(9).Text = Intensity(c1)
                    LvResults.Items(c2).SubItems(10).Text = UncIntensity(c1)
                End If
            Next
        Next

        Else
            TextBoxOpenFile.Text = ""
        End If

    End Sub
    'BOTÃO PARA FORMATAÇÃO DOS SUBITENS
    Private Sub ButtonSalvar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSalvar.Click

        If LvResults.SelectedItems.Count > 0 Then
            LvResults.SelectedItems(0).SubItems(9).Text = Replace(TextBoxInt.Text, " ", "")
            LvResults.SelectedItems(0).SubItems(10).Text = Replace(TextBoxDesvInt.Text, " ", "")
        End If
    End Sub
    'FECHANDO FORMRESULTTE
    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()

    End Sub
    'CALCULO DA TEMPERATURA ELETRÔNICA
    Private Sub ButtonCalcTemp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalcTemp.Click
        Dim c1, c2 As Integer

        'CRIAÇÃO DO ARQUIVO DE SAIDA PARA A SIMULAÇÃO DE DADOS
        DATAFILEOUT = New IO.StreamWriter("DATAGRAPH.txt")
        DATAFILEOUT2 = New IO.StreamWriter("DATAGRAPH2.txt")

        'LEITURA DAS LINHAS ESCOLHIDAS JOGANDO SEUS VALORES EM VARIÁVEIS PARA O CALCULA DA TEMPERATURA
        For c1 = 0 To N
            'O CALCULO SERÁ REALIZADO APENAS COM AS LINHAS QUE APRESENTAM INTENSIDADE EXPERIMENTAL
            If (LvResults.Items(c1).SubItems(9).Text <> "") Then
                NumberLine(NPoints) = Val(LvResults.Items(c1).Text)
                Lambda(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(1).Text, ",", "."))
                RI(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(2).Text, ",", "."))
                Aif(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(3).Text, ",", "."))
                DesvA(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(4).Text, ",", "."))
                Ei(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(5).Text, ",", "."))
                Ji(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(6).Text, ",", "."))
                Ef(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(7).Text, ",", "."))
                Jf(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(8).Text, ",", "."))

                Intensity(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(9).Text, ",", "."))
                UncIntensity(NPoints) = Val(Replace(LvResults.Items(c1).SubItems(10).Text, ",", "."))

                'DETERMINAR Bif
                Bif(NPoints) = ((2 * Jf(NPoints) + 1) / (2 * Ji(NPoints) + 1)) * (Aif(NPoints) * h * h * c * c) * (0.5 / (Math.Pow(Math.Abs(Ei(NPoints) - Ef(NPoints)), 3)))
                DesvB(NPoints) = (DesvA(NPoints) / Aif(NPoints)) * Bif(NPoints)

                'VARIÁVEIS PARA CALCULOS DE SOMATÓRIAS E MÉDIAS

                If RadioButtonEm.Checked = True Then 'PARA EMISSÃO
                    AddE = AddE + Ef(NPoints) * h * c
                    AddE2 = AddE2 + Ef(NPoints) * Ef(NPoints) * h * h * c * c


                ElseIf RadioButtonAbs.Checked = True Then 'PARA ABSORÇÃO
                    AddE = AddE + Ei(NPoints) * h * c
                    AddE2 = AddE2 + Ei(NPoints) * Ei(NPoints) * h * h * c * c
                End If

                MedDesvA = MedDesvA + DesvA(NPoints) / Aif(NPoints)
                MedDesvB = MedDesvB + DesvB(NPoints) / Bif(NPoints)
                MedDesvI = MedDesvI + UncIntensity(NPoints) / Intensity(NPoints)


                'MsgBox(NumberLine(NPoints) & "   " & Lambda(NPoints) & "   " & Aif(NPoints) & "   " & DesvA(NPoints) & "   " & Jf(NPoints) & "   " & Ef(NPoints) & "   " & Intensity(NPoints) & "   " & UncIntensity(NPoints))
                'ACRESCENTANDO PONTOS NO GRÁFICO PARA CADA LINHA COM INT. EXPERIMENTAL
                NPoints = NPoints + 1
            End If

        Next

        'CALCULO DO DESVIO MÉDIO DO COEFICIENTE DE EMISSÃO,ABSORÇÃO E DA INTENSIDADE
        MedDesvB = MedDesvB / (NPoints)
        MedDesvA = MedDesvA / (NPoints)
        MedDesvI = MedDesvI / (NPoints)

        'CANCELAMENTO DA ROTINA CASO O NUMERO DE PONTOS SEJA NULO
        If NPoints = 0 Then
            MessageBox.Show("Insert Intensity of the Lines!", "", MessageBoxButtons.OK)
            'FECHANDO ARQUIVO
            DATAFILEOUT.Close()
            DATAFILEOUT2.Close()
            MedDesvB = 0
            MedDesvA = 0
            MedDesvI = 0
            AddE = 0
            AddE2 = 0
            NPoints = 0
            Z = 0
            Exit Sub
        End If

        'DESABILITANDO GROUPBOX DE RESULTADOS
        GroupBoxResultTE.Visible = True
        ButtonClear.Visible = True
        ButtonClear.Enabled = True

        'BLOQUEIO DO BOTÃO DO CALCULO
        GroupBoxTypes.Enabled = False
        ButtonCalcTemp.Enabled = False
        ButtonImportFile.Enabled = False
        GroupBoxInsert.Enabled = False

        'CALCULO PARA O ESPECTRO DE EMISSÃO
        If RadioButtonEm.Checked = True Then
            TYPESPECTRA = " - Emission Spectrum"
            'CABEÇALHO DOS ARQUIVOS
            DATAFILEOUT.WriteLine("#Upper Level [1/cm]     Y ")

            'CALCULO DOS PONTOS NO GRAFICO ln(fi) X ENERGIA
            For c2 = 0 To NPoints - 1
                'CALCULO DO PAREMETR fi
                fi(c2) = (Intensity(c2) * Lambda(c2) * 0.000001) / (Aif(c2) * h * c * c * (2 * Jf(c2) + 1))

                'CALCULO DA COORDENADA Y NO GRAFICO ln(fi)
                CoordY(c2) = Math.Log(fi(c2))

                MedY = MedY + CoordY(c2)

                'CALCULO DA COORDENADA X NO GRAFICO ENERGIA
                CoordX(c2) = 1.4388 * Ef(c2)

                MedX = MedX + CoordX(c2)

                'PLOT DOS PONTOS NO GRAFICO
                DATAFILEOUT.WriteLine(Replace(Ef(c2).ToString("0.00000"), ",", ".") & " " & Replace(CoordY(c2).ToString("0.00000"), ",", "."))

            Next

            'CALCULO PARA O ESPECTRO DE ABSORÇÃO

        Else
            'CABEÇALHO DOS ARQUIVOS
            DATAFILEOUT.WriteLine("#Lower Level [1/cm]     Y ")
            TYPESPECTRA = " - Absorption Spectrum"

            'CALCULO DOS PONTOS NO GRAFICO ln(fi) X ENERGIA
            For c2 = 0 To NPoints - 1
                'CALCULO DO PAREMETRO fi
                fi(c2) = (Intensity(c2)) / (Bif(c2) * (2 * Ji(c2) + 1))

                'CALCULO DA COORDENADA Y NO GRAFICO ln(fi)
                CoordY(c2) = Math.Log(fi(c2))

                MedY = MedY + CoordY(c2)

                'CALCULO DA COORDENADA X NO GRAFICO ENERGIA
                CoordX(c2) = 1.4388 * Ei(c2)

                MedX = MedX + CoordX(c2)

                'PLOT DOS PONTOS NO GRAFICO
                DATAFILEOUT.WriteLine(Replace(Ei(c2).ToString("0.00000"), ",", ".") & " " & Replace(CoordY(c2).ToString("0.00000"), ",", "."))
            Next
        End If

        '********************************** MMQ AJUSTE LINEAR Y = A.X + B*************************
        'VALORES MÉDIOS DE X E Y
        MedX = MedX / (NPoints)
        MedY = MedY / (NPoints)

        'MÉDIA DOS VALORES DE ENERGIA SUPERIOR 
        'CALCULO DO COEFICIENTE ANGULAR DA RETA A
        For i As Integer = 0 To NPoints - 1
            A = A + ((CoordX(i) - MedX) * (CoordY(i) - MedY))
            denA = denA + ((CoordX(i) - MedX) * (CoordX(i) - MedX))
        Next
        'TEMPERATURA É DADA POR A*(-1)
        A = A / denA
        T = (-1) * (1 / A)

        'CALCULO DO COEFICIENTE LINEAR DA RETA B
        B = MedY - A * MedX

        'DESVIO DA TEMPERATURA
        If RadioButtonEm.Checked = True Then
            DesvT = (KB * T / (Math.Sqrt(AddE2 - (AddE * AddE) / NPoints))) * (MedDesvA + MedDesvI)
            DesvT = DesvT * T
        Else
            DesvT = (KB * T / (Math.Sqrt(AddE2 - (AddE * AddE) / NPoints))) * (MedDesvB + MedDesvI)
            DesvT = DesvT  * T
        End If

        'VALORES PARA DEFINIÇÃO DE R (COEF. DE DETERMINAÇÃO)
        'DETERMINAR SOMA DOS QADRADOS TOTAL
        For i As Integer = 0 To NPoints - 1
            SQtot = SQtot + (CoordY(i) - MedY) * (CoordY(i) - MedY)
        Next

        'DETERMINAR SOMA DOS QADRADOS EXPLICADA
        For i As Integer = 0 To NPoints - 1
            coordYmod(i) = A * CoordX(i) + B
            SQexp = SQexp + (coordYmod(i) - MedY) * (coordYmod(i) - MedY)
        Next

        R2 = SQexp / SQtot

        R = Math.Sqrt(R2)
        'GRÁFICO DO PLOT DE BOLTZAMANN

        '********************************** DISTRIBUIÇÃO ELETRONICA*************************
        'LIMPANDO TABELA DE RESULTADOS
        LVDistribution.Clear()


        'CRIAÇÃO DAS COLUNAS DAS TABELAS DE DADOS DA DISTRIBUIÇÃO ELETRÔNICA
        LVDistribution.Columns.Add(" Number ", 90)
        LVDistribution.Columns.Add("λ", 90)
        If RadioButtonEm.Checked = True Then
            LVDistribution.Columns.Add("Eu", 90)
        Else
            LVDistribution.Columns.Add("El", 90)
        End If
        LVDistribution.Columns.Add("P(%)", 100)

        'CABEÇALHO DOS ARQUIVOS
        DATAFILEOUT2.WriteLine("#Number  Level[1/cm]   P[%]")

        'CALCULO DA FUNÇÃO DE PARTIÇÃO
        'EMISSÃO
        If RadioButtonEm.Checked = True Then

            For i As Integer = 0 To LinhasE
                Z = Z + (2 * Jn1(i) + 1) * (Math.Exp(-En1(i) * h * c / (KB * T)))
            Next


            'CALCULO DA PROBABILIDADE DE OCUPAÇÃO
            For i As Integer = 0 To NPoints - 1
                Dim LVDistItem As New ListViewItem
                P(i) = (((2 * Jf(i) + 1) * Math.Exp((-Ef(i) * h * c) / (KB * T))) / Z) * 100
                'PREENCHENDO TABELA DE DADOS
                LVDistItem.Text = (NumberLine(i).ToString("000"))
                LVDistribution.Items.Add(LVDistItem)
                LVDistItem.SubItems.Add(Lambda(i).ToString("0.000"))
                LVDistItem.SubItems.Add(Ef(i).ToString("0.00"))
                LVDistItem.SubItems.Add((P(i).ToString("0.000000")))

            Next

            'ORDENANDO EM ORDEM DE NÍVEL DE ENERGIA
            For j As Integer = 0 To NPoints - 1
                For i As Integer = 0 To NPoints - 1
                    Do While (Ef(i) > Ef(j)) And (i <> j)
                        Ech = Ef(i)
                        Ef(i) = Ef(j)
                        Ef(j) = Ech
                        Pch = P(i)
                        P(i) = P(j)
                        P(j) = Pch
                    Loop
                Next
            Next
            Np = 0

            LEVELp(0) = Ef(0)
            'VERIFICANDO DIFERENTES NÍVEIS E SOMANDO PROBABILIDADE DE NIVEIS IGUAIS
            For i As Integer = 0 To NPoints - 1
                If (LEVELp(Np) = Ef(i)) Then
                    PEtotal(Np) = P(i)

                    If Pmaior < PEtotal(Np) Then
                        Pmaior = PEtotal(Np)
                    End If

                ElseIf (LEVELp(Np) <> Ef(i)) Then
                    Np = Np + 1
                    LEVELp(Np) = Ef(i)
                    PEtotal(Np) = P(i)
                    If Pmaior < PEtotal(Np) Then
                        Pmaior = PEtotal(Np)
                    End If
                End If
            Next
            For i As Integer = 0 To Np
                If (i = 0) Then
                    COMANDOGNUDIST = "plot 'DATAGRAPH2.txt' index 0 using ($2):($3) t '" & Replace(LEVELp(i).ToString("0.0000"), ",", ".") & " (" & Replace(PEtotal(i).ToString("0.000000"), ",", ".") & "%)' with boxes lc " & (i) & " lw 3"
                Else
                    COMANDOGNUDIST = COMANDOGNUDIST & ", " & ("'DATAGRAPH2.txt' index " & i & " using ($2):($3) t '" & Replace(LEVELp(i).ToString("0.0000"), ",", ".") & " (" & Replace(PEtotal(i).ToString("0.000000"), ",", ".") & "%)' with boxes lc " & (i) & " lw 3")
                End If
            Next

            ' SAIDA DE ARQUIVO
            For i As Integer = 0 To Np
                DATAFILEOUT2.WriteLine((i + 1) & " " & Replace((LEVELp(i)).ToString("0.000000"), ",", ".") & " " & Replace(PEtotal(i).ToString("0.000"), ",", "."))
                DATAFILEOUT2.WriteLine("")
                DATAFILEOUT2.WriteLine("")
            Next

            'PARA ABSORÇÃO
        ElseIf RadioButtonAbs.Checked = True Then
            For i As Integer = 0 To LinhasE
                Z = Z + (2 * Jn1(i) + 1) * (Math.Exp(-En1(i) * h * c / (KB * T)))
            Next

            'CALCULO DA PROBABILIDADE DE OCUPAÇÃO
            For i As Integer = 0 To NPoints - 1
                Dim LVDistItem As New ListViewItem
                P(i) = (((2 * Ji(i) + 1) * Math.Exp(-Ei(i) * h * c / (KB * T))) / Z) * 100
                'PREENCHENDO TABELA DE DADOS
                LVDistItem.Text = (NumberLine(i).ToString("000"))
                LVDistribution.Items.Add(LVDistItem)
                LVDistItem.SubItems.Add(Lambda(i).ToString("0.000"))
                LVDistItem.SubItems.Add(Ei(i).ToString("0.00"))
                LVDistItem.SubItems.Add(P(i).ToString("0.000000"))

            Next

            'ORDENANDO EM ORDEM DE NÍVEL DE ENERGIA
            For j As Integer = 0 To NPoints - 1
                For i As Integer = 0 To NPoints - 1
                    Do While (Ei(i) > Ei(j)) And (i <> j)
                        Ech = Ei(i)
                        Ei(i) = Ei(j)
                        Ei(j) = Ech
                        Pch = P(i)
                        P(i) = P(j)
                        P(j) = Pch
                    Loop
                Next
            Next

            Np = 0

            LEVELp(0) = Ei(0)
            'VERIFICANDO DIFERENTES NÍVEIS E SOMANDO PROBABILIDADE DE NIVEIS IGUAIS
            For i As Integer = 0 To NPoints - 1
                If (LEVELp(Np) = Ei(i)) Then
                    PEtotal(Np) = P(i)

                    If Pmaior < PEtotal(Np) Then
                        Pmaior = PEtotal(Np)
                    End If

                ElseIf (LEVELp(Np) <> Ei(i)) Then
                    
                    Np = Np + 1
                    LEVELp(Np) = Ei(i)
                    PEtotal(Np) = P(i)

                    If Pmaior < PEtotal(Np) Then
                        Pmaior = PEtotal(Np)
                    End If
                End If
            Next
            For i As Integer = 0 To Np
                If (i = 0) Then
                    COMANDOGNUDIST = "plot 'DATAGRAPH2.txt' index 0 using ($2):($3) t '" & Replace(LEVELp(i).ToString("0.0000"), ",", ".") & " (" & Replace(PEtotal(i).ToString("0.000000"), ",", ".") & "%)' with boxes lc " & (i) & " lw 3"
                Else
                    COMANDOGNUDIST = COMANDOGNUDIST & ", " & ("'DATAGRAPH2.txt' index " & i & " using ($2):($3) t '" & Replace(LEVELp(i).ToString("0.0000"), ",", ".") & " (" & Replace(PEtotal(i).ToString("0.000000"), ",", ".") & "%)' with boxes lc " & (i) & " lw 3")
                End If
            Next

            ' SAIDA DE ARQUIVO
            For i As Integer = 0 To Np
                DATAFILEOUT2.WriteLine((i + 1) & " " & Replace((LEVELp(i)).ToString("0.0000"), ",", ".") & " " & Replace(PEtotal(i).ToString("0.000000"), ",", "."))
                DATAFILEOUT2.WriteLine("")
                DATAFILEOUT2.WriteLine("")
            Next

        End If

        'RESULTADOS DE TEMPERATURA E COEF. R2
        LabelT.Text = Replace(T.ToString("0"), ",", ".") & " ± " & Replace(DesvT.ToString("0"), ",", ".") & " K"
        LabelR2.Text = Replace(R2.ToString("0.00"), ",", ".")
        LabelZ.Text = Replace(Z.ToString("0.00"), ",", ".")

        
        'FECHANDO ARQUIVOS
        DATAFILEOUT.Close()
        DATAFILEOUT2.Close()

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxInt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxInt.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If

        End If
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxDesvInt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxDesvInt.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub ButtonBoltzPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBoltzPlot.Click

        'CRIAÇÃO DO ARQUIVO DE COMANDO DO GNUPLOT
        DATAFILEOUTEXE = New IO.StreamWriter(PATHINDEX & "\DATAGRAPHEXE.gnu")
        DATAFILEOUTEXE.WriteLine("reset")
        DATAFILEOUTEXE.WriteLine("set terminal jpeg size 800,500")
        DATAFILEOUTEXE.WriteLine("set output 'GraphicSS.jpeg'")
        DATAFILEOUTEXE.WriteLine("set grid")
        DATAFILEOUTEXE.WriteLine("set title ' Boltzmann Plot - " & Me.Text & TYPESPECTRA & " '")
        DATAFILEOUTEXE.WriteLine("set xlabel 'Energy of the Level (1/cm)'")
        DATAFILEOUTEXE.WriteLine("set ylabel 'Y'")

        DATAFILEOUTEXE.WriteLine("f1(x) = a1*x + b1")
        DATAFILEOUTEXE.WriteLine("a1 = " & Replace(A.ToString("0.0000000000"), ",", ".") & "; b1 = " & Replace(B.ToString("0.0000000000"), ",", "."))
        DATAFILEOUTEXE.WriteLine("fit f1(x) 'DATAGRAPH.txt' using ($1):($2) via a1,b1")

        DATAFILEOUTEXE.WriteLine("plot f1(x) t 'Fit Linear [T = " & Replace(T.ToString("0"), ",", ".") & " +/- " & Replace(DesvT.ToString("0"), ",", ".") & " K (R^2 = " & Replace(R2.ToString("0.00"), ",", ".") & ")]','DATAGRAPH.txt' t '(Ei;Y)' w points pt 9 lc 5")
        DATAFILEOUTEXE.WriteLine("set terminal windows font 'Arial,12'")
        DATAFILEOUTEXE.WriteLine("replot")

        'FECHANDO ARQUIVOS
        DATAFILEOUTEXE.Close()

        'ABRINDO GRAFICO DE BOLTZMANN
        Shell("cmd /k echo load 'DATAGRAPHEXE.gnu'|pgnuplot -persist", AppWinStyle.Hide)
        FormGraph.Text = "OPTIONS - Plot Boltzmann -" & Me.Text & TYPESPECTRA
        FormGraph.Show()
    End Sub

    Private Sub ButtonDistPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDistPlot.Click
        'GRÁFICO DA DISTRIBUIÇÃO POPULACIONAL
        'CRIAÇÃO DO ARQUIVO DE COMANDO DO GNUPLOT
        DATAFILEOUTEXE = New IO.StreamWriter(PATHINDEX & "\DATAGRAPHEXE.gnu")
        DATAFILEOUTEXE.WriteLine("reset")
        DATAFILEOUTEXE.WriteLine("set terminal jpeg size 800,500")
        DATAFILEOUTEXE.WriteLine("set output 'GraphicSS.jpeg'")
        DATAFILEOUTEXE.WriteLine("set title 'Electronic Distribution'")
        DATAFILEOUTEXE.WriteLine("set grid")
        DATAFILEOUTEXE.WriteLine("set xrange [" & ((LEVELp(0) - 1000).ToString("0") & ":" & (LEVELp(Np) + 7000).ToString("0") & "]"))
        DATAFILEOUTEXE.WriteLine("set yrange [0:" & Replace((Pmaior + Pmaior * 0.05).ToString("0.00000"), ",", ".") & "]")


        DATAFILEOUTEXE.WriteLine("set title ' " & Replace(Me.Text, "Temperature", "Population") & TYPESPECTRA & " '")
        DATAFILEOUTEXE.WriteLine("set xlabel 'Energy of th Level (1/cm)'")
        DATAFILEOUTEXE.WriteLine("set ylabel 'Probability (%)'")
        DATAFILEOUTEXE.WriteLine(COMANDOGNUDIST)
        DATAFILEOUTEXE.WriteLine("set terminal windows font 'Arial,12'")
        DATAFILEOUTEXE.WriteLine("replot")

        'FECHANDO ARQUIVOS
        DATAFILEOUTEXE.Close()

        'ABRINDO GRAFICO DE BARRAS DE DISTRIBUIÇÃO
        Shell("cmd /k echo load 'DATAGRAPHEXE.gnu'|pgnuplot -persist", AppWinStyle.Hide)
        FormGraph.Text = "OPTIONS - Electronic Population Plot - " & Me.Text & TYPESPECTRA
        FormGraph.Show()
    End Sub

    Private Sub ButtonSaveTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveTable.Click
        SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\ELECTRONIC TEMPERATURE"
        SaveFileDialogDATA.FileName = ""

        If SaveFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DATAFILEOUT = New IO.StreamWriter(SaveFileDialogDATA.FileName)

            Try
                If LvResults.Items.Count <= 0 Then Throw New Exception
                'LEITURA DAS LINHAS DO LISTVIEW LVRESULT
                For Index As Integer = 0 To LvResults.Items.Count - 1
                    'CRIANDO NOVA LINHA DE DADOS APENAS PARA AS LINHAS COM INTENSIDADE
                    If (Val(LvResults.Items(Index).SubItems(9).Text) <> 0) Then
                        DATAFILEOUT.WriteLine(Replace(LvResults.Items(Index).SubItems(0).Text, ",", ".") & " " & Replace(LvResults.Items(Index).SubItems(9).Text, ",", ".") & " " & Replace(LvResults.Items(Index).SubItems(10).Text, ",", "."))
                    End If
                Next

            Catch ex As Exception
                MsgBox("Error saving the  text file!" & ex.Message)
            Finally
                DATAFILEOUT.Close()
            End Try

        End If
    End Sub
    'LIMPEZA DE DADOS
    Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        'ZERANDO VARIÁVEIS
        COMANDOGNUDIST = ""
        NPoints = 0
        N = Val(LabelNTrans.Text) - 1
        A = 0
        denA = 0
        B = 0
        T = 0
        R = 0
        R2 = 0
        Z = 0
        MedX = 0
        MedY = 0
        DesvT = 0
        SQtot = 0
        SQexp = 0
        MedDesvA = 0
        MedDesvI = 0
        Pmaior = 0
        MedDesvB = 0
        MedDesvA = 0
        MedDesvI = 0
        AddE = 0
        AddE2 = 0
        NPoints = 0
        TextBoxOpenFile.Text = ""
        'LIMPEZA DO LISTVIEW
        For i As Integer = 0 To N
            LvResults.Items(i).SubItems(9).Text = ""
            LvResults.Items(i).SubItems(10).Text = ""
            PEtotal(i) = 0
        Next

        'DEIXANDO GROUPBOX e botões INVISIVEIS
        GroupBoxTypes.Enabled = True
        GroupBoxResultTE.Visible = False
        GroupBoxInsert.Enabled = True
        ButtonCalcTemp.Enabled = True
        ButtonImportFile.Enabled = True
        ButtonClear.Enabled = False
    End Sub

End Class