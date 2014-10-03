using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove the TcmUri if added to a tridion tempalte name.
        /// for development purposes, tempalte could be prefixed with the tcmuri.
        /// </summary>
        /// <param name="templateTitle"></param>
        /// <returns></returns>
        public static string StripTcmUriFromTempalteTitle(this string templateTitle)
        {

            Regex re = new Regex(@"tcm:(\d+)-(\d+)-?(\d*)-?v?(\d*)");

            Match m = re.Match(templateTitle);
            if (m.Success)
            {
                return templateTitle.Replace(m.Value, string.Empty);
            }

            return templateTitle;
        }
    }
}
