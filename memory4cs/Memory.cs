using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory4cs
{
    public partial class Memory
    {
        Process process;
        
        public Memory(string name)
        {
            var processes = Process.GetProcessesByName(name);
            this.process = processes.Length > 0 ? processes.First() : null;
        }

        public Memory(int pid)
        {
            this.process = Process.GetProcessById(pid);
        }

        public Memory(Process process)
        {
            this.process = process;
        }

        public int BaseAddress()
        {
            return process != null ? process.MainModule.BaseAddress.ToInt32() : 0;
        }

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

        public int ReadInt(int address, int size, int byteOrder = 0)
        {
            var bytes = Read(address, size);
            if(byteOrder == Memory.BYTE_ORDER_LE) Array.Reverse(bytes);
            return int.Parse(BitConverter.ToString(bytes).Replace("-", ""), NumberStyles.HexNumber);
        }

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
