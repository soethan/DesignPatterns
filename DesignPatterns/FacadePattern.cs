using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //Sub-system for inventory management
    public interface IInventory
    {
        void Update(int productId);
    }
    
    public class InventoryManager : IInventory
    {
        public void Update(int productId)
        {
            Console.WriteLine(string.Format("Product# {0} is subtracted from the store's inventory.", productId));
        }
    }

    //Sub-system for order verification
    public interface IOrderVerify
    {
        bool VerifyShippingAddress(int postalCode);
    }
    
    public class OrderVerificationManager : IOrderVerify
    {
        public bool VerifyShippingAddress(int postalCode)
        {
            Console.WriteLine(string.Format("The product can be shipped to the postalCode {0}.", postalCode));
            return true;
        }
    }

    //Sub-system for discounts and costing
    public interface ICosting
    {
        float ApplyDiscounts(float price, float discountPercent);
    }
    
    public class CostManager : ICosting
    {
        public float ApplyDiscounts(float price, float discountPercent)
        {
            Console.WriteLine(string.Format("A discount of {0}% has been applied on the product's price of {1}", discountPercent,price));            
            return price - ((discountPercent / 100) * price);
        }
    }

    //Sub-system for payment gateway
    public interface IPaymentGateway
    {
        bool VerifyCardDetails(string cardNo);
        bool ProcessPayment(string cardNo, float cost);
    }
    
    public class PaymentGatewayManager : IPaymentGateway
    {
        public bool VerifyCardDetails(string cardNo)
        {
            Console.WriteLine(string.Format("Card# {0} has been verified and is accepted.", cardNo));
            return true;
        }

        public bool ProcessPayment(string cardNo, float cost)
        {
            Console.WriteLine(string.Format("Card# {0} is used to make a payment of {1}.", cardNo, cost));
            return true;
        }
    }

    //Sub-system for Logistics
    public interface ILogistics
    {
        void ShipProduct(string productName, string shippingAddress);
    }
    
    public class LogisticsManager : ILogistics
    {
        public void ShipProduct(string productName, string shippingAddress)
        {
            Console.WriteLine(
                string.Format("Congratulations your product {0} has been shipped at the following address: {1}", 
                productName, shippingAddress));
        }
    }

    public class OrderDetails
    {
        public int ProductNo { get; private set; }

        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public float Price { get; set; }
        public float DiscountPercent { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public int PostalCode { get; private set; }
        public string CardNo { get; private set; }

        public OrderDetails(string productName, string prodDescription, float price,
                            float discount, string addressLine1, string addressLine2,
                            int postalCode, string cardNo)
        {
            this.ProductNo = new Random(1).Next(1, 100);
            this.ProductName = productName;
            this.ProductDescription = prodDescription;
            this.Price = price;
            this.DiscountPercent = discount;
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.PostalCode = postalCode;
            this.CardNo = cardNo;
        }
    }

    public class OnlineShoppingFacade
    {
        IInventory inventory = new InventoryManager();
        IOrderVerify orderVerify = new OrderVerificationManager();
        ICosting costManger = new CostManager();
        IPaymentGateway paymentGateWay = new PaymentGatewayManager();
        ILogistics logistics = new LogisticsManager();

        public void FinalizeOrder(OrderDetails orderDetails)
        {
            inventory.Update(orderDetails.ProductNo);
            orderVerify.VerifyShippingAddress(orderDetails.PostalCode);
            orderDetails.Price = costManger.ApplyDiscounts(orderDetails.Price,
                                                           orderDetails.DiscountPercent);
            paymentGateWay.VerifyCardDetails(orderDetails.CardNo);
            paymentGateWay.ProcessPayment(orderDetails.CardNo, orderDetails.Price);

            logistics.ShipProduct(orderDetails.ProductName, string.Format("{0}, {1} - {2}.",
                                  orderDetails.AddressLine1, orderDetails.AddressLine2,
                                  orderDetails.PostalCode));
        }
    }
}
