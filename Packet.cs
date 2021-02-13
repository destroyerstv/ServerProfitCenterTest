using System;
using System.Collections.Generic;
using System.Text;

namespace ServerProfitCenterTest
{
    struct Packet
    {
        public UInt64 NumPacket;
        public double Quotation;
        public double StartRange;
        public double EndRange;
        public byte[] GetBytes()
        {
            List<byte> listByte = new List<byte>();
            listByte.AddRange(BitConverter.GetBytes(NumPacket));
            listByte.AddRange(BitConverter.GetBytes(Quotation));
            listByte.AddRange(BitConverter.GetBytes(StartRange));
            listByte.AddRange(BitConverter.GetBytes(EndRange));
            return listByte.ToArray();
        }

        public void GetPacket(byte[] Arr)
        {
            int offset = 0;
            NumPacket = BitConverter.ToUInt64(Arr, offset);
            offset = offset + sizeof(UInt64);
            Quotation = BitConverter.ToDouble(Arr, offset);
            offset = offset + sizeof(double);
            StartRange = BitConverter.ToDouble(Arr, offset);
            offset = offset + sizeof(double);
            EndRange = BitConverter.ToDouble(Arr, offset);
        }
    }
}
