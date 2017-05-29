using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


class Poemwriter
{
    static Random r = new Random();

    public enum ConnectionType
    {
        free = 0,
        defaultOne = 1,
        zOne = 2,
        lOne = 3,
        two = 4
    }
    public class Line
    {   
        public static List<Line> lineList = new List<Line>();
        public static List<Words> addedword = new List<Words>();
        public List<Words> wordsInLine = new List<Words>();
        public string toneCode;
        public char penultVowel;
        public char lastVowel;
        public string rhymCode;
        public int length;
        public bool addedWord = false;

        public Line(string r)
        {
            toneCode = r;
            length = toneCode.Length;
            lineList.Add(this);
        }
 
        public static void readFromSchema(string [] tone)
        {
            for (int i = 0; i < tone.Length; i++)
            {
                Line l = new Line(tone[i]);
            }
        }
        public static void addRhymToListElements(string[] rhym)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                lineList[i].rhymCode = rhym[i];
            }
        }

        public static int percentRandom(int max)
        {
            int result = 0;
            do
            {
                int temp = r.Next(1, 101);
                int indx = temp < 1 ? 0 : temp < 8 ? 1 : temp < 33 ? 2 : temp < 65 ? 3 : temp < 91 ? 4 : temp < 95 ? 5 :
                    temp < 96 ? 6 : temp < 97 ? 7 : 8;
                result = indx;
            } while (max < result);

            return result;
        }
        public static int counter(string s)
        {
            int ndxOfString = s.Length - 1;
            int result = 0;
            while (s[ndxOfString] == '1' || s[ndxOfString] == '2' && ndxOfString != 0)
            {
                ndxOfString--;
                result++;
            }
            return result;
        }
        public static void writer(List<Line> lineList)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                int errorcounter = 10;
                bool rhymeUrge = false;
                bool end = false;
                string tone = lineList[i].toneCode;
                int tonlengthNdx = tone.Length - 1; 
                int readerIndex = tonlengthNdx;      
                do
                {
                    List<Words> templist = Words.basicWordList;
                    int randLength = readerIndex < 2 ? 1 : percentRandom(readerIndex);
                    string toCode = "";
                    rhymeUrge = false;
                    while (readerIndex > -1 && (tone[readerIndex] == '1' || tone[readerIndex] == '2') && randLength != 0)
                    {
                        if (readerIndex + 2 > tonlengthNdx && lineList[i].wordsInLine.Count == 0 ||
                            (lineList[i].wordsInLine.Count == 1 && lineList[i].wordsInLine.Last().wordCode.Length < 2))
                        {
                            rhymeUrge = true;
                        }
                        toCode = toCode.Insert(0, tone[readerIndex].ToString());
                        randLength--;
                        readerIndex--;
                    }
                    if (toCode == "")
                    {
                        lineList[i].wordsInLine.Add(addedword[tone[readerIndex] - 65]);
                        readerIndex--;
                    }
                    else
                    {
                        if (rhymeUrge)
                        {
                            if (lineList[i].wordsInLine.Count == 0)
                            {
                                if (toCode.Length == 1)
                                {

                                    if (lineList[i].lastVowel > 0)
                                    {
                                        templist = templist.FindAll(x => x.wordCode == toCode && x.lastVowels[0] == lineList[i].lastVowel);
                                    }
                                }
                                else
                                {
                                    if (lineList[i].lastVowel < 60 && lineList[i].penultVowel < 60)
                                    {
                                        templist = templist.FindAll(x => x.wordCode == toCode);
                                    }
                                    else
                                    {
                                        if (lineList[i].lastVowel > 0)
                                        {
                                            templist = templist.FindAll(x => x.wordCode == toCode &&
                                            lineList[i].lastVowel == (toCode.Length == 1 ? x.lastVowels[0] : x.lastVowels[1]));
                                        }
                                        if (toCode.Length != 1)
                                        {
                                            if (lineList[i].penultVowel > 0)
                                            {
                                                templist = templist.FindAll(x => x.wordCode == toCode && x.lastVowels[0] == lineList[i].penultVowel);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (lineList[i].penultVowel > 0)
                                {
                                    templist = templist.FindAll(x => x.wordCode == toCode && x.lastVowels[0] == lineList[i].penultVowel);
                                }
                                else
                                {
                                    templist = templist.FindAll(x => x.wordCode == toCode);
                                }
                            }
                        }
                        if (readerIndex > -1 && tone[readerIndex] == '2')
                        {
                            templist = templist.FindAll(x => x.first != (ConnectionType)4);
                            if(lineList[i].wordsInLine.Count != 0)
                            {
                                templist = selector(lineList[i].wordsInLine.Last(), toCode, templist, false);

                            }
                        }
                        else if (readerIndex > -1 && tone[readerIndex] != '1')
                        {
                            templist = selector(addedword[tone[readerIndex] - 65], toCode, templist, true);
                        }
                        if (lineList[i].wordsInLine.Count != 0)
                        {
                            templist = selector(lineList[i].wordsInLine.Last(), toCode, templist, false);
                        }
                    }
                    if (templist.Count != 0)
                    {
                        if (toCode != "")
                        {
                            Words newWordToLine = templist[r.Next(templist.Count - 1)];
                            lineList[i].wordsInLine.Add(newWordToLine);
                        }
                        if (rhymeUrge)
                        {
                            if (lineList[i].wordsInLine[0].lastVowels.Length == 1)
                            {
                                lineList[i].lastVowel = lineList[i].wordsInLine[0].lastVowels[0];
                                if (lineList[i].wordsInLine.Count > 1)
                                {
                                    lineList[i].penultVowel = (lineList[i].wordsInLine[1].lastVowels.Length == 1 ? lineList[i].wordsInLine[1].lastVowels[0] : lineList[i].wordsInLine[1].lastVowels[1]);
                                }
                            }
                            else
                            {
                                lineList[i].penultVowel = lineList[i].wordsInLine[0].lastVowels[0];
                                lineList[i].lastVowel = lineList[i].wordsInLine[0].lastVowels[1];
                            }
                            rhymeCheck(lineList, i);
                        }

                        if (readerIndex < 0) { end = true; }
                    }
                    else
                    {
                        readerIndex += toCode.Length;
                        if (errorcounter == 0)
                        {
                            Console.WriteLine("GÁz van " + toCode + "nincs meg");
                            break;
                        }
                        errorcounter--;
                    }

                } while (!end);
            }
        }

        public static List<Words> selector(Words basis, string codePart, List<Words> list, bool firstConn)
        {
            if (firstConn)
            {
                switch (basis.last)
                {
                    case (ConnectionType)0:
                        list = list.FindAll(x => x.first != (ConnectionType)4 && x.wordCode == codePart);
                        break;
                    case (ConnectionType)1:
                        list = list.FindAll(x => x.first == (ConnectionType)0 && x.wordCode == codePart);
                        break;
                    case (ConnectionType)3:
                        list = list.FindAll(x => x.first == (ConnectionType)0 && x.wordCode == codePart);
                        break;
                    case (ConnectionType)2:
                        list = list.FindAll(x => (x.first == (ConnectionType)0 || x.first == (ConnectionType)2) && x.wordCode == codePart);
                        break;
                    default:
                        list = list.FindAll(x => x.wordCode == codePart);
                        break;
                }

                return list;
            }
            else
            {
                switch (basis.first)
                {
                    case (ConnectionType)1:
                        list = list.FindAll(x => x.last == (ConnectionType)0 && x.wordCode == codePart);
                        break;
                    case (ConnectionType)2:
                        list = list.FindAll(x => x.last == (ConnectionType)0 && x.wordCode == codePart);
                        break;
                    case (ConnectionType)3:
                        list = list.FindAll(x => (x.last == (ConnectionType)0 || x.last == (ConnectionType)3)
                        && x.wordCode == codePart);
                        break;
                    default:
                        list = list.FindAll(x => x.wordCode == codePart);
                        break;
                }
                return list;
            }
        }
        public static void rhymeCheck(List<Line> lineList, int ndx)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                if (ndx != i && lineList[ndx].rhymCode == lineList[i].rhymCode)
                {
                    if (lineList[ndx].penultVowel > 0) { lineList[i].penultVowel = lineList[ndx].penultVowel; }
                    if (lineList[ndx].lastVowel > 0) { lineList[i].lastVowel = lineList[ndx].lastVowel; }
                }
            }
        }

        public static void addWordAndRhym(List<Line> lineList, List<Words> ListOfAddedWords)
        {
            for (int i = 0; i < ListOfAddedWords.Count; i++)
            {
                bool gothca = false;
                for (int j = 0; j < lineList.Count; j++)
                {

                    lineList[j].toneCode = findAndReplace(ListOfAddedWords[i], lineList[j], Convert.ToChar(i + 65), out gothca);
                    if (lineList[j].lastVowel > 0 || lineList[j].penultVowel > 0)
                    {
                        rhymeCheck(lineList, j);
                    }
                    if (gothca) { break; }
                }
            }

        }
        public static string findAndReplace(Words addWord, Line baseLine, char listindex, out bool gotcha)
        {
            int indexOfWord = 0;
            bool okToReplacing = false;
            bool goodConnection = true;
            if (baseLine.toneCode.Contains(addWord.wordCode) && (addWord.first != (ConnectionType)4 || baseLine.toneCode[indexOfWord - 1] != '2'))
            {
                indexOfWord = baseLine.toneCode.IndexOf(addWord.wordCode);
                if (indexOfWord != 0 && baseLine.toneCode[indexOfWord - 1] > 60)
                {
                    char lastchar = baseLine.toneCode[indexOfWord - 1];
                    if (addedword[(int)lastchar - 65].wordCode[addedword[(int)lastchar - 65].wordCode.Length - 1] == '2')
                    {
                        List<Words> temp = new List<Words>();
                        temp.Add(addedword[(int)lastchar - 65]);
                        temp = selector(addWord, temp[0].wordCode, temp, false);
                        if (temp.Count == 0) { goodConnection = false; }
                    }

                }
                if (goodConnection)
                {
                    int whereIsEnd = baseLine.toneCode.Length - (indexOfWord + addWord.wordCode.Length);
                    if (whereIsEnd > 1)
                    {

                        okToReplacing = true;
                    }
                    else if (whereIsEnd == 1)
                    {
                        if (baseLine.penultVowel < 1 || (addWord.lastVowels.Length == 2 ?
                            addWord.lastVowels[1] : addWord.lastVowels[0]).Equals(baseLine.penultVowel))
                        {
                            okToReplacing = true;
                            if (baseLine.penultVowel < 1)
                            {
                                baseLine.penultVowel = addWord.lastVowels.Length == 2 ?
                                    addWord.lastVowels[1] : addWord.lastVowels[0];
                            }
                        }
                    }
                    else if (whereIsEnd == 0)
                    {
                        if (addWord.wordCode.Length > 1)
                        {
                            if (baseLine.penultVowel < 1 || baseLine.penultVowel.Equals(addWord.lastVowels[0])
                                && baseLine.lastVowel < 1 || baseLine.lastVowel.Equals(addWord.lastVowels[1]))
                            {
                                okToReplacing = true;
                                if (baseLine.penultVowel < 1) { baseLine.penultVowel = addWord.lastVowels[0]; }
                                if (baseLine.lastVowel < 1) { baseLine.lastVowel = addWord.lastVowels[1]; }
                            }
                        }
                        else if (baseLine.lastVowel < 1 || baseLine.penultVowel.Equals(addWord.lastVowels[0]))
                        {
                            okToReplacing = true;
                            if (baseLine.penultVowel < 1) { baseLine.penultVowel = addWord.lastVowels[0]; }
                        }
                    }
                }
            }

            if (okToReplacing)
            {
                gotcha = true;
                baseLine.addedWord = true;
                baseLine.toneCode = baseLine.toneCode.Remove(indexOfWord, addWord.wordCode.Length)
                    .Insert(indexOfWord, listindex.ToString());
                return baseLine.toneCode;
            }
            gotcha = false;
            return baseLine.toneCode;
        }
    }

    public class Schema
    {
        public string poemForm;
        public string[] poemCode;
        public string strictRyme;
        public static List<Schema> schemaList = new List<Schema>();

        public Schema(string poemForm, string[] poemCode)
        {
            this.poemForm = poemForm;
            this.poemCode = poemCode;
            schemaList.Add(this);
        }
   
        public static void schemaCodeReader()
        {
        
            string[] t = File.ReadAllLines("../../text/Schemas.txt", Encoding.Default);
            string toSchemaName = "";
            string toRhyme = "";
            List<string> toSchemaCode = new List<string>();
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != "")
                {
                    if (t[i][0] == '*') { toSchemaName = t[i].ToUpper(); }
                    else if (t[i][0] == ';')
                    {
                        if (toRhyme.Length > 0) { Schema s = new Schema(toSchemaName.Remove(0, 1), toSchemaCode.ToArray()) { strictRyme = toRhyme.Remove(0, 1) }; }
                        else { Schema s = new Schema(toSchemaName.Remove(0, 1), toSchemaCode.ToArray()); }
                        toSchemaCode.Clear();
                    }
                    else if (t[i][0] == '#')
                    {
                        toRhyme = t[i];
                    }
                    else { toSchemaCode.Add(t[i]); }
                }
            }

        }
        public static void schemaTextReader(bool fromTxt)
        {
            string[] arr;
            string toSchemaName = "";
            if (fromTxt)
            {
                arr = File.ReadAllLines("../../text/vers.txt", Encoding.Default);
                toSchemaName = arr[0];

            }
            else
            {
                arr = Console.ReadLine().Split(' ');
            }
            List<string> toSchemaCode = new List<string>();
            for (int i = 1; i < arr.Length; i++)
            {
                toSchemaCode.Add(Words.codemake(Words.clearing(arr[i].Replace(" ", "").ToUpper())));

            }
            Schema s = new Schema(toSchemaName, toSchemaCode.ToArray());
        }
        public static void schemaWriter(List<string>  codedText)
        {
            StreamWriter sw = new StreamWriter("../../text/Schemas.txt", true, Encoding.UTF8);
            for (int i = 0; i < codedText.Count; i++)
            {
                if (codedText[i] != "") { sw.WriteLine(codedText[i]); }
               
            }
            sw.Close();
        }


    }
    public class Words
    {
        public string baseWord;
        public string wordCode;
        public string lastVowels;
        public string firstCode;
        public ConnectionType first;
        public ConnectionType last;
        public string lastCode;
        public static List<Words> basicWordList = new List<Words>();
        public static List<Words> newWordlist = new List<Words>();
        public static List<char> zarHang = new List<char>() { 'P', 'T', 'K', 'B', 'D', 'G' };
        public static List<char> likvida = new List<char>() { 'L', 'R' };


        public Words(string bW, bool added)
        {
            baseWord = bW.ToUpper();
            wordCode = codemake(baseWord.Replace(" ", ""));
            lastVowels = lastVowel(baseWord);
            firstCode = firsConnectCode(baseWord);
            lastCode = lastConnectCode(baseWord);
            first = switcher(firstCode);
            last = switcher(lastCode);
            if (added) Line.addedword.Add(this);
            else basicWordList.Add(this);

        }

        public Words(string[] input)
        {
            baseWord = input[0].ToUpper();
            wordCode = input[1];
            lastVowels = input[2];
            firstCode = input[3];
            lastCode = input[4];
            first = switcher(firstCode);
            last = switcher(lastCode);
            basicWordList.Add(this);

        }

        public ConnectionType switcher(string code)
        {

            if (code == "10" || code == "130" || code == "145") { return (ConnectionType)1; }
            if (code == "15") { return (ConnectionType)3; }
            if (code == "14") { return (ConnectionType)2; }
            if (code.Length > 2) { return (ConnectionType)4; }
            else { return (ConnectionType)0; }
        }

        public static string firsConnectCode(string w)
        {
            int counter = 0;
            string result = "1";
            if (w.Length != 1)
            {
                while (!vwls(w[counter]))
                {
                    result += wordCheck(w[counter], w, counter);
                    counter++;
                }
            }
            return result;
        }
        public static string lastConnectCode(string w)
        {
            int counter = w.Length - 1;
            string result = "1";
            if (w.Length != 1)
            {
                while (!vwls(w[counter]))
                {
                    result = result.Insert(1, (wordCheck(w[counter], w, counter)).ToString());
                    counter--;
                }
            }
            return result;
        }
        public static bool vwls(char x)
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
        public static string lastVowel(string baseword)
        {
            string toList;
            int counter = 0;
            for (int i = 0; i < baseword.Length; i++)
            {
                if (vwls(baseword[i]))
                {
                    if (counter == 2) break;
                    counter++;
                }
            }
            int l = 0;
            char[] y = new char[counter];
            for (int j = baseword.Length - 1; j > -1; j--)
            {
                if (vwls(baseword[j]))
                {
                    y[l] = baseword[j];
                    l++;
                    if (counter == l) break;
                }
            }
            Array.Reverse(y);
            toList = new string(y);
            return toList;
        }
        public static string clearing(string rawString)
        {
            char[] arr = rawString.Where(c => (char.IsLetter(c) || c == '-')).ToArray();

            rawString = new string(arr);
            return rawString;
        }
        public static char wordCheck(char x, string word, int i)
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
            if ((x == 'N' || x == 'L' || x == 'G' || x == 'T') && i != word.Length - 1 && word[i + 1] == 'Y')
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
            if (x == 'S' && i != word.Length - 1 && word[i + 1] == 'Z')
            {
                y = '3';
                return y;
            }
            if (x == 'Z' && i != word.Length - 1 && word[i + 1] == 'S')
            {
                y = '3';
                return y;
            }
            if (x == 'C' && i != word.Length - 1 && word[i + 1] == 'S')
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
        public static void Read()
        {
            string file = "../../text/magyar3.txt";
            StreamReader sr = new StreamReader(file, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] input = sr.ReadLine().Split(' ');
                Words w = new Words(input);
            }
            sr.Close();
        }
    }
    }

