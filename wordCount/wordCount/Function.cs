using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;


namespace wordCount
{
    //基类，基础的功能函数
    public class Function 
    {
        public static int getChacactor(string filePath)
        {
            //统计字符数的方法 
            int i, count;
            count = 0;
            //打开文件
            StreamReader fs = new StreamReader(filePath);
            string str = null;
            while ((str = fs.ReadLine()) != null)
            {
                for (i = 0; i < str.Length; i++)
                {
                    if (str[i] >= 0 && str[i] <= 127)
                    {
                        // Console.Write(str[i]);
                        count++;
                    }
                }
            }
            fs.Close();
            Console.Write("字符统计成功!\n");
            Console.Write("characters：" + count + "\n");
            return count;
        }
        public static int getRows(string filePath)
        {
            //统计行数的方法         
            FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
            StreamReader sr = new StreamReader(fs, Encoding.Default);//用特定方式读取文件中信息
            string s = sr.ReadToEnd();//读出所有信息
            fs.Close();
            sr.Close();
            char[] c = { '\n' };//定义跳过的字符类型，换行符
            string[] words = s.Split(c, StringSplitOptions.RemoveEmptyEntries);//将读出的信息按跳过的字符类型，分割成字符串
            Console.Write("行数统计成功\n");
            Console.Write("lines：" + words.Length + "\n");
            return words.Length;//返回字符串的个数,即行数          
        }
        public static int totalWord(string filePath)
        {
            //统计单词的总数
            FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string s = sr.ReadToEnd();//读取所有信息
            Regex rg = new Regex("[A-Za-z-]+");//用正则表达式匹配单词
            MatchCollection mc;//通过正则表达式所找到的成功匹配的集合
            mc = rg.Matches(s);
            fs.Close();
            sr.Close();
            Console.Write("words:" + mc.Count + "\n");
            return mc.Count;
        }
        public static Hashtable countWord(string filePath)
        {
            //统计单个单词的出现次数
            FileStream fs = new FileStream(filePath, FileMode.Open);//打开文件
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string s = sr.ReadToEnd();//读取所有信息

            Regex rg = new Regex("[A-Za-z-]+");//用正则表达式匹配单词
            MatchCollection mc;//通过正则表达式所找到的成功匹配的集合
            mc = rg.Matches(s);
            Hashtable wordList = new Hashtable();
            for (int i = 0; i < mc.Count; i++)
            {
                string mcTmp = mc[i].Value.ToLower();//大小写不敏感
                if (mcTmp.Length >= 4)
                {
                    if (!wordList.ContainsKey(mcTmp))//第一次出现的单词添加为key
                    {
                        wordList.Add(mcTmp, 1);
                    }
                    else
                    {
                        int value = (int)wordList[mcTmp];
                        value++;
                        wordList[mcTmp] = value;
                    }
                }
            }
            fs.Close();
            sr.Close();
            string[] keys = new string[wordList.Count];
            int[] values = new int[wordList.Count];
            wordList.Keys.CopyTo(keys, 0);
            wordList.Values.CopyTo(values, 0);
            Array.Sort(values, keys);
            Array.Reverse(keys);
            try
            {
                for (int j = 0; j < 10 && j<wordList.Count; j++)
                {
                    Console.WriteLine(keys[j] + ":" + wordList[keys[j]]);
                }
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine("Exception caught:{0}", e);
            }
            return wordList;
        }
    }
    //派生类，优化用户体验
    class FunctionEX
    {

    }

}
