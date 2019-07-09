using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataContext.Until
{
    public class ConvertTextToLink
    {
        public static string ConvertUrlsToLinks(string unicode)
        {
            if (!string.IsNullOrEmpty(unicode))
            {
                unicode = unicode.ToLower();
            }

            unicode = Regex.Replace(unicode, "[áàảãạăắằẳẵặâấầẩẫậåä]", "a");
            unicode = Regex.Replace(unicode, "[óòỏõọôồốổỗộơớờởỡợ]", "o");
            unicode = Regex.Replace(unicode, "[éèẻẽẹêếềểễệ]", "e");
            unicode = Regex.Replace(unicode, "[íìỉĩị]", "i");
            unicode = Regex.Replace(unicode, "[úùủũụưứừửữự]", "u");
            unicode = Regex.Replace(unicode, "[ýỳỷỹỵ]", "y");
            unicode = Regex.Replace(unicode, "[đ]", "d");

            //unicode = Regex.Replace(unicode, "[-\\s+/]+", "-");
            unicode = Regex.Replace(unicode, "\\W+", "-"); //Nếu bạn muốn thay dấu khoảng trắng thành dấu "" hoặc dấu cách " " thì thay kí tự bạn muốn vào đấu ""

            int last = unicode.LastIndexOf("-");
            if (last == unicode.Length - 1)
            {
                unicode = unicode.Remove(last, 1);
            }

            if (unicode.Length >= 230)
            {
                unicode = unicode.Substring(0, 230);
            }
            return unicode;
        }
    }

}