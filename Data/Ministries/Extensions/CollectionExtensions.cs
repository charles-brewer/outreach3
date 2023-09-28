
    using System;
    using System.Collections.Generic;
    using System.Linq;
namespace outreach3.Data.Ministries.Extensions
{
    public static class CollectionExtensions
    {
        public static void RemoveAll<T>(this ICollection<T> @this, Func<T, bool> predicate)
        {
            List<T> list = @this as List<T>;

            if (list != null)
            {
                list.RemoveAll(new Predicate<T>(predicate));
            }
            else
            {
                List<T> itemsToDelete = @this
                    .Where(predicate)
                    .ToList();

                foreach (var item in itemsToDelete)
                {
                    @this.Remove(item);
                }
            }
        }
    }
}