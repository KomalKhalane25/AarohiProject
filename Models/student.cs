using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CunsumeWebAPI.Models
{
    public class student
    {
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public int RollNo { get; set; }
            public string Div { get; set; }
            public string Address { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public string EmailID { get; set; }
            public string Password { get; set; }
    }

    public class data
    {
        public int UserId { get; set; }
        public string Id { get; set; }
        public int Title { get; set; }
        public string Body { get; set; }
    }
}
