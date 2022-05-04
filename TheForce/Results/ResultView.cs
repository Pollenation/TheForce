using System;
using System.Collections.Generic;
using TheForce.Face;
using System.Threading.Tasks;
namespace TheForce.Results
{
    public class Result
    {
        private Dictionary<string, int> results = new Dictionary<string, int>();
        private Dictionary<string, int> calcresults = new Dictionary<string, int>();

        private Dictionary<string, int> numresults = new Dictionary<string, int>();

        public void Add(string name)
        {
            if (!results.ContainsKey(name))
            {
                results.Add(name, 1);
            }
            else
            {
                results[name] = results[name] + 1; 
            }
        }

        public void Add(string name, int number)
        {
            Boolean found = false;
            int nameint = 1;
            while (found == false)
            {
                string nameadd = $"{name} ({nameint.ToString()})";
                if (!numresults.ContainsKey(nameadd))
                {
                    numresults.Add(nameadd, number);
                    found = true;
                }
                else
                {
                    nameint += 1;
                }
            }
        }

        public void clear()
        {
            results.Clear();
        }

        public List<ResultView> getresults()
        {
            List<ResultView> listresults = new List<ResultView>();
            foreach (KeyValuePair<string, int> i in results)
            {
                listresults.Add(new ResultView { Name = i.Key, Quantity = i.Value });
            }
            foreach (KeyValuePair<string, int> i in numresults)
            {
                listresults.Add(new ResultView { Name = i.Key, Quantity = i.Value });
            }
            return listresults;
        }

        public async Task<List<ResultView>> getcalcresults()
        {
            HashSet<string> seen = new HashSet<string>();
            Dictionary<string, string> faces = new Dictionary<string, string>();
            var facelist = await App.diedb.GetFaceAsync();
            foreach (FacePool i in facelist)
            {
                faces.Add(i.Name, i.OpFace);
            }
            foreach (KeyValuePair<string, int> i in results)
            {
                if (faces.ContainsKey(faces[i.Key]))
                {
                    if (!seen.Contains(i.Key))
                    {
                        if (faces[i.Key] != "NA")
                        {
                            int opquan = 0;
                            try
                            {
                                opquan = results[faces[i.Key]];
                            }
                            catch
                            { }
                            if (i.Value > opquan)
                            {
                                calcresults.Add(i.Key, Math.Abs(i.Value - opquan));
                            }
                            else if (i.Value < opquan)
                            {
                                calcresults.Add(faces[i.Key], Math.Abs(i.Value - opquan));
                            }

                            seen.Add(i.Key);
                            seen.Add(faces[i.Key]);
                        }
                        else
                        {
                            calcresults.Add(i.Key, i.Value);
                            seen.Add(i.Key);
                        }
                    }
                }
                else
                {

                }
            }

            List<ResultView> listresults = new List<ResultView>();
            foreach (KeyValuePair<string, int> i in calcresults)
            {
                listresults.Add(new ResultView { Name = i.Key, Quantity = i.Value });
            }

            //adds up numeric dice
            if (numresults.Count != 0)
            {
                int total = 0;
                foreach (KeyValuePair<string, int> i in numresults)
                {
                    total += i.Value;
                }
                listresults.Add(new ResultView { Name = "Numeric Results", Quantity = total });
            }
            
            return listresults;
        }
    }

    public class ResultView
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
