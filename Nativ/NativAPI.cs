using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractiveObject.Nativ
{
    public class NativAPI
    {

        #region events
        /// <summary>
        /// Event raised when the Title property changes
        /// </summary>
        public event EventHandler FlipEvent;

        /// <summary>
        /// Raises the Title property changed event
        /// </summary>
        /// <param name="refOldValue">Old property value</param>
        /// <param name="refNewValue">New property value</param>
        protected void RaiseFlipEvent()
        {
            if (FlipEvent != null)
            {
                FlipEvent(this, new EventArgs());
            }
        }
        #endregion

        #region Sigleton
        private static NativAPI instance;

        private NativAPI() { }

        public static NativAPI Instance
       {
          get 
          {
             if (instance == null)
             {
                 instance = new NativAPI();
             }
             return instance;
          }
       }
        #endregion

        #region Function

        public string HelloWorld()
        {
            return "Hello from C#";
        }

        public void Flip()
        {
            RaiseFlipEvent();
        }
        #endregion

    }
}
