namespace MiniAPICourse
{
    public class Uint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values == null || values.Count == 0 || string.IsNullOrWhiteSpace(routeKey))
            {
                return false;
            }
            var result = uint.TryParse(values[routeKey].ToString(), out uint value);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
