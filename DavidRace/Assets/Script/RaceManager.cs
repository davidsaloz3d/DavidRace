using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public GameObject Cp;
    public GameObject CpHolder;

    public GameObject[] Coches;
    public Transform[] CpPosition;
    
    public GameObject[] CpPaCadaCoche;

    private int CochesTotales;
    private int ChekpointTotales;

    public Text PosicionTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CochesTotales = Coches.Length;
        ChekpointTotales = CpHolder.transform.childCount;

        SetCheckpoint();
        setCarPostion();
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

    void setCarPostion(){
        for(int i = 0; i < CochesTotales; i++){
            Coches[i].GetComponent<Car_CP_Manager>().CarPosition = i + 1;
            Coches[i].GetComponent<Car_CP_Manager>().NumeroCoche = i;
        }

        PosicionTxt.text = "POS: " + Coches[0].GetComponent<Car_CP_Manager>().CarPosition + "/" + CochesTotales;
    }

    public void CocheCollectCP(int carNumber, int CpNumber){
        CpPaCadaCoche[carNumber].transform.position = CpPosition[CpNumber].transform.position;
        CpPaCadaCoche[carNumber].transform.rotation = CpPosition[CpNumber].transform.rotation;

        comparePosition(carNumber);
    }

    void comparePosition(int carNumber){
        if(Coches[carNumber].GetComponent<Car_CP_Manager>().CarPosition > 1){
            GameObject currentCar = Coches[carNumber];
            int currentCarPosition = currentCar.GetComponent<Car_CP_Manager>().CarPosition;
            int currentCarCP = currentCar.GetComponent<Car_CP_Manager>().cpCrossed;

            GameObject carInFront = null;
            int carInFrontPos = 0;
            int carInFrontCP = 0;

            for(int i = 0; i < CochesTotales; i++){
                if(Coches[i].GetComponent<Car_CP_Manager>().CarPosition == currentCarPosition-1){
                    carInFront = Coches[i];
                    carInFrontCP = carInFront.GetComponent<Car_CP_Manager>().cpCrossed;
                    carInFrontPos = carInFront.GetComponent<Car_CP_Manager>().CarPosition;
                    break;
                }
            }

            if(currentCarCP > carInFrontCP){
                currentCar.GetComponent<Car_CP_Manager>().CarPosition = currentCarPosition - 1;
                carInFront.GetComponent<Car_CP_Manager>().CarPosition = carInFrontPos + 1;
            }

        PosicionTxt.text = "POS: " + Coches[0].GetComponent<Car_CP_Manager>().CarPosition + "/" + CochesTotales;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
