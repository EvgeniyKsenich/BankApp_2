using System;
using System.Collections.Generic;
using System.Text;

namespace DA.Business.Modeles
{
    public class BooleanErrorModel
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public BooleanErrorModel()
        {
            Error = false;
            ErrorMessage = string.Empty;
        }
    }
}
