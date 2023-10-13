using Atbash.LanguageSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Cryptography
{
    internal interface ICryptoService<T, V> : IEncryptMethod<T, V>, IDecryptMethod<V, T>
    {

    }
}
