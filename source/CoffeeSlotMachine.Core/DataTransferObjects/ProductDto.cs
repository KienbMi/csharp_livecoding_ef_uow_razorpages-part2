using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeSlotMachine.Core.DataTransferObjects
{
  public class ProductDto
  {
    public int ProductId { get; set; }

    [DisplayName("Product")]
    [MinLength(2)]
    [MaxLength(30)]
    [Required]
    public string ProductName { get; set; }

    [DisplayName("Price")]
    [Range(0, 1000, ErrorMessage = "The {0} has to be between {1} and {2}")]
    public int PriceInCents { get; set; }

    [DisplayName("Orders")]
    [Range(0, int.MaxValue)]
    public int NrOfOrders { get; set; }
  }
}
