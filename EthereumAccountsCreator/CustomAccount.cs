using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAccountsCreator
{
    public class CustomAccount
    {
        public string address { get; set; }
        public string privateKey { get; set; }
        public string mnemo { get; set; }
        public List<string> projects { get; set; } = new List<string>();

        public CustomAccount(string Address, string PrivateKey, string Mnemo)
        {
            address = Address;
            privateKey = PrivateKey;
            mnemo = Mnemo;
        }
    }
}
