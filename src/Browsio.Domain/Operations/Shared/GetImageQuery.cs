namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using Incoding.CQRS;
    using Incoding.Data;

    #endregion

    public class GetImageQuery : QueryBase<byte[]>
    {
        #region Properties

        public string Id { get; set; }

        public SearchItemOfType Type { get; set; }

        #endregion

        protected override byte[] ExecuteResult()
        {
            IImageEntity image = null;
            switch (Type)
            {
                case SearchItemOfType.Product:
                    image = this.Repository.GetById<Product>(Id);
                    break;
                case SearchItemOfType.Store:
                    image = this.Repository.GetById<Store>(Id);
                    break;
                case SearchItemOfType.User:
                    image = this.Repository.GetById<User>(Id);
                    break;
                ////ncrunch: no coverage start
                default:
                    return null;
                ////ncrunch: no coverage end        
            }

            return image.Image;
        }
    }
}