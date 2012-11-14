namespace Bullet.Repository {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Bullet.Entities;
    using System.Linq.Expressions;

    /// <summary>
    /// Main Interface Definition
    /// </summary>
    /// <typeparam name="T">The object type of this repository</typeparam>
    public interface IRepository<T> where T : IEntity {

        /// <summary>
        /// Returns the T by it's given id (ObjectId)
        /// </summary>
        /// <param name="id">The string representation of the ObjectId</param>
        /// <returns>T</returns>
        T WithId(string id);

        /// <summary>
        /// Returns the T that matches the criteria passed in
        /// </summary>
        /// <param name="criteria">The expression to match against</param>
        /// <returns>T</returns>
        T ThatMatches(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Returns all the documents of T
        /// </summary>
        /// <returns>IList&lt;T&gt;</returns>
        IList<T> All();

        /// <summary>
        /// Paged list of T
        /// </summary>
        /// <param name="page">The page of documents to return</param>
        /// <param name="take">The page size, no. of documents to return</param>
        /// <returns>List of T</returns>
        IList<T> All(int page, int take);

        /// <summary>
        /// Paged list of T
        /// </summary>
        /// <param name="skip">The number of documents to skip (page)</param>
        /// <param name="take">The page size, no. of documents to return</param>
        /// <param name="sort">Property to sort the results by</param>
        /// <returns>List of T</returns>
        IList<T> All(int skip, int take, Func<T, string> sort);

        /// <summary>
        /// Returns all the documents of T that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>IList&lt;T&gt;</returns>
        IList<T> AllThatMatch(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Adds the new entity of T into the store
        /// </summary>
        /// <param name="entity">The new entity</param>
        /// <returns>T</returns>
        T Add(T entity);

        /// <summary>
        /// Adds all new entities of T in the list into the store
        /// </summary>
        /// <param name="entities">The list of entities</param>
        void Add(IList<T> entities);

        /// <summary>
        /// Upserts an entity (inserts if it doesn't already exist)
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>T</returns>
        T Update(T entity);

        /// <summary>
        /// Upserts all the entities of T in the list (inserts if it doesn't already exist)
        /// </summary>
        /// <param name="entities">The list of entities</param>
        void Update(IList<T> entities);

        /// <summary>
        /// Removes the entity from the store
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        void Delete(T entity);

        /// <summary>
        /// Removes all the entities of T in the list from the store
        /// </summary>
        /// <param name="entities">The list of entities to remove</param>
        void Delete(IList<T> entities);

        /// <summary>
        /// Removes all entites that match the criteria from the store
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        void Delete(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Removes all entities from the store
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Returns a count of all entities in the store
        /// </summary>
        /// <returns>int</returns>
        long Count();

        /// <summary>
        /// Returns the count of entities that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>int</returns>
        long Count(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Whether or not an entity or entities exist that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>bool</returns>
        bool Exists(Expression<Func<T, bool>> criteria);

        /// <summary>
        /// Increment the property by the amount specified
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="property">which property to update</param>
        /// <param name="by">The amount the increment by</param>
        /// <returns>The updated entity</returns>
        T Increment(T entity, Expression<Func<T, double>> property, double by);
    }
}
