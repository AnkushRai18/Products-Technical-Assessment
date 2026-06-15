using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.DTOs;

public class ProductDto
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; } = DateTime.Now;

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
