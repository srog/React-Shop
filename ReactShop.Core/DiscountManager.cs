using ReactShop.Core.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ReactShop.Core
{
    public class DiscountManager : IDiscountManager
    {
        public List<DiscountRuleDTO> Discounts { get; private set; }

        static DiscountManager instance;
        public static DiscountManager Instance => instance ?? (instance = new DiscountManager());

        public DiscountManager()
        {
            Discounts = new List<DiscountRuleDTO>();
            Discounts.Add(new DiscountRuleDTO(0, 499.99M, 0));
            Discounts.Add(new DiscountRuleDTO(500M, 599.99M, .05M));
            Discounts.Add(new DiscountRuleDTO(600M, 699.99M, .10M));
            Discounts.Add(new DiscountRuleDTO(700M, decimal.MaxValue, .15M));
        }

        public DiscountRuleDTO GetDiscount(decimal subtotal)
        {
            return Discounts.FirstOrDefault(d => d.CheckRange(subtotal));
        }
    }
}
