using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string Id { get; set; }
        [StringLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
