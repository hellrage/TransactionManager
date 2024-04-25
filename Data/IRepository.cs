namespace TransactionManager
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds the given entity to storage, overwriting in case of id conflict.
        /// </summary>
        /// <param name="entity">Entity instance to save.</param>
        public void Add(T entity);

        /// <summary>
        /// Fetch the entity with the given id, if it exists.
        /// </summary>
        /// <param name="id">Id of the entity to fetch.</param>
        /// <returns>Entity with the given id. Null if none exist.</returns>
        public T Get(int id);

        /// <summary>
        /// Checks whether the given id exists in storage.
        /// </summary>
        /// <param name="id">Id of the transaction to check</param>
        /// <returns>True if the given id exists in storage, false otherwise</returns>
        public bool IdExists(int id);
    }
}
