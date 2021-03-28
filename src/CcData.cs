namespace MccBrowser
{
    public class CcData
    {
        public bool Valid { get; set; }
        public int Type { get; set; }
        public byte Data1 { get; set; }
        public byte Data2 { get; set; }

        public string GetCcType()
        {
            return Type switch
            {
                0b00000000 => "NTSC line 21 field 1 Closed Captions",
                0b00000001 => "NTSC line 21 field 2 Closed Captions",
                0b00000010 => "DTVCC Channel Packet Data",
                0b00000011 => "DTVCC Channel Packet Start",
                _ => "unknown"
            };
        }
    }
}
