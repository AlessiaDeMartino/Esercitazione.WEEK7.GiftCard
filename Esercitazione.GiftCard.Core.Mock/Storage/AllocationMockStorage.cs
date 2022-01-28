using Esercitazione.GiftCard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.GiftCard.Core.Mock.Storage
{
    public class AllocationMockStorage
    {
        public static IList<Card> Cards { get; set; }


        public static void Initialize()
        {

            //creazione della lista vuota
            Cards = new List<Card>();

           Cards.Add(new Card
            {
                Id = 1,
                Mittente = "Alessia",
                Destinatario ="Giulia",
                Messaggio="Buon onomastico",
                Importo=50,
                Datadiscadenza= new DateTime (2022,10,15)

            });
            Cards.Add(new Card
            {
                Id = 2,
                Mittente = "Ginevra",
                Destinatario = "Luca",
                Messaggio = "Buon compleanno!",
                Importo = 100,
                Datadiscadenza = new DateTime(2022,09,01)
            });
        }
    }
}
