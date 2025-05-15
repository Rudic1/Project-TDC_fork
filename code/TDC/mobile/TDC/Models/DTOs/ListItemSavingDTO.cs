using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.Models.DTOs
{
    public class ListItemSavingDto
    {
        public ListItemSavingDto() { }
        public ListItemSavingDto(string description, int effort)
        {
            Description = description;
            Effort = effort;
        }

        public string Description { get; set; }
        public int Effort { get; set; }
    }
}
