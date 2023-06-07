using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Command;
using UnityEngine;

public class message 
{
   //把需要给服务器的数据进行重组
/// dataLenth(为 了优化粘包问题) +RequestCode (区分请求的操作类型》+Ac ionCode (操作的事件)+data
// BitConverter 根据值类型把数据转成字节数组
// Encoding.UTF8 根据字符转成字节数组
// </summary>
private byte[] data = new byte[1024];
///每次读取时候定义一个索引读取索引的内容
private int startIndex = 0;

public byte[] Data { get { return data; } }
public int StartIndex { get { return startIndex; } }
public int Reaninszie { get { return data.Length - startIndex; } }
public void ReadMessage(int newCont,Action<ActionCode,string>processDataCallback)
{
   startIndex += newCont;
   while (true)
   {
      //当前数据没有可以获取的数据要到下一次接受到数据里面读取了
      if (startIndex <= 4) return;
      int count = BitConverter.ToInt32(data, 0);
      //判断当前data里面数据够不够我们一次读取
      if (startIndex - 4 >= count)
      {
         //  string str = Encoding.UTF8.GetString(data, 4, count);
         //  Console.WriteLine("接收到客户端发送一次数据" + str);
         //解析完成之后把数据给下一次解析
         //Requestcode requestcode =(Requestcode) BitConverter.ToInt32(data, 4);
         ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);

         string str = Encoding.UTF8.GetString(data, 8, count - 4);
         processDataCallback(actionCode, str);
         Array.Copy(data, 4 + count, data, 0, startIndex - count - 4);
         startIndex -= (count + 4);
      }
      else
         break;//数据不完整终止条件
   }
}
   public static byte[] PackData(Requestcode requestcode, ActionCode actionCode, string data)
   {
      byte[] requestCodeBytes = BitConverter.GetBytes((int) requestcode);
      byte[] actionCodebyte = BitConverter.GetBytes((int) actionCode);
      byte[] databyBytes = Encoding.UTF8.GetBytes(data);
      int dataAmout = requestCodeBytes.Length + actionCodebyte.Length + databyBytes.Length;
      byte[] dataAmountBytes = BitConverter.GetBytes(dataAmout);
      return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
         .Concat(actionCodebyte).ToArray<byte>()
         .Concat(databyBytes).ToArray<byte>();

   }
}
