
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Movies.Utilities;
using Android.Support.V7.App;
using System.Json;
using FFImageLoading.Views;
using FFImageLoading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Movies
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : ActionBarActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Detail);

            //set toolbar
            #region
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Movie Explorer";
            #endregion

            //get views and layouts
            LinearLayout movieImage = FindViewById<LinearLayout>(Resource.Id.image);
            TextView movieName = FindViewById<TextView>(Resource.Id.movieName);
            TextView movieDate = FindViewById<TextView>(Resource.Id.movieDate);
            TextView movieIntro = FindViewById<TextView>(Resource.Id.movieIntro);
            RatingBar movieRating = FindViewById<RatingBar>(Resource.Id.movieRating);
            TextView movieVotes = FindViewById<TextView>(Resource.Id.movieVotes);


            //get the movie id
            string movieId = Intent.GetStringExtra("movieId");

            //baseUri and key
            string baseUri = "http://api.themoviedb.org/3/movie";
            string api_key = "?api_key=ab41356b33d100ec61e6c098ecc92140";

            //get movie object
            #region
            string uri = baseUri + "/" + movieId + api_key;
            JsonValue jsonValue = await FetchMoviesAsync.FetchMoviesAsyncCall(uri);
            JObject movieObject = (JObject)JsonConvert.DeserializeObject(jsonValue.ToString());
            #endregion

            //populate image
            string imageUri = "http://image.tmdb.org/t/p/original" + movieObject["backdrop_path"];
            LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(350, 450);
            ImageViewAsync imageView = DefineImageView.GetImageView(this, lp);

            ImageService
                .LoadUrl(imageUri)
                .FadeAnimation(true)
                .DownSample(width: 150)
                .Into(imageView);

            movieImage.AddView(imageView);

            //populate the name text
            movieName.Text = movieObject["title"].ToString();

            //populate the release date
            movieDate.Text = "Release Date: " + movieObject["release_date"].ToString();

            //populate the rating
            movieRating.Rating = (float)movieObject["vote_average"]/2;

            //populate the votes
            movieVotes.Text = "(from " + movieObject["vote_count"]  + " votes)";

            //populate the description
            movieIntro.Text = movieObject["overview"].ToString();

            //add click event to the button

            #region similarMovie
            //get the similarMovies linearlayout
            LinearLayout similarMovies = FindViewById<LinearLayout>(Resource.Id.similarMovies);

            //get the similar uri and populate the similar moives
            string similarUri = baseUri + "/" + movieId + "/similar" + api_key;
            await PopulateMovie.populateMovieList(similarUri, similarMovies, this);
            #endregion
        }
    }
}