/// <reference path="jquery-1.7.1.min.js" />

/*Easy通用JS插件包
方法说明 以下方法均在Easy命名空间下
↘ Processor(); 函数节流（禁止函数因频繁运行而影响性能）
↘ LoadScript(); 动态加载JavaScript
↘ LoadStyle(); 动态加载CSS样式
↘ GetIframeDocument(); 获取对应ID的iframe的document对象
↘ WindowSize();例:WindowSize().width;WindowSize().height 获取窗体大小
↘ MousePosition(e)  例:MousePosition(e).X;MousePosition(e).Y 获取鼠标坐标
↘ Copy_url(); 复制当前地址仅适用与IE与FirFox;
↘ BusyIndicator 动画加载(两种样式)
↘ OpacityBackGround 半透明遮罩层
↘ ShowMessageBox(title, msg, fnOk, ShowCancel, zindex) 弹出提示窗口
↘ MessageTip.Show(msg); 显示消息框
↘ Cookie 设置和获取Cookie
↘ GetScroll()  例：getScroll().top getScroll().left getScroll().width getScroll().height 获取滚动条相关信息
↘ GetFieldSelection(id)  获取选中的文本
↘ ConfirmExit(Exmsg);  关闭或跳转页面前弹出提示 Exmsg：提示消息
↘ UnAbleToSelectText(); 禁止鼠标选择文本
↘ NoRightMenu();屏蔽右键菜单
↘ GetRandomColor() 获取随机颜色
↘ Easy.ShowModalWindow(); 打开模式窗口
↘ ShowUrlWindow(); 打开窗口
///以下在Easy.Share名空间下
↘ BaiDuZone(); 分享到百度空间
↘ Qzone();腾讯空间
↘ TencentWeiBo();腾讯微博
↘ SinaWeiBo(); 新浪微博

///以下在Easy.Check名空间下
↘ CheckIE(); 检测IE版本;
↘ CanPngOpacity()  验证是否可对半透明PNG图片，进行透明处理
↘ IsSpaceOnly(str) 输入是否为空字符串
↘ IsDigit(s)  校验是否全由数字组成
↘ IsUserName(s)  校验登录名：只能输入5-20个以字母开头、可带数字、“_”、“.”的字串
↘ IsNickName(s)  校验用户姓名：只能输入1-30个以字母开头的字串
↘ IsPasswd(s)  校验密码：只能输入6-20个字母、数字、下划线
↘ IsTel(s)  校验普通电话、传真号码：可以“+”开头，除数字外，可含有“-”
↘ IsMobil(s)  校验手机号码：必须以数字开头，除数字外，可含有“-”
↘ IsPostalCode(s)  校验邮政编码
↘ IsIP(s)  校验IP
↘ IsEmail(str) 校验电子邮箱
↘ IsChinese(str) 中文验证

///以下在Easy.EventUtil名空间下
↘ AddEventListener() 添加事件
↘ RemoveEventListener()  移除事件
↘ GetTarget() 获取引发事件对象
↘ PrEventDefault() 阻止事件引发
↘ StopPropagation() 阻止事件冒泡


///以下是jQuery扩展方法
↘ $("#textbox").BeRequired()  校验必填
↘ $("#textbox").BeNumber()  校验纯数字+0
↘ $("#textbox").BeEmail()  校验Email格式
↘ $("#textbox").BeUserName() 校验用户名格式，只能输入5-20个以字母开头、可带数字、“_”、“.”的字串
↘ $("#textbox").BeNickName()  校验用户姓名：只能输入1-30个以字母开头的字串
↘ $("#textbox").BePassWord()  校验密码：只能输入6-20个字母、数字、下划线
↘ $("#textbox").BeTelephone() 校验普通电话、传真号码：可以“+”开头，除数字外，可含有“-”
↘ $("#textbox").BeMobelPhone() 校验手机号码：必须以数字开头，除数字外，可含有“-”
↘ $("#textbox").BeChinese()  中文验证
↘ $("#textbox").BeCheckPassWord("#PassWord_1_ID")  验证textbox里的二次输入的密码，是否与原密码PassWord_1_ID里的一致
↘ $("#textbox").BeIpAddress()  验证IP地址格式
↘ $("#form").BeCheckSubmit()  前交前验证

↘ $("#ID").AutoChangeImage(da,ImgWidth,ImgHeight); 只淡出，适合做背景切换
↘ $("#ID").BackGroundAnimation(da);只淡出，适合做背景切换
↘ $("#box").DragElement();  这样box就可以拖动了;
↘ $("#box").FocusImages(data);  将box变为焦点图;
↘ $("#about-text").mousewheel(function (e, delta) {$(this).scrollTop($(this).scrollTop() - delta * 10);}); 鼠标滚轮事件
↘ $("#bar").VerticalScrollBar("#ContentID"); 垂直滚动条自动添加鼠标滚轮事件bar不用嵌套元素将自动添加一个样式为"Vertical-bar"的DIV
↘ $("#imgBox").AutoScroll(); 鼠标移动，滚动内容查看
↘ $("#MarqueeH").MarqueeH(); 水平跑马灯
↘ $("#MarqueeV").MarqueeV(); 垂直跑马灯
↘ $("#Box").PopUpShow(); 自小变大显示
↘ $("#Box").PopDownClose(); 自大变小关闭
↘ $("#Menu").AnimateMenu(); 动画抽屉菜单
↘ $("#img").ImageMagnifier(); 放大镜查看图片
↘ $("#img").ImageCenter_Adapt(); 完全居中显示图片，缩小图片以适应
↘ $("#img").ImageCenter_Full(); 完全居中显示图片，放大图片以适应
↘ $("#img").ImagePreview();放大图片进行预览
↘ $("#img").ImageShow();弹出框查看原图
↘ $("#Div").HtmlInput();html输入框
*/

Array.prototype.DelbyIndex = function (index) {
    /// <summary>按索引删除</summary>
    /// <param name="index" type="Int">索引</param>
    /// <returns type="Array" />
    if (index < 0)
        return this;
    else
        return this.slice(0, index).concat(this.slice(index + 1, this.length));
}

Array.prototype.DelbyValue = function (val) {
    /// <summary>按值删除</summary>
    /// <param name="val" type="Object">要删除的值</param>
    /// <returns type="Array" />
    var index = -1;
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val)
            index = i;
    }
    if (index == -1)
        return this;
    else return this.slice(0, index).concat(this.slice(index + 1, this.length));
}
Array.prototype.ContainsValue = function (val) {
    /// <summary>是否包含某值</summary>
    /// <param name="val" type="Object">要查询的值</param>
    /// <returns type="Boolean" />
    var index = -1;
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val)
            index = i;
    }
    if (index == -1)
        return false;
    else return true;
}
Array.prototype.ValueIndex = function (val) {
    /// <summary>是否包含某值</summary>
    /// <param name="val" type="Object">要查询的值</param>
    /// <returns type="Int" />
    var index = -1;
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) {
            index = i;
            break;
        }
    }
    return index;
}
String.prototype.replaceAll = function (oldValue, newVlaue) {
    var str = this;
    while (str.indexOf(oldValue, 0) >= 0) {
        str = str.replace(oldValue, newVlaue);
    }
    return str;
}
var Easy = {};
Easy.checks = new Array();
Easy.MaxZindex = 1;
Easy.Processor = function (fun, delay) {
    /// <summary>函数节流（禁止函数因频繁运行而影响性能）</summary>
    /// <param name="fun" type="Function">要推迟执行的函数</param>
    /// <param name="delay" type="Int">推迟执行的时间</param>
    clearTimeout(fun.tid);
    if (typeof delay != "number")
        delay = 60;
    fun.tid = setTimeout(function () { if (fun) fun.call(); }, delay);
}
Easy.LoadScript = function (url, callBack) {
    /// <summary>动态加载JavaScript</summary>
    /// <param name="url" type="String">JavaScript地址</param>
    /// <param name="callBack" type="Function">加载完成后引发回调</param>
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = url;
    Easy.EventUtil.AddEventListener(script, "load", function () { if (callBack) callBack.call(); });
    document.body.appendChild(script);
}
Easy.LoadStyle = function (url) {
    /// <summary>动态加载CSS样式</summary>
    /// <param name="url" type="String">CSS样式地址</param>
    var link = document.createElement("link");
    link.rel = "stylesheet";
    link.type = "text/css";
    link.href = url;
    document.getElementsByTagName("head")[0].appendChild(link);
}
Easy.GetIframeDocument = function (IframeID) {
    /// <summary>获取对应ID的iframe的document对象</summary>
    /// <param name="IframeID" type="String">iframe的id</param>
    /// <returns type="Document" />
    if (typeof IframeID != "string") {
        alert("GetIframeDocument：请传入Iframe的ID")
        return false;
    }
    var ifr = document.getElementById(IframeID);
    if (!ifr) {
        alert("GetIframeDocument()：没有找到对应的iframe");
        return false;
    }
    else return ifr.contentWindow.document;
}
Easy.WindowSize = function () {
    /// <summary>浏览器窗体大小</summary>
    var w, h;
    if (!!(window.attachEvent && !window.opera)) {
        h = document.documentElement.clientHeight;
        w = document.documentElement.clientWidth;
    } else {
        h = window.innerHeight;
        w = window.innerWidth;
    }
    return { width: w, height: h };
}

