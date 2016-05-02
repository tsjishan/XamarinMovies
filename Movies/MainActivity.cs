using Android.App;
using Android.Widget;
using Android.OS;
using Movies.Utilities;
using Android.Support.V7.App;

namespace Movies
{
    [Activity(MainLauncher = true) ]
    public class MainActivity : ActionBarActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //set toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Movie Explorer";

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
            await PopulateMovie.populateMovieList(topRatedMovieUri, topRatedMovies, this);
            await PopulateMovie.populateMovieList(popularUri, popularMovies, this);
            await PopulateMovie.populateMovieList(nowPlayingUri, nowPlayingMovies, this);
        }
    }
}

