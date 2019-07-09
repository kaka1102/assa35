using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ASS_35.Areas.ADMIN
{
    public class ADMINAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ADMIN";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            var loginAreaContext = new DomainAreaRegistrationContext("admin.localhost", context);
            //var loginAreaContext = new DomainAreaRegistrationContext("admin.assa35.com", context);

            loginAreaContext.MapRoute(
                name: "LoginDefault",
                url: "",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            loginAreaContext.MapRoute(
                name: "LoginDefault2",
                url: "login",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            //context.MapRoute(
            //    name: "ConfirmEmail",
            //    url: "ConfirmEmail",
            //    defaults: new
            //    {
            //        controller = "Account",
            //        action = "ConfirmEmail"
            //    },
            //    namespaces: new[] { "ASS_35.Areas.ADMIN.Controllers" }
            //);

            loginAreaContext.MapRoute(
                name: "DefaultAdmin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

         
            //context.MapRoute(
            //    "ADMIN_default",
            //    "ADMIN/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

        }
    }
}


internal static class DomainRegexCache
{
    // since we're often going to have the same pattern used in multiple routes, it's best to build just one regex per pattern
    private static ConcurrentDictionary<string, Regex> _domainRegexes = new ConcurrentDictionary<string, Regex>();

    internal static Regex CreateDomainRegex(string domain)
    {
        return _domainRegexes.GetOrAdd(domain, (d) =>
        {
            d = d.Replace("/", @"\/")
                 .Replace(".", @"\.")
                 .Replace("-", @"\-")
                 .Replace("{", @"(?<")
                 .Replace("}", @">(?:[a-zA-Z0-9_-]+))");

            return new Regex("^" + d + "$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
        });
    }
}

public class DomainRoute : Route
{
    private const string DomainRouteMatchKey = "DomainRoute.Match";
    private const string DomainRouteInsertionsKey = "DomainRoute.Insertions";

    private Regex _domainRegex;
    public string Domain { get; private set; }

    public DomainRoute(string domain, string url, RouteValueDictionary defaults)
        : this(domain, url, defaults, new MvcRouteHandler())
    {
    }

    public DomainRoute(string domain, string url, object defaults)
        : this(domain, url, new RouteValueDictionary(defaults), new MvcRouteHandler())
    {
    }

    public DomainRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
        : this(domain, url, new RouteValueDictionary(defaults), routeHandler)
    {
    }

    public DomainRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
        : base(url, defaults, routeHandler)
    {
        Domain = domain;
        _domainRegex = DomainRegexCache.CreateDomainRegex(Domain);
    }

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        var requestDomain = httpContext.Request.Url.Host;

        var domainMatch = _domainRegex.Match(requestDomain);
        if (!domainMatch.Success)
            return null;

        var existingMatch = httpContext.Items[DomainRouteMatchKey] as string;

        if (existingMatch == null)
            httpContext.Items[DomainRouteMatchKey] = Domain;
        else if (existingMatch != Domain)
            return null;

        var data = base.GetRouteData(httpContext);
        if (data == null)
            return null;

        var myInsertions = new HashSet<string>();

        for (var i = 1; i < domainMatch.Groups.Count; i++)
        {
            var group = domainMatch.Groups[i];
            if (group.Success)
            {
                var key = _domainRegex.GroupNameFromNumber(i);
                if (!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(group.Value))
                {
                    // could throw here if data.Values.ContainsKey(key) if we wanted to prevent multiple matches
                    data.Values[key] = group.Value;
                    myInsertions.Add(key);
                }
            }
        }

        httpContext.Items[DomainRouteInsertionsKey] = myInsertions;
        return data;
    }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        return base.GetVirtualPath(requestContext, RemoveDomainTokens(requestContext, values));
    }

    private RouteValueDictionary RemoveDomainTokens(RequestContext requestContext, RouteValueDictionary values)
    {
        var myInsertions = requestContext.HttpContext.Items[DomainRouteInsertionsKey] as HashSet<string>;

        if (myInsertions != null)
        {
            foreach (var key in myInsertions)
            {
                if (values.ContainsKey(key))
                    values.Remove(key);
            }
        }

        return values;
    }
}

// For MVC routes
public class DomainRouteCollection
{
    private string Domain { get; set; }
    private RouteCollection Routes { get; set; }

    public DomainRouteCollection(string domain, RouteCollection routes)
    {
        Domain = domain;
        Routes = routes;
    }

    public Route MapRoute(string name, string url)
    {
        return MapRoute(name, url, null, null, null);
    }

    public Route MapRoute(string name, string url, object defaults)
    {
        return MapRoute(name, url, defaults, null, null);
    }

    public Route MapRoute(string name, string url, string[] namespaces)
    {
        return MapRoute(name, url, null, null, namespaces);
    }

    public Route MapRoute(string name, string url, object defaults, object constraints)
    {
        return MapRoute(name, url, defaults, constraints, null);
    }

    public Route MapRoute(string name, string url, object defaults, string[] namespaces)
    {
        return MapRoute(name, url, defaults, null, namespaces);
    }

    public Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
    {
        if (name == null)
            throw new ArgumentNullException("name");
        if (url == null)
            throw new ArgumentNullException("url");

        var route = new DomainRoute(Domain, url, defaults, new MvcRouteHandler())
        {
            Constraints = new RouteValueDictionary(constraints),
            DataTokens = new RouteValueDictionary()
        };

        if (namespaces != null && namespaces.Length > 0)
            route.DataTokens["Namespaces"] = namespaces;

        Routes.Add(name, route);

        return route;
    }
}

// For Areas routes
public class DomainAreaRegistrationContext
{
    private AreaRegistrationContext Context { get; set; }
    private DomainRouteCollection Routes { get; set; }

    public DomainAreaRegistrationContext(string domain, AreaRegistrationContext context)
    {
        Context = context;
        Routes = new DomainRouteCollection(domain, Context.Routes);
    }

    public Route MapRoute(string name, string url)
    {
        return MapRoute(name, url, null, null, null);
    }

    public Route MapRoute(string name, string url, object defaults)
    {
        return MapRoute(name, url, defaults, null, null);
    }

    public Route MapRoute(string name, string url, string[] namespaces)
    {
        return MapRoute(name, url, null, null, namespaces);
    }

    public Route MapRoute(string name, string url, object defaults, object constraints)
    {
        return MapRoute(name, url, defaults, constraints, null);
    }

    public Route MapRoute(string name, string url, object defaults, string[] namespaces)
    {
        return MapRoute(name, url, defaults, null, namespaces);
    }

    public Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
    {
        if (namespaces == null && Context.Namespaces != null)
            namespaces = Context.Namespaces.ToArray();

        var route = Routes.MapRoute(name, url, defaults, constraints, namespaces);
        route.DataTokens["area"] = Context.AreaName;

        // disabling the namespace lookup fallback mechanism keeps this area from accidentally picking up
        // controllers belonging to other areas
        bool useNamespaceFallback = (namespaces == null || namespaces.Length == 0);
        route.DataTokens["UseNamespaceFallback"] = useNamespaceFallback;

        return route;
    }
}
