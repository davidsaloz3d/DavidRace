using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public GameObject[] cars;          // Array de coches en la carrera
    public Transform[] checkpoints;   // Posiciones de los checkpoints
    public Text positionText;         // Texto para mostrar la posición del jugador

    public int totalCheckpoints;      // Número total de checkpoints en el circuito
    public int totalLaps = 3;         // Número total de vueltas

    private int[] lapsCompleted;      // Vueltas completadas por cada coche
    private int[] currentCheckpoints; // Último checkpoint cruzado por cada coche
    private float[] progress;         // Progreso acumulativo en la pista para cada coche

    void Start()
    {
        totalCheckpoints = checkpoints.Length;

        lapsCompleted = new int[cars.Length];
        currentCheckpoints = new int[cars.Length];
        progress = new float[cars.Length];

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

        UpdatePositions();
    }

    void UpdatePositions()
    {
        // Calcular progreso acumulativo para cada coche
        for (int i = 0; i < cars.Length; i++)
        {
            progress[i] = CalculateProgress(i);
        }

        // Ordenar coches por vueltas, checkpoints y progreso
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

            // 3. Comparar por progreso acumulativo
            return progress[indexB].CompareTo(progress[indexA]);
        });

        // Actualizar posiciones y texto
        for (int i = 0; i < cars.Length; i++)
        {
            var carManager = cars[i].GetComponent<CarManager>();
            carManager.carNumber = i + 1;

            if (carManager.carNumber == 1) // Mostrar posición del jugador (coche 0)
            {
                positionText.text = "POS: " + (i + 1) + "/" + cars.Length;
            }
        }
    }

    float CalculateProgress(int carIndex)
    {
        // Progreso acumulado entre checkpoints
        float progress = 0f;
        for (int i = 0; i < currentCheckpoints[carIndex]; i++)
        {
            int nextIndex = (i + 1) % totalCheckpoints;
            progress += Vector3.Distance(checkpoints[i].position, checkpoints[nextIndex].position);
        }

        // Añadir distancia al siguiente checkpoint
        int nextCheckpoint = (currentCheckpoints[carIndex] + 1) % totalCheckpoints;
        progress += Vector3.Distance(cars[carIndex].transform.position, checkpoints[nextCheckpoint].position);

        return progress;
    }
}

