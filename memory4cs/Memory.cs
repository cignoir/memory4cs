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
        public int BaseAddress
        {
            get { return process != null ? process.MainModule.BaseAddress.ToInt32() : 0; }
        }
        
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
        
    }
        
}
