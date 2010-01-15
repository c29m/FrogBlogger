using System;
using FrogBlogger.Dal.Interfaces;

namespace FrogBlogger.Dal
{
    /// <summary>
    /// Implements IUnitOfWork to create a wrapper around an IObjectContext
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields

        /// <summary>
        /// Stores the object context used by the UnitOfWork
        /// </summary>
        private readonly IObjectContext _objectContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents an instance of the UnitOfWork class from being instantiated
        /// </summary>
        private UnitOfWork()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class
        /// </summary>
        /// <param name="objectContext">The object context</param>
        public UnitOfWork(IObjectContext objectContext)
        {
            _objectContext = objectContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        public void SaveChanges()
        {
            _objectContext.SaveChanges();
        }

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.UnitOfWork class
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Disposes all resources used by the FrogBlogger.Dal.UnitOfWork
        /// </summary>
        /// <param name="disposing">Indicates whether or not the object is disposing of managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _objectContext.Dispose();
            }
        }

        #endregion
    }
}
