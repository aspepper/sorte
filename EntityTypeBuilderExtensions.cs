using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sorte.Dal
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> ConfigureTrackable<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureMultiTenant<TEntity>(this EntityTypeBuilder<TEntity> builder, SorteContext context)
            where TEntity : class
        {
            return builder.ConfigureTrackable();
        }
    }

}
