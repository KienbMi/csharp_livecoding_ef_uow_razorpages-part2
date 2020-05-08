using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeSlotMachine.Core.Entities
{
  /// <summary>
  /// Produkt mit Namen und Preis
  /// </summary>
  public class Product : EntityObject
  {
    /// <summary>
    /// Produktbezeichnung
    /// </summary>
    [DisplayName("Product")]
    [MinLength(2)]
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }

    [DisplayName("Price")]
    [Range(0, 1000, ErrorMessage ="The {0} has to be between {1} and {2}")]
    public int PriceInCents { get; set; }

    /// <summary>
    /// Bild wird als Byte[] direkt in Datenbank gespeichert
    /// </summary>
    public byte[] Image { get; set; }
    public ICollection<Order> Orders { get; set; }

    public Product()
    {
      Orders = new List<Order>();
    }
  }
}
