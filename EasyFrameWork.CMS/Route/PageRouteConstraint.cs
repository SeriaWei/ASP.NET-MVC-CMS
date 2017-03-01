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
        public bool Match(HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == "path")
            {
                string path = values[parameterName].ToString();
                int postId = 0;
                int categoryId = 0;
                int page = 0;
                if (CustomRegex.PageRegex.IsMatch(path))
                {
                    path = CustomRegex.PageRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out page);
                        return "";
                    });
                    values.Add(StringKeys.RouteValue_Page, page);
                }

                if (CustomRegex.PostIdRegex.IsMatch(path))
                {
                    path = CustomRegex.PostIdRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out postId);
                        return "";
                    });
                }
                else if (CustomRegex.CategoryIdRegex.IsMatch(path))
                {
                    path = CustomRegex.CategoryIdRegex.Replace(path, evaluator =>
                    {
                        int.TryParse(evaluator.Groups[1].Value, out categoryId);
                        return "";
                    });
                }
                values[parameterName] = path;
                if (postId > 0)
                {
                    values.Add(StringKeys.RouteValue_Post, postId);
                }
                if (categoryId > 0)
                {
                    values.Add(StringKeys.RouteValue_Category, categoryId);
                }
            }
            return true;
        }
    }
}