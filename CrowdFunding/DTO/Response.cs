using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.DTO
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? Description { get; set; }
    }
}
