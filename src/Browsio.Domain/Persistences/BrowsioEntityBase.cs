namespace Browsio.Domain
{
    using System;
    using Incoding.Data;

    public class BrowsioEntityBase:IncEntityBase
    {
        public BrowsioEntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}