using Microsoft.Extensions.Options;
using PayPal.Api;
using PaypalExpressCheckout.BusinessLogic.ConfigOptions;
using PaypalExpressCheckout.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace PaypalExpressCheckout.BusinessLogic
{
    public class PaypalServices : IPaypalServices
    {
        private readonly PayPalAuthOptions _options;
        //getting the apiContext as earlier
        private APIContext apiContext = new APIContext(new OAuthTokenCredential("AdDK_DrhY6dV9ZdYG91VuHLV_NrwdmAmKReFN5WcanB7IeQjIRON1OYyT4erIBvWbb5O4EU-NRogwy5A", "EBbk5QW5erAOGTl5oiNNIQXFVbJW8fGEZ4WAjquiOvUu3hBrgXlbYfW5x3ZpboTyfeE-9Nnf-Fw4aVlr").GetAccessToken());

        public PaypalServices(IOptions<PayPalAuthOptions> options)
        {
            _options = options.Value;
        }

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent)
        {
            // var sach = await context.Sach
            //             .Include(x => x.TacGia)
            //             .Include(x => x.NhaXuatBan)
            //             .Include(x => x.DanhMuc)
            //             .FirstOrDefaultAsync(x => x.id == id);
            var payment = new Payment()
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(amount),
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                }
            };

            var createdPayment = payment.Create(this.apiContext);

            return createdPayment;
        }


        private List<Transaction> GetTransactionsList(decimal amount)
        {
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = amount.ToString(),
                    details = new Details()
                    {
                        tax = "0",
                        shipping = "0",
                        subtotal = amount.ToString()
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Payment",
                            currency = "USD",
                            price = amount.ToString(),
                            quantity = "1",
                            sku = "sku"
                        }
                    }
                },
                payee = new Payee
                {
                    // TODO.. Enter the payee email address here
                    email = "duc.huu128-facilitator@gmail.com",

                    // TODO.. Enter the merchant id here
                    merchant_id = "MBS7H57SPXT6S"
                }
            });

            return transactionList;
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {

            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            var executedPayment = new Payment() { id = paymentId }.Execute(this.apiContext, paymentExecution);

            return executedPayment;
        }

        private string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999999).ToString();
        }
    }
}
