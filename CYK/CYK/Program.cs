using System;
using System.Collections.Generic;
using System.Linq;
namespace CYK
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<List<string>>> lan = new Dictionary<string, List<List<string>>>();
            int num = int.Parse(Console.ReadLine());
            List<string> terminal = new List<string>();
            for (int i = 0; i < num; i++)
            {
                string temp = Console.ReadLine();
                List<string> temp2 =  temp.Substring(temp.IndexOf('>') - temp.Length - temp.IndexOf('>')).Split('|').ToList();
                lan.Add(temp, new List<List<string>>());
                for (int k = temp.IndexOf('>'); k < temp2.Count - 1; k++)
                {
                    List<string> temp_each_char = new List<string>();
                    //lan[temp].Add(temp2[k]);
                    
                    string every_char = "a";
                    bool flag = true;
                    for (int j = 0; j < temp2[k].Length; j++)
                    {
                        if (temp2[k][j] != '<' & flag == false)
                        {
                            every_char = temp2[k][j].ToString();
                            temp_each_char.Add(every_char);
                        }
                        else if (temp2[k][j] == '<')
                        {
                            every_char = temp2[k][j].ToString();

                        }
                        else if (flag == true)
                        {
                            every_char = every_char + temp2[k][j].ToString();
                        }
                        else if (temp2[k][j] == '>')
                        {
                            every_char = every_char + temp2[k][j].ToString();
                            flag = true;
                            temp_each_char.Add(every_char);
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }

            }
        }
    }
}

