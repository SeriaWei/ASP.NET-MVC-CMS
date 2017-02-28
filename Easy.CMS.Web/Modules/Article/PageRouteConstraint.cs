using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Easy.CMS.Article
{
    public class PageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == "path")
            {
                int articleId = 0;
                string path = values[parameterName].ToString();
                path = Regex.Replace(path, @"/article-(\d+)$", evaluator =>
                 {
                     int.TryParse(evaluator.Groups[1].Value, out articleId);
                     return "";
                 });
                if (articleId > 0)
                {
                    values[parameterName] = path;
                    values.Add("id", articleId);
                    return true;
                }
            }
            return false;
        }
    }
}