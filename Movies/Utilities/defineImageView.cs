using Android.Content;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using Android.Graphics;

namespace Movies.Utilities
{
    public static class DefineImageView
    {
        public static ImageViewAsync GetImageView(Context context, LinearLayout.LayoutParams lp)
        {
            ImageViewAsync imageView = new ImageViewAsync(context);

            imageView.LayoutParameters = lp;
            imageView.Visibility = ViewStates.Visible;
            imageView.SetPadding(2, 2, 2, 2);
            imageView.SetBackgroundColor(Color.White);
            imageView.CropToPadding = true;
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);

            return imageView;
        }
    }
}