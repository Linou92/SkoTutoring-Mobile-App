using AppDbContext.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace StudentApi.Results
{
    
    public class Message
    {
        public string Type { get; set; }
        public string Content { get; set; }

        public Message(string content, MessageType type = MessageType.Error)
        {
            Type = type.ToString();
            Content = content;
        }
    }

    public class StateModel : Result
    {
        public string Status { get; set; }
        public string Messages { get; set; }

        public StateModel()
        {

        }
        public StateModel(string Status, string Message)
        {
            this.Status = Status;
            this.Messages = Message;
        }
    }


    /// <summary>
    /// Base Result Class
    /// </summary>
    public class Result
    {
        [DefaultValue(true)]
        public bool IsOk { get; set; } = true;
        public Message Message { get; set; }
    }

    /// <summary>
    /// Base List Result Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListResult<T> : Result
    {
        public ListResult()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }
    }

    public class TResult<T> : Result where T : class,new()
    {
        public T Item { get; set; }
    }

}