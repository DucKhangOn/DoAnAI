using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnAI
{

    public partial class Form1 : Form
    {
        public struct Node
        {
            public string PreLink;
            public string CurLink;
        };
        List<Node> Queue;
        List<Node> Path;
        public Form1()
        {
            InitializeComponent();
            Queue = new List<Node>();
            Path = new List<Node>();
        }

        private void readFileButton_Click(object sender, EventArgs e)
        {
            linkTextBox.Clear();
            var flag = true;
            int index = 0;
            var s = docketqua(@"C:\Users\Thanh\Desktop\Test\1.html");
            foreach (var x in s)
            {
                Queue.Add(new Node() { PreLink = @"C:\Users\Thanh\Desktop\Test\1.html", CurLink = @"C:\Users\Thanh\Desktop\Test\" + x });
            }
            while (flag)
            {
                int end = Queue.Count;
                if (index >= end)
                {
                    break;
                }
                var temp = docketqua(Queue[index].CurLink);
                if (CheckInPath(Queue[index].CurLink, Path))
                {
                    Path.Add(Queue[index]);
                }
                if (temp.Count > 0)
                {
                    foreach (var t in temp)
                    {
                        if (CheckInPath(t, Path) == true)
                        {
                            Queue.Add(new Node() { PreLink = Queue[index].CurLink, CurLink = @"C:\Users\Thanh\Desktop\Test\" + t });
                        }
                        if (t == goalLinkTextBox.Text)
                        {
                            Path.Add(new Node() { PreLink = Queue[index].CurLink, CurLink = @"C:\Users\Thanh\Desktop\Test\" + t });
                            flag = false;
                        }
                    }

                }
                if (temp.Count != 0)
                {
                    Queue.Remove(Queue[index]);
                }
                if (temp.Count == 0)
                {
                    index++;
                }
            }
            var tempFlag = true;
            var tempPath = Path[Path.Count - 1].PreLink;
            List<string> Paths = new List<string>();
            Paths.Add(Path[Path.Count - 1].PreLink.Replace(@"C:\Users\Thanh\Desktop\Test\", "") + "->" + Path[Path.Count - 1].CurLink.Replace(@"C:\Users\Thanh\Desktop\Test\", "") + Environment.NewLine);
            while (tempFlag)
            {
                foreach (var x in Path)
                {
                    if (x.CurLink == tempPath)
                    {
                        Paths.Add(x.PreLink.Replace(@"C:\Users\Thanh\Desktop\Test\", "") + "->" + x.CurLink.Replace(@"C:\Users\Thanh\Desktop\Test\", "") + Environment.NewLine);
                        tempPath = x.PreLink;
                        if (x.PreLink.Replace(@"C:\Users\Thanh\Desktop\Test\", "") == "1.html")
                        {
                            tempFlag = false;
                            break;
                        }
                        break;
                    }              
                }
            }         
            Paths.Reverse();
            foreach(var path in Paths)
            {
                linkTextBox.Text += path;
            }    
        }
        public static bool CheckInPath(string path, List<Node> Paths)
        {
            foreach (var node in Paths)
            {
                if (@"C:\Users\Thanh\Desktop\Test\" + path == node.PreLink)
                    return false;
            }
            return true;
        }
        public static List<string> docketqua(string fileName)
        {
            //string s = "";
            List<string> link = new List<string>();
            //OpenFileDialog openFileDialog1 = new OpenFileDialog
            //{
            //    InitialDirectory = @"C:\",
            //    Title = "Browse Text Files",

            //    CheckFileExists = true,
            //    CheckPathExists = true,

            //    DefaultExt = "txt",
            //    Filter = "html files (*.html)|*.html",
            //    FilterIndex = 2,
            //    RestoreDirectory = true,

            //    ReadOnlyChecked = true,
            //    ShowReadOnly = true
            //};

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    s = openFileDialog1.FileName;
            //}
            if (fileName != "")
            {
                String input = File.ReadAllText(fileName);
                foreach (var row in input.Split('\n'))
                {
                    if (row.Contains("<a"))
                    {
                        string temp = "";
                        var href = row.IndexOf("href");
                        var html = row.IndexOf(".html");
                        for (int i = href + 6; i < html; i++)
                        {
                            temp += row[i];
                        }
                        link.Add(temp + ".html");
                    }
                }
                return link;
            }
            return null;
        }

    }
}