Easy.NoRightMenu = function () {
    /// <summary>屏蔽右键菜单</summary>
    $(document).bind("contextmenu", function () { return false; });
}
Easy.EventUtil = (function () {
    function BindEvent(Element, EventName, fun) {
        /// <summary>为指定的Com对象绑定事件</summary>
        /// <param name="Element" type="DOMElement">要绑定到的对象</param>
        /// <param name="EventName" type="String">事件名称，例：click</param>
        /// <param name="fun" type="String">响应的事件</param>
        if (Element.attachEvent) {
            Element.attachEvent("on" + EventName, fun);
        } else if (Element.addEventListener) {
            Element.addEventListener(EventName, fun, false);
        }
        else {
            Element["on" + EventName] = fun;
        }
    }

    function UnBindEvent(Element, EventName, fun) {
        /// <summary>解除绑定事件</summary>
        /// <param name="Element" type="DOMElement">要解除事件的对象</param>
        /// <param name="EventName" type="String">事件名称,例：click</param>
        if (Element.detachEvent) {
            Element.detachEvent("on" + EventName, fun);
        }
        else if (Element.removeEventListener) {
            Element.removeEventListener(EventName, fun, false);
        }
        else {
            Element["on" + EventName] = null;
        }
    }
    function GetTarget(event) {
        /// <summary>获取引发事件对象</summary>
        /// <param name="event" type="Event">event对象</param>
        return event.target || event.srcElement;
    }
    function PrEventDefault(event) {
        /// <summary>阻止事件引发</summary>
        /// <param name="event" type="Event">event对象</param>
        if (event.preventDefault)
            event.preventDefault();
        else event.returnValue = false;
    }
    function StopPropagation(event) {
        /// <summary>阻止事件冒泡</summary>
        /// <param name="event" type="Event">event对象</param>
        if (event.stopPropagation)
            event.stopPropagation();
        else event.cancelBubble = true;
    }
    return { AddEventListener: BindEvent, RemoveEventListener: UnBindEvent, GetTarget: GetTarget, PrEventDefault: PrEventDefault, StopPropagation: StopPropagation }
})();

Easy.Cookie = (function () {
    /// <summary>设置Cookie</summary>
    function SetCookie(c_name, value, expiredays) {
        /// <summary>设置Cookie</summary>
        /// <param name="c_name" type="String">Cookie的名称</param>
        /// <param name="value" type="String">Cookie值</param>
        /// <param name="expiredays" type="Int">有效天数</param>
        var exdate = new Date()
        exdate.setDate(exdate.getDate() + expiredays)
        document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString()) + ";path=/;";
    }

    function GetCookie(c_name) {
        /// <summary>获取Cookie值</summary>
        /// <param name="c_name" type="String">Cookie的名称</param>
        /// <returns type="String" />
        if (document.cookie.length > 0) {
            c_start = document.cookie.indexOf(c_name + "=")
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1
                c_end = document.cookie.indexOf(";", c_start)
                if (c_end == -1) c_end = document.cookie.length
                return unescape(document.cookie.substring(c_start, c_end))
            }
        }
        else return ""
    }
    function DeleteCookie(c_name) {
        if (GetCookie(c_name)) {
            document.cookie = c_name + "=" +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
        }
    }
    return { SetCookie: SetCookie, GetCookie: GetCookie, DeleteCookie: DeleteCookie }
})();

Easy.AddFavorite = function () {
    /// <summary>添加收藏</summary>
    window.external.AddFavorite(window.location.href, document.title);
}

Easy.SetHomePage = function () {
    /// <summary>设置主页</summary>
    window.setHomePage(window.location.href);
}

Easy.Copy_url = function () {
    /// <summary>复制URL地址，当前地址</summary>
    var clipBoardContent = window.location.href;
    if (window.clipboardData) {
        window.clipboardData.setData("Text", clipBoardContent);
        return true;
    }
    else if (window.netscape) {
        netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip)
            return false;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans)
            return false;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = clipBoardContent;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip)
            return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
        return true;
    }
    else {
        return false;
    }
}
Easy.Share = (function () {
    /// <summary>分享到各大空间</summary>
    function ShareToQzone() {
        /// <summary>分享到QQ空间</summary>
        var p = {
            url: window.location.href,
            desc: '', /*默认分享理由(可选)*/
            summary: '', /*摘要(可选)*/
            title: document.title, /*分享标题(可选)*/
            site: '', /*分享来源 如：腾讯网(可选)*/
            pics: '' /*分享图片的路径(可选)*/
        };
        var s = [];
        for (var i in p) {
            s.push(i + '=' + encodeURIComponent(p[i] || ''));
        }
        Easy.ShowModalWindow("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?" + s.join('&'), 577, 529);
    }
    function ShareToSinaWBo() {
        /// <summary>分享到新浪微博</summary>
        var title = encodeURIComponent(document.title);
        var url = encodeURIComponent(window.location.href);
        Easy.ShowModalWindow("http://service.weibo.com/share/share.php?title=" + title + "&url=" + url + "&pic=&ralateUid=", 634, 386);
    }
    function ShareToQQWBo() {
        /// <summary>分享到QQ微博</summary>
        var title = encodeURIComponent(document.title);
        var url = encodeURIComponent(window.location.href);
        var site = title;
        Easy.ShowModalWindow("http://v.t.qq.com/share/share.php?title=" + title + "&url=" + url + "&site=" + site + "&pic=", 632, 706);
    }
    function ShareToBaidu() {
        /// <summary>分享到百度空间</summary>
        var title = encodeURIComponent(document.title);
        var url = encodeURIComponent(window.location.href);
        Easy.ShowModalWindow("http://apps.hi.baidu.com/share/?url=" + url + "&title=" + title, 830, 602);
    }
    return { "Qzone": ShareToQzone, "SinaWeiBo": ShareToSinaWBo, "TencentWeiBo": ShareToQQWBo, "BaiDuZone": ShareToBaidu }
})();

Easy.ShowModalWindow = function (url, width, height) {
    /// <summary>弹出模式窗口</summary>
    /// <param name="url" type="String">请求的URL地址</param>
    /// <param name="width" type="Int">窗口宽度</param>
    /// <param name="height" type="Int">窗口高度</param>

    if (typeof width != "number")
        width = 300;
    if (typeof height != "number")
        height = 300;
    try {
        window.showModalDialog(url, "", "dialogWidth:" + width + "px;dialogHeight:" + height + "px;scroll:no;status:no");
    }
    catch (e) {
        window.open(url);
    }
}

Easy.GetFieldSelection = function (id) {
    /// <summary>获取对应ID的Com对象的选中文本</summary>
    /// <param name="id" type="String">目标对象的id</param>
    var select_field = document.getElementById(id);
    if (document.selection) {
        var sel = document.selection.createRange();
        if (sel.text.length > 0) {
            word = sel.text;
        }
    }    /*ie浏览器*/
    else if (select_field.selectionStart || select_field.selectionStart == '0') {
        var startP = select_field.selectionStart;
        var endP = select_field.selectionEnd;
        if (startP != endP) {
            word = select_field.value.substring(startP, endP);
        }
    }   /*标准浏览器*/
    return word;
}

Easy.ConfirmExit = function (Exmsg) {
    /// <summary>离开页面确认</summary>
    /// <param name="Exmsg" type="String">提示的消息</param>
    window.onbeforeunload = function confExit() {
        if (typeof Exmsg != "string")
            return "确认要离开吗？";
        else return Exmsg.toString();
    }
}
Easy.UnAbleToSelectText = function () {
    /// <summary>禁止鼠标选择文本</summary>
    var omitformtags = ["input", "textarea", "select"]
    omitformtags = omitformtags.join("|")
    function disableselect(e) {
        if (omitformtags.indexOf(e.target.tagName.toLowerCase()) == -1)
            return false
    }
    function reEnable() {
        return true
    }
    if (typeof document.onselectstart != "undefined")
        document.onselectstart = new Function("return false")
    else {
        document.onmousedown = disableselect
        document.onmouseup = reEnable
    }
}

Easy.GetRandomColor = function () {
    /// <summary>获取随机颜色</summary>
    var arr = []
    i = 0;
    C = '0123456789ABCDEF';
    while (i++ < 6) {
        x = Math.random() * 16;
        b = parseInt(x);
        c = C.substr(b, 1);
        arr.push(c);
    }
    var cl = "#" + arr.join('');
    return cl;
}



Easy.MousePosition = function (e) {
    /// <summary>获取鼠标坐标(接用jQuery事件)</summary>
    /// <param name="e" type="jQuery">jQuery事件e对象</param>
    var x, y;
    var eX = e || window.event;
    x = eX.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
    y = eX.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    return { X: x, Y: y }

}

Easy.GetScroll = function () {
    /// <summary>获取滚动条相关信息left top width height</summary>
    var t, l, w, h;
    if (document.documentElement && document.documentElement.scrollTop) {
        t = document.documentElement.scrollTop;
        l = document.documentElement.scrollLeft;
        w = document.documentElement.scrollWidth;
        h = document.documentElement.scrollHeight;

    } else if (document.body) {
        t = document.body.scrollTop;
        l = document.body.scrollLeft;
        w = document.body.scrollWidth;
        h = document.body.scrollHeight;
    }
    return { top: t, left: l, width: w, height: h };
}

