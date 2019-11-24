using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Enumaration
    {
        public enum MessageCode
        {
            Success_Add = 1,
            Success_Update = 2,
            Success_Delete = 3,
            Failure_Add = 4,
            Failure_Update =5,
            Failure_Delete = 6,
            Unknown =7,
            Custom=8,

        };

        public enum MessageType
        {
            Insert = 1,
            Update = 2,
            Error = 3,
            Delete=4,
            Message=5,
        };
        public enum SortDirection
        {
            asc = 1,
            desc = 2,
        };

        public enum FormOperation
        { 
            Add=1,
            Edit=2,
            Find=3,
            Delete=4,
        }

        public enum QueryType
        {
            Equals = 0,
            Like = 1
        }
    }
}
