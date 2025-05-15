using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        [JsonPropertyName("listId")]
        public long ListId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isCollaborative")]
        public bool IsCollaborative { get; set; }
    }
}
