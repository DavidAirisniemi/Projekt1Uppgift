using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }
}
