using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    public class ListMembersDto
    {
        public List<string> Members { get; set; }
        public ListMembersDto() { }

        public ListMembersDto(List<string> members)
        {
            Members = members;
        }

    }
}
