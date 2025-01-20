using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    [SerializeField] GameObject menuPausa;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume(){
        menu.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1;
    }

    public void Salir(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        
    }

    public void Reiniciar(){
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
        
    }
}
