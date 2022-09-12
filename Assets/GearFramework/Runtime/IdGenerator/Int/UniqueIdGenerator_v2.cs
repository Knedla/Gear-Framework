using System;
using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Unique Id generator on runtime level. Does not persist through game sessions.
    /// </summary>
    public class UniqueIdGenerator_v2
    {
        static Dictionary<Type, int> lastAssignedIds; // expected scenario: int.MaxValue will never be used
        static UniqueIdGenerator_v2() => lastAssignedIds = new Dictionary<Type, int>();
        /// <summary>
        /// Get next available Id
        /// </summary>
        /// <typeparam name="T">Type level Id</typeparam>
        /// <returns>Unique Id</returns>
        public static int GetNextId<T>()
        {
            Type type = typeof(T);
            int lastAssignedId;

            lastAssignedIds.TryGetValue(type, out lastAssignedId);
            lastAssignedIds[type] = ++lastAssignedId;

            return lastAssignedId;
        }
    }
}
