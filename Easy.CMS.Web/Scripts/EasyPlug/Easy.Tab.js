/// <reference path="jquery.js" />
/// <reference path="Easy.js" />
var Easy = Easy || {};

Easy.Tab = (function (selector, tabs, tabContent, dbClose) {
    var autoLoad = false;
    var MainContent = $(selector);
    var Tabs;
    var TabContent;
    var tabHtml = "<li><label class='TabBtn'>{Title}</label></li>";
    var ContentHtml = "<div class='Contents'></div>";
    var AllTab = new Array();
    var History = new Array();
    var tabCount = 0;
    if (tabs == null) {
        MainContent.append("<div class='Tabtitle'><ul class='TopMenu'></ul><div style='clear:both'></div></div>");
        MainContent.append("<div class='TabContent'></div>");
        Tabs = MainContent.find(".TopMenu");
        TabContent = MainContent.find(".TabContent");
    }
    else {
        Tabs = MainContent.find(tabs);
        TabContent = MainContent.find(tabContent);
        TabContent.addClass("TabContent");
        if (Tabs.children("li").size() > 0) {
            var liFirst = Tabs.children("li:first").html();
            liFirst = $(liFirst);
            liFirst.html("{Title}");
            tabHtml = "<li class='Active'>" + liFirst[0].outerHTML + "</li>";
            Tabs.children("li").each(function (index) {
                $(this).click(TabClick);
                if (dbClose)
                    $(this).dblclick(TabDoubleClick);
                var tabId = $(this).attr("id");
                tabId = CreateTabId(tabId);
                AllTab.push(tabId);
                $(this).attr("id", tabId);
                var tabcon = TabContent.children().not("script").eq(index);
                tabcon.attr("id", "tabContent_" + tabId);
                tabcon.addClass("Contents");
            });
            Tabs.children("li:first").addClass("Active");
            TabContent.children().first().addClass("Active");
        }
    }
    function TabClick() {
        SwitchTo($(this).attr("id"));
    }
    function TabDoubleClick() {
        tabId = $(this).attr("id");
        while (AllTab.ContainsValue(tabId)) {
            AllTab = AllTab.DelbyValue(tabId);
        }
        while (History.ContainsValue(tabId)) {
            History = History.DelbyValue(tabId);
        }
        TabContent.children("#tabContent_" + tabId).animate({ height: "0px" }, 100, function () { $(this).remove() });
        $(this).animate({ width: "0px" }, 100, function () { $(this).remove() });
        var next = History.pop();
        if (!next) {
            next = AllTab[AllTab.length - 1];
        }
        SwitchTo(next);
    }
    function CloseTab(tabId) {
        if (tabId == null)
            tabId = $(this).attr("id");
        if (tabId == null) return false;
        while (AllTab.ContainsValue(tabId)) {
            AllTab = AllTab.DelbyValue(tabId);
        }
        while (History.ContainsValue(tabId)) {
            History = History.DelbyValue(tabId);
        }
        TabContent.children("#tabContent_" + tabId).animate({ height: "0px" }, 100, function () { $(this).remove() });
        Tabs.find("#" + tabId).animate({ width: "0px" }, 100, function () { $(this).remove() });
        var next = History.pop();
        if (!next) {
            next = AllTab[AllTab.length - 1];
        }
        SwitchTo(next);
    }
    function CloseCurrentTab() {
        CloseTab(Tabs.children("li.Active").attr("id"));
    }
    function AddTab(title, content, tabId) {
        var tabId = CreateTabId(tabId);
        if (ExistTab(tabId)) {
            SwitchTo(tabId);
        }
        else {
            var newTab = $(tabHtml.replace("{Title}", title)).attr("id", tabId);
            newTab.click(TabClick);
            newTab.dblclick(TabDoubleClick);
            AllTab.push(tabId);
            Tabs.append(newTab);
            var con = $(ContentHtml).append(content);
            con.attr("id", "tabContent_" + tabId);
            TabContent.append(con);
            if (Tabs.children("li").size() == 1) {
                SwitchTo(tabId);
            }
        }
        return returnThis;
    }
    function AddTabAndShow(title, content, tabId) {
        var tabId = CreateTabId(tabId);
        AddTab(title, content, tabId);
        SwitchTo(tabId);
        return returnThis;
    }
    function AddTabUrl(title, url, tabId) {
        var tabId = CreateTabId(tabId);
        if (ExistTab(tabId)) {
            SwitchTo(tabId);
        }
        else {
            var iframe = "<iframe src='" + url + "' width='100%' frameborder='0' height='100%'></iframe>";
            AddTab(title, iframe, tabId);
           
        }
        return returnThis;
    }
    function AddTabUrlAndShow(title, url, tabId) {
        var tabId = CreateTabId(tabId);
        AddTabUrl(title, url, tabId);
        SwitchTo(tabId);
        return returnThis;
    }
    function GetLastTabId() {
        return Tabs.children("li:last").attr("id");
    }
    function GetLastTabContentId() {
        return TabContent.children().not("script").last().attr("id");
    }
    function ExistTab(tabId) {
        if (Tabs.children("#" + tabId).size() > 0) {
            return true;
        }
        else return false;
    }
    function GetTabByTabId(tabId) {
        return Tabs.children("#" + tabId);
    }
    function SwitchTo(tabId) {
        History.push(tabId);
        Tabs.children().removeClass("Active");
        TabContent.children().removeClass("Active");
        var curtab = Tabs.children("#" + tabId);
        curtab.addClass("Active");
        if (curtab.attr("url")) {
            $.get(curtab.attr("url"), function (data) {
                TabContent.children("#tabContent_" + tabId).html(data);
            }, "html");
            curtab.removeAttr("url");
        }
        TabContent.children("#tabContent_" + tabId).addClass("Active");
    }
    function CreateTabId(tabId) {
        var tabId = tabId || "tab_" + tabCount;
        tabCount++;
        return tabId = tabId;
    }
    function SetTabModel(str) {
        tabHtml = str;
        return returnThis;
    }
    var returnThis = {
        AddTab: AddTab, SetTabModel: SetTabModel, AddTabAndShow: AddTabAndShow,
        AddTabUrl: AddTabUrl, AddTabUrlAndShow: AddTabUrlAndShow, CloseTab: CloseTab, CloseCurrentTab: CloseCurrentTab
    }
    return returnThis;
});