using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspComet.MessageHandlers
{
    public static class ErrorMessages
    {
        public static string ClientIDNotRecognized(string clientId)
        {
            return "402:" + clientId + ":clientId not recognized.";
        }
    }
}
