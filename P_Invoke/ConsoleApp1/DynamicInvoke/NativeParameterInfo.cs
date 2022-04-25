using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AnLock_General_All
{
    /// <summary>
    /// 方法参数信息
    /// </summary>
    public class NativeParameterInfo
    {
        /// <summary>
        /// 参数名(可为空)
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 参数数据类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 参数属性(默认输入参数)
        /// </summary>
        public ParameterAttributes Mode { get; set; } = ParameterAttributes.In;
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        public NativeParameterInfo() { }

        public NativeParameterInfo(object value)
        {
            this.Value = value;
            if (value != null)
                this.Type = value.GetType();
        }

        public NativeParameterInfo(object value, ParameterAttributes mode)
        {
            this.Value = value;
            if (value != null)
                this.Type = value.GetType();
            this.Mode = mode;
        }

        public NativeParameterInfo(string name, object value, ParameterAttributes mode)
        {
            this.Name = name;
            this.Value = value;
            if (value != null)
                this.Type = value.GetType();
            this.Mode = mode;
        }
    }
}
