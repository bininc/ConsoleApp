using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp1
{
    public class Load2006Api
    {
        private const string Path = @"\PYHDKey2006.dll";

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="hAppWindow">窗口句柄</param>
        /// <param name="comPort">串口号</param>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int OpenCom(IntPtr hAppWindow, int comPort);

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int CloseCom();

        /// <summary>
        /// 返回消息字符串
        /// 返回卡片消息 PWM_CARDVAILD、PWM_GETREADERDATA 指向地址的字符串
        /// </summary>
        /// <param name="iAddr">字符串指针地址</param>
        /// <param name="sStr">字符串</param>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int GetMessageStr(int iAddr, StringBuilder sStr);

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="iErrCode">错误编号</param>
        /// <param name="sErrStr">20字符长度，错误信息</param>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int GetErrInfo(int iErrCode, [MarshalAs(UnmanagedType.LPStr)] ref string sErrStr);

        /// <summary>
        /// 制空卡
        /// </summary>
        /// <param name="sRom"></param>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int WriteSpaceKey([MarshalAs(UnmanagedType.LPStr)] ref string sRom);

        /// <summary>
        /// 制宾客卡
        /// </summary>
        /// <param name="sRom">12个字符长度返回的卡号</param>
        /// <param name="FloorCode">楼栋(1-255)</param>
        /// <param name="FloorLayCode">楼层(1-255)</param>
        /// <param name="RoomCode">房号(1-65535)</param>
        /// <param name="iSubRoomCode">套间房号(1-255)</param>
        /// <param name="EndDateTime">结束时间 20字符长度  格式'yyyy-mm-dd hh:nn:ss'</param>
        /// <returns></returns>
        [DllImport(Path)]
        public static extern int WriteGuestKey(StringBuilder sRom, uint FloorCode, uint FloorLayCode, uint RoomCode, uint iSubRoomCode, string EndDateTime);
    }
}
