namespace Bullet.GridFS {

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using MongoDB.Driver.GridFS;
    using MongoDB.Driver;
    using MongoDB.Bson;

    public class GridFS : IGridFS {

        /// <summary>
        /// The mongo database
        /// </summary>
        private MongoDatabase store;

        /// <summary>
        /// The default constructor, uses the default connection string settings
        /// </summary>
        /// <remarks>The default name for the connection string setting is "MongoConnection"</remarks>
        public GridFS() : this(Helpers.DefaultConnectionString()) {
        }

        /// <summary>
        /// Initialises a new mongo database and collection
        /// </summary>
        /// <param name="connectionString">the connection string to use</param>
        public GridFS(string connectionString) {
            OpenStore(new MongoUrl(connectionString));
        }

        /// <summary>
        /// Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url of the database</param>
        private void OpenStore(MongoUrl url) {
            var server = MongoServer.Create(url.ToServerSettings());
            this.store = server.GetDatabase(url.DatabaseName);
        }

        /// <summary>
        /// Store a file in the database
        /// </summary>
        /// <param name="stream">The stream of the files content</param>
        /// <param name="fileName">The remote filename</param>
        /// <param name="contentType">The file's content type</param>
        /// <returns>GridFS File Info</returns>
        public MongoGridFSFileInfo Upload(Stream stream, string fileName, string contentType) {
            MongoGridFS fs = new MongoGridFS(this.store);
            MongoGridFSCreateOptions options = new MongoGridFSCreateOptions();
            options.ContentType = contentType;
            return fs.Upload(stream, fileName, options);
        }

        /// <summary>
        /// Retrieve a file from the database
        /// </summary>
        /// <param name="id">The id of the file</param>
        /// <returns>GridFS File Info</returns>
        public MongoGridFSFileInfo Download(string id) {
            return store.GridFS.FindOne(MongoDB.Driver.Builders.Query.EQ("_id", new ObjectId(id)));
        }
    }
}
