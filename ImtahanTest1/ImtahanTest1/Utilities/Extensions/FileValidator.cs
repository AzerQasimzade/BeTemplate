using ImtahanTest1.Utilities.Enums;

namespace ImtahanTest1.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool ValidateFileType(this IFormFile file,FileHelper type)
        {
            if (type==FileHelper.Image)
            {
                if (!file.ContentType.Contains("/image"))
                {
                    return false;
                }
                return true;
            }
            if (type == FileHelper.Video)
            {
                if (!file.ContentType.Contains("/video"))
                {
                    return false;
                }
                return true;
            }
            if (type == FileHelper.Audio)
            {
                if (!file.ContentType.Contains("/audio"))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool ValidateFileSize()
    }
}
