using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace HtmlParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            
            string html = HtmlContentParser.ParseTable(txtInput.Text);

            txtOutput.Text = html;
        }
    }

   
}
public class HtmlObjLine
{
    private string _tag;
    private int _level;

    public string Tag
    {
        get { return _tag; }   // get method
        set { _tag = value; }  // set method
    }

    public int Level
    {
        get { return _level; }   // get method
        set { _level = value; }  // set method
    }
}
public class HtmlContentParser : HtmlObjLine
{



    public static string ParseTable(string input)
    {
        StringBuilder builder = new StringBuilder();
        Stack<HtmlObjLine> stack = new Stack<HtmlObjLine>();

        foreach (string line in input.Split('\n'))
        {
            int level = line.Length - line.TrimStart(' ').Length;

            // Remove indentation and trim any trailing spaces
            string content = line.TrimStart(' ').TrimEnd();

            string tag = content.Split(' ')[0];


            if (level == 0)
            {
                // Top-level element, should be "table"
                //stack.Push("table");
                builder.AppendLine("<table>");
                HtmlObjLine htmlLine = new HtmlObjLine();
                htmlLine.Tag = content;
                htmlLine.Level = level;
                stack.Push(htmlLine);
            }
            else
            {
                // Determine the tag name based on the content of the line


                HtmlObjLine lastObjLine = stack.Peek();

                if (level > lastObjLine.Level)
                {
                    builder.AppendLine(new string(' ', level) + $"<{tag}>");
                    HtmlObjLine newhtmlLine = new HtmlObjLine();
                    newhtmlLine.Level = level;
                    newhtmlLine.Tag = tag;

                    stack.Push(newhtmlLine);
                    // Handle text content (if any)
                    if (content.Length > tag.Length)
                    {
                        string text = content.Substring(tag.Length + 1);
                        builder.AppendLine(new string(' ', level + 2) + text);

                    }
                }
                else if (level == lastObjLine.Level)
                {

                    stack.Pop();
                    builder.AppendLine(new string(' ', lastObjLine.Level) + $"</{lastObjLine.Tag}>");
                    builder.AppendLine(new string(' ', level) + $"<{tag}>");

                    // Handle text content (if any)
                    if (content.Length > tag.Length)
                    {
                        string text = content.Substring(tag.Length + 1);
                        builder.AppendLine(new string(' ', level + 2) + text);
                        builder.AppendLine(new string(' ', level) + $"</{tag}>");

                    }
                    HtmlObjLine newhtmlLine = new HtmlObjLine();
                    newhtmlLine.Level = level;
                    newhtmlLine.Tag = tag;
                    stack.Push(newhtmlLine);
                }
                else
                {
                    stack.Pop();
                    builder.AppendLine(new string(' ', lastObjLine.Level) + $"</{lastObjLine.Tag}>");

                    //Close higher or same level previous tag before proceed
                    while (lastObjLine.Level >= level)
                    {
                        HtmlObjLine currhtmlLine = stack.Pop();
                        builder.AppendLine(new string(' ', currhtmlLine.Level) + $"</{currhtmlLine.Tag}>");
                        lastObjLine = stack.Peek();
                    }

                    builder.AppendLine(new string(' ', level) + $"<{tag}>");
                    HtmlObjLine newhtmlLine = new HtmlObjLine();
                    newhtmlLine.Level = level;
                    newhtmlLine.Tag = tag;
                    stack.Push(newhtmlLine);
                }
               
            }

           

        }

        // Close any remaining tags
        while (stack.Count > 0)
        {
            HtmlObjLine currhtmlLine = stack.Pop();
            builder.AppendLine(new string(' ', currhtmlLine.Level) + $"</{currhtmlLine.Tag}>");
        }

        return builder.ToString();
    }

 


}


