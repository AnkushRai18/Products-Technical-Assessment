using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.DTOs;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public string CreatedBy { get; set; }
}
