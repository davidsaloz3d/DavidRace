using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject Cp;
    public GameObject CpHolder;

    public GameObject[] Coches;
    public Transform[] CpPosition;
    
    public GameObject[] CpPaCadaCoche;

    private int CochesTotales;
    private int ChekpointTotales;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CochesTotales = Coches.Length;
        ChekpointTotales = CpHolder.transform.childCount;

        SetCheckpoint();
    }

    void SetCheckpoint(){
        CpPosition = new Transform[ChekpointTotales];

        for(int i = 0; i < ChekpointTotales; i++){
            CpPosition[i] = CpHolder.transform.GetChild(i).transform;
        }

        CpPaCadaCoche = new GameObject[CochesTotales];

        for(int i = 0; i < CochesTotales; i++){
            CpPaCadaCoche[i] = Instantiate(Cp, CpPosition[0].position, CpPosition[0].rotation);
            CpPaCadaCoche[i].name = "CP " + i;
            CpPaCadaCoche[i].layer = 6 + i;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
