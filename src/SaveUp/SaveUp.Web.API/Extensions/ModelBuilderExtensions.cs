using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SaveUp.Web.API.Extensions;

public static class ModelBuilderExtensions
{

    public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        var entities = modelBuilder.Model
            .GetEntityTypes()
            .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
            .Select(e => e.ClrType);

        foreach (var entity in entities)
        {
            var filterValues = Expression.Parameter(entity);
            var filterExpression = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), filterValues, expression.Body);
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(filterExpression, filterValues));
        }
    }
}