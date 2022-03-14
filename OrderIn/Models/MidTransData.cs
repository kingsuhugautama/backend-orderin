using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderInBackend.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class MidTransAuthorizationResponse{
        public string token { get; set; }
        public string redirect_url { get; set; }
    }

    public class Root
    {
        public CustomerDetails customer_details { get; set; }
        public List<ItemDetail> item_details { get; set; }
        public TransactionDetails transaction_details { get; set; }
        public string user_id { get; set; }
    }

    public class BillingAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
    }

    public class ShippingAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
    }

    public class CustomerDetails
    {
        public BillingAddress billing_address { get; set; }
        public string customer_identifier { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }

    public class ItemDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }

    public class TransactionDetails
    {
        public string currency { get; set; }
        public double gross_amount { get; set; }
        public string order_id { get; set; }
    }


}
