using System;
using System.Collections.Generic;
using System.Linq;

namespace FrogBlogger.Dal.Interfaces
{
    /// <summary>
    /// Defines a contract for a generic data repository
    /// </summary>
    /// <typeparam name="T">The entity type for which to query with</typeparam>
    public interface IDataRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Attaches the entity to a context
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        void Attach(T entity);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        void Create(T entity);

        /// <summary>
        /// Fetches an IQueryable object of type T
        /// </summary>
        /// <returns>An IQueryable object of type T</returns>
        IQueryable<T> Fetch();

        /// <summary>
        /// Fetches an IEnumerable object using the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to search on</param>
        /// <returns>An IEnumerable object containing the results of the query</returns>
        IEnumerable<T> Fetch(Func<T, bool> predicate);

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        T GetSingle(Func<T, bool> predicate);

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        T First(Func<T, bool> predicate);

        /// <summary>
        /// Deletes the supplied entity
        /// </summary>
        /// <param name="entity">Entitiy object to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes any entities matching the supplied criteria
        /// </summary>
        /// <param name="predicate">Criteria used to determine which records to delete</param>
        void Delete(Func<T, bool> predicate);

        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        void SaveChanges();
    }
}
