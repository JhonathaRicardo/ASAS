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
Imports System
Imports System.IO
Imports System.IO.IOException
Imports System.Collections
Public Class FormMain

    Shared Property enable As Boolean

    'EQUAÇÃO DE CIDDOR TRANSFORMAÇÃO DO COMPRIMENTO DE ONDA DO VACUO PARA O AR
    Public Function Ciddor(ByVal x As Double) As Double
        Return 1.00035396 - x * 0.000000419823 + x ^ 2 * 0.000000000838492 - x ^ 3 * 0.000000000000777411 + x ^ 4 * 0.000000000000000276385
    End Function
    'ARQUIVOS DE ENTRADA
    Dim LEVELFILEIN, LINEFILEIN As IO.StreamReader
    'ARQUIVOS DE SAIDA PARA OS GRAFICOS DE SETAS
    Dim DATAFILEOUT, DATAFILEOUT2, DATAFILEOUTEXE As IO.StreamWriter
    'Dim DATAFILEOUT As StreamWriter = File.CreateText("DATAGRAPH.txt")
    'Dim DATAFILEOUTEXE As StreamWriter = File.CreateText("DATAGRAPHEXE.gnu")
    'VARIÁVEIS ALFANUMÉRICAS
    Dim TYPEMED, ATOM, PATHINDEX, PATHLINES, PATHLEVELS, STRTYPESPEC As String
    Dim FWHMstr, LEVELstr, LIMINF2str, LIMSUP2str, LIMSUPstr, LIMINFstr As String
    Dim TEXTLINE(9), TEXTLEVEL(4) As String
    'VARIÁVEIS NUMÉRICAS
    Dim P, DeltaE, DeltaJ, DeltaL, LEVEL, LIMINF2, LIMSUP2, J, LIMINF, LIMSUP, FWHM As Double

    'VARIÁVEL PARA O TIPO DA FUNÇÃO
    '1 - TRANS. FORM A LEVEL
    '2 - POS. TRANS.
    '3 - SPECT. SIMULATOR
    '4 - TEMP. ELETRON
    Dim TYPEFUNC As Integer
    'VETORES DE SAÍDA DE DADOS
    Dim Parity(250000), N, Nsec As Integer
    Dim SecJLevel(250000), SecLevel(250000), LAMBDALT(250000), LAMBDASS(250000), LAMBDAPT(250000), Lvac(250000), Lair(250000), IR(250000), Aif(250000), DesvA(250000), Ei(250000), Ji(250000), Ef(250000), Jf(250000), DesvTime(250000), LifeTime(250000) As Double
    'VETORES PARA ORDENAR EM ORDEM AS LINHAS DA ROTINA POSSIVEIS TRANSIÇÕES
    Dim LMAIOR(250000), N3(250000), Lvac3(250000), Lair3(250000), IR3(250000), Aif3(250000), DesvA3(250000), Ei3(250000), Ji3(250000), Ef3(250000), Jf3(250000) As Double
    Dim N4, Lvac4, Lair4, IR4, Aif4, DesvA4, Ei4, Ji4, Ef4, Jf4 As Double 'VARIÁVEIS AUXILIARES PARA ORGANIZAÇÃO

    'VETORES DE ENTRADA DAS LINHAS N/LAMBDA VAC./LAMBDA AIR/IR/COEF. DE EMISSÃO/ DESVIO DO COEF./EI/JI/EF/JF
    Dim N2(250000), N1(250000) As Integer
    Dim Lvac2(250000), Lair2(250000), IR2(250000), Aif2(250000), DesvA2(250000), Ei2(250000), Ji2(250000), Ef2(250000), Jf2(250000) As Double

    'VETORES DE ENTRADA DOS NÍVEIS J/E/PARITY/DESDOBRAMENTO E/ TEMPO DE VIDA
    Dim Parity2(250000), Parity1(250000) As Integer
    Dim En2(250000), Jn2(250000), En1(250000), Jn1(250000), DesvTime1(250000), LifeTime1(250000) As Double


    'INICIO DE COMANDOS
    Private Sub FormMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'LOCALIZANDO PATH PARA UTILIZAÇÃO DO BANCO DE DADOS
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)

        'LIMPANDO TABELA DE RESULTADOS
        FormResultPT.LvResults.Columns.Clear()
        N = 0

        'PERMISSÃO TOTAAL AOS USUÁRIOS
        'Shell("cacls """ & PATHINDEX & """ /E /G users", AppWinStyle.Hide)

    End Sub

    Private Sub ButtonPT_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPT.MouseHover
        GroupBoxDesc.Text = "TRANSITIONS FROM A LEVEL"
        RichTextBoxDe.Text = "This routine identify possible transitions from a particular energy level considering the energy levels parity and total angular momentum J selection rules for dipole momentum transitions (absorption or emission of electromagnetic radiation)." & System.Environment.NewLine.ToString() & "Requires: levels.txt and lines.txt databases."
    End Sub

    Private Sub ButtonTL_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonTL.MouseHover
        GroupBoxDesc.Text = "POSSIBLE TRANSITIONS"
        RichTextBoxDe.Text = "This routine identify possible transitions from any initial energy level to any other energy level within a defined region of the spectrum. Applying energy levels parity and total angular momentum J selection rules for dipole momentum transitions (absorption or emission of electromagnetic radiation)." & System.Environment.NewLine.ToString() & "Requires: levels.txt and lines.txt databases."
    End Sub
    Private Sub ButtonSS_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSS.MouseHover
        GroupBoxDesc.Text = "SPECTRA SIMULATION"
        RichTextBoxDe.Text = "This routine simulates spectra (atoms or molecules) in the spectral range and linewidth defined by the user employing Lorentz lineshapes. The spectra can be emission or absorption on the input data." & System.Environment.NewLine.ToString() & "Requires: lines.txt databases or user txt files."
    End Sub

    Private Sub ButtonBoltz_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonET.MouseHover
        GroupBoxDesc.Text = "ELECTRONIC TEMPERATURE"
        RichTextBoxDe.Text = "This routine calculates the electronic temperature of atoms in an experimental environment through the Boltzmann plot." & System.Environment.NewLine.ToString() & "Requires: lines.txt database."
    End Sub
    'ROTINA POSSÍVEIS TRANSIÇÕES
    Private Sub ButtonPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPT.Click
        'TIPO DA FUNÇÃO
        TYPEFUNC = 1

        GroupBoxMain.Text = "TRANSITIONS FROM A LEVEL"
        'CONFIGURAÇÕES DA ROTINA

        'TORNANDO PARAMETROS DA FUNÇÃO VISÍVEIS
        GroupBoxOptUser.Visible = False
        GroupBoxAtom.Visible = True
        GroupBoxFilters.Visible = True
        GroupBoxLimits.Visible = True
        CheckBox2fot.Visible = True
        GroupBoxGS.Visible = False
        GroupBoxTypes.Visible = True

        'HABILITANDO GROUPBOX
        RadioButtonAir.Visible = False
        RadioButtonVac.Visible = False
        GroupBoxFunc.Enabled = False
        GroupBoxMain.Enabled = True
        GroupBoxGS.Enabled = False
        GroupBoxFilters.Enabled = True
        GroupBoxAtom.Enabled = True
        GroupBoxSecfoton.Enabled = True
        GroupBoxLimits.Enabled = True


        'HABILITANDO BOTÕES
        ButtonCancel.Enabled = True
        ButtonSetPar.Enabled = True
        ButtonExe.Enabled = False
        'MUDANÇA DE NOME DO GBOX LIMITES
        GroupBoxLimits.Text = "Spectral Range"
        LabelLimI.Visible = True
        LabelLimS.Visible = True
        LabelunitLI.Visible = True
        LabelunitLS.Visible = True
        TextBoxLimS.Visible = True
        TextBoxLimI.Visible = True

        'HABILITANDO CONFIGURAÇÕES ESPECÍFICAS
        LabelLevel.Text = "Energy Level"
        Labelunit.Text = "(1/cm)"
        CheckBox2fot.Enabled = True
        CheckBoxRI.Enabled = False


    End Sub
    'ROTINA LOCALIZADOR DE TRANSIÇÕES
    Private Sub ButtonTL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTL.Click
        'TIPO DA FUNÇÃO
        TYPEFUNC = 2

        GroupBoxMain.Text = "POSSIBLE TRANSITIONS"
        'CONFIGURAÇÕES DA ROTINA
        'TORNANDO PARAMETROS DA FUNÇÃO VISÍVEIS
        GroupBoxOptUser.Visible = False
        GroupBoxAtom.Visible = True
        GroupBoxFilters.Visible = True
        GroupBoxLimits.Visible = True
        GroupBoxGS.Visible = False

        'HABILITANDO GROUPBOX
        RadioButtonAir.Visible = False
        RadioButtonVac.Visible = False
        GroupBoxFunc.Enabled = False
        GroupBoxMain.Enabled = True
        GroupBoxFilters.Enabled = True
        GroupBoxAtom.Enabled = True
        CheckBox2fot.Enabled = False
        GroupBoxSecfoton.Visible = False
        GroupBoxLimits.Enabled = True


        'HABILITANDO BOTÕES
        ButtonSetPar.Enabled = True
        ButtonCancel.Enabled = True
        ButtonExe.Enabled = False

        'HABILITANDO CONFIGURAÇÕES ESPECÍFICAS
        LabelLevel.Text = ""
        Labelunit.Text = ""
        
        TextBoxLevel.Visible = False
        'MUDANÇA DE NOME DO GBOX LIMITES
        GroupBoxLimits.Text = "Spectral Range" & System.Environment.NewLine.ToString() & "(Maximum: 10 nm)"
        LabelLimI.Visible = True
        LabelLimS.Visible = True
        LabelunitLI.Visible = True
        LabelunitLS.Visible = True
        TextBoxLimS.Visible = True
        TextBoxLimI.Visible = True

    End Sub
    'ROTINA SIMULADOR ESPECTRAL
    Private Sub ButtonSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSS.Click
        'TIPO DA FUNÇÃO
        TYPEFUNC = 3

        GroupBoxMain.Text = "SPECTRA SIMULATION"
        'CONFIGURAÇÕES DA ROTINA

        'TORNANDO PARAMETROS DA FUNÇÃO VISÍVEIS
        GroupBoxOptUser.Visible = True
        RadioButtonAir.Visible = True
        RadioButtonVac.Visible = True
        GroupBoxAtom.Visible = True
        GroupBoxLimits.Visible = True
        GroupBoxGS.Visible = True
        GroupBoxOpenFile.Visible = True

        'HABILITANDO GROUPBOX
        GroupBoxOpenFile.Enabled = False
        GroupBoxFunc.Enabled = False
        GroupBoxMain.Enabled = True
        GroupBoxGS.Enabled = True
        GroupBoxFilters.Enabled = True
        GroupBoxAtom.Enabled = True
        CheckBox2fot.Enabled = False
        GroupBoxSecfoton.Visible = False
        GroupBoxLimits.Enabled = True
        GroupBoxFilters.Enabled = False

        'HABILITANDO BOTÕES
        ButtonOpenFile.Enabled = True
        ButtonSetPar.Enabled = True
        ButtonCancel.Enabled = True
        RadioButtonAir.Visible = True
        RadioButtonVac.Visible = True
        ButtonExe.Enabled = False

        'MUDANÇA DE NOME DO GBOX LIMITES
        GroupBoxLimits.Text = "Spectral Range"
        LabelLimI.Visible = True
        LabelLimS.Visible = True
        LabelunitLI.Visible = True
        LabelunitLS.Visible = True
        TextBoxLimS.Visible = True
        TextBoxLimI.Visible = True

        'HABILITANDO CONFIGURAÇÕES ESPECÍFICAS
        LabelLevel.Text = "FWHM"
        Labelunit.Text = "(nm)"

        'TROCA DE TIPO DE ARQUIVO ESCOLHIDO PELO USUÁRIO
        If RadioButtonDatabase.Checked = True Then
            GroupBoxOpenFile.Enabled = False
            TextBoxAtom.Enabled = True
        Else
            'DESABILITAR O USO DO BANCO DE DADOS
            TextBoxAtom.Text = ""
            GroupBoxOpenFile.Enabled = True
            TextBoxAtom.Enabled = False
        End If

    End Sub
    'UTILIZAÇÃO DO GROUPBOX DO SEG. FÓTON
    Private Sub CheckBox2fot_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2fot.CheckedChanged
        If CheckBox2fot.Checked = True Then
            GroupBoxSecfoton.Visible = True
            TextBoxLimI2.Text = TextBoxLimI.Text
            TextBoxLimS2.Text = TextBoxLimS.Text
            'TRAVAR OPÇÃO DE ESPECTRO DE EMISSÃO
            RadioButtonEm.Enabled = False
            RadioButtonAbs.Checked = True
            'DESABILITAR SIMULAÇÃO DE ESPECTRO
            FormResultPT.GroupBoxSS.Enabled = False
        Else
            GroupBoxSecfoton.Visible = False
            RadioButtonEm.Enabled = True
            FormResultPT.GroupBoxSS.Enabled = True
        End If
    End Sub

    'ROTINA TEMP. ELETRONICA
    Private Sub ButtonET_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonET.Click
        'TIPO DA FUNÇÃO
        TYPEFUNC = 4

        GroupBoxMain.Text = "ELECTRONIC TEMPERATURE"
        'CONFIGURAÇÕES DA ROTINA

        'TORNANDO PARAMETROS DA FUNÇÃO VISÍVEIS
        GroupBoxOptUser.Visible = False
        GroupBoxFilters.Visible = False
        GroupBoxLimits.Visible = True
        GroupBoxGS.Visible = False
        GroupBoxAtom.Visible = True


        'HABILITANDO GROUPBOX
        GroupBoxFunc.Enabled = False
        GroupBoxMain.Enabled = True
        GroupBoxGS.Enabled = False
        GroupBoxFilters.Enabled = False
        GroupBoxAtom.Enabled = True
        CheckBox2fot.Enabled = False
        GroupBoxSecfoton.Visible = False
        GroupBoxLimits.Enabled = True

        'HABILITANDO BOTÕES
        ButtonSetPar.Enabled = True
        ButtonCancel.Enabled = True
        ButtonExe.Enabled = False

        'HABILITANDO CONFIGURAÇÕES ESPECÍFICAS
        LabelLevel.Text = ""
        Labelunit.Text = ""
        RadioButtonAir.Visible = True
        RadioButtonVac.Visible = True
        TextBoxLevel.Visible = False
        'MUDANÇA DE NOME DO GBOX LIMITES
        GroupBoxLimits.Text = "Wavelength in:"
        LabelLimI.Visible = False
        LabelLimS.Visible = False
        LabelunitLI.Visible = False
        LabelunitLS.Visible = False
        TextBoxLimS.Visible = False
        TextBoxLimI.Visible = False

        'POSICIONANDO O GROUPBOXTE NO LUGAR GROUPBOXFILTERS
        GroupBoxTE.Location = New Point(768, 186)
        GroupBoxTE.Visible = True

    End Sub

    Private Sub ButtonHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHelp.Click
        'ABRINDO About
        AboutBoxASAS.Show()
    End Sub
    'BOTÃO EXECUTE'
    Private Sub ButtonExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExe.Click
        'DESATIVANDO BOTÃO EXECUTE
        ButtonExe.Enabled = False
        ButtonCancel.Enabled = False

        'ESTE BOTÃO É USADO PARA TODAS AS FUNÇÕES
        If TYPEFUNC = 1 Then
            FormResultPT.Show()

        ElseIf TYPEFUNC = 2 Then
            FormResultLT.Show()

        ElseIf TYPEFUNC = 3 Then
            'ABRINDO ESPECTRO SIMULADO PELO GNUPLOT
            Shell("cmd /k echo load 'DATAGRAPHEXE.gnu'|pgnuplot -persist", AppWinStyle.Hide)
            FormGraph.Show()

        ElseIf TYPEFUNC = 4 Then
            'NOMEANDO FORM APARTIR DOS DADOS DE ENTRADA
            FormResultTE.Text = "Electronic Temperature of the Atom " & ATOM

            For Index As Integer = 0 To LvTE.Items.Count - 1
                'LEITURA DAS LINHAS DO LISTVIEW LVRESULT
                If LvTE.Items.Item(Index).Checked = True Then
                    Dim LVItem As New ListViewItem
                    LVItem.Text = (LvTE.Items(Index).Text)
                    FormResultTE.LvResults.Items.Add(LVItem)

                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(1).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(2).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(3).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(4).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(5).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(6).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(7).Text)
                    LVItem.SubItems.Add(LvTE.Items(Index).SubItems(8).Text)
                    LVItem.SubItems.Add("")
                    LVItem.SubItems.Add("")

                    N = N + 1
                End If
            Next

            GroupBoxTE.Enabled = False
            FormResultTE.LabelNTrans.Text = N
            FormResultTE.Show()
        End If

    End Sub
    'BOTÃO SAÍDA
    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        'MSGBOX DECISÃO
        'Dim MSG As MsgBoxResult
        'MSG = MsgBox("You want to close the program ASAS?", MsgBoxStyle.OkCancel + MsgBoxStyle.Question)
        ' If MSG = MsgBoxResult.Ok Then
        'FECHAMENTO DO GNUPLOT
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)
        End
        'End If
    End Sub

    'BOTÃO PARA CANCELAR ROTINAS 
    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        'MOUSE
        Me.Cursor = Cursors.Default

        'RETIRANDO O NOME DA FUNÇÃO DO FORM
        GroupBoxMain.Text = ""
        'DESABILITANDO A VISIBILIDADE DOS GROUPBOX
        GroupBoxOptUser.Visible = False
        GroupBoxAtom.Visible = False
        GroupBoxFilters.Visible = False
        GroupBoxLimits.Visible = False
        GroupBoxGS.Visible = False
        GroupBoxOpenFile.Visible = False
        CheckBox2fot.Visible = False
        GroupBoxTE.Visible = False
        GroupBoxTE.Enabled = False
        TextBoxAtom.Enabled = True
        GroupBoxGS.Visible = False
        GroupBoxTypes.Visible = False

        'HABILITANDO OS GROUPBOX
        GroupBoxOptUser.Enabled = True
        GroupBoxFunc.Enabled = True
        GroupBoxMain.Enabled = False
        'HABILITANDO OS BOTÕES

        'DESABILITANDO OPÇOES ESPECIFICAS
        TextBoxLevel.Visible = True
        CheckBox2fot.Checked = False

        'LIMPANDO ENTRADA DE DADOS
        TextBoxOpenFile.Text = ""

        'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY

        For Index As Integer = 0 To LvTE.Items.Count - 1
            LvTE.Items.Item(Index).Checked = False
        Next

        'LIMPANDO TABELA DE DADOS
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)

        FormResultLT.LvResults.Clear()
        FormResultPT.LvResults.Clear()
        FormResultTE.LvResults.Clear()
        LvTE.Clear()
        N = 0
        'FECHANDO FORMS ABERTOS
        FormResultLT.Close()
        FormResultPT.Close()
        FormResultTE.Close()
        FormData.Close()

        'FECHANDO GNUPLOT
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)

        Return
    End Sub
    'BOTÃO PARA FIXAR PARAMETROS PARA AS 4 FUNÇOES
    Private Sub ButtonSetPar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSetPar.Click
        'CONTADORES
        Dim c1, c2, c3, c4, c5, linhasL, linhasE, GSColor As Integer

        'LIMPANDO TABELA DE RESULTADOS
        FormResultLT.LvResults.Columns.Clear()
        FormResultPT.LvResults.Columns.Clear()
        'INICIALIZANDO VALORES DE LINHAS
        linhasE = 0
        linhasL = 0

        'DEFININDO ESCOLHA DA FUNÇÃO
        Select Case TYPEFUNC

            Case 1
                '##################################################################################################################
                '####################################### TRANSITIONS FROM A LEVEL #####################################################

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.AppStarting

                'LIMPANDO TABELA DE RESULTADOS
                FormResultPT.LvResults.Columns.Clear()

                ' DESABILITANDO O BOTÃO
                ButtonSetPar.Enabled = False

                'CRIAÇÃO DAS COLUNAS DA TABELA DE SAIDA DE DADOS A PARTIR DOS FILTROS
                'NUMERO DAS LINHAS
                FormResultPT.LvResults.Columns.Add("Number", 70)
                'LAMBDA VAC
                FormResultPT.LvResults.Columns.Add("λo (nm)", 95)
                'LAMBDA AR
                FormResultPT.LvResults.Columns.Add("λair (nm)", 95)

                'INTENSIDADE RELATIVA
                If CheckBoxRI.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("RI (a.u.)", 100)
                End If
                'COEF.DE EMISSÃO DE EINSTEN
                If CheckBoxAif.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("A (1/s)", 100)
                    FormResultPT.LvResults.Columns.Add("Unc.A (+/- 1/s)", 120)
                End If
                'ENERGIA INFERIOR
                If CheckBoxEi.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("El (1/cm)", 100)
                End If
                'MOMENTO ANGULAR INFERIOR
                If CheckBoxJi.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("Jl", 80)
                End If
                'ENERGIA SUPERIOR
                If CheckBoxEf.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("Eu (1/cm)", 100)
                End If
                'MOMENTO ANGULAR SUPERIOR
                If CheckBoxJf.Checked = True Then
                    FormResultPT.LvResults.Columns.Add("Ju", 80)
                End If
                
                '____________________________________________________________
                'ORGANIZAR PARÂMETROS DE ENTRADA
                ATOM = Trim(TextBoxAtom.Text)
                LEVELstr = Trim(TextBoxLevel.Text)
                LIMINFstr = Trim(TextBoxLimI.Text)
                LIMSUPstr = Trim(TextBoxLimS.Text)

                ATOM = Replace(ATOM, " ", "")
                LEVELstr = Replace(LEVELstr, ",", ".")
                LIMINFstr = Replace(LIMINFstr, ",", ".")
                LIMSUPstr = Replace(LIMSUPstr, ",", ".")

                TextBoxAtom.Text = ATOM
                TextBoxLevel.Text = LEVELstr
                TextBoxLimI.Text = LIMINFstr
                TextBoxLimS.Text = LIMSUPstr

                'SEGUNDO FOTON
                LIMINF2str = Trim(TextBoxLimI2.Text)
                LIMSUP2str = Trim(TextBoxLimS2.Text)
                LIMINF2str = Replace(LIMINF2str, ",", ".")
                LIMSUP2str = Replace(LIMSUP2str, ",", ".")
                TextBoxLimI2.Text = LIMINF2str
                TextBoxLimS2.Text = LIMSUP2str

                'CRIANDO PARAMETROS PARA O FORM RESULTPT
                FormResultPT.LabelnumLI.Text = LIMINFstr & " nm"
                FormResultPT.LabelnumLS.Text = LIMSUPstr & " nm"

                'RECEPÇÃO DE VALORES NUMÉRICOS PELAS VARIÁVEIS
                LEVEL = Val(TextBoxLevel.Text)
                LIMINF = Val(TextBoxLimI.Text)
                LIMSUP = Val(TextBoxLimS.Text)
                LIMINF2 = Val(TextBoxLimI2.Text)
                LIMSUP2 = Val(TextBoxLimS2.Text)

                'VERIFICAÇÃO DOS PARÂMETROS INVÁLIDOS E VAZIOS
                If (TextBoxLevel.Text = "") Or (TextBoxLimI.Text = "") Or (TextBoxLimS.Text = "") Then
                    MessageBox.Show("Incomplete field parameters!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf (LIMINF >= LIMSUP) Or (LIMINF < 0) Or (LIMSUP < 0) Then
                    MessageBox.Show("Invalid spectral range!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                End If

                'VERIFICANDO A EXISTENCIA DOS ARQUIVOS RELACIONADOS AO ATOMO ESCOLHIDO NO BANCO DE DADO
                PATHLINES = PATHINDEX & "\DATABASE\LINES\Lines " & ATOM & ".txt"
                PATHLEVELS = PATHINDEX & "\DATABASE\LEVELS\Levels " & ATOM & ".txt"


                'SE NÃO EXISTIR ALGUM DOS ARQUIVOS DE ENTRADA
                If IO.File.Exists(PATHLINES) = False And IO.File.Exists(PATHLEVELS) = False Then
                    MessageBox.Show("Spectral lines and energy levels of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return

                ElseIf IO.File.Exists(PATHLEVELS) = False Then
                    MessageBox.Show("Energy levels of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return

                ElseIf IO.File.Exists(PATHLINES) = False Then
                    MessageBox.Show("Spectral lines of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return

                Else

                    'ABRINDO ARQUIVO DE SAIDA PARA O GRAFICO
                    DATAFILEOUT = New IO.StreamWriter(PATHINDEX & "\DATAGRAPH.txt")

                    ' SE EXISTIR O ARQUIVO INÍCIO DA COMPILAÇÃO DE DADOS
                    'DESABILITANDO OS GROUPS
                    GroupBoxFilters.Enabled = False
                    GroupBoxAtom.Enabled = False
                    GroupBoxLimits.Enabled = False
                    GroupBoxSecfoton.Enabled = False
                    CheckBox2fot.Enabled = False
                    'DEFININDO TIPO DE ESPECTRO ABSORÇÃO/EMISSÃO
                    If RadioButtonAbs.Checked = True Then
                        STRTYPESPEC = "Absorption"
                    ElseIf RadioButtonEm.Checked = True Then
                        STRTYPESPEC = "Emission"
                    End If

                    '###_________LEITURA DOS ARQUIVO DE ENTRADA_________###
                    'ARQUIVO 1) DE NIVEIS DE ENERGIA
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
                    linhasE = c1 - 1
                    c1 = 0

                    'ARQUIVO 2) LINHAS DE EMISSÃO
                    LINEFILEIN = New IO.StreamReader(PATHLINES)
                    c2 = 0

                    Do While LINEFILEIN.Peek <> -1
                        'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                        TEXTLINE = LINEFILEIN.ReadLine.Split(" ")

                        If TEXTLINE(0) <> "" Then
                            'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                            Try
                                'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                                N2(c2) = Val(TEXTLINE(0))
                                Lvac2(c2) = Val(TEXTLINE(1))
                                Lair2(c2) = Val(TEXTLINE(2))
                                IR2(c2) = Val(TEXTLINE(3))
                                Aif2(c2) = Val(TEXTLINE(4))
                                DesvA2(c2) = Val(TEXTLINE(5))
                                Ei2(c2) = Val(TEXTLINE(6))
                                Ji2(c2) = Val(TEXTLINE(7))
                                Ef2(c2) = Val(TEXTLINE(8))
                                Jf2(c2) = Val(TEXTLINE(9))
                                c2 = c2 + 1
                                'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                            Catch ex As IndexOutOfRangeException
                            End Try

                        End If

                    Loop

                    'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                    linhasL = c2 - 1
                    c2 = 0

                    'FECHANDO ARQUIVOS DE ENTRADA
                    LEVELFILEIN.Close()
                    LINEFILEIN.Close()


                    '###_________INÍCIO ROTINA POSSÍVEIS TRANSIÇÕES____________##
                    'NUMERO DE NIVEIS PARA SEGUNDO FOTON
                    Nsec = 0
                    c4 = 0
INDEX1:

                    'PROCURA DO NÍVEL DESEJADO E SEU MOMENTO ANGULAR
                    For c1 = 0 To linhasE
                        If (En1(c1) >= LEVEL - 0.01) And (En1(c1) <= LEVEL + 0.01) Then
                            J = Jn1(c1)
                            P = Parity1(c1)
                        End If

                    Next

                    For c1 = 0 To linhasE
                        'CALCULO DO DELTA J E DELTA E
                        DeltaE = Math.Abs(En1(c1) - LEVEL)
                        DeltaJ = Math.Abs(Jn1(c1) - J)

                        'CALCULO DO COMPRIMENTO DE ONDA PARTINDO DO DELTA E
                        If DeltaE <> 0 Then
                            Lvac(c1) = (10 ^ 7) / DeltaE
                            IR(c1) = 0
                            N1(c1) = 0
                            Aif(c1) = 0
                            DesvA(c1) = 0
                            'CONVERSÃO DE LAMBDA VACUO - AR 
                            Lair(c1) = Lvac(c1) / Ciddor(Lvac(c1))

                            LAMBDAPT(c1) = Lvac(c1)

                            'LINK ENTRE OS DADOS DOS ARQUIVOS DE NIVEIS E LINHAS DE EMISSÃO ATRAVÉS DE SEU COMPRIMENTO DE ONDA NO VÁCUO
                            'ADMITINDO ERRO DO LAMBDA DE 0,005 nm
                            For c3 = 0 To linhasL
                                DeltaL = Math.Abs(LAMBDAPT(c1) - Lvac2(c3))

                                If DeltaL <= 0.0001 Then
                                    'ATRIBUINDO VALORES A PARTIR DO ARQUIVO DE LINHAS
                                    N1(c1) = N2(c3)
                                    IR(c1) = IR2(c3)
                                    Aif(c1) = Aif2(c3)
                                    DesvA(c1) = DesvA2(c3)

                                End If
                                'SEPARAMOOS APARTIR DESTE PONTO A IMPRESSÃO DE ACORDO COM O TIPO DE ESPECTRO
                            Next


                            '___ESPECTRO DE ABSORÇÃO______________________
                            If STRTYPESPEC = "Absorption" Then

                                'APLICANDO AS REGRAS DE SELEÇÃO MOMENTO ANGULAR 
                                If (DeltaJ <= 1) And (DeltaE <> 0) And (LAMBDAPT(c1) >= LIMINF) And (LAMBDAPT(c1) <= LIMSUP) And (LEVEL < En1(c1)) Then
                                    'APLICANDO AS REGRAS DE SELEÇÃO DE PARUDADE
                                    If (Parity1(c1) = 0) Or (P = 0) Or (P <> Parity1(c1)) Then

                                        'CONSTRUÇÃO DA TABELA DE SAIDAS 
                                        'CRIA UMA VARIÁVEL PARA O LISTVIEWITEMS
                                        Dim LVItem1 As New ListViewItem
                                        N = N + 1 'NÚMERO DE TRANSIÇÕES
                                        'COMPRIMENTO DE ONDA DEVE SER UM DADO OBRIGATÓRIO
                                        LVItem1.Text = (N1(c1).ToString("0"))

                                        FormResultPT.LvResults.Items.Add(LVItem1)
                                        LVItem1.SubItems.Add(Lvac(c1).ToString("0.000000"))
                                        LVItem1.SubItems.Add(Lair(c1).ToString("0.000000"))
                                        LVItem1.SubItems.Add((IR(c1).ToString("0.00")))
                                        'OS DEMAIS DADOS SÃO OPCIONAIS

                                        If CheckBoxAif.Checked = True Then
                                            LVItem1.SubItems.Add(Aif(c1).ToString)
                                            LVItem1.SubItems.Add((DesvA(c1).ToString("0.00")))
                                        End If
                                        If CheckBoxEi.Checked = True Then LVItem1.SubItems.Add((LEVEL.ToString("0.0000")))
                                        If CheckBoxJi.Checked = True Then LVItem1.SubItems.Add((J.ToString("0.0")))
                                        If CheckBoxEf.Checked = True Then LVItem1.SubItems.Add((En1(c1).ToString("0.0000")))
                                        If CheckBoxJf.Checked = True Then LVItem1.SubItems.Add((Jn1(c1).ToString("0.0")))

                                        'DADOS PARA O ARQUIVO DE GRAFICOS: LVAC, LAIR, EI, EF E IR.
                                        DATAFILEOUT.WriteLine(Replace(Lvac(c1).ToString("0.000000"), ",", ".") & " " & Replace(Lair(c1).ToString("0.000000"), ",", ".") & " " & Replace(LEVEL.ToString("0.0000"), ",", ".") & " " & Replace(En1(c1).ToString("0.0000"), ",", ".") & " " & Replace(IR(c1).ToString("0.00"), ",", "."))

                                        'VALORES DE NIVEIS EXCITADOS PARA CALCULO DE SEGUNDO FOTON/ APENAS PARA ESPEC. DE ABSORÇÃO
                                        If (Nsec = 0) Then
                                            SecLevel(c4) = En1(c1)
                                            c4 = c4 + 1
                                        End If
                                    End If
                                End If

                                '___ESPECTRO DE EMISSÃO______________________
                            ElseIf STRTYPESPEC = "Emission" Then
                                'APLICANDO AS REGRAS DE SELEÇÃO MOMENTO ANGULAR 
                                If (DeltaJ <= 1) And (DeltaE <> 0) And (LAMBDAPT(c1) >= LIMINF) And (LAMBDAPT(c1) <= LIMSUP) And (LEVEL > En1(c1)) Then

                                    'APLICANDO AS REGRAS DE SELEÇÃO DE PARUDADE
                                    If (Parity1(c1) = 0) Or (P = 0) Or (P <> Parity1(c1)) Then
                                        'CONSTRUÇÃO DA TABELA DE SAIDAS 
                                        'CRIA UMA VARIÁVEL PARA O LISTVIEWITEMS
                                        Dim LVItem1 As New ListViewItem
                                        N = N + 1 'NÚMERO DE TRANSIÇÕES
                                        LVItem1.Text = (N1(c1).ToString("0"))

                                        FormResultPT.LvResults.Items.Add(LVItem1)
                                        LVItem1.SubItems.Add(Lvac(c1).ToString("0.000000"))
                                        LVItem1.SubItems.Add(Lair(c1).ToString("0.000000"))
                                        LVItem1.SubItems.Add((IR(c1).ToString("0.00")))

                                        'OS DEMAIS DADOS SÃO OPCIONAIS
                                        If CheckBoxAif.Checked = True Then
                                            LVItem1.SubItems.Add(Aif(c1).ToString)
                                            LVItem1.SubItems.Add((DesvA(c1).ToString("0.00")))
                                        End If
                                        
                                        If CheckBoxJi.Checked = True Then LVItem1.SubItems.Add((Jn1(c1).ToString("0.0")))
                                        If CheckBoxEi.Checked = True Then LVItem1.SubItems.Add((En1(c1).ToString("0.0000")))
                                        If CheckBoxEf.Checked = True Then LVItem1.SubItems.Add((LEVEL.ToString("0.0000")))
                                        If CheckBoxJf.Checked = True Then LVItem1.SubItems.Add((J.ToString("0.0")))

                                        'DADOS PARA O ARQUIVO DE GRAFICOS: LVAC, LAIR, EI, EF E IR.
                                        DATAFILEOUT.WriteLine(Replace(Lvac(c1).ToString("0.000000"), ",", ".") & " " & Replace(Lair(c1).ToString("0.000000"), ",", ".") & " " & Replace(LEVEL.ToString("0.0000"), ",", ".") & " " & Replace(En1(c1).ToString("0.0000"), ",", ".") & " " & Replace(IR(c1).ToString("0.00"), ",", "."))


                                        'VALORES DE NIVEIS EXCITADOS PARA CALCULO DE SEGUNDO FOTON/ APENAS PARA ESPEC. DE ABSORÇÃO
                                        If (Nsec = 0) Then
                                            SecLevel(c4) = En1(c1)
                                            c4 = c4 + 1
                                        End If
                                    End If
                                End If

                            End If 'FINAL DA ESCOLHA DE ESPECTRO
                        End If
                    Next
                    'FINAL DA ROTINA POSSIVEIS TRANSÇÕES

                    'PARA SEGUNDO FOTON RETORNAMOS O PROGRAMA AO INICIO DA ROTINA PT
                    'DEFINEINDO NUMERO DE NÍVEIS DE SEGUNDO FOTON DE ENERGIA
                    If (CheckBox2fot.Checked = True) And (Nsec = 0) Then
                        Nsec = N + 1
                        c5 = 0
                    End If
                    If (CheckBox2fot.Checked = True) And (Nsec <> 0) Then
                        LEVEL = SecLevel(c5)
                        c5 = c5 + 1
                        'MUDANÇA DE RANGE DE BUSCA DE SEGUNDO FOTON
                        If (c5 < Nsec) Then
                            LIMINF = LIMINF2
                            LIMSUP = LIMSUP2
                            DATAFILEOUT.WriteLine("")
                            GoTo INDEX1
                        End If
                    End If
                End If

                'CRIANDO NOME NO FORMRESULT A PARTIR DOS DADOS DE ENTRADA
                'PARA SEGUNDO FOTON
                If (CheckBox2fot.Checked = True) Then
                    FormResultPT.Text = "Transitions of " & ATOM & " - First and Second Photon"
                Else
                    FormResultPT.Text = "Transitions of " & ATOM & " From Level " & Replace(LEVEL.ToString("0.0000"), ",", ".") & " (" & Replace(LIMINF.ToString(), ",", ".") & " nm - " & Replace(LIMSUP.ToString(), ",", ".") & " nm) " & STRTYPESPEC
                End If

                FormResultPT.LabelNTrans.Text = "Number of Transitions: " & N.ToString()
                'HABILITANDO BOTÃO DE EXECUÇÃO
                ButtonExe.Enabled = True

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.Default

                'FECHANDO ARQUIVO DE DADOS 
                DATAFILEOUT.Close()

                '##############################FIM ROTINA 1######################################################################################

                '####################################################################################################################  


            Case 2
                '####################################### POSSIVEIS TRANSIÇÕES #####################################################

                'LIMPANDO TABELA DE RESULTADOS
                FormResultLT.LvResults.Columns.Clear()
                N = 0
                c1 = 0

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.AppStarting

                ' DESABILITANDO O BOTÃO
                ButtonSetPar.Enabled = False

                'CRIAÇÃO DAS COLUNAS DA TABELA DE SAIDA DE DADOS A PARTIR DOS FILTROS
                'LAMBDA VAC
                FormResultLT.LvResults.Columns.Add("Number", 70)

                'LAMBDA VAC
                FormResultLT.LvResults.Columns.Add("λo (nm)", 90)

                'LAMBDA AR
                FormResultLT.LvResults.Columns.Add("λair (nm)", 90)

                'INTENSIDADE RELATIVA
                If CheckBoxRI.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("RI (a.u.)", 100)
                End If
                'COEF.DE EMISSÃO DE EINSTEN
                If CheckBoxAif.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("A (1/s)", 100)
                    FormResultLT.LvResults.Columns.Add("Unc.A (+/- 1/s)", 120)
                End If
                'ENERGIA INFERIOR
                If CheckBoxEi.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("El (1/cm)", 100)
                End If
                'MOMENTO ANGULAR INFERIOR
                If CheckBoxJi.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("Jl", 80)
                End If
                'ENERGIA SUPERIOR
                If CheckBoxEf.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("Eu (1/cm)", 100)
                End If
                'MOMENTO ANGULAR SUPERIOR
                If CheckBoxJf.Checked = True Then
                    FormResultLT.LvResults.Columns.Add("Ju", 80)
                End If
               

                'ORGANIZAR PARÂMETROS DE ENTRADA
                ATOM = Trim(TextBoxAtom.Text)
                LIMINFstr = Trim(TextBoxLimI.Text)
                LIMSUPstr = Trim(TextBoxLimS.Text)

                ATOM = Replace(ATOM, " ", "")
                LIMINFstr = Replace(LIMINFstr, ",", ".")
                LIMSUPstr = Replace(LIMSUPstr, ",", ".")

                TextBoxAtom.Text = ATOM
                TextBoxLimI.Text = LIMINFstr
                TextBoxLimS.Text = LIMSUPstr

                'RECEPÇÃO DE VALORES NUMÉRICOS PELAS VARIÁVEIS
                LIMINF = Val(TextBoxLimI.Text)
                LIMSUP = Val(TextBoxLimS.Text)

                'CORRIGINDO ERRO PARA GRANDES INTERVALOS DE PROCURA
                If (LIMSUP - LIMINF > 10) Then
                    MessageBox.Show("Spectral range demand exceeds standard 10 nm!", "", MessageBoxButtons.OK)
                    LIMSUP = LIMINF + 10
                    TextBoxLimS.Text = LIMSUP
                End If

                'VERIFICAÇÃO DOS PARÂMETROS INVÁLIDOS E VAZIOS
                If (TextBoxLimI.Text = "") Or (TextBoxLimS.Text = "") Then
                    MessageBox.Show("Incomplete field parameters!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf (LIMINF >= LIMSUP) Or (LIMINF < 0) Or (LIMSUP < 0) Then
                    MessageBox.Show("Invalid spectral range!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                End If

                'VERIFICANDO A EXISTENCIA DOS ARQUIVOS RELACIONADOS AO ATOMO ESCOLHIDO NO BANCO DE DADO
                PATHLINES = PATHINDEX & "\DATABASE\LINES\Lines " & ATOM & ".txt"
                PATHLEVELS = PATHINDEX & "\DATABASE\LEVELS\Levels " & ATOM & ".txt"

                'SE NÃO EXISTIR ALGUM DOS ARQUIVOS DE ENTRADA
                If IO.File.Exists(PATHLINES) = False And IO.File.Exists(PATHLEVELS) = False Then
                    MessageBox.Show("Spectral lines and energy levels of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf IO.File.Exists(PATHLEVELS) = False Then
                    MessageBox.Show("Energy levels of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf IO.File.Exists(PATHLINES) = False Then
                    MessageBox.Show("Spectral lines of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                Else

                    ' SE EXISTIR O ARQUIVO INÍCIO DA COMPILAÇÃO DE DADOS

                    'DESABILITANDO OS GROUPS
                    GroupBoxFilters.Enabled = False
                    GroupBoxAtom.Enabled = False
                    GroupBoxLimits.Enabled = False

                    '###_________LEITURA DOS ARQUIVO DE ENTRADA_________###
                    'ARQUIVO 1) DE NIVEIS DE ENERGIA
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

                                'VARIAVEIS PARA SEGUNDO FOTON
                                Jn2(c1) = Jn1(c1)
                                En2(c1) = En1(c1)
                                Parity2(c1) = Parity1(c1)

                                c1 = c1 + 1
                                'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                            Catch ex As IndexOutOfRangeException
                            End Try
                        End If
                    Loop

                    'NUMEROS DE LINHAS DO ARQUIVO DE NÍVEIS DE ENERGIA
                    linhasE = c1 - 1
                    c1 = 0

                    'ARQUIVO 2) LINHAS DE ESPECTRO
                    LINEFILEIN = New IO.StreamReader(PATHLINES)
                    c2 = 0

                    'LE O ARQUIVO ATÉ ACABAR SUAS LINHAS
                    Do While LINEFILEIN.Peek <> -1
                        'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                        TEXTLINE = LINEFILEIN.ReadLine.Split(" ")

                        If TEXTLINE(0) <> "" Then
                            'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                            Try
                                'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                                N2(c2) = Val(TEXTLINE(0))
                                Lvac2(c2) = Val(TEXTLINE(1))
                                Lair2(c2) = Val(TEXTLINE(2))
                                IR2(c2) = Val(TEXTLINE(3))
                                Aif2(c2) = Val(TEXTLINE(4))
                                DesvA2(c2) = Val(TEXTLINE(5))
                                Ei2(c2) = Val(TEXTLINE(6))
                                Ji2(c2) = Val(TEXTLINE(7))
                                Ef2(c2) = Val(TEXTLINE(8))
                                Jf2(c2) = Val(TEXTLINE(9))
                                c2 = c2 + 1
                                'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                            Catch ex As IndexOutOfRangeException
                            End Try
                        End If

                    Loop

                    'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                    linhasL = c2 - 1
                    c2 = 0
                    c4 = 0
                    N = 0
                    'FECHANDO ARQUIVOS DE ENTRADA
                    LEVELFILEIN.Close()
                    LINEFILEIN.Close()

                    'MessageBox.Show("Spectral lines of the " & LEVEL & " cm", "", MessageBoxButtons.OK)
                    '###_________INÍCIO ROTINA LOCALIZADOR DE TRANSIÇÕES____________#
                    'LEITURA DE PARA TODOS OS NÍVEIS DE ENERGIA POSSÍVEIS
                    For c4 = 0 To linhasE

                        'TESTE
                        For c1 = 0 To linhasE
                            'CALCULO DO DELTA J E DELTA E
                            DeltaE = Math.Abs(En1(c1) - En2(c4))
                            DeltaJ = Math.Abs(Jn1(c1) - Jn2(c4))

                            'CALCULO DO COMPRIMENTO DE ONDA PARTINDO DO DELTA E
                            If DeltaE <> 0 Then
                                Lvac(c1) = (10 ^ 7) / DeltaE
                                IR(c1) = 0
                                N1(c1) = 0

                                'CONVERSÃO DE LAMBDA VACUO - AR 
                                Lair(c1) = Lvac(c1) / Ciddor(Lvac(c1))

                                'PARA LAMBDA NO VÁCUO
                                LAMBDALT(c1) = Lvac(c1)


                                'VERIFICAÇÃO SE A LINHA ESTRA DENTRO DO LIMITE ESPECTRAL DEFINIDO (FEITA ANTES DA PROCURA PARA AGILIZAR OS CALCULOS)
                                If (LAMBDALT(c1) > LIMINF) And (LAMBDALT(c1) < LIMSUP) And (En1(c1) > En2(c4)) Then

                                    'LINK ENTRE OS DADOS DOS ARQUIVOS DE NIVEIS E LINHAS DE EMISSÃO ATRAVÉS DE SEU COMPRIMENTO DE ONDA NO VÁCUO
                                    'ADMITINDO ERRO DO LAMBDA DE 0,005 nm
                                    For c3 = 0 To linhasL
                                        DeltaL = Math.Abs(Lvac(c1) - Lvac2(c3))

                                        If (DeltaL <= 0.0001) Then
                                            'ATRIBUINDO VALORES A PARTIR DO ARQUIVO DE LINHAS
                                            N1(c1) = N2(c3)
                                            IR(c1) = IR2(c3)
                                            Aif(c1) = Aif2(c3)
                                            DesvA(c1) = DesvA2(c3)
                                        End If
                                        'SEPARAMOOS APARTIR DESTE PONTO A IMPRESSÃO DE ACORDO COM O TIPO DE ESPECTRO
                                    Next
                                    'APLICANDO AS REGRAS DE SELEÇÃO MOMENTO ANGULAR 
                                    If (DeltaJ <= 1) And (DeltaE <> 0) Then

                                        'APLICANDO AS REGRAS DE SELEÇÃO DE PARUDADE
                                        If (Parity1(c1) <> Parity2(c4)) Or (Parity2(c4) = 0) Then

                                            'LER AS TRANSIÇÕES POSSÍVEIS FORA DE ORDEM
                                            N3(N) = N1(c1)
                                            Lvac3(N) = Lvac(c1)
                                            Lair3(N) = Lair(c1)
                                            IR3(N) = IR(c1)
                                            Aif3(N) = Aif(c1)
                                            DesvA3(N) = DesvA(c1)
                                            Ei3(N) = En2(c4)
                                            Ji3(N) = Jn2(c4)
                                            Ef3(N) = En1(c1)
                                            Jf3(N) = Jn1(c1)
                                            N = N + 1 'NÚMERO DE TRANSIÇÕES

                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                    N = N - 1
                    'ORGANIZAÇÃO EM ORDEM CRESCENTE DE COMP. DE ONDA.
                    For j As Integer = 0 To N
                        For i As Integer = 0 To N - 1
                            Do While (Lvac3(i) < Lvac3(j)) And (i <> j)
                                N4 = N3(i)
                                N3(i) = N3(j)
                                N3(j) = N4

                                Lvac4 = Lvac3(i)
                                Lvac3(i) = Lvac3(j)
                                Lvac3(j) = Lvac4

                                Lair4 = Lair3(i)
                                Lair3(i) = Lair3(j)
                                Lair3(j) = Lair4

                                Aif4 = Aif3(i)
                                Aif3(i) = Aif3(j)
                                Aif3(j) = Aif4

                                DesvA4 = DesvA3(i)
                                DesvA3(i) = DesvA3(j)
                                DesvA3(j) = DesvA4


                                Ei4 = Ei(i)
                                Ei(i) = Ei(j)
                                Ei(j) = Ei4

                                Ji4 = Ji(i)
                                Ji(i) = Ji(j)
                                Ji(j) = Ji4

                                Ef4 = Ef(i)
                                Ef(i) = Ef(j)
                                Ef(j) = Ef4

                                Ji4 = Jf(i)
                                Jf(i) = Jf(j)
                                Jf(j) = Jf4

                            Loop
                        Next
                    Next

                End If
                'PREENCHENDO AS TABELAS DE DADOS
                For c1 = 0 To (N)
                    'CONSTRUÇÃO DA TABELA DE SAIDAS 
                    'CRIA UMA VARIÁVEL PARA O LISTVIEWITEMS
                    Dim LVItem2 As New ListViewItem
                    'COMPRIMENTO DE ONDA DEVE SER UM DADO OBRIGATÓRIO
                    LVItem2.Text = (N3(c1).ToString("0"))

                    FormResultLT.LvResults.Items.Add(LVItem2)
                    LVItem2.SubItems.Add(Lvac3(c1).ToString("0.000000"))
                    LVItem2.SubItems.Add(Lair3(c1).ToString("0.000000"))
                    LVItem2.SubItems.Add((IR3(c1).ToString("0.00")))
                    'GUARDANDO DADOS PARA TABELA EM ORDEM DE COMPRIMENTO DE ONDA DECRESCENTE
                    'OS DEMAIS DADOS SÃO OPCIONAIS
                    If CheckBoxAif.Checked = True Then
                        LVItem2.SubItems.Add(Aif3(c1).ToString)
                        LVItem2.SubItems.Add((DesvA3(c1).ToString("0.00")))
                    End If
                    If CheckBoxEi.Checked = True Then LVItem2.SubItems.Add((Ei3(c1).ToString("0.0000")))
                    If CheckBoxJi.Checked = True Then LVItem2.SubItems.Add((Ji3(c1).ToString("0.0")))
                    If CheckBoxEf.Checked = True Then LVItem2.SubItems.Add((Ef3(c1).ToString("0.0000")))
                    If CheckBoxJf.Checked = True Then LVItem2.SubItems.Add((Jf3(c1).ToString("0.0")))
                Next

                'CRIANDO NOME NO FORMRESULT A PARTIR DOS DADOS DE ENTRADA
                FormResultLT.Text = "Possible Transitions of " & ATOM & " ( " & Replace(LIMINF.ToString(), ",", ".") & " nm - " & Replace(LIMSUP.ToString(), ",", ".") & " nm ) " & STRTYPESPEC
                FormResultLT.LabelNTrans.Text = "Number of Transitions: " & (N + 1).ToString

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.Default

                'HABILITANDO BOTÃO DE EXECUÇÃO
                ButtonExe.Enabled = True

                'ZERANDO VALORES
                For c1 = 0 To N
                    Lvac3(c1) = 0
                    N3(c1) = 0
                    Lvac3(c1) = 0
                    Lair3(c1) = 0
                    IR3(c1) = 0
                    Aif3(c1) = 0
                    DesvA3(c1) = 0
                    Ei3(c1) = 0
                    Ji3(c1) = 0
                    Ef3(c1) = 0
                    Jf3(c1) = 0
                Next

                '##################################################FIM ROTINA##################################################################

                '####################################################################################################################

            Case 3

                '#################################################################################################################
                '####################################### SPECTRA SIMULATION #####################################################
                ' DESABILITANDO O BOTÃO
                ButtonSetPar.Enabled = False

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.AppStarting

                'VARIÁVEIS DESTA FUNÇÃO
                Dim NPOINTS As Integer
                N = 0

                'ORGANIZAR PARÂMETROS DE ENTRADA
                ATOM = Trim(TextBoxAtom.Text)
                LIMINFstr = Trim(TextBoxLimI.Text)
                LIMSUPstr = Trim(TextBoxLimS.Text)
                FWHMstr = Trim(TextBoxLevel.Text)

                ATOM = Replace(ATOM, " ", "")
                LIMINFstr = Replace(LIMINFstr, ",", ".")
                LIMSUPstr = Replace(LIMSUPstr, ",", ".")
                FWHMstr = Replace(FWHMstr, ",", ".")

                TextBoxAtom.Text = ATOM
                TextBoxLimI.Text = LIMINFstr
                TextBoxLimS.Text = LIMSUPstr
                TextBoxLevel.Text = FWHMstr

                'RECEPÇÃO DE VALORES NUMÉRICOS PELAS VARIÁVEIS
                LIMINF = Val(TextBoxLimI.Text)
                LIMSUP = Val(TextBoxLimS.Text)
                FWHM = Val(TextBoxLevel.Text)

                'VERIFICAÇÃO DOS PARÂMETROS INVÁLIDOS E VAZIOS
                If (TextBoxLimI.Text = "") Or (TextBoxLimS.Text = "") Or (TextBoxLevel.Text = "") Then
                    MessageBox.Show("Incomplete field parameters!", "", MessageBoxButtons.OK)
                    Me.Cursor = Cursors.Default
                    ButtonSetPar.Enabled = True
                    Return
                ElseIf (LIMINF >= LIMSUP) Or (LIMINF < 0) Or (LIMSUP < 0) Then
                    MessageBox.Show("Invalid spectral range!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                ElseIf (FWHM = 0) Then
                    'DESABILITANDO FHWM=0
                    MessageBox.Show("Invalid value of FHWM!", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                End If

                'VERIFICANDO A EXISTENCIA DO ARQUIVO DE LINHA RELACIONADOS AO ATOMO ESCOLHIDO NO BANCO DE DADO
                'OBS: ESTA FUNÇÃO USA APENAS O ARQUIVO DE LINHAS PARA SIMULAR O ESPECTRO

                'VERIFICANDO A EXISTENCIA DOS ARQUIVOS RELACIONADOS AO ATOMO ESCOLHIDO NO BANCO DE DADO
                If (ButtonOpenFile.Enabled = False) And (TextBoxOpenFile.Text <> "") Then
                    PATHLINES = TextBoxOpenFile.Text
                Else
                    PATHLINES = PATHINDEX & "\DATABASE\LINES\Lines " & ATOM & ".txt"
                End If

                'CALCULO NO NÚMERO DE PONTOS DO ESPECTRO
                NPOINTS = ((LIMSUP - LIMINF) / (FWHM)) * 10

                c2 = 0

                'SE NÃO EXISTIR ALGUM DOS ARQUIVOS DE ENTRADA
                If IO.File.Exists(PATHLINES) = False Then
                    MessageBox.Show("Spectral lines of the " & ATOM & "  atom does not exist! Check database.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                    
                    'OBS: ESTE NUMERO NÃO PODE EXCEDER 100000 PONTOS
                ElseIf (NPOINTS >= 200000) Then
                    MessageBox.Show("The number of points on the graph are exceeded! Increase the FWHM or decrease the Spectral Range.", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return

                Else
                'SE EXISTIR ARQUIVO DE ENTRADA
                'ARQUIVO 2) LINHAS DE EMISSÃO E ABS.
                LINEFILEIN = New IO.StreamReader(PATHLINES)
                    'DESABILITANDO OS GROUPS
                    GroupBoxOptUser.Enabled = False
                GroupBoxFilters.Enabled = False
                GroupBoxAtom.Enabled = False
                GroupBoxLimits.Enabled = False
                GroupBoxSecfoton.Enabled = False
                CheckBox2fot.Enabled = False
                GroupBoxGS.Enabled = False
                GroupBoxOpenFile.Enabled = False

                'LE O ARQUIVO ATÉ ACABAR SUAS LINHAS
                Do While LINEFILEIN.Peek <> -1
                    'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                    TEXTLINE = LINEFILEIN.ReadLine.Split(" ")

                    If TEXTLINE(0) <> "" Then
                        'ENTRADA DE DADOS A PARTIR DO ARQUIVO LINHAS
                        Try
                            'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                            If (ButtonOpenFile.Enabled = False) And (TextBoxOpenFile.Text <> "") And (RadioButtonAir.Checked = True) Then
                                    Lair2(c2) = Val(Replace(TEXTLINE(0), ",", "."))
                                    IR2(c2) = Val(Replace(TEXTLINE(1), ",", "."))
                            ElseIf (ButtonOpenFile.Enabled = False) And (TextBoxOpenFile.Text <> "") And (RadioButtonVac.Checked = True) Then
                                    Lvac2(c2) = Val(Replace(TEXTLINE(0), ",", "."))
                                    IR2(c2) = Val(Replace(TEXTLINE(1), ",", "."))
                            Else
                                    N2(c2) = Val(Replace(TEXTLINE(0), ",", "."))
                                    Lvac2(c2) = Val(Replace(TEXTLINE(1), ",", "."))
                                    Lair2(c2) = Val(Replace(TEXTLINE(2), ",", "."))
                                    IR2(c2) = Val(Replace(TEXTLINE(3), ",", "."))
                                    Aif2(c2) = Val(Replace(TEXTLINE(4), ",", "."))
                                    DesvA2(c2) = Val(Replace(TEXTLINE(5), ",", "."))
                                    Ei2(c2) = Val(Replace(TEXTLINE(6), ",", "."))
                                    Ji2(c2) = Val(Replace(TEXTLINE(7), ",", "."))
                                    Ef2(c2) = Val(Replace(TEXTLINE(8), ",", "."))
                                    Jf2(c2) = Val(Replace(TEXTLINE(9), ",", "."))
                            End If

                            'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                        Catch ex As IndexOutOfRangeException
                        End Try
                        c2 = c2 + 1
                    End If
                Loop

                'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                linhasL = c2 - 1
                c2 = 0

                'FECHANDO ARQUIVOS DE ENTRADA
                LINEFILEIN.Close()

                'COR DA LINHA DO GRÁFICO
                If ComboBoxColor.Text = "Red" Then
                    GSColor = 1
                ElseIf ComboBoxColor.Text = "Green" Then
                    GSColor = 2
                ElseIf ComboBoxColor.Text = "Blue" Then
                    GSColor = 3
                ElseIf ComboBoxColor.Text = "Gray" Then
                    GSColor = 0
                ElseIf ComboBoxColor.Text = "Yellow" Then
                    GSColor = 15
                ElseIf ComboBoxColor.Text = "Black" Then
                    GSColor = 8
                End If


                'CRIAÇÃO DO ARQUIVO DE SAIDA PARA A SIMULAÇÃO DE DADOS
                    DATAFILEOUT = New IO.StreamWriter(PATHINDEX & "\DATAGRAPH1.txt")

                'INICIO DE CALCULOS PARA PLOTAGEM DE ESPECTRO

                LAMBDASS(1) = LIMINF

                For c1 = 2 To (NPOINTS + 2)
                        LAMBDASS(c1) = LAMBDASS(c1 - 1) + FWHM / 10

                Next

                For c3 = 1 To (NPOINTS + 1)

                    IR(c3) = 0

                    If RadioButtonAir.Checked = True Then
                        TYPEMED = "Air"
                        'ESPECTRO PARA LAMBDA NO AR
                        For c1 = 0 To linhasL
                            DeltaL = LAMBDASS(c3) - Lair2(c1)
                            IR(c3) = IR(c3) + ((IR2(c1) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2)))
                        Next
                    Else
                        TYPEMED = "Vacuum"
                        'ESPECTRO PARA LAMBDA NO VÁCUO
                        For c1 = 0 To linhasL
                            DeltaL = LAMBDASS(c3) - Lvac2(c1)
                            IR(c3) = IR(c3) + (IR2(c1) * (FWHM / Math.PI)) / (Math.Pow(DeltaL, 2) + Math.Pow(FWHM, 2))
                        Next
                    End If
                        DATAFILEOUT.WriteLine(Replace(LAMBDASS(c3).ToString("0.0000000000"), ",", ".") & " " & Replace(IR(c3).ToString("0.0000000000"), ",", "."))

                Next

                'CRIAÇÃO DO ARQUIVO DE COMANDO DO GNUPLOT
                DATAFILEOUTEXE = New IO.StreamWriter(PATHINDEX & "\DATAGRAPHEXE.gnu")
                DATAFILEOUTEXE.WriteLine("reset")
                DATAFILEOUTEXE.WriteLine("set terminal jpeg size 800,500")
                DATAFILEOUTEXE.WriteLine("set output 'GraphicSS.jpeg'")
                    DATAFILEOUTEXE.WriteLine("set xrange[" & (Replace(LIMINF.ToString(), ",", ".")) & ":" & (Replace(LIMSUP.ToString(), ",", ".")) & "]")
                DATAFILEOUTEXE.WriteLine("set grid")
                If (ATOM = "") And (TextBoxTitleGraph.Text = "" Or TextBoxTitleGraph.Text = "Default") Then
                    DATAFILEOUTEXE.WriteLine("set title 'Simulated Spectra of " & PATHLINES & "'")
                ElseIf (ATOM <> "") And (TextBoxTitleGraph.Text = "" Or TextBoxTitleGraph.Text = "Default") Then
                    DATAFILEOUTEXE.WriteLine("set title 'Simulated Spectrum of " & ATOM & "'")
                Else
                    DATAFILEOUTEXE.WriteLine("set title 'Simulated Spectrum of " & TextBoxTitleGraph.Text & "'")
                End If

                DATAFILEOUTEXE.WriteLine("set xlabel 'Wavelength in " & TYPEMED & " (nm)'")
                DATAFILEOUTEXE.WriteLine("set ylabel 'Relative Intensity (a.u.)'")
                If (ButtonOpenFile.Enabled = False) And (TextBoxOpenFile.Text <> "") Then
                        DATAFILEOUTEXE.WriteLine("plot 'DATAGRAPH1.txt' using ($1):($2) t 'Simulated Spectrum' with line lc " & GSColor & " lw 2")
                Else
                        DATAFILEOUTEXE.WriteLine("plot 'DATAGRAPH1.txt' using ($1):($2) t '" & ATOM & "'with line lc " & GSColor & " lw 2")

                End If
                DATAFILEOUTEXE.WriteLine("set terminal windows font 'Arial,12'")
                DATAFILEOUTEXE.WriteLine("replot")

                'FECHANDO ARQUIVOS
                DATAFILEOUT.Close()
                DATAFILEOUTEXE.Close()

                End If
                'CRIANDO NOME NO FORMGRAPH A PARTIR DOS DADOS DE ENTRADA
                If (ButtonOpenFile.Enabled = False) And (TextBoxOpenFile.Text <> "") Then
                    FormGraph.Text = "OPTIONS - Simulated Spectrum of FILE in " & TYPEMED & " (" & Replace(LIMINF.ToString(), ",", ".") & "nm - " & Replace(LIMSUP.ToString(), ",", ".") & "nm)"

                Else
                    FormGraph.Text = "OPTIONS - Simulated Spectrum of " & ATOM & " in " & TYPEMED & " (" & Replace(LIMINF.ToString(), ",", ".") & "nm - " & Replace(LIMSUP.ToString(), ",", ".") & "nm)"

                End If

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.Default

                'HABILITANDO BOTÃO DE EXECUÇÃO
                ButtonExe.Enabled = True

                '##################################################FIM ROTINA##################################################################

                '####################################################################################################################

            Case Else
                '####################################### TEMP. ELETRONICA #####################################################
                'LIMPANDO TABELA DE RESULTADOS
                LvTE.Clear()

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.AppStarting

                ' DESABILITANDO O BOTÃO
                ButtonSetPar.Enabled = False

                'CRIAÇÃO DAS COLUNAS DAS TABELAS DE DADOS PARA ESCOLHA DAS LINHAS DO GRÁFICO DE BOLTZMANN
                LvTE.Columns.Add(" Number ", 80)
                FormResultTE.LvResults.Columns.Add(" Number ", 80)
                'MUDANÇA DE MEIO PARA O COMPRIMENTO DE ONDA
                If RadioButtonVac.Checked = True Then
                    LvTE.Columns.Add("λo (nm)", 90)
                    FormResultTE.LvResults.Columns.Add("λo (nm)", 90)
                    FormResultTE.Label2.Text = "λo (nm)"
                Else
                    LvTE.Columns.Add("λair (nm)", 90)
                    FormResultTE.LvResults.Columns.Add("λair (nm)", 90)
                    FormResultTE.Label2.Text = "λair (nm)"
                End If

                'ALTERANDO ENTRE AS CRIAÇÕES DAS COLUNAS DAS TABELAS DO FORMMAIN E DO FORMRESULTTE
                LvTE.Columns.Add("RI (a.u.)", 100)
                FormResultTE.LvResults.Columns.Add("RI (a.u.)", 100)
                LvTE.Columns.Add("A (1/s)", 100)
                FormResultTE.LvResults.Columns.Add("A (1/s)", 100)
                LvTE.Columns.Add("Unc.A (+/- 1/s)", 120)
                FormResultTE.LvResults.Columns.Add("Unc.A (+/- 1/s)", 120)
                LvTE.Columns.Add("El (1/cm)", 100)
                FormResultTE.LvResults.Columns.Add("El (1/cm)", 100)
                LvTE.Columns.Add("Jl", 80)
                FormResultTE.LvResults.Columns.Add("Jl", 80)
                LvTE.Columns.Add("Eu (1/cm)", 100)
                FormResultTE.LvResults.Columns.Add("Eu (1/cm)", 100)
                LvTE.Columns.Add("Ju", 80)
                FormResultTE.LvResults.Columns.Add("Ju", 80)


                'PARAMETROS EXPERIMENTAIS
                FormResultTE.LvResults.Columns.Add("Intensity", 100)
                FormResultTE.LvResults.Columns.Add("Unc.Intensity", 100)

                'ORGANIZAR PARÂMETROS DE ENTRADA
                ATOM = Trim(TextBoxAtom.Text)

                ATOM = Replace(ATOM, " ", "")

                TextBoxAtom.Text = ATOM

                'VERIFICANDO A EXISTENCIA DOS ARQUIVOS RELACIONADOS AO ATOMO ESCOLHIDO NO BANCO DE DADO
                PATHLINES = PATHINDEX & "\DATABASE\LINES\Lines " & ATOM & ".txt"

                'SE NÃO EXISTIR ALGUM DOS ARQUIVOS DE ENTRADA
                If IO.File.Exists(PATHLINES) = False Then
                    MessageBox.Show("Spectral lines of the " & ATOM & "  atom does not exist! Check the Data Base", "", MessageBoxButtons.OK)
                    ButtonSetPar.Enabled = True
                    Me.Cursor = Cursors.Default
                    Return
                Else

                    ' SE EXISTIR O ARQUIVO INÍCIO DA COMPILAÇÃO DE DADOS

                    'DESABILITANDO OS GROUPS
                    GroupBoxAtom.Enabled = False
                    GroupBoxLimits.Enabled = False

                    '###_________LEITURA DOS ARQUIVO DE ENTRADA_________###

                    'ARQUIVO 2) LINHAS DE EMISSÃO E ABS.
                    LINEFILEIN = New IO.StreamReader(PATHLINES)
                    c2 = 0
                    'LE O ARQUIVO ATÉ ACABAR SUAS LINHAS
                    Do While LINEFILEIN.Peek <> -1
                        'LE UMA LINHA DO ARQUIVO SEPARANDO AS VARIÁVEIS DE ACORDO COM O ESPAÇO
                        TEXTLINE = LINEFILEIN.ReadLine.Split(" ")

                        If TEXTLINE(0) <> "" Then
                            'ENTRADA DE DADOS A PARTIR DO ARQUIVO DE NÍVEIS DE ENERGIA
                            Try
                                'A LEITURA SERÁ FEITA ATÉ O RANGE MÁXIMO DO ARRAY
                                N2(c2) = Val(TEXTLINE(0))
                                Lvac2(c2) = Val(TEXTLINE(1))
                                Lair2(c2) = Val(TEXTLINE(2))
                                IR2(c2) = Val(TEXTLINE(3))
                                Aif2(c2) = Val(TEXTLINE(4))
                                DesvA2(c2) = Val(TEXTLINE(5))
                                Ei2(c2) = Val(TEXTLINE(6))
                                Ji2(c2) = Val(TEXTLINE(7))
                                Ef2(c2) = Val(TEXTLINE(8))
                                Jf2(c2) = Val(TEXTLINE(9))
                                c2 = c2 + 1
                                'EXCEÇÃO PARA LEITURAS QUE ULTRAPASSEM O RANGE DO ARRAY
                            Catch ex As IndexOutOfRangeException
                            End Try


                        End If
                    Loop


                    'NUMEROS DE LINHAS DO ARQUIVO DE LINHAS
                    linhasL = c2 - 1
                    c2 = 0

                    'FECHANDO ARQUIVOS DE ENTRADA
                    LINEFILEIN.Close()

                    'CONSTRUÇÃO DA TABELA DE SAIDAS
                    'VERIFICANDO SE AS LINHAS ESTÃO ENTRE OS LIMITES DEFINIDOS

                    For c1 = 0 To linhasL
                        If (RadioButtonAir.Checked = True) Then
                            If Aif2(c1) <> 0 Then
                                'CRIA UMA VARIÁVEL PARA O LISTVIEWITEMS
                                Dim LVItem As New ListViewItem

                                'COMPRIMENTO DE ONDA DEVE SER UM DADO OBRIGATÓRIO
                                LVItem.Text = (N2(c1).ToString("000"))
                                LvTE.Items.Add(LVItem)
                                LVItem.SubItems.Add(Lair2(c1).ToString("0.000000"))
                                LVItem.SubItems.Add((IR2(c1).ToString("0.00")))
                                LVItem.SubItems.Add(Aif2(c1).ToString)
                                LVItem.SubItems.Add((DesvA2(c1).ToString("0.00")))
                                LVItem.SubItems.Add((Ei2(c1).ToString("0.0000")))
                                LVItem.SubItems.Add((Ji2(c1).ToString("0.0")))
                                LVItem.SubItems.Add((Ef2(c1).ToString("0.0000")))
                                LVItem.SubItems.Add((Jf2(c1).ToString("0.0")))


                            End If

                        ElseIf (RadioButtonVac.Checked = True) Then
                            If Aif2(c1) <> 0 Then
                                'CRIA UMA VARIÁVEL PARA O LISTVIEWITEMS
                                Dim LVItem As New ListViewItem

                                'COMPRIMENTO DE ONDA DEVE SER UM DADO OBRIGATÓRIO
                                LVItem.Text = (N2(c1).ToString("000"))
                                LvTE.Items.Add(LVItem)
                                LVItem.SubItems.Add(Lvac2(c1).ToString("0.000000"))
                                LVItem.SubItems.Add((IR2(c1).ToString("0.00")))
                                LVItem.SubItems.Add(Aif2(c1).ToString)
                                LVItem.SubItems.Add((DesvA2(c1).ToString("0.00")))
                                LVItem.SubItems.Add((Ei2(c1).ToString("0.0000")))
                                LVItem.SubItems.Add((Ji2(c1).ToString("0.0")))
                                LVItem.SubItems.Add((Ef2(c1).ToString("0.0000")))
                                LVItem.SubItems.Add((Jf2(c1).ToString("0.0")))
                            End If
                        End If
                    Next

                End If

                'MUDANDO TIPO CURSOR
                Me.Cursor = Cursors.Default

                'HABILITANDO BOTÃO DE EXECUÇÃO
                ButtonExe.Enabled = True
                GroupBoxTE.Enabled = True

                '##################################################FIM ROTINA##################################################################

                '####################################################################################################################

        End Select
    End Sub
    'ABRINDO ARQUIVO EXTERNO PARA SIMULAÇÃO
    Private Sub ButtonOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenFile.Click
        'OpenFileDialogDATA.InitialDirectory = PATHINDEX
        
        'ABRINDO ARQUIVO
        If OpenFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBoxOpenFile.Text = OpenFileDialogDATA.FileName
            ButtonOpenFile.Enabled = False
            
        Else
            TextBoxOpenFile.Text = ""
            ButtonOpenFile.Enabled = True
           
        End If

    End Sub
    'MARCANDO TODAS AS LINHAS
    Private Sub ButtonSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSelectAll.Click
        For Index As Integer = 0 To LvTE.Items.Count - 1
            LvTE.Items.Item(Index).Checked = True
        Next
    End Sub
    'DESMARCANDO TODAS AS LINHAS
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For Index As Integer = 0 To LvTE.Items.Count - 1
            LvTE.Items.Item(Index).Checked = False
        Next
    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxLevel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxLevel.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxLimI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxLimI.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxLimS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxLimS.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxSF1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxSF2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'PERMISSÃO NUMÉRICA PARA TEXTBOX
    Private Sub TextBoxSF3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        If e.KeyChar <> Chr(8) Then
            KeyAscii = CShort(NumberBox(KeyAscii))

            If KeyAscii = 0 Then
                e.Handled = True
            End If
        End If

    End Sub
    'ACESSO AOS ARQUIVOS DE BASE DE DADOS
    Private Sub ButtonDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDatabase.Click
        FormData.Show()
    End Sub
    'DESABILITANDO A OPÇÃO DE SEGUNDO FOTON PARA ESPECTROS DE EMISSÃO
    Private Sub RadioButtonEm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonEm.CheckedChanged
        If RadioButtonEm.Checked = True Then
            CheckBox2fot.Enabled = False
        ElseIf RadioButtonEm.Checked = False Then
            CheckBox2fot.Enabled = True
        End If
    End Sub

    Private Sub RadioButtonDatabase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonDatabase.CheckedChanged
        'TROCA DE TIPO DE ARQUIVO ESCOLHIDO PELO USUÁRIO
        If RadioButtonDatabase.Checked = True Then
            GroupBoxOpenFile.Enabled = False
            TextBoxAtom.Enabled = True
        Else
            'DESABILITAR O USO DO BANCO DE DADOS
            TextBoxAtom.Text = ""
            GroupBoxOpenFile.Enabled = True
            TextBoxAtom.Enabled = False
        End If
    End Sub
End Class
