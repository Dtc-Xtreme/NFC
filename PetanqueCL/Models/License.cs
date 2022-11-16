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
        private byte[]? image;

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
                   id == license.id &&
                   nr == license.nr &&
                   firstName == license.firstName &&
                   lastName == license.lastName &&
                   birthDate == license.birthDate &&
                   EqualityComparer<Gender>.Default.Equals(gender, license.gender) &&
                   EqualityComparer<byte[]?>.Default.Equals(image, license.image) &&
                   EqualityComparer<Club?>.Default.Equals(club, license.club) &&
                   disqualified == license.disqualified &&
                   description == license.description &&
                   active == license.active;
        }
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(nr);
            hash.Add(firstName);
            hash.Add(lastName);
            hash.Add(birthDate);
            hash.Add(gender);
            hash.Add(image);
            hash.Add(club);
            hash.Add(disqualified);
            hash.Add(description);
            hash.Add(active);
            return hash.ToHashCode();
        }
        public override string ToString()
        {
            return nr + " " + FullName;
        }
        public string FullLicenseNr
        {
            get {
                return this.Club.Province.Id.ToString("D3") + "-" + this.Club.Id.ToString("D2") + "-" + this.Nr.ToString("D5");
            }
        }
    }
}
