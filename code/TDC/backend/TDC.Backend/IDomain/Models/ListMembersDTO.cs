using System.Collections.Generic;

namespace TDC.Backend.IDomain.Models
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
