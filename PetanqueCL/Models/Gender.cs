using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace PetanqueCL.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public partial class Gender : BaseModel
    {
        [ObservableProperty]
        [Key]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        private string name;

        public Gender()
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Gender gender &&
                   Id == gender.Id &&
                   Name == gender.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
        public override string ToString()
        {
            return Id + " - " + Name;
        }
    }
}
