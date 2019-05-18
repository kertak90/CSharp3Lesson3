using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSenderLib.Linq2SQL;
using MailSenderLib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lesson1.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IrecepientsData _RecepientsData;
        private string _Title = "Рассыльщик почты v1";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private string _Status = "Готов";

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        private ObservableCollection<Recepients> _Recipients;

        public ObservableCollection<Recepients> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        private Recepients _SelectedRecepient;

        public Recepients SelectedRecepient
        {
            get => _SelectedRecepient;
            set => Set(ref _SelectedRecepient, value);
        }

        #region Commands
        public ICommand RefreshDataCommand { get; }
        public ICommand WriteRecipientDataCommand { get; }
        public ICommand CreateNewRecepientCommand { get; }
        
        #endregion

        public MainWindowViewModel(IrecepientsData RecepientsData)
        {
            _RecepientsData = RecepientsData;
            
            RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanrefreshDataCommandExecute);
            WriteRecipientDataCommand = new RelayCommand<Recepients>(OnWriteRecepientDataCommandExecute, CanWriteRecipientDataCommandExecute);
            CreateNewRecepientCommand = new RelayCommand(OnCreateNewRecepientCommandExecute, CanCreateNewRecepientCommandExecute);
        }

        private bool CanCreateNewRecepientCommandExecute() => true;

        private void OnCreateNewRecepientCommandExecute()
        {
            var new_recepient = new Recepients();
            var id = _RecepientsData.Create(new_recepient);
            if(id != null)
            {
                Recipients.Add(new_recepient);
                SelectedRecepient = new_recepient;
            }

        }

        private bool CanWriteRecipientDataCommandExecute(Recepients recipient) => recipient != null;

        private void OnWriteRecepientDataCommandExecute(Recepients recipient)
        {
            _RecepientsData.Write(recipient);
            _RecepientsData.SaveChanges();
        }

        private bool CanrefreshDataCommandExecute() => true;

        private void OnRefreshDataCommandExecuted() => LoadData();

        private void LoadData()
        {            
            var recipients = _RecepientsData.GetAll();
            Recipients = new ObservableCollection<Recepients>(recipients);
        }
    }
}
