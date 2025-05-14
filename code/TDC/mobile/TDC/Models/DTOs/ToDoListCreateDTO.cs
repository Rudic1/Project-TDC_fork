using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    internal class ToDoListCreateDto
    {
        public ToDoListCreateDto() { }
        public ToDoListCreateDto(string name, bool isCollaborative)
        {
            Name = name;
            IsCollaborative = isCollaborative;
        }

        public string Name { get; }
        public bool IsCollaborative { get; }
    }
}
