using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public partial class Province : BaseModel
    {
        [ObservableProperty]
        [Key]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private string name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private Federation federation;

        public Province()
        {

        }

        public virtual ICollection<Club> Clubs { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Province province &&
                   Id == province.Id &&
                   Name == province.Name &&
                   EqualityComparer<Federation>.Default.Equals(Federation, province.Federation);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Federation);
        }
        public override string ToString()
        {
            return Id.ToString("D2") + " - " + Name;
        }
    }
}
