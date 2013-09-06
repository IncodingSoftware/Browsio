namespace Browsio.Domain
{
    public class SearchItemVm
    {
        #region Constructors

        public SearchItemVm(string message, SearchItemOfType Type, string groupId)
        {
            this.IsHeader = true;
            this.Message = message;
            this.Id = groupId;
            this.Type = Type;
        }

        public SearchItemVm(SearchItem searchItem)
        {
            this.Id = searchItem.Id.ToString();
            this.OwnerId = searchItem.OwnerId;
            this.IsHeader = false;
            this.Message = searchItem.Query;
            this.Type = searchItem.Type;
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        public string OwnerId { get; set; }

        public bool IsHeader { get; set; }

        public string Message { get; set; }

        public SearchItemOfType Type { get; set; }

        #endregion
    }
}