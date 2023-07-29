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
        [Range(750101, 750599, ErrorMessage = "Roll no shoud be within 7501XX--7505XX")]
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
        public DateTime ActualReturnDate { get; set; }  //deadline date


        [DataType(DataType.Date)]
        [Display(Name = "Returned Date")]
       
        public DateTime? BookReturnDate { get; set; }       //studdent le return gareko date

       // public string StdBookReturnDate { get; set; }

        [ForeignKey("Book")]
        [Display(Name ="Book Name")]
        public int BookID { get; set; }
        public Book? Book { get; set; }

       
    }
}


