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
using System.Windows;
using System.Windows.Input;

namespace Esercitazione.GiftCard.WPF.ViewModels
{
    public class CardRowViewModel : ViewModelBase
    {
        private Card item;

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }

        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value; RaisePropertyChanged(); }
        }

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged(); }
        }


        private DateTime _DataScadenza;
        public DateTime DataScadenza
        {
            get { return _DataScadenza; }
            set { _DataScadenza = value; RaisePropertyChanged(); }
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged(); }
        }

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CardRowViewModel()
        {
            
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            DeleteCommand = new RelayCommand(() => ExecuteDelete());

        }

        public CardRowViewModel(Card item) : this()
        {
            this.item = item;
            Importo = item.Importo;
            Destinatario = item.Destinatario;
            Mittente = item.Mittente;
            DataScadenza = item.Datadiscadenza;
            Messaggio=item.Messaggio;
            Id = item.Id;
        }

        private bool viewGiftCard = false;
        public bool ViewGiftCard
        {
            get { return viewGiftCard; }
            set { viewGiftCard = value; RaisePropertyChanged(); }
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm delete",
                Content = "Are you sure?",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }

        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new CardRepositoryMock());

                //Cancello l'elemento selezionato
                var response = layer.DeleteCard(item);

                //se la response è ok
                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Deletion Confirmed",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }

        private void ExecuteUpdate()
        {
            Messenger.Default.Send(new ShowUpdateCardMessage { Entity = item });
        }
    }

}
