namespace Bullet.Attributes {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Used to override the default name of the collection. The default being the class name
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionNameAttribute : Attribute {

        /// <summary>
        /// Gets the name of the collection
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Default constructor, setting the name of the collection to something other than the class name
        /// </summary>
        /// <param name="name"></param>
        public CollectionNameAttribute(string name) {

            if(!string.IsNullOrWhiteSpace(name)){
                throw new ArgumentException("Collection must have a name", "name");
            }
            this.Name = name;
        }
    }
}
