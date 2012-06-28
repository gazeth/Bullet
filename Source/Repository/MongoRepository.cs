using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullet.Repository {

    /// <summary>
    /// Concrete implementation of IRepository
    /// </summary>
    /// <typeparam name="T">The object type of this repository</typeparam>
    public class MongoRepository<T> : IRespository<T> {
    }
}
