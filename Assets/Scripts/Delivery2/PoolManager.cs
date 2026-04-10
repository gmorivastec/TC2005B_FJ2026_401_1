using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // Singleton 
    // design pattern

    // - limits the creation of instances to only 1 
    // we have to redefine how are we going to do our singleton
    // we are doing a corrective (instead of preventive) Singleton!

    // properties in C#
    // mechanism that works as a regulator to define who reads and who writes a variable

    // variables can be explicit or implicit

    // variable declared explicitely
    private float _exampleVar;

    public float ExampleVar
    {
        get
        {
            return _exampleVar;
        }
        private set
        {
            _exampleVar = value;
        }
    }

    // hidden variable
    public static PoolManager Instance
    {
        get;
        private set;
    }

    // Pool 
    // ??? 
    // (recursos comunes)

    // mechanism in which we have a central source of gameobjects
    // and we "lend" and "return" instead of creating and destroying
    private Queue<GameObject> _pool;

    [SerializeField]
    private GameObject _original;

    [SerializeField]
    private int _poolSize = 10;

    void Awake()
    {
        // check if i'm the only one
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // how to use set in a property
        // (syntactically identical to variable)
        ExampleVar = 5;
        print(ExampleVar);

        // initialize pool
        _pool = new Queue<GameObject>();
        
        // loop used to create the actual objects
        for(int i = 0; i < _poolSize; i ++)
        {
            GameObject newObject = Instantiate(_original);
            _pool.Enqueue(newObject);
            newObject.SetActive(false);
        }
    }

    // pool methods
    // - lend
    // - return

    // this method will substitute the creation logic
    public GameObject LendObject(Vector3 position, Quaternion rotation)
    {
        if(_pool.Count == 0)
            return null;
            
        GameObject go = _pool.Dequeue();
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.SetActive(true);

        return go;
        
    }

    public void ReturnObject(MotionBullet go)
    {
        _pool.Enqueue(go.gameObject);
        go.gameObject.SetActive(false);
    }
}
