
QTW_PROTOTYPE = function () {
    var dateFormat = function () {
        var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
            timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
            timezoneClip = /[^-+\dA-Z]/g,
            pad = function (val, len) {
                val = String(val);
                len = len || 2;
                while (val.length < len) val = "0" + val;
                return val;
            };

        // Regexes and supporting functions are cached through closure
        return function (date, mask, utc) {
            var dF = dateFormat;

            // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
            if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
                mask = date;
                date = undefined;
            }

            // Passing date through Date applies Date.parse, if necessary
            date = date ? new Date(date) : new Date;
            if (isNaN(date)) throw SyntaxError("invalid date");

            mask = String(dF.masks[mask] || mask || dF.masks["default"]);

            // Allow setting the utc argument via the mask
            if (mask.slice(0, 4) == "UTC:") {
                mask = mask.slice(4);
                utc = true;
            }

            var _ = utc ? "getUTC" : "get",
                d = date[_ + "Date"](),
                D = date[_ + "Day"](),
                m = date[_ + "Month"](),
                y = date[_ + "FullYear"](),
                H = date[_ + "Hours"](),
                M = date[_ + "Minutes"](),
                s = date[_ + "Seconds"](),
                L = date[_ + "Milliseconds"](),
                o = utc ? 0 : date.getTimezoneOffset(),
                flags = {
                    d: d,
                    dd: pad(d),
                    ddd: dF.i18n.dayNames[D],
                    dddd: dF.i18n.dayNames[D + 7],
                    m: m + 1,
                    mm: pad(m + 1),
                    mmm: dF.i18n.monthNames[m],
                    mmmm: dF.i18n.monthNames[m + 12],
                    yy: String(y).slice(2),
                    yyyy: y,
                    h: H % 12 || 12,
                    hh: pad(H % 12 || 12),
                    H: H,
                    HH: pad(H),
                    M: M,
                    MM: pad(M),
                    s: s,
                    ss: pad(s),
                    l: pad(L, 3),
                    L: pad(L > 99 ? Math.round(L / 10) : L),
                    t: H < 12 ? "a" : "p",
                    tt: H < 12 ? "am" : "pm",
                    T: H < 12 ? "A" : "P",
                    TT: H < 12 ? "AM" : "PM",
                    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
                };

            return mask.replace(token, function ($0) {
                return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
            });
        };
    }();

    // Some common format strings
    dateFormat.masks = {
        "default": "ddd mmm dd yyyy HH:MM:ss",
        shortDate: "m/d/yy",
        mediumDate: "mmm d, yyyy",
        longDate: "mmmm d, yyyy",
        fullDate: "dddd, mmmm d, yyyy",
        shortTime: "h:MM TT",
        mediumTime: "h:MM:ss TT",
        longTime: "h:MM:ss TT Z",
        isoDate: "yyyy-mm-dd",
        isoTime: "HH:MM:ss",
        isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
        isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
    };

    dateFormat.i18n = {
        dayNames: [
            "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
            "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
        ],
        monthNames: [
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
        ]
    };

    Date.prototype.format = function (mask, utc) {
        return dateFormat(this, mask, utc);
    };
    if (!String.prototype.format) {
        String.prototype.format = function () {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                    ? args[number]
                    : match
                ;
            });
        };
    };
    Storage.prototype.setObject = function (key, value) {
        this.setItem(key, JSON.stringify(value));
    };

    Storage.prototype.getObject = function (key) {
        return JSON.parse(this.getItem(key));
    };
    Storage.prototype.removeObject = function (key) {
        localStorage.removeItem(key);
    };
    Storage.prototype.setItemInsideObject = function (key, name, item) {
        var ob = JSON.parse(this.getItem(key));
        if (item == undefined) {
            delete ob[name];
        }
        else {
            ob[name] = item;
        }
        this.setItem(key, JSON.stringify(ob));
    };
    Array.prototype.hasMin = function (attrib) {
        return this.reduce(function (prev, curr) {
            return prev[attrib] < curr[attrib] ? prev : curr;
        });
    };
    Array.prototype.hasMax = function (attrib) {
        return this.reduce(function (prev, curr) {
            return prev[attrib] > curr[attrib] ? prev : curr;
        });
    };
    Array.prototype.remove = function () {
        var what, a = arguments, L = a.length, ax;
        while (L && this.length) {
            what = a[--L];
            while ((ax = this.indexOf(what)) !== -1) {
                this.splice(ax, 1);
            }
        }
        return this;
    };
    Array.prototype.filterObjects = function (key, value) {
        return this.filter(function (x) { return x[key] === value; })
    };
    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (searchElement, fromIndex) {

            var k;

            // 1. Let o be the result of calling ToObject passing
            //    the this value as the argument.
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let lenValue be the result of calling the Get
            //    internal method of o with the argument "length".
            // 3. Let len be ToUint32(lenValue).
            var len = o.length >>> 0;

            // 4. If len is 0, return -1.
            if (len === 0) {
                return -1;
            }

            // 5. If argument fromIndex was passed let n be
            //    ToInteger(fromIndex); else let n be 0.
            var n = fromIndex | 0;

            // 6. If n >= len, return -1.
            if (n >= len) {
                return -1;
            }

            // 7. If n >= 0, then Let k be n.
            // 8. Else, n<0, Let k be len - abs(n).
            //    If k is less than 0, then let k be 0.
            k = Math.max(n >= 0 ? n : len - Math.abs(n), 0);

            // 9. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ToString(k).
                //   This is implicit for LHS operands of the in operator
                // b. Let kPresent be the result of calling the
                //    HasProperty internal method of o with argument Pk.
                //   This step can be combined with c
                // c. If kPresent is true, then
                //    i.  Let elementK be the result of calling the Get
                //        internal method of o with the argument ToString(k).
                //   ii.  Let same be the result of applying the
                //        Strict Equality Comparison Algorithm to
                //        searchElement and elementK.
                //  iii.  If same is true, return k.
                if (k in o && o[k] === searchElement) {
                    return k;
                }
                k++;
            }
            return -1;
        };
    };
    if (!Array.prototype.forEach) {

        Array.prototype.forEach = function (callback/*, thisArg*/) {

            var T, k;

            if (this == null) {
                throw new TypeError('this is null or not defined');
            }

            // 1. Let O be the result of calling toObject() passing the
            // |this| value as the argument.
            var O = Object(this);

            // 2. Let lenValue be the result of calling the Get() internal
            // method of O with the argument "length".
            // 3. Let len be toUint32(lenValue).
            var len = O.length >>> 0;

            // 4. If isCallable(callback) is false, throw a TypeError exception.
            // See: http://es5.github.com/#x9.11
            if (typeof callback !== 'function') {
                throw new TypeError(callback + ' is not a function');
            }

            // 5. If thisArg was supplied, let T be thisArg; else let
            // T be undefined.
            if (arguments.length > 1) {
                T = arguments[1];
            }

            // 6. Let k be 0
            k = 0;

            // 7. Repeat, while k < len
            while (k < len) {

                var kValue;
                if (k in O) {

                    // i. Let kValue be the result of calling the Get internal
                    // method of O with argument Pk.
                    kValue = O[k];

                    // ii. Call the Call internal method of callback with T as
                    // the this value and argument list containing kValue, k, and O.
                    callback.call(T, kValue, k, O);
                }
                // d. Increase k by 1.
                k++;
            }
            // 8. return undefined
        };
    };

    if (Object.defineProperty
        && Object.getOwnPropertyDescriptor
        && Object.getOwnPropertyDescriptor(Element.prototype, "textContent")
        && !Object.getOwnPropertyDescriptor(Element.prototype, "textContent").get) {
        (function () {
            var innerText = Object.getOwnPropertyDescriptor(Element.prototype, "innerText");
            Object.defineProperty(Element.prototype, "textContent",
                // Passing innerText or innerText.get directly does not work,
                // wrapper function is required.
                {
                    get: function () {
                        return innerText.get.call(this);
                    },
                    set: function (s) {
                        return innerText.set.call(this, s);
                    }
                }
            );
        })();
    }
    Array.prototype.filterObjects = function (key, value) {
        return this.filter(function (x) { return x[key] === value; })
    };

},

    QTW_VALIDATION = {
        common: {
            ispass: function (val) {
                var re = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{6,16}$/;
                return re.test(val);
            },
            addCommas2: function (nStr) {
                nStr += '';
                x = nStr.split('.');
                x1 = x[0];
                x2 = x.length > 1 ? '.' + x[1] : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }
                return x1 + x2 + ' ₫';
            },
            getMoneyOrNull: function (str) {
                var money = parseFloat(String(str).replace(/[,. ₫]/g, ""));
                return (!isNaN(money) ? money : null);
            },
            setMoneyFomart: function (num) {
                if (isNaN(num)) {
                    return '0 ₫';
                }
                else
                    return (this.addCommas2(String(num)));
            },
            isValidDate: function (strDate, split) {
                if (strDate.length != 10) return false;
                var dateParts = strDate.split(split);
                var date = new Date(dateParts[2], (dateParts[1] - 1), dateParts[0]);
                if (date.getDate() == dateParts[0] && date.getMonth() == (dateParts[1] - 1) && date.getFullYear() == dateParts[2]) {
                    return true;
                }
                else {
                    date = new Date(dateParts[2], (dateParts[0] - 1), dateParts[1]);
                    if (date.getDate() == dateParts[1] && date.getMonth() == (dateParts[0] - 1) && date.getFullYear() == dateParts[2]) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            },
            is_Boolean: function (val) {
                if (typeof (val) === "boolean")
                    return val;
                return true;
            },
            isUniCode: function (str) {
                for (var i = 0, n = str.length; i < n; i++) {
                    if (str.charCodeAt(i) > 255) { return true; }
                }
                return false;
            },
            isnot_special: function (str) {
                var regex = new RegExp("^[a-zA-Z0-9.]*$");
                return regex.test(str);
            },
            hasWhiteSpace: function (str) {
                return /\s/g.test(str);
            },
            isEmptyObject: function (str) {
                return (!str || /^\s*$/.test(str));
            },
            isnotSpecialChar: function (str) {
                return !/[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?]/g.test(str);
            },
            isEmail: function (str) {
                var isValid = false;
                var regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (regex.test(str)) {
                    isValid = true;
                }
                return isValid;
            },

            isNumeric: function () {
                return /^-*[0-9,\.]+$/.test(str);
            },
            is_Numeric: function (str) {
                return /^\d+$/.test(str);
            },
            isInt: function (n) {
                return n % 1 === 0;
            },
            isFloat: function (n) {
                return Number(n) === n && n % 1 !== 0;
            },
            addCommas: function (nStr) {
                nStr += '';
                x = nStr.split('.');
                x1 = x[0];
                x2 = x.length > 1 ? '.' + x[1] : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + '.' + '$2');
                }
                return x1 + x2 + ' ' + "0 ₫".substring("0 ₫".length - 1, "0 ₫".length);
            },
            isInt_Float: function (str) {
                if ((undefined === str) || (null === str)) {
                    return false;
                }
                if (typeof str == 'number') {
                    return true;
                }
                return !isNaN(str - 0);
            },
            isCurrency: function (str) {
                var number = Number(str.replace(/[^0-9\.]+/g, ""));
                return !isNaN(number)
            },
            fm_Currency: function (str) {
                return Number(str.replace(/[^0-9\.]+/g, ""));
            },
            validateEmail: function (str) {
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(str);
            },
            validatePhone: function (str) {
                // var re = /^(01[0123456789]|09)[0-9]{8}$/;
                var re = /^\+?[0-9]{6,15}$/;
                return re.test(str);
            },
            validateURL: function (str) {
                var re = /\b(?:(?:https?|ftp):\/\/|www\.)[-a-z0-9+&@#\/%?=~_|!:,.;]*[-a-z0-9+&@#\/%=~_|]/i;
                return re.test(str);
            },
            validateFAX: function (str) {
                var re = /^\+?[0-9]{6,}$/;
                return re.test(str);
            },
            typeError: 1,
            appendError: function (str, parent) {
                var $txt = document.createElement('div');
                $txt.classList.add('help');
                $txt.innerHTML = str;
                parent.previousElementSibling.classList.add('error');
                if (parent.querySelector('input')) {
                    parent.querySelector('input').classList.add('parsley-error');
                }
                if (parent.querySelector('textarea')) {
                    parent.querySelector('textarea').classList.add('parsley-error');
                }

                parent.appendChild($txt);
            },
            getParentByClassName: function (node, classname) {
                while ((node = node.parentElement) && !node.classList.contains(classname));
                return node;
            },
            removeValidation: function (obj, className) {

                var parent = obj.parentNode;
                if (parent != undefined) {
                    var help = parent.querySelector('.help');
                    if (help) {
                        help.parentNode.removeChild(help);
                    }
                }
                parent.previousElementSibling.classList.remove('error');
                obj.classList.remove('parsley-error');
                return parent;
            },
            trim: function (str) {
                return str.replace(/^\s+|\s+$/gm, '');
            },
            formatDateBC: function (dt, format) {
                var Days = [
                    "Sunday", "Monday", "Tuesday", "Wednesday",
                    "Thursday", "Friday", "Saturday"
                ];
                var monthNames = [
                    "January", "February", "March",
                    "April", "May", "June", "July",
                    "August", "September", "October",
                    "November", "December"
                ];
                var pad = function (n, width, z) {
                    z = z || '0';
                    n = n + '';
                    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
                };
                var formatDate = function (dt, format) {
                    format = format.replace('ss', pad(dt.getSeconds(), 2));
                    format = format.replace('s', dt.getSeconds());
                    format = format.replace('dd', pad(dt.getDate(), 2));
                    format = format.replace('d', dt.getDate());
                    format = format.replace('mm', pad(dt.getMinutes(), 2));
                    format = format.replace('m', dt.getMinutes());
                    format = format.replace('MMMM', monthNames[dt.getMonth()]);
                    format = format.replace('MMM', monthNames[dt.getMonth()].substring(0, 3));
                    format = format.replace('MM', pad(dt.getMonth() + 1, 2));
                    format = format.replace(/M(?![ao])/, dt.getMonth() + 1);
                    format = format.replace('DD', Days[dt.getDay()]);
                    format = format.replace(/D(?!e)/, Days[dt.getDay()].substring(0, 3));
                    format = format.replace('yyyy', dt.getFullYear());
                    format = format.replace('YYYY', dt.getFullYear());
                    format = format.replace('yy', (dt.getFullYear() + "").substring(2));
                    format = format.replace('YY', (dt.getFullYear() + "").substring(2));
                    format = format.replace('HH', pad(dt.getHours(), 2));
                    format = format.replace('H', dt.getHours());
                    return format;
                };
                return formatDate(dt, format);
            },
            obEmpty: function (o) {
                return Object.keys(o).length === 0;
            },
            isnum: function (val) {
                return /^\d+$/.test(val);
            }
        },
        convertJsonToDate: function (data) {
            if (typeof data === 'string') {
                return (data != '') ? new Date(parseInt(data.substr(6))) : '';
            }
            return '';
        },
        isNumberFomat: function (obj, className, min, max) {
            var ischeck = false;
            var lengthfocus = this.common.trim(obj.value).replace(/[ ₫]/g, "").length;
            var str = this.common.trim(obj.value).replace(/[,. ₫]/g, "");
            var isnum = /^\d+$/.test(str);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (isnum) {
                    var valmoney = this.common.addCommas2(str);
                    obj.value = valmoney;
                    obj.dataset.money = parseFloat(str);
                    obj.setSelectionRange((valmoney.length - 2), (valmoney.length - 2));
                    if (parseFloat(str) >= min && parseFloat(str) <= max) {
                        ischeck = true;
                    }
                    else {
                        this.common.appendError('Phải từ ' + min + ' đến ' + max, parent, obj);
                    }
                }
                else {
                    this.common.appendError('Giá trị phải là kiểu số', parent, obj);
                }
            }
            return ischeck;
        },
        is_Password: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.ispass(str)) {
                        ischeck = true;
                    }
                    else {
                        this.common.appendError('Mật khẩu phải lớn hơn 6 ký tự, có chữ hóa, chữ thường và ký tự đặc biệt', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        is_Files: function (obj, className) {
            var ischeck = false;
            var file = obj.files;
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (file.length > 0) {// nếu không rỗng
                    ischeck = true;
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        is_Date2: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    var day = moment(str, "DD/MM/YYYY");
                    if (!isNaN(Date.parse(day))) {
                        ischeck = true;
                    }
                    else {
                        this.common.appendError('Không đúng định dạng ngày tháng', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        is_Email: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.validateEmail(str)) {
                        ischeck = true;
                    }
                    else {
                        this.common.appendError('Không đúng định dạng email', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        is_Phone: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.validatePhone(str)) {
                        ischeck = true;
                    }
                    else {
                        this.common.appendError('Không đúng định dạng số điện thoại', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        UserName: function (obj, className, min) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                        if (this.common.isnot_special(str)) {// không có kí tự unicode
                            if (str.length >= min) {// kí tự >= 6
                                ischeck = true;
                            }
                            else {//kí tự < 6
                                this.common.appendError('Phải lớn hơn hoặc bằng ' + (min) + ' kí tự', parent);
                            }
                        }
                        else {// có kí tự unicode
                            this.common.appendError('Không được có kí tự đặc biệt', parent);
                        }
                    }
                    else { // có khoảng trắng
                        this.common.appendError('Không được có khoảng trắng', parent);
                    }
                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },

        UserName2: function (obj, className, min1, min2) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.isnum(str)) {
                        if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                            if (this.common.isnot_special(str)) {// không có kí tự unicode
                                if (str.length == min1) {// kí tự >= 6
                                    ischeck = true;
                                }
                                else {//kí tự < 6
                                    if (str.length == min2) {// kí tự >= 6
                                        ischeck = true;
                                    }
                                    else {//kí tự < 6
                                        this.common.appendError('Phải bằng ' + (min1) + ' hoặc ' + (min2) + ' kí tự', parent);
                                    }
                                }
                            }
                            else {// có kí tự unicode
                                this.common.appendError('Không được có kí tự đặc biệt', parent);
                            }
                        }
                        else { // có khoảng trắng
                            this.common.appendError('Không được có khoảng trắng', parent);
                        }
                    }
                    else {
                        this.common.appendError('Không được có chữ', parent);
                    }

                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        isSelectes: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var _str = parseFloat(str);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str) && _str > 0) {// nếu không rỗng
                    ischeck = true;
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        UserName2: function (obj, className, min1, min2) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.isnum(str)) {
                        if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                            if (this.common.isnot_special(str)) {// không có kí tự unicode
                                if (str.length == min1) {// kí tự >= 6
                                    ischeck = true;
                                }
                                else {//kí tự < 6
                                    if (str.length == min2) {// kí tự >= 6
                                        ischeck = true;
                                    }
                                    else {//kí tự < 6
                                        this.common.appendError('Phải bằng ' + (min1) + ' hoặc ' + (min2) + ' kí tự', parent);
                                    }
                                }
                            }
                            else {// có kí tự unicode
                                this.common.appendError('Không được có kí tự đặc biệt', parent);
                            }
                        }
                        else { // có khoảng trắng
                            this.common.appendError('Không được có khoảng trắng', parent);
                        }
                    }
                    else {
                        this.common.appendError('Không được có chữ', parent);
                    }

                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        UserName3: function (obj, className, min1, min2) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                        if (this.common.isnot_special(str)) {// không có kí tự unicode
                            if (str.length == min1) {// kí tự >= 6
                                ischeck = true;
                            }
                            else {//kí tự < 6
                                if (str.length == min2) {// kí tự >= 6
                                    ischeck = true;
                                }
                                else {//kí tự < 6
                                    this.common.appendError('Phải bằng ' + (min1) + ' hoặc ' + (min2) + ' kí tự', parent);
                                }
                            }
                        }
                        else {// có kí tự unicode
                            this.common.appendError('Không được có kí tự đặc biệt', parent);
                        }
                    }
                    else { // có khoảng trắng
                        this.common.appendError('Không được có khoảng trắng', parent);
                    }
                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        UserName4: function (obj, className, min1, min2, min3) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                        if (this.common.isnot_special(str)) {// không có kí tự unicode
                            if (this.common.is_Numeric(str)) {//Phải là số
                                if (str.length == min1) {// kí tự >= 6
                                    ischeck = true;
                                }
                                else {//kí tự < 6
                                    if (str.length == min2) {// kí tự >= 6
                                        ischeck = true;
                                    }
                                    else {//kí tự < 6
                                        if (str.length == min3) {// kí tự >= 6
                                            ischeck = true;
                                        }
                                        else {//kí tự < 6
                                            this.common.appendError('Phải bằng ' + (min1) + ' hoặc ' + (min2) + ' hoặc ' + (min3) + ' kí tự', parent);
                                        }
                                    }
                                }
                            } else {
                                this.common.appendError('Phải là chữ số', parent);
                            }
                        }
                        else {// có kí tự unicode
                            this.common.appendError('Không được có kí tự đặc biệt', parent);
                        }
                    }
                    else { // có khoảng trắng
                        this.common.appendError('Không được có khoảng trắng', parent);
                    }
                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        UserName5: function (obj, className, min1, min2, min3, min4) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (!this.common.hasWhiteSpace(str)) {// nếu không có khoảng trắng
                        if (this.common.isnot_special(str)) {// không có kí tự unicode
                            if (this.common.is_Numeric(str)) {//Phải là số
                                if (str.length == min1) {// kí tự >= 6
                                    ischeck = true;
                                }
                                else {//kí tự < 6
                                    if (str.length == min2) {// kí tự >= 6
                                        ischeck = true;
                                    }
                                    else {//kí tự < 6
                                        if (str.length == min3) {// kí tự >= 6
                                            ischeck = true;
                                        }
                                        else {//kí tự < 6
                                            if (str.length == min4) {// kí tự >= 6
                                                ischeck = true;
                                            } else {
                                                this.common.appendError('Phải bằng ' + (min1) + ' hoặc ' + (min2) + ' hoặc ' + (min3) + ' hoặc ' + (min4) + ' kí tự', parent);
                                            }
                                        }
                                    }
                                }
                            } else {
                                this.common.appendError('Phải là chữ số', parent);
                            }
                        }
                        else {// có kí tự unicode
                            this.common.appendError('Không được có kí tự đặc biệt', parent);
                        }
                    }
                    else { // có khoảng trắng
                        this.common.appendError('Không được có khoảng trắng', parent);
                    }
                }
                else {// có rỗng
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        FullName: function (obj, className, min) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.isnotSpecialChar(str)) {// không được có kí tự đặc biệt
                        if (str.length >= min) {// kí tự >= 6
                            ischeck = true;
                        }
                        else {//kí tự < 6
                            this.common.appendError('Phải lớn hơn hoặc bằng ' + min + ' kí tự', parent);
                        }
                    }
                    else {// có kí tự đặc biệt
                        this.common.appendError('Không được có kí tự đặc biệt', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        is_Date: function (obj, className, min, max) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (!isNaN(Date.parse(str))) {
                        var dt = new Date(str);
                        if ((dt.getFullYear() >= min) && (dt.getFullYear() <= max)) {
                            ischeck = true;
                        }
                        else {
                            this.common.appendError('Ngày sinh phải trong khoảng từ ' + (min) + ' đến ' + (max) + '', parent);
                        }
                    }
                    else {
                        this.common.appendError('Không đúng định dạng ngày tháng', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        Is_number: function (obj, className, min) {

            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.isnum(str)) {
                        if (parseFloat(str) > min) {
                            ischeck = true;
                        } else {
                            this.common.appendError('Phải lớn hơn ' + min, parent);
                        }
                    } else {
                        this.common.appendError('Dữ liệu phải là kiểu số tu nhien', parent);
                    }
                } else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },
        FullName_Special: function (obj, className, min) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (str.length >= min) {// kí tự >= 6
                        ischeck = true;
                    }
                    else {//kí tự < 6
                        this.common.appendError('Phải lớn hơn hoặc bằng ' + min + ' kí tự', parent);
                    }
                }
                else {
                    this.common.appendError('Không được để trống trường dữ liệu', parent);
                }
            }
            return ischeck;
        },

        //multi language
        
        CheckFiled_Requite: function (obj, className, min, max) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {
                    if (max == undefined) {
                        if (str.length >= min) {// kí tự >= 6
                            ischeck = true;
                        }
                        else {//kí tự < 6
                            //  this.common.appendError('Phải lớn hơn hoặc bằng ' + min + ' kí tự', parent);
                            this.common.appendError(ReturnLanguageJs(Mustbegreater, MustbegreaterEn) + min + ReturnLanguageJs(Characters, CharactersEn), parent);
                        }
                    } else {
                        if (str.length >= min && str.length <= max) {// kí tự >= min or <=max
                            ischeck = true;
                        }
                        else {
                            // this.common.appendError('Phải từ ' + min + ' đến ' + max + ' kí tự', parent);
                            this.common.appendError(ReturnLanguageJs(Mustbefrom, MustbefromEn) + min + ReturnLanguageJs(To, ToEn) + max + ReturnLanguageJs(Characters, CharactersEn), parent);
                        }
                    }
                }
                else {
                    this.common.appendError(ReturnLanguageJs(NotEmptyDataField, NotEmptyDataFieldEn), parent);
                }
            }
            return ischeck;
        },
        CheckField_NNonRequite: function (obj, className, min, max) {
            var ischeck = true;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (max != undefined) {
                        if (str.length >= min && str.length <= max) {// kí tự >= min or <=max
                            ischeck = true;
                        }
                        else {
                            ischeck = false;
                            //  this.common.appendError('Phải từ ' + min + ' đến ' + max + ' kí tự', parent);
                            this.common.appendError(ReturnLanguageJs(Mustbefrom, MustbefromEn) + min + ReturnLanguageJs(To, ToEn) + max + ReturnLanguageJs(Characters, CharactersEn), parent);
                        }
                    } else {
                        if (str.length >= min) {// kí tự >= min
                            ischeck = true;
                        }
                        else {//kí tự < min
                            ischeck = false;
                            //  this.common.appendError('Phải lớn hơn hoặc bằng ' + min + ' kí tự', parent);
                            this.common.appendError(ReturnLanguageJs(Mustbegreater, MustbegreaterEn) + min + ReturnLanguageJs(Characters, CharactersEn), parent);
                        }
                    }

                } else {
                    ischeck = true;
                }
            }
            return ischeck;
        },
        CheckField_CKEditor_Requite: function (obj, className, idCKEditor, min, max) {
            var ischeck = true;
            var str = this.common.trim(CKEDITOR.instances[idCKEditor].getData());
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {

                if (str.trim().length <= 59) {
                    ischeck = false;
                    //    this.common.appendError('Không được để trống trường dữ liệu ', parent);
                    this.common.appendError(ReturnLanguageJs(NotEmptyDataField, NotEmptyDataFieldEn), parent);
                }
            }
            return ischeck;
        },// ck cu chua co upload anh
        CheckField_CKEditor_Requite2: function (obj, className, idCKEditor, min, max) {
            var ischeck = true;
            var str = this.common.trim(CKEDITOR.instances[idCKEditor].getData());
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (str.trim().length <= 7) {
                    ischeck = false;
                    //  this.common.appendError('Không được để trống trường dữ liệu ', parent);
                    this.common.appendError(ReturnLanguageJs(NotEmptyDataField, NotEmptyDataFieldEn), parent);
                }
            }
            return ischeck;
        }, // ck moi co upload anh

        isURLNotRequite: function (obj, className) {
            var ischeck = true;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.validateURL(str)) {
                        ischeck = true;
                    }
                    else {
                        ischeck = false;
                        //    this.common.appendError('Không đúng định dạng link', parent);
                        this.common.appendError(ReturnLanguageJs(Incorrectlinkformat, IncorrectlinkformatEn), parent);
                    }
                }
            }
            return ischeck;
        },
        isIframeRequite: function (obj, className) {
            var ischeck = false;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (str.indexOf("iframe")) {
                        var len = str.match(/iframe+/g).length;
                        if (len == "2") {
                            ischeck = true;
                        } else {
                            //  this.common.appendError('Chỉ được thêm 1 video ', parent);
                            this.common.appendError(ReturnLanguageJs(OnlyOneVideo, OnlyOneVideoEn), parent);
                        }
                    } else {
                        // this.common.appendError('Không đúng định dạng', parent);
                        this.common.appendError(ReturnLanguageJs(Malformed, MalformedEn), parent);
                    }

                } else {
                    //  this.common.appendError('Không được để trống trường dữ liệu', parent);
                    this.common.appendError(ReturnLanguageJs(NotEmptyDataField, NotEmptyDataFieldEn), parent);
                }
            }
            return ischeck;
        },
        isFAXNotRequite: function (obj, className) {
            var ischeck = true;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str)) {// nếu không rỗng
                    if (this.common.validateFAX(str)) {
                        ischeck = true;
                    }
                    else {
                        ischeck = false;
                        // this.common.appendError('Không đúng định dạng fax', parent);
                        this.common.appendError(ReturnLanguageJs(Incorrectfaxformat, IncorrectfaxformatEn), parent);
                    }
                }
            }
            return ischeck;
        },

        CheckSelectCBX: function (obj, className) {
            var ischeck = true;
            var str = this.common.trim(obj.value);
            var parent = this.common.removeValidation(obj, className);
            if (parent != null) {
                if (this.common.isEmptyObject(str) || str == 0) {
                    ischeck = false;
                    //  this.common.appendError('Mời bạn lựa chọn', parent);
                    this.common.appendError(ReturnLanguageJs(Yourchoice, YourchoiceEn), parent);
                }
            }
            return ischeck;
        },
        CheckConfirmPassWord: function (obj, obj2, className) {
            var ischeck = true;

            var str = this.common.trim(obj.value);
            var str2 = this.common.trim(obj2.value);
            var parent = this.common.removeValidation(obj2, className);
            if (parent != null) {
                if (!this.common.isEmptyObject(str2)) {
                    if (str2 != str) {
                        ischeck = false;
                        // this.common.appendError('Nhập lại mật khẩu không đúng', parent);
                        this.common.appendError(ReturnLanguageJs(TryPassword, TryPasswordEn), parent);
                    }
                } else {
                    ischeck = false;
                    //  this.common.appendError('Mời bạn nhập lại mật khẩu', parent);
                    this.common.appendError(ReturnLanguageJs(EnterPAss, EnterPAssEn), parent);
                }
            }
            return ischeck;
        }
    },
    QTW_RUN_MESS = {
        alter_Message_Success: function (message) {
            swal(ReturnLanguageJs(Notification, NotificationEn), message, "success");
    }, alter_Message_Success_to_Homepage: function (message, callback) {
            swal({
                    title: ReturnLanguageJs(Notification, NotificationEn),
                    text: message,
                    type: "success",
                    showCancelButton: false,
                    confirmButtonColor: "#65f442",
                    confirmButtonText: "OK",                    
                    closeOnConfirm: false
                },
                function check(kq) {
                    if (kq) {
                        callback();
                    }
                });
        },
       
        alter_Message_Error: function (message) {
            swal(ReturnLanguageJs(Notification, NotificationEn), message, "error");
        },
        alter_Message_Warning: function (message) {
            swal(ReturnLanguageJs(Notification, NotificationEn), message, "warning");
        },
        alter_Message_AutoClose: function (message) {
            swal({
                title: message,
                text: ReturnLanguageJs(Transferafter, TransferafterEn),
                timer: 3000,
                showConfirmButton: false
            });
        },
        alter_Message_Question_Callbaclk: function (message, callback) {
            swal({
                title: ReturnLanguageJs(Notification, NotificationEn),
                text: message,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: ReturnLanguageJs(Yes_agree, Yes_agreeEn),
                cancelButtonText: ReturnLanguageJs(No_Thank, No_ThankEn),
                closeOnConfirm: false
            },
                function check(kq) {
                    if (kq) {
                        callback();
                    }
                });
        }
    },
    QTW_JQUERY = {
        RUN_AJAX: function (type, url, dataPost, handleCallback, param) {
            createLoading();

            switch (type) {
                case 'ajaxPOST':
                    $.ajax({
                        type: "POST",
                        url: url,
                        data: dataPost,
                        processData: false,
                        contentType: false,
                        success: function (response, textStatus, xhr) {
                            removeLoading();
                            handleCallback(response);
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            removeLoading();
                            //QTW_VAR.complate = true;
                            console.log("error");
                        }
                    });
                    break;
                case 'getJSON':
                    if (param) {
                        $.ajaxSetup({
                            async: param.async
                        });
                    }
                    $.getJSON(url, dataPOST, handleCallback);
                    break;
                case 'postFile':
                    $.ajax({
                        type: "POST",
                        url: url,
                        data: dataPOST,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (response, textStatus, xhr) {
                            removeLoading();
                            handleCallback(JSON.parse(response));
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            removeLoading();
                            //QTW_VAR.complate = true;
                            console.log("error");
                        }
                    });
                    break;
            }

        },
        getInnerHtml: {
            element: null,
            elementX2: null,
            innerHTML: '',
            get: function (options) {
                var _ = this;
                if (options) {
                    if (options.element) {
                        _.element = options.element;
                        _.fixedValue();
                        _.elementX2 = _.element.cloneNode(true);
                        _.innerHTML = _.elementX2.innerHTML;
                    }
                }
                return _.elementX2;
            },
            fixedValue: function () {
                var _ = this;
                var inputs = _.element.querySelectorAll('input');
                var selects = _.element.querySelectorAll('select');
                for (i = 0; i < inputs.length; i++) {
                    var input = inputs[i];
                    if (input.type == 'radio' || input.type == 'checkbox') {
                        if (input.checked) {
                            input.setAttribute('checked', 'checked');
                        }
                        else {
                            input.removeAttribute('checked');
                        }
                    }
                    else {
                        input.setAttribute('value', input.value);
                    }
                }
                for (i = 0; i < selects.length; i++) {
                    var select = selects[i];
                    for (ii = 0; ii < select.options.length; ii++) {
                        var option = select.options[ii];
                        if (option.selected) {
                            option.setAttribute('selected', true);
                        }
                        else {
                            option.removeAttribute('selected');
                        }
                    }
                }
            }
        },
        fmDate: function (str) {
            var today = new Date(str.replace(/(\d{1,2})\/(\d{1,2})\/(\d{4})/, "$2/$1/$3"));
            var dd = today.getDate();
            var mm = today.getMonth() + 1;//January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            return dd + '/' + mm + '/' + yyyy
        },
        TaoSelect: function (select, data, firstchild) {
            select.innerHTML = null;
            var option;
            if (typeof firstchild === 'object') {
                option = document.createElement('option');
                option.value = firstchild.value;
                option.innerHTML = firstchild.name;
                select.append(option);
            }
            for (i = 0; i < data.length; i++) {
                var item = data[i];
                option = document.createElement('option');
                option.value = item.id;
                option.innerHTML = item.name;
                option.setAttribute('data-db', JSON.stringify(item));
                if (typeof firstchild === 'object') {
                    if (firstchild.idselect) {
                        if (String(item.id) == String(firstchild.idselect)) {
                            option.selected = true;
                        }
                    }
                }
                select.append(option);
            }
        },
        resetData: function (element) {
            debugger
            var parent = element.parentNode;
            var form = document.createElement('form');
            parent.replaceChild(form, element);
            form.appendChild(element);
            form.reset();
            parent = form.parentNode;
            parent.removeChild(form);
            parent.appendChild(element);
        }
    },

   //multi language


    addPreload = function () {
        var cicle = document.getElementById('preload');
        if (cicle) {
            cicle.parentNode.removeChild(cicle);
        }
        cicle = document.createElement('div');
        cicle.id = 'preload';
        cicle.innerHTML = '<div id="circle1"></div>';
        return cicle;
    };
