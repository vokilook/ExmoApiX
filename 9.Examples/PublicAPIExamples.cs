using System;
using System.Collections.Generic;
using ExmoApiX.PublicAPIRequests;

namespace ExmoApiX.Examples
{
    static class PublicAPIExamples
    {
        public static void Currency()
        {
            Console.WriteLine("--------------------API command \"currency\":");
            Currency currency = new Currency();
            foreach (var curr in currency.currencies)
                Console.Write(curr + "; ");
            Console.WriteLine();
            Console.WriteLine($"--------Request status {currency.RequestSucceed}. " +
                $"{currency.RequestError}.");
        }
        public static void Pair_Settings()
        {
            Console.WriteLine("--------------------API command \"pair_settings\":");
            List<string> pairs = new List<string> { "BTC_USD", "ETH_BTC" };
            Pair_Settings settings = new Pair_Settings();
            foreach (var pair in pairs)
                Console.WriteLine($"Pair {pair} minimum quantity: " +
                    $"{settings.pair_settings[pair].min_quantity}");
            Console.WriteLine($"--------Request status {settings.RequestSucceed}. " +
                $"{settings.RequestError}.");
        }
        public static void Ticker()
        {
            Console.WriteLine("--------------------API command \"ticker\":");
            List<string> pairs = new List<string> { "BTC_USD", "ETH_BTC" };
            Ticker ticker = new Ticker();
            foreach (var pair in pairs)
                Console.WriteLine($"Pair {pair} buy price: " +
                    $"{ticker.ticker[pair].buy_price}, {ticker.ticker[pair].updated}");
            Console.WriteLine($"--------Request status {ticker.RequestSucceed}. " +
                $"{ticker.RequestError}.");
        }
        public static void Trade()
        {
            Console.WriteLine("--------------------API command \"trades\":");
            List<string> pairs = new List<string> { "BTC_USD", "ETH_BTC" };
            Trades trade = new Trades(pairs, 5);
            foreach (var tradesPair in trade.trades)
            {
                Console.WriteLine(tradesPair.Key);
                int counter = 0;
                foreach (var t in tradesPair.Value)
                {
                    if (++counter > 10)
                    {
                        counter = 0;
                        Console.WriteLine(">>>>10+");
                        break;
                    }
                    Console.WriteLine($"trade_id {t.trade_id}, " +
                        $"date {t.date}");
                }
            }
            Console.WriteLine($"--------Request status {trade.RequestSucceed}. " +
                $"{trade.RequestError}.");
        }
        public static void OrderBook()
        {
            Console.WriteLine("--------------------API command \"order_book\":");
            List<string> pairs = new List<string> { "BTC_USD", "ETH_BTC" };
            Order_Book book = new Order_Book(pairs, 10);
            foreach (var ordersPair in book.orderBook)
            {
                Console.WriteLine(ordersPair.Key);
                Console.WriteLine($"ask_top {ordersPair.Value.ask_top}, " +
                        $"bid_top {ordersPair.Value.bid_top}");
            }
            Console.WriteLine($"--------Request status {book.RequestSucceed}. " +
                $"{book.RequestError}.");

        }
    }
}
