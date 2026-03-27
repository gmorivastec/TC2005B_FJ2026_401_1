using UnityEngine;

public class MotionBullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        // para destruir usamos el método destroy
        Destroy(gameObject);
    }
}
