using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProductsDemo.Models;

namespace ProductsDemo.Controllers
{
    public class CardsController : ApiController
    {
        static List<Cards> cards = new List<Cards>();

        // Returns all cards in the array
        public IEnumerable<Cards> GetAllCardss()
        {
            return cards;
        }

        // Returns individual cards within the array by its id
        public IHttpActionResult GetCards(int id)
        {
            var card = cards.FirstOrDefault((p) => p.Id == id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        // Add a new card or update existing
        [HttpPost]
        public IHttpActionResult PostCard(Cards card)
        {
            if (card == null)
            {
                return BadRequest("Card cannot be null");
            }

            // Normalize input (trim whitespace)
            var name = card.Name?.Trim() ?? "";
            var edition = card.Edition?.Trim() ?? "";
            var quality = card.Quality?.Trim() ?? "";

            // Check if a card with the same Name, Edition, and Quality already exists (case-insensitive)
            var existingCard = cards.FirstOrDefault(c => 
                string.Equals(c.Name?.Trim(), name, StringComparison.OrdinalIgnoreCase) && 
                string.Equals(c.Edition?.Trim(), edition, StringComparison.OrdinalIgnoreCase) && 
                string.Equals(c.Quality?.Trim(), quality, StringComparison.OrdinalIgnoreCase));

            if (existingCard != null)
            {
                // Card exists - increment quantity
                existingCard.Quantity++;
                
                // Update price if different
                if (existingCard.Price != card.Price)   
                {
                    existingCard.Price = card.Price;
                }
                
                return Ok(existingCard);
            }
            else
            {
                // New card - add to list
                card.Id = cards.Count + 1;
                card.Name = name;
                card.Edition = edition;
                card.Quality = quality;
                card.Quantity = 1;
                cards.Add(card);
                return Ok(card);
            }
        }
    }
}
