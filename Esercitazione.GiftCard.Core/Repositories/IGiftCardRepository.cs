using Esercitazione.GiftCard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.GiftCard.Core.Repository
{
    public interface IGiftCardRepository
    {
        IList <Card> GetAll();

        void Create(Card card);

        void Update(Card oldCard, Card newCard);

        void Delete(Card card);

    }
}
