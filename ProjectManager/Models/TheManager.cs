using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class TheManager
    {
        [ForeignKey("Programmer")]
        public int ProgrammerId { get; set; }
        public virtual Programmer Programmer { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

    }
}
