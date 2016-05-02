using System.Net;
using Android.Graphics;

namespace Movies.Utilities
{
    public static class GetImageBitmap
    {
        //transfer image uri to Bitmap
        public static Bitmap GetImageBitmapFromUri(string uri)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(uri);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length, new BitmapFactory.Options { InDensity = 6, InTargetDensity = 1 });
                }
            }
            return imageBitmap;
        }
    }
}