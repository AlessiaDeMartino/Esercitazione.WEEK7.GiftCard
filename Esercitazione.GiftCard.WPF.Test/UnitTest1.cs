using Esercitazione.GiftCard.Core.BusinessLayer;
using Esercitazione.GiftCard.Core.Entities;
using Esercitazione.GiftCard.Core.Mock.Repositories;
using Esercitazione.GiftCard.Core.Mock.Storage;
using Esercitazione.GiftCard.Core.Repository;
using Esercitazione.GiftCard.Core.Utils;
using System;
using System.Linq;
using Xunit;

namespace Esercitazione.GiftCard.WPF.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldCreateBeOk()
        {
            //ARRANGE
            Card card = new Card()
            {
                Mittente = "Max",
                Destinatario = "Lucia",
                Messaggio = "Buone vacanze",
                Importo = 50,
                Datadiscadenza = new DateTime(2023, 09, 06)

            };
            AllocationMockStorage.Initialize();

            //Creazione del repository
            IGiftCardRepository cardRepository = new CardRepositoryMock();

            //Creazione del business layer
            MainBusinessLayer layer = new MainBusinessLayer(cardRepository);

            int countGiftCardBefore = layer.FetchAllCards().Count();

            //ACT
            Response add = layer.CreateCard(card);

            //ASSERT
            int countGiftAfter=layer.FetchAllCards().Count();
            Assert.Equal(countGiftCardBefore + 1, countGiftAfter);

        }
    }
}
