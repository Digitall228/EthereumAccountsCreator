using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using NBitcoin;
using Nethereum.Web3.Accounts;
using Nethereum.Util;
using Nethereum.HdWallet;
using PassGen;

namespace EthereumAccountsCreator
{
    class Program
    {
        public static string ethPath = "ethWallets.json";
        public static string algoPath = "algoWallets.json";
        
        static void Main(string[] args)
        {
            Console.WriteLine("STARTED");
            Console.WriteLine("/create {eth/algo} {count} - to create {eth/algo} accounts in amount of {count}");
            Console.WriteLine("/change_path {eth/algo} {path} - to change path of saving {eth/algo} accounts");

            while (true)
            {
                string command = Console.ReadLine();
                if(command.Contains("/create"))
                {
                    string[] data = command.Split(' ');
                    if (int.TryParse(data[2], out int count))
                    {
                        switch(data[1])
                        {
                            case "eth":
                                CreateEthAccounts(count);
                                break;
                            case "algo":
                                IEnumerable<CustomAccount> accounts = AlgoGenerator.CreateAlgoAccounts(count);
                                Logger.LogAdd($"Created {accounts.Count()} {data[1]} accounts", ConsoleColor.Green);
                                SaveAccounts(accounts, algoPath);
                                break;
                            default:
                                Logger.LogAdd($"Have not found such a netwotk type", ConsoleColor.Red);
                                break;
                        }
                    }
                    else
                    {
                        Logger.LogAdd($"Count entered incorrect", ConsoleColor.Red);
                    }
                }
                else if (command.Contains("/change_path"))
                {
                    string[] data = command.Split(' ');
                    switch(data[1])
                    {
                        case "eth":
                            Logger.LogAdd($"Successfully changed eth path from {ethPath} to {data[2]}", ConsoleColor.Green);
                            ethPath = data[2];
                            break;
                        case "algo":
                            Logger.LogAdd($"Successfully changed algo path from {algoPath} to {data[2]}", ConsoleColor.Green);
                            algoPath = data[2];
                            break;
                        default:
                            Logger.LogAdd($"Have not found such a netwotk type", ConsoleColor.Red);
                            break;
                    }
                    
                }
            }

        }
        static void CreateEthAccounts(int count)
        {
            List<CustomAccount> accounts = new List<CustomAccount>();
            while (accounts.Count < count)
            {
                Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
                string password = PasswordGenerator.Generate();
                var wallet = new Wallet(mnemo.ToString(), password);
                var account = wallet.GetAccount(0);

                CustomAccount newAccount = new CustomAccount(account.Address, account.PrivateKey, mnemo.ToString());
                accounts.Add(newAccount);
            }
            Logger.LogAdd($"Created {accounts.Count} eth accounts", ConsoleColor.Green);
            SaveAccounts(accounts, ethPath);
        }
        
        static void ReadAccounts<T>(string path, ref List<T> accounts)
        {
            accounts = new List<T>();
            if (!File.Exists(path))
            {
                return;
            }
            using (StreamReader sr = new StreamReader(path))
            {
                accounts = JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());
                if (accounts == null)
                {
                    accounts = accounts = new List<T>();
                }
            }
        }
        static void SaveAccounts<T>(IEnumerable<T> accounts, string path)
        {
            List<T> oldAccounts = new List<T>();
            ReadAccounts(path, ref oldAccounts);

            using (StreamWriter sw = new StreamWriter(path))
            {
                oldAccounts.AddRange(accounts);
                string text = JsonConvert.SerializeObject(oldAccounts, Formatting.Indented);
                sw.Write(text);
            }

            Logger.LogAdd("Accounts saved", ConsoleColor.Green);
        }
    }
}
