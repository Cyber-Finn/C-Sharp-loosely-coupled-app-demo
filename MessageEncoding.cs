using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DelegateEventsTest.MessageEncoding;

//file-scoped namespace
namespace DelegateEventsTest;
internal class MessageEncoding
{
    //the actual event that gets fired - although, this isnt called directly, this is just a definition of the event
    //  the object of this type is what gets called and has stuff listen for it
    public delegate void MessageEncodedEventHandler(object obj, EventArgs e);
    public event MessageEncodedEventHandler? MessageEncodedEvent;
    public string messageEncodingObjectDataString = "";

    //do some stuff, then call the method that will raise the event
    public void messageEncode()
    {
        Console.WriteLine("Encoding the message for you..");
        Thread.Sleep(100);
        onMessageEncoded();
    }
    protected virtual void onMessageEncoded()
    {
        if(MessageEncodedEvent != null) 
        {
            //just loading up our object's string with some data (This is part of a reusability test/concept I was messing around with)
            messageEncodingObjectDataString = "hello world!";

            //raise the event (Basically just updating the event object, by passing in some data)
            MessageEncodedEvent(this, EventArgs.Empty);
        }
    }
}
