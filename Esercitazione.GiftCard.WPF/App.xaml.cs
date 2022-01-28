using Esercitazione.GiftCard.Core.Mock.Storage;
using Esercitazione.GiftCard.WPF.Messaging.Misc;
using Esercitazione.GiftCard.WPF.ViewModels;
using Esercitazione.GiftCard.WPF.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Esercitazione.GiftCard.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Messenger.Default.Register<DialogMessage>(this, OnDialogMessageReceived);
            AllocationMockStorage.Initialize();

            CardEditorView view = new CardEditorView();

            //Inizializzo il view model associato alla login
            CardEditorViewModel vm = new CardEditorViewModel();

            //Collego il vm alla view
            view.DataContext = vm;

            //Mostro la view
            view.Show();

            base.OnStartup(e);
        }

        private void OnDialogMessageReceived(DialogMessage obj)
        {
            MessageBoxResult result = MessageBox.Show(
               obj.Content,
               obj.Title,
               obj.Buttons, obj.Icon);

      
            if (obj.Callback != null)
                obj.Callback(result);
        }
    }
}
