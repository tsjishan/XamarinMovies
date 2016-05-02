using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using Android.Graphics;
using Movies.Utilities;

namespace Movies
{
    [Activity(MainLauncher = true) ]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //set Urls to retrieve movies
            string baseUri = "http://api.themoviedb.org/3/movie";
            string api_key = "?api_key=ab41356b33d100ec61e6c098ecc92140";

            string topRatedMovieUri = baseUri + "/top_rated" + api_key;
            string popularUri = baseUri + "/popular" + api_key;
            string nowPlayingUri = baseUri + "/now_playing" + api_key;

            //get corresponding linear layouts
            LinearLayout topRatedMovies = FindViewById<LinearLayout>(Resource.Id.topRatedMovies);
            LinearLayout popularMovies = FindViewById<LinearLayout>(Resource.Id.popularMovies);
            LinearLayout nowPlayingMovies = FindViewById<LinearLayout>(Resource.Id.nowPlayingMovies);

            //populate movie list
            await populateMovieList(topRatedMovieUri, topRatedMovies);
            await populateMovieList(popularUri, popularMovies);
            await populateMovieList(nowPlayingUri, nowPlayingMovies);
        }

        //retrieve movies to movie list
        private async Task<LinearLayout> populateMovieList(string url, LinearLayout movieList)
        {
            string imageUri = "https://image.tmdb.org/t/p/original";
            JsonValue jsonValue = await FetchMoviesAsync.FetchMoviesAsyncCall(url);
            var results = jsonValue["results"];
            var i = 0;
            foreach (JsonObject movieObject in results)
            {
                Android.Net.Uri androidUri = Android.Net.Uri.Parse(imageUri + movieObject["poster_path"]);

                //layout parameters
                LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(400, 700);
                lp.LeftMargin = 10;
                lp.RightMargin = 10;

                //define ImageView
                ImageView imageView = new ImageView(this);
                imageView.LayoutParameters = lp;
                imageView.Visibility = ViewStates.Visible;
                var imageBitmap = GetImageBitmap.GetImageBitmapFromUri(imageUri + movieObject["poster_path"]);
                imageView.SetImageBitmap(imageBitmap);

                //add click event to ImageView
                imageView.Click += delegate
                {
                    var detailActivity = new Intent(this, typeof(DetailActivity));
                    detailActivity.PutExtra("movieObject", (int)movieObject["id"]);
                    StartActivity(detailActivity);
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

