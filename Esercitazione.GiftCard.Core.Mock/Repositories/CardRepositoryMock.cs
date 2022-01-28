using Esercitazione.GiftCard.Core.Entities;
using Esercitazione.GiftCard.Core.Mock.Storage;
using Esercitazione.GiftCard.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.GiftCard.Core.Mock.Repositories
{
    public class CardRepositoryMock : IGiftCardRepository
    {
        public void Create(Card card)
        {
            var newId = AllocationMockStorage.Cards.Max(e => e.Id) + 1;
            card.Id = newId;
            AllocationMockStorage.Cards.Add(card);
        }

        public void Delete(Card card)
        {
            var existingCard = AllocationMockStorage.Cards.FirstOrDefault(c => c.Id == card.Id);
            AllocationMockStorage.Cards.Remove(existingCard);
        }

        public IList<Card> GetAll()
        {
            return AllocationMockStorage
              .Cards
              .OrderBy(x => x.Mittente)
              .ToList();
        }

        public void Update(Card oldCard, Card newCard)
        {
            var existingCard = AllocationMockStorage.Cards.FirstOrDefault(c => c.Id == newCard.Id);
            AllocationMockStorage.Cards.Remove(oldCard);
            AllocationMockStorage.Cards.Add(newCard);
        }
    }
}
