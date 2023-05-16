using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public   string  Username { get; set;}
        
        
         [Required]
        public   string  KnownAs { get; set;}
         [Required]
        public   string  Gender { get; set;}
         [Required]
        public   DateOnly?  DateofBirth { get; set;} // to kanoume 
      //  ? gia na mporei na einai null
         [Required]
        public   string  City{ get; set;}
         [Required]
        public   string  Country { get; set;}
        [Required]
        [StringLength(8 , MinimumLength = 4)]
        public   string  Password{get; set;}
        
        
        
        
        
    }
}