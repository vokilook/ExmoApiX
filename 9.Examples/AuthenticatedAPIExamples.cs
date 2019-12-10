using System;
using System.Collections.Generic;
using System.Text;
using ExmoApiX.AuthenticatedAPIRequests;

namespace ExmoApiX.Examples
{
    static class AuthenticatedAPIExamples
    {
        public static void User_Info(User user)
        {
            Console.WriteLine("--------------------API command \"user_info\":");
            var userInfo = new User_Info(ref user);
            Console.WriteLine($"User ID: {userInfo.uid}");
            foreach (KeyValuePair<string, double> balance in userInfo.balances)
                if (balance.Value > 0)
                    Console.WriteLine(balance.Key + " " + balance.Value);
            Console.WriteLine($"--------Request status {userInfo.RequestSucceed}. " +
                $"{userInfo.RequestError}.");

        }
        public static string Order_Create(User user)
        {
            Console.WriteLine("--------------------API command \"order_create\":");
            double quantity = 0.002;
            double price = 1.1 * (new Required_Amount("BTC_USD", quantity).avg_price);
            var newOrder = new Order_Create(user, "BTC_USD", quantity, price, TypeOfOrder.sell);
            if (!newOrder.RequestSucceed)
                Console.WriteLine($"Result of operation: fail, because of {newOrder.RequestError}.");
            else
                Console.WriteLine($"Order {newOrder.order_id} created.");
            Console.WriteLine($"--------Request status {newOrder.RequestSucceed}. " +
                $"{newOrder.RequestError}.");
            return newOrder.order_id;
        }
        public static void User_Open_Orders(User user)
        {
            Console.WriteLine("--------------------API command \"user_open_orders\":");
            var Orders = new User_Open_Orders(user);
            foreach (KeyValuePair<string, List<Order>> pair in Orders.userOpenOrders)
                foreach (Order order in pair.Value)
                    Console.WriteLine(pair.Key + " " + order.order_id + " " + order.quantity);
            Console.WriteLine($"--------Request status {Orders.RequestSucceed}. " +
                $"{Orders.RequestError}.");
        }
        public static void Order_Trades(User user, string order_id)
        {
            Console.WriteLine("--------------------API command \"order_trades\":");
            var trade = new Order_Trades(user, order_id);
            Console.WriteLine($"Order {order_id}. Type: {trade.orderTrade.type}. " +
                $"Number of deals: {trade.orderTrade.trades.Count}.");
            Console.WriteLine($"--------Request status {trade.RequestSucceed}. " +
                $"{trade.RequestError}.");
        }
        public static void Order_Cancel(User user, string order_id)
        {
            Console.WriteLine("--------------------API command \"order_cancel\":");
            var cancelOrder = new Order_Cancel(user, order_id);
            if (!cancelOrder.RequestSucceed)
                Console.WriteLine($"Result of operation: fail, because of {cancelOrder.RequestError}.");
            else
                Console.WriteLine($"Order {order_id} cancelled.");
            Console.WriteLine($"--------Request status {cancelOrder.RequestSucceed}. " +
                $"{cancelOrder.RequestError}.");
        }
        public static void User_Trades(User user)
        {
            Console.WriteLine("--------------------API command \"user_trades\":");
            List<string> pairs = new List<string> { "BTC_USD", "ETH_BTC" };
            var trades = new User_Trades(user, pairs, 3, 0);
            foreach (KeyValuePair<string, List<Order>> pair in trades.userTrades)
                foreach (Order order in pair.Value)
                    Console.WriteLine(order.order_id + " " + order.pair + " " 
                        + order.type + " " + order.quantity);
            Console.WriteLine($"--------Request status {trades.RequestSucceed}. " +
                $"{trades.RequestError}.");
        }
        public static void User_Cancelled_Orders(User user)
        {
            Console.WriteLine("--------------------API command \"user_cancelled_orders\":");
            var cancelledOrders = new User_Cancelled_Orders(user, 5, 0);
            foreach (Order order in cancelledOrders.userCancelledOrders)
                Console.WriteLine(order.order_id + " " + order.pair + " " + order.quantity);
            Console.WriteLine($"--------Request status {cancelledOrders.RequestSucceed}. " +
                $"{cancelledOrders.RequestError}.");
        }
        public static void Required_Amount()
        {
            Console.WriteLine("--------------------API command \"required_amount\":");
            var reqAmount = new Required_Amount("BTC_USD", 1.1);
            if (reqAmount.RequestSucceed)
                Console.WriteLine($"To buy {1.1} BTC needs {reqAmount.amount} USD.");
            else
                Console.WriteLine(reqAmount.RequestError);
            Console.WriteLine($"--------Request status {reqAmount.RequestSucceed}. " +
                $"{reqAmount.RequestError}.");
        }
        public static void Deposit_Address(User user)
        {
            Console.WriteLine("--------------------API command \"deposit_address\":");
            var Addresses = new Deposit_Address(user);
            if (Addresses.depositAddress != null && Addresses.depositAddress.ContainsKey("BTC"))
                Console.WriteLine("BTC: " + Addresses.depositAddress["BTC"]);
            else
                Console.WriteLine(Addresses.RequestError);
            Console.WriteLine($"--------Request status {Addresses.RequestSucceed}. " +
                $"{Addresses.RequestError}.");
        }
    }
}
