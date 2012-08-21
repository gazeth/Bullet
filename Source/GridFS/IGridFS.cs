namespace Bullet.GridFS {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MongoDB.Driver.GridFS;
    using System.IO;

    public interface IGridFS {

        /// <summary>
        /// Store a file in the database
        /// </summary>
        /// <param name="stream">The stream of the files content</param>
        /// <param name="fileName">The remote filename</param>
        /// <param name="contentType">The file's content type</param>
        /// <returns>GridFS File Info</returns>
        MongoGridFSFileInfo Upload(Stream stream, string fileName, string contentType);

        /// <summary>
        /// Retrieve a file from the database
        /// </summary>
        /// <param name="id">The id of the file</param>
        /// <returns>GridFS File Info</returns>
        MongoGridFSFileInfo Download(string id);
    }
}
