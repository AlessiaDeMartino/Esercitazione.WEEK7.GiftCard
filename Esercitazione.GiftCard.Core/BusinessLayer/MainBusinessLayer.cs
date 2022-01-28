using Esercitazione.GiftCard.Core.Entities;
using Esercitazione.GiftCard.Core.Repository;
using Esercitazione.GiftCard.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.GiftCard.Core.BusinessLayer
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository _CardRepository;

        public MainBusinessLayer(IGiftCardRepository cardRepo)
        {
            _CardRepository = cardRepo;
        }

        //Restituire la lista di giftcards
        public IList<Card> FetchAllCards()
        {
         return _CardRepository.GetAll();
        }

        public Response CreateCard(Card entity)
        {
            
            if (entity == null)
                return new Response { Success = false, Message = "Invalid entity" };

            if (entity.Importo < 0.0)
                return new Response { Success = false, Message = "Importo must be positive" };

            _CardRepository.Create(entity);
            
            return new Response
            {
                Success = true,
                Message = $"GiftCard for: {entity.Destinatario} with euro {entity.Importo} created"
            };
        }

        public Response DeleteCard(Card entity)
        {
            if (entity == null)
                return new Response { Success = false, Message = "Invalid entity" };
            if (entity.Id < 0)
                return new Response { Success = false, Message = "Invalid ID" };
            var cardToDelete = FetchAllCards().FirstOrDefault(x => x.Id == entity.Id);
            if (cardToDelete == null)
                return new Response
                {
                    Success = false,
                    Message = $"No card with ID: {entity.Id}"
                };
            _CardRepository.Delete(cardToDelete);

            return new Response { Success = true, Message = $"Card deleted" };
        }

        public Response UpdateCard(Card entity)
        {
            //Validazione argomenti
            if (entity == null)
                return new Response() { Success = false, Message = "Incorrect entity" };

            //CERCO LA CARTA DA AGGIORNARE CON L'ID
            var cardToUpdate = FetchAllCards().FirstOrDefault(x => x.Id == entity.Id);            
            _CardRepository.Update(cardToUpdate, entity);
            return new Response() { Success = true, Message = "Card updated" };
        }

    }
}

