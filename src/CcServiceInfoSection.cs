using System.Windows.Forms;

namespace MccBrowser
{

    public class CcServiceInfoSectionElement
    {
        public bool CsnSize { get; set; }
        public int CaptionServiceNumber { get; set; }
        public byte[] ServiceDataByte { get; set; }

        public CcServiceInfoSectionElement()
        {
            ServiceDataByte = new byte[6];
        }
    }

    public class CcServiceInfoSection
    {
        public byte Id { get; set; }

        public bool Start { get; set; }
        public bool Change { get; set; }
        public bool Complete { get; set; }
        public int ServiceCount { get; set; }
        public CcServiceInfoSectionElement[] CcServiceInfoSectionElement { get; set; }

        public int GetLength()
        {
            return 2 + ServiceCount * 7;
        }

        public CcServiceInfoSection(byte[] bytes, int index)
        {
            Id = bytes[index];

            Start = (bytes[index + 1] & 0b01000000) > 0;
            Change = (bytes[index + 1] & 0b00100000) > 0;
            Complete = (bytes[index + 1] & 0b00010000) > 0;
            ServiceCount = bytes[index + 1] & 0b00001111;

            CcServiceInfoSectionElement = new CcServiceInfoSectionElement[ServiceCount];
            for (int i = 0; i < ServiceCount; i++)
            {
                CcServiceInfoSectionElement[i] = new CcServiceInfoSectionElement
                {
                    CaptionServiceNumber = bytes[index + i * 7 + 1] & 0b00011111,
                    ServiceDataByte =
                    {
                        [0] = bytes[index + i * 7 + 3],
                        [1] = bytes[index + i * 7 + 4],
                        [2] = bytes[index + i * 7 + 5],
                        [3] = bytes[index + i * 7 + 6],
                        [4] = bytes[index + i * 7 + 7],
                        [5] = bytes[index + i * 7 + 8]
                    }
                };
            }
        }

        public TreeNode GetNodes()
        {
            var root = new TreeNode($"CC Service Info Section (0x{Id:X2})");
            root.Nodes.Add($"Start={Start}");
            root.Nodes.Add($"Change={Change}");
            root.Nodes.Add($"Complete={Complete}");
            root.Nodes.Add($"ServiceCount={ServiceCount}");
            for (int i = 0; i < ServiceCount; i++)
            {
                var elementNode = new TreeNode($"Element {i}");
                var element = CcServiceInfoSectionElement[i];
                elementNode.Nodes.Add($"CsnSize={element.CsnSize}");
                elementNode.Nodes.Add($"CaptionServiceNumber={element.CaptionServiceNumber} (0x{element.CaptionServiceNumber:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[0]={element.ServiceDataByte[0]} (0x{element.ServiceDataByte[0]:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[1]={element.ServiceDataByte[1]} (0x{element.ServiceDataByte[1]:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[2]={element.ServiceDataByte[2]} (0x{element.ServiceDataByte[2]:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[3]={element.ServiceDataByte[3]} (0x{element.ServiceDataByte[3]:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[4]={element.ServiceDataByte[4]} (0x{element.ServiceDataByte[4]:X2})");
                elementNode.Nodes.Add($"ServiceDataByte[5]={element.ServiceDataByte[5]} (0x{element.ServiceDataByte[5]:X2})");
                root.Nodes.Add(elementNode);
            }

            return root;
        }
    }
}
