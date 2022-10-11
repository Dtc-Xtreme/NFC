using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Models
{
    public partial class BaseModel : ObservableValidator
    {
        private bool isDirty = false;
        private bool isNew = false;

        [NotMapped]
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
            set
            {
                isDirty = value;
            }
        }

        [NotMapped]
        public bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                isNew = value;
                IsDirty = true;
            }
        }

        protected void SetDirty()
        {
            IsDirty = true;
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }
    }
}
