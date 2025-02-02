using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;
using Unity.VisualScripting;
using UnityEditor;

public class RaceManager : MonoBehaviour
{
    public GameObject[] cars;          // Array de coches en la carrera
    public Transform[] checkpoints;   // Array de checkpoints en el circuito
    [SerializeField] TMP_Text positionText, lapsText, tTime;         // Texto para mostrar la posición del jugador

    [SerializeField] GameObject FinDeCarrera;


    private int vueltasCoche;

    bool inicio = false;

    public int totalLaps = 3;         // Número total de vueltas en la carrera
    public int totalCheckpoints;      // Número total de checkpoints en el circuito

    private int[] lapsCompleted;      // Vueltas completadas por cada coche
    private int[] currentCheckpoints; // Último checkpoint cruzado por cada coche
    private float[] lastCheckpointTime;

    [SerializeField] float time = 4;

    [SerializeField] GameObject ChasisRojo;
    [SerializeField] GameObject ChasisAzul;
    [SerializeField] GameObject ChasisGris;

    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject menu;

    void Start()
    {
        if(MenuControl.CocheSeleccionado == 1){
            ChasisRojo.SetActive(true);
            ChasisAzul.SetActive(false);
            ChasisGris.SetActive(false);
        }else if(MenuControl.CocheSeleccionado == 2){
            ChasisRojo.SetActive(false);
            ChasisAzul.SetActive(true);
            ChasisGris.SetActive(false);
        }else if(MenuControl.CocheSeleccionado == 3){
            ChasisRojo.SetActive(false);
            ChasisAzul.SetActive(false);
            ChasisGris.SetActive(true);
        }
        totalCheckpoints = checkpoints.Length;

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<CarController>().Velocidad0();
        }

        // Inicializar los arrays para los coches
        lapsCompleted = new int[cars.Length];
        currentCheckpoints = new int[cars.Length];
        lastCheckpointTime = new float[cars.Length];

        // Configurar el CarManager de cada coche
        for (int i = 0; i < cars.Length; i++)
        {
            var carManager = cars[i].GetComponent<CarManager>();
            carManager.carNumber = i;
            carManager.raceManager = this;
        }


    }

    public void UpdateCarProgress(int carNumber, int laps, int checkpoint)
    {
        lapsCompleted[carNumber] = laps;
        currentCheckpoints[carNumber] = checkpoint;
        lastCheckpointTime[carNumber] = Time.time; // Actualizar el tiempo del último checkpoint

        UpdatePositions();
    }

    void UpdatePositions()
    {
        // Ordenar coches según la lógica de posición
        System.Array.Sort(cars, (a, b) =>
        {
            int indexA = a.GetComponent<CarManager>().carNumber;
            int indexB = b.GetComponent<CarManager>().carNumber;

            // 1. Comparar por vueltas completadas
            if (lapsCompleted[indexA] != lapsCompleted[indexB])
                return lapsCompleted[indexB].CompareTo(lapsCompleted[indexA]);

            // 2. Comparar por checkpoints cruzados
            if (currentCheckpoints[indexA] != currentCheckpoints[indexB])
                return currentCheckpoints[indexB].CompareTo(currentCheckpoints[indexA]);

            // 3. Comparar por tiempo desde el último checkpoint
            return lastCheckpointTime[indexA].CompareTo(lastCheckpointTime[indexB]);
        });

        // Actualizar posiciones y mostrar texto para el jugador (coche 0)
        for (int i = 0; i < cars.Length; i++)
        {
            var carManager = cars[i].GetComponent<CarManager>();
            if (carManager.carNumber == 3) // Coche del jugador
            {
                positionText.text = "POS: " + (i + 1) + "/" + cars.Length;
            }
        }
    }

    void LapsRealizadas()
    {
        

        for (int i = 0; i < cars.Length; i++)
        {
            var carLaps = cars[i].GetComponent<CarManager>();
            if (carLaps.carNumber == 3) // Coche del jugador
            {
                if (carLaps.lapsCompleted < 3)
                {
                    lapsText.text = "LAP: " + (carLaps.lapsCompleted + 1) + "/" + 3;
                }
                else
                {
                    FinDeCarrera.SetActive(true);
                    for (int j = 0; j < cars.Length; j++)
                    {
                        cars[j].SetActive(false);
                    }

                    Invoke("Creditos", 3);

                }
            }
        }



    }

    void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuPausa.SetActive(true);
                menu.SetActive(false);
                Time.timeScale = 0;
            }

        UpdatePositions();
        LapsRealizadas();

        if (!inicio)
        {
            time = time - Time.deltaTime;
            if (time < 1)
            {
                time = 0;


                tTime.text = "GO!";
                inicio = true;
                Invoke("Run", 1);

            }
            else
            {
                float sec;
                sec = Mathf.Floor(time % 60);
                tTime.text = sec.ToString("0");
            }


        }

    }

    void Run()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].GetComponent<CarController>().Velocidad100();
        }
        tTime.text = "";
    }
}