Easy.Check = (function () {
    function IsSpaceOnly(str) {
        /// <summary>验证输入是否为空字符串</summary>
        /// <param name="str" type="String">要验证的字符串</param>
        if (str.replace(/\s+/g, "") == "")
            return true;
        else return false;
    }
    function IsDigit(s) {
        /// <summary>校验是否全由数字组成</summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[0-9]{1,20}$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsUserName(s) {
        /// <summary>校验登录名：只能输入5-20个以字母开头、可带数字、“_”、“.”的字串</summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsNickName(s) {
        /// <summary>校验用户姓名：只能输入1-30个以字母开头的字串 </summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[a-zA-Z]{1,30}$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsPasswd(s) {
        /// <summary>校验密码：只能输入6-20个字母、数字、下划线</summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^(\w){6,20}$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsTel(s) {
        /// <summary>校验普通电话、传真号码：可以“+”开头，除数字外，可含有“-” </summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsMobil(s) {
        /// <summary>校验手机号码：必须以数字开头，除数字外，可含有“-” </summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsPostalCode(s) {
        /// <summary>校验邮政编码</summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[a-zA-Z0-9 ]{3,12}$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsIP(s) {
        /// <summary>校验IP</summary>
        /// <param name="s" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/);
        if (!patrn.exec(s))
            return false
        else return true
    }
    function IsEmail(str) {
        /// <summary>校验电子邮箱</summary>
        /// <param name="str" type="String">要验证的字符串</param>
        if (str.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
            return true;
        else return false;
    }
    function IsChinese(str) {
        /// <summary>中文验证</summary>
        /// <param name="str" type="String">要验证的字符串</param>
        var patrn = new RegExp(/^[\u4e00-\u9fa5]{1,5}$/);
        if (!patrn.test(str))
            return false;
        else return true;
    }
    function GetErrorBox(clas, text) {
        var ele = $("<div class='" + clas + "' style='position:absolute;left:0px;'><div class='errorTop'></div><div class='errorCen'><span>" + text + "</span></div><div class='errorBottom'></div></div>");
        ele.click(function () {

            $(this).remove();
        });
        return ele;
    }

    function IsCanSubmit() {
        /// <summary>是否可以提交，即有没有符合验证</summary>
        for (var i = 0; i < Easy.checks.length; i++) {
            Easy.checks[i].triggerHandler('blur');
        }
        if (document.getElementsByClassName("RequireError").length != 0)
            return false;
        else if (document.getElementsByClassName("DigitError").length != 0)
            return false;
        else if (document.getElementsByClassName("EmailError").length != 0)
            return false;
        else if (document.getElementsByClassName("UserNameError").length != 0)
            return false;
        else if (document.getElementsByClassName("NickNameError").length != 0)
            return false;
        else if (document.getElementsByClassName("PassWordError").length != 0)
            return false;
        else if (document.getElementsByClassName("TelePhoneError").length != 0)
            return false;
        else if (document.getElementsByClassName("MobelPhomeError").length != 0)
            return false;
        else if (document.getElementsByClassName("PassWordSameError").length != 0)
            return false;
        else if (document.getElementsByClassName("IpAddressError").length != 0)
            return false;
        else { return true; }
    }

    function CheckIE() {
        /// <summary>检测IE版本</summary>
        if (IsIE6()) {
            alert("您正在使用IE6内核的浏览器访问该网站，可能导致显示不正常！请升级您的IE以提升您的电脑性能！");
            if (confirm("希望自动下载IE8吗？")) {
                window.location.href = "http://download.microsoft.com/download/1/6/1/16174D37-73C1-4F76-A305-902E9D32BAC9/IE8-WindowsXP-x86-CHS.exe";
            }
        }
    }
    function IsIE6() {
        /// <summary>检测IE6</summary>
        if (window.ActiveXObject) {
            var browser = navigator.appName;
            var b_version = navigator.appVersion;
            var version = b_version.split(";");
            var trim_Version = version[1].replace(/[ ]/g, "");
            if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE6.0") {
                return true;
            }
            else return false;
        }
        else return false;
    }

    function CanPngOpacity() {
        /// <summary>验证是否可对半透明PNG图片，进行透明处理</summary>
        /// <returns type="Boolean" />
        if (window.ActiveXObject) {
            var browser = navigator.appName;
            var b_version = navigator.appVersion;
            var version = b_version.split(";");
            var trim_Version = version[1].replace(/[ ]/g, "");
            if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0") {
                return true;
            }
            else return false;
        }
        else return true;
    }
    return { IsIE6: IsIE6, IsSpaceOnly: IsSpaceOnly, IsDigit: IsDigit, IsUserName: IsUserName, IsNickName: IsNickName, IsPasswd: IsPasswd, IsTel: IsTel, IsMobil: IsMobil, IsPostalCode: IsPostalCode, IsIP: IsIP, IsEmail: IsEmail, IsChinese: IsChinese, GetErrorBox: GetErrorBox, IsCanSubmit: IsCanSubmit, CheckIE: CheckIE, CanPngOpacity: CanPngOpacity }
})();

Easy.OpacityBackGround = (function () {
    var bgs = new Array();
    var opa = 0.5;
    function ShowOpactiyBg(zindex, callBack) {
        /// <summary>显示半透明遮罩层</summary>
        /// <param name="zindex" type="Int">z方向层次</param>
        /// <param name="callBack" type="Function">完成后的回调函数</param>

        if (typeof zindex != "number")
            zindex = 1;
        var Ele = $("<div id='SeriaWeiOBg' class='OpacityBackGround'></div>");
        if (Easy.Check.IsIE6()) {
            Ele.addClass("IE6Fixed");
        }
        else {
            Ele.addClass("OtherFixed");
        }
        Ele.attr("id", "SeriaWeiOBg" + bgs.length);
        bgs.push("#" + Ele.attr("id"));
        $("body").append(Ele);
        Ele.css("z-index", zindex);
        Ele.fadeTo(0, 0);
        Ele.fadeTo(300, opa, function () {
            if (callBack) callBack.call();
        });
    }
    function CloseOpacityBg(callBack) {
        /// <summary>关闭最近一次添加的半透明遮罩层</summary>
        /// <param name="callBack" type="Function">完成后的回调函数</param>
        var Bele = $(bgs[bgs.length - 1]);
        bgs = bgs.DelbyIndex(bgs.length - 1);
        Bele.fadeTo(300, 0, function () { $(this).remove(); if (callBack) callBack.call(); });
    }
    function SetOpacity(opacity) {
        /// <summary>设置半透明度</summary>
        /// <param name="opacity" type="Double">透明度</param>
        opa = opacity;
    }
    return { Show: ShowOpactiyBg, Close: CloseOpacityBg, SetOpacity: SetOpacity }
})();
Easy.BusyIndicator = (function () {
    /// <summary>显示忙碌加载动画，显示LOADER</summary>


})();



Easy.ShowMessageBox = function (title, msg, fnOk, ShowCancel, zindex) {
    /// <summary>弹出消息提示窗口</summary>
    /// <param name="title" type="String">显示的标题</param>
    /// <param name="msg" type="String">消息内容</param>
    /// <param name="fnOk" type="Function">单击确定时的回调函数</param>
    /// <param name="ShowCancel" type="Boolean">是否显示取消按钮</param>
    /// <param name="zindex" type="Int">z方向的层次</param>
    var box = $("<div class='MessageBox BoxShadow'>" +
            "<div class='MBContent'>" +
                "<div id='MessageBoxTitle' class='MBTitle'></div>" +
                "<div id='MessageBoxMsg' class='MBMSgText'></div>" +
                "<div class='MBFoot'>" +
                    "<div id='MessageBoxActions' class='MBActions'></div>" +
                    "<div style='clear: both'></div></div></div></div>");
    box.find("#MessageBoxMsg").html(msg);
    box.find("#MessageBoxTitle").html(title);
    if (!Easy.Check.IsIE6()) {
        box.css("position", "fixed");
    }
    if (typeof zindex != "number")
        zindex = 1;
    box.css("z-index", zindex);
    box.fadeTo(0, 0);
    var okButton = $("<input id='MessageBoxOkBtn' type='button' class='btn btn-danger btn-xs' value='确定' />");
    box.find("#MessageBoxActions").append(okButton);
    okButton.click(function () {
        if (fnOk) {
            fnOk.call();
        }
        Easy.OpacityBackGround.Close();
        box.animate({ top: "45%", opacity: 0 }, 200, function () { $(this).remove(); });
    });
    if (typeof ShowCancel == "boolean") {
        if (ShowCancel) {
            var CancelButton = $("<input id='MessageBoxCancelBtn' type='button' class='btn btn-default btn-xs' value='取消' />");
            CancelButton.click(function () {
                Easy.OpacityBackGround.Close();
                box.animate({ top: "45%", opacity: 0 }, 200, function () { $(this).remove(); });
            });
            box.find("#MessageBoxActions").append(CancelButton);
        }
    }
    Easy.OpacityBackGround.Show(zindex);
    $("body").append(box);
    box.animate({ top: "50%", opacity: 1 }, 200, function () { box.find("#MessageBoxOkBtn").focus(); });
}

Easy.MessageTip = (function () {
    var timeId = 0;
    function Show(msg) {
        var $tip = $(".MessageTip");
        if ($tip.size() == 0) {
            var tipHtml = new Array();
            tipHtml.push("<div class='MessageTip'>");
            tipHtml.push("</div>");
            $tip = $(tipHtml.join(""));
            $tip.css("bottom", 10);
            $tip.append("<p>" + msg + "</p>");
            $("body").append($tip);
            $tip.css("right", 0 - $tip.width());
            $tip.animate({ right: -2 }, 200, function () {
                timeId = setTimeout(function () {
                    $tip.animate({ right: (0 - $tip.width()) }, 200, function () { $(this).remove(); });
                }, 5000);
            });
        }
        else {
            $tip.append("<p>" + msg + "</p>");
            clearTimeout(timeId);
            timeId = setTimeout(function () {
                $tip.animate({ right: (0 - $tip.width()) }, 200, function () { $(this).remove(); });
            }, 5000);
        }
    }

    return { Show: Show }
})();

Easy.ShowUrlWindow = function (op) {
    /// <summary>打开窗口 Op = { url: "", title: "", width: 800, height: 600, callBack: function () { },isDialog:false }</summary>
    var boxWindow = $("<div class='WeiWindow BoxShadow'><div class='TitleBar'><div class='Left TitleBarLeft'></div><div class='Mid TitleBarMid'></div>" +
            "<div class='Right TitleBarRight'><div class='CloseWindow'></div></div></div><div class='Content'><div class='Left ContentLeft'></div><div class='Mid ContentMid'>" +
            "<iframe src='' width='100%' height='100%' frameborder='0'></iframe></div><div class='Right ContentRight'></div></div><div class='Botoom'>" +
            "<div class='Left BottomLeft'></div><div class='Mid BottomMid'></div><div class='Right BottomRight'></div></div></div>");
    var deOp = { url: "", title: "", width: 800, height: 500, callBack: function () { }, isDialog: true, animate: false, onLoad: function () { } };
    deOp = $.extend(deOp, op);
    if (deOp.isDialog) {
        Easy.OpacityBackGround.Show(++Easy.MaxZindex);
    }
    boxWindow.appendTo("body");
    boxWindow.find(".Mid.TitleBarMid").DragElement(boxWindow, boxWindow.find(".CloseWindow"), boxWindow.find(".Right.ContentRight"), boxWindow.find(".Right.BottomRight"), boxWindow.find(".Mid.BottomMid"));
    boxWindow.css("z-index", ++Easy.MaxZindex);

    boxWindow.find(".CloseWindow").click(function () {
        if (deOp.isDialog) {
            Easy.OpacityBackGround.Close(deOp.callBack);
        } else {
            deOp.callBack.call();
        }
    });
    boxWindow.close = function () {
        boxWindow.find(".CloseWindow").click();
    }
    boxWindow.center = function () {
        boxWindow.animate({ left: (Easy.WindowSize().width - deOp.width) / 2, top: (Easy.WindowSize().height - deOp.height) / 2 + det }, { speed: 200 });
    }
    $(window).resize(function () {
        Easy.Processor(boxWindow.center, 300);
    });
    var det = 0;
    if (Easy.Check.IsIE6()) {
        boxWindow.css("position", "absolute");
        det = Easy.GetScroll().top;
    }
    boxWindow.width(deOp.width);
    boxWindow.height(deOp.height);
    boxWindow.css("left", (Easy.WindowSize().width - deOp.width) / 2);
    boxWindow.css("top", (Easy.WindowSize().height - deOp.height) / 2 + det);
    boxWindow.find(".Mid.TitleBarMid").html(deOp.title);

    var reSet = true;
    boxWindow.find("iframe").load(function () {
        if (deOp.title == "") {
            boxWindow.find(".Mid.TitleBarMid").html(this.contentWindow.document.title);
        }
        var w = Number($(this.contentWindow.document).find("body").attr("width"));
        var h = Number($(this.contentWindow.document).find("body").attr("height"));
        var anop = { width: boxWindow.width(), left: boxWindow.css("left"), height: boxWindow.height(), top: boxWindow.css("top"), go: false };
        if (w) {
            anop.width = w;
            anop.go = true;
            anop.left = Number(boxWindow.css("left").replace("px", "")) - ((w - boxWindow.width()) / 2);
        }
        if (h) {
            anop.height = h;
            anop.go = true;
            anop.top = Number(boxWindow.css("top").replace("px", "")) - ((h - boxWindow.height()) / 2);
        }
        if (anop.go && reSet) {
            boxWindow.animate(anop, 300);
        }
        reSet = false;
        deOp.onLoad.call(this.contentWindow, boxWindow);
    });
    boxWindow.find("iframe").attr("src", deOp.url);
}

