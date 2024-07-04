using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SimpleInventoryManagementSystem.ProductManagement.Entity
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public Product(string name, double price)
        {

            Name = name;
            Price = price;
        }

        public Product(string id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public virtual void IncreaseQuantity(int quantityToIncrease)
        {
            Quantity += quantityToIncrease;
        }

        public void DecreaseQuantity(int quantityToDecrease)
        {
            if (quantityToDecrease < Quantity)
            {
                Quantity = Quantity - quantityToDecrease;
            }
            else
            {
                Console.WriteLine($"Not enough items on stock. {Quantity} available but {quantityToDecrease} requested.");
            }

        }

        public void PrintProduct()
        {

            Console.WriteLine($"Id: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}");
        }


    }
}
