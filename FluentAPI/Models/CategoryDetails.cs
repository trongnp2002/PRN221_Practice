using EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.Models
{
    public class CategoryDetails
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int CountProduct { get; set; }

        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
