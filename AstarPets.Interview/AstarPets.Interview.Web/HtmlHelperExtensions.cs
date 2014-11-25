using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AstarPets.Interview.Web
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DropDownList(this HtmlHelper html, string name, Type constantsClass)
        {
            return html.DropDownList(name,
                ReflectionHelpers.GetConstants(constantsClass).Select(s => new SelectListItem() {Text = s, Value = s}));
        }
    }

    public static class ReflectionHelpers
    {
        public static IEnumerable<string> GetConstants(Type constantsClass)
        {
            return constantsClass.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(f => f.GetValue(null).ToString());
        }
    }
}