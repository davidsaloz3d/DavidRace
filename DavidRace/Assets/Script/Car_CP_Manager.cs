using UnityEngine;

public class Car_CP_Manager : MonoBehaviour
{
    public int cpCrossed = 0;
    public int NumeroCoche;

    public int CarPosition;

    public RaceManager raceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("CP")){
            cpCrossed += 1;
            raceManager.CocheCollectCP(NumeroCoche,cpCrossed);
        }
    }
}
