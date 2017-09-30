using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class fnSecurity
    {

        public string GetMd5Hash(string Input)
        {
            byte[] array = MD5.Create().ComputeHash(Encoding.Default.GetBytes(Input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= array.Length - 1; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

    }

}
