using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveJunction.Shared.Services {
    public enum MessageTypeEnum {
        Information,
        Warning,
        Error
    }

    public class Message {
        public string Text { get; set; }
        public MessageTypeEnum Type { get; set; }
        public Message(string text, MessageTypeEnum type) {
            Text = text;
            Type = type;
        }
    }

    public interface IMessageLogger {
        public void AddMessage(Message message);
        public bool HasErrors();
        public bool HasErrorsOrWarnings();
        public void Clear();
        public void Merge(MessageLogger logger);
        public string Log();
    }


    public class MessageLogger : IMessageLogger {
        public List<Message> Messages { get; set; }
        public MessageLogger() {
            Messages = new List<Message>();
        }

        public void AddMessage(Message message) {
            Messages.Add(message);
        }

        public bool HasErrors() {
            return Messages.Exists(x => x.Type == MessageTypeEnum.Error);
        }

        public bool HasErrorsOrWarnings() {
            return Messages.Exists(x => x.Type == MessageTypeEnum.Error || x.Type == MessageTypeEnum.Warning);
        }

        public void Clear() {
            Messages.Clear();
        }

        public void Merge(MessageLogger logger) {
            Messages.AddRange(logger.Messages);
        }

        public string Log() {
            string res = String.Empty;
            foreach (Message message in Messages) {
                if (res != String.Empty) {
                    res = res + "\n";
                }
                res = res + message.Type + ":" + message.Text;
            }
            return res;
        }
    }


}
