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
        
        public Memory()
        {
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

        public byte[] Read(int address, int size, int baseAddress = 0)
        {
            var buffer = new byte[size];
            var absoluteAddress = baseAddress == 0 ? BaseAddress() + address : address;
            Win32API.ReadProcessMemory(process.Handle.ToInt32(), absoluteAddress, buffer, size, 0);
            return buffer;
        }

        public string ReadString(int address, int size, int baseAddress = 0)
        {
            return Encoding.Unicode.GetString(Read(address, size, baseAddress));
        }

        public int ReadInt(int address, int size, int baseAddress = 0)
        {
            var bytes = Read(address, size, baseAddress);
            return int.Parse(BitConverter.ToString(bytes).Replace("-", ""), NumberStyles.HexNumber);
        }

        public void Write(int address, byte[] bytes, int baseAddress = 0)
        {
            var absoluteAddress = baseAddress == 0 ? BaseAddress() + address : address;
            Win32API.WriteProcessMemory(process.Handle.ToInt32(), absoluteAddress, bytes, bytes.Length, 0);
        }

        public void WriteString(int address, string str, int baseAddress = 0)
        {
            Write(address, Encoding.Unicode.GetBytes(str + "\0"), baseAddress);
        }

        public void WriteInt(int address, int num, int baseAddress = 0)
        {
            WriteString(address, Convert.ToString(num, 16), baseAddress);
        }
    }
        
}
