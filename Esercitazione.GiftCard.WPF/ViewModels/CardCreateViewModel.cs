using Esercitazione.GiftCard.Core.BusinessLayer;
using Esercitazione.GiftCard.Core.Entities;
using Esercitazione.GiftCard.Core.Mock.Repositories;
using Esercitazione.GiftCard.WPF.Messaging.Card;
using Esercitazione.GiftCard.WPF.Messaging.Misc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Esercitazione.GiftCard.WPF.ViewModels
{
    public class CardCreateViewModel : ViewModelBase
    {
        public ICommand CreateCommand { get; set; }

        private string _Mittente;
        public string Mittente
         {
            get { return _Mittente; }
            set
            {
                _Mittente = value; RaisePropertyChanged();
            }
        }

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set
            {
                _Destinatario = value; RaisePropertyChanged();
            }
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set
            {
                _Messaggio = value; RaisePropertyChanged();
            }
        }

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set
            {
                _Importo = value; RaisePropertyChanged();
            }
        }

        private DateTime _DataDiScadenza;
        public DateTime DataDiScadenza
        {
            get { return _DataDiScadenza; }
            set
            {
                _DataDiScadenza = value; RaisePropertyChanged();
            }
        }

        public CardCreateViewModel()
        {
            CreateCommand = new RelayCommand(() => ExecuteCreate(), () => CanExecuteCreate());

            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }
        }

        private bool CanExecuteCreate()
        {
            
            return !string.IsNullOrEmpty(Destinatario) &&
                !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Messaggio) &&
                !string.IsNullOrEmpty(Importo.ToString()) &&
                !string.IsNullOrEmpty(DataDiScadenza.ToString());
        }

        private void ExecuteCreate()
        {
            
            var entity = new Card
            {
                Mittente = Mittente,
                Messaggio = Messaggio,
                Importo = Importo,
                Destinatario = Destinatario,
                Datadiscadenza = DataDiScadenza,
                    
            };

            //Inizializzo il business layer
            var layer = new MainBusinessLayer(new CardRepositoryMock());
            //Richiamo l'operazione del layer
            var response = layer.CreateCard(entity);

            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Something wrong",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Creazione completata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateCardMessage());
        }
    }
}
