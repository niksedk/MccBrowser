﻿using System.Collections.Generic;
using System.Text;

namespace MccBrowser
{
    public static class Cea708
    {
        private static readonly Dictionary<byte, string> SingleCharLookupTable = new Dictionary<byte, string>
        {
            // G0 character table
            { 000, "" },
            { 001, "" },
            { 002, "" },
            { 003, "" },
            { 004, "" },
            { 005, "" },
            { 006, "" },
            { 007, "" },
            { 008, "" },
            { 009, "" },
            { 010, "" },
            { 011, "" },
            { 012, "" },
            { 013, "\r {NewLine}" },
            { 014, "" },
            { 015, "" },
            { 016, "" },
            { 017, "" },
            { 018, "" },
            { 019, "" },
            { 020, "" },
            { 021, "" },
            { 022, "" },
            { 023, "" },
            { 024, "" },
            { 025, "" },
            { 026, "" },
            { 027, "" },
            { 028, "" },
            { 029, "" },
            { 030, "" },
            { 031, "" },
            { 032, " " },
            { 033, "!" },
            { 034, "\"" },
            { 035, "#" },
            { 036, "$" },
            { 037, "%" },
            { 038, "&" },
            { 039, "'" },
            { 040, "(" },
            { 041, ")" },
            { 042, "*" },
            { 043, "+" },
            { 044, "," },
            { 045, "-" },
            { 046, "." },
            { 047, "/" },
            { 048, "0" },
            { 049, "1" },
            { 050, "2" },
            { 051, "3" },
            { 052, "4" },
            { 053, "5" },
            { 054, "6" },
            { 055, "7" },
            { 056, "8" },
            { 057, "9" },
            { 058, ":" },
            { 059, ";" },
            { 060, "<" },
            { 061, "=" },
            { 062, ">" },
            { 063, "?" },
            { 064, "@" },
            { 065, "A" },
            { 066, "B" },
            { 067, "C" },
            { 068, "D" },
            { 069, "E" },
            { 070, "F" },
            { 071, "G" },
            { 072, "H" },
            { 073, "I" },
            { 074, "J" },
            { 075, "K" },
            { 076, "L" },
            { 077, "M" },
            { 078, "N" },
            { 079, "O" },
            { 080, "P" },
            { 081, "Q" },
            { 082, "R" },
            { 083, "S" },
            { 084, "T" },
            { 085, "U" },
            { 086, "V" },
            { 087, "W" },
            { 088, "X" },
            { 089, "Y" },
            { 090, "Z" },
            { 091, "[" },
            { 092, "\\" },
            { 093, "]" },
            { 094, "^" },
            { 095, "_" },
            { 096, "`" },
            { 097, "a" },
            { 098, "b" },
            { 099, "c" },
            { 100, "d" },
            { 101, "e" },
            { 102, "f" },
            { 103, "g" },
            { 104, "h" },
            { 105, "i" },
            { 106, "j" },
            { 107, "k" },
            { 108, "l" },
            { 109, "m" },
            { 110, "n" },
            { 111, "o" },
            { 112, "p" },
            { 113, "q" },
            { 114, "r" },
            { 115, "s" },
            { 116, "t" },
            { 117, "u" },
            { 118, "v" },
            { 119, "w" },
            { 120, "x" },
            { 121, "y" },
            { 122, "z" },
            { 123, "{" },
            { 124, "|" },
            { 125, "}" },
            { 126, "~" },
            { 127, "♪" },
            { 128, "" },
            { 129, "" },
            { 130, "" },
            { 131, "" },
            { 132, "" },
            { 133, "" },
            { 134, "" },
            { 135, "" },
            { 136, "" },
            { 137, "" },
            { 138, "" },
            { 139, "" },
            { 140, "" },
            { 141, "" },
            { 142, "" },
            { 143, "" },
            { 144, "" },
            { 145, "" },
            { 146, "" },
            { 147, "" },
            { 148, "" },
            { 149, "" },
            { 150, "" },
            { 151, "" },
            { 152, "" },
            { 153, "" },
            { 154, "" },
            { 155, "" },
            { 156, "" },
            { 157, "" },
            { 158, "" },
            { 159, "" },

            // G1 character table
            { 160, " " }, // non breaking space
            { 161, "¡" },
            { 162, "￠" },
            { 163, "￡" },
            { 164, "¤" },
            { 165, "￥" },
            { 166, "¦" },
            { 167, "§" },
            { 168, "¨" },
            { 169, "©" },
            { 170, "ª" },
            { 171, "«" },
            { 172, "￢" },
            { 173, "-" },
            { 174, "®" },
            { 175, "￣" },
            { 176, "°"},
            { 177, "±" },
            { 178, "²" },
            { 179, "³" },
            { 180, "´" },
            { 181, "µ" },
            { 182, "¶" },
            { 183, "·" },
            { 184, "¸" },
            { 185, "¹" },
            { 186, "º" },
            { 187, "»" },
            { 188, "¼" },
            { 189, "½" },
            { 190, "¾" },
            { 191, "¿" },
            { 192, "À" },
            { 193, "Á" },
            { 194, "Â" },
            { 195, "Ã" },
            { 196, "Ä" },
            { 197, "Å" },
            { 198, "Æ" },
            { 199, "Ç" },
            { 200, "È" },
            { 201, "É" },
            { 202, "Ê" },
            { 203, "Ë" },
            { 204, "Ì" },
            { 205, "Í" },
            { 206, "Î" },
            { 207, "Ï" },
            { 208, "Ð" },
            { 209, "Ñ" },
            { 210, "Ò" },
            { 211, "Ó" },
            { 212, "Ô" },
            { 213, "Õ" },
            { 214, "Ö" },
            { 215, "×" },
            { 216, "Ø" },
            { 217, "Ù" },
            { 218, "Ú" },
            { 219, "Û" },
            { 220, "Ü" },
            { 221, "Ý" },
            { 222, "Þ" },
            { 223, "ß" },
            { 224, "à" },
            { 225, "á" },
            { 226, "â" },
            { 227, "ã" },
            { 228, "ä" },
            { 229, "å" },
            { 230, "æ" },
            { 231, "ç" },
            { 232, "è" },
            { 233, "é" },
            { 234, "ê" },
            { 235, "ë" },
            { 236, "ì" },
            { 237, "í" },
            { 238, "î" },
            { 239, "ï" },
            { 240, "ð" },
            { 241, "ñ" },
            { 242, "ò" },
            { 243, "ó" },
            { 244, "ô" },
            { 245, "õ" },
            { 246, "ö" },
            { 247, "÷" },
            { 248, "ø" },
            { 249, "ù" },
            { 250, "ú" },
            { 251, "û" },
            { 252, "ü" },
            { 253, "ý" },
            { 254, "þ" },
            { 255, "ÿ" },
        };

