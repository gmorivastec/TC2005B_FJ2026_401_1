using UnityEngine;

public class MotionSpaceship : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private GameObject _original;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 motion = inputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(motion * Time.deltaTime * _speed, Space.World);

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
    }
}
