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
        public const string SourceDir = @"C:\Users\KhangOD\Desktop\Tess\";
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
        List<string> showList;
        public Form1()
        {
            InitializeComponent();
        }

        private void readFileButton_Click(object sender, EventArgs e)
        {
            showList = new List<string>();
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
                    showList.Add(path);
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
            TextWriter writer = new StreamWriter($@"C:\Users\KhangOD\Desktop\Test\path.txt");
            foreach (Node node in Path)
            {
                writer.WriteLine(node.PreLink + " -> " + node.CurLink);
            }
            writer.Close();
        }

        private void dfsSearchButton_Click(object sender, EventArgs e)
        {
            showList = new List<string>();
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
                    showList.Add(path);
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
        private static List<string> GetForwardResult(List<Node> Path, List<string> Paths, string source, string goal)
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
                var tempPath = SourceDir + goal;
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
        private static List<string> GetBackwardResult(List<Node> Path, List<string> Paths, string source, string goal)
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
                var tempPath = SourceDir + goal;
                while (tempFlag)
                {
                    foreach (var x in Path)
                    {
                        if (x.CurLink == tempPath)
                        {
                            Paths.Add(x.CurLink.Replace(SourceDir, "") + "->" + x.PreLink.Replace(SourceDir, "") + Environment.NewLine);
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
            int iterative = 1;
            string desLink = "";
            while (!desLink.Equals(goalLinkTextBox.Text))
            {
                iterative++;
                stackNode = new Stack<Node>();
                Path = new List<Node>();
                var source = docketqua(SourceDir + sourceTextBox.Text);
                source.Reverse();       
                foreach (var s in source)
                {
                    if (s == goalLinkTextBox.Text)
                    {
                        linkTextBox.Text += sourceTextBox.Text + "->" + s;
                        desLink = s;
                        break;
                    }
                    stackNode.Push(new Node() { PreLink = SourceDir + sourceTextBox.Text, CurLink = SourceDir + s });
                }
                if (stackNode.Count == 0)
                {
                    break;
                }
                var temp = stackNode.Peek();
                var tempLink = docketqua(temp.CurLink);
                tempLink.Reverse();
                if(CheckInPath(temp.CurLink,Path))
                {
                    Path.Add(temp);
                }
                if (tempLink.Count > 0)
                {
                    foreach (var t in tempLink)
                    {
                        if (CheckInPath(t, Path) == true)
                        {
                            stackNode.Push(new Node() { PreLink = temp.CurLink, CurLink = SourceDir + t });
                        }
                        if (t == goalLinkTextBox.Text)
                        {
                            Path.Add(new Node() { PreLink = temp.CurLink, CurLink = SourceDir + t });
                            desLink = goalLinkTextBox.Text;
                            break;
                        }

                    }
                }
            }
        }

        private void bidirectionalSearch_Click(object sender, EventArgs e)
        {
            showList = new List<string>();
            Node IntersectionNodeForward = new Node();
            Node IntersectionNodeBackward = new Node();
            QueueForward = new Queue<Node>();
            QueueBackward = new Queue<Node>();
            PathForward = new List<Node>();
            PathBackward = new List<Node>();
            Path = new List<Node>();
            linkTextBox.Clear();
            var flag = true;
            var source = docketqua(SourceDir + sourceTextBox.Text);
            var des = docketqua(SourceDir + goalLinkTextBox.Text);
            foreach (var s in source)
            {
                if (s == goalLinkTextBox.Text)
                {
                    linkTextBox.Text += sourceTextBox.Text + "->" + s;
                    IntersectionNodeForward.PreLink = SourceDir + sourceTextBox.Text;
                    IntersectionNodeForward.CurLink = SourceDir + s;
                    flag = false;
                    break;
                }
                var temp = docketqua(SourceDir + s);
                if (temp.Contains(sourceTextBox.Text))
                {
                    QueueForward.Enqueue(new Node() { PreLink = SourceDir + sourceTextBox.Text, CurLink = SourceDir + s });
                }
            }
            foreach (var d in des)
            {
                if (d == sourceTextBox.Text)
                {
                    if (source.Contains(d))
                    {
                        linkTextBox.Text += sourceTextBox + "->" + d;
                        IntersectionNodeBackward.PreLink = SourceDir + goalLinkTextBox.Text;
                        IntersectionNodeBackward.CurLink = SourceDir + d;
                        flag = false;
                        break;
                    }
                }
                var temp = docketqua(SourceDir + d);
                if (temp.Contains(goalLinkTextBox.Text))
                {
                    QueueBackward.Enqueue(new Node() { PreLink = SourceDir + goalLinkTextBox.Text, CurLink = SourceDir + d });

                }
            }
            Node forward = new Node();
            while (flag)
            {
                if (QueueForward.Count == 0 && QueueBackward.Count == 0)
                {
                    break;
                }
                if (QueueForward.Count > 0)
                {
                    forward = QueueForward.Peek();
                    var forwardAble = docketqua(forward.CurLink);
                    if (CheckInPath(forward.CurLink, PathForward))
                    {
                        PathForward.Add(forward);
                    }
                    foreach (var fa in forwardAble)
                    {
                        if (fa == goalLinkTextBox.Text)
                        {
                            IntersectionNodeForward = forward;
                            PathForward.Add(new Node() { PreLink = forward.CurLink, CurLink = SourceDir + fa });
                            break;
                        }
                        if (fa != forward.PreLink.Replace(SourceDir, ""))
                        {
                            var temp = docketqua(SourceDir + fa);
                            if (temp.Contains(forward.CurLink.Replace(SourceDir, "")))
                            {
                                QueueForward.Enqueue(new Node() { PreLink = forward.CurLink, CurLink = SourceDir + fa });
                            }
                        }
                    }
                    QueueForward.Dequeue();
                }
                if(QueueBackward.Count > 0)
                {
                    var backward = QueueBackward.Peek();
                    var backwardAble = docketqua(backward.CurLink);

                    if (CheckInPath(backward.CurLink, PathBackward))
                    {
                        PathBackward.Add(backward);
                    }

                    foreach (var ba in backwardAble)
                    {
                        if (ba == forward.PreLink.Replace(SourceDir, ""))
                        {
                            IntersectionNodeBackward = backward;
                            PathBackward.Add(new Node() { PreLink = backward.CurLink, CurLink = SourceDir + ba });
                            break;
                        }
                        if (ba != backward.PreLink.Replace(SourceDir, ""))
                        {
                            var temp = docketqua(SourceDir + ba);
                            if (temp.Contains(backward.CurLink.Replace(SourceDir, "")))
                            {
                                QueueBackward.Enqueue(new Node() { PreLink = backward.CurLink, CurLink = SourceDir + ba });
                            }
                        }
                    }
                    QueueBackward.Dequeue();
                }
                foreach(var fw in PathForward)
                {
                    foreach(var bw in PathBackward)
                    {
                        if (fw.CurLink.Equals(bw.CurLink))
                        {
                            IntersectionNodeForward = fw;
                            IntersectionNodeBackward = bw;
                            flag = false;
                            break;
                        }
                    }
                }
            }
            List<string> Path1 = new List<string>();
            List<string> Path2 = new List<string>();
            List<string> Path3 = new List<string>();
            if (IntersectionNodeForward.CurLink != null && IntersectionNodeBackward.CurLink != null && IntersectionNodeForward.CurLink == IntersectionNodeBackward.CurLink)
            {
                Path1 = GetForwardResult(PathForward, Path1, sourceTextBox.Text, IntersectionNodeForward.CurLink.Replace(SourceDir, ""));
                Path2 = GetBackwardResult(PathBackward, Path2, goalLinkTextBox.Text, IntersectionNodeBackward.CurLink.Replace(SourceDir, ""));
                string t = IntersectionNodeForward.PreLink.Replace(SourceDir, "") + "->" + IntersectionNodeForward.CurLink.Replace(SourceDir, "") + Environment.NewLine;
                Path2.Remove(t);
                Path2.Reverse();
                Path3 = Path1.Concat(Path2).ToList();
                foreach(var path in Path3)
                {
                    linkTextBox.Text += path;
                    showList.Add(path);
                }    
                linkTextBox.Text += "Trung gian: " + IntersectionNodeForward.CurLink.Replace(SourceDir, "");
            }
            else if (linkTextBox.Text == "")
            {
                linkTextBox.Text += "Không có đường đi";
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (showList != null)
            {
                List<string> hehe = new List<string>();
                foreach (var row in showList)
                {
                    string temp = "";
                    var end = row.IndexOf("->");
                    for (int i = 0; i < end; i++)
                    {
                        temp += row[i];
                    }
                    hehe.Add(temp);
                }
                //Show từng file
                for (int i = 0; i < hehe.Count; i++)
                {
                    MessageBox.Show("Access to file " + hehe[i]);
                    label3.Text = "Url: " + hehe[i];
                    //Thread.Sleep(1000);
                    webBrowser.Navigate(SourceDir + hehe[i]);
                }
            }
            else
            {
                MessageBox.Show("Nothing in Path");
            }
        }
    }
}
