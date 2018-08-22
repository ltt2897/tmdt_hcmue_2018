using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using PhuKienDienThoai.Models.SanPhamViewModels;
using PayPal.Api;
using PaypalExpressCheckout.BusinessLogic.ConfigOptions;
using PaypalExpressCheckout.BusinessLogic.Interfaces;

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
        

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent, List<GioHangViewModel> ListItemTrongGioHang)
        {


            var payment = new Payment()
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(amount, ListItemTrongGioHang),
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                }
            };

            var createdPayment = payment.Create(this.apiContext);

            return createdPayment;
        }


        private List<Transaction> GetTransactionsList(decimal amount, List<GioHangViewModel> ListItemTrongGioHang)
        {
            var transactionList = new List<Transaction>();
            var get_item_list = new List<Item>();

            foreach (var item in ListItemTrongGioHang)
            {
                // ChiTietHoaDon.Add(new Models.ChiTietHoaDon
                // {
                //     SanPhamId = item.SanPham.id,
                //     SoLuong = item.SoLuong,
                //     ThanhTien = item.SanPham.DonGia * item.SoLuong,
                // });
                var paypal_item = new Item()
                {
                    name = item.SanPham.TenSanPham,
                    currency = "VND",
                    price = amount.ToString(),
                    quantity = item.SoLuong.ToString(),
                    sku = "sku"
                };
                get_item_list.Add(paypal_item);
            }
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "VND",
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
                    items = get_item_list
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
