using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Login
{
    public enum LoginResult : int
    {
        Successfull,
        InvalidInputs,
        AccountDoesNotExists,
        WrongPassword,
    }
}
