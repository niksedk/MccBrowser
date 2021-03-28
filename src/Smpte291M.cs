using System.Windows.Forms;

namespace MccBrowser
{
    public class Smpte291M
    {
        public int DataId { get; set; }
        public int SecondaryDataId { get; set; }
        public int DataCount { get; set; }
        public int CaptionDistributionPacketId { get; set; }
        public int CaptionDistributionPacketDataCount { get; set; }
        public int CaptionDistributionPacketFramingRate { get; set; }
        public bool CaptionDistributionPacketTimeCodeAdded { get; set; }
        public bool CaptionDistributionPacketDataBlockAdded { get; set; }
        public bool CaptionDistributionPacketServiceInfoAdded { get; set; }
        public bool CaptionDistributionPacketServiceInfoStart { get; set; }
        public bool CaptionDistributionPacketServiceInfoChanged { get; set; }
        public bool CaptionDistributionPacketServiceInfoEnd { get; set; }
        public bool CaptionDistributionPacketContainsCaptions { get; set; }
        public int CaptionDistributionPacketHeaderSequenceCounter { get; set; }
        public CaptionDistributionPacketCcData CaptionDistributionPacketCcData { get; set; }
        public CcServiceInfoSection CcServiceInfoSection { get; set; }
        public int CaptionDistributionPacketFooterSection { get; set; }
        public int CaptionDistributionPacketHeaderSequenceCounter2 { get; set; }
        public int CaptionDistributionPacketChecksum { get; set; }

        private readonly int _checkSumIndex;

        public string GetFrameRateDisplay()
        {
            switch (CaptionDistributionPacketFramingRate)
            {
                case 1: return "24000 / 1001 (23.976)";
                case 2: return "24";
                case 3: return "25";
                case 4: return "30000 / 1001 (29.97)";
                case 5: return "30";
                case 6: return "50";
                case 7: return "60000 / 1001 (59.94)";
                case 8: return "60";
                default:
                    return "unknown";
            }
        }

        public Smpte291M(byte[] bytes)
        {
            DataId = bytes[0];
            SecondaryDataId = bytes[1];
            DataCount = bytes[2];
            CaptionDistributionPacketId = (bytes[3] << 8) + bytes[4];
            CaptionDistributionPacketDataCount = bytes[5];
            CaptionDistributionPacketFramingRate = bytes[6] >> 4;
            CaptionDistributionPacketTimeCodeAdded = (bytes[7] & 0b10000000) > 0;
            CaptionDistributionPacketDataBlockAdded = (bytes[7] & 0b01000000) > 0;
            CaptionDistributionPacketServiceInfoAdded = (bytes[7] & 0b00100000) > 0;
            CaptionDistributionPacketServiceInfoStart = (bytes[7] & 0b00010000) > 0;
            CaptionDistributionPacketServiceInfoChanged = (bytes[7] & 0b00001000) > 0;
            CaptionDistributionPacketServiceInfoEnd = (bytes[7] & 0b00000100) > 0;
            CaptionDistributionPacketContainsCaptions = (bytes[7] & 0b00000010) > 0;
            CaptionDistributionPacketHeaderSequenceCounter = (bytes[8] << 8) + bytes[9];

            CaptionDistributionPacketCcData = new CaptionDistributionPacketCcData(bytes, 10);

            var idx = 9 + CaptionDistributionPacketCcData.GetLength();

            if (CaptionDistributionPacketServiceInfoAdded)
            {
                CcServiceInfoSection = new CcServiceInfoSection(bytes, idx + 1);
                idx += CcServiceInfoSection.GetLength();
            }

            CaptionDistributionPacketFooterSection = bytes[1 + idx];
            CaptionDistributionPacketHeaderSequenceCounter2 = (bytes[2 + idx] << 8) + bytes[3 + idx];
            CaptionDistributionPacketChecksum = bytes[4 + idx];
            _checkSumIndex = 4 + idx;
        }

        public TreeNode GetNodes(byte[] bytes)
        {
            var root = new TreeNode($"Smpte291M (length={bytes.Length} bytes)");
            root.Nodes.Add($"DataId={DataId} (0x{DataId:X2})");
            root.Nodes.Add($"SecondaryDataId={SecondaryDataId} (0x{SecondaryDataId:X2})");
            root.Nodes.Add($"DataCount={DataCount} (0x{DataCount:X2})");
            root.Nodes.Add($"CaptionDistributionPacketId={CaptionDistributionPacketId} (0x{CaptionDistributionPacketId:X4})");
            root.Nodes.Add($"CaptionDistributionPacketDataCount={CaptionDistributionPacketDataCount} (0x{CaptionDistributionPacketDataCount:X2})");
            root.Nodes.Add($"CaptionDistributionPacketFramingRate={CaptionDistributionPacketFramingRate} (0x{CaptionDistributionPacketFramingRate:X2}) = {GetFrameRateDisplay()}");
            root.Nodes.Add($"CaptionDistributionPacketTimeCodeAdded={CaptionDistributionPacketTimeCodeAdded}");
            root.Nodes.Add($"CaptionDistributionPacketDataBlockAdded={CaptionDistributionPacketDataBlockAdded}");
            root.Nodes.Add($"CaptionDistributionPacketServiceInfoAdded={CaptionDistributionPacketServiceInfoAdded}");
            root.Nodes.Add($"CaptionDistributionPacketServiceInfoStart={CaptionDistributionPacketServiceInfoStart}");
            root.Nodes.Add($"CaptionDistributionPacketServiceInfoChanged={CaptionDistributionPacketServiceInfoChanged}");
            root.Nodes.Add($"CaptionDistributionPacketServiceInfoEnd={CaptionDistributionPacketServiceInfoEnd}");
            root.Nodes.Add($"CaptionDistributionPacketContainsCaptions={CaptionDistributionPacketContainsCaptions}");
            root.Nodes.Add($"CaptionDistributionPacketHeaderSequenceCounter={CaptionDistributionPacketHeaderSequenceCounter} (0x{CaptionDistributionPacketHeaderSequenceCounter:X4})");
            root.Nodes.Add(CaptionDistributionPacketCcData.GetNodes());

            if (CaptionDistributionPacketServiceInfoAdded)
            {
                root.Nodes.Add(CcServiceInfoSection.GetNodes());
            }


            // This 8-bit field shall contain the 8-bit value necessary to make the arithmetic sum of the entire
            // packet(first byte of cdp_identifier to packet_checksum, inclusive) modulo 256 equal zero.
            long total = 0;
            for (int i = 3; i < _checkSumIndex; i++)
            {
                total += bytes[i];
            }
            var check2 = (byte)(total % 256);
            check2 = (byte)(256 - check2);

            root.Nodes.Add($"CaptionDistributionPacketFooterSection={CaptionDistributionPacketFooterSection} (0x{CaptionDistributionPacketFooterSection:X2} - should be 0x74)");
            root.Nodes.Add($"CaptionDistributionPacketHeaderSequenceCounter2={CaptionDistributionPacketHeaderSequenceCounter2} (0x{CaptionDistributionPacketHeaderSequenceCounter2:X4})");
            root.Nodes.Add($"CaptionDistributionPacketChecksum={CaptionDistributionPacketChecksum} (0x{CaptionDistributionPacketChecksum:X2} - should be 0x{check2:X2})");

            return root;
        }

        public string GetText()
        {
            return CaptionDistributionPacketCcData.GetText();
        }
    }
}
