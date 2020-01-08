using System;

namespace MotoresDeJogos
{

    class Message
    {

        private MessageType messageType;

        public MessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private Boolean read;

        public Boolean Read
        {
            get { return read; }
            set { read = value; }
        }

        public Message(MessageType messageType)
        {
            this.messageType = messageType;
            this.read = false;
        }
    }
}
