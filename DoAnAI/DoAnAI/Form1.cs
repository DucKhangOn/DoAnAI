using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnAI
{

    public partial class Form1 : Form
    {
        public const string SourceDir = @"C:\Users\Thanh\Desktop\Test\";
        public struct Node
        {
            public string PreLink;
            public string CurLink;
        };
        List<Node> Queue;
        List<Node> Path;
        List<Node> PathForward;
        List<Node> PathBackward;
        Stack<Node> stackNode;
        Queue<Node> QueueForward;
        Queue<Node> QueueBackward;
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
            string html = ReadFileHtml(SourceDir + sourceTextBox.Text);
            webBrowser.Navigate(SourceDir + sourceTextBox.Text);
            //webBrowser.DocumentText = "0";
            //webBrowser.Document.OpenNew(true);
            //webBrowser.Document.Write(html);
            //webBrowser.Refresh();
            //HtmlElementCollection links = webBrowser.Document.GetElementsByTagName("A");
            //foreach (HtmlElement link in links)
            //{
            //    if (link.InnerText.Equals("2"))
            //    {
            //        link.InvokeMember("Click");
            //    }
            //}
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

            ghiketqua(Path);

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
        public string ReadFileHtml(string path)
        {
            var file = File.ReadAllText(path);
            return file;
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

        public static void ghiketqua(List<Node> Path)
        {
            string filePath = "$" + SourceDir + "path.txt";
            TextWriter writer = new StreamWriter($@"C:\Users\Thanh\Desktop\Test\path.txt");
            foreach (Node node in Path)
            {
                writer.WriteLine(node.PreLink + " -> " + node.CurLink);
            }
            writer.Close();
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
            if (Paths != null)
            {
                foreach (var path in Paths)
                {
                    linkTextBox.Text += path;
                }
            }
            else if (linkTextBox.Text == "")
            {
                linkTextBox.Text = "Không có đường đi";
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

        private void iddfsButton_Click(object sender, EventArgs e)
        {

        }

        private void bidirectionalSearch_Click(object sender, EventArgs e)
        {
            QueueForward = new Queue<Node>();
            QueueBackward = new Queue<Node>();
            PathForward = new List<Node>();
            PathBackward = new List<Node>();
            linkTextBox.Clear();
            var flag = true;
            var source = docketqua(SourceDir + sourceTextBox.Text);
            var des = docketqua(SourceDir + goalLinkTextBox.Text);
            foreach (var s in source)
            {
                if (s == goalLinkTextBox.Text)
                {
                    linkTextBox.Text += sourceTextBox.Text + "->" + s;
                    flag = false;
                    break;
                }
                var temp = docketqua(SourceDir + s);
                if (temp.Contains(SourceDir + sourceTextBox.Text))
                {
                    QueueForward.Enqueue(new Node() { PreLink = SourceDir + sourceTextBox.Text, CurLink = SourceDir + s });
                }
            }
            foreach (var d in des)
            {
                if (d == sourceTextBox.Text)
                {
                    if (source.Contains(SourceDir + d))
                    {
                        linkTextBox.Text += sourceTextBox + "->" + d;
                        flag = false;
                        break;
                    }
                }
                var temp = docketqua(SourceDir + d);
                if (temp.Contains(SourceDir + goalLinkTextBox.Text))
                {
                    QueueForward.Enqueue(new Node() { PreLink = SourceDir + goalLinkTextBox.Text, CurLink = SourceDir + d });

                }
            }
            while (flag)
            {
                if (QueueForward.Count == 0 || QueueBackward.Count == 0)
                {
                    break;
                }
                var forward = QueueForward.Peek();
                var backward = QueueBackward.Peek();
                var forwardAble = docketqua(SourceDir + forward);
                var backwardAble = docketqua(SourceDir + backward);
                if (CheckInPath(forward.CurLink, Path))
                {
                    PathForward.Add(forward);
                }
                foreach (var fa in forwardAble)
                {
                    var temp = docketqua(SourceDir + fa);
                    foreach(var t in temp)
                    {
                        if(t == SourceDir + goalLinkTextBox.Text)
                        {
                            flag = false;
                            PathForward.Add(new Node() { PreLink = SourceDir + fa,CurLink = SourceDir + t});
                            break;
                        }
                        if(t != SourceDir + sourceTextBox.Text)
                        { 
                            if(t == SourceDir + forward.CurLink)
                            {
                                QueueForward.Enqueue(new Node() { PreLink = SourceDir + forward.CurLink, CurLink = SourceDir + t });
                            }
                        }
                    }
                }
                QueueForward.Dequeue();
            }
        }
    }
}
