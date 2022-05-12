using System;
using System.Collections.Generic;

#nullable disable

namespace ServerApp_v0._1.Data.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public string Requester { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsProcessed { get; set; }
        public string TableAffected { get; set; }
        public bool? WillDelete { get; set; }
        public string Arguments { get; set; }
        public override string ToString() => $"Request №{RequestId}. By {RequesterToString()}. At {Timestamp.ToString()}";

        private string RequesterToString()
        {
            return "not implemented";
        }
    }
}
