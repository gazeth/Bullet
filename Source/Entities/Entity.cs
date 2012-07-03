namespace Bullet.Entities {

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    [BsonIgnoreExtraElements(Inherited=true)]
    public abstract class Entity :  IEntity {

        /// <summary>
        /// Gets or sets the id for this object(document)
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// When the entity was created
        /// </summary>
        public DateTime CreationTime {
            get {
                return new ObjectId(this.Id).CreationTime;
            }
        }

    }
}
