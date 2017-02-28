using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Easy.Web.CMS.Route
{
    public class PageRouteConstraint : IRouteConstraint
    {
        static Regex postIdRegex = new Regex(@"/post-(\d+)");
        static Regex categoryIdRegex = new Regex(@"/cate-(\d+)");
        static Regex pageRegex = new Regex(@"/p-(\d+)");
        public bool Match(HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == "path")
            {
                string path = values[parameterName].ToString();
                int postId = 0;
                int categoryId = 0;
                int page = 0;
                if (pageRegex.IsMatch(path))
                {
                    path = pageRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out page);
                        return "";
                    });
                    values.Add("page", page);
                }

                if (postIdRegex.IsMatch(path))
                {
                    path = postIdRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out postId);
                        return "";
                    });
                }
                else if (categoryIdRegex.IsMatch(path))
                {
                    path = categoryIdRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out categoryId);
                        return "";
                    });
                }
                values[parameterName] = path;
                if (postId > 0)
                {
                    values.Add("post", postId);
                }
                if (categoryId > 0)
                {
                    values.Add("cate", categoryId);
                }
            }
            return true;
        }
    }
}