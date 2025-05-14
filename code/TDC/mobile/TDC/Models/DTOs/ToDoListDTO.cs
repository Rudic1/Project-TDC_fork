using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    internal class ToDoListDto
    {
        public ToDoListDto() { }

        public ToDoListDto(long listId, string name, List<ListItemDto> items, List<string> members, bool isCollaborative)
        {
            ListId = listId;
            Name = name;
            IsCollaborative = isCollaborative;
        }

        public long ListId { get; set; }
        public string Name { get; set; }
        public bool IsCollaborative { get; set; }
    }
}
