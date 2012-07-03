namespace Bullet {

    using System;
    using System.Configuration;
    using Bullet.Attributes;
    using Bullet.Entities;

    internal static class Helpers {

        /// <summary>
        /// The default key Bullet will look for in the app/web config files
        /// </summary>
        private const string defaultConnection = "MongoConnection";

        /// <summary>
        /// Gets the default connection string from the app/web config file
        /// </summary>
        /// <returns></returns>
        public static string DefaultConnectionString() {
            return ConfigurationManager.ConnectionStrings[defaultConnection].ConnectionString;
        }

        /// <summary>
        /// Gets the name of the collection using either the custom name (via CollectionNameAttribute) or the class name
        /// </summary>
        /// <typeparam name="T">The type to get the name from</typeparam>
        /// <returns>The name of the collection</returns>
        public static string WithTheName<T>() where T : IEntity {

            /*Check for the CollectionName attribute*/
            Attribute a = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionNameAttribute));
            if (a != null) {                
                /*a custom name has been set*/
                return ((CollectionNameAttribute)a).Name;
            } else {
                /*use the name of the class*/
                return typeof(T).Name;
            }

        }
    }
}
