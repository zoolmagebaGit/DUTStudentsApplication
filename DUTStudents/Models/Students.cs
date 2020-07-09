using Newtonsoft.Json;
using Microsoft.Azure.Documents;
using System.ComponentModel.DataAnnotations;

namespace DUTStudents.Models
{
    public class Students
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Enter your Student Number")]
        [Range(20000000, 29999999)]
        [JsonProperty(PropertyName = "StudentNo")]
        public string StudentNo { get; set; }
        [Display(Name = "Student Image")]
        public string ImageLink { get; set; }

        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Enter your student name")]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [Display(Name = "Student Surname")]
        [Required(ErrorMessage = "Enter your surname")]
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter your Email address")]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telephone Number")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        [RegularExpression(pattern: @"^\(?([031]{1})\)?[-.]?([1-9]{1})[-.]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid")]
        [StringLength(maximumLength: 10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        [JsonProperty(PropertyName = "Tel")]
        public string Tel { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        [RegularExpression(pattern: @"^\(?([0]{1})\)?[-.]?([1-9]{1})[-.]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid")]
        [StringLength(maximumLength: 10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        [JsonProperty(PropertyName = "Mobile")]
        public string Mobile { get; set; }


        [JsonProperty(PropertyName = "isActive")]
        public bool isActive { get; set; }

        public string HtmlStudent()
        {
            return "Student No:      " + StudentNo +
                "<br>Name:      " + Name +
                "<br>Surname:       " +Surname +
                "<br>Email:     " +Email +
                "<br>Tellephone:        " + Tel +
                "<br>Mobile:        " + Mobile +
                "<br>Is Active:     " + isActive; ;
        }
    }
}