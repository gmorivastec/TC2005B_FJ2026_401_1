// we are going to be using c#
// .NET

// using namespaces
using UnityEngine;

public class Motion : MonoBehaviour
{

    private InputSystem_Actions inputActions;

    // how to add some external parameters
    [SerializeField]
    private float _speed = 5;

    // we are going to be using the monobehaviour lifecycle
    // lifecycle?
    // we cannot control the execution flow, we inject logic on specific points
    
    // lifecycle
    // a set of methods that are invoked autonomously by the engine in a particular point
    // in the life of an entity

    // first method to be invoked when a GO is created
    void Awake() 
    {
        print("AWAKE WORLD");
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    // happens after awake, only if component is enabled
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // static method 
        Debug.Log("START");
    }

    // how the engine works
    // frame - time window in which we run game logic and render graphics
    // fps

    // try to keep it at 60+
    // 30 is ok
    // less is not great

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("UPDATECITO");

        // we will try to keep here only:
        // 1 input
        // 2 motion

        // let's check for a button press!
        if(inputActions.Player.Jump.triggered)
        {
            // the triggered property will be true
            // ONLY if the previous frame Jump was not activated
            // and it is activated in this frame
            print("***** JUMP WAS TRIGGERED");
        }

        // let's capture a value
        Vector2 motion = inputActions.Player.Move.ReadValue<Vector2>();
        // print(motion);

        // the values we retrieve are in a range [-1, 1]

        // transform (LOWER CASE t) - 
        // - is an object that we inherit from MonoBehaviour
        // - it already contains a reference to the tranform component in 
        // also part of our component's owner

        // transform is in charge of spatial operations (transformations)
        // translate, rotate, scale (skew not here but exists)

        // Time.deltaTime - a variable in which unity saves how much time has passed
        // between the last frame and the current one
        transform.Translate(motion * Time.deltaTime * _speed);
    }

    // this method is invoked every loop after all updates are done
    void LateUpdate()
    {
        //Debug.Log("LATE UPDATE");
    }

    // this is an update that will be invoked on a fixed timestep
    // (in an interval that tries to be regular)
    void FixedUpdate()
    {
        //Debug.Log("FIXED UPDATE");
    }

    // Collision requirements:
    // 1 all of the objects involved have colliders
    // 2 at least one object has a rigidbody
    // 3 the object with the rigidbody is moving
}
