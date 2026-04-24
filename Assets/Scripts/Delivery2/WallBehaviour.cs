using System;
using UnityEngine;
using UnityEngine.Events;

public class WallBehaviour : MonoBehaviour
{

    // unity events 
    // a mechanism that unity uses to inform that smoething happened 
    // based on the observer design pattern
    // https://en.wikipedia.org/wiki/Observer_pattern

    // if we want one more args on our events we need to declare a subclass of event
    [Serializable]
    public class OneArgEvent : UnityEvent<string> {}

    [Serializable]
    public class ThreeArgEvent : UnityEvent<float, float, float> {}

    [SerializeField]
    private UnityEvent _eventNoArgs;

    [SerializeField]
    private OneArgEvent _eventOneArg;

    [SerializeField]
    private ThreeArgEvent _eventThreeArgs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // this is how you inform something happened
        _eventNoArgs.Invoke();

        // with one arg
        _eventOneArg.Invoke(collision.transform.name);

        Vector3 collisionPoint = collision.contacts[0].point;
        // with three args
        _eventThreeArgs.Invoke(
            collisionPoint.x,
            collisionPoint.y,
            collisionPoint.z
        );
    }
}
