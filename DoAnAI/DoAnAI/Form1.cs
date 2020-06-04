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
        public const string SourceDir = @"C:\Users\KhangOD\Desktop\Test\";
        public struct Node
        {
            public string PreLink;
            public string CurLink;
        };
        List<Node> Queue;
        List<Node> Path;
        Stack<Node> stackNode;
        public Form1()
        {
            InitializeComponent();
        }

        private void readFileButton_Click(object sender, EventArgs e)
        {
            Queue = new List<Node>();
            Path = new List<Node>();
            linkTextBox.Clear();
            var flag = true;
            int index = 0;
            var s = docketqua(SourceDir + sourceTextBox.Text);
            foreach (var x in s)
            {
                if (x == goalLinkTextBox.Text)
                {
                    linkTextBox.Text += sourceTextBox.Text + "->" + x;
                    flag = false;
                    break;
                }
                Queue.Add(new Node() { PreLink = SourceDir + sourceTextBox.Text, CurLink = SourceDir + x });
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
                            Queue.Add(new Node() { PreLink = Queue[index].CurLink, CurLink = SourceDir + t });
                        }
                        if (t == goalLinkTextBox.Text)
                        {
                            Path.Add(new Node() { PreLink = Queue[index].CurLink, CurLink = SourceDir + t });
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

            List<string> Paths = new List<string>();
            Paths = GetResult(Path, Paths, sourceTextBox.Text, goalLinkTextBox.Text);
            if (Paths != null)
            {
                foreach (var path in Paths)
                {
                    linkTextBox.Text += path;
                }
            }
            else if (linkTextBox.Text == "")
            {
                linkTextBox.Text += "Không có đường đi";
            }
        }
        public static bool CheckInPath(string path, List<Node> Paths)
        {
            foreach (var node in Paths)
            {
                if (SourceDir + path == node.PreLink)
                    return false;
            }
            return true;
        }
        public static List<string> docketqua(string fileName)
        {
            List<string> link = new List<string>();
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

        private void dfsSearchButton_Click(object sender, EventArgs e)
        {
            Path = new List<Node>();
            stackNode = new Stack<Node>();
            linkTextBox.Clear();
            var flag = true;

            var s = docketqua(SourceDir + sourceTextBox.Text);
            s.Reverse();
            foreach (var x in s)
            {
                if (x == goalLinkTextBox.Text)
                {
                    linkTextBox.Text += sourceTextBox.Text + "->" + x;
                    flag = false;
                    break;
                }
                stackNode.Push(new Node() { PreLink = SourceDir + sourceTextBox.Text, CurLink = SourceDir + x });
            }

            while (flag)
            {
                int end = stackNode.Count;
                if (end == 0)
                {
                    break;
                }
                var popop = stackNode.Peek();
                var temp = docketqua(popop.CurLink);
                temp.Reverse();
                if (CheckInPath(popop.CurLink, Path))
                {
                    Path.Add(popop);
                }
                stackNode.Pop();
                if (temp.Count > 0)
                {
                    foreach (var t in temp)
                    {
                        if (CheckInPath(t, Path) == true)
                        {
                            stackNode.Push(new Node() { PreLink = popop.CurLink, CurLink = SourceDir + t });
                        }
                        if (t == goalLinkTextBox.Text)
                        {
                            Path.Add(new Node() { PreLink = popop.CurLink, CurLink = SourceDir + t });
                            flag = false;
                        }
                    }

                }
            }

            List<string> Paths = new List<string>();
            Paths = GetResult(Path, Paths, sourceTextBox.Text, goalLinkTextBox.Text);
            if (Paths != null) {
                foreach (var path in Paths)
                {
                    linkTextBox.Text += path;
                }
            }
            else if (linkTextBox.Text == "")
            {
                linkTextBox.Text += "Không có đường đi";
            }
        }

        private static List<string> GetResult(List<Node> Path, List<string> Paths, string source, string goal)
        {
            var superFlag = false;
            foreach (var node in Path)
            {
                if (node.CurLink == SourceDir + goal)
                {
                    superFlag = true;
                    break;
                }
            }
            if (superFlag)
            {
                var tempFlag = true;
                var tempPath = Path[Path.Count - 1].PreLink;
                Paths.Add(Path[Path.Count - 1].PreLink.Replace(SourceDir, "") + "->" + Path[Path.Count - 1].CurLink.Replace(SourceDir, "") + Environment.NewLine);
                while (tempFlag)
                {
                    foreach (var x in Path)
                    {
                        if (x.CurLink == tempPath)
                        {
                            Paths.Add(x.PreLink.Replace(SourceDir, "") + "->" + x.CurLink.Replace(SourceDir, "") + Environment.NewLine);
                            tempPath = x.PreLink;
                            if (x.PreLink.Replace(SourceDir, "") == source)
                            {
                                tempFlag = false;
                                break;
                            }
                            break;
                        }
                    }
                }
                Paths.Reverse();
                return Paths;
            }
            return null;
        }
    }
}
