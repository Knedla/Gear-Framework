namespace UnityEngine
{
    /// <summary>
    /// Unique Id generator on runtime level. Does not persist through game sessions.
    /// </summary>
    public class UniqueIdGenerator
    {
        /// <summary>
        /// Get next available Id
        /// </summary>
        /// <returns>Unique Id</returns>
        public static int GenerateId() => UniqueIdGenerator<UniqueIdGenerator>.GetNextId();
    }
}
