using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_MVC.Models
{
    public class BookIssueReturn
    {

        [Key]
        public int Id { get; set; }


        [Display(Name = "Roll No")]
        [Required]
        public int RollNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Departmet { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Issued Date")]
        public DateTime BookIssueDate { get; set; }

       
        [DataType(DataType.Date)]
        [Display(Name ="Deadline")]
        public DateTime ActualReturnDate { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Returned Date")]
        public DateTime? BookReturnDate { get; set; }

       // public string StdBookReturnDate { get; set; }

        [ForeignKey("Book")]
        [Display(Name ="Book Name")]
        public int BookID { get; set; }
        public Book? Book { get; set; }

       
    }
}


