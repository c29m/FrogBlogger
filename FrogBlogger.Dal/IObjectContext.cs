using System;
using System.Data.Objects;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Defines the contract for an ObjectContext to be used as an adapter for other ObjectContext types
    /// </summary>
    public interface IObjectContext : IDisposable
    {
        /// <summary>
        /// Initializes an ObjectSet that is used to perform create, read, update, and delete operations. 
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The ObjectSet to query against</returns>
        IObjectSet<T> CreateObjectSet<T>() where T : class;

        /// <summary>
        /// Persists all updates to the data source and resets change tracking in the object context.
        /// </summary>
        void SaveChanges();
    }
}