Easy.KeyEvent = (function () {
    var ModeKeys = new Array();
    var UserPress = new Array();
    var callBack;
    Keys = (function () {
        return {
            "Ctrl": 17, "Alt": 18, "Shift": 16, "Enter": 13, "Delete": 46, "BackSpace": 8, "Tab": 9, "Esc": 27, "Up": 38, "Left": 37, "Right": 39, "Down": 40
        }
    })();
    function KeysCode(keychar) {
        /// <summary>获取相应字母的ASICC码值</summary>
        try {
            return keychar.toUpperCase().charCodeAt();
        }
        catch (e) {
            alert("传入的按键字符不正确！Error:" + e.Message);
            return 0;
        }
    }
    function AddKeys(keys) {
        /// <summary>添加组合键</summary>
        /// <param name="keys" type="Int">请使用KeyEvent对象的Keys或KeysCode()方法</param>
        if (!ModeKeys.ContainsValue(keys))
            ModeKeys.push(keys);
    }
    function OnKeyPress(PressEvent) {
        /// <summary>响应用户按键事件</summary>
        /// <param name="PressEvent" type="Function">要绑定的事件方法</param>
        callBack = PressEvent;
    }
    function BindEventTo(EleID) {
        /// <summary>将键盘事件绑定到对象</summary>
        /// <param name="EleID" type="String">对象id，留空则将事件绑定到document上</param>
        if (typeof EleID == "string") {
            var ele = document.getElementById(EleID);
            if (ele != null) {
                Easy.EventUtil.AddEventListener(ele, "keydown", keyDown_Event);
                Easy.EventUtil.AddEventListener(ele, "keyup", keyUp_Event);
            } else {
                alert("BindEventTo()：要绑定到的对象为空");
            }
        }
        else {
            Easy.EventUtil.AddEventListener(document, "keyup", keyUp_Event);
            Easy.EventUtil.AddEventListener(document, "keydown", keyDown_Event);
        }
        function keyDown_Event() {
            if (!UserPress.ContainsValue(event.keyCode)) {
                UserPress.push(event.keyCode);
                if (UserPress.length == ModeKeys.length) {
                    var Las = true;
                    for (var i = 0; i < ModeKeys.length; i++) {
                        var theSame = false;
                        for (var j = 0; j < UserPress.length; j++) {
                            if (ModeKeys[i] == UserPress[j]) {
                                theSame = true;
                            }
                        }
                        if (theSame)
                            continue;
                        else {
                            Las = false;
                            break;
                        }
                    }
                    if (Las) {
                        if (callBack)
                            callBack.call();
                        UserPress = new Array();
                        return false;
                    }
                }
            }
            return true;
        }
        function keyUp_Event() {
            UserPress = new Array();
        }
    }
    return { Keys: Keys, KeysCode: KeysCode, AddKeys: AddKeys, OnKeyPress: OnKeyPress, BindEventTo: BindEventTo }
});

Easy.jQueryEasing = (function () {
    /// <summary>http://jqueryui.com/demos/effect/easing.html</summary>
    return { linear: "linear", swing: "swing", easeInQuad: "easeInQuad", easeOutQuad: "easeOutQuad", easeInOutQuad: "easeInOutQuad", easeInCubic: "easeInCubic", easeOutCubic: "easeOutCubic", easeInOutCubic: "easeInOutCubic", easeInQuart: "easeInQuart", easeOutQuart: "easeOutQuart", easeInOutQuart: "easeInOutQuart", easeInQuint: "easeInQuint", easeOutQuint: "easeOutQuint", easeInOutQuint: "easeInOutQuint", easeInSine: "easeInSine", easeOutSine: "easeOutSine", easeInOutSine: "easeInOutSine", easeInExpo: "easeInExpo", easeOutExpo: "easeOutExpo", easeInOutExpo: "easeInOutExpo", easeInCirc: "easeInCirc", easeOutCirc: "easeOutCirc", easeInOutCirc: "easeInOutCirc", easeInElastic: "easeInElastic", easeOutElastic: "easeOutElastic", easeInOutElastic: "easeInOutElastic", easeInBack: "easeInBack", easeOutBack: "easeOutBack", easeInOutBack: "easeInOutBack", easeInBounce: "easeInBounce", easeOutBounce: "easeOutBounce", easeInOutBounce: "easeInOutBounce" }
})();

Easy.LoadImage = function (url, callBack) {
    var load = $("<div class='Loading'></div>");
    load.css("top", "40%");
    load.fadeTo(0, 0);
    $("body").append(load);
    load.animate({ top: "50%", opacity: "100" }, 300, function () {
        var img = $("<img src='" + url + "' />");
        img.load(function () {
            load.animate({ top: "40%", opacity: "0" }, 300, function () { load.remove(); });
            callBack.call(this, img);
        });
    });
}

jQuery.fn.extend({
    AutoChangeImage: function (da, ImgWidth, ImgHeight) {
        /// <summary>只淡出，适合做背景切换</summary>
        /// <param name="da" type="Json">Json对象图片地址，格式{ Pics: [{ "Url": "" }, { "Url": ""}] }</param>
        /// <param name="ImgWidth" type="Int">图片宽度</param>
        /// <param name="ImgHeight" type="Int">图片高度</param>
        var BgPic = da;
        if (!BgPic || !BgPic.Pics || !BgPic.Pics[0].Url) {
            alert("AutoChangeImage()：给定的JSON数据格式不正确\r\n例：{ Pics: [{ Url: '' }, { Url: ''}] }");
            return false;
        }
        var jqThis = this;
        var Bgindex = 0;
        if (typeof ImgWidth != "number")
            ImgWidth = jqThis.width();
        if (typeof ImgHeight != "number")
            ImgHeight = jqThis.height();
        if (jqThis.find("img").length != 1) {
            jqThis.html("");
            jqThis.append("<img/>");
        }
        if (jqThis.css("position") != "fixed" && jqThis.css("position") != "absolute" && jqThis.css("position") != "relative") {
            jqThis.css("position", "relative");
        }
        jqThis.append("<div style='width:100%;height:100%; background-color:white;position:absolute'></div>");
        jqThis.find("img").bind("load", function () {
            jqThis.find("img").unbind("load");
            jqThis.find("img").width(ImgWidth);
            jqThis.find("img").height(ImgHeight);
            jqThis.find("div").fadeOut(1000, function () {
                $(this).remove();
                setTimeout(ChangeBg, 6000);
            });
        });
        jqThis.find("img").attr("src", BgPic.Pics[Bgindex].Url);
        jqThis.find("img").css("position", "absolute");

        function ChangeBg() {
            Bgindex++;
            if (Bgindex >= BgPic.Pics.length) {
                Bgindex = 0;
            }
            var imgg = $("<img alt='' style='position:absolute; left:0px; top:0px;' />");
            imgg.width(ImgWidth);
            imgg.height(ImgHeight);
            imgg.attr("src", jqThis.find("img").attr("src"));
            jqThis.append(imgg);
            $(jqThis.find("img")[0]).bind("load", FiIt);
            $(jqThis.find("img")[0]).attr("src", BgPic.Pics[Bgindex].Url);
        }
        function FiIt() {
            $(jqThis.find("img")[1]).fadeOut(2000, function () {
                $(this).remove();
                $(jqThis.find("img")[0]).unbind("load", FiIt);
                setTimeout(ChangeBg, 6000);
            });
        }
    },
    BackGroundAnimation: function (da, BgColor) {
        /// <summary>只淡出，适合做背景切换</summary>
        /// <param name="da" type="Json">Json对象图片地址，格式{ Pics: [{ "Url": "" }, { "Url": ""}] }</param>
        /// <param name="BgColor" type="String">加载图片时显示的背景色</param>
        var BgPic = da;
        if (!BgPic || !BgPic.Pics || !BgPic.Pics[0].Url) {
            alert("BackGroundAnimation():给定的JSON数据格式不正确！\r\n例：{ Pics: [{ Url: '' }, { Url: ''}] }");
            return false;
        }
        if (typeof BgColor != "string")
            BgColor = "White";
        var BgIndex = -1;
        var jqThis = this;
        var width = jqThis.width();
        var height = jqThis.height();
        if (jqThis.css("position") != "fixed" && jqThis.css("position") != "absolute" && jqThis.css("position") != "relative") {
            jqThis.css("position", "relative");
        }
        jqThis.append("<div style='width:" + width + "px;height:" + height + "px;background-color:" + BgColor + "; position:absolute;background-position:center center;left:0px;top:0px;'></div>");
        var img = new Image();
        img.onload = function () {
            var inDiv = $("<div style='width:" + width + "px;height:" + height + "px;position:absolute;background-position:center center;left:0px;top:0px;'></div>");
            inDiv.css("background-image", "url('" + this.src + "')");
            jqThis.prepend(inDiv);
            $(jqThis.children("div")[1]).fadeOut(1500, 0, function () {
                $(this).remove();
                setTimeout(StartChange, 8000);
            });
        }
        StartChange();
        function StartChange() {
            BgIndex++;
            if (BgIndex >= BgPic.Pics.length)
                BgIndex = 0;
            img.src = BgPic.Pics[BgIndex].Url;
        }
    }
});

