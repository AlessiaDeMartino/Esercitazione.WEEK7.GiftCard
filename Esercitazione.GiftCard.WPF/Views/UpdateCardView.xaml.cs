using Esercitazione.GiftCard.WPF.Messaging.Card;
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
    /// Interaction logic for UpdateCardView.xaml
    /// </summary>
    public partial class UpdateCardView : Window
    {
        public UpdateCardView()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseUpdateCardMessage>(this, _ => Close());

            //Alla chiusura della finestra
            Closing += (s, e) =>
            {
                
                Messenger.Default.Unregister(this);
                Messenger.Default.Unregister(DataContext);
            };
        }
    }
}
