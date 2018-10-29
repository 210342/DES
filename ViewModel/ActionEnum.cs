using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESDe_Encryptor.ViewModel
{
    public enum ActionEnum
    {
        [Description("Decrypt")]
        Decrypt,
        [Description("Encrypt")]
        Encrypt
    }
}
