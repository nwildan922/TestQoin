using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Model;

namespace Task2
{
    public class MqModel
    {
        public string Command { get; set; }
        public Test01 Data { get; set; }
    }
}
