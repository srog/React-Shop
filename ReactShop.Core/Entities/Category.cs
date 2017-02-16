using System.ComponentModel.DataAnnotations.Schema;

namespace ReactShop.Core.Entities
{
    [Table("Category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }
    }
}