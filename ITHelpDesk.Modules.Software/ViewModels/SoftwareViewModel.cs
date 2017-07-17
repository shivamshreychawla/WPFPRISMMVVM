using ITHelpDesk.Common.Infrastructure.Models;
using ITHelpDesk.Common.Infrastructure.Repository;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITHelpDesk.Modules.Software.ViewModels
{
    public class SoftwareViewModel : NotificationObject, IActiveAware
    {
        #region Private Fields
        private int categoryId;
        private string categoryName;
        private string selectedSoftwareCategory;
        private ObservableCollection<SoftwareCategory> softwareCategories;
        private ObservableCollection<SoftwareItems> softwareList;
        private DelegateCommand submitRequestCommand;
        private string comment;
        IEmployeeService employeeService;
        private string selectedSoftwareName;
        private string message;
        bool isPopupOpen;

        #endregion
        public SoftwareViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            fetchSoftwareCategory();
            submitRequestCommand = new DelegateCommand(saveRequest);
        }

        #region Public Properties
        public int CategoryId
        {
            get { return categoryId; }

            set
            {
                RaisePropertyChanged("CategoryId");

            }
        }
        public string CategoryName
        {
            get { return categoryName; }

            set
            {
                categoryName = value;
                RaisePropertyChanged("CategoryName");
            }
        }
        public ObservableCollection<SoftwareCategory> SoftwareCategories
        {
            get { return softwareCategories; }
            set
            {
                softwareCategories = value;
                RaisePropertyChanged("SoftwareCategories");
            }
        }

        public ObservableCollection<SoftwareItems> SoftwareList
        {
            get { return softwareList; }
            set
            {
                softwareList = value;
                RaisePropertyChanged("SoftwareList");

            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RaisePropertyChanged("Comment");
            }
        }
        public DelegateCommand SubmitRequestCommand
        {
            get
            {
                return submitRequestCommand; // need to display message
            }

        }
        public string SelectedSoftwareCategory
        {
            get { return selectedSoftwareCategory; }

            set
            {
                selectedSoftwareCategory = value;
                if (value != null)
                {
                    fetchSoftwareList(selectedSoftwareCategory);
                }
                RaisePropertyChanged("SelectedSoftwareCategory");
            }
        }
        public string SelectedSoftwareName
        {
            get { return selectedSoftwareName; }

            set
            {
                selectedSoftwareName = value;
                RaisePropertyChanged("SelectedSoftwareName");
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                RaisePropertyChanged("IsPopupOpen");
            }
        }
        public DelegateCommand ClosePopup
        {
            get
            {
                return new DelegateCommand(closePopup); // need to display message
            }

        }
        #endregion

        #region Private Methods

        private void closePopup()
        {
            IsPopupOpen = false;
        }
        private void fetchSoftwareCategory()
        {
            List<SoftwareCategory> allsoftwareCategories = employeeService.GetSoftwareCategories();
            SoftwareCategories = new ObservableCollection<SoftwareCategory>(allsoftwareCategories);
        }

        private void fetchSoftwareList(string selectedCategory)
        {
            List<SoftwareItems> allSoftwareList = employeeService.GetSoftwareItemsByCategory(selectedCategory);
            SoftwareList = new ObservableCollection<SoftwareItems>(allSoftwareList);
        }

        private void saveRequest()
        {
            string selectedCategory = SelectedSoftwareCategory;
            string selectedSoftware = SelectedSoftwareName;
            string comment = Comment;
            bool result = employeeService.SaveSoftwareRequest(selectedCategory, selectedSoftware, comment);

            if (result == true)
            {
                Message = "Successful: Your request is accepted.";
                IsPopupOpen = true;
            }
            else
            {
                Message = "Unsuccessful: Please try again.";
                IsPopupOpen = true;
            }
        }
        #endregion

        #region IActiveAware implementation
        bool active;
        public bool IsActive
        {
            get
            {
                return active;
            }
            set
            {
                if (active != value)
                {
                    active = value;
                    if (active == true)
                        refreshSoftwareModule();
                }
            }
        }

        private void refreshSoftwareModule()
        {
            SelectedSoftwareCategory = null;
            SelectedSoftwareName = null;
            Comment = null;
        }

        public event EventHandler IsActiveChanged;

        #endregion

    }
}