ready = function () {
    Array.from(document.querySelectorAll('.uk-date')).forEach(function (ip) {
        $(ip).datetimepicker({
            timepicker: false,
            lang: 'vi',
            mask: false, // '9999/19/39 29:59' - digit is the maximum possible for a cell
            format: 'd/m/Y',
            roundTime: 'round', // ceil, floor
        });
    });
    window.addEventListener('keydown', function (e) {
        if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
            if (e.target.nodeName == 'INPUT' && e.target.type == 'text') {
                e.preventDefault();
                return false;
            }
        }
    }, true);
    //dichvucong_socket.sendToServer({ type: 'start' });
};
setDate = function () {
    Array.from(document.querySelectorAll('.uk-date')).forEach(function (ip) {
        $(ip).datetimepicker({
            timepicker: false,
            lang: 'vi',
            mask: true, // '9999/19/39 29:59' - digit is the maximum possible for a cell
            format: 'd/m/Y',
            roundTime: 'round', // ceil, floor
        });
    });
}
bytesToSize = function (bytes) {
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes == 0) return '0 Byte';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
};
clearInputFile = function (f) {
    if (f.value) {
        try {
            f.value = ''; //for IE11, latest Chrome/Firefox/Opera...
        } catch (err) { }
        if (f.value) { //for IE5 ~ IE10
            var form = document.createElement('form'),
                parentNode = f.parentNode, ref = f.nextSibling;
            form.appendChild(f);
            form.reset();
            parentNode.insertBefore(f, ref);
        }
    }
};
IsNullOrEmptyString = function (value) {
    var isEmptyObject = function (a) {
        if (typeof a.length === 'undefined') { // it's an Object, not an Array
            var hasNonempty = Object.keys(a).some(function nonEmpty(element) {
                return !IsNullOrEmptyString(a[element]);
            });
            return hasNonempty ? false : isEmptyObject(Object.keys(a));
        }
        return !a.some(function nonEmpty(element) { // check if array is really not empty as JS thinks
            return !IsNullOrEmptyString(element); // at least one element should be non-empty
        });
    };
    return (
        value == false
        || typeof value === 'undefined'
        || value == null
        || (typeof value === 'object' && isEmptyObject(value))
    );
};


