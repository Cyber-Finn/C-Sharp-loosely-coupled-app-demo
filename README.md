# C# loosely-coupled app demo
This is a demo of how one could use Delegate events to create a very loosely coupled, and easily extendable, application.
<br>
The scenario I had in mind when creating this was: a simple video/audio encoding app, which would then notify listeners that the video was encoded once done, and sent out as messages. The underlying concept, however, can be used for most other use-cases as well.

# Technical overview:
We have a very basic class, which does something (in this case, it "encodes a video"), it then alerts the app that the activity has completed and fires an event. This event's listeners then notice that the event has happened, and they each independently perform their own operations - like "sending messages over whatsapp" and "sending messages over Steam" (They dont actually do this, it's just a mock-up scenario to demo the potential of decoupling code)
