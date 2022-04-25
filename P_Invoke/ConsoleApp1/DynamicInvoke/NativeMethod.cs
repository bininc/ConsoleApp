using System;
using System.Reflection;
using System.Reflection.Emit;

namespace AnLock_General_All
{
    public sealed class NativeMethod
    {
        private DynamicMethod m_Method;

        internal NativeMethod(DynamicMethod method)
        {
            m_Method = method;
		}

        public object Invoke()
        {
            return Invoke(new object[0]);
        }

        public object Invoke(params object[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            ParameterInfo[] pis = m_Method.GetParameters();
            int argLength = m_Method.GetParameters().Length;
            if (args.Length != argLength)
                throw new ArgumentException("参数长度不正确。");
            for (int i = 0; i < argLength; i++)
            {
                if (!pis[i].ParameterType.IsInstanceOfType(args[i]))
                {
                    throw new ArgumentException(string.Format("第 {0} 个参数 {1} 的类型与方法签名不兼容。", i, pis[i].Name));
                }
            }
            return m_Method.Invoke(null, args);
        }

        public MethodInfo Method
        {
            get { return m_Method; }
        }
    }
}
