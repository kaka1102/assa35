using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataContext.Constants;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DataContext.Until
{
    public class ValidateForm
    {

        public string replaceWhiteSpace(string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", "");
        }
        /// bắt buộc phải nhập
        public dataInput CheckRequiredField(string data, string name, int lengthMin, int lengthMax = -1)
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                //dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (lengthMax == -1)
            {
                if (dt.valueName.Trim().Length < lengthMin)
                {
                    // dt.error = name + " nhập vào phải lớn hơn " + lengthMin + " ký tự";
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MinlengthOfText, SystemMessageConst.ValidateConst.MinlengthOfTextEn), name, lengthMin);
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            if (dt.valueName.Trim().Length < lengthMin || dt.valueName.Trim().Length > lengthMax)
            {
                //  dt.error = name + " nhập vào phải từ " + lengthMin + " đến " + lengthMax + " ký tự";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MinMaxlengthOfText, SystemMessageConst.ValidateConst.MinMaxlengthOfTextEn), name, lengthMax, lengthMin);
                return dt;
            }
            return dt;
        }
        public dataInput CheckRequiteEmail(string data, string name)
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                //   dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (string.IsNullOrEmpty(dt.valueName))
            {
                //  dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            bool isPhone = Regex.IsMatch(dt.valueName, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!isPhone)
            {
                //dt.error = name + " không đúng định dạng";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), name);
                return dt;
            }
            return dt;
        }
        public dataInput CheckRequitePhone(string data, string name)
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                // dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "<[^>]*style", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (string.IsNullOrEmpty(dt.valueName))
            {
                //  dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
         //   bool isPhone = Regex.IsMatch(dt.valueName, @"^(01[0123456789]|09)[0-9]{8}$");
            bool isPhone = Regex.IsMatch(dt.valueName, @"^[0-9]{6,15}$");
            if (!isPhone)
            {
                // dt.error = name + " không đúng định dạng";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), name);
                return dt;
            }
            return dt;
        }
        public dataInput Check_CKEditor_Requite(string data, string name, int lengthMin, int lengthMax = -1)
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                // dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }

            List<string> lst = new List<string>() { "web.xml", "win.ini", "passwd", "%c0%ae", "Injection", "master.dbo", "declare @", "select.*?from", "{applicationScope}", "window.location", "<[^>]*script" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (dt.valueName.Trim().Length <= 60)
            {
                //dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            return dt;
        } // ck chua co upload anh
        public dataInput Check_CKEditor_Requite2(string data, string name, int lengthMin, int lengthMax = -1)  // ck có upload anh
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                //    dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            data = Regex.Replace(data, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            List<string> lst = new List<string>() { "web.xml", "win.ini", "passwd", "%c0%ae", "Injection", "master.dbo", "declare @", "select.*?from", "{applicationScope}", "window.location", "<[^>]*script" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (dt.valueName.Trim().Length <= 7)
            {
                //  dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            return dt;
        }
        public dataInput CheckRequiteIframe(string data, string name)
        {
            dataInput dt = new dataInput();
            if (string.IsNullOrEmpty(data))
            {
                //   dt.error = name + " không được để trống";
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            data = Regex.Replace(data, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            //"allow", "encrypted-media",
            List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "<[^>]*style", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
            foreach (var item in lst)
            {
                //if(test==true)
                if (Regex.IsMatch(data, item))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    //return dt;
                    //test = false;
                    break;
                }
                //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            dt.valueName = data;

            if (string.IsNullOrEmpty(data))
            {
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                return dt;
            }

            if (!string.IsNullOrEmpty(dt.valueName))
            {
                string key = @"iframe";
                int val = Regex.Matches(dt.valueName, key).OfType<Match>().Count();
                if (val != 2)
                {
                    // dt.error = name + " chỉ được dùng một video";
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.IsOnlyVideo, SystemMessageConst.ValidateConst.IsOnlyVideoEn), name);
                    return dt;
                }
            }
            else
            {
                dt.valueName = null;
                return dt;
            }
            return dt;
        }
        public dataInput CheckConfirm_PassWord(string data, string data2, string name)
        {
            dataInput dt = new dataInput();

            if (!string.IsNullOrEmpty(data2))
            {

                if (data != data2)
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.IsNotCorrect, SystemMessageConst.ValidateConst.IsNotCorrectEn), name);
                    return dt;
                }
                else
                {
                    dt.valueName = data2;
                }
            }
            else
            {
                dt.valueName = null;
                dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), name);
                return dt;
            }
            return dt;
        }

        public dataInput CheckNonRequiredDate(DateTime? data, string name)
        {
            dataInput dt = new dataInput();
            
            if (data.HasValue)
            {
                var value = data.Value.ToString();
                value = Regex.Replace(value, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "<[^>]*style", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
                foreach (var item in lst)
                {
                    //if(test==true)
                    if (Regex.IsMatch(data.Value.ToString(), item))
                    {
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                        //return dt;
                        //test = false;
                        break;
                    }
                    //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }
                dt.valueName = value;
                if (string.IsNullOrEmpty(value))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    return dt;
                }

                DateTime Test;

                if (DateTime.TryParseExact(value, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out Test))
                {
                    
                }
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotValid, SystemMessageConst.ValidateConst.DateNotValidEn), name);
                    return dt;

                }
                

            }
            else
            {
                dt.valueName = null;
                return dt;
            }

            return dt;
        }
        public dataInput CheckNonRequiredTime(TimeSpan? value, string name)
        {
            dataInput dt = new dataInput();

            if (value.HasValue)
            {
                var data = value.Value.ToString();


                List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
                foreach (var item in lst)
                {
                    //if(test==true)
                    if (Regex.IsMatch(data, item))
                    {
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                        //return dt;
                        //test = false;
                        break;
                    }
                    //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }

                TimeSpan Test;
                if (TimeSpan.TryParse(data, out Test) == false)
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotValid, SystemMessageConst.ValidateConst.DateNotValidEn), name);
                    return dt;
                }


            }
            else
            {
                dt.valueName = null;
                return dt;
            }

            return dt;
        }
        /// có thể nhập có thể không
        public dataInput CheckNonRequiredField(string data, string name, int lengthMin, int lengthMax = -1)
        {
            dataInput dt = new dataInput();

            if (!string.IsNullOrEmpty(data))
            {
               
                //bool test = true;
                dt.valueName = data;
                List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
                foreach (var item in lst)
                {
                    //if(test==true)
                    if(Regex.IsMatch(data, item))
                    {
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                        //return dt;
                        //test = false;
                        break;
                    }
                    //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }
                
                //if (string.IsNullOrEmpty(data))
                //{
                //    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                //    return dt;
                //}
                //if(test==false)
                    return dt;
                if (lengthMax == -1)
                {
                    if (dt.valueName.Trim().Length < lengthMin)
                    {
                        // dt.error = name + " nhập vào phải lớn hơn " + lengthMin + " ký tự";
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MinlengthOfText, SystemMessageConst.ValidateConst.MinlengthOfTextEn), name, lengthMin);
                        return dt;
                    }
                    else
                    {
                        return dt;
                    }
                }
                if (dt.valueName.Trim().Length < lengthMin || dt.valueName.Trim().Length > lengthMax)
                {
                    //    dt.error = name + " nhập vào phải từ " + lengthMin + " đến " + lengthMax + " ký tự";
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MinMaxlengthOfText, SystemMessageConst.ValidateConst.MinMaxlengthOfTextEn), name, lengthMax, lengthMin);
                    return dt;
                }
            }
            else
            {
                dt.valueName = null;
                return dt;
            }

            return dt;
        }
        public dataInput CheckNonRequiteURL(string data, string name)
        {
            dataInput dt = new dataInput();

            if (!string.IsNullOrEmpty(data))
            {
                data = Regex.Replace(data, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "<[^>]*style", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
                foreach (var item in lst)
                {
                    //if(test==true)
                    if (Regex.IsMatch(data, item))
                    {
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                        //return dt;
                        //test = false;
                        break;
                    }
                    //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }
                dt.valueName = data;
                if (string.IsNullOrEmpty(data))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    return dt;
                }

                bool isURL = Regex.IsMatch(data, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
                if (!isURL)
                {
                    // dt.error = name + " không đúng định dạng";
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), name);
                    return dt;
                }
            }
            else
            {
                dt.valueName = null;
                return dt;
            }
            return dt;
        }
        public dataInput CheckNonRequiteFAX(string data, string name)
        {
            dataInput dt = new dataInput();

            if (!string.IsNullOrEmpty(data))
            {
                data = Regex.Replace(data, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                List<string> lst = new List<string>() { "/web.xml", "win.ini", "etc/passwd", "%c0%ae/", "Injection", "master.dbo", "declare @", "select.*?from", "<[^>]*script", "<[^>]*style", "{applicationScope}", "<script.*?", "&lt;script.*", "window.location" };
                foreach (var item in lst)
                {
                    //if(test==true)
                    if (Regex.IsMatch(data, item))
                    {
                        dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                        //return dt;
                        //test = false;
                        break;
                    }
                    //data = Regex.Replace(data, item, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                }
                dt.valueName = data;

                if (string.IsNullOrEmpty(data))
                {
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DataIsNotCorrect, SystemMessageConst.ValidateConst.DataIsNotCorrectEn), name);
                    return dt;
                }

                bool isURL = Regex.IsMatch(data, @"^\+?[0-9]{6,}$");
                if (!isURL)
                {
                    // dt.error = name + " không đúng định dạng";
                    dt.error = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), name);
                    return dt;
                }
            }
            else
            {
                dt.valueName = null;
                return dt;
            }
            return dt;
        }



        public class dataInput
        {
            public string error { get; set; }
            public string valueName { get; set; }
        }


        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool IsValidFullName(string s)
        {

            var regex = new Regex(@"^[^<>.!@#%/]{1,50}$");
            return regex.IsMatch(s);
        }
        public bool IsValidUserName(string s)
        {
            var regex = new Regex(@"^(?=[A-Za-z])(?!.*[._()\[\]-]{2})[A-Za-z0-9._()\[\]-]{3,15}$");
            return regex.IsMatch(s);
        }

        public bool IsValidUserNameNotSpecial(string s)
        {
            var regex = new Regex(@"^[a-zA-Z0-9.]*$");
            return regex.IsMatch(s);
        }
        public bool IsValidPassWord(string s)
        {
            // có kt hoa.số,kt đặc biệt 10-16
            var regex = new Regex(@"^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{10,16}$");
            return regex.IsMatch(s);
        }

        public bool IsValidNumber(string s)
        {
            var regex = new Regex(@"^-*[0-9,\.]+$");
            return regex.IsMatch(s);
        }
        public bool IsValidInteger(string s)
        {
            var regex = new Regex(@"^-*[0-9,]+$");
            return regex.IsMatch(s);
        }

        public static bool IsValidTimeFormat(string input)
        {
            DateTime value2;
            var result = DateTime.TryParseExact(input,"h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out value2);
            return result;
        }
        public static bool IsValidDateFormat(string input)
        {
            DateTime dummyOutput;
            return DateTime.TryParse(input, out  dummyOutput);
        }
        /// <summary>
        /// Kiểm tra ngày họp so với ngày họp đã đăng xem có bị trùng lặp thời gian ko
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeStart"></param>
        /// <param name="timeEnd"></param>
        /// <returns></returns>
        public static bool CheckDateTimeMeeting(DateTime date,TimeSpan timeStart,TimeSpan timeEnd,int id = 0)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_cuochop
                        .FirstOrDefault(ob => ob.id != id && ob.trangthai == SystemMessageConst.StatusConst.Active &&
                                     (ob.ngaydienra.Day == date.Day && ob.ngaydienra.Month == date.Month && ob.ngaydienra.Year == date.Year) && ((ob.thoigianbatdau >= timeStart && ob.thoigianketthuc <= timeEnd) || (ob.thoigianbatdau <= timeStart && timeStart  <= ob.thoigianketthuc) || (ob.thoigianbatdau <= timeEnd && timeEnd <= ob.thoigianketthuc)));
                    if (result == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
