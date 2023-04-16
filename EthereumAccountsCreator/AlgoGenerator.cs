using System;
using System.Collections.Generic;
using Algorand;

namespace EthereumAccountsCreator
{
    public static class AlgoGenerator
    {
        public static IEnumerable<CustomAccount> CreateAlgoAccounts(int count)
        {
            List<CustomAccount> accounts = new List<CustomAccount>();
            while (accounts.Count < count)
            {
                Account account = new Account();
                CustomAccount newAccount = new CustomAccount(account.Address.ToString(), account.ToMnemonic(), account.ToMnemonic());

                yield return newAccount;
            }
        }
    }
}
