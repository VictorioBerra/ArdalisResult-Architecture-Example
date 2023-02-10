using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArdalisResultArch.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }

        public int CategoryId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
