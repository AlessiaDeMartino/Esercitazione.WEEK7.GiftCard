using Esercitazione.GiftCard.WPF.Messaging.Card;
using Esercitazione.GiftCard.WPF.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Esercitazione.GiftCard.WPF.Views
{
    /// <summary>
    /// Interaction logic for CardEditorView.xaml
    /// </summary>
    public partial class CardEditorView : Window
    {
        public CardEditorView()
        {
            InitializeComponent();
            Messenger.Default.Register<ShowCreateCardMessage>(this, OnShowCreateCardExecuted);
            Messenger.Default.Register<ShowUpdateCardMessage>(this, OnShowUpdateCardMessageReceived);
        }

        private void OnShowUpdateCardMessageReceived(ShowUpdateCardMessage obj)
        {
            UpdateCardView view = new UpdateCardView();
            UpdateCardViewModel vm = new UpdateCardViewModel(obj.Entity);
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void OnShowCreateCardExecuted(ShowCreateCardMessage obj)
        {
            CardCreateView view = new CardCreateView();
            CardCreateViewModel vm = new CardCreateViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
