using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class BaseProvider
    {
        public const int TotalDataPerPage = 2;

        protected static int GetHalaman(int totalData)
        {
            int totalHalaman = (int)Math.Ceiling(totalData / (decimal)TotalDataPerPage);
            return totalHalaman;
        }

        protected static int GetSkip(int page)
        {
            int skip = (TotalDataPerPage * (page - 1));
            return skip;

        }

        public static void MapingModel<TDest, TSource>(TDest destination, TSource source)
        where TDest : class
        where TSource : class
        {
            var destinationProperties = destination.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var property = destinationProperties.FirstOrDefault(a => a.Name == sourceProperty.Name);
                if (property != null)
                {
                    property.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }
    }
}
