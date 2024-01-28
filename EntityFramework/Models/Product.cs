using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
/*
 * Table(Name): Chỉ ra 1 model nào đó tương ứng với 1 table trên sql server
 * Key: Thiết lập khóa chính
 * Required: Yêu cầu trường dữ liệu khác null
 * StringLength: Thiết lập độ dài tối đa
 * Column: Quy định tên cột và kiểu dữ liệu
*/
{
    [Table("PRODUCTS")]
    public class Product
    {
        [Key]
        [Required]
        [Column("Id")]
        public int ProductId { get; set;}

        [Required]
        [StringLength(50)]
        [Column("ProductName", TypeName ="ntext")]
        public string Name { get; set;}

        [Column(TypeName = "money")]
        public decimal Price {  get; set;}

        public int CategoryId;

        //Tao Foreign Key
        // Referece Navigation
        [ForeignKey("CategoryId")]
        [Required]
        public virtual Category Category { get; set;} // FK -> PK

        public int? CategoryId2;

        //Tao Foreign Key
        // Referece Navigation
        [InverseProperty("Products")]
        [ForeignKey("CategoryId2")]
        public virtual Category Category2 { get; set; } // FK -> PK

        public void PrinInfo()
        {
            if (this.Category != null)
            {
                Console.WriteLine($"{ProductId,-4} {Name,-20} {Price,-20} {CategoryId,-4}");

                Console.WriteLine($"Category : {this.Category.Id} - {this.Category.Name}");

            }
            else
            {
                Console.WriteLine($"{ProductId,-4} {Name,-20} {Price,-20} {CategoryId,-4}");

                Console.WriteLine("Category is null");
            }
        }
    }
}
