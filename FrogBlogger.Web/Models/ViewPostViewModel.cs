using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FrogBlogger.Dal;

namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Defines the ViewModel used on the ViewPost page
    /// </summary>
    public class ViewPostViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the BlogPost that wil be displayed
        /// </summary>
        public BlogPost Post
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a reference to a list of comments for the blog post
        /// </summary>
        public IList<UserComment> Comments
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the average rating for a post
        /// </summary>
        public int AverageRating
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ViewPostViewModel class
        /// </summary>
        public ViewPostViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ViewPostViewModel class
        /// </summary>
        /// <param name="blogPost">The BlogPost to view</param>
        /// <param name="comments">Any user comments associated with the blog post</param>
        /// <param name="averageRating">Specifies the average user rating for the post</param>
        public ViewPostViewModel(BlogPost blogPost, IList<UserComment> comments, int averageRating)
        {
            Post = blogPost;
            Comments = comments;
            AverageRating = averageRating;
        }

        #endregion
    }
}