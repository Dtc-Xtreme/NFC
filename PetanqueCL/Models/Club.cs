using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Models
{
    [Index(nameof(Nr), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Club : BaseModel
    {
        [ObservableProperty]
        [Key]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Precision(3)]
        private int nr;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(30)]
        private string name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private Province province;

        [ObservableProperty]
        private string? image;

        public Club()
        {

        }

        public override string ToString()
        {
            return Nr.ToString("D3") + " - " + Name;
        }
        public override bool Equals(object? obj)
        {
            return obj is Club club &&
                   Id == club.Id &&
                   Nr == club.Nr &&
                   Name == club.Name &&
                   EqualityComparer<Province>.Default.Equals(Province, club.Province) &&
                   Image == club.Image;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nr, Name, Province, Image);
        }
    }
}
