using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrogBlogger.Dal.Interfaces
{
    /// <summary>
    /// Defines the interface for a unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves changes in the current context
        /// </summary>
        void SaveChanges();
    }
}
