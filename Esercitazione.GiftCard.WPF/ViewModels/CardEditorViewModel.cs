using Esercitazione.GiftCard.Core.BusinessLayer;
using Esercitazione.GiftCard.Core.Entities;
using Esercitazione.GiftCard.Core.Mock.Repositories;
using Esercitazione.GiftCard.Core.Repository;
using Esercitazione.GiftCard.WPF.Messaging.Card;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Esercitazione.GiftCard.WPF.ViewModels
{
    public class CardEditorViewModel: ViewModelBase
    {
        public ICommand CreateCard { get; set; }


        public ObservableCollection<CardRowViewModel> _CardsSource;
        private ICollectionView _Cards;
        public ICollectionView Cards
        {
            get { return _Cards; }
            set { _Cards = value; RaisePropertyChanged(); }
        }

        public ICommand LoadCardsCommand { get; set; }

        public CardEditorViewModel()
        {
            CreateCard = new RelayCommand(() => ExecuteShowCreateCard());
            LoadCardsCommand = new RelayCommand(() => ExecuteLoadCard());
            _CardsSource = new ObservableCollection<CardRowViewModel>();
            _Cards = new CollectionView(_CardsSource);
            LoadCardsCommand.Execute(null);
        }

        private void ExecuteLoadCard()
        {
            IGiftCardRepository repo = new CardRepositoryMock();
            MainBusinessLayer layer = new MainBusinessLayer(repo);           
            var cards = layer.FetchAllCards();
            _CardsSource.Clear();
           
            foreach (Card item in cards)
            {
                var vmCardRow = new CardRowViewModel(item);
                _CardsSource.Add(vmCardRow);
            }
        }

        private void ExecuteShowCreateCard()
        {
            Messenger.Default.Send(new ShowCreateCardMessage());
        }         

        
    }
}
