using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tridion.ContentManager;

namespace Tridion.Extensions.ContentManager
{
    public static class TcmUriExtensions
    {
        public static string StripTcmUri(this TcmUri tcmUri)
        {
            var tcm = tcmUri.ToString();
            return tcm.Replace(":", string.Empty).Replace("-", string.Empty);

        }
    }
}
