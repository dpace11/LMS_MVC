using System.ComponentModel.DataAnnotations;

namespace LMS_MVC.Models.ViewModel
{
    public class ImageCreateModel
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "Enter author name")]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }


        [Required(ErrorMessage = "Enter author's address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter author's phone number")]
        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }


        [Required(ErrorMessage = "Enter author's email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Select a image ")]
        [Display(Name ="Author Image")]
        public IFormFile ImagePath { get; set; }
    }
}
