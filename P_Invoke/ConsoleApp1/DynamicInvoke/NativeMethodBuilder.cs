using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Reflection;

namespace AnLock_General_All
{

    public sealed class NativeMethodBuilder
    {
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(SafeHandle hModule, string lpProcName);

        private readonly List<NativeParameterInfo> _nativeParameterInfos = new List<NativeParameterInfo>();
        private Type _ReturnType;
        private CallingConvention _CallingConvention;

        private Type[] m_Types
        {
            get { return _nativeParameterInfos.Select(p => p.Type).ToArray(); }
        }

        public NativeMethodBuilder()
        {
            this.ParameterLength = 0;
            this.CallingConvention = CallingConvention.StdCall;
            this.ReturnType = typeof(int);
        }

        public CallingConvention CallingConvention
        {
            get { return _CallingConvention; }
            set { _CallingConvention = value; }
        }

        /// <summary>
        /// ∑µªÿ¿‡–Õ
        /// </summary>
        public Type ReturnType
        {
            get { return _ReturnType; }
            set { _ReturnType = value; }
        }

        public int ParameterLength
        {
            get { return _nativeParameterInfos.Count; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                int length = value;
                _nativeParameterInfos.Clear();
                if (length > 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        _nativeParameterInfos.Add(new NativeParameterInfo());
                        SetParameterInfo(i, typeof(int));
                    }
                }
            }
        }

        public void SetParameterInfo(int index, NativeParameterInfo parameter)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (index >= ParameterLength)
                throw new ArgumentOutOfRangeException("index");
            _nativeParameterInfos[index] = parameter;
        }

        public void SetParameterInfo(int index, String name, Type type, ParameterAttributes mode)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (index >= ParameterLength)
                throw new ArgumentOutOfRangeException("index");
            NativeParameterInfo info = _nativeParameterInfos[index];
            info.Name = name;
            info.Type = type;
            info.Mode = mode;
        }

        public void SetParameterInfo(int index, Type type)
        {
            this.SetParameterInfo(index, "", type, ParameterAttributes.In);
        }

        public void AddParameterInfo(Type type, string name = "", ParameterAttributes mode = ParameterAttributes.In)
        {
           this.AddParameterInfo(new NativeParameterInfo() { Type = type, Name = name, Mode = mode });
        }

        public void AddParameterInfo(NativeParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            _nativeParameterInfos.Add(parameter);
        }

        public void AddParameterInfos(IEnumerable<NativeParameterInfo> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            _nativeParameterInfos.AddRange(parameters);
        }

        public NativeMethod MakeMethod(DynamicLibrary dll, string rawName)
        {
            return MakeMethod(dll, rawName, "");
        }

        public NativeMethod MakeMethod(DynamicLibrary dll, string rawName, string name)
        {
            IntPtr address = GetProcAddress(dll.Handle, rawName);
            if (address == IntPtr.Zero)
                throw new ArgumentException(string.Format("'{0}'is invalid rawName!", rawName), "rawName");
            return MakeMethod(name, address);
        }

        public NativeMethod MakeMethod(IntPtr pointer)
        {
            return MakeMethod("", pointer);
        }

        public NativeMethod MakeMethod(string name, IntPtr pointer)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            DynamicMethod dm = new DynamicMethod(name,
                MethodAttributes.Public | MethodAttributes.Static,
                CallingConventions.Standard,
                ReturnType, m_Types, typeof(DBNull), true);

            for (int i = 0; i < _nativeParameterInfos.Count; i++)
            {
                NativeParameterInfo info = _nativeParameterInfos[i];
                dm.DefineParameter(i + 1, info.Mode , info.Name);
            }
            ILGenerator il = dm.GetILGenerator();
            for (int i = 0; i < ParameterLength; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }
            if (IntPtr.Size == 4)
            {
                il.Emit(OpCodes.Ldc_I4, pointer.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                il.Emit(OpCodes.Ldc_I8, pointer.ToInt64());
            }
            il.EmitCalli(OpCodes.Calli, CallingConvention, ReturnType, m_Types);
            il.Emit(OpCodes.Ret);
            return new NativeMethod(dm);
        }

    }
}
