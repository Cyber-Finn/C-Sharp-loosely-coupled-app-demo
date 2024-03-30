using System.ComponentModel.DataAnnotations;

//file-scoped namespace
namespace DelegateEventsTest;
    internal class Program
    {
        static void Main(string[] args)
        {
            //create new instances of the classes
            MessageEncoding newEncoding = new MessageEncoding();
            SendEmail sendEmail = new SendEmail();
            SendWhatsapp sw = new SendWhatsapp();
            NotifySent nfs = new NotifySent();
            SendSteamMessage steamMessage = new SendSteamMessage();

            //load up the subscriber methods to the publisher method (subscriber method's input equals the Event's)
            newEncoding.MessageEncodedEvent += sendEmail.subscriber;
            newEncoding.MessageEncodedEvent += sw.subscriber;
            newEncoding.MessageEncodedEvent += steamMessage.subscriber;

            //making it more extensible and less-coupled by having a "message" function that all classes share
            //  this is nice, because we dont explicitly call a method anywhere, we just fire the event
            sw.messageSent += nfs.subscriber;
            sendEmail.messageSent += nfs.subscriber;
            steamMessage.messageSent += nfs.subscriber;

            //now we need to actually exec the method - or else it won't work
            newEncoding.messageEncode();

            //unsubscribe from the events - this helps to ensure there are no memory leaks or piles of objects in memory, etc.
            newEncoding.MessageEncodedEvent -= sendEmail.subscriber;
            newEncoding.MessageEncodedEvent -= sw.subscriber;
            newEncoding.MessageEncodedEvent -= steamMessage.subscriber;
            sw.messageSent += nfs.subscriber;
            sendEmail.messageSent += nfs.subscriber;
            steamMessage.messageSent += nfs.subscriber;
        }
    }

    class SendEmail
    {
        public delegate void messageSentEventHandler(string s);
        public event messageSentEventHandler? messageSent;

        public void onMessageSent()
        {
            if (messageSent != null)
            {
                messageSent("Email Sent");
            }
        }

        public void subscriber(object obj, EventArgs e)
        {
            Console.WriteLine("Sending email..");
            Thread.Sleep(100);
            onMessageSent();
        }
    }

    class SendSteamMessage
    {
        public delegate void messageSentEventHandler(string s);
        public event messageSentEventHandler? messageSent;

        public void subscriber(object obj, EventArgs e)
        {
            Console.WriteLine("Sending message to Steam account");

            //clever way of getting data from the source object
            Console.WriteLine("Stephen test: " + ((MessageEncoding)obj).messageEncodingObjectDataString);
            Thread.Sleep(100);
            onMessageSent();
        }
        public void onMessageSent()
        {
            if (messageSent != null)
            {
                messageSent("Message Sent");
            }
        }
    }
    class SendWhatsapp
    {
        public delegate void messageSentEventHandler(string s);
        public event messageSentEventHandler? messageSent;

        public void onMessageSent()
        {
            if (messageSent != null)
            {
                messageSent("Whatsapp Sent");
            }
        }
        public void subscriber(object obj, EventArgs e)
        {
            Console.WriteLine("Sending WhatsApp message..");
            Thread.Sleep(100);
            onMessageSent();
        }
    }

    class NotifySent
    {
        public void subscriber(string s)
        {
            Thread.Sleep(100);
            Console.WriteLine(s);
        }
    }
