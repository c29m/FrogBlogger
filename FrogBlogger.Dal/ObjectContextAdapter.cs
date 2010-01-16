using System;
using System.Data.Objects;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Creates an adapter for the Entity Framework ObjectContext type
    /// </summary>
    public class ObjectContextAdapter : IObjectContext
    {
        #region Fields

        /// <summary>
        /// An Entity Framework ObjectContext
        /// </summary>
        private readonly ObjectContext _context;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ObjectContextAdapter class
        /// </summary>
        public ObjectContextAdapter()
            : this(DatabaseUtility.GetContext())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ObjectContextAdapter class
        /// </summary>
        /// <param name="context">Entity Framework context</param>
        public ObjectContextAdapter(ObjectContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes an ObjectSet that is used to perform create, read, update, and delete operations. 
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The IObjectSet to query against</returns>
        public IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            return _context.CreateObjectSet<T>();
        }

        /// <summary>
        /// Persists all updates to the data source and resets change tracking in the object context.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.ObjectContextAdapter
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.ObjectContextAdapter
        /// </summary>
        /// <param name="disposing">Indicates whether or not the object is disposing of managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        #endregion
    }
}
