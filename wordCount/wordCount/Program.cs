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
        public static ArrayList returnTxt = new ArrayList();
        public static void Main(string[] args)
        {
            string filePath = Environment.CurrentDirectory + "\\file.txt";//记录执行文件的路径，默认为DEBUG目录        
            Console.Write("wordCount.exe ");

            string message = Console.ReadLine();//读取写入的各种操作符（-c -w -l -o）,顺序可颠倒
            char[] c = { ' ' };//操作符之间由空格分开
            string[] s = message.Split(c, StringSplitOptions.RemoveEmptyEntries);//将分开的命令写入数组

            for (int i = 0; i < s.Length; i++)
            {
                //对相应的命令执行相应的操作，结果写入returnNumber
                try
                {
                    if (s[i] == "-r" && i == 0) //指定读取的文件 注意：使用该指令时，该指令必须在一开始申明
                    {
                        i++;
                        try
                        {
                            FileInfo fi = new FileInfo(s[i]);
                            if (File.Exists(s[i]) == false || fi.Length == 0)//找不到文件或者文件为空
                            {
                                Console.WriteLine("can't find file or file is empty!");
                                break;
                            }
                            Console.WriteLine("found file success!");
                            filePath = s[i];
                        }
                        catch (IndexOutOfRangeException e)//用户使用了-r指令，却未输入地址和操作指令
                        {
                            Console.WriteLine("file path is invalid !");
                            Console.WriteLine("Exception caught:{0}", e);
                        }

                    }
                    else if (s[i] == "-c")//统计总字符数
                    {
                        returnTxt.Add(Function.getChacactor(filePath));
                    }
                    else if (s[i] == "-l")//统计总行数
                    {
                        returnTxt.Add(Function.getRows(filePath));
                    }
                    else if (s[i] == "-n")//统计单词数
                    {
                        returnTxt.Add(Function.totalWord(filePath));
                    }
                    else if (s[i] == "-w")//统计出现频率最高的10个单词及出现次数
                    {
                        returnTxt.AddRange(Function.countWord(filePath));
                    }
                    else if (s[i] == "-o")//输出结果
                    {
                        i++;
                        if (i == s.Length)
                        {
                            save(null);//保存默认地址
                            break;
                        }
                        string outPath = s[i];
                        save(s[i]);
                    }
                        else//输入错误，弹出程序
                    {
                        Console.WriteLine("error:"+"'" + s[i] + "'" + "is an unknown command");
                        break;
                    }
                }
                catch (IndexOutOfRangeException e)//用户重复调用了太多次指令，导致超出了数组的界限
                {
                    Console.WriteLine("too much conmand !");
                    Console.WriteLine("Exception caught:{0}", e);
                    break;
                }
               
            }
            Console.WriteLine("程序结束,任意键结束！\n");
            Console.ReadLine();
            
        }

        public static void save(string filePath)
        {
            //写入文件的方法     
            if(filePath == null)
                filePath = Environment.CurrentDirectory + "\\result.txt";   //被执行的文件，默认路径
            else
                filePath = filePath + "\\result.txt";                       //被执行的文件，指定路径

            FileStream fs = new FileStream(filePath, FileMode.Create);//定义文件操作类型,实例化
            StreamWriter sw = new StreamWriter(fs);//用特定方式写入信息,实例化
            foreach(string info in returnTxt)
            {
                sw.Write(info);
                sw.Write("\r\n");//换行
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            Console.Write("文件写入成功\n");
        }
    }
}
