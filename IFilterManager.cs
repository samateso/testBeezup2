using BeezupAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeezupAPI
{
    public interface IFilterManager
    {
        List<Values> step2(string path);
        string step3(string type, string path, int? take, int? skip);
    }
}
