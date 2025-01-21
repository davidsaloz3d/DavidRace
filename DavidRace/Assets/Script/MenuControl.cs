using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] GameObject carRed;

    [SerializeField] GameObject carBlue;

    [SerializeField] GameObject carGrey;

    [SerializeField] GameObject PanelSeleccion;

    [SerializeField] GameObject PanelMenu;

    public static int CocheSeleccionado = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Carrera()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ElegirCoche()
    {
        PanelMenu.SetActive(false);
        PanelSeleccion.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void cambioCarLeft()
    {
        if (CocheSeleccionado == 1)
        {
            carRed.SetActive(false);
            carBlue.SetActive(false);
            carGrey.SetActive(true);
            CocheSeleccionado = 3;
        }
        else if (CocheSeleccionado == 2)
        {
            carRed.SetActive(true);
            carBlue.SetActive(false);
            carGrey.SetActive(false);
            CocheSeleccionado = 1;
        }
        else if (CocheSeleccionado == 3)
        {
            carRed.SetActive(false);
            carBlue.SetActive(true);
            carGrey.SetActive(false);
            CocheSeleccionado = 2;
        }
        Debug.Log(CocheSeleccionado);
    }

    public void cambioCarRight()
    {
        if (CocheSeleccionado == 1)
        {
            carRed.SetActive(false);
            carBlue.SetActive(true);
            carGrey.SetActive(false);
            CocheSeleccionado = 2;
        }
        else if (CocheSeleccionado == 2)
        {
            carRed.SetActive(false);
            carBlue.SetActive(false);
            carGrey.SetActive(true);
            CocheSeleccionado = 3;
        }
        else if (CocheSeleccionado == 3)
        {
            carRed.SetActive(true);
            carBlue.SetActive(false);
            carGrey.SetActive(false);
            CocheSeleccionado = 1;
        }
        Debug.Log(CocheSeleccionado);
    }

    public void Volver()
    {
        PanelMenu.SetActive(true);
        PanelSeleccion.SetActive(false);
    }

}
