using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataContext.DAL;
using System.Web;
using DataContext.Constants;

namespace DataContext.Until
{
    public class DisplayUntils
    {
        private static Random random = new Random();
        public static string RandomString(int length, string chars)
        {
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomPass(string contents, int length)
        {
            var chars = contents;
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static string Encrypt(string toEncrypt, string key, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt, string key, bool useHashing = true)
        {
            var rt = "";
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                rt = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                //return "";
            }
            return rt;
        }
        public static string ConvertObjectToStringJson(SocketTypeMessages socketMessage)
        {
            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(socketMessage);
            return jsonString.ToString();
        }

        public static bool GetJson<T>(string name, JObject b, out T Tvalue)
        {
            var t = typeof(T);
            try
            {
                object value = b[name];
                Tvalue = (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(t) ?? t);
                return true;
            }
            catch (Exception ex)
            {
                Tvalue = default(T);
                return false;
            }
        }
        public static string TaoMatKhau()
        {
            var charsALL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var randomIns = new Random();
            int N = 10;
            var rndChars = Enumerable.Range(0, N).Select(_ => charsALL[randomIns.Next(charsALL.Length)]).ToArray();
            rndChars[1] = "0123456789"[randomIns.Next(10)];
            rndChars[2] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[randomIns.Next(26)];
            rndChars[3] = "abcdefghijklmnopqrstuvwxyz"[randomIns.Next(26)];
            var randomstr = new String(rndChars);
            return randomstr;
        }
        public static string RanDomChar(string charsALL)
        {
            var randomIns = new Random();
            int N = 6;
            var rndChars = Enumerable.Range(0, N)
                            .Select(_ => charsALL[randomIns.Next(charsALL.Length)])
                            .ToArray();
            rndChars[randomIns.Next(rndChars.Length)] = "0123456789"[randomIns.Next(10)];

            var randomstr = new String(rndChars);
            return randomstr;
        }
        public static string ReturnMessageBylanguage(string contentVn, string contentEn)
        {
            if (GetLanguageValues() == "en")
            {
                return contentEn;
            }
            return contentVn;
        }
        public static string GetLanguageValues()
        {
            string language = HttpContext.Current.Request.Cookies["Language"] != null
                ? HttpContext.Current.Request.Cookies["Language"].Value
                : null;
            return language;
        }

        public static string GetVersionValues()
        {
            string Version = HttpContext.Current.Request.Cookies["Version"] == null
                ? "en" : HttpContext.Current.Request.Cookies["Version"].Value;


            return Version;
        }
        public static string GetDocumentTab()
        {
            string Version = HttpContext.Current.Request.Cookies["type"] != null
                ? HttpContext.Current.Request.Cookies["type"].Value
                : "adenda";
            return Version;
        }

        public static string GetPracticalTab()
        {
            string Version = HttpContext.Current.Request.Cookies["tab"] != null
                ? HttpContext.Current.Request.Cookies["tab"].Value
                : "custom";
            return Version;
        }

        public static string GetPortfolioTab()
        {
            string Version = HttpContext.Current.Request.Cookies["Type_Speaker"] != null
                ? HttpContext.Current.Request.Cookies["Type_Speaker"].Value
                : "KEYNOTESPEAKER";
            return Version;
        }

        public static string ConvertUrlsToLinks(string unicode)
        {
            unicode = unicode.ToLower();
            unicode = Regex.Replace(unicode, "[áàảãạăắằẳẵặâấầẩẫậåä]", "a");
            unicode = Regex.Replace(unicode, "[óòỏõọôồốổỗộơớờởỡợ]", "o");
            unicode = Regex.Replace(unicode, "[éèẻẽẹêếềểễệ]", "e");
            unicode = Regex.Replace(unicode, "[íìỉĩị]", "i");
            unicode = Regex.Replace(unicode, "[úùủũụưứừửữự]", "u");
            unicode = Regex.Replace(unicode, "[ýỳỷỹỵ]", "y");
            unicode = Regex.Replace(unicode, "[đ]", "d");
            //unicode = Regex.Replace(unicode, "[-\\s+/]+", "-");
            unicode = Regex.Replace(unicode, "\\W+", "-"); //Nếu bạn muốn thay dấu khoảng trắng thành dấu "" hoặc dấu cách " " thì thay kí tự bạn muốn vào đấu ""
            return unicode;
        }
        public static string TimeSpan_PmAmFormat(TimeSpan? timeSpan)
        {
            var result = "";
            if(timeSpan != null)
            {
                DateTime dateTime = DateTime.MinValue.Add(timeSpan.Value);

                CultureInfo cultureInfo = CultureInfo.InvariantCulture;

                // optional
                //CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
                //cultureInfo.DateTimeFormat.PMDesignator = "PM";

                result = dateTime.ToString("h:mm tt", cultureInfo);
            }

            return result;
        }

        public static string DisplayDayMonYear(DateTime? dateData)
        {
            if (dateData != null)
            {
                return dateData.Value.ToString("dd-MM-yyyy");
               
            }
         return  ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData,
                SystemMessageConst.systemmessage.NoDataEn);
        }
    }
}
