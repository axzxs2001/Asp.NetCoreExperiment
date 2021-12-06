using System.Reflection;

namespace MiniAPICourse
{
    public class Area1
    {
        public Coordinates[]? Coordinateses { get; set; }
        public static bool TryParse(string? value, IFormatProvider? provider, out Area1? area)
        {
            var CoordinatesGroupStrings = value?.Split(new string[] { "[(", ")]", "),(" },
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (CoordinatesGroupStrings != null)
            {
                var coordinatesList = new List<Coordinates>();
                foreach (var coordinateGroupString in CoordinatesGroupStrings)
                {
                    var coordinateStrings = coordinateGroupString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    var latitudeResult = double.TryParse(coordinateStrings[0], out double latitude);
                    var longitudeResult = double.TryParse(coordinateStrings[1], out double longitude);
                    if (latitudeResult && longitudeResult)
                    {
                        coordinatesList.Add(new Coordinates(latitude, longitude));
                    }
                }
                area = new Area1 { Coordinateses = coordinatesList.ToArray() };
                return true;
            }
            area = null;
            return false;
        }
    }
    public record Coordinates(double Latitude, double Longitude);

    public class Area2
    {
        public Coordinates[]? Coordinateses { get; set; }

        public static ValueTask<Area2?> BindAsync(HttpContext context, ParameterInfo parameter)
        {
            var CoordinatesGroupStrings = context.Request.Query["area"].ToString().Split(new string[] { "[(", ")]", "),(" },
                   StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (CoordinatesGroupStrings != null)
            {
                var coordinatesList = new List<Coordinates>();
                foreach (var coordinateGroupString in CoordinatesGroupStrings)
                {
                    var coordinateStrings = coordinateGroupString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    var latitudeResult = double.TryParse(coordinateStrings[0], out double latitude);
                    var longitudeResult = double.TryParse(coordinateStrings[1], out double longitude);
                    if (latitudeResult && longitudeResult)
                    {
                        coordinatesList.Add(new Coordinates(latitude, longitude));
                    }
                }
                return ValueTask.FromResult<Area2?>(new Area2 { Coordinateses = coordinatesList.ToArray() });

            }
            return ValueTask.FromResult<Area2?>(null);
        }
    }
}
