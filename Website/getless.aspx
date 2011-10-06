<%@ Page Language="C#" %>
<% 
string root = Server.MapPath("~");
string less = Request.QueryString["less"];
less = less.Replace("/", "\\");
System.IO.StreamReader sr = new System.IO.StreamReader(root + less + ".less");
string line;

while(sr.Peek() != -1)
{
   line = sr.ReadLine();
   Response.Write(line + "\n");
}
%>
