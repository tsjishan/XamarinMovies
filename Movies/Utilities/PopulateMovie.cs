using Android.Content;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Threading.Tasks;
using Android.Graphics;
using FFImageLoading;
using FFImageLoading.Views;

namespace Movies.Utilities
{
    public static class PopulateMovie
    {
        //retrieve movies to movie list
        public async static Task<LinearLayout> populateMovieList(string uri, LinearLayout movieList, Context context)
        {
            string imageUri = "https://image.tmdb.org/t/p/original";
            JsonValue jsonValue = await FetchMoviesAsync.FetchMoviesAsyncCall(uri);
            var results = jsonValue["results"];

            //loop through results to add imageviews to LinearLayout - moviewList
            #region
            foreach (JsonObject movieObject in results)
            {
                string movieUri = imageUri + movieObject["poster_path"];

                //layout parameters
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(350, 450);
                lp.RightMargin = 10;

                //define imageView
                ImageViewAsync imageView = DefineImageView.GetImageView(context, lp);

                ImageService
                    .LoadUrl(movieUri)
                    .FadeAnimation(true)
                    .DownSample(width:150)
                    .Into(imageView);
                #endregion

                //add click event to ImageView
                #region click event on ImageView
                imageView.Click += delegate
                {
                    var detailActivity = new Intent(context, typeof(DetailActivity));
                    detailActivity.PutExtra("movieId", movieObject["id"].ToString());
                    context.StartActivity(detailActivity);
                };
                #endregion 

                //add imageView to movieList
                movieList.AddView(imageView);
            }

            return movieList;
        }
    }
}