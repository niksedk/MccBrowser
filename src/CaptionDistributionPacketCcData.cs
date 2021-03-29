using System;
using System.Text;
using System.Windows.Forms;

namespace MccBrowser
{
    public class CaptionDistributionPacketCcData
    {
        public int DataSection { get; set; }
        public bool ProcessEmData { get; set; }
        public bool ProcessCcData { get; set; }
        public bool AdditionalData { get; set; }
        public CcData[] CcData { get; set; }

        public int GetLength()
        {
            return 2 + CcData.Length * 3;
        }

        public CaptionDistributionPacketCcData(byte[] bytes, int index)
        {
            DataSection = bytes[index];
            ProcessEmData = (bytes[index + 1] & 0b10000000) > 0;
            ProcessCcData = (bytes[index + 1] & 0b01000000) > 0;
            AdditionalData = (bytes[index + 1] & 0b00100000) > 0;
            var ccCount = bytes[index + 1] & 0b00011111;
            CcData = new CcData[ccCount];
            for (var i = 0; i < ccCount; i++)
            {
                CcData[i] = new CcData
                {
                    Valid = (bytes[index + i * 3 + 2] & 0b00000100) > 0,
                    Type = bytes[index + i * 3 + 2] & 0b00000011,
                    Data1 = bytes[index + i * 3 + 3],
                    Data2 = bytes[index + i * 3 + 4]
                };
            }
        }

        public TreeNode GetNodes()
        {
            var root = new TreeNode("CC Data");
            root.Nodes.Add($"DataSection={DataSection} (0x{DataSection:X2})");
            root.Nodes.Add($"ProcessEmData={ProcessEmData}");
            root.Nodes.Add($"ProcessCcData={ProcessCcData}");
            root.Nodes.Add($"AdditionalData={AdditionalData}");
            var nodeCcCount = new TreeNode($"CcCount={CcData.Length} (0x{CcData.Length:X2}, length={CcData.Length * 3} bytes)");
            root.Nodes.Add(nodeCcCount);
            var hex = new StringBuilder();
            root.Tag = CcData;
            for (int i = 0; i < CcData.Length; i++)
            {
                var node = new TreeNode($"CC element {i}");
                var cc = CcData[i];
                node.Nodes.Add($"Valid={cc.Valid}");
                node.Nodes.Add($"Type={cc.Type} (0x{cc.Type:X2}) {cc.GetCcType()}");
                var data1Node = new TreeNode($"Data1={cc.Data1} (0x{cc.Data1:X2})");
                node.Nodes.Add(data1Node);
                if (cc.Type == 3)
                {
                    var sequenceNumber = cc.Data1 >> 6;
                    var packetSize = cc.Data1 & 0b00111111;
                    data1Node.Nodes.Add("Sequence number: " + sequenceNumber);
                    data1Node.Nodes.Add("Packet size: " + packetSize);
                }
                node.Nodes.Add($"Data2={cc.Data2} (0x{cc.Data2:X2})");
                root.Nodes.Add(node);
                if (cc.Valid && cc.Type == 2)
                {
                    hex.Append($"{cc.Data1:X2}{cc.Data2:X2}");
                }
            }

            var text = Cea708.Decode(HexStringToByteArray(hex.ToString()));
            root.Text += " " + text;
            return root;
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

        public string GetText()
        {
            var hex = new StringBuilder();
            for (int i = 0; i < CcData.Length; i++)
            {
                var cc = CcData[i];
                if (cc.Valid && cc.Type == 2)
                {
                    hex.Append($"{cc.Data1:X2}{cc.Data2:X2}");
                }
            }

            var text = Cea708.Decode(HexStringToByteArray(hex.ToString()));
            return text;
        }
    }
}
