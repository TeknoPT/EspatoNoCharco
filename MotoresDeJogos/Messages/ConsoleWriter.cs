using System;
using System.Collections.Generic;

namespace MotoresDeJogos
{
    class ConsoleWriter
    {
        List<Message> messages;
        ConsoleMessage consoleMessage;

        public void Update()
        {
            messages = MessageBus.GetMessagesOfType(MessageType.Console);
            foreach(Message message in messages)
            {
                consoleMessage = (ConsoleMessage) message;
                Console.WriteLine(consoleMessage.Message);
            }
        }
    }
}
