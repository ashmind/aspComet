﻿using System.Collections.Generic;

namespace AspComet.MessageHandlers
{
    public class PassThruHandler : IMessageHandler
    {
        public string ChannelName { get; private set; }
        public IEnumerable<Client> Recipients { get; private set; }

        public bool ShouldWait
        {
            get { return false; }
        }

        public PassThruHandler(string channelName, IEnumerable<Client> recipients)
        {
            this.ChannelName = channelName;
            this.Recipients = recipients;
        }

        public Message HandleMessage(MessageBus source, Message request)
        {
            bool sendToSelf = false;

            Message forward = new Message
            {
                channel = this.ChannelName,
                data = request.data
                // TODO What other fields should be forwarded?
            };

            foreach (Client client in this.Recipients)
            {
                if (client.ID == request.clientId)
                {
                    sendToSelf = true;
                }
                else
                {
                    client.Enqueue(forward);
                    client.FlushQueue();
                }
            }

            return sendToSelf ? forward : null;
        }
    }
}
