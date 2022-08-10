using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class SortHelper<T>
    {
        public static IEnumerable<T> SortT(IEnumerable<T> sortList, string parameter)
        {
            var isAscending = GetIsAscending(parameter);

            string param = string.Empty;
            if (parameter.Length > 0)
            {
                param = parameter[1..];
            }
            else
            {
                return null;
            }

            var pi = typeof(T).GetProperty(param);

            if (pi != null)
                sortList = isAscending ? sortList.OrderBy(a => pi.GetValue(a, null)) : sortList.OrderByDescending(a => pi.GetValue(a, null));

            return sortList;
        }

        public static IQueryable<T> OrderByDynamic(IQueryable<T> query, string attribute)
        {
            try
            {
                var isAscending = GetIsAscending(attribute);
                string parameter = string.Empty;
                if (attribute.Length > 0)
                {
                    parameter = attribute[1..];
                }
                else
                {
                    return query;
                }
                string orderMethodName = isAscending ? "OrderBy" : "OrderByDescending";
                Type t = typeof(T);

                var newParameter = char.ToUpper(parameter[0]) + parameter.Substring(1);
                var param = Expression.Parameter(t, newParameter);
                var property = t.GetProperty(newParameter);

                return query.Provider.CreateQuery<T>(
                    Expression.Call(
                        typeof(Queryable),
                        orderMethodName,
                        new Type[] { t, property.PropertyType },
                        query.Expression,
                        Expression.Quote(
                            Expression.Lambda(
                                Expression.Property(param, property),
                                param))
                    ));
            }
            catch (Exception)
            {
                return query;
            }
        }

        private static bool GetIsAscending(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                return false;
            char sign = parameter[0];

            if (sign == '-')
                return false;
            else
                return true;
        }
    }
}
