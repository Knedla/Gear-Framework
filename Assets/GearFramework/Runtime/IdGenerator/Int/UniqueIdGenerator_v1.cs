namespace UnityEngine
{
    /// <summary>
    /// Unique Id generator on runtime level. Does not persist through game sessions.
    /// </summary>
    /// <typeparam name="T">Type level Id</typeparam>
    public class UniqueIdGenerator<T>
    {
        /// <summary>
        /// Get next available Id
        /// </summary>
        /// <returns>Unique Id</returns>
        public static int GetNextId() => Instance.GetNextId_();
        static UniqueIdGenerator<T> instance;
        static UniqueIdGenerator<T> Instance
        {
            get
            {
                if (instance == null)
                    instance = new UniqueIdGenerator<T>();
                
                return instance;
            }
        }
        int lastAssignedId; // expected scenario: int.MaxValue will never be used
        int GetNextId_() => ++lastAssignedId;
    }
}
