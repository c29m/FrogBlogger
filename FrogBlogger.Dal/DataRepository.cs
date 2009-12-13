using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Implements IDataRepository to create a data repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        #region Fields

        /// <summary>
        /// Stores the default object context
        /// </summary>
        private ObjectContext _context;

        /// <summary>
        /// Stores the default object set
        /// </summary>
        private IObjectSet<T> _objectSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataRepository class
        /// </summary>
        public DataRepository()
            : this(DatabaseUtility.GetContext())
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataRepository class
        /// </summary>
        /// <param name="context">The Entity Framework ObjectContext</param>
        public DataRepository(ObjectContext context)
        {
            _context = context;
            _objectSet = _context.CreateObjectSet<T>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Attaches the entity to a context
        /// </summary>
        /// <param name="Entity">Entity to attach</param>
        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        public void Create(T entity)
        {
            _objectSet.AddObject(entity);
        }

        /// <summary>
        /// Fetches an IQueryable object of type T
        /// </summary>
        /// <returns>An IQueryable object of type T</returns>
        public IQueryable<T> Fetch()
        {
            return _objectSet;
        }

        /// <summary>
        /// Fetches an IEnumerable object using the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to search on</param>
        /// <returns>An IEnumerable object containing the results of the query</returns>
        public IEnumerable<T> Fetch(Func<T, bool> predicate)
        {
            return (from x in _objectSet.Where(predicate) select x).AsEnumerable();
        }

        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _objectSet.Where<T>(predicate);
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public T Single(Func<T, bool> predicate)
        {
            return _objectSet.Single<T>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public T First(Func<T, bool> predicate)
        {
            return _objectSet.First<T>(predicate);
        }

        /// <summary>
        /// Deletes the supplied entity
        /// </summary>
        /// <param name="entity">Entitiy object to delete</param>
        public void Delete(T entity)
        {
            _objectSet.DeleteObject(entity);
        }

        /// <summary>
        /// Deletes any entities matching the supplied criteria
        /// </summary>
        /// <param name="predicate">Criteria used to determine which records to delete</param>
        public void Delete(Func<T, bool> predicate)
        {
            IEnumerable<T> records = from x in _objectSet.Where(predicate) select x;

            foreach (T record in records)
            {
                Delete(record);
            }
        }

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.DataRepository
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.DataRepository
        /// </summary>
        /// <param name="disposing">Indicates whether the current object is disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        /// <param name="options">Specifies the behavior of the object context</param>
        public void SaveChanges(SaveOptions options)
        {
            _context.SaveChanges(options);
        }

        #endregion
    }
}
