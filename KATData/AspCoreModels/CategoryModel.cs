using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreModels
{
    public class CategoryModel
    { 
        public string CategoryGUID { get; set; }
        public string ParentCategoryGUID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int RecordStatus { get; set; }

    }
}
