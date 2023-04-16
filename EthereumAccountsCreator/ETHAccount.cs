using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAccountsCreator
{
    public class ETHAccount
    {
        public string address { get; set; }
        public string privateKey { get; set; }
        public string publicKey { get; set; }
        public List<string> projects { get; set; } = new List<string>();

        public ETHAccount(string Address, string PrivateKey, string PublicKey)
        {
            address = Address;
            privateKey = PrivateKey;
            publicKey = PublicKey;
        }
    }
}
