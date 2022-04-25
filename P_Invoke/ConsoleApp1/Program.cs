using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using AnLock_General_All;
using Microsoft.Win32;
using SeasideResearch.LibCurlNet;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            StringBuilder sRom= new StringBuilder(12);
            uint floorCode = 1, floorLayCode = 1, roomCode = 1102, subRoomCode = 255;
            string endTime = "2019-08-24 13:05:00";
            //int n= Load2006Api.WriteGuestKey(sRom, 1, 1, 1102, 255, "2019-08-24 13:05:00");
            DynamicInvokeDll PYHDKey2006_dll = new DynamicInvokeDll(@"PYHDKey2006.dll");
            var result = PYHDKey2006_dll.InvokeMethod<int>("WriteGuestKey", CallingConvention.Cdecl, new[]
            {
                new NativeParameterInfo(sRom),
                new NativeParameterInfo(floorCode),
                new NativeParameterInfo(floorLayCode),
                new NativeParameterInfo(roomCode),
                new NativeParameterInfo(subRoomCode),
                new NativeParameterInfo(endTime),
            });*/

            //TimeStruct sStruct = GetTimes("20190824160000", "20190824160000");

            /*
            int i = 1;
            Console.WriteLine($"0x{i:X8}");
            DateTime tmp;
            bool suc2 = DateTime.TryParseExact("20190801235916", "yyyyMMddHHmmss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out tmp);

            Console.WriteLine(i = 2);
            Console.WriteLine(2 == 1);
            bool bsu = false;
            Console.WriteLine(bsu = i == 2);
            Console.WriteLine(bsu = i == 3);*/

            //string a = "2";
            //string b = "11";
            //Console.WriteLine(b.CompareTo(a));


            //string source =
            //    @" \CardSn=DBBC846300000000\CardType=1\BuildNo=1\FloorNo=00.00.00.00.00.00.00.00.00.00.\RoomNo=00388\RoomMask=0000000000000000\StartTime1=2019/09/11 13:46:00\StartTime2=2000/00/00 00:00:00\StartTime3=2000/00/00 00:00:00\EndTime1=2019/09/25 14:15:00\EndTime2=2000/00/00 00:00:00\EndTime3=2000/00/00 00:00:00\AirConditionEn=0\MasterCard=1\LostCardNo=0000000000000000\WholeSys=0\WholeBuilding=0";
            //Console.WriteLine(GetField(source, "BuildNo").ToString("D2"));

            //RegistryKey rootKey= RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            //var key= rootKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
            //    RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);

            //key.Close();
            //rootKey.Close();

            //string str = "sdfdf+124";
            //int addH = int.Parse(str.Substring(str.LastIndexOf('+') + 1));

            //byte[] bytes = new byte[4];
            //int[] nunmbers = new int[4];

            //Console.WriteLine(Marshal.SizeOf(bytes.GetType().GetElementType()) * bytes.Length + "-" + Marshal.SizeOf(nunmbers.GetType().GetElementType())*nunmbers.Length);

            //string result = W_Card("8305", "201911201310", "1", "1400", "1", "1", "8888", "1",
            //     "1+2+3+E+5+6+7+8+9+10+11+12+13+14+15+16+17+18+19+20+21+22+23+24+25+26+27+28+29+30");
            //Console.WriteLine(result);

            
            string hotelID = "索乐公寓CN_SZX1120";
            byte[] bs = Encoding.ASCII.GetBytes(hotelID.ToUpper());//转换大写
            bs = bs.Reverse().Take(5).ToArray(); //反序，倒数5个字节
            string str = string.Empty;
            for (int i = bs.Length-1; i >=0; i--)
            {
                str += bs[i].ToString("D2").Replace('0', 'a');
            }
            Console.WriteLine(str);

            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            List<byte> bytes = new List<byte>();
            Easy.WriteFunction wf = new Easy.WriteFunction(
                (Byte[] buf, Int32 size, Int32 nmemb, Object extraData) =>
                {
                    bytes.AddRange(buf);
                    return size * nmemb;
                }
            );
                      
            using (Easy easy = new Easy())
            {
                easy.SetOpt(CURLoption.CURLOPT_URL, "https://gateway.oyohotels.cn/hcis/driver/upgrade/checkFirmwareUpgrade");
                easy.SetOpt(CURLoption.CURLOPT_POST, 1);
                easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
                easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, 1);
                easy.SetOpt(CURLoption.CURLOPT_NOSIGNAL, 1);
                Slist slist = new Slist();
                slist.Append("Content-Type:application/json;charset=UTF-8");
                easy.SetOpt(CURLoption.CURLOPT_HTTPHEADER, slist);
                string jsonData = "{\"hotelId\":\"329442\",\"firmwareVersion\":\"V4.2.0.5\",\"mac\":\"00-1F-D0-10-C0-63\",\"reserve\":\"\"}";
                easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS, jsonData);

                easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYPEER, false);
                easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYHOST, false);
                Easy.SSLContextFunction sf = new Easy.SSLContextFunction((SSLContext ctx, Object extraData) =>
                {
                    // To do anything useful with the SSLContext object, you'll need
                    // to call the OpenSSL native methods on your own. So for this
                    // demo, we just return what cURL is expecting.
                    return CURLcode.CURLE_OK;
                });
                easy.SetOpt(CURLoption.CURLOPT_SSL_CTX_FUNCTION, sf);
                easy.Perform();
            }

            Console.WriteLine(Encoding.UTF8.GetString(bytes.ToArray()));
            Console.ReadKey();
        }

        private const string Path = @"RFLOCKDLL.dll";

        [DllImport(Path, CallingConvention = CallingConvention.StdCall)]
        public static extern string W_Card(String room, String startTime, String days, String endTime, String beep, String isAlarm, String user, String isCover, String lift);

        public static TimeStruct GetTimes(string startTime, string endTime)
        {
            TimeSpan ts = new TimeSpan();

            var arriveTime = DateTime.ParseExact(startTime, Constant.TimeFormat, CultureInfo.InvariantCulture);
            var depatureTime = DateTime.ParseExact(endTime, Constant.TimeFormat, CultureInfo.InvariantCulture);

            ts = depatureTime - arriveTime;
            if (ts.TotalHours > 63)
            {
                return new TimeStruct
                {
                    TimeUnits = 1,
                    TimeLength = (int)ts.TotalDays
                };
            }
            return new TimeStruct
            {
                TimeUnits = 0,
                // 当取余的小时数大于0时则加1小时
                TimeLength = ts.TotalMinutes % 60 > 0 ? (int)ts.TotalHours + 1 : (int)ts.TotalHours
            };
        }

        public struct TimeStruct
        {
            public int TimeUnits;
            public int TimeLength;
        }

        public class Constant
        {
            public const int Braud = 9600;
            public const string TimeFormat = "yyyyMMddHHmmss";
        }

        public static string GetField(String StrSource, String StrField)
        {
            string result = string.Empty;
            int i;
            int j;
            if (!string.IsNullOrEmpty(StrSource) && !string.IsNullOrEmpty(StrField))
            {
                i = StrSource.IndexOf(StrField, StringComparison.InvariantCulture);
                if (i >= 0)
                {
                    j = StrSource.IndexOf('\\', i + 1);
                    if (j >= 0)
                    {
                        i = i + StrField.Length + 1;    //去除名称和等号
                        result = StrSource.Substring(i, j - i);
                    }
                }
            }

            return result;
        }
    }
}
