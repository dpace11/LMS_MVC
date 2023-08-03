using System.ComponentModel.DataAnnotations;

namespace LMS_MVC.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }


        [Required(ErrorMessage ="Enter author name")]
        [Display(Name ="Author Name")]        
        public String AuthorName { get; set; }

        [Required(ErrorMessage ="UserName required")]
        [StringLength(15,MinimumLength =6,ErrorMessage ="username must be at least 6 charcters long")]
        [Display(Name ="User Name")]
        public string AuthorUserName { get; set; }

        [Required(ErrorMessage ="Enter author's phone number")]
        [Display(Name ="Phone Number")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Phone number must be 9 to 10 digits.")]
        public long PhoneNumber { get; set; }

        [Required(ErrorMessage ="Enter author's address")]
        [Display(Name ="Address")]
        public string AuthorAddress { get; set; }


        [Required(ErrorMessage = "Enter author's email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }

        [Required(ErrorMessage ="Please Select a image ")]
        public string ImagePath { get; set; }
    }
}
