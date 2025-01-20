using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] GameObject carRed;

    [SerializeField] GameObject carBlue;

    [SerializeField] GameObject PanelSeleccion;

    [SerializeField] GameObject PanelMenu;

    public static bool CocheRojoSeleccionado = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Carrera(){
        SceneManager.LoadScene("SampleScene");
    }

    public void ElegirCoche(){
        PanelMenu.SetActive(false);
        PanelSeleccion.SetActive(true);
    }

    public void Salir(){
        Application.Quit();
    }

    public void cambioCar(){
        if(CocheRojoSeleccionado){
            carRed.SetActive(false);
            carBlue.SetActive(true);
            CocheRojoSeleccionado = false;
        }else{
            carRed.SetActive(true);
            carBlue.SetActive(false);
            CocheRojoSeleccionado = true;
        }
    }

    public void Volver(){
        PanelMenu.SetActive(true);
        PanelSeleccion.SetActive(false);
    }

}
