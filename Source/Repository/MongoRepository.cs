

namespace Bullet.Repository {
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Bullet.Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;

    /// <summary>
    /// Concrete implementation of IRepository
    /// </summary>
    /// <typeparam name="T">The object type of this repository</typeparam>
    public class MongoRepository<T> : IRespository<T> {

        /// <summary>
        /// The Mongo Collection
        /// </summary>
        private MongoCollection<T> collection;

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
    }
}
