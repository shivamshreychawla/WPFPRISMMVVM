using ITHelpDesk.Common.Infrastructure.Models;
using ITHelpDesk.Common.Infrastructure.Repository;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITHelpDesk.Modules.Hardware.ViewModels
{
    public class HardwareViewModel : NotificationObject, IActiveAware
    {
        #region Private Fields
        private int categoryId;
        private string categoryName;
        private string selectedHardwareCategory;
        private ObservableCollection<HardwareCategory> hardwareCategories;
        private ObservableCollection<HardwareItems> hardwareList;
        private DelegateCommand submitRequestCommand;
        private string comment;
        private IEmployeeService employeeService;
        private string selectedHardwareName;
        private string message;
        bool isPopupOpen;

        #endregion

        public HardwareViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            fetchHardwareCategory();
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

        public ObservableCollection<HardwareCategory> HardwareCategories
        {
            get { return hardwareCategories; }
            set
            {
                hardwareCategories = value;
                RaisePropertyChanged("HardwareCategories");
            }
        }

        public ObservableCollection<HardwareItems> HardwareList
        {
            get { return hardwareList; }
            set
            {
                hardwareList = value;
                RaisePropertyChanged("HardwareList");

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

        public DelegateCommand SubmitRequestCommand
        {
            get
            {
                return submitRequestCommand; // need to display message
            }

        }

        public DelegateCommand ClosePopup
        {
            get
            {
                return new DelegateCommand(closePopup); // need to display message
            }

        }

        private void closePopup()
        {
            IsPopupOpen = false;
        }


        public string SelectedHardwareCategory
        {
            get { return selectedHardwareCategory; }

            set
            {
                selectedHardwareCategory = value;
                if (value != null)
                {
                    fetchHardwareList(selectedHardwareCategory);
                }

                RaisePropertyChanged("SelectedHardwareCategory");

            }
        }

        public string SelectedHardwareName
        {
            get { return selectedHardwareName; }

            set
            {
                selectedHardwareName = value;
                RaisePropertyChanged("SelectedHardwareName");
            }
        }


        #endregion

        #region Private Methods
        private void fetchHardwareCategory()
        {
            List<HardwareCategory> allHardwareCategories = employeeService.GetHardwareCategories();
            hardwareCategories = new ObservableCollection<HardwareCategory>(allHardwareCategories);
            HardwareCategories = hardwareCategories;
        }

        private void fetchHardwareList(string selectedCategory)
        {
            List<HardwareItems> allHardwareList = employeeService.GetHardwareItemsByCategory(selectedCategory);
            hardwareList = new ObservableCollection<HardwareItems>(allHardwareList);
            HardwareList = hardwareList;
        }
        private void saveRequest()
        {
            string selectedCategory = SelectedHardwareCategory;
            string selectedHardware = SelectedHardwareName;
            string comment = Comment;
            bool result = employeeService.SaveHardwareRequest(selectedCategory, selectedHardware, comment);
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

        #region IActiveAware  implementation

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
                        refreshHardwareModule();
                }
            }
        }

        private void refreshHardwareModule()
        {
            SelectedHardwareCategory = null;
            SelectedHardwareName = null;
            Comment = null;
        }

        public event EventHandler IsActiveChanged;

        #endregion
    }
}

