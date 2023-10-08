using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Cryptography
{
    interface IEncryptMethod<in T, out V>
    {
        public V Encrypt(T data);
    }
}
