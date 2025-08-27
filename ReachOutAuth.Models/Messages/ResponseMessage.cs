using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// The Messages namespace.
/// </summary>
namespace ReachOutAuth.Models.Messages
{
    /// <summary>
    /// Class ResponseMessage.
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// True or false
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// If Success is false, MsgCode will contain a corresponding code for this error
        /// </summary>
        /// <value>The MSG code.</value>
        public string MsgCode { get; set; }

        /// <summary>
        /// If Success is false, Message will contain the faulting value
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
    }
}
