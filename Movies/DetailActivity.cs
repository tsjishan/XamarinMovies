using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Movies.Utilities;

namespace Movies
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Detail);

            //get the movie id
            string movieId = Intent.GetStringExtra("movieId");

            //baseUri and key
            string baseUri = "http://api.themoviedb.org/3/movie";
            string api_key = "?api_key=ab41356b33d100ec61e6c098ecc92140";

            //get the similarMovies linearlayout
            LinearLayout similarMovies = FindViewById<LinearLayout>(Resource.Id.similarMovies);

            //get the similar uri and populate the similar moives
            string similarUri = baseUri + "/" + movieId + "/similar" + api_key;
            await PopulateMovie.populateMovieList(similarUri, similarMovies, this);


            string id = Intent.GetStringExtra("movieId");
            string uri = baseUri + "/" + id + api_key;

//            FetchMoviesAsync.FetchMoviesAsyncCall(uri);
        }
    }
}