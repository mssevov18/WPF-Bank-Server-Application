using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ServerApp_v0._1.Data.Models
{
    public partial class Log
    {
        public Log(string Query, int LogType, string Requester, int RowsReturned, bool IsProcessed)
        {
            this.Query = Query; 
            this.LogType = LogType; 
            this.Requester = Requester; 
            this.RowsReturned = RowsReturned;
            this.IsProcessed = IsProcessed;
        }

        public int LogId { get; set; }
        public string Query { get; set; }

        // Null = 0,
        // GetData = 1,
        // AddData = 2,
        // ChangeData = 3,
        // RemoveData = 4,
        // AddLog = 5
        public int LogType { get; set; }
        public string Requester { get; set; }
        public int RowsReturned { get; set; }
        public bool IsProcessed { get; set; }
        public override string ToString() => $"Log №{LogId}({ToLogType(LogType)}) by {Requester}.";
        public string ToLogType(int number)
        {
            switch (number)
            {
                case 1:
                    return "GetData";
                case 2:
                    return "AddData";
                case 3:
                    return "ChangeData";
                case 4:
                    return "RemoveData";
                case 5:
                    return "AddLog";
                case 6:
                    return "ReadLog";
                case 0:
                    return "Null";
                default:
                    return "";
            }
        }
    }
}
