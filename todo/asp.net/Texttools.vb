Imports Microsoft.VisualBasic
Namespace Estate.Common.Text
	Public Class Texttools
		''' <summary>
		''' Cut string after some number of characters
		''' </summary>
		Public Shared Function CutString(ByVal lStr As String, ByVal iMaxLen As Integer, Optional ByVal _postFix As String = "") As String
			Dim strReturn As String = lStr

			If strReturn.Length > iMaxLen Then
				strReturn = lStr.Substring(0, iMaxLen)
				strReturn = Trim(strReturn)
				If strReturn.Length = iMaxLen Then
					strReturn = strReturn.Substring(0, InStrRev(strReturn, " ") - 1)
				End If
				strReturn &= " " & _postFix
			End If

			Return strReturn
		End Function

		''' <summary>
		''' Print string with BR if string is not empty
		''' </summary>
		Public Overloads Shared Function PrintLine(ByVal _printString As String) As String
			If _printString.Length > 0 Then
				Return _printString & "<br />"
			Else
				Return ""
			End If
		End Function

		Public Overloads Shared Function PrintLine(ByVal _identifier As String, ByVal _printString As String) As String
			If _printString.Length > 0 Then
				Return "<strong>" & _identifier & "</strong> " & _printString & "<br />"
			Else
				Return ""
			End If
		End Function

		''' <summary>
		''' 
		''' </summary>
		Public Shared Function TagAttributeSafe(ByVal lStr As String) As String
			Dim strReturn As String = lStr

			Return Replace(strReturn, """", "'")
		End Function

		''' <summary>
		''' Convert HTML to text while preserving as much formatting as possible
		''' </summary>
		Public Shared Function HTML2Text(ByVal TextHTML As String) As String
			Dim regEx As New Regex(String.Empty, RegexOptions.Multiline Or RegexOptions.IgnoreCase)

			'Remove rubbish before the body
			If InStr(1, TextHTML, "<body", CompareMethod.Text) > 0 Then TextHTML = Right(TextHTML, TextHTML.Length - InStr(1, TextHTML, "<body", CompareMethod.Text) + 1)

			'Remove space/tabs
			TextHTML = regEx.Replace(TextHTML, "\s+", " ")

			'Insert new formatting
			TextHTML = regEx.Replace(TextHTML, "<br[^>]*>|<p[^>]*>|<div[^>]*>|<tr[^>]*>|<li[^>]*>", vbCrLf)
			TextHTML = regEx.Replace(TextHTML, "<td[^>]*>", vbTab)

			'Remove style and script structures.
			TextHTML = regEx.Replace(TextHTML, "<style[^>]*>.*</style>", String.Empty)
			TextHTML = regEx.Replace(TextHTML, "<script[^>]*>.*</script>", String.Empty)

			'Convert HTML links to Text and URL. If link are text are same, ignore text 
			TextHTML = regEx.Replace(TextHTML, "<a[^>]+href=['|""]([^'|""]*)['|""][^>]*>\1</a>", " $1 ")
			TextHTML = regEx.Replace(TextHTML, "<a[^>]+href=['|""]([^'|""]+)['|""][^>]*>(.*)</a>", " $2 $1 ")

			'Line feeds and other tags inside the anchor are sometimes missed by the last statement (and this one misses ones the last want catches)
			TextHTML = regEx.Replace(TextHTML, "<a[^>]+href=['|""]([^'|""]+)['|""][^>]*>([^<]*)", " $2 $1 ")

			'Strip remaining HTML
			TextHTML = regEx.Replace(TextHTML, "<[^>]*>", " ")

			'Replace html encoded chars (ie &#64;,&nbsp;)
			TextHTML = HttpContext.Current.Server.HtmlDecode(TextHTML)

			'Remove surplus space and line feeds & left trim (from tag removal)
			TextHTML = regEx.Replace(TextHTML, "^ +", String.Empty)
			Do While TextHTML.Contains("  ")
				TextHTML = TextHTML.Replace("  ", " ")
			Loop
			Do While TextHTML.Contains(vbCrLf & vbCrLf & vbCrLf)
				TextHTML = TextHTML.Replace(vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
			Loop

			Return TextHTML
		End Function

		Public Shared Function stripFirstLastP(ByVal _tekst As String) As String
			Dim regEx As New Regex(String.Empty, RegexOptions.Multiline Or RegexOptions.IgnoreCase)
			Dim werkstring As String = _tekst
			werkstring = regEx.Replace(werkstring, "^<p[^>]*>", "")
			werkstring = regEx.Replace(werkstring, "</p>$", "")
			stripFirstLastP = werkstring


		End Function

		Public Shared Sub ScrambleEmailAddress(ByVal oHyperlink As HyperLink)
			If oHyperlink.NavigateUrl.IndexOf("scrambled:") = -1 Then
				oHyperlink.NavigateUrl = oHyperlink.NavigateUrl.Replace("mailto:", "")
				oHyperlink.NavigateUrl = "scrambled:" & ScrambleText(oHyperlink.NavigateUrl)
				If oHyperlink.Text = "" Then
					oHyperlink.Text = "[Zet JavaScript aan voor mailadres]"
				End If
			End If
		End Sub

		Private Shared Function ScrambleText(ByVal value As String) As String
			Dim EncodedString As New System.Text.StringBuilder(value.Length * 6)
			Dim Character As Char

			For Each Character In value
				EncodedString.Append("[")
				EncodedString.Append(Convert.ToInt32(Character))
				EncodedString.Append("]")
			Next

			Return EncodedString.ToString()
		End Function


		Public Shared Function URLRewrite(ByVal _url As String) As String
			Dim strTemp, strSearchPatterns, strReplacePatterns As String
			strTemp = "" & _url
			strSearchPatterns = "çÇâäàáåÂÄÀÁÅêëèéÊËÈÉïîìíÏÎÌÍôöòóÔÖÒÓûüùúÛÜÙÚÿýÝñÑ\'""?&/|\`!~@#$%€^*();:[]{},.<>+=-"
			strReplacePatterns = "cCaaaaaAAAAAeeeeEEEEiiiiIIIIooooOOOOuuuuUUUUyyYnN                                 _"
			For i = 1 To len(strSearchPatterns)
				strTemp = replace("" & strTemp, mid(strSearchPatterns, i, 1), mid(strReplacePatterns, i, 1))
			Next
			strTemp = Replace(Trim("" & strTemp), " ", "_")
			Dim re As New Regex("([_]+)")
			strTemp = re.Replace(strTemp, "-").ToLower
			Return strTemp
		End Function
	End Class
End Namespace
