using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        Write("Enter page number: ");
        if (!int.TryParse(ReadLine(), out int pageNumber))
        {
            WriteLine("Something went wrong...");
            return;
        }

        const string url = @"https://tools.ietf.org/html/rfc4648";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.UserAgent = "ConsoleTest";
        Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();

        StreamReader streamReader = new StreamReader(responseStream);
        string line = "";
        bool readPage = false;

        StringBuilder sb = new StringBuilder();

        while (line != null)
        {
            line = streamReader.ReadLine();
            if (line != null)
            {
                line = WebUtility.HtmlDecode(Regex.Replace(line, "<[^>]*(>|$)", ""));
                if (line.Contains(string.Format("[Page {0}]", pageNumber)))
                {
                    readPage = true;
                }
                if (line.Contains(string.Format("[Page {0}]", pageNumber + 1)))
                {
                    break;
                }
                if (readPage)
                {
                    sb.Append(line).Append("\n");
                }
            }
        }

        streamReader.Close();
        WriteLine(sb.ToString());
    }
}
class StringBuilder
{
    private string[] strings;
    private int size;

    public StringBuilder()
    {
        strings = new string[16];
        size = 0;
    }
    public StringBuilder Append(string str)
    {
        if (strings.Length == size)
        {
            Expand();
        }
        strings[size] = str;
        size += 1;
        return this;
    }
    private void Expand()
    {
        int oldCapacity = strings.Length;
        string[] oldArray = strings;
        strings = new string[oldCapacity * 2];
        Array.Copy(oldArray, strings, oldCapacity);
    }
    private int GetTotalLength()
    {
        int charCounter = 0;
        for (int i = 0; i < size; i++)
        {
            string str = strings[i];
            charCounter += str.Length;
        }
        return charCounter;
    }
    public override string ToString()
    {
        int charCounter = GetTotalLength();
        char[] buffer = new char[charCounter];
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            string str = strings[i];
            Array.Copy(str.ToCharArray(), 0, buffer, index, str.Length);
            index += str.Length;
        }
        string allStrings = new string(buffer);
        return allStrings;
    }
}
