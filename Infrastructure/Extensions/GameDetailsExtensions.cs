using vindiniumcore.Infrastructure.Map;

namespace vindiniumcore.Infrastructure.Extensions
{
    public static class GameDetailsExtensions
    {
        public static string GetDirection(CoOrdinates currentLocation, CoOrdinates moveTo)
        {
            var direction = "Stay";
            if (moveTo == null)
            {
                return direction;
            }
            if (moveTo.X > currentLocation.X)
            {
                direction = "East";
            }
            else if (moveTo.X < currentLocation.X)
            {
                direction = "West";
            }
            else if (moveTo.Y > currentLocation.Y)
            {
                direction = "South";
            }
            else if (moveTo.Y < currentLocation.Y)
            {
                direction = "North";
            }

            return direction;
        }

    }
}
