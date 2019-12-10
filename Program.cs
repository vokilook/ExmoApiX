using System;
using ExmoApiX.Examples;
using ExmoApiX.AuthenticatedAPIRequests;

namespace ExmoApiX
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("---------------Public API---------------");
                PublicAPIExamples.Currency();
                PublicAPIExamples.Pair_Settings();
                PublicAPIExamples.Ticker();
                PublicAPIExamples.Trade();
                PublicAPIExamples.OrderBook();
            }
            {
                string key = "/*type your key*/";
                string secret = "/*type your secret*/";
                User user = new User(key, secret);

                Console.WriteLine();
                Console.WriteLine("---------------Authenticated API---------------");
                AuthenticatedAPIExamples.User_Info(user);
                string order_id = AuthenticatedAPIExamples.Order_Create(user);
                AuthenticatedAPIExamples.User_Open_Orders(user);
                AuthenticatedAPIExamples.Order_Trades(user, order_id);
                AuthenticatedAPIExamples.Order_Cancel(user, order_id);

                AuthenticatedAPIExamples.User_Trades(user);
                AuthenticatedAPIExamples.User_Cancelled_Orders(user);
                AuthenticatedAPIExamples.Required_Amount();
                AuthenticatedAPIExamples.Deposit_Address(user);
            }
        }
    }
}
