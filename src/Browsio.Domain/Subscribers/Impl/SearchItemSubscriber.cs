namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.Data;
    using Incoding.EventBroker;

    #endregion

    public class SearchItemSubscriber : IEventSubscriber<OnChangeSearchItemEvent>
    {
        #region Fields

        readonly IRepository repository;

        #endregion

        #region Constructors

        public SearchItemSubscriber(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region IEventSubscriber<OnSearchItemEvent> Members

        public void Subscribe(OnChangeSearchItemEvent @event)
        {
            var searchItem = this.repository.Query(whereSpecification: new SearchItemByOwnerWhereSpec(@event.OwnerId))
                                 .FirstOrDefault();

            if (searchItem == null)
            {
                this.repository.Save(new SearchItem
                                         {
                                                 OwnerId = @event.OwnerId, 
                                                 Query = @event.Query, 
                                                 Type = @event.Type
                                         });
            }
            else
                searchItem.Query = @event.Query;
        }
        
        #endregion

        ////ncrunch: no coverage start
        #region Disposable

        public void Dispose() { }

        #endregion

        ////ncrunch: no coverage end    
    }
}