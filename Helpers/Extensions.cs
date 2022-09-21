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

        public static string ToBase64String(this IFormFile formFile)
        {
            if (formFile == null) return String.Empty;

            var bytes = GetBytes(formFile);

            return Convert.ToBase64String(bytes);
        }

        private static byte[] GetBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            } 
        }
    }
}
