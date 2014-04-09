using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory4cs
{
    partial class Memory
    {
        public void Write(int address, byte[] bytes)
        {
            Win32API.WriteProcessMemory(process.Handle.ToInt32(), address, bytes, bytes.Length, 0);
        }

        public void WriteString(int address, string str)
        {
            Write(address, Encoding.Unicode.GetBytes(str + "\0"));
        }

        public void WriteInt(int address, int num)
        {
            WriteString(address, Convert.ToString(num, 16));
        }
    }
}
