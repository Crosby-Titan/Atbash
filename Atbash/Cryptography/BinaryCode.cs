using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Cryptography
{
    public class BinaryCode : ICryptoService<string, string>
    {
        private Encoding _ASCIIEncoding;

        public BinaryCode(Encoding codepage)
        {
            _ASCIIEncoding = codepage;
        }

        public BinaryCode()
        {
            _ASCIIEncoding = Encoding.ASCII;
        }

        public string Decrypt(string? data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return BinaryDecrypt(data);
        }

        public string Encrypt(string? data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return BinaryEncrypt(data);
        }

        private string BinaryDecrypt(string data)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < (data.Length / 8); i++)
            {
                var a = data.Substring(i * 8, 8);
                byte b = Convert.ToByte(a, 2);
                bytes.Add(b);
            }

            return _ASCIIEncoding.GetString(bytes.ToArray());

        }

        private string BinaryEncrypt(string data)
        {
            var bytes = _ASCIIEncoding.GetBytes(data);

            string result = "";

            foreach (var item in bytes)
            {
                result += Convert.ToString(item, 2).PadLeft(8, '0');
            }

            return result;
        }
    }
}