setStyleInputTable = function (node) {
    var fnFilter = node.querySelector('input');
    node.parentNode.style.display = 'inline-block';
    node.parentNode.style.float = 'right';
    fnFilter.setAttribute('class', 'select1 form-control inputSearch');
    return $(fnFilter);
};
setStyleSelectTable = function (node) {
    node.parentNode.style.display = 'inline-block';
    node.parentNode.style.float = 'left';
    var fnPageChange = node.querySelector('select');
    fnPageChange.setAttribute('class', 'select1');
    return $(fnPageChange);
};


QTW_PROTOTYPE();


var createLoading = function () {
    $('.preloader').css('display', 'block');
}
var removeLoading = function () {
    $('.preloader').css('display', 'none');
}

//$(function () {
//    var t = "";
//    var protocol = window.location.protocol;
//
//    if (protocol == "http:") {
//        t = new WebSocket("ws://" + window.location.host + "/SocketUser/SocketProcessRequest");
//    } else {
//        t = new WebSocket("wss://" + window.location.host + "/SocketUser/SocketProcessRequest");
//    }
//
//    t.onmessage = function (t) {
//        var e = JSON.parse(t.data);
//        if (e.newlogin) {
//            swal({
//                title: ReturnLanguageJs(Accountsignedsomewhere, AccountsignedsomewhereEn),
//                text: ReturnLanguageJs(ExxitAfter2s, ExxitAfter2sEn),
//                timer: 2000,
//                showConfirmButton: false
//            });
//
//            $.getJSON("/Admin/LogoutAllTab"), function (t) {
//                t.success ? swal(ReturnLanguageJs(Notification, NotificationEn), t.msg, "success") : swal(ReturnLanguageJs(Notification, NotificationEn), ReturnLanguageJs(Err_operation, Err_operationEn), "error")
//            };
//        }
//
//        if (e.logout) {
//            window.location.reload();
//        }
//        if (e.pageLoad) {
//            (window.location.href = window.location.origin + "/Login/Index");
//        }
//        if (e.coppySessionId) {
//            swal(ReturnLanguageJs(Notification, NotificationEn), ReturnLanguageJs(Accountunderattack, AccountunderattackEn), "warning")
//        }
//        if (e.coppySessionIdFail) {
//            setTimeout(3000, swal(ReturnLanguageJs(Notification, NotificationEn), ReturnLanguageJs(Invalidaccess, InvalidaccessEn), "warning"));
//        }
//
//    }
//})