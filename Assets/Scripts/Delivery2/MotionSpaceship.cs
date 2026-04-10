using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MotionSpaceship : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private GameObject _original;

    // Coroutines
    // pseudoconcurrent code 
    // concurrency?

    // 2 main cases:
    // 1 - an activity that requires a wait for whatever reason
        // - wait for something to be finished
        // - wait for a condition to happen
    // 2 - loop - behavior that we want to repeat but shouldn't go on the update

    // more about coroutines:
    // - they don't behave like a thread
    // - coroutines belong to the component that started them
    // - a component can manage its own coroutines (start, stop)
    // - when destroying a component all its coroutines are destroyed

    // how to keep track of running coroutines
    private IEnumerator _loopEnumerator, _shootEnumerator;
    private Coroutine _loopCoroutine;

    void Awake() 
    {
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // coroutines are defined as methods but to actually run them 
        // you need to invoke StartCoroutine
        StartCoroutine(WaitExample());
        _loopEnumerator = LoopExample();
        _loopCoroutine = StartCoroutine(_loopEnumerator);

        _shootEnumerator = Shoot();

        //StartCoroutine("LoopExample");
        // third choice - start coroutine with string and object StartCoroutine("LoopExample", someParam)
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 motion = inputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(motion * Time.deltaTime * _speed, Space.World);

/*
        // i want to generate new bullets when pressing spacebar
        if(inputActions.Player.Jump.triggered)
        {
            print("PEW!");

            // to clone a gameobject we are using instantiate
            Instantiate(
                _original,
                transform.position,
                transform.rotation
            );
        }
*/
        if(inputActions.Player.Jump.WasPressedThisFrame())
        {
            StartCoroutine(_shootEnumerator);
        }

        if(inputActions.Player.Jump.WasReleasedThisFrame())
        {
            StopCoroutine(_shootEnumerator);
        }

        // how to stop corroutines
        if(inputActions.Player.Interact.triggered)
        {
            // tecla E
            // the most effective / easier
            StopAllCoroutines();
        }

        if(inputActions.Player.Crouch.triggered)
        {
            // tecla C
            // stop coroutines individually
            // several choices
            // using ienumerator
            StopCoroutine(_loopEnumerator);
            //StopCoroutine(_loopCoroutine);
            //StopCoroutine("LoopCoroutine");
        }
    }

    // to define a coroutine we are going to declare a method
    IEnumerator WaitExample()
    {
        // doing a non-blocking operation
        yield return new WaitForSeconds(1);
        print("HELLO (WAIT EXAMPLE)");
    }

    // coroutines have little overhead
    // advantage vs implementing logic in update:
    // - you can separate the logic from update
    // - you can actually have several loops
    // - you can have recurring code running in a lesser frequency than update
    IEnumerator LoopExample()
    {
        while(true)
        {
            print("COROUTINE LOOP (BEFORE WAIT)");
            yield return new WaitForSeconds(2);
            print("COROUTINE LOOP (AFTER WAIT)");
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // to clone a gameobject we are using instantiate
            /*
            Instantiate(
                _original,
                transform.position,
                transform.rotation
            );
            */

            // instead of instantiating we are using the queue now
            // one big advantage of a singleton - access speed

            // another way to get reference to an object
            // GameObject pool = GameObject.Find("PoolManager");

            PoolManager.Instance.LendObject(
                transform.position, 
                transform.rotation
            );

            // just an example about properties
            //PoolManager.Instance.ExampleVar = 1;
            //print(PoolManager.Instance.ExampleVar);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
