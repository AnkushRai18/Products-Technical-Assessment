using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.DTOs;

public class TokenModelDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
