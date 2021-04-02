using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                if (index > 1000)
                {
                    break;
                }

                var line = lines[index];
                var s = line.Trim();
                var match = RegexTimeCodes.Match(s);
                if (!match.Success)
                {
                    continue;
                }

                TreeNode node = null;
                var text = s.Substring(match.Index + match.Length).Trim();
                var hex = GetHex(text);
                try
                {
                    var bytes = HexStringToByteArray(hex);
                    var smpte291M = new Smpte291M(bytes);
                    node = smpte291M.GetNodes(bytes);
                    node.Text = $"{count:000} {s.Substring(0, match.Length).Trim()} ({smpte291M.GetLength()} bytes)";
                    int validCcDataItems = 0;
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        if (subNode.Text.StartsWith("CC Data"))
                        {
                            var ccData = (CcData[])subNode.Tag;
                            foreach (var data in ccData)
                            {
                                if (data.Valid)
                                {
                                    validCcDataItems++;
                                }
                            }

                            node.Text += $" - CC valid items: {validCcDataItems} - ";
                            break;
                        }
                    }

                    node.Text += " " + smpte291M.GetText();

                    var rawNode = new TreeNode("Raw hex");
                    rawNode.Nodes.Add(string.Join(' ', bytes.Select(p => p.ToString("X2"))));
                    node.Nodes.Add(rawNode);
                }
                catch (Exception e)
                {
                    node = new TreeNode($"Unable to parse data: {e.Message}: {e.StackTrace}");
                }

                treeView1.Nodes.Add(node);
                count++;
            }
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }

            if (e.KeyData == (Keys.Control | Keys.C))
            {
                e.SuppressKeyPress = true;
                Clipboard.SetText(treeView1.SelectedNode.Text);
            }
        }
    }
}
