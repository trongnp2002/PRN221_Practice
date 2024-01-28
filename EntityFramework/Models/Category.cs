using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    [Table("CATEGORY")]
    public class Category
    {
        [Column("CategoryId")]
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName ="ntext")]
        public string Description { get; set; }

        // Collect Navigation: Dieu huong tap hop
        public virtual List<Product> Products { get; set; }

        public void PrintInfo()
        {
            if (this.Products != null)
            {
                Console.WriteLine($"So san pham {this.Products.Count()}");
                this.Products.ForEach(p => p.PrinInfo());
            }
            else
            {
                Console.WriteLine("Products is null");
            }
        }
    }
}
