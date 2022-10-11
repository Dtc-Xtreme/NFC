using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Models
{
    public abstract class NewBaseModel : INotifyDataErrorInfo
    {
        private bool isDirty = false;
        private bool isNew = false;
        private Dictionary<String, List<String>> errors = new Dictionary<string, List<string>>();

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


        public bool HasErrors => errors.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return errors.ContainsKey(propertyName) ? errors[propertyName] : null;
            //return !errors.ContainsKey(propertyName) ? null : String.Join(Environment.NewLine, errors[propertyName]);
        }

        public void AddError(string error, [CallerMemberName] string propertyName = null)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                errors[propertyName].Add(error);
            }
        }
        public void RemoveError([CallerMemberName] string propertyName = null)
        {
            if (errors.ContainsKey(propertyName))
            {
                errors.Remove(propertyName);
            }
        }
    }
}
