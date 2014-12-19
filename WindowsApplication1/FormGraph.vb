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

Public Class FormGraph
    Dim PATHINDEX As String
    'SALVANDO GRÁFICO DO ESPECTRO SIMULADO
    Private Sub ButtonGraphicSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGraphicSave.Click

        If (Me.Text.Contains("Temperature") = True) Then
            SaveFileDialogGRAPH.InitialDirectory = PATHINDEX & "\RESULTS\ELECTRONIC TEMPERATURE"

        ElseIf (Me.Text.Contains("Possible") = True) Or (Me.Text.Contains("Diagram") = True) Then
            SaveFileDialogGRAPH.InitialDirectory = PATHINDEX & "\RESULTS\TRANSITIONS FROM A LEVEL"

        ElseIf Me.Text.Contains("Simulated") = True Then
            SaveFileDialogGRAPH.InitialDirectory = PATHINDEX & "\RESULTS\SPECTRA SIMULATION"
        End If

        SaveFileDialogGRAPH.FileName = Replace(Me.Text, "OPTIONS - ", "")

        If SaveFileDialogGRAPH.ShowDialog() = Windows.Forms.DialogResult.OK Then
            FileCopy(PATHINDEX & "\GraphicSS.jpeg", SaveFileDialogGRAPH.FileName)
        End If

    End Sub
    'SALVANDO ARQUIVO DE DADOS DO GRÁFICO
    Private Sub ButtonDataSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDataSS.Click

        If Me.Text.Contains("Temperature") = True Then
            SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\ELECTRONIC TEMPERATURE"

        ElseIf Me.Text.Contains("Simulated") = True Then
            SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\SPECTRA SIMULATION"

        ElseIf Me.Text.Contains("Transitions") = True Then
            SaveFileDialogDATA.InitialDirectory = PATHINDEX & "\RESULTS\POSSIBLE TRANSITIONS"

        End If

        SaveFileDialogDATA.FileName = Replace(Me.Text, "OPTIONS - ", "")

        If SaveFileDialogDATA.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If Me.Text.Contains("Population") = True Then
                FileCopy(PATHINDEX & "\DATAGRAPH2.txt", SaveFileDialogDATA.FileName)

            ElseIf (Me.Text.Contains("- Arrows Diagram -") = True) Or (Me.Text.Contains("- Simulated") = True) Then
                FileCopy(PATHINDEX & "\DATAGRAPH1.txt", SaveFileDialogDATA.FileName)

            Else
                FileCopy(PATHINDEX & "\DATAGRAPH.txt", SaveFileDialogDATA.FileName)
            End If
        End If
    End Sub

    Private Sub FormGraph_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'HABILITANDO FUNÇOES APÓS FECHAMENTO DO PROGRAMA
        FormMain.ButtonCancel.Enabled = True
        FormMain.Enabled = True
    End Sub

    Private Sub FormGraph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'LOCALIZANDO PATH PARA UTILIZAÇÃO DO BANCO DE DADOS
        FormMain.Enabled = False
        PATHINDEX = Application.StartupPath
        System.IO.Directory.SetCurrentDirectory(PATHINDEX)
    End Sub

    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        'FECHAMENTO DO GNUPLOT
        Shell("taskkill /f /fi ""windowtitle eq gnuplot""", AppWinStyle.Hide)
        Shell("taskkill /f /fi ""windowtitle eq gnuplot graph""", AppWinStyle.Hide)
        Me.Close()
    End Sub
End Class