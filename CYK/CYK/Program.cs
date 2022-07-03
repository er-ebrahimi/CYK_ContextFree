using System;
using System.Collections.Generic;
using System.Linq;
namespace CYK
{
    class Program
    {
        static void cykParse(List<string> w, Dictionary<string, List<List<string>>> lan)
        {
            int n = w.Count;

            // Initialize the table
            SortedDictionary<int, SortedDictionary<int, SortedSet<string>>> T = new SortedDictionary<int, SortedDictionary<int, SortedSet<string>>>();

            // Filling in the table
            for (int j = 0; j < n; j++)
            {

                // Iterate over the rules
                foreach (KeyValuePair<string, List<List<string>>> x in lan)
                {
                    string lhs = x.Key;
                    List<List<string>> rule = x.Value;

                    foreach (List<string> rhs in rule)
                    {

                        // If a terminal is found
                        if (rhs.Count == 1 && rhs[0] == w[j])
                        {
                            if (!T.ContainsKey(j))
                            {
                                T.Add(j, new SortedDictionary<int, SortedSet<string>>());
                            }
                            if (!T[j].ContainsKey(j))
                            {
                                T[j].Add(j, new SortedSet<string>());
                            }
                            T[j][j].Add(lhs);
                        }
                    }

                }
                for (int i = j; i >= 0; i--)
                {

                    // Iterate over the range from i to j
                    for (int k = i; k <= j; k++)
                    {

                        // Iterate over the rules
                        foreach (KeyValuePair<string, List<List<string>>> x in lan)
                        {
                            string lhs = x.Key;
                            List<List<string>> rule = x.Value;

                            foreach (List<string> rhs in rule)
                            {
                                // If a terminal is found
                                if (rhs.Count == 2 &&
                                    T.ContainsKey(i) &&
                                    T[i].ContainsKey(k) &&
                                    T[i][k].Contains(rhs[0]) &&
                                    T.ContainsKey(k + 1) &&
                                    T[k + 1].ContainsKey(j) &&
                                    T[k + 1][j].Contains(rhs[1]))
                                {
                                    if (!T.ContainsKey(i))
                                    {
                                        T.Add(i, new SortedDictionary<int, SortedSet<string>>());
                                    }
                                    if (!T[i].ContainsKey(j))
                                    {
                                        T[i].Add(j, new SortedSet<string>());
                                    }
                                    T[i][j].Add(lhs);
                                }
                            }

                        }
                    }
                }
            }
            // If word can be formed by rules
            // of given grammar
            if (T.ContainsKey(0) && T[0].ContainsKey(n - 1) && T[0][n - 1].Count != 0)
            {
                Console.Write("True\n");
            }
            else
            {
                Console.Write("False\n");
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
                List<string> temp2 =  temp.Substring(size, size1).Split(" | ").ToList();
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
                                if (lan[var[i]][j].Count == 1 & !landa.Contains(lan[var[i]][j][0]) )
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
            
            cykParse(str, lan);
        }
    }
}

