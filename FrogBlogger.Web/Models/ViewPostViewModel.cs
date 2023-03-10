using System.Collections.Generic;
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

        /// <summary>
        /// Gets the total number of user ratings for a post
        /// </summary>
        public int TotalRatings
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
        /// <param name="totalRatings">Specifies the total number of user ratings for the current post</param>
        public ViewPostViewModel(BlogPost blogPost, IList<UserComment> comments, int averageRating, int totalRatings)
        {
            Post = blogPost;
            Comments = comments;
            AverageRating = averageRating;
            TotalRatings = totalRatings;
        }

        #endregion
    }
}