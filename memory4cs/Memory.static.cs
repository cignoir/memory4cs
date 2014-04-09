using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace memory4cs
{
    public partial class Memory
    {
        static uint DELETE = 0x00010000;
        static uint READ_CONTROL = 0x00020000;
        static uint WRITE_DAC = 0x00040000;
        static uint WRITE_OWNER = 0x00080000;
        static uint SYNCHRONIZE = 0x00100000;
        static uint END = 0xFFF; //if you have Windows XP or Windows Server 2003 you must change this to 0xFFFF
        static uint PROCESS_ALL_ACCESS = (DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER | SYNCHRONIZE | END);

        public static int ObjectSize(object TestObject)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            bf.Serialize(ms, TestObject);
            Array = ms.ToArray();
            return Array.Length;
        }
    }
}