/*jQuery扩展方法*/
jQuery.fn.extend({
    DragElement: function (targetEle, closeEle, ResizeE_Ele, ResizeNW_Ele, ResizeN_Ele) {
        /// <summary>让某个元素可以自由拖动示例：$("#box").DragElement();</summary>
        /// <param name="targetEle" type="jQuery">要移动的目标对象（如果是移动本身，请留空）</param>
        /// <param name="closeEle" type="jQuery">关闭按钮</param>
        /// <param name="ResizeE_Ele" type="jQuery">水平右调整大小</param>
        /// <param name="ResizeNW_Ele" type="jQuery">右下角调整大小</param>
        /// <param name="ResizeN_Ele" type="jQuery">垂直调整大小</param>
        var MouseX = 0;
        var MouseY = 0;
        var Qthis = this;
        if (!(targetEle instanceof jQuery)) {
            if (Qthis.css("position") != "absolute" && Qthis.css("position") != "fixed" && Qthis.css("position") != "relative")
                Qthis.css("position", "relative");
            Qthis.css("left", Qthis.offset().left);
            Qthis.css("top", Qthis.offset().top);
        }
        else {
            if (targetEle.css("position") != "absolute" && targetEle.css("position") != "fixed" && targetEle.css("position") != "relative") {
                targetEle.css("position", "relative");
            }
            targetEle.css("left", targetEle.offset().left);
            targetEle.css("top", targetEle.offset().top);
        }
        Qthis.css("cursor", "move");
        Qthis.bind("mousedown", { ac: "move" }, EleMouseDown);
        if (closeEle instanceof jQuery) {
            closeEle.click(function () {
                if (targetEle instanceof jQuery) {
                    targetEle.remove();
                }
                else {
                    Qthis.remove();
                }
            });
        }
        if (!Easy.Check.IsIE6()) {
            if (ResizeE_Ele instanceof jQuery) {
                ResizeE_Ele.css("cursor", "e-resize");
                ResizeE_Ele.bind("mousedown", { ac: "e-resize" }, EleMouseDown);
            }
            if (ResizeNW_Ele instanceof jQuery) {
                ResizeNW_Ele.css("cursor", "nw-resize");
                ResizeNW_Ele.bind("mousedown", { ac: "nw-resize" }, EleMouseDown);
            }
            if (ResizeN_Ele instanceof jQuery) {
                ResizeN_Ele.css("cursor", "n-resize");
                ResizeN_Ele.bind("mousedown", { ac: "n-resize" }, EleMouseDown);
            }
        }
        function EleMouseDown(e) {
            if (targetEle instanceof jQuery) {
                targetEle.css("z-index", ++Easy.MaxZindex);
            } else {
                Qthis.css("z-index", ++Easy.MaxZindex);
            }
            MouseX = Easy.MousePosition(e).X;
            MouseY = Easy.MousePosition(e).Y;
            var ele = CreateHelpeDiv(e.data.ac);
            ele.bind("mousemove", { ac: e.data.ac }, BindItGo);
            ele.mouseup(mouseUp_Event);
            return false;
        }
        function BindItGo(e) {
            var MX = Easy.MousePosition(e).X;
            var MY = Easy.MousePosition(e).Y;
            var Tleft = 0, Ttop = 0;
            if (e.data.ac == "move") {
                if (targetEle instanceof jQuery) {
                    Tleft = parseInt(targetEle.css("left"));
                    Ttop = parseInt(targetEle.css("top"));
                    targetEle.css("left", Tleft + MX - MouseX);
                    targetEle.css("top", Ttop + MY - MouseY);
                }
                else {
                    Tleft = parseInt(Qthis.css("left"));
                    Ttop = parseInt(Qthis.css("top"));
                    Qthis.css("left", Tleft + MX - MouseX);
                    Qthis.css("top", Ttop + MY - MouseY);
                }
            }
            else if (e.data.ac == "e-resize") {
                if (targetEle instanceof jQuery) {
                    Tleft = targetEle.width();
                    targetEle.width(Tleft + MX - MouseX);
                }
                else {
                    Tleft = Qthis.width();
                    Qthis.width(Tleft + MX - MouseX);
                }
            }
            else if (e.data.ac == "nw-resize") {
                if (targetEle instanceof jQuery) {
                    Tleft = targetEle.width();
                    Ttop = targetEle.height();
                    targetEle.width(Tleft + MX - MouseX);
                    targetEle.height(Ttop + MY - MouseY);
                }
                else {
                    Tleft = Qthis.width();
                    Ttop = Qthis.height();
                    Qthis.width(Tleft + MX - MouseX);
                    Qthis.height(Ttop + MY - MouseY);
                }
            }
            else if (e.data.ac == "n-resize") {
                if (targetEle instanceof jQuery) {
                    Ttop = targetEle.height();
                    targetEle.height(Ttop + MY - MouseY);
                }
                else {
                    Ttop = Qthis.height();
                    Qthis.height(Ttop + MY - MouseY);
                }
            }
            MouseX = MX;
            MouseY = MY;
            return false;
        }
        function CreateHelpeDiv(cursor) {
            if (typeof cursor != "string")
                cursor = "move";
            var ele = $("<div style='cursor:" + cursor + ";background-color:Black;z-index:100000'></div>");
            if (Easy.Check.IsIE6()) {
                ele.addClass("IE6Fixed");
            }
            else {
                ele.addClass("OtherFixed");
            }
            ele.fadeTo(0, 0);
            $("body").append(ele);
            return ele;
        }
        function mouseUp_Event() {
            $(this).remove();
            return false;
        }
    }
});
/*数据验证*/
jQuery.fn.extend({
    BeRequired: function (errorText) {
        /// <summary>必填项验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".RequireError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (Easy.Check.IsSpaceOnly($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-required");
                    if (!errorText) {
                        errorText = '这是必填项，在提交前请输入内容！';
                    }
                }
                var ele = Easy.Check.GetErrorBox('RequireError', errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeNumber: function (errorText) {
        /// <summary>纯数字验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".DigitError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsDigit($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "这里只能填写数字，请输入正确内容！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("DigitError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeEmail: function (errorText) {
        /// <summary>邮箱验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".EmailError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsEmail($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "邮箱格式错误，请输入正确内容！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("EmailError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeUserName: function (errorText) {
        /// <summary>注册用户名合法性验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".UserNameError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsUserName($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "只能输入5-20个字母开头、可带数字，“_”,“.”的字符串！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("UserNameError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeNickName: function (errorText) {
        /// <summary>昵称填写验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".NickNameError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsNickName($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "只能输入1-30个以字母开头的字串！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("NickNameError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BePassWord: function (errorText) {
        /// <summary>密码规则验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".PassWordError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsPasswd($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "只能输入6-20个字母、数字、下划线！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("PassWordError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeTelephone: function (errorText) {
        /// <summary>电话号码验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".TelePhoneError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsTel($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "电话号码格式不正确;例010-8888888";
                    }
                }
                var ele = Easy.Check.GetErrorBox("TelePhoneError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeMobelPhone: function (errorText) {
        /// <summary>手机号码验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".MobelPhomeError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsMobil($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "手机号码格式不正确！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("MobelPhomeError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeChinese: function () {
        /// <summary>中文验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".ChineseError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsChinese($(this).val())) {
                if (typeof errorText != "string") {
                    errorText = $(this).attr("data-val-regex");
                    if (!errorText) {
                        errorText = "这里只能输入1-5个汉字，请输入中文！";
                    }
                }
                var ele = Easy.Check.GetErrorBox("ChineseError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeCheckPassWord: function (selector) {
        /// <summary>密码重复验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="selector" type="String">验证密码输入的对象ID，采用jQuery检索方式，'#SthID'</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".PassWordSameError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if ($(this).val() != $(selector).val()) {
                var ele = Easy.Check.GetErrorBox("PassWordSameError", "两次输入的密码不一致，请重新输入！");
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeIpAddress: function (errorText) {
        /// <summary>IP地址验证，最后将提交按钮添加验证$('#form').BeCheckSubmit()，或调用方法:Easy.Check.IsCanSubmit()判断能否提交</summary>
        /// <param name="errorText" type="String">错误时的提示文字，可留空</param>
        Easy.checks.push(this);
        var tipId = Easy.checks.length;
        this.blur(function () {
            var tip = $(this).attr("errortip");
            if (!tip) {
                tip = "tip_" + tipId;
                $(this).attr("errortip", tip);
            }
            $(".IpAddressError[id='" + tip + "']").remove();
            $(this).removeClass("error");
            if (!Easy.Check.IsIP($(this).val())) {
                if (typeof errorText != "string")
                    errorText = "输入的IP地址格式错误";
                var ele = Easy.Check.GetErrorBox("IpAddressError", errorText);
                ele.attr("id", tip);
                $("body").append(ele);
                ele.css("left", $(this).offset().left - 10);
                ele.css("top", $(this).offset().top - ele.height());
                $(this).addClass("error");
            }
        });
        $("form").BeCheckSubmit();
    },
    BeCheckSubmit: function (callBack) {
        /// <summary>提交验证，是否可提交，如如是异步提交，请自主使用Easy.Check.IsCanSubmit()来验证是否可提交</summary>
        /// <param name="callBack" type="Function">验证失败后回调的函数</param>
        if ($(this).length == 0) {
            return false;
        }
        if (!this[0].submit) {
            return false;
        }
        if ($(this).attr("valid")) return false;
        $(this).attr("valid", "valid");
        Easy.EventUtil.AddEventListener(this[0], "submit", function () {
            for (var i = 0; i < Easy.checks.length; i++) {
                Easy.checks[i].triggerHandler('blur');
            }
            if (!Easy.Check.IsCanSubmit()) {
                Easy.EventUtil.PrEventDefault(event);
                if (callBack)
                    callBack.call();
            }
        });
    }
});

/*********mousewheel**********/
jQuery.fn.extend({
    mousewheel: function (up, down, preventDefault) {
        /// <summary>鼠标滚轮事件$("#a").mousewheel(function (e, delta) {$(this).scrollTop($(this).scrollTop() - delta * 10);});</summary>
        return this.hover(
            function () { jQuery.event.mousewheel.giveFocus(this, up, down, preventDefault); },
            function () { jQuery.event.mousewheel.removeFocus(this); }
        );
    },
    mousewheeldown: function (fn, preventDefault) {
        return this.mousewheel(function () { }, fn, preventDefault);
    },
    mousewheelup: function (fn, preventDefault) {
        return this.mousewheel(fn, function () { }, preventDefault);
    },
    unmousewheel: function () {
        return this.each(function () {
            jQuery(this).unmouseover().unmouseout();
            jQuery.event.mousewheel.removeFocus(this);
        });
    },
    unmousewheeldown: jQuery.fn.unmousewheel,
    unmousewheelup: jQuery.fn.unmousewheel
});

jQuery.event.mousewheel = {
    giveFocus: function (el, up, down, preventDefault) {
        if (el._handleMousewheel) jQuery(el).unmousewheel();
        if (preventDefault == window.undefined && down && down.constructor != Function) {
            preventDefault = down;
            down = null;
        }
        el._handleMousewheel = function (event) {
            if (!event) event = window.event;
            if (preventDefault)
                if (event.preventDefault) event.preventDefault();
                else event.returnValue = false;
            var delta = 0;
            if (event.wheelDelta) {
                delta = event.wheelDelta / 120;
                if (window.opera) delta = -delta;
            } else if (event.detail) {
                delta = -event.detail / 3;
            }
            if (up && (delta > 0 || !down)) up.apply(el, [event, delta]);
            else if (down && delta < 0) down.apply(el, [event, delta]);
        };
        if (window.addEventListener)
            window.addEventListener('DOMMouseScroll', el._handleMousewheel, false);
        window.onmousewheel = document.onmousewheel = el._handleMousewheel;
    },
    removeFocus: function (el) {
        if (!el._handleMousewheel) return;
        if (window.removeEventListener)
            window.removeEventListener('DOMMouseScroll', el._handleMousewheel, false);
        window.onmousewheel = document.onmousewheel = null;
        el._handleMousewheel = null;
    }
};

jQuery.fn.extend({
    VerticalScrollBar: function (selector) {
        /// <summary>垂直滚动条，例：<div id="Vertical-scroll-bar"></div> 不要子元素</summary>
        /// <param name="selector" type="String">滚动内容区的jQuery检索方式</param>
        var Qthis = this;
        Qthis.attr("title", "点击进行滚动");
        var scroBar = $("<div class='Vertical-bar'><div>");
        Qthis.append(scroBar);
        if (scroBar.height() == 0) {
            scroBar.height(30);
            scroBar.css("background-color", "White");
            scroBar.width(Qthis.width() - 2);
            scroBar.css("border", "solid 1px Gray");
        }
        var CotentPlace = $(selector);
        Qthis.css("position", "relative");
        scroBar.css("position", "absolute");
        scroBar.css("top", "0px");
        CotentPlace.css("overflow", "hidden");
        var ContentHeight = GetScrollHeight();
        var vBarHeight = Qthis.height() - scroBar.height();
        var Vdet = ContentHeight / vBarHeight;
        var MouseDownPosiY = 0;
        Qthis.children().click(function () { return false; });
        Qthis.children().mousedown(function (e) {
            MouseDownPosiY = Easy.MousePosition(e).Y - Qthis.offset().top;
            MouseDownPosiY -= parseInt(scroBar.css("top").toString().replace("px", ""));
            $(document).bind("mousemove", MoveHelp);
            $(document).bind("mouseup", dmouseUp);
            return false;
        });
        Qthis.click(function (e) {
            MouseDownPosiY = Easy.MousePosition(e).Y - Qthis.offset().top - scroBar.height() / 2;
            CotentPlace.animate({ "scrollTop": Vdet * (MouseDownPosiY) }, 300);
            return false;
        });
        if (ContentHeight <= 0) {
            Qthis.hide();
        }
        function MoveHelp(e) {
            var MouseY = Easy.MousePosition(e).Y - Qthis.offset().top;
            if (MouseY - MouseDownPosiY < 0) {
                return false;
            }
            if (MouseY - MouseDownPosiY > vBarHeight) {
                return false;
            }
            CotentPlace.scrollTop(Vdet * (MouseY - MouseDownPosiY));
            return false;
        }
        function dmouseUp() {
            $(document).unbind("mousemove", MoveHelp);
            $(document).unbind("mouseup", dmouseUp);
        }
        CotentPlace.mousewheel(function (e, delta) {
            $(this).scrollTop($(this).scrollTop() - delta * 25);
        });
        CotentPlace.scroll(function () {
            var top = $(this).scrollTop() / Vdet;
            if (top > vBarHeight)
                top = vBarHeight;
            scroBar.css("top", top);
        });
        function GetScrollHeight() {
            if (CotentPlace.children().length > 1) {
                var CpDiv = $("<div></div>");
                CotentPlace.children().appendTo(CpDiv);
                CpDiv.appendTo(CotentPlace);
            }
            CotentPlace.scrollTop(CotentPlace.children().height() * 2);
            var Ch = CotentPlace.scrollTop();
            CotentPlace.scrollTop(0);
            return Ch;
        }
        function FreshHeight() {
            ContentHeight = GetScrollHeight();
            vBarHeight = Qthis.height() - scroBar.height();
            Vdet = ContentHeight / vBarHeight;
            if (ContentHeight <= 0) {
                Qthis.hide();
            }
            else Qthis.show();
        }
        Easy.EventUtil.AddEventListener(window, "load", function () { FreshHeight(); });
        return FreshHeight;
    },
    AutoScroll: function () {
        /// <summary>根据鼠标移动，自滚动内容,如果有图片，请在onload以后调用该方法</summary>
        var Qthis = $(this);
        var ChHeight = 100;
        Qthis.children().each(function () {
            ChHeight += $(this).outerHeight();
        });
        var Vdet = (ChHeight - Qthis.height()) / Qthis.height();
        var ChWidth = Qthis.children().width();
        var Hdet = (ChWidth - Qthis.width()) / Qthis.width();
        Qthis.bind("mousemove", move_Event);
        function move_Event(e) {
            var mouseY = Easy.MousePosition(e).Y - $(this).offset().top;
            var mouseX = Easy.MousePosition(e).X - $(this).offset().left;
            if (Vdet > 0)
                $(this).scrollTop(Vdet * mouseY);
            if (Hdet > 0)
                $(this).scrollLeft(Hdet * mouseX);
        }
        function FreshHeight() {
            ChHeight = Qthis.children().height() * Qthis.children().size();
            Vdet = (ChHeight - Qthis.height()) / Qthis.height();
            ChWidth = Qthis.children().width();
            Hdet = (ChWidth - Qthis.width()) / Qthis.width();
        }
        return FreshHeight;
    },
    MarqueeH: function (offset) {
        /// <summary>水平跑马灯</summary>
        if (typeof offset != "number")
            offset = 2;
        var JqThis = this;
        var maxleft = JqThis.children().width() - JqThis.width();
        if (maxleft < 0)
            return false;
        var Pac = 1;
        var stop = false;
        setInterval(AutoH, 50);
        JqThis.mouseenter(function () {
            stop = true;
        });
        JqThis.mouseleave(function () {
            stop = false;
        });
        function AutoH() {
            if (!stop) {
                if (Pac == 1)
                    JqThis.scrollLeft(JqThis.scrollLeft() + offset);
                else JqThis.scrollLeft(JqThis.scrollLeft() - offset);
            }
            if (JqThis.scrollLeft() >= maxleft)
            { Pac = 2; }
            if (JqThis.scrollLeft() == 0)
                Pac = 1;
        }
    },
    MarqueeV: function (offset) {
        /// <summary>垂直跑马灯</summary>
        if (typeof offset != "number")
            offset = 2;
        var JqThis = this;
        var maxTop = JqThis.children().height() - JqThis.height();
        if (maxTop < 0)
            return false;
        var Pac = 1;
        var stop = false;
        setInterval(AutoV, 50);
        JqThis.mouseenter(function () {
            stop = true;
        });
        JqThis.mouseleave(function () {
            stop = false;
        });
        function AutoV() {
            if (!stop) {
                if (Pac == 1)
                    JqThis.scrollTop(JqThis.scrollTop() + offset);
                else JqThis.scrollTop(JqThis.scrollTop() - offset);
            }
            if (JqThis.scrollTop() >= maxTop)
                Pac = 2;
            if (JqThis.scrollTop() == 0)
                Pac = 1;
        }
    }
});


jQuery.fn.extend({
    PopUpShow: function (speed, BackGroundColor, callBack) {
        /// <summary>自小变大弹出提示窗</summary>
        /// <param name="speed" type="Int">弹出动画时间</param>
        /// <param name="BackGroundColor" type="String">动画效果时的背景颜色</param>
        /// <param name="callBack" type="Function">完成后回调</param>
        if (typeof speed != "number")
            speed = 300;
        var box = this;
        var overflow = box.css("overflow");
        var width = box.width();
        var height = box.height();
        var bgColor = box.css("background-color");
        if (typeof BackGroundColor != "string") {
            BackGroundColor = "Gray";
        }
        box.css("background-color", BackGroundColor);
        box.width(0);
        box.height(0);
        box.show();
        box.children().hide();
        if (box.css("left").toString().indexOf('%') != -1 && box.css("top").toString().indexOf('%') != -1) {
            var marginLeft = box.css("margin-left");
            var marginTop = box.css("margin-top");
            box.css("margin-left", 0);
            box.css("margin-top", 0);
            box.animate({ "width": width, "height": height, "margin-left": marginLeft, "margin-top": marginTop }, speed, function () {
                box.children().show();
                box.css("background-color", bgColor);
                box.css("overflow", overflow);
                if (callBack)
                    callBack.call();
            });
        }
        else {
            var left = parseInt(box.css("left"));
            if (left.toString() == "NaN") {
                left = (Easy.WindowSize().width - width) / 2;
            }
            var top = parseInt(box.css("top"));
            if (top.toString() == "NaN") {
                top = (Easy.WindowSize().height - height) / 2;
            }
            var leftM = left + width / 2;
            var topM = top + height / 2;
            box.css("left", leftM);
            box.css("top", topM);
            box.animate({ width: width, height: height, left: left, top: top }, speed, function () {
                box.width(width);
                box.height(height);
                box.css("left", left);
                box.css("top", top);
                box.css("background-color", bgColor);
                box.css("overflow", overflow);
                box.children().show();
                if (callBack)
                    callBack.call();
            });
        }
    },
    PopDownClose: function (speed, remove, BackGroundColor, callBack) {
        /// <summary>自大变小关闭</summary>
        /// <param name="speed" type="Int">关闭动画时间</param>
        /// <param name="remove" type="Boolean">是否移除该对象</param>
        /// <param name="BackGroundColor" type="String">动画效果时的背景颜色</param>
        /// <param name="callBack" type="Function">完成后的回调函数</param>
        if (typeof speed != "number")
            speed = 300;
        if (typeof remove != "boolean")
            remove = false;
        var box = $(this);
        var bgColor = box.css("background-color");
        var width = box.width();
        var height = box.height();
        if (typeof BackGroundColor != "string") {
            BackGroundColor = "Gray";
        }
        box.css("background-color", BackGroundColor);
        box.children().hide();
        if (box.css("left").toString().indexOf('%') != -1 && box.css("top").toString().indexOf('%') != -1) {
            var marleft = box.css("margin-left");
            var martop = box.css("margin-top");
            box.animate({ width: 0, height: 0, "margin-left": 0, "margin-top": 0 }, speed, function () {
                box.css("margin-left", marleft);
                box.css("margin-top", martop);
                box.css("background-color", bgColor);
                box.width(width);
                box.height(height);
                if (callBack)
                    callBack.call();
                if (remove)
                    box.remove();
                else
                    box.hide();
            });
        }
        else {
            var left = parseInt(box.css("left"));
            if (left.toString() == "NaN") {
                left = (Easy.WindowSize().width - width) / 2;
            }
            var top = parseInt(box.css("top"));
            if (top.toString() == "NaN") {
                top = (Easy.WindowSize().height - height) / 2;
            }
            var leftM = left + width / 2;
            var topM = top + height / 2;
            box.animate({ width: 0, height: 0, left: leftM, top: topM }, speed, function () {
                box.css("left", left);
                box.css("top", top);
                box.css("background-color", bgColor);
                box.width(width);
                box.height(height);
                if (callBack)
                    callBack.call();
                if (remove)
                    box.remove();
                else
                    box.hide();
            });
        }
    },
    AnimateMenu: function () {
        /// <summary>由ul,li组成的动画菜单</summary>
        this.find("ul").hide();
        this.find("li").click(function () {
            $(this).siblings().children("ul").slideUp(300);
            $(this).children("ul").slideDown(300);
        });
    },
    ImageMagnifier: function (n) {
        /// <summary>图片放大镜</summary>
        /// <param name="n" type="Int">放大的倍数,默认2倍</param>
        if (typeof n != "number")
            n = 2;
        var jQthis = this;
        Easy.EventUtil.AddEventListener(window, "load", function () {
            jQthis = $(jQthis.selector);
            Init();
        });
        function Init() {
            var thisWidth = jQthis.width();
            var thisHeight = jQthis.height();
            var imgDiv = $("<div class='Magnifier_ImageBox'></div>");
            imgDiv.width(thisWidth);
            imgDiv.height(thisHeight);
            var MoveBar = $("<div class='Magnifier_BgPoint'></div>");
            MoveBar.width(150);
            MoveBar.height(150);
            var jqPa = jQthis.parent();
            jqPa.append(imgDiv);
            if (jqPa.css("position") != "relative" && jqPa.css("position") != "absolute" && jqPa.css("position") != "relative")
                jqPa.css("position", "relative");
            var MaxImageDiv = $("<div class='Magnifier_MaxImageDiv'><img src='" + jQthis.attr("src") + "' /></div>");
            jqPa.append(MaxImageDiv);
            jQthis.appendTo(imgDiv);
            MaxImageDiv.css("left", 10 + thisWidth);
            MaxImageDiv.width(150 * n);
            MaxImageDiv.height(150 * n);
            MaxImageDiv.css("overflow", "hidden");
            var MaxImg = MaxImageDiv.find("img");
            var MiWidth = MaxImg.width();
            var MiHeight = MaxImg.height();
            MaxImg.load(function () {
                MiWidth = MaxImg.width();
                MiHeight = MaxImg.height();
                jQthis = $(jQthis.selector);
                thisWidth = jQthis.width();
                thisHeight = jQthis.height();
                imgDiv.width(thisWidth);
                imgDiv.height(thisHeight);
            });
            imgDiv.append(MoveBar);
            MaxImg.attr("width", MaxImageDiv.width() * (jQthis.width() / MoveBar.width()));
            jQthis.mouseenter(function () {
                MoveBar.fadeIn(300);
                MaxImageDiv.show(300);
                MaxImg.attr("src", $(jQthis.selector).attr("src"));
            });
            MoveBar.mouseout(function () { MoveBar.fadeOut(300); MaxImageDiv.fadeOut(300); });
            jQthis.bind("mouseenter", MoveBarHelp_Event);
            jQthis.bind("mousemove", MoveBarHelp_Event);
            MoveBar.bind("mousemove", MoveBarHelp_Event);
            var meX = jQthis.offset().left + MoveBar.width() / 2;
            var meY = jQthis.offset().top + MoveBar.height() / 2;
            Easy.EventUtil.AddEventListener(window, "resize", function () {
                meX = jQthis.offset().left + MoveBar.width() / 2;
                meY = jQthis.offset().top + MoveBar.height() / 2;
            });
            function MoveBarHelp_Event(e) {
                var x = Easy.MousePosition(e).X - meX;
                var y = Easy.MousePosition(e).Y - meY;
                MoveBar.css("left", x);
                MoveBar.css("top", y);
                var thx = x / thisWidth;
                var thy = y / thisHeight;
                if (MiWidth == 0 || MiHeight == 0) {
                    MiWidth = MaxImg.width();
                    MiHeight = MaxImg.height();
                }
                MaxImg.css("left", MiWidth * thx * -1);
                MaxImg.css("top", MiHeight * thy * -1);
            }

        }
    },
    ImageCenter_Adapt: function (PaWidth, PaHeight, BackGroundColor) {
        /// <summary>图片居中显示-适应(应有一定大小的父容器)</summary>
        /// <param name="PaWidth" type="Int">父容器的宽度</param>
        /// <param name="PaHeight" type="Int">父容器的高度</param>
        /// <param name="BackGroundColor" type="String">父容器填充的背景色</param>
        var inde = 1;
        this.each(function () {
            var ht = inde;
            var Qthis = $(this);
            Qthis.css("display", "block");
            Qthis.hide();
            var Pa = Qthis.parent();
            Pa.css("display", "block");
            if (typeof PaWidth == "number") {
                Pa.width(PaWidth);
            }
            if (typeof PaHeight == "number") {
                Pa.height(PaHeight);
            }
            Pa.css("background-color", BackGroundColor);
            var s = Pa.width() / Pa.height();
            var Img = new Image();
            Img.onload = function () {
                var w = Img.width;
                var h = Img.height;
                var s2 = w / h;
                if (s > s2) {
                    Qthis.height(Pa.height());
                    Qthis.css("width", "auto");
                    Qthis.css("margin", "0px auto");
                }
                else {
                    Qthis.width(Pa.width());
                    Qthis.css("height", "auto");
                    Qthis.css("margin-top", (Pa.height() - Qthis.height()) / 2);
                }
                setTimeout(function () {
                    Qthis.fadeIn(300);
                }, ht * 50);
            }
            Img.src = Qthis.attr("src");
            inde++;
        });
    },
    ImageCenter_Full: function (PaWidth, PaHeight) {
        /// <summary>图片居中显示-填充(应有一定大小的父容器)</summary>
        /// <param name="PaWidth" type="Int">父容器的宽度</param>
        /// <param name="PaHeight" type="Int">父容器的高度</param>
        var inde = 1;
        this.each(function () {
            var ht = inde;
            var Qthis = $(this);
            Qthis.hide();
            var Pa = Qthis.parent();
            Pa.css("overflow", "hidden");
            if (typeof PaWidth == "number") {
                Pa.width(PaWidth);
            }
            if (typeof PaHeight == "number") {
                Pa.height(PaHeight);
            }
            var s = Pa.width() / Pa.height();
            var Img = new Image();
            Img.onload = function () {
                var w = Img.width;
                var h = Img.height;
                var s2 = w / h;
                if (s > s2) {
                    Qthis.width(Pa.width());
                    Qthis.css("height", "");
                    Qthis.css("margin-top", (Pa.height() - Qthis.height()) / 2);
                }
                else {
                    Qthis.height(Pa.height());
                    Qthis.css("width", "");
                    Qthis.css("margin-left", (Pa.width() - Qthis.width()) / 2);
                }
                setTimeout(function () {
                    Qthis.fadeIn(300);
                }, ht * 50);
            }
            Img.src = Qthis.attr("src");
            inde++;
        });
    },
    ImagePreview: function (n, TargetUrl) {
        /// <summary>图片放大预览(应有一定大小的父容器)</summary>
        /// <param name="n" type="Double">图片放大倍数</param>
        /// <param name="TargetUrl" type="String">点击放大图片要跳转的地址</param>
        this.each(function () {
            if (typeof n != "number")
                n = 1.5;
            var Qthis = $(this);
            var Pa = Qthis.parent();
            if (Pa.css("position") != "relative" && Pa.css("position") != "absolute" && Pa.css("position") != "relative") {
                Pa.css("position", "relative");
            }
            Qthis.mouseenter(function () {
                var img = $("<img style='position:absolute;left:0px;top:0px;' />");
                if (typeof TargetUrl == "string") {
                    img.css("cursor", "pointer");
                    img.click(function () {
                        open(TargetUrl);
                    });
                }
                img.width(Qthis.width());
                img.height(Qthis.height());
                img.attr("src", Qthis.attr("src"));
                Pa.append(img);
                img.animate({ width: Qthis.width() * n, height: Qthis.height() * n, left: (Pa.width() - Qthis.width() * n) / 2, top: (Pa.height() - Qthis.height() * n) / 2 }, 300);
                img.mouseout(function () {
                    $(this).animate({ width: Qthis.width(), height: Qthis.height(), left: 0, top: 0 }, 200, function () { $(this).remove(); });
                });
            });
        });
    },
    ImageShow: function (Imgs) {
        /// <summary>弹出框查看原图（如果大图地址不同，请存于图片的bigsrc属性中）</summary>
        /// <param name="Imgs" type="Json">所有图片JSON格式{ Imgs: [{ src: "",alt:""}] }(可调用$("img").ImageShow_GetJson()来获取所有图片地址)</param>
        var Qthis = this;
        if (Imgs) {
            if (!Imgs.Imgs || !Imgs.Imgs[0].src) {
                alert("ImageShow()：给定的JSON数据格式不正确！\r\n 例：{ Imgs: [{ src: '',alt:''}] }");
                return false;
            }
        }
        Easy.OpacityBackGround.Show(100, function () {
            var div = $("<div class='RealImageBox'><div id='RealImageBox-chi'><div id='RealImageBox-Content'></div><div id='RealImageBox-Bottom'><div id='RealImageBox-Pre'></div><div id='RealImageBox-Next'></div><div id='RealImageBox-Text'></div><div id='RealImageBox-Close'></div></div></div></div>");
            $("body").append(div);
            if (Easy.Check.IsIE6()) {
                div.css("position", "absolute");
                div.css("left", (Easy.WindowSize().width - div.width()) / 2);
                div.css("top", Easy.GetScroll().top + (Easy.WindowSize().height - div.height()) / 2);
            }
            else {
                div.css("position", "fixed");
                div.css("left", (Easy.WindowSize().width - div.width()) / 2);
                div.css("top", (Easy.WindowSize().height - div.height()) / 2);
            }
            var OldRes = window.onresize;
            div.PopUpShow(200, "white", function () {
                var RealUrl = Qthis.attr("bigsrc");
                if (typeof RealUrl != "string") {
                    if (Imgs == null) {
                        RealUrl = Qthis.attr("src");
                    }
                    else {
                        RealUrl = Imgs.Imgs[0].src;
                    }
                }
                div.find("#RealImageBox-Close").click(CloseIt);
                var inputHelp = $("<input id='RealImageBox-KeyHelper' type='text' style='position:absolute; left:-300px; top:" + (Easy.GetScroll().top + 100) + "px'/>")[0];
                inputHelp.onblur = function () {
                    this.focus();
                }
                div.append(inputHelp);
                inputHelp.focus();
                var kevent = Easy.KeyEvent();
                kevent.AddKeys(kevent.Keys.Esc);
                kevent.BindEventTo("RealImageBox-KeyHelper");
                kevent.OnKeyPress(CloseIt);

                if (Imgs != null) {
                    var Index = 0;
                    for (var i = 0; i < Imgs.Imgs.length; i++) {
                        if (RealUrl == Imgs.Imgs[i].src) {
                            Index = i;
                            break;
                        }
                    }
                    setCor();
                    var Levent = Easy.KeyEvent();
                    Levent.AddKeys(kevent.Keys.Left);
                    Levent.BindEventTo("RealImageBox-KeyHelper");
                    Levent.OnKeyPress(Pre);

                    div.find("#RealImageBox-Pre").click(Pre);
                    function Pre() {
                        Index--;
                        if (Index < 0) {
                            Index = Imgs.Imgs.length - 1
                        }
                        setCor();
                        StartLoad(Imgs.Imgs[Index].src);
                    }
                    var Revent = Easy.KeyEvent();
                    Revent.AddKeys(kevent.Keys.Right);
                    Revent.BindEventTo("RealImageBox-KeyHelper");
                    Revent.OnKeyPress(Next);
                    div.find("#RealImageBox-Next").click(Next);
                    function Next() {
                        Index++;
                        if (Index >= Imgs.Imgs.length) {
                            Index = 0;
                        }
                        setCor();
                        StartLoad(Imgs.Imgs[Index].src);

                    }
                    function setCor() {
                        div.find("#RealImageBox-Text").html("<span id='PicIndex'>[" + (Index + 1) + "/" + Imgs.Imgs.length + "]</span><span id='PicInfo'>" + Imgs.Imgs[Index].alt + "</span>");
                    }
                } else {
                    div.find("#RealImageBox-Pre").hide();
                    div.find("#RealImageBox-Next").hide();
                }

                var img = new Image();
                StartLoad(RealUrl);
                function StartLoad(imgSrc) {
                    div.find("#RealImageBox-Content>img").remove();
                    img.onload = null;
                    img = new Image();
                    img.style.display = "none";
                    img.onload = function () {
                        var w = img.width;
                        var h = img.height;
                        window.onresize = function () {
                            if (OldRes) {
                                OldRes.call();
                            }
                            Easy.Processor(SetSizeAndLocation);
                        }
                        SetSizeAndLocation();
                        function SetSizeAndLocation() {
                            var WW = Easy.WindowSize().width * 0.7;
                            var HH = Easy.WindowSize().height * 0.7;
                            var ops = {
                                queue: false, duration: 300, complete: function () {
                                    div.find("#RealImageBox-Bottom").show(500);
                                    div.find("#RealImageBox-Content").append(img);
                                    div.find("#RealImageBox-Content>img").fadeIn(500);
                                }
                            };
                            if (jQuery.effects) {
                                ops = {
                                    queue: false, duration: 400, easing: Easy.jQueryEasing.easeOutBack, complete: function () {
                                        div.find("#RealImageBox-Bottom").show(500);
                                        div.find("#RealImageBox-Content").append(img);
                                        div.find("#RealImageBox-Content>img").fadeIn(300);
                                    }
                                };
                            }
                            if (w / h > WW / HH) {
                                var top = (Easy.WindowSize().height - WW / (w / h)) / 2;
                                if (Easy.Check.IsIE6())
                                    top += Easy.GetScroll().top;
                                div.animate({ width: WW, height: (WW / (w / h)), left: (Easy.WindowSize().width - WW) / 2, top: top }, ops);
                            }
                            else {
                                var topp = (Easy.WindowSize().height - HH) / 2;
                                if (Easy.Check.IsIE6())
                                    topp += Easy.GetScroll().top;
                                div.animate({ width: HH * (w / h), height: HH, left: (Easy.WindowSize().width - HH * (w / h)) / 2, top: top }, ops);
                            }
                        }
                    }
                    img.src = imgSrc;
                }
            });
            function CloseIt() {
                div.css("background-image", "none");
                div.PopDownClose(200, true, "white", function () { Easy.OpacityBackGround.Close(); });
                window.onresize = OldRes;
            }

        });
    },
    ImageShow_GetJson: function () {
        /// <summary>将筛选的img对象，转换成$("#img").ImageShow()可用的JSON对象</summary>
        var jsonStr = "{ Imgs: [";
        this.each(function () {
            var Qthis = $(this);
            if (typeof Qthis.attr("bigsrc") == "string") {
                jsonStr += "{src:'" + Qthis.attr("bigsrc") + "',alt:'" + (Qthis.attr("alt") != null ? Qthis.attr("alt") : '') + "'},";
            }
            else {
                jsonStr += "{src:'" + Qthis.attr("src") + "',alt:'" + (Qthis.attr("alt") != null ? Qthis.attr("alt") : '') + "'},";
            }
        });
        jsonStr = jsonStr.substring(0, jsonStr.length - 1) + "]}";
        return eval("(" + jsonStr + ")");
    }
});

jQuery.fn.extend({
    HtmlInput: function (msg) {
        $(this).attr("contenteditable", "true");
        $(this).addClass("HtmlInput");
        if (!msg) {
            msg = "单击以入内容...";
        }
        $(this).attr("PlaceHolder", msg);
        $(this).append("<span class='PlaceHolder'>" + msg + "</span>");
        $(this).blur(function () {
            if ($.trim($(this).text()) == "") {
                $(this).append("<span class='PlaceHolder'>" + $(this).attr("PlaceHolder") + "</span>");
            } else {
                $(this).find("a").attr("target", "_blank");
            }
        });
        $(this).focus(function () {
            var oThis = this;
            setTimeout(function () {
                if ($(oThis).find(".PlaceHolder").length > 0) {
                    $(oThis).html("");
                    $(oThis).focus();
                }
            }, 100);
        });
    },
    BePaged: function (child, pageSize) {
        var objThis = this;
        var children = $(this).find(child);
        var allPage = Math.floor(children.length / pageSize);
        allPage += children.length % pageSize == 0 ? 0 : 1;
        var box = $("<div class='paged'><div class='pageContent'></div></div>");
        box.insertAfter(this);
        $(this).appendTo(box.find(".pageContent"));
        var page = $("<ul class='pagination'></ul>");
        page.append("<li class='disable pre'>上一页</li>");
        for (var i = 0; i < allPage; i++) {
            var pageLi = $("<li pageIndex='" + i + "'>" + (i + 1) + "</li>");
            pageLi.click(function () { SetPageList($(this).attr("pageIndex")); });
            page.append(pageLi);
        }
        page.append("<li class='disable next'>下一页</li>");
        page.find(".pre").click(function () {
            if (!$(this).hasClass("disable")) {
                SetPageList(Number(page.find("li.select").attr("pageIndex")) - 1);
            }
        });
        page.find(".next").click(function () {
            if (!$(this).hasClass("disable")) {
                SetPageList(Number(page.find("li.select").attr("pageIndex")) + 1);
            }
        });
        box.append(page);
        box.append("<div style='clear:both;'></div>");
        $(objThis).find(child).remove();
        SetPageList(0);
        //box.find(".pageContent").height($(objThis).height());//固定高
        function SetPageList(pageIndex) {
            var cur = page.find("li.select");
            if (cur.length > 0 && Number(cur.attr("pageIndex")) == pageIndex) {
                return;
            }
            pageIndex = Number(pageIndex);

            $(objThis).find(child).remove();

            for (var i = (pageIndex * pageSize) ; i < children.length && i < ((pageIndex + 1) * pageSize) ; i++) {

                $(children[i]).appendTo(objThis);
            }
            page.find("li").removeClass("select");
            page.find("li[pageIndex='" + pageIndex + "']").addClass("select");
            if (pageIndex == 0) {
                page.find(".pre").addClass("disable");
            } else {
                page.find(".pre").removeClass("disable");
            }
            if (pageIndex == allPage - 1) {
                page.find(".next").addClass("disable");
            } else {
                page.find(".next").removeClass("disable");
            }
        }
    }
});