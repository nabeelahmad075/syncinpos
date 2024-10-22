using System.ComponentModel.DataAnnotations;

namespace syncinpos.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}