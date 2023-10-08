using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Cryptography
{
    interface IDecryptMethod<in V, out T>
    {
        public T Decrypt(V data);
    }
}
