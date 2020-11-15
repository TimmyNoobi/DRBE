using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRBE
{
    public class DRBE_swpacket
    {
        private DRBE_Objs dobj;

        DRBE_swpacket(DRBE_Objs x)
        {
            dobj = x;
        }

        public List<byte> pbuild(int mode)
        {
            List<byte> result = new List<byte>();

            //Message ID
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);

            //Timestamp 1
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);

            //Timestamp 2
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);
            result.Add(0x00);

            //Source ID
            result.Add(0x00);
            result.Add(0x00);

            //Destination ID
            result.Add(0x00);
            result.Add(0x00);

            //Message Type
            result.Add(0x00);
            result.Add(0x00);

            //Reserve
            result.Add(0x00);
            result.Add(0x00);

            //




            return result;
        }
    }
}
