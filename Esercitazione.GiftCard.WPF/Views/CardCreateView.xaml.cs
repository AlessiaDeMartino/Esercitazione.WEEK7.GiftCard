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
    /// Interaction logic for CardCreateView.xaml
    /// </summary>
    public partial class CardCreateView : Window
    {
        public CardCreateView()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseCreateCardMessage>(this, _ => Close());

            Closing += (s, e) =>
            {               
                Messenger.Default.Unregister(this);
                Messenger.Default.Unregister(DataContext);
            };
        }
    }
}
