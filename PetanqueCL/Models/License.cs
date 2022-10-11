using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetanqueCL.Models
{
    [Index(nameof(Nr), IsUnique = true)]
    public partial class License : BaseModel
    {
        [ObservableProperty]
        [Key]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int nr;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(30)]
        private string firstName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(30)]
        private string lastName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private DateTime birthDate;

        [ObservableProperty]
        [Required]
        private Gender gender;

        [ObservableProperty]
        private string? image;

        [ObservableProperty]
        private Club? club;

        [ObservableProperty]
        [Required]
        private bool disqualified;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        [Required]
        private bool active = true;

        public License()
        {
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public override bool Equals(object? obj)
        {
            return obj is License license &&
                   FullName == license.FullName &&
                   Id == license.Id &&
                   Nr == license.Nr &&
                   FirstName == license.FirstName &&
                   LastName == license.LastName &&
                   BirthDate == license.BirthDate &&
                   EqualityComparer<Gender>.Default.Equals(Gender, license.Gender) &&
                   Image == license.Image &&
                   EqualityComparer<Club?>.Default.Equals(Club, license.Club) &&
                   Disqualified == license.Disqualified &&
                   Description == license.Description &&
                   Active == license.Active;
        }
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(FullName);
            hash.Add(Id);
            hash.Add(Nr);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(BirthDate);
            hash.Add(Gender);
            hash.Add(Image);
            hash.Add(Club);
            hash.Add(Disqualified);
            hash.Add(Description);
            hash.Add(Active);
            return hash.ToHashCode();
        }
        public override string ToString()
        {
            return nr + " " + FullName;
        }
    }
}
