﻿using System;
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
        public const string SourceDir = @"Data\";
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
        Node IntersectionNodeForward;
        Node IntersectionNodeBackward;
        public struct TempNode
        {
            public string CurLink;
            public List<string> NextLink;
        }
        List<TempNode> LinkMap;
        Queue<Node> TestQueue;
        public Form1()
        {
            InitializeComponent();
            GetBaseMap();
            IntersectionNodeForward = new Node();
            IntersectionNodeBackward = new Node();
        }
        public void GetBaseMap()
        {
            LinkMap = new List<TempNode>();
            TestQueue = new Queue<Node>();
            foreach (var file in Directory.GetFiles(SourceDir))
            {
                var temp = docketqua(file);
                LinkMap.Add(new TempNode()
                {
                    CurLink = file,
                    NextLink = temp
                });
            }

        }

        private void readFileButton_Click(object sender, EventArgs e)
        {
            if (!sourceTextBox.Text.Trim().Contains(".html") || !goalLinkTextBox.Text.Trim().Contains(".html"))
            {
                MessageBox.Show("Vui lòng nhập với định dạng *.html");
            }
            else
            {
                showList = new List<string>();
                Queue = new List<Node>();
                Path = new List<Node>();
                linkTextBox.Clear();
                var flag = true;
                int index = 0;
                if (sourceTextBox.Text.Trim().Trim().Equals(goalLinkTextBox.Text.Trim().Trim()))
                {
                    linkTextBox.Text = sourceTextBox.Text.Trim();
                }
                else
                {
                    try
                    {
                        var s = docketqua(SourceDir + sourceTextBox.Text.Trim());
                        string html = ReadFileHtml(SourceDir + sourceTextBox.Text.Trim());
                        webBrowser.Navigate(System.IO.Path.Combine(Environment.CurrentDirectory,SourceDir) + sourceTextBox.Text.Trim());
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
                            if (x == goalLinkTextBox.Text.Trim())
                            {
                                linkTextBox.Text += sourceTextBox.Text.Trim() + "->" + x;
                                flag = false;
                                break;
                            }
                            Queue.Add(new Node() { PreLink = SourceDir + sourceTextBox.Text.Trim(), CurLink = SourceDir + x });
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
                                    if (t == goalLinkTextBox.Text.Trim())
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
                        Paths = GetResult(Path, Paths, sourceTextBox.Text.Trim(), goalLinkTextBox.Text.Trim());
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
                    catch (Exception x)
                    {
                        Console.WriteLine(x);
                        MessageBox.Show("Bạn đã nhập không đúng tên file", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            if (!sourceTextBox.Text.Trim().Contains(".html") || !goalLinkTextBox.Text.Trim().Contains(".html"))
            {
                MessageBox.Show("Vui lòng nhập với định dạng *.html");
            }
            else
            {
                showList = new List<string>();
                Path = new List<Node>();
                stackNode = new Stack<Node>();
                linkTextBox.Clear();
                var flag = true;
                webBrowser.Navigate(System.IO.Path.Combine(Environment.CurrentDirectory, SourceDir) + sourceTextBox.Text.Trim());
                if (sourceTextBox.Text.Trim().Equals(goalLinkTextBox.Text.Trim()))
                {
                    linkTextBox.Text = sourceTextBox.Text.Trim();
                }
                else
                {
                    try
                    {
                        var s = docketqua(SourceDir + sourceTextBox.Text.Trim());
                        s.Reverse();
                        foreach (var x in s)
                        {
                            if (x == goalLinkTextBox.Text.Trim())
                            {
                                linkTextBox.Text += sourceTextBox.Text.Trim() + "->" + x;
                                flag = false;
                                break;
                            }
                            stackNode.Push(new Node() { PreLink = SourceDir + sourceTextBox.Text.Trim(), CurLink = SourceDir + x });
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
                                    if (t == goalLinkTextBox.Text.Trim())
                                    {
                                        Path.Add(new Node() { PreLink = popop.CurLink, CurLink = SourceDir + t });
                                        flag = false;
                                    }

                                }

                            }
                        }

                        List<string> Paths = new List<string>();
                        Paths = GetResult(Path, Paths, sourceTextBox.Text.Trim(), goalLinkTextBox.Text.Trim());
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
                    catch (Exception x)
                    {
                        Console.WriteLine(x);
                        MessageBox.Show("Bạn đã nhập không đúng tên file", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            while (!desLink.Equals(goalLinkTextBox.Text.Trim()))
            {
                iterative++;
                stackNode = new Stack<Node>();
                Path = new List<Node>();
                var source = docketqua(SourceDir + sourceTextBox.Text.Trim());
                source.Reverse();
                foreach (var s in source)
                {
                    if (s == goalLinkTextBox.Text.Trim())
                    {
                        linkTextBox.Text += sourceTextBox.Text.Trim() + "->" + s;
                        desLink = s;
                        break;
                    }
                    stackNode.Push(new Node() { PreLink = SourceDir + sourceTextBox.Text.Trim(), CurLink = SourceDir + s });
                }
                if (stackNode.Count == 0)
                {
                    break;
                }
                var temp = stackNode.Peek();
                var tempLink = docketqua(temp.CurLink);
                tempLink.Reverse();
                if (CheckInPath(temp.CurLink, Path))
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
                        if (t == goalLinkTextBox.Text.Trim())
                        {
                            Path.Add(new Node() { PreLink = temp.CurLink, CurLink = SourceDir + t });
                            desLink = goalLinkTextBox.Text.Trim();
                            break;
                        }

                    }
                }
            }
        }
        private void BDS(Queue<Node> fwQueue, Queue<Node> bwQueue, List<Node> fwPath, List<Node> bwPath, List<TempNode> linkMap)
        {
            List<Node> fwBFS = new List<Node>();
            List<Node> bwBFS = new List<Node>();
            var sourceNode = linkMap.Where(u => u.CurLink == SourceDir + sourceTextBox.Text.Trim()).FirstOrDefault();
            IntersectionNodeForward = new Node()
            {
                CurLink = sourceNode.CurLink
            };
            var desNode = linkMap.Where(u => u.CurLink == SourceDir + goalLinkTextBox.Text.Trim()).FirstOrDefault();
            IntersectionNodeBackward = new Node()
            {
                CurLink = desNode.CurLink
            };
            var bwNode = linkMap.Where(u => u.NextLink.Contains(goalLinkTextBox.Text.Trim())).ToList();
            foreach (var s in sourceNode.NextLink)
            {
                if (s.Equals(goalLinkTextBox.Text.Trim()))
                {
                    IntersectionNodeForward.PreLink = sourceNode.CurLink;
                    IntersectionNodeForward.CurLink = SourceDir + s;
                    IntersectionNodeBackward.CurLink = SourceDir + s;
                    IntersectionNodeBackward.PreLink = SourceDir + goalLinkTextBox.Text.Trim();
                    break;
                }
                Node node = new Node()
                {
                    PreLink = sourceNode.CurLink,
                    CurLink = SourceDir + s
                };
                fwBFS.Add(node);
                fwQueue.Enqueue(node);
            }
            foreach (var d in bwNode)
            {
                if (d.CurLink.Replace(SourceDir, "").Equals(sourceTextBox.Text.Trim()))
                {
                    IntersectionNodeForward.PreLink = sourceNode.CurLink;
                    IntersectionNodeForward.CurLink = SourceDir + d;
                    IntersectionNodeBackward.CurLink = SourceDir + d;
                    IntersectionNodeBackward.PreLink = SourceDir + goalLinkTextBox.Text.Trim();
                    break;
                }
                Node node = new Node()
                {
                    PreLink = desNode.CurLink,
                    CurLink = d.CurLink
                };
                bwBFS.Add(node);
                bwQueue.Enqueue(node);
            }
            while (!IntersectionNodeForward.CurLink.Equals(IntersectionNodeBackward.CurLink) && !IntersectionNodeForward.CurLink.Equals(IntersectionNodeBackward.PreLink))
            {
                Node sourceTemp = new Node();
                Node desTemp = new Node();
                fwBFS = new List<Node>();
                bwBFS = new List<Node>();
                if (fwQueue.Count == 0 && bwQueue.Count == 0)
                {
                    break;
                }
                if (fwQueue.Count > 0)
                {
                    sourceTemp = fwQueue.Peek();
                    IntersectionNodeForward = sourceTemp;
                    if (CheckInPath(sourceTemp.CurLink, fwPath))
                    {
                        fwPath.Add(sourceTemp);
                    }

                }
                if (bwQueue.Count > 0)
                {
                    desTemp = bwQueue.Peek();
                    IntersectionNodeBackward = desTemp;
                    if (CheckInPath(desTemp.CurLink, bwPath))
                    {
                        bwPath.Add(desTemp);
                    }
                }
                if (IntersectionNodeForward.CurLink.Equals(IntersectionNodeBackward.PreLink))
                {
                    break;
                }
                if (sourceTemp.CurLink != null)
                {
                    var nextTemp = linkMap.Where(u => u.CurLink.Equals(sourceTemp.CurLink)).FirstOrDefault();
                    foreach (var temp in nextTemp.NextLink)
                    {
                        if (temp.Equals(goalLinkTextBox.Text))
                        {
                            IntersectionNodeForward.PreLink = nextTemp.CurLink;
                            IntersectionNodeForward.CurLink = SourceDir + temp;
                            IntersectionNodeBackward.CurLink = nextTemp.CurLink;
                            IntersectionNodeBackward.PreLink = SourceDir + goalLinkTextBox.Text.Trim();
                            PathForward.Add(IntersectionNodeForward);
                            PathBackward.Add(IntersectionNodeBackward);
                            fwBFS.Add(IntersectionNodeForward);
                            bwBFS.Add(IntersectionNodeBackward);
                            break;
                        }
                        if (temp != sourceTemp.PreLink.Replace(SourceDir, ""))
                        {
                            Node node = new Node()
                            {
                                PreLink = nextTemp.CurLink,
                                CurLink = SourceDir + temp
                            };
                            fwQueue.Enqueue(node);
                            fwBFS.Add(node);
                        }
                    }
                    fwQueue.Dequeue();
                }
                if (desTemp.CurLink != null)
                {
                    var previousTemp = linkMap.Where(u => u.NextLink.Contains(desTemp.CurLink.Replace(SourceDir, "")) && u.CurLink != desTemp.PreLink).ToList();
                    foreach (var temp in previousTemp)
                    {
                        if(temp.Equals(sourceTextBox.Text))
                        {
                            IntersectionNodeForward.PreLink = SourceDir + sourceTextBox.Text;
                            IntersectionNodeForward.CurLink = SourceDir + temp;
                            IntersectionNodeBackward.CurLink = SourceDir + temp;
                            IntersectionNodeBackward.PreLink = desTemp.PreLink;
                            PathForward.Add(IntersectionNodeForward);
                            PathBackward.Add(IntersectionNodeBackward);
                            fwBFS.Add(IntersectionNodeForward);
                            bwBFS.Add(IntersectionNodeBackward);
                            break;
                        }    
                        if(temp.CurLink != desTemp.PreLink.Replace(SourceDir, ""))
                        {
                            Node node = new Node()
                            {
                                PreLink = desTemp.CurLink,
                                CurLink = temp.CurLink
                            };
                            bwQueue.Enqueue(node);
                            bwBFS.Add(node);
                        }        
                    }
                    bwQueue.Dequeue();
                }
                foreach (var fw in fwBFS)
                {
                    foreach (var bw in bwBFS)
                    {
                        if (bw.CurLink.Equals(fw.CurLink))
                        {
                            fwQueue = new Queue<Node>();
                            bwQueue = new Queue<Node>();
                            fwQueue.Enqueue(fw);
                            bwQueue.Enqueue(bw);
                        }
                    }
                }
            }
        }
        //BDS
        private void bidirectionalSearch_Click(object sender, EventArgs e)
        {
            if (!sourceTextBox.Text.Trim().Contains(".html") || !goalLinkTextBox.Text.Trim().Contains(".html"))
            {
                MessageBox.Show("Vui lòng nhập với định dạng *.html");
            }
            else
            {
                //Khoi tao cac bien nho
                showList = new List<string>();
                QueueForward = new Queue<Node>();
                QueueBackward = new Queue<Node>();
                PathForward = new List<Node>();
                PathBackward = new List<Node>();
                Path = new List<Node>();
                linkTextBox.Clear();
                webBrowser.Navigate(System.IO.Path.Combine(Environment.CurrentDirectory, SourceDir) + sourceTextBox.Text.Trim());
                //kiem tra neu ket qua o ngay file dau tien
                if (sourceTextBox.Text.Trim().Equals(goalLinkTextBox.Text.Trim()))
                {
                    linkTextBox.Text = sourceTextBox.Text.Trim();
                }
                else
                {
                    try
                    {
                        BDS(QueueForward, QueueBackward, PathForward, PathBackward, LinkMap); //Ham BDS
                        List<string> Path1 = new List<string>();
                        List<string> Path2 = new List<string>();
                        List<string> Path3 = new List<string>();
                        if (IntersectionNodeForward.CurLink != null && IntersectionNodeBackward.CurLink != null)
                        {
                            if (IntersectionNodeForward.CurLink == IntersectionNodeBackward.PreLink || IntersectionNodeForward.CurLink == IntersectionNodeBackward.CurLink)
                            {
                                Path1 = GetForwardResult(PathForward, Path1, sourceTextBox.Text.Trim(), IntersectionNodeForward.CurLink.Replace(SourceDir, ""));
                                Path2 = GetBackwardResult(PathBackward, Path2, goalLinkTextBox.Text.Trim(), IntersectionNodeBackward.CurLink.Replace(SourceDir, ""));
                                if(Path1 != null || Path2 != null)
                                {
                                    string t = IntersectionNodeForward.PreLink.Replace(SourceDir, "") + "->" + IntersectionNodeForward.CurLink.Replace(SourceDir, "") + Environment.NewLine;
                                    Path2.Remove(t);
                                    Path2.Reverse();
                                    Path3 = Path1.Concat(Path2).ToList();
                                    foreach (var path in Path3)
                                    {
                                        linkTextBox.Text += path;
                                        showList.Add(path);
                                    }
                                    linkTextBox.Text += "Gặp nhau tại: " + IntersectionNodeForward.CurLink.Replace(SourceDir, "");
                                }
                                else
                                {
                                    linkTextBox.Text = IntersectionNodeForward.PreLink.Replace(SourceDir, "") + "->" + IntersectionNodeBackward.PreLink.Replace(SourceDir, "");
                                }
                            }
                        }
                        else if (linkTextBox.Text == "")
                        {
                            linkTextBox.Text += "Không có đường đi";
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Bạn đã nhập không đúng tên file", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                    webBrowser.Navigate(System.IO.Path.Combine(Environment.CurrentDirectory, SourceDir) + hehe[i]);
                }
            }
            else
            {
                MessageBox.Show("Nothing in Path");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
