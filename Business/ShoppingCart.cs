using Abstract_Factory_Pattern.Business.Models.Commerce;
using Abstract_Factory_Pattern.Business.Models.Shipping;
using Abstract_Factory_Pattern.Business.Models.Shipping.Factories;
using System;

namespace Abstract_Factory_Pattern.Business
{
    public class ShoppingCart
    {
        private readonly Order order;
        private readonly IPurchaseProviderFactory purchaseProviderFactory;

        public ShoppingCart(Order order, IPurchaseProviderFactory purchaseProviderFactory)
        {
            this.order = order;
            this.purchaseProviderFactory = purchaseProviderFactory;
        }

        public string Finalize()
        {
            var shippingProvider = purchaseProviderFactory.CreateShippingProvider(order);
            var invoice = purchaseProviderFactory.CreateInvoice(order);

            // Send invoice..
            var summary = purchaseProviderFactory.CreateSummary(order);
            summary.Send();

            order.ShippingStatus = ShippingStatus.ReadyForShippment;

            return shippingProvider.GenerateShippingLabelFor(order);
        }
    }
}
