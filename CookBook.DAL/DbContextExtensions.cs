using System.Data.Entity;

namespace CookBook.DAL
{
    public static class DbContextExtensions
    {
        public static void Clear<T>(this IDbSet<T> dbSet, DbContext dbContext) where T : class
        {
            foreach (var item in dbSet)
                dbSet.Remove(item);
            dbContext.SaveChanges();
        }
    }
}