using System;
using System.Collections.Generic;

namespace MyEventTracker.Core
{
    /// <summary>
    /// Generic game event with key-value pairs as parameters
    /// </summary>
    [Serializable]
    public class GameEvent
    {
        /// <summary>
        /// Identifier of the event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Event parameters
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
