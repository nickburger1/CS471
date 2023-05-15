using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> ViewModelBase.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS BusinessLogic</PartOfProject>
/// <DateCreated> 2/22/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/22/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to provide base functionality for the view models.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IDictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get
            {
                return _propertyErrors.Values.Any(r => r.Any());
            }
        }

        /// <summary>
        /// Invokes the property to change on the front end if changed in the view model.
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void ConfirmDelete() { }
        public virtual void Add() { }
        public virtual void Update() { }
        public virtual void Cancel() { }
        public virtual void Export() { }
        public virtual void Dispose() { }
        public virtual void Validate() { }
        public virtual void ActionFailed(string message, string caption) { }


        /// <summary>
        /// Get properties with errors.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        /// <returns>Errors or null.</returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName != null)
            {
                if (_propertyErrors.TryGetValue(propertyName, out var errors))
                {
                    return errors;
                }
            }

            return new List<string>();
        }

        /// <summary>
        /// Adds errors to error list and notify change.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="error"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public void AddError(string propertyName, string error)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
                _propertyErrors[propertyName] = new List<string>();

            if (!_propertyErrors[propertyName].Contains(error))
            {
                _propertyErrors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// Notify error changed
        /// </summary>
        /// <param name="propertyName"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged("HasErrors");
        }

        /// <summary>
        /// Clear error.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearAllErrors()
        {
            List<string> propKeys = _propertyErrors.Keys.ToList();

            _propertyErrors.Clear();

            foreach (var key in propKeys)
            {
                OnErrorsChanged(key);
            }
        }
    }
}
