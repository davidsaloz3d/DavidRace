using UnityEngine;
using UnityEngine.SceneManagement;

public class Cretdits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            MenuControl.CocheSeleccionado = 1;
            SceneManager.LoadScene("Menu");
        }
    }
}
