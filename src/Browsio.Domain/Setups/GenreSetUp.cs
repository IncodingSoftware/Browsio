namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    ////ncrunch: no coverage start
    public class GenreSetUp : ISetUp
    {
        #region Fields

        readonly IDispatcher dispatcher;

        #endregion

        #region Constructors

        public GenreSetUp(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #endregion

        #region ISetUp Members

        public int GetOrder()
        {
            return 2;
        }

        public void Execute()
        {
            if (this.dispatcher.Query(new GetEntitiesQuery<Genre>()).Any())
                return;

            this.dispatcher.Push(new AddGenreCommand { Name = "Fantastic", Category = CategoryOfType.Book });
            this.dispatcher.Push(new AddGenreCommand { Name = "Thriller", Category = CategoryOfType.Book });
            this.dispatcher.Push(new AddGenreCommand { Name = "Comedy", Category = CategoryOfType.Book });

            this.dispatcher.Push(new AddGenreCommand { Name = "Scary", Category = CategoryOfType.Movie });
            this.dispatcher.Push(new AddGenreCommand { Name = "Documentation", Category = CategoryOfType.Movie });
            this.dispatcher.Push(new AddGenreCommand { Name = "History", Category = CategoryOfType.Movie });

            this.dispatcher.Push(new AddGenreCommand { Name = "Music", Category = CategoryOfType.TVShow });
            this.dispatcher.Push(new AddGenreCommand { Name = "News", Category = CategoryOfType.TVShow });

            this.dispatcher.Push(new AddGenreCommand { Name = "Action", Category = CategoryOfType.VideoGame });
            this.dispatcher.Push(new AddGenreCommand { Name = "Strategy", Category = CategoryOfType.VideoGame });
            this.dispatcher.Push(new AddGenreCommand { Name = "Arcade", Category = CategoryOfType.VideoGame });
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}

////ncrunch: no coverage end