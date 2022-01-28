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
    public class UpdateCardViewModel : ViewModelBase
    {
        private Card _Entity;

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged(); }
        }

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


        public ICommand UpdateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public UpdateCardViewModel()
        {
            
            UpdateCommand = new RelayCommand(() => ExecuteUpdate(), CanExecuteUpdate());
            CancelCommand = new RelayCommand(() => ExecuteCancel(), CanExecuteUpdate());
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (UpdateCommand as RelayCommand).RaiseCanExecuteChanged();
                };

            }
        }

        public UpdateCardViewModel(Card entity) : this()
        {
           
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
            _Entity = entity;
            Id = entity.Id;
            Mittente = entity.Mittente;
            Destinatario = entity.Destinatario;
            DataDiScadenza = entity.Datadiscadenza;
            Messaggio = entity.Messaggio;
            Importo = entity.Importo;
            
        }

        private void ExecuteCancel()
        {
           
            Messenger.Default.Send(new CloseUpdateCardMessage());
        }

        private bool CanExecuteUpdate()
        {
           
            return
                !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Destinatario) && !string.IsNullOrEmpty(Messaggio);
        }

        private void ExecuteUpdate()
        {            
            var entity = new Card
            {
                Mittente = Mittente,
                Messaggio = Messaggio,
                Importo = Importo,
                Destinatario = Destinatario,
                Datadiscadenza = DataDiScadenza,
            };
            

            var layer = new MainBusinessLayer(new CardRepositoryMock());

           
            var result = layer.UpdateCard(entity);

            
            if (!result.Success)
            {
               
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Attenzione! Alcuni dati non sono validi!",
                    Content = result.Message,
                    Icon = MessageBoxImage.Warning
                });
                return;
            }

           
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Conferma",
                Content = $"La GiftCard da parte di: {Mittente} per {Destinatario} è stato aggiornato!",
                Icon = MessageBoxImage.Information
            });

            
            CancelCommand.Execute(null);
        }
    }
}

