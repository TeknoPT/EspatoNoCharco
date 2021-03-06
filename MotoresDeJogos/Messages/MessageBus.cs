﻿using System.Collections.Generic;

namespace MotoresDeJogos
{
    static class MessageBus
    {
        static List<Message> messages;
        static List<Message> tempMessageList;

        public static void Initialize()
        {
            messages = new List<Message>(100);
            tempMessageList = new List<Message>(100);
        }

        public static List<Message> GetMessagesOfType(MessageType messageType)
        {
            tempMessageList.Clear();
            foreach(Message message in messages)
            {
                if(message.MessageType == messageType)
                {
                    message.Read = true;
                    tempMessageList.Add(message);
                }
            }

            return tempMessageList;
        }

        public static void InsertNewMessage(Message message)
        {
            messages.Add(message);
        }

        public static void Update()
        {
            tempMessageList.Clear();
            foreach(Message message in messages)
            {
                if (message.Read)
                {
                    tempMessageList.Add(message);
                }
            }
            foreach(Message message in tempMessageList)
            {
                messages.Remove(message);
            }
        }
    }
}
