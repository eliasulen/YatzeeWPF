using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzeeWPF.Models
{
    class HighScoreException : Exception
    {
        public HighScoreException(string m) : base(m)
        {

        }
    }
}
