using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory4cs
{
    partial class Memory
    {
        public byte[] Read(int address, int size)
        {
            var buffer = new byte[size];
            Win32API.ReadProcessMemory(process.Handle.ToInt32(), address, buffer, size, 0);
            return buffer;
        }

        public string ReadString(int address, int size)
        {
            return Encoding.Unicode.GetString(Read(address, size));
        }

        public string ReadHexString(int address, int size, ByteOrder byteOrder = ByteOrder.LITTLE_ENDIAN)
        {
            var bytes = Read(address, size);
            if(byteOrder == ByteOrder.LITTLE_ENDIAN) Array.Reverse(bytes);
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public int ReadInt(int address, int size, ByteOrder byteOrder = ByteOrder.LITTLE_ENDIAN)
        {
            return int.Parse(ReadHexString(address, size, byteOrder), NumberStyles.HexNumber);
        }
    }
}
