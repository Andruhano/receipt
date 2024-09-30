public enum DiscountType
{
    None,       
    Percentage, 
    FixedAmount 
}

public struct Product
{
    public string Name;         
    public int Quantity;       
    public decimal UnitPrice;   
    public decimal Discount;    
    public DiscountType DiscountType;  

    public Product(string name, int quantity, decimal unitPrice, decimal discount, DiscountType discountType)
    {
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        DiscountType = discountType;
    }

    public decimal GetTotalPrice()
    {
        decimal total = UnitPrice * Quantity;

        if (DiscountType == DiscountType.Percentage)
        {
            total -= total * (Discount / 100);
        }
        else if (DiscountType == DiscountType.FixedAmount)
        {
            total -= Discount;
        }

        return total;
    }
}

public struct Receipt
{
    public List<Product> Products; 

    public Receipt()
    {
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
    public void PrintReceipt()
    {
        Console.WriteLine("---------- КАССОВЫЙ ЧЕК ----------");
        Console.WriteLine("Наименование\tКол-во\tЦена\tИтого\tСкидка");

        decimal totalPrice = 0;

        foreach (var product in Products)
        {
            decimal productTotal = product.GetTotalPrice();
            totalPrice += productTotal;

            string discountInfo = product.DiscountType == DiscountType.None ? "-" : product.Discount.ToString();
            Console.WriteLine($"{product.Name}\t{product.Quantity}\t{product.UnitPrice}\t{productTotal}\t{discountInfo}");
        }

        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Общая сумма: {totalPrice}");
        Console.WriteLine("Спасибо за покупку!");
    }
}

class Program
{
    static void Main()
    {
        Receipt receipt = new Receipt();

        receipt.AddProduct(new Product("Молоко", 2, 1.50m, 10, DiscountType.Percentage));
        receipt.AddProduct(new Product("Хлеб", 1, 0.80m, 0, DiscountType.None));
        receipt.AddProduct(new Product("Шоколад", 3, 2.00m, 5, DiscountType.FixedAmount));

        receipt.PrintReceipt();
    }
}