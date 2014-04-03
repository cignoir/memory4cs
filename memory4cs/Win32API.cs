using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace memory4cs
{
    public class Win32API
    {

        public static int PROCESS_VM_READ = 0x0010;
        public static int PROCESS_VM_WRITE = 0x0020;
        public static int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);
    }
}
