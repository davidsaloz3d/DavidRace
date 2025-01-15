using UnityEngine;

public class Retrovisor : MonoBehaviour
{
   [SerializeField] GameObject retrovisor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            retrovisor.SetActive(true);
        }

        if(Input.GetKeyUp(KeyCode.R)){
            retrovisor.SetActive(false);
        }
    }
}

