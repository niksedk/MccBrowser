using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MccBrowser
{
    public partial class Form1 : Form
    {
        private static readonly Regex RegexTimeCodes = new Regex(@"^\d\d:\d\d:\d\d:\d\d\t", RegexOptions.Compiled);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = string.Empty;
            treeView1.Nodes.Clear();

            if (textBoxInput.Text.Trim().Length < 20)
            {
                return;
            }

            var hex = GetHex(textBoxInput.Text.Trim());

            textBoxResult.Text = hex;

            var bytes = HexStringToByteArray(hex);
            var smpte291M = new Smpte291M(bytes);
            treeView1.Nodes.Add(smpte291M.GetNodes(bytes));
            treeView1.ExpandAll();
            textBoxResultText.Text = smpte291M.GetText();
        }

        private string GetHex(string input)
        {
            // ANC data bytes may be represented by one ASCII character according to the following schema:
            var dictionary = new Dictionary<char, string>()
            {
                { 'G', "FA0000" },
                { 'H', "FA0000FA0000" },
                { 'I', "FA0000FA0000FA0000" },
                { 'J', "FA0000FA0000FA0000FA0000" },
                { 'K', "FA0000FA0000FA0000FA0000FA0000" },
                { 'L', "FA0000FA0000FA0000FA0000FA0000FA0000" },
                { 'M', "FA0000FA0000FA0000FA0000FA0000FA0000FA0000" },
                { 'N', "FA0000FA0000FA0000FA0000FA0000FA0000FA0000FA0000" },
                { 'O', "FA0000FA0000FA0000FA0000FA0000FA0000FA0000FA0000FA0000" },
                { 'P', "FB8080" },
                { 'Q', "FC8080" },
                { 'R', "FD8080" },
                { 'S', "9669" },
                { 'T', "6101" },
                { 'U', "E1000000" },
                { 'Z', "00" },
            };

            var sb = new StringBuilder();
            foreach (var ch in input)
            {
                if (dictionary.TryGetValue(ch, out var hexValue))
                {
                    sb.Append(hexValue);
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        private static byte[] HexStringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            buttonDecode_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonDecode_Click(null, null);
        }

        private void buttonBrowseMcc_Click(object sender, EventArgs e)
        {
            using var fileDialog = new OpenFileDialog { Filter = "MCC files|*.mcc" };
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxInput.Text = string.Empty;
                textBoxResult.Text = string.Empty;
                textBoxResultText.Text = string.Empty;

                OpenMccFile(File.ReadAllLines(fileDialog.FileName));
                labelDecodedObjects.Text = $"Decoded objects from {fileDialog.FileName}";
            }
        }

        private void OpenMccFile(string[] lines)
        {
            treeView1.Nodes.Clear();
            int count = 0;

            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                var s = line.Trim();
                var match = RegexTimeCodes.Match(s);
                if (!match.Success)
                {
                    continue;
                }

                var node = new TreeNode($"{count:000} {s.Substring(0, match.Length).Trim()}");
                var text = s.Substring(match.Index + match.Length).Trim();
                var hex = GetHex(text);
                try
                {
                    var bytes = HexStringToByteArray(hex);
                    var smpte291M = new Smpte291M(bytes);
                    var nodes = smpte291M.GetNodes(bytes);
                    node = nodes;
                    node.Text = $"{count:000} {s.Substring(0, match.Length).Trim()}";
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        if (subNode.Text.StartsWith("CC Data"))
                        {
                            var ccData = (CcData[])subNode.Tag;
                            var sb = new StringBuilder();
                            if (ccData.Length > 3)
                            {
                                sb.Append($"Type={ccData[0].Type} {ccData[0].Data1:X2}{ccData[0].Data2:X2}, ");
                                sb.Append($"Type={ccData[1].Type} {ccData[1].Data1:X2}{ccData[1].Data2:X2}, ");
                                sb.Append($"Type={ccData[2].Type} {ccData[2].Data1:X2}{ccData[2].Data2:X2}");
                            }

                            node.Text = $"{node.Text} {sb} {subNode.Text}";
                            break;
                        }
                    }
                }
                catch
                {
                    node.Nodes.Add("Unable to parse data");
                }
                treeView1.Nodes.Add(node);
                count++;
            }
        }
    }
}
