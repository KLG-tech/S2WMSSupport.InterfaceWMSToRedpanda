using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterfaceWMSToRedpanda.Helper
{
    internal class ClassGeneral
    {
        private const string cstrKey = "k4w4NLam4P45t1Bi54";
        public static DateTime cstrDate = Convert.ToDateTime("12/12/2012");
        public void GetSettings(String FileName, ref string conn1)
        {
            String serverip = "";
            String databaseserver = "";
            String usersql = "";
            String password = "";
            String pooling = "";
            String connstring = "";
            String AccPass = "";
            String DestinationServerDBProd = "";
            string RemoteFolderUtamaPath = "";
            string RemoteFolderArchivePath = "";
            string RemoteFolderErrorPath = "";
            string LocalFolderPath = "";
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(FileName);
                string s = sr.ReadToEnd();
                string[] datasplit1;
                string[] datasplit2;
                datasplit1 = Regex.Split(s, Environment.NewLine, RegexOptions.None, TimeSpan.FromMilliseconds(100));
                if (datasplit1[0] == "")
                {
                    conn1 = "";
                }

                for (int i = datasplit1.GetLowerBound(0); i <= datasplit1.GetUpperBound(0); i++)
                {
                    if (i > 0)
                    {
                        datasplit2 = datasplit1[i].Split('=');
                        if (datasplit2[0] == "Datasource")
                            serverip = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "InitialCatalog")
                            databaseserver = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "UserID")
                            usersql = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "Password")
                            password = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "Pooling")
                            pooling = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "AccPass")
                            AccPass = datasplit2[1].Replace("\"", "");
                        else if (datasplit2[0] == "DestinationServerDBProd")
                            DestinationServerDBProd = datasplit2[1].Replace("\"", "");
                    }
                }
                password = fDecrypt(password, cstrKey);
                ClassVar.RemoteFolderUtamaPath = RemoteFolderUtamaPath;
                ClassVar.RemoteFolderArchivePath = RemoteFolderArchivePath;
                ClassVar.RemoteFolderErrorPath = RemoteFolderErrorPath;
                ClassVar.LocalFolderPath = LocalFolderPath;

                connstring = "User ID=" + usersql + ";Data Source=" + serverip + ";Password=" + password + ";Initial Catalog=" + databaseserver + ";pooling=true;Max Pool Size=" + pooling;
                
                conn1 = connstring;

                sr.Dispose();
            }
            catch (Exception ex)
            {
                serverip = ex.Message;
                conn1 = "";
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }
        }
        public string fDecrypt(string sInput, string sKey)
        {
            DateTime dDate;
            string[] s = new string[0];
            string[] sK = new string[0];
            string[] sR = new string[0];
            string[] sD = new string[0];
            int intLenInputan;
            int intLenKey;
            string strOut;
            string strDate;
            try
            {
                if (sKey == "")
                {
                    sKey = cstrKey;
                }
                dDate = cstrDate;
                

                strDate = Convert.ToDateTime(dDate).ToString("ddMMyyyy");

                intLenInputan = sInput.Length / 2;
                intLenKey = sKey.Length;

                strDate = mfFitStr(strDate, intLenInputan);

                if (intLenKey > intLenInputan)
                {
                    sKey = sKey.Substring(sKey.Length - intLenInputan, intLenInputan);
                }

                if (intLenKey < intLenInputan)
                {
                    sKey = mfAddX(sKey, intLenInputan - intLenKey);
                }

                for (int i = 1; i <= intLenInputan; i++)
                {
                    Array.Resize(ref sK, i);
                    Array.Resize(ref sD, i);
                    Array.Resize(ref s, i);
                    Array.Resize(ref sR, i);
                }

                for (int i = 0; i <= intLenInputan - 1; i++)
                {
                    sK[i] = sKey.Substring(i, 1);
                }

                for (int i = 0; i <= intLenInputan - 1; i++)
                {
                    sD[i] = strDate.Substring(i, 1);
                }

                for (int i = 0; i <= intLenInputan - 1; i++)
                {
                    s[i] = sInput.Substring(((i) * 2), 2);
                }

                for (int i = 0; i <= intLenInputan - 1; i++)
                {
                    sR[i] = ((int.Parse(s[i], System.Globalization.NumberStyles.HexNumber) ^ Convert.ToInt32(Convert.ToChar(sK[i]))) ^ Convert.ToInt32(Convert.ToChar(sD[i]))).ToString();
                }

                strOut = "";

                for (int i = 0; i <= intLenInputan - 1; i++)
                {
                    strOut = strOut + char.ConvertFromUtf32(Convert.ToInt32(sR[i])).ToString();
                }

                return strOut;
            }
            catch (Exception ex)
            {
                strOut = "";
                return strOut;
            }
        }
        public static string mfFitStr(string strInput, int intLength)
        {
            if (strInput.Length > intLength)
            {
                strInput = strInput.Substring(0, intLength);
            }
            else
            {
                strInput = mfAddX(strInput, intLength - strInput.Length);
            }
            return strInput;
        }
        public static string mfAddX(string strInput, int intAdd)
        {
            int x = 0;
            int y = 0;

            x = strInput.Length;
            y = strInput.Length + intAdd - 1;

            for (int i = x; i <= y; i++)
            {
                strInput = strInput + "x";
            }
            return strInput;
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            if (param.Length < length) length = param.Length;
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static String Right(String Words, Int32 Length)
        {
            String strResult = "";

            for (int i = (Words.Length) - Length; i <= Words.Length - 1; i++)
            {
                strResult += Words.Substring(i, 1);
            }

            return strResult;
        }
        public static string Mid(String Words, char chr, int i)
        {
            int indexofdash = 0;
            indexofdash = Words.IndexOf(chr, 0) + i;

            string strResult = "";
            strResult = Right(Words, Words.Length - indexofdash).Trim();

            return strResult;
        }
        public static string Reverse(string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }
    }
}
