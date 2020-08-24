using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRBE
{
    public class MatlabPacket
    {
        public List<byte> packet = new List<byte>();

        public List<byte> Create()
        {
            List<byte> result = new List<byte>();
            int i = 0;
            i = 0;
            while(i<255)
            {
                result.Add(0x02);
                i++;
            }
            
            return result;
        }
        public List<byte> Pass(List<byte> x)
        {
            List<byte> result = new List<byte>();
            int i = 0;
            i = 0;
            while (i < 255)
            {
                result.Add(0x50);
                i++;
            }
            result[0] = 0x02;
            result[1] = 0x00;
            result[2] = 0xF8;
            result[3] = 0x10;
            result[4] = 0x04;

            result[5] = 0x01;
            result[6] = 0xD6;

            i = 0;
            while(i<x.Count)
            {
                result[7 + i] = 1;
                i++;
            }

            result[254] = 0x02;

            return result;
        }
        public List<byte> HC_APO4(List<byte> x)
        {
            List<byte> result = new List<byte>();
            int i = 0;
            i = 0;
            while (i < 255)
            {
                result.Add(0x50);
                i++;
            }
            result[0] = 0x02;
            result[1] = 0x00;
            result[2] = 0xF8;
            result[3] = 0x10;
            result[4] = 0x04;

            result[5] = 0x00;
            result[6] = 0x14;

            result[7] = 0x00;
            result[8] = 0x3C;

            result[9] = 0x00;
            result[10] = 0x0A;

            result[11] = 0x00;
            result[12] = 0x64;


            result[13] = 0x00;
            result[14] = 0x0A;

            result[15] = 0x00;
            result[16] = 0x3C;

            result[17] = 0x00;
            result[18] = 0x0A;

            result[19] = 0x00;
            result[20] = 0x64;

            result[21] = 0x00;
            result[22] = 0x80;

            result[23] = 0x00;
            result[24] = 0x01;

            result[25] = 0x00;
            result[26] = 0x32;


            result[27] = 0x00;
            result[28] = 0x02;

            result[29] = 0x02;

            i = 0;
            while (i < x.Count)
            {
                result[7 + i] = 1;
                i++;
            }

            result[254] = 0x02;

            return result;
        }
    }
}
