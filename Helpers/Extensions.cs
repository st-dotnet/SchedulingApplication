namespace SchedulingApplication.Helpers
{
    public static class Extensions
    {
        public static int GetId(this object obj, int defaultId = 0)
        {
            if (obj == null) return defaultId;

            if (int.TryParse(obj.ToString(), out int value))
                return value;

            return defaultId;
        }
    }
}
