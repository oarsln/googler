'GitHub: oarsln
Public Class Form1
    Dim WithEvents WB As New WebBrowser
    Sub Search(ByVal Query As String)
        TextBox1.Enabled = False
        Try
            Dim WClient As New Net.WebClient
            WClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.0; rv:29.0) Gecko/20100101 Firefox/29.0")
            Dim Kaynak As String = WClient.DownloadString("https://www.google.com.tr/search?num=100&q=" & TextBox1.Text)
            Kaynak = Kaynak.Replace(vbNewLine, String.Empty)
            Kaynak = Kaynak.Replace(StrReverse(Chr(34) & Chr(61) & Chr(102) & Chr(101) & Chr(114) & Chr(104) & Chr(32) & Chr(97) & Chr(60) & Chr(62) & Chr(34) & Chr(114) & Chr(34) & Chr(61) & Chr(115) & Chr(115) & Chr(97) & Chr(108) & Chr(99) & Chr(32) & Chr(51) & Chr(104) & Chr(60)), vbNewLine)
            For Each Satır In Split(Kaynak, vbNewLine)
                If Satır.Replace(Satır.Substring(Satır.IndexOf(Chr(34))), String.Empty).Contains("://") Then
                    Dim Link As New Uri(Satır.Replace(Satır.Substring(Satır.IndexOf(Chr(34))), String.Empty).Replace("&amp;", "&"))
                    ListView1.Items.Add(Satır.Replace(Satır.Substring(Satır.IndexOf(Chr(34))), String.Empty).Replace("&amp;", "&"))
                End If
            Next
        Catch ex As Exception
        End Try
        TextBox1.Enabled = True
        TextBox1.Text = "GitHub: oarsln"
    End Sub
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Control.CheckForIllegalCrossThreadCalls = False
            Dim Google As New Threading.Thread(AddressOf Search)
            Google.Start()
        End If
    End Sub
End Class
