using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp {

  /**
   **/
  public class OrderDetail {

    public int Index { get; set; } //序号

    public Product ProductItem { get; set; }

    public String ProductName { get => ProductItem!=null? this.ProductItem.Name:""; }

    public double UnitPrice { get => ProductItem != null ? this.ProductItem.Price : 0.0; }


    public int Quantity { get; set; }

    public OrderDetail() { }

    public OrderDetail(int index, Product goods, int quantity) {
      this.Index = index;
      this.ProductItem = goods;
      this.Quantity = quantity;
    }

    public double TotalPrice {
      get => ProductItem==null?0.0: ProductItem.Price * Quantity;
    }

    public override string ToString() {
      return $"[No.:{Index},product:{ProductName},quantity:{Quantity},totalPrice:{TotalPrice}]";
    }

    public override bool Equals(object obj) {
      var item = obj as OrderDetail;
      return item != null &&
             ProductName == item.ProductName;
    }

    public override int GetHashCode() {
      var hashCode = -2127770830;
      hashCode = hashCode * -1521134295 + Index.GetHashCode();
      hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
      hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
      return hashCode;
    }
  }
}
