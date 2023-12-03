using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Validations;

namespace Educational_Website.ViewModels
{
    public class CourseResultViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 100, ErrorMessage = "{0} must be {1} to {2}")]
        public int Degree { get; set; }
        [DisplayName("Course")]
        public int? crs_id { get; set; }
        [DisplayName("Trainee")]
        [UniqueResult]
        public int? trainee_id { get; set; }
        public Course? course { get; set; }
        public Trainee? Trainee { get; set; }

    }
}
