using Android.Content;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Threading.Tasks;
using Android.Graphics;

namespace Movies.Utilities
{
    public static class PopulateMovie
    {
        //retrieve movies to movie list
        public async static Task<LinearLayout> populateMovieList(string url, LinearLayout movieList, Context context)
        {
            string imageUri = "https://image.tmdb.org/t/p/original";
            JsonValue jsonValue = await FetchMoviesAsync.FetchMoviesAsyncCall(url);
            var results = jsonValue["results"];
            var i = 0;
            foreach (JsonObject movieObject in results)
            {
                Android.Net.Uri androidUri = Android.Net.Uri.Parse(imageUri + movieObject["poster_path"]);

                //layout parameters
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(350, 450);
                lp.RightMargin = 10;

                //define ImageView
                ImageView imageView = new ImageView(context);
                imageView.LayoutParameters = lp;
                imageView.Visibility = ViewStates.Visible;
                imageView.SetPadding(2, 2, 2, 2);
                imageView.SetBackgroundColor(Color.White);
                imageView.CropToPadding = true;
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                var imageBitmap = GetImageBitmap.GetImageBitmapFromUri(imageUri + movieObject["poster_path"]);
                imageView.SetImageBitmap(imageBitmap);

                //add click event to ImageView
                imageView.Click += delegate
                {
                    var detailActivity = new Intent(context, typeof(DetailActivity));
                    detailActivity.PutExtra("movieObject", (int)movieObject["id"]);
                    context.StartActivity(detailActivity);
                };

                //add imageView to movieList
                movieList.AddView(imageView);

                //placeholder. Loading 7 movies for now
                i++;
                if (i > 7)
                    break;
            }
            return movieList;
        }
    }
}