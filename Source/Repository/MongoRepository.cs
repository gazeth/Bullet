namespace Bullet.Repository {
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Bullet.Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;

    /// <summary>
    /// Concrete implementation of IRepository
    /// </summary>
    /// <typeparam name="T">The object type of this repository</typeparam>
    public class MongoRepository<T> : IRepository<T> where T : IEntity {

        /// <summary>
        /// The Mongo Collection
        /// </summary>
        private MongoCollection<T> collection;

        /// <summary>
        /// The mongo database
        /// </summary>
        private MongoDatabase store;

        /// <summary>
        /// The default constructor, uses the default connection string settings
        /// </summary>
        /// <remarks>The default name for the connection string setting is "MongoConnection"</remarks>
        public MongoRepository() : this(Helpers.DefaultConnectionString()) {
        }

        /// <summary>
        /// Initialises a new mongo database and collection
        /// </summary>
        /// <param name="connectionString">the connection string to use</param>
        public MongoRepository(string connectionString) {
            OpenStore(new MongoUrl(connectionString));
            this.collection = store.GetCollection<T>(Helpers.WithTheName<T>());
        }

        /// <summary>
        /// Returns the T by it's given id (ObjectId)
        /// </summary>
        /// <param name="id">The string representation of the ObjectId</param>
        /// <returns>T</returns>
        public T WithId(string id) {

            /*Check for subtype*/
            if (typeof(T).IsSubclassOf(typeof(Entity))) {
                return this.collection.FindOneByIdAs<T>(new ObjectId(id));
            }

            return this.collection.FindOneByIdAs<T>(id);
        }

        /// <summary>
        /// Returns the T that matches the criteria passed in
        /// </summary>
        /// <param name="criteria">The expression to match against</param>
        /// <returns>T</returns>
        public T ThatMatches(Expression<Func<T, bool>> criteria) {
            return this.collection.AsQueryable<T>().Where(criteria).FirstOrDefault();
        }

        /// <summary>
        /// Returns all the documents of T
        /// </summary>
        /// <returns>IList&lt;T&gt;</returns>
        public IList<T> All() {
            return this.collection.FindAll().ToList();
        }

        /// <summary>
        /// Returns all the documents of T that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>IList&lt;T&gt;</returns>
        public IList<T> AllThatMatch(Expression<Func<T, bool>> criteria) {
            return this.collection.AsQueryable<T>().Where(criteria).ToList();
        }

        /// <summary>
        /// Adds the new entity of T into the store
        /// </summary>
        /// <param name="entity">The new entity</param>
        /// <returns>T</returns>
        public T Add(T entity) {            
            SafeModeResult result = this.collection.Insert<T>(entity);

            return entity;
        }

        /// <summary>
        /// Adds all new entities of T in the list into the store
        /// </summary>
        /// <param name="entities">The list of entities</param>
        public void Add(IList<T> entities) {            
            this.collection.InsertBatch<T>(entities);
        }

        /// <summary>
        /// Upserts an entity (inserts if it doesn't already exist)
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>T</returns>
        public T Update(T entity) {
            this.collection.Save<T>(entity);
            return entity;
        }

        /// <summary>
        /// Upserts all the entities of T in the list (inserts if it doesn't already exist)
        /// </summary>
        /// <param name="entities">The list of entities</param>
        public void Update(IList<T> entities) {
            foreach (T entity in entities) {
                this.collection.Save<T>(entity);
            }
        }

        /// <summary>
        /// Removes the entity from the store
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        public void Delete(T entity) {
            /*Check for subtype*/
            if (typeof(T).IsSubclassOf(typeof(Entity))) {
                this.collection.Remove(Query.EQ("_id", new ObjectId(entity.Id)));
            } else {
                this.collection.Remove(Query.EQ("_id", entity.Id));
            }
        }

        /// <summary>
        /// Removes all the entities of T in the list from the store
        /// </summary>
        /// <param name="entities">The list of entities to remove</param>
        public void Delete(IList<T> entities) {
            foreach (T entity in entities) {
                this.Delete(entity);
            }
        }

        /// <summary>
        /// Removes all entites that match the criteria from the store
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        public void Delete(Expression<Func<T, bool>> criteria) {
            foreach (T entity in this.collection.AsQueryable<T>().Where(criteria)) {
                this.Delete(entity);
            }
        }

        /// <summary>
        /// Removes all entities from the store
        /// </summary>
        public void DeleteAll() {
            this.collection.RemoveAll();
        }

        /// <summary>
        /// Returns a count of all entities in the store
        /// </summary>
        /// <returns>int</returns>
        public long Count() {
            return this.collection.Count();
        }

        /// <summary>
        /// Returns the count of entities that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>int</returns>
        public long Count(Expression<Func<T, bool>> criteria) {
            return this.collection.AsQueryable<T>().Where(criteria).Count();
        }

        /// <summary>
        /// Whether or not an entity or entities exist that match the criteria passed in
        /// </summary>
        /// <param name="criteria">The criteria to match against</param>
        /// <returns>bool</returns>
        public bool Exists(Expression<Func<T, bool>> criteria) {
            return this.collection.AsQueryable<T>().Any(criteria);
        }

        /// <summary>
        /// Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url of the database</param>
        private void OpenStore(MongoUrl url) {
            var server = MongoServer.Create(url.ToServerSettings());
            this.store = server.GetDatabase(url.DatabaseName);
        }
    }
}
