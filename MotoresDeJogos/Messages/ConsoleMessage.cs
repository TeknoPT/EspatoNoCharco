using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{
    class ConsoleMessage : Message
    {
        private String message;

        public String Message
        {
            get { return message; }
            set { message = value; }
        }

        public ConsoleMessage(String message) : base(MessageType.Console)
        {
            this.message = message;
        }
    }
}
