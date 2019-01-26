using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramData.Core
{
    public static class CSVExtention
    {
        public static string ReplaceCSVRule(this String comment)
        {
            return comment?.Replace(System.Environment.NewLine, "").Replace("\"", "").Replace(",", "");
        }
    }

}
