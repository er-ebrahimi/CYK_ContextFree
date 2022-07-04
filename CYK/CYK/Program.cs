using System;
using System.Collections.Generic;
using System.Linq;
namespace CYK
{
    class Program
    {
         static bool inserting(List<string> choosed_str, Dictionary<string, List<List<string>>> lan, List<string> var)
        {
            if (var.Count <= choosed_str.Count)
            {
                for (int i = 0; i < var.Count; i++)
                {
                    if (var[i][0] == '<')
                    {
                        string oper = var[i];
                        for (int j = 0; j < lan[oper].Count; j++)
                        {
                            //var[i] = lan[oper][j];
                            List<string> temp = var.ToList();
                            temp.RemoveAt(i);
                            temp.InsertRange(i, lan[oper][j]);
                            if (inserting(choosed_str, lan, temp))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < choosed_str.Count; i++)
            {
                try
                {
                    if (choosed_str[i] != var[i])
                    {
                        return false;
                    }
                    else if (i == var.Count - 1 & i == choosed_str.Count - 1)
                    {
                        return true;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                    throw;
                }
                
            }
            return false;
        }
        static void creating_string(List<string> choosed_str, Dictionary<string, List<List<string>>> lan, List<string> var)
        {

            List<List<string>> table = new List<List<string>>();
            List<string> created = new List<string>();

                for (int j = 0; j < lan[var[0]].Count; j++)
                {
                    for (int k = 0; k < lan[var[0]][j].Count; k++)
                    {
                        if (created.Count + lan[var[0]][j].Count <= choosed_str.Count)
                        {
                            created.AddRange(lan[var[0]][j]);
                            List<string> answer = new List<string>();
                            if (inserting(choosed_str, lan, created))
                            {
                                Console.WriteLine("Accepted");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Rejected");
                                return;
                            }
                        }
                    }
                }
            
        }
        static void Main(string[] args)
        {
            Dictionary<string, List<List<string>>> lan = new Dictionary<string, List<List<string>>>();
            int num = int.Parse(Console.ReadLine());
            List<string> var = new List<string>();
            List<string> terminal = new List<string>();//save all the variables
            for (int i = 0; i < num; i++)
            {
                string temp = Console.ReadLine();
                int size = temp.IndexOf('-') + 3;
                int size1 = temp.Length - temp.IndexOf('-') - 3;
                List<string> temp2 =  temp.Substring(size, size1).Split(' ', '|').ToList();
                var.Add(temp.Substring(0, temp.IndexOf('-') - 1));
                lan.Add(temp.Substring(0, temp.IndexOf('-') - 1), new List<List<string>>());
                for (int k = 0; k < temp2.Count ; k++)
                {
                    List<string> temp_each_char = new List<string>();
                    //lan[temp].Add(temp2[k]);
                    
                    string every_char = "a";
                    bool flag = false;
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
                            flag = true;

                        }
                        else if (temp2[k][j] == '>')
                        {
                            every_char = every_char + temp2[k][j].ToString();
                            flag = false;
                            temp_each_char.Add(every_char);
                        }
                        else if (flag == true)
                        {
                            every_char = every_char + temp2[k][j].ToString();
                        }
                        //else
                        //{
                        //    flag = true;
                        //}
                    }
                    lan[temp.Substring(0 , temp.IndexOf('-') - 1)].Add(temp_each_char);
                }
                
            }
            string input = Console.ReadLine();
            List<string> str = input.ToCharArray().Select(x => x.ToString()).ToList();
            List<string> landa = new List<string>();//save variables which has #
                List<string> landa2 = new List<string>();
            foreach (var operation in lan)
            {
                for (int i = 0; i < operation.Value.Count; i++)
                {
                    for (int j = 0; j < operation.Value[i].Count; j++)
                    {
                        if (operation.Value[i][j] == "#")
                        {
                            landa.Add(operation.Key);
                            operation.Value.RemoveAt(i);//TODO maybe we have error with this in quera
                            break;
                        }
                    }
                }
            }
            landa2 = landa.ToList();
            while (landa2.Count != 0)
            {
                landa = landa2.ToList();//TODO check it works or not
                landa2.Clear();

                //delete landa
                for (int i = 0; i < lan.Count; i++)
                {
                    List<List<string>> op = new List<List<string>>();
                    for (int j = 0; j < lan[var[i]].Count; j++)
                    {

                        for (int z = 0; z < landa.Count; z++)
                        {
                            if (lan[var[i]][j].Contains(landa[z]))
                            {
                                int ind = lan[var[i]][j].IndexOf(landa[z]);
                                //lan[var[i]][j].RemoveAt(ind);
                                if (lan[var[i]][j].Count == 1 & !landa.Contains(lan[var[i]][j][0]) & lan[var[i]][j][0][0] == '<')
                                {
                                    //List<string> save = new List<string> { "#" };
                                    //lan[var[i]].Add(save);
                                    landa2.Add(var[i]);
                                    lan[var[i]].RemoveAt(j);
                                    continue;
                                }
                                List<string> for_deleting = lan[var[i]][j].ToList();
                                for_deleting.RemoveAt(ind);
                                lan[var[i]].Add(for_deleting);
                            }
                        }

                    }
                }
            }
            for (int i = 0; i < lan.Count; i++)
            {
                for (int j = 0; j < lan[var[i]].Count; j++)
                {

                    for (int z = 0; z < landa.Count; z++)
                    {
                        if (lan[var[i]][j].Contains(landa[z]))
                        {
                            int ind = lan[var[i]][j].IndexOf(landa[z]);
                            //lan[var[i]][j].RemoveAt(ind);
                            if (lan[var[i]][j].Count == 1 & !landa.Contains(lan[var[i]][j][0]) & lan[var[i]][j][0][0] == '<')
                            {
                                lan[var[i]].AddRange(lan[lan[var[i]][j][0]]);
                                lan[var[i]].RemoveAt(j);
                            }
                        }
                    }

                }
            }
            creating_string(str, lan, var);
        }
    }
}