        public static string Decode(byte[] bytes)
        {
            var i = 0;
            var sb = new StringBuilder();

            while (i < bytes.Length)
            {
                var b = bytes[i];

                if (b == 0x03)
                {
                    sb.Append("{EndOfText}");
                    //The EndOfText command is a Null Command which can be used to flush any buffered text to the current window. All commands force a flush of any buffered text to the current window, so this command is only needed when no other command is pending.
                }
                else if (b >= 0x80 && b <= 0x87)
                {
                    sb.Append("{SetCurrentWindow}");
                    //SetCurrentWindow tells the caption decoder which window the following commands describe: SetWindowAttributes, SetPenAttributes, SetPenColor, SetPenLocation. If the window specified has not already been created with a DefineWindow command then
                    //SetCurrentWindow and the window property commands can be safely ignored. 
                }
                else if (b == 0x88)
                {
                    sb.Append("{ClearWindows}");
                    // ClearWindows clears all the windows specified in the 8 bit window bitmap.
                    i++;
                }
                else if (b == 0x89)
                {
                    sb.Append("{DisplayWindows}");
                    // DisplayWindows displays all the windows specified in the 8 bit window bitmap.
                    i++;
                }
                else if (b == 0x8A)
                {
                    sb.Append("{HideWindows}");
                    // HideWindows hides all the windows specified in the 8 bit window bitmap.
                    i++;
                }
                else if (b == 0x8B)
                {
                    sb.Append("{ToggleWindows}");
                    // ToggleWindows hides all displayed windows, and displays all hidden windows specified in the 8 bit window bitmap.
                    i++;
                }
                else if (b == 0x8C)
                {
                    sb.Append("{DeleteWindows}");
                    // DeleteWindows deletes all the windows specified in the 8 bit window bitmap.If the current window, as specified by the last SetCurrentWindow command, is deleted then the current window becomes undefined and the window attribute commands should have no effect until after the next SetCurrentWindow or DefineWindow command.
                    i++;
                }
                else if (b == 0x8D)
                {
                    sb.Append("{Delay}");
                    // Delay suspends all processing of the current service, except for DelayCancel and Reset scanning.The period of suspension is set to by the one byte parameter.The parameter specifies the delay in tenths of a second, so the minimum delay is 0.1 seconds, and the maximum delay is 25.5 seconds.A zero second delay can safely be ignored in a decoder, but should not be emitted from an encoder.A delay should be cancelled if the caption decoder's input buffer becomes full, a DelayCancel or Reset is received, or the specified delay time elapses.
                    i++;
                }
                else if (b == 0x8E)
                {
                    sb.Append("{DelayCancel}");
                    // DelayCancel terminates any active delay and resumes normal command processing. DelayCancel should be scanned for during a Delay.
                }
                else if (b == 0x8F)
                {
                    sb.Append("{Reset}");
                    // Reset deletes all windows, cancels any active delay, and clears the buffer before the Reset command. Reset should be scanned for during a Delay. 
                }
                else if (b == 0x90)
                {
                    sb.Append("{SetPenAttributes}");
                    // The SetPenAttributes command specifies how certain attributes of subsequent characters are to be rendered in the current window, until the next SetPenAttributes command.
                    i += 2;
                }
                else if (b == 0x91)
                {
                    sb.Append("{SetPenColor}");
                    // SetPenColor sets the foreground, background, and edge color for the subsequent characters. Color is specified with 6 bits, 2 for each of blue, green and red. The lowest order bits are for blue, the next two for green and the highest order bits represent red. Opacity is represented by two bits, they represent SOLID=0, FLASH=1, TRANSLUCENT=2, and TRANSPARENT=3. The edge color is the color of the outlined edges of the text, but the outline shares its opacity with the foreground, so the highest order bits of the third parameter byte should both be cleared.
                    i += 3;
                }
                else if (b == 0x92)
                {
                    sb.Append("{SetPenLocation}");
                    // SetPenLocation sets the location of for the next bit of appended text in the current window. It has two parameters, row and column. If a window is not locked (see Define Window) and the SMALL font is in effect the location can be outside the otherwise valid addresses. 
                    i += 2;
                }
                else if (b == 0x97)
                {
                    sb.Append("{SetWindowAttributes}");
                    // SetWindowAttributes Sets the window attributes of the current window. Fill Color is specified with 6 bits, 2 for each of blue, green and red. The lowest order bits are for blue, the next two for green and the highest order bits represent red. Fill Opacity is represented by two bits, they represent SOLID=0, FLASH=1, TRANSLUCENT=2, and TRANSPARENT=3. The window's Border Color is specified the same way. However, the Border Type is split into two fields. They should be combined, with border type 01 representing the low order bits, and border type 2 the high order bit. Once combined the Border Type has 6 valid values: NONE=0, RAISED=1, DEPRESSED=2, UNIFORM=3, SHADOW_LEFT=4, and SHADOW_RIGHT=5. 
                    i += 4;
                }
                else if (b >= 0x98 && b <= 0x9F)
                {
                    sb.Append("{DefineWindow}");
                    //DefineWindow0-7 creates one of the eight windows used by a caption decoder. This command should be sent periodically by a caption encoder even for pre-existing windows so that a newly tuned in caption decoder can begin displaying captions. When issued on a pre-existing window the pen style and window style can be left null, this tells the decoder not to change the current styles if they exist, and initialize both to style 1 if the window does not exist in its context
                    i += 6;
                }

                else if (b == 0x10 && i < bytes.Length - 1)
                {
                    // ext 1
                    var b2 = bytes[i] << 8 + bytes[i + 1];
                    i++;

                    if (b2 >= 0x1000 && b2 <= 0x101F)
                    {
                        // CL Group: C2: ISO 8859 - Extended Miscellaneous Control Codes
                    }
                    else if (b2 >= 0x1020 && b2 <= 0x107F)
                    {
                        // GL Group: G2: Extended Control Code Set 1
                    }
                    else if (b2 >= 0x1080 && b2 <= 0x109F)
                    {
                        // CR Group: C3: Extended Control Code Set 2
                    }
                    else if (b2 >= 0x10A0 && b2 <= 0x10FF)
                    {
                        // GR Group: G3:  Future characters and icons
                    }
                }
                else if (b <= 0x1F)
                {
                    // CL Group: C0: Subset of ASCII Control Codes
                    sb.Append(SingleCharLookupTable[b]);

                    //if (b >= 0x10 && b <= 0x17)
                    //{
                    //    i++;
                    //}
                    //else if (b >= 0x18 && b <= 0x1F)
                    //{
                    //    i+=2;
                    //}
                }
                else if (b >= 0x20 && b <= 0x7F)
                {
                    // Modified version of ANSI X3.4 Printable Character Set(ASCII)
                    sb.Append(SingleCharLookupTable[b]);
                }
                else if (b >= 0x80 && b <= 0x9f)
                {
                    // CR Group: C1: Caption Control Codes
                    sb.Append(SingleCharLookupTable[b]);
                }
                else if (b >= 0xA0 && b <= 0xFF)
                {
                    // ISO 8859 - 1 Latin 1 Characters
                    sb.Append(SingleCharLookupTable[b]);
                }

                i++;
            }

            return sb.ToString();
        }
    }
}
