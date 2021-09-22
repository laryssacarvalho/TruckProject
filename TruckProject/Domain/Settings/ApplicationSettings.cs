using System;
using System.Collections.Generic;
using System.Text;

namespace TruckProject.Domain.Settings
{
    public class ApplicationSettings
    {
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }
    }
}
