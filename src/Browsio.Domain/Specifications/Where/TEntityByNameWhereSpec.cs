using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Browsio.Domain
{
    using System.Linq.Expressions;
    using Incoding;

    public class TEntityByNameWhereSpec<TEntity> : Specification<TEntity> where TEntity : INameEntity
    {

        readonly string Name;

        public TEntityByNameWhereSpec(string Name)
        {
            this.Name = Name;
        }

        public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return tEntity => tEntity.Name.Contains(this.Name);
        }
    }
}