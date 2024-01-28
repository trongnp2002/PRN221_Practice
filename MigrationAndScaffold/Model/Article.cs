using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationAndScaffold.Model
{

    [Table("article")]
    public class Article
    {
        [Key]
        public int ArticleId { set; get; }

        [StringLength(100)]
        public string Title { set; get; }

        public string Description { set; get; }

    }

}
