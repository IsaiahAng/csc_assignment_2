using csc_assignment_2.Areas.Identity.Data;
using csc_assignment_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.BillingPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace csc_assignment_2.Controllers
{

    public class PaymentController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public static string customer_id { get; set; }
        public static string global_plan_id { get; set; }

        private readonly IConfiguration _configuration;
        public IActionResult Index()
        {
            return View();
        }
        public PaymentController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = "sk_test_51HtmObHQbClkzxezsIKa0GNcvyKMngwztyFvlxirWE6QW4YKGADejt1ay8rTZqY4VvBu8a4aSTgYpNMtpLHJVBpC00P6J1uOX3";
            _userManager = userManager;


        }

        public IActionResult Subscribe()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(string email, string plan, string stripeToken)
        {
            CustomerModel cm = new CustomerModel();
            var customerOptions = new CustomerCreateOptions
            {
                Email = email,
                Source = stripeToken,
            };

            try
            {

                var customerService = new CustomerService();
                var customer = customerService.Create(customerOptions);
                customer_id = customer.Id;
                // Previous code in action

                var planId = "";
                if (plan == "freePlan")
                {
                    planId = "price_1IDVJ4HQbClkzxezOyceDztZ";
                    global_plan_id = "price_1IDVKDHQbClkzxezYq9YKGeA";
                }
                else
                {
                    planId = "price_1IDVKDHQbClkzxezYq9YKGeA";
                    global_plan_id = "price_1IDVKDHQbClkzxezYq9YKGeA";

                }

                var subscriptionOptions = new SubscriptionCreateOptions
                {
                    Customer = customer.Id,
                    Items = new List<SubscriptionItemOptions>
        {
            new SubscriptionItemOptions
            {
                Plan = planId
            },
        },
                };

                subscriptionOptions.AddExpand("latest_invoice.payment_intent");

                var subscriptionService = new SubscriptionService();
                var subscription = subscriptionService.Create(subscriptionOptions);
                DateTime dateTime = DateTime.UtcNow.Date;



                ViewBag.stripeKey = "pk_test_51HtmObHQbClkzxez6WOcUxOdar5GULzc7ZrGVaVtFgX4D8S76VYvFFa0HZCdmLbjBibRKlPn2xltaRghua3UGrup00zTmwT1ag";
                ViewBag.subscription = subscription.ToJson();
                cm.customer_id = subscription.CustomerId;
                cm.email = email.ToString();
                cm.subscription_status = subscription.Status;
                cm.customer_user_id = _userManager.GetUserId(HttpContext.User);
                if (planId == "price_1IDVJ4HQbClkzxezOyceDztZ")
                {
                    //cm.free = "True";
                    cm.premium = "False";
                    cm.SaveIntoUser("Free", email);

                }
                else
                {
                    //cm.free = "False";
                    cm.premium = "True";
                    cm.SaveIntoUser("Premium", email);

                }

                cm.SaveDetails();


                return LocalRedirect("/Home/Index");
                //return View("SubscribeResult");

            }
            catch (Exception e)
            {
                cm.customer_id = "not-valid";
                cm.email = email.ToString();
                cm.subscription_status = "not-valid";
                //cm.free = null;
                cm.premium = null;
                cm.SaveDetails();

                return View("SubscribeFailed");
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Payment/webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                string json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                string signingSecret = "whsec_HJE77ihhjSvGXnbeMZ7SQbVWe7QHWzvP";

                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], signingSecret);
                CustomerModel cm = new CustomerModel();

                switch (stripeEvent.Type)
                {
                    case "charge.succeeded":
                        cm.RecordChargeStatus("succeed", customer_id);
                        DateTime dt = DateTime.UtcNow.Date;

                        cm.LastPaid(dt.ToString("d"), customer_id);
                        break;
                    case "charge.failed":
                        cm.RecordChargeStatus("failed", customer_id);

                        break;

                    case "customer.subscription.deleted":
                        cm = new CustomerModel();
                        cm.PlanRemove(customer_id);

                        break;

                    //case "customer.subscription.updated":
                    //    var subUp = stripeEvent.Data.Object as Subscription;

                    //    cm = new CustomerModel();
                    //    //var result = cm.getDetailsWithId("cus_Ihi5ctmRFKeQ6X");
                    //    //var result = "True";
                    //    if (global_plan_id == "price_1IDVJ4HQbClkzxezOyceDztZ")
                    //    {
                    //        cm.PlanChange("premium", customer_id);
                    //        global_plan_id = "price_1IDVKDHQbClkzxezYq9YKGeA";
                    //    }
                    //    else
                    //    {
                    //        cm.PlanChange("free", customer_id);
                    //        global_plan_id = "price_1IDVJ4HQbClkzxezOyceDztZ";

                    //    }
                    //    break;


                }

                return Ok();
            }
            catch (StripeException ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPost("customer-portal")]
        public async Task<IActionResult> CustomerPortal()
        {
            // Authenticate your user.

            var options = new SessionCreateOptions
            {
                Customer = customer_id,
                ReturnUrl = "https://284c1e4ec544.ngrok.io/payment",
            };
            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);

        }

    }
}
