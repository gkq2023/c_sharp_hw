using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp {
  public class Product {
    public string ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product() {
    }

    public Product(string iD, string name, double price) {
      ID = iD;
      Name = name;
      Price = price;
    }

    public override bool Equals(object obj) {
      var product = obj as Product;
      return product != null &&
             ID == product.ID &&
             Name == product.Name;
    }

    public override int GetHashCode() {
      var hashCode = 1479869798;
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
      return hashCode;
    }
  }


}
