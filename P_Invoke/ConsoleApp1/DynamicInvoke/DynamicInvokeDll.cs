using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AnLock_General_All
{
    public class DynamicInvokeDll
    {
        public readonly string DllPath;
        public readonly DynamicLibrary DynamicLibrary;

        public DynamicInvokeDll(string dllPath)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
            {
                throw new ArgumentNullException(nameof(dllPath));
            }

            DllPath = dllPath;
            DynamicLibrary = new DynamicLibrary(dllPath);
        }

        public T InvokeMethod<T>(string methodName, CallingConvention callingConvention = CallingConvention.StdCall,
            params NativeParameterInfo[] parameters)
        {
            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            NativeMethodBuilder builder = new NativeMethodBuilder();
            builder.CallingConvention = callingConvention;
            builder.ReturnType = typeof(T);
            builder.AddParameterInfos(parameters);

            NativeMethod method = builder.MakeMethod(DynamicLibrary, methodName);
            if (parameters.Length == 0)
            {
                return (T)method.Invoke();

            }
            else
            {
                object[] args = parameters.Select(p => p.Value).ToArray();
                
                return (T)method.Invoke(args);
            }
        }

        public T InvokeMethod<T>(string methodName, params object[] args)
        {
            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (args.Length == 0)
            {
                return InvokeMethod<T>(methodName, CallingConvention.StdCall);
            }
            else
            {
                NativeParameterInfo[] infos = args.Select(p => new NativeParameterInfo() { Type = p.GetType(), Value = p }).ToArray();
                return InvokeMethod<T>(methodName, CallingConvention.StdCall, infos);
            }
        }

        public void Close()
        {
            DynamicLibrary.Free();
        }
    }
}
