using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace poemwriter2
{
    class Poemwriter
    {
        static Random r = new Random();
        public enum listchooser
        {
            none = 0,
            toBasic = 1,
            toNew = 2

        }
        public class Line
        {
            public static List<Line> lineList = new List<Line>();
#pragma warning disable CS0649 // Field 'Poemwriter.Line.word' is never assigned to, and will always have its default value null
            public List<Words> word;
#pragma warning restore CS0649 // Field 'Poemwriter.Line.word' is never assigned to, and will always have its default value null
            public string rhymeCode;
#pragma warning disable CS0649 // Field 'Poemwriter.Line.rhymUrge' is never assigned to, and will always have its default value false
            public bool rhymUrge;
#pragma warning restore CS0649 // Field 'Poemwriter.Line.rhymUrge' is never assigned to, and will always have its default value false
            public char penultVowel;
            public char lastVowel;
            public string rhythm;
            public int length;

            public Line(string r, string rm)
            {
                rhymeCode = r;
                rhythm = rm;
                length = rm.Length;
                lineList.Add(this);
            }
            public static void adVowel(Line l, char v, bool first)
            {
                if (first) { l.penultVowel = v; }
                else { l.lastVowel = v; }
            }
            public static void adVowel(Line l, string vs)
            {

            }
            public static void adCut(List<Line> rC, List<Words> aW)
            {
                for (int i = 0; i < aW.Count; i++)
                {
                    string temp = aW[i].wordCode;
                    for (int j = 0; j < rC.Count; j++)
                    {
                        int ind = 0;
                        if (rC[j].rhymeCode.Contains(temp))
                        {
                            ind = rC[j].rhymeCode.IndexOf(temp);
#pragma warning disable CS0219 // The variable 'ok' is assigned but its value is never used
                            bool ok = false;
#pragma warning restore CS0219 // The variable 'ok' is assigned but its value is never used
                            int whereIsEnd = rC[j].rhymeCode.Length - (rC[j].rhymeCode.IndexOf(temp) + aW[i].wordCode.Length);
                            if (!rC[j].rhymUrge || whereIsEnd > 2)
                            {
                                ok = true;
                                break;
                            }
                            else if (whereIsEnd < 3)
                            {
                                for (int k = j; k > -1; k--)
                                {
                                    //if(whereIsEnd == 1 && rC[k].rhymeCode == rC[j].rhymeCode && rC[j].rhym == null || rC[j].rhym == aW[i].lastVowels) { ok = true; }
                                    //if(whereIsEnd == 2&&rC[k].rhymeCode == rC[j].rhymeCode && rC[j].rhym == null || rC[j].rhym[0] == aW[i].lastVowels[1]) { ok = true; }
                                }

                            }
                        }
                        rC[j].rhymeCode.Remove(ind, temp.Length);
                        rC[j].rhymeCode.Insert(ind, (char)97 + i.ToString());
                    }
                }

            }
        }
        public class AdditionalMethods
        {


            public static List<string> ready = new List<string>();

            public List<string> getlist() { return ready; }

            public static void Reader(string[] t)
            {
                string toSchemaName = "";
                List<string> toSchemaCode = new List<string>();
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i][0] == '*') { toSchemaName = t[i].ToUpper(); }
                    else if (t[i][0] == ';')
                    {
                        Schema s = new Schema(toSchemaName.Remove(0, 1), toSchemaCode.ToArray());
                        toSchemaCode.Clear();
                    }
                    else { toSchemaCode.Add(t[i]); }
                }
            }
            public static void writerEditor(Schema chosedSchema, string[] rhymecode, string[] additionalWords)
            {

                for (int i = 0; i < chosedSchema.poemCode.Length; i++)
                {
                    Line l = new Line(rhymecode[i], chosedSchema.poemCode[i]);
                }
                //string[] rhymes = new string[chosedSchema.poemCode.Length];
                //Words w = new Words();

                //for (int i = 0; i < chosedSchema.poemCode.Length; i++)
                //{
                //    bool rymeurge = false;
                //    for (int j = i - 1; j > -1; j--)
                //    {
                //        if (rhymecode[i] == rhymecode[j])
                //        {
                //            rymeurge = true;
                //            rhymes[i] = rhymes[j];

                //        }
                //    }

                //    bool nullGate = false;
                //    while (nullGate == false)
                //    {
                //        nullGate = true;
                //        int lineLength = chosedSchema.poemCode[i].Length;
                //        int start = 0;
                //        do
                //    {                     

                //        string part = chosedSchema.poemCode[i].Substring(start, percentRandom(lineLength - start));
                //        if (start + part.Length == lineLength - 1)
                //        {
                //            if (rymeurge == true)
                //            {
                //                w = w.selection(part, rhymes[i][0].ToString(), true);
                //                ready.Add(w.baseWord);
                //            }
                //            else
                //            {
                //                w = w.selection(part);
                //                ready.Add(w.baseWord);
                //                rhymes[i] = w.lastVowels.Length == 2 ? w.lastVowels[1].ToString() : w.lastVowels;
                //            }
                //        }
                //        else if (start + part.Length == lineLength)
                //        {
                //            if (rymeurge == true)
                //            {
                //                w = w.selection(part, part.Length==1?rhymes[i][1].ToString(): rhymes[i], false);
                //                ready.Add(w.baseWord);
                //            }
                //            else
                //            {
                //                w = w.selection(part);
                //                ready.Add(w.baseWord);
                //                rhymes[i] += w.lastVowels;
                //            }
                //        }
                //        else
                //            ready.Add(w.selection(part).baseWord);
                //        start += part.Length;
                //            if (w.wordCode == null)//----------------------------
                //            {
                //                nullGate = false;
                //                break;
                //            }


                //    } while (start != lineLength);

                //    }
                //    ready.Add("\n");
                //}

            }
            public void writer(string linecode, string start, string end, string rhymecode)
            {

            }
            public static int percentRandom(int max)
            {
                int result = 0;
                do
                {
                    int temp = r.Next(1, 101);
                    int indx = temp < 1 ? 0 : temp < 3 ? 1 : temp < 22 ? 2 : temp < 57 ? 3 : temp < 84 ? 4 : temp < 94 ? 5 :
                        temp < 96 ? 6 : temp < 97 ? 7 : temp < 98 ? 8 : temp < 99 ? 9 : temp == 100 ? 10 : 2;
                    result = indx;
                } while (max < result);

                return result;
            }
        }
        public class Schema
        {
            public string poemForm;
            public string[] poemCode;
            public static List<Schema> schemaList = new List<Schema>();
            public Schema(string poemForm, string[] poemCode)
            {
                this.poemForm = poemForm;
                this.poemCode = poemCode;
                schemaList.Add(this);
            }
            public Schema(bool codedWords, string[] poemCode)
            {
                if (codedWords == false)
                {
                    this.poemForm = "Costume";
                    read();

                }
                else
                {
                    this.poemForm = "Costume";
                    this.poemCode = poemCode;
                }
                schemaList.Add(this);

            }
            public static void read()
            {
                string[] t = File.ReadAllLines("../../text/Schemas.txt", Encoding.Default);
                //for (int i = 0; i < poemCode.Length; i++)
                //{
                //    poemCode[i] = Words.codemake(poemCode[i]);
                //}
            }
            public static List<Schema> getList() { return schemaList; }

        }
        public class Words
        {
            public string baseWord;
            public string wordCode;
            public string lastVowels;
            public string firstCode;
            public string lastCode;
            public static List<Words> basicWordList = new List<Words>();
            public static List<Words> newWordlist = new List<Words>();
            public static List<char> zarHang = new List<char>() { 'P', 'T', 'K', 'B', 'D', 'G' };
            public static List<char> likvida = new List<char>() { 'L', 'R' };
            public Words() { }
            /// <summary>
            /// none regular construktor
            /// </summary>
            /// <param name="bW"></param>
            /// <param name="iCForWord">what type of list to get
            /// 0=none, 1=basic, 2=new </param>
            public Words(string bW, listchooser iCForWord)
            {
                baseWord = bW.ToUpper();
                wordCode = codemake(baseWord);
                lastVowels = lastVowel(baseWord);
                firstCode = firsLastConsonent(baseWord, true);
                lastCode = firsLastConsonent(baseWord, false);
                if (iCForWord == listchooser.toNew) newWordlist.Add(this);
                if (iCForWord == listchooser.toBasic) basicWordList.Add(this);

            }
            /// <summary>
            /// regular constructor
            /// </summary>
            public Words(string[] input)
            {
                baseWord = input[0].ToUpper();
                wordCode = input[1];
                lastVowels = input[2];
                firstCode = input[3];
                lastCode = input[4];
                basicWordList.Add(this);

            }
            public Words(string bW, string wC, string lV)
            {
                baseWord = bW.ToUpper();
                wordCode = wC;
                lastVowels = lV;
                basicWordList.Add(this);
            }
            public Words selection(string code)
            {
                List<Words> temp = basicWordList.FindAll(x => x.wordCode == code).ToList();
                if (temp.Count == 0)
                    return null;
                return temp[r.Next(temp.Count)];
            }
            public Words selection(string code, string lastvowles, bool justTheLast)
            {
                List<Words> temp;
                if (justTheLast == true)
                {
                    temp = basicWordList.FindAll(x => x.wordCode == code && (x.lastVowels.Length == 2 ? x.lastVowels[1].ToString() :
                      x.lastVowels) == lastvowles).ToList();
                }
                else
                {
                    temp = basicWordList.FindAll(x => x.wordCode == code && x.lastVowels == lastvowles).ToList();
                }
                if (temp.Count == 0)
                    return null;
                return temp[r.Next(temp.Count)];
            }
            public string firsLastConsonent(string w, bool first)
            {
                int counter = 0;
                string result = "1";
                if (!first) { counter = w.Length - 1; }
                if (w.Length != 1)
                {
                    while (vwls(w[counter]) != true)
                    {
                        result += wordCheck(w[counter], w, counter);
                        if (first) { counter++; }
                        else { counter--; }

                    }
                }
                return result;
            }
            public bool vwls(char x)
            {
                if (x == 'A' || x == 'Á' || x == 'E' || x == 'É' || x == 'U' || x == 'Ú'
                    || x == 'Ü' || x == 'Ű' || x == 'O' || x == 'Ó' || x == 'Ö'
                    || x == 'Ő' || x == 'I' || x == 'Í')
                {
                    return true;
                }
                return false;
            }
            public static string codemake(string code)
            {
                string pr;
                char[] x = new char[code.Length];
                for (int j = 0; j < code.Length; j++)
                {
                    x[j] = wordCheck(code[j], code, j);

                }
                pr = new string(x);
                pr = pr.Replace("3", "");
                pr = pr.Replace("45", "0");
                pr = pr.Replace("4", "0");
                pr = pr.Replace("5", "0");
                pr = pr.Replace("200", "1");
                pr = pr.Replace("0", "");
                return pr;

            }
            public string lastVowel(string vwls)
            {
                string listbe;
                int counter = 0;
                for (int i = 0; i < vwls.Length; i++)
                {
                    if (this.vwls(vwls[i]) == true)
                    {
                        if (counter == 2) break;
                        counter++;
                    }
                }
                int l = 0;
                char[] y = new char[counter];
                for (int j = vwls.Length - 1; j > -1; j--)
                {
                    if (this.vwls(vwls[j]) == true)
                    {
                        y[l] = vwls[j];
                        l++;
                        if (counter == l) break;
                    }
                }
                Array.Reverse(y);
                listbe = new string(y);
                return listbe;
            }
            public static string clearing(string nyers)
            {

                nyers = nyers.Replace(" ", "");
                nyers = nyers.Replace("’", "");
                nyers = nyers.Replace("!", "");
                nyers = nyers.Replace("?", "");
                nyers = nyers.Replace(".", "");
                nyers = nyers.Replace(",", "");
                nyers = nyers.Replace("(", "");
                nyers = nyers.Replace(")", "");
                nyers = nyers.Replace("{", "");
                nyers = nyers.Replace("}", "");
                nyers = nyers.Replace("'", "");
                nyers = nyers.Replace(":", "");
                nyers = nyers.Replace(";", "");
                nyers = nyers.Replace(" ", "");
                nyers = nyers.Replace("«", "");
                nyers = nyers.Replace("»", "");
                if (nyers.StartsWith("-"))
                    nyers = nyers.Remove(0, 1);

                return nyers;
            }
            public static char wordCheck(char x, string szo, int i)
            {
                char y;
                if (x == 'A' || x == 'E' || x == 'U' || x == 'Ü' || x == 'O' || x == 'Ö' || x == 'I')
                {
                    y = '2';
                    return y;
                }
                if (x == 'Á' || x == 'É' || x == 'Ú' || x == 'Ű' || x == 'Ó' || x == 'Ő' || x == 'Í')
                {
                    y = '1';
                    return y;
                }
                if ((x == 'N' || x == 'L' || x == 'G' || x == 'T') && i != szo.Length - 1 && szo[i + 1] == 'Y')
                {
                    y = '3';
                    return y;
                }
                if (zarHang.Contains(x))
                {
                    y = '4';
                    return y;
                }
                if (likvida.Contains(x))
                {
                    y = '5';
                    return y;
                }
                if (x == 'S' && i != szo.Length - 1 && szo[i + 1] == 'Z')
                {
                    y = '3';
                    return y;
                }
                if (x == 'Z' && i != szo.Length - 1 && szo[i + 1] == 'S')
                {
                    y = '3';
                    return y;
                }
                if (x == 'C' && i != szo.Length - 1 && szo[i + 1] == 'S')
                {
                    y = '3';
                    return y;
                }
                y = '0';
                return y;
            }
            public static void write()
            {
                StreamWriter sw = new StreamWriter("magyar3.txt", true, Encoding.Default);
                for (int i = 0; i < basicWordList.Count; i++)
                {
                    string temp = basicWordList[i].baseWord + " " + basicWordList[i].wordCode + " " + basicWordList[i].lastVowels +
                        " " + basicWordList[i].firstCode + " " + basicWordList[i].lastCode;
                    sw.WriteLine(temp);
                }
                sw.Close();
            }
        }

    }
}
