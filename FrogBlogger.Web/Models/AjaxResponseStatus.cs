namespace FrogBlogger.Web.Models
{
    /// <summary>
    /// Contains properties for returning an AJAX response to the client
    /// </summary>
    public class AjaxResponseStatus
    {
        #region Properties

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AjaxReponseStatus class
        /// </summary>
        public AjaxResponseStatus()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AjaxReponseStatus class
        /// </summary>
        /// <param name="status">Specifies the status of the operation</param>
        public AjaxResponseStatus(string status)
        {
            Status = status;
        }

        #endregion
    }
}