using UnityEngine;

public class EventManager : MonoBehaviour
{
    // how to declare a method that can be used by an event
    // 2 possibilities:
    // - static values
    // - dynamic values

    // static values - an event sends a predefined value to the listeners
    // if you have no arguments no value is sent :D

    public void EventListenerNoArgs()
    {
        print("NO ARGS HERE!");
    }

    public void EventListenerOneArg(string value)
    {
        print("ONE ARG: " + value);
    }

    public void EventListenerThreeArgs(float x, float y, float z)
    {
        print("THREE ARGS: " + x + ", " + y + ", " + z);
    }
}
