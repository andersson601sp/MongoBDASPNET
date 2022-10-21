using System;
using System.Collections.Generic;
using System.Text;

namespace application.Infra
{
    public  class ProductStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProdsCollectionName { get; set; } = null!;
    }
}
