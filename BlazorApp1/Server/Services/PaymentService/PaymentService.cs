using Stripe;
using Stripe.Checkout;

namespace BlazorApp1.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        const string secret = "";

        public PaymentService(ICartService cartService,
            IAuthService authService,
            IOrderService orderService)
        {
            StripeConfiguration.ApiKey = "sk_test_51NiizJHXW61iEZ4lsMG6Grdsj0G816VEWtIhpgTsXeqjYDIs7QAH7CMszNVI3FSFtUtQTq2EOBvBDPszW4xVZ0vF00LSwuraBG";

            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            var lineitems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineitems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl}
                    }
                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineitems,
                Mode = "payment",
                SuccessUrl = "http://localhost/7011/order-success",
                CancelUrl = "http://localhost/7011/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    secret
                );

                if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }

                return new ServiceResponse<bool> { Data = true };
            }
            catch (Exception e) 
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };    
            }
        }
    }
}
