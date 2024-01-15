using EFCORE_05;
using Microsoft.Extensions.Configuration;
using System;
namespace EFCORE
{
    class Program
    {
        public static void Main()
        {
            RetrieveData();
            Console.ReadKey();

        }

        private static void RetrieveData()
        {
            using (var context = new AppDbContext())
            {
                foreach (var wallet in context.Wallets)
                {
                    Console.WriteLine(wallet);
                }
            }
            Console.ReadKey();
        }

        private static void RetrieveOneItem()
        {
            var itemToRetrieve = 2;

            using (var context = new AppDbContext())
            {
                var item = context.Wallets.FirstOrDefault(x => x.Id == itemToRetrieve);
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        private static void InsertData()
        {
            var wallet = new Wallet
            {
                Holder = "Salah",
                Balance = 1200m
            };
            using (var context = new AppDbContext())
            {
                context.Wallets.Add(wallet);
                context.SaveChanges();
            }
            Console.ReadKey();
        }
        private static void UpdateData()
        {
            using (var context = new AppDbContext())
            {
                //update wallet (id = 4) increase balance by 1000

                var wallet = context.Wallets.Single(x => x.Id == 4);

                wallet.Balance += 100000000000;
                context.SaveChanges();

            }
            Console.ReadKey();
        }
        private static void DeleteData()
        {
            using (var context = new AppDbContext())
            {
                var wallet = context.Wallets.Single(x => x.Id == 4);
                context.Wallets.Remove(wallet);
                context.SaveChanges();
            }

            Console.ReadKey();
        }
        private static void QueryData()
        {
            using (var context = new AppDbContext())
            {
                var result = context.Wallets.Where(x => x.Balance > 5000);
                foreach (var wallet in result)
                {
                    Console.WriteLine(wallet);
                }
            }

            Console.ReadKey();
        }
        private static void Transaction()
        {
            using (var context = new AppDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    //transfer $500 from wallet id = 5 to wallet id = 6
                    var fromWallet = context.Wallets.Single(x => x.Id == 5);
                    var toWallet = context.Wallets.Single(x => x.Id == 6);
                    var amountToTransfer = 500m;
                    fromWallet.Balance -= amountToTransfer;
                    context.SaveChanges();
                    //
                    toWallet.Balance += amountToTransfer;
                    context.SaveChanges();
                    transaction.Commit();  //excute
                }
            }

            Console.ReadKey();
        }
    }
}