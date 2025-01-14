using UnityEngine;

public class Carfix : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
