using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_FE.ViewModels
{
    public class BaseResponse
    {
        [DefaultValue(false)]
        public bool Success { get; set; }

        [DefaultValue("")]
        public string Message { get; set; }
    }
}
