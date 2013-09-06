namespace Browsio.Domain
{
    public class AmazonConfig : IAmazonConfig
    {
        public string AmazonStoreId { get; private set; }

        public AmazonConfig(string amazonStoreId)
        {
            AmazonStoreId = amazonStoreId;
        }
    }
}