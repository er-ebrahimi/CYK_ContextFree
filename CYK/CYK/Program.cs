﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace CYK
{
    class Program
    {
        static bool check_twoList(List<string> one, List<string> two)
        {
            for (int i = 0; i < one.Count; i++)
            {
                try
                {
                    if (one[i] != two[i])
                    {
                        return false;
                    }
                    else if (i == two.Count - 1 & i == one.Count - 1)
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
         static bool inserting(List<string> choosed_str, Dictionary<string, List<List<string>>> lan, List<string> var)
        {
            if (var.Count > choosed_str.Count)
            {
                return false;
            }
            else if (var.Count == choosed_str.Count)
            {
                if (check_twoList(choosed_str, var))
                {
                    return true;
                }
            }
            if (choosed_str.Count >= var.Count)
            {
                for (int i = 0; i < var.Count; i++)
                {
                    if (var[i][0] == '<')
                    {
                        for (int j = 0; j < lan[var[i]].Count; j++)
                        {
                            List<string> temp = var.ToList();
                            temp.RemoveAt(i);
                            temp.InsertRange(i, lan[var[i]][j]);
                            if (inserting(choosed_str, lan, temp))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            

            return false;
           
        }

        static void creating_string(List<string> choosed_str, Dictionary<string, List<List<string>>> lan, List<string> var)
        {

            List<List<string>> table = new List<List<string>>();
            List<string> created = new List<string>();
            bool flag = false;
                for (int j = 0; j < lan[var[0]].Count; j++)
                {

                        if (created.Count + lan[var[0]][j].Count <= choosed_str.Count)
                        {
                            created = lan[var[0]][j].ToList();
                            List<string> answer = new List<string>();
                            if (inserting(choosed_str, lan, created))
                            {
                                flag = true;
                            }
                            created.Clear();
                        }
                    
                }
            if (flag)
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
        static bool check_unit(List<List<string>> vs)
        {

                int size = vs.Count;
            for (int j = 0; j < size; j++)
            {

                if (vs[j][0][0] == '<' && vs[j].Count == 1)
                {
                    return true;
                }

            }
            
            return false;
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
                List<string> temp2 =  temp.Substring(size, size1).Split(new string[] { " | " }, StringSplitOptions.None).ToList();
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
                                if (lan[var[i]][j].Count == 1 & landa.Contains(lan[var[i]][j][0]) & lan[var[i]][j][0][0] == '<')
                                {
                                    //List<string> save = new List<string> { "#" };
                                    //lan[var[i]].Add(save);
                                    landa2.Add(var[i]);
                                    continue;
                                }
                                List<string> for_deleting = lan[var[i]][j].ToList();
                                for_deleting.RemoveAt(ind);
                                if (!lan[var[i]].Contains(for_deleting))
                                {
                                    lan[var[i]].Add(for_deleting);

                                }
                                
                            }
                        }

                    }
                }
            }
            //fro deleting <D> -> <D>
            for (int i = 0; i < lan.Count; i++)
            {
                int size = lan[var[i]].Count;
                for (int j = 0; j < size; j++)
                {

                    if (lan[var[i]][j][0][0] == '<' && lan[var[i]][j].Count == 1)
                    {

                        if (lan[var[i]][j][0] == var[i])
                        {
                            lan[var[i]].RemoveAt(j);
                            if (lan[var[i]].Count == 0)
                            {
                                lan.Remove(var[i]);
                                var.Remove(var[i]);
                            }

                        }

                    }

                }
            }
            //bool flag1 = true ;
            //while (flag1)
            //{
            //    flag1 = false;
            //    for (int i = 0; i < lan.Count; i++)
            //    {
            //        int size = lan[var[i]].Count;
            //        for (int j = 0; j < size; j++)
            //        {

            //            if (lan[var[i]][j][0][0] == '<' && lan[var[i]][j].Count == 1)
            //            {

            //                if (lan[var[i]][j][0] == var[i])
            //                {
            //                    lan[var[i]].RemoveAt(j);
            //                    if (lan[var[i]].Count == 0)
            //                    {
            //                        lan.Remove(var[i]);
            //                        var.Remove(var[i]);
            //                    }

            //                }
            //                else if (lan.ContainsKey(lan[var[i]][j][0]))
            //                {
            //                    string temp = lan[var[i]][j][0];
            //                    lan[var[i]].RemoveAt(j);
            //                    lan[var[i]].InsertRange(j, lan[temp]);
            //                    size = lan[var[i]].Count;
            //                }

            //            }

            //        }
            //    }
            //}


            creating_string(str, lan, var);
        }
    }
}

