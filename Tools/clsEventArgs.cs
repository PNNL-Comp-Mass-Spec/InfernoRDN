using System;

namespace DAnTE.Tools
{

    /// <summary>
    /// Message event args
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Message.
        /// </summary>
        public readonly string Message;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">
        /// Message.
        /// </param>
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }

        #endregion
    }

    /// <summary>
    /// The progress event args.
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// Value between 0 and 100
        /// </summary>
        public readonly double PercentComplete;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEventArgs"/> class.
        /// </summary>
        /// <param name="percentComplete">
        /// Percent complete.
        /// </param>
        public ProgressEventArgs(double percentComplete)
        {
            this.PercentComplete = percentComplete;
        }

        #endregion
    }

}