using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

/*
 * 代码规范:
 * （驼峰命名法）第一个单词首字母小写，后面其他单词首字母大写。
 * 规定：  1.尽可能使用英文命名
 *         2.尽可能不使用缩写
 *         3.使用TAB作为缩进，缩进大小为4
 *         4.类型名称和源文件名称一致
 *         5.调用类型成员内部其他成员，需加this,调用父类成员需加base
 * 参考博客：https://www.cnblogs.com/AaronBlogs/p/6815848.html
 */
namespace wordCount
{
    class Program
    {
        public static string[] Information = { "", "", "" ,""};//定义写入文件的3种信息
        public static void Main(string[] args)
        {
            string fileName = Environment.CurrentDirectory + "\\file.txt";//执行DEBUG目录下的文件        
            Console.Write("wordCount.exe ");

            string message = Console.ReadLine();//读取写入的各种操作符（-c -w -l -o）,顺序可颠倒
            char[] c = { ' ' };//操作符之间由空格分开
            string[] s = message.Split(c, StringSplitOptions.RemoveEmptyEntries);//将分开的命令写入数组

            int[] returnNumber = { -1, -1, -1};//最终返回的文档数据
            for (int i = 0; i < s.Length; i++)
            {
                //对相应的命令执行相应的操作，结果写入returnNumber
                if (s[i] == "-c")
                {
                    returnNumber[i] = Function.getChacactor(fileName);
                    Information[i] = "characters：：" + returnNumber[i] + "\n";
                }
                else if (s[i] == "-l")
                {
                    returnNumber[i] = Function.getRows(fileName);
                    Information[i] = "lines：" + returnNumber[i] + "\n";
                }
                else if (s[i] == "-n")
                {
                    returnNumber[i] = Function.totalWord(fileName);
                    Information[i] = "words:" + returnNumber[i] + "\n";
                }
                else if(s[i] == "-w")
                {
                    Hashtable returnTable = Function.countWord(fileName);
                             
                }
                else if (s[i] == "-o")
                {
                    save();
                }
            }
            Console.WriteLine("程序结束,任意键结束！\n");
            Console.ReadLine();
            
        }

        public static void save()
        {
            //写入文件的方法        
            string fileName = Environment.CurrentDirectory + "\\result.txt";//被执行的文件        
            FileStream fs = new FileStream(fileName, FileMode.Create);//定义文件操作类型,实例化
            StreamWriter sw = new StreamWriter(fs);//用特定方式写入信息,实例化
            for (int i = 0; i < 3; i++)
            {
                sw.Write(Information[i]);//写入第i种信息
                sw.Write("\r\n");//换行
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            Console.Write("文件写入成功\n");
        }
    }
}
