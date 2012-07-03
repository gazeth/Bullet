namespace Bullet.Entities {

    using MongoDB.Bson.Serialization.Attributes;
    using System;

    /// <summary>
    /// Allows you to keep your domain classes free from any dependencies on the 10gen C# Driver
    /// </summary>
    public interface IEntity {

        /// <summary>
        /// Gets or Sets the Id of the Entity
        /// </summary>
        [BsonId]
        string Id { get; set; }

        /// <summary>
        /// When the entity was created
        /// </summary>
        DateTime CreationTime { get; }
    }
}
