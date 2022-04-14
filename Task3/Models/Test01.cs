using System;

namespace Task3.Models
{
    public class Test01
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public short Status { get; set; }
    }
    public class MqModel
    {
        public string Command { get; set; }
        public Test01 Data { get; set; }
    }

}
