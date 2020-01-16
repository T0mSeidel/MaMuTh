using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMuTh
{
    public class Scale
    {
        public List<long> scale = new List<long>();
        public string title = "";
        public long tonic;
        public Key key;
        List<long> distance;

        public Scale( long tonic, Key key, Temperament temperament)
        {
            int denominator = 0;
            if (temperament == Temperament.Default)
                denominator = 12;

            this.tonic = tonic;
            this.key = key;
            scale.Add(tonic);

            this.title = Enum.GetName(typeof(Notes), tonic) + " " + key.ToString();
            if (key == Key.major)
            {
               distance = new List<long> { 2, 2, 1, 2, 2, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.minor)
            {
               distance = new List<long> { 2, 1, 2, 2, 1, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.wholeTone)
            {
                distance = new List<long> { 2, 2, 2, 2, 2};

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.locrian)
            {
                distance = new List<long> { 1, 2, 2, 1, 2, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.myxolydian)
            {
                distance = new List<long> { 2, 2, 1, 2, 2, 1 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.lydian)
            {
               distance = new List<long> { 2, 2, 2, 1, 2, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.phrygian)
            {
               distance = new List<long> { 1, 2, 2, 2, 1, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.dorian)
            {
               distance = new List<long> { 2, 1, 2, 2, 2, 1 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.dorian)
            {
               distance = new List<long> { 2, 1, 2, 2, 2, 1 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.harmonicMinor)
            {
               distance = new List<long> { 2, 1, 2, 2, 1, 3 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.melodicMinor)
            {
                distance = new List<long> { 2, 1, 2, 2, 2, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.altered)
            {
                distance = new List<long> { 1, 2, 1, 2, 2, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen2)
            {
               distance = new List<long> { 1, 2, 1, 2, 1, 2, 1 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen3)
            {
                distance = new List<long> { 2, 1, 1, 2, 1, 1, 2, 1};

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen4)
            {
                distance = new List<long> { 1, 1, 3, 1, 1, 1, 3 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen5)
            {
                distance = new List<long> { 1, 4, 1, 1, 4 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen6)
            {
                distance = new List<long> { 2, 2, 1, 1, 2, 2, 1 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
            else if (key == Key.messiaen7)
            {
                distance = new List<long> { 1, 1, 1, 2, 1, 1, 1, 1, 2 };

                for (int i = 0; i < distance.Count; i++)
                {
                    scale.Add((scale[i] + distance[i]) % denominator);
                }
            }
        }

        public Scale(long tonic, char[] customScale, Temperament temperament)
        {
            int denominator = 0;
            if (temperament == Temperament.Default)
                denominator = 12;

            this.tonic = tonic;
            scale.Add(tonic);

            this.title = Enum.GetName(typeof(Notes), tonic) + " " + key.ToString();
            this.key = (Key)0;

            distance = new List<long>();

            foreach(char c in customScale)
            {
                distance.Add(Convert.ToInt64(c));
            }


            for (int i = 0; i < distance.Count; i++)
            {
                scale.Add((scale[i] + distance[i]) % denominator);
            }

        }

        public bool Contains(long note)
        {
            return scale.Contains(note);
        }

        public void PrintScale()
        {
            String str = "( ";
            foreach (var item in scale)
            {
                str += " " + (Notes)item + ",";
            }
            str = str.Remove(str.Length - 1);
            str += " )";
            Console.WriteLine("{0,-20} {1,20}", title, str);
        }
        public void PrintDistance()
        {
            String str = "( ";
            foreach (var item in distance)
            {
                str += " " + item.ToString() + ",";
            }
            str = str.Remove(str.Length - 1);
            str += " )";
            String name = Enum.GetName(typeof(Key), key);
            Console.WriteLine("{0,-15} {1,-30}", name, str);
        }

    }
}
