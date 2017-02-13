using ReactShop.Core.Entities;

namespace ReactShop.Core.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Quantity * Price;
            }
        }

        public static CartItemDTO FromCartItem(CartItem cartItem)
        {
            return new CartItemDTO
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                CustomerId = cartItem.CustomerId
            };
        }

        public static explicit operator CartItemDTO(CartItem cartItem)
        {
            return FromCartItem(cartItem);
        }


        public static explicit operator CartItem(CartItemDTO dto)
        {
            return dto.ToCartItem();
        }

        public CartItem ToCartItem()
        {
            return new CartItem(Id, ProductId, Quantity, CustomerId);
        }
    }
}