namespace Bullet.Repository {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Main Interface Definition
    /// </summary>
    /// <typeparam name="T">The object type of this repository</typeparam>
    public interface IRespository<T> {

        /// <summary>
        /// Returns the T by it's given id (ObjectId)
        /// </summary>
        /// <param name="id">The string representation of the ObjectId</param>
        /// <returns>T</returns>
        T WithId(string id);
    }
}
