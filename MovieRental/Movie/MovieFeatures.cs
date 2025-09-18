using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        /*
		 There is no Filtering or Pagination, which could lead to performance issues if the dataset is large especially without the use of asnotracking.
		there is no error handling, which could lead to unhandled exceptions if the database query fails.
		 */
        public List<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}


	}
}
