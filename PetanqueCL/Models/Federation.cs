using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public partial class Federation : BaseModel
    {
        [ObservableProperty]
        [Key]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(40)]
        private string name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [MaxLength(5)]
        private string prefix;

        [ObservableProperty]
        private byte[]? image;

        public Federation()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Federation federation &&
                   Id == federation.Id &&
                   Name == federation.Name &&
                   Prefix == federation.Prefix &&
                   Image == federation.Image;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Prefix, Image);
        }
        public override string ToString()
        {
            return Name + " (" + Prefix + ")";
        }
    }
}
