using System;

namespace Project.Utilities
{
    public class MenuUtility
    {
        public static string MenuTitle(string Label)
        {
            string html = string.Empty;
            html = html + "<li class='menu-title'>";
            html = html + "<span>" + Label + "</span>";
            html = html + "</li>";
            return html;
        }
        public static string Simple_li_List(string URL, string Class, string Label)
        {
            string html = string.Empty;
            html = html + "<li>";
            html = html + "<a href =\"" + URL + "\"><i class=\"" + Class + "\"></i><span>" + Label + "</span></a>";
            html = html + "</li>";
            return html;
        }
        public static string Sub_Menu_li_ListStart(string URL, string Class, string Label)
        {
            string html = string.Empty;
            html = html + "<li class='submenu'>";
            html = html + "<a href =\"" + URL + "\"><i class=\"" + Class + "\"></i><span>" + Label + "</span><span class='menu-arrow'></span></a>";
            html = html + "<ul>";
            return html;
        }
        public static string Sub_Menu_li_List_ul(string URL, string Label)
        {
            string html = string.Empty;
            html = html + "<li>";
            html = html + "<a href =\"" + URL + "\">" + Label + "</a>";
            html = html + "<li>";
            return html;
        }
        public static string Sub_Menu_li_ListEnd()
        {
            string html = string.Empty;
            html = html + "</ul>";
            html = html + "</li>";
            return html;
        }
    }
}
