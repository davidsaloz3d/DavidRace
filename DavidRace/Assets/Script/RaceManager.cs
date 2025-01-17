using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public GameObject[] cars;          // Array de coches en la carrera
    public Transform[] checkpoints;   // Array de checkpoints en el circuito
    [SerializeField] TMP_Text positionText, lapsText;         // Texto para mostrar la posición del jugador

    [SerializeField] GameObject FinDeCarrera;

    private int vueltasCoche;

    public int totalLaps = 3;         // Número total de vueltas en la carrera
    public int totalCheckpoints;      // Número total de checkpoints en el circuito

    private int[] lapsCompleted;      // Vueltas completadas por cada coche
    private int[] currentCheckpoints; // Último checkpoint cruzado por cada coche
    private float[] lastCheckpointTime;
    void Start()
    {
        totalCheckpoints = checkpoints.Length;


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

        int posInicial;
        // Establecer la posición inicial del coche según el identificador (invertido)
        posInicial = cars[0].GetComponent<CarManager>().position;  // Asigna la posición invertida
        
    positionText.text = "POS: " + (posInicial + 1) + "/" + cars.Length;
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
            if (carManager.carNumber == 0) // Coche del jugador
            {
                positionText.text = "POS: " + (i + 1) + "/" + cars.Length;
            }
        }
    }

    void LapsRealizadas(){
        vueltasCoche = cars[0].GetComponent<CarManager>().lapsCompleted;

        Debug.Log ( "Coche Rojo:" + vueltasCoche);

        if(vueltasCoche < 1){
            lapsText.text = "Lap: " + (vueltasCoche + 1) + "/" + 3 ;
        }else{
            FinDeCarrera.SetActive(true);
            for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        Invoke("Creditos", 3);

        }
        
    }

    void Creditos(){
        SceneManager.LoadScene("Creditos");
    }


    void Update()
    {
        UpdatePositions();
        LapsRealizadas();
    }
}

