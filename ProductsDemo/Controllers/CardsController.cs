using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;
using ProductsDemo.Models;

namespace ProductsDemo.Controllers
{
    public class CardsController : ApiController
    {
        static List<Cards> cards = new List<Cards>();

        /*
        // Returns all cards in the array
        public IEnumerable<Cards> GetAllCards()
        {
            return cards;
        }
        */

        // Get the cards for a specific binder
        [HttpGet]
        [Route("api/cards/binder/{binderId}")]
        public IEnumerable<Cards> GetCardsByBinder(int binderId)
        {
            return cards.Where(c => c.BinderId == binderId);
        }

        /*
        // Calculates total value of collection
        [HttpGet]
        [Route("api/cards/totalvalue")]
        public IHttpActionResult GetCardValues()
        {
            decimal total = 0;
            foreach (var card in cards)
            {
                total += card.Price * card.Quantity;
            }

            return Ok(new { totalValue = total });
        }
        */

        // Calcuates total value of binder
        [HttpGet]
        [Route("api/cards/binder/{binderId}/totalvalue")]
        public IHttpActionResult GetBinderValue(int binderId)
        {
            decimal total = 0;
            var binderCards = cards.Where(c => c.BinderId == binderId);
            foreach (var card in binderCards)
            {
                total += card.Price * card.Quantity;
            }
            return Ok(new { totalValue = total });
        }

        /*
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
        */

        /*
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
        */

        // Add a new card or update existing in binder
        [HttpPost]
        [Route("api/cards/binder/{binderId}")]
        public IHttpActionResult PostCardToBinder(int binderId, Cards card)
        {
            if (card == null)
            {
                return BadRequest("Card cannot be null");
            }
            // Normalize input (trim whitespace)
            var name = card.Name?.Trim() ?? "";
            var edition = card.Edition?.Trim() ?? "";
            var quality = card.Quality?.Trim() ?? "";
            // Check if a card with the same Name, Edition, and Quality already exists in the binder (case-insensitive)
            var existingCard = cards.FirstOrDefault(c =>
                c.BinderId == binderId &&
                string.Equals(c.Name?.Trim(), name, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Edition?.Trim(), edition, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Quality?.Trim(), quality, StringComparison.OrdinalIgnoreCase));
            if (existingCard != null)
            {
                // Card exists in binder - increment quantity
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
                // New card for binder - add to list
                card.Id = cards.Count + 1;
                card.Name = name;
                card.Edition = edition;
                card.Quality = quality;
                card.Quantity = 1;
                card.BinderId = binderId;
                cards.Add(card);
                return Ok(card);
            }
        }

        /*
        // Remove a card or decrease amount
        [HttpDelete]
        [Route("api/cards/remove")]
        public IHttpActionResult RemoveCard(Cards card)
        {
            if (card == null)
            {
                return BadRequest("Card cannot be null");
            }

            // Sanitize input
            var name = card.Name?.Trim() ?? "";
            var edition = card.Edition?.Trim() ?? "";
            var quality = card.Quality?.Trim() ?? "";

            // Find match
            var existingCard = cards.FirstOrDefault(c =>
            string.Equals(c.Name?.Trim(), name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c.Edition?.Trim(), edition, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c.Quality?.Trim(), quality, StringComparison.OrdinalIgnoreCase));

            if (existingCard == null)
            {
                return BadRequest("Card not in collection");
            }

            if(card.Quantity > existingCard.Quantity)
            {
                return BadRequest($"Cannot remove {card.Quantity} amount.");
            }

            // Calculate remaining
            existingCard.Quantity -= card.Quantity;

            // If remaining is 0, remove from list
            if(existingCard.Quantity == 0)
            {
                cards.Remove(existingCard);
                return Ok(new { message = "Card remove from collection" });
            }

            return (Ok(new { message = $"Removed {card.Quantity} of {card.Name} from collection" }));

        }
        */

        // Remove a card or decrease amount from binder
        [HttpDelete]
        [Route("api/cards/binder/{binderId}/remove")]
        public IHttpActionResult RemoveCardFromBinder(int binderId, Cards card)
        {
            if (card == null)
            {
                return BadRequest("Card cannot be null");
            }
            // Sanitize input
            var name = card.Name?.Trim() ?? "";
            var edition = card.Edition?.Trim() ?? "";
            var quality = card.Quality?.Trim() ?? "";
            // Find match in binder
            var existingCard = cards.FirstOrDefault(c =>
                c.BinderId == binderId &&
                string.Equals(c.Name?.Trim(), name, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Edition?.Trim(), edition, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Quality?.Trim(), quality, StringComparison.OrdinalIgnoreCase));
            if (existingCard == null)
            {
                return BadRequest("Card not in binder");
            }
            if (card.Quantity > existingCard.Quantity)
            {
                return BadRequest($"Cannot remove {card.Quantity} amount.");
            }
            // Calculate remaining
            existingCard.Quantity -= card.Quantity;
            // If remaining is 0, remove from list
            if (existingCard.Quantity == 0)
            {
                cards.Remove(existingCard);
                return Ok(new { message = "Card removed from binder" });
            }
            return Ok(new { message = $"Removed {card.Quantity} of {card.Name} from binder" });
        }

        // Remove all cards in a binder when binder is deleted
        [HttpDelete]
        [Route("api/cards/binder/{binderId}")]
        public IHttpActionResult DeleteBinderCards(int binderId)
        {
            var binderCards = cards.Where(c => c.BinderId == binderId).ToList();
            if (!binderCards.Any())
            {
                return NotFound();
            }
            foreach (var card in binderCards)
            {
                cards.Remove(card);
            }
            return Ok(new { message = $"All cards in binder {binderId} have been removed" });
        }
    }
}
