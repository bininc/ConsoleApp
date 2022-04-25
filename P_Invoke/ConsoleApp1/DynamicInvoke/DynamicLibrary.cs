using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace AnLock_General_All
{
    public sealed class DynamicLibrary : IDisposable
    {
        private sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private extern static IntPtr LoadLibrary(string lpLibFileName);

            [DllImport("kernel32.dll", SetLastError = true)]
            private extern static bool FreeLibrary(IntPtr hLibModule);

            public SafeLibraryHandle(string library)
                : base(true)
            {
                this.SetHandle(LoadLibrary(library));
            }

            protected override bool ReleaseHandle()
            {
                if (FreeLibrary(this.handle))
                {
                    this.SetHandleAsInvalid();
                    return true;
                }
                return false;
            }
        }

        private IList<NativeMethod> methods = new List<NativeMethod>();
        private SafeHandle _Handle;

        public DynamicLibrary(string library)
        {
            _Handle = new SafeLibraryHandle(library);
        }

        internal SafeHandle Handle
        {
            get { return _Handle; }
        }

        public void Free()
        {
            if (!IsFree)
            {
                _Handle.Close();
            }
        }

        public bool IsFree
        {
            get { return _Handle.IsClosed; }
        }

        void IDisposable.Dispose()
        {
            Free();
        }
    }
}

