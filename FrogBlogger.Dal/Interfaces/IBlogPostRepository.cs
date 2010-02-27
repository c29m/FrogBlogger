using System;
using System.Collections.Generic;
using System.Linq;

namespace FrogBlogger.Dal.Interfaces
{
    /// <summary>
    /// Defines the contract for a BlogPost repository
    /// </summary>
    public interface IBlogPostRepository : IDisposable
    {
        /// <summary>
        /// Attaches the entity to a context
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        void Attach(BlogPost entity);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        void Create(BlogPost entity);

        /// <summary>
        /// Fetches an IQueryable object of type BlogPost
        /// </summary>
        /// <returns>An IQueryable object of type BlogPost</returns>
        IQueryable<BlogPost> Fetch();

        /// <summary>
        /// Fetches an IEnumerable object using the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to search on</param>
        /// <returns>An IEnumerable object containing the results of the query</returns>
        IEnumerable<BlogPost> Fetch(Func<BlogPost, bool> predicate);

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        BlogPost GetSingle(Func<BlogPost, bool> predicate);

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        BlogPost First(Func<BlogPost, bool> predicate);

        /// <summary>
        /// Deletes the supplied entity
        /// </summary>
        /// <param name="entity">Entitiy object to delete</param>
        void Delete(BlogPost entity);

        /// <summary>
        /// Deletes any entities matching the supplied criteria
        /// </summary>
        /// <param name="predicate">Criteria used to determine which records to delete</param>
        void Delete(Func<BlogPost, bool> predicate);

        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        void SaveChanges();
    }
}
