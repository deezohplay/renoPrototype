using UnityEngine;

public class CityManager : MonoBehaviour
{
    public GameObject[] housePrefabs;
    private Vector2 housePosition;
    public CityManager Instance{set;get;}
    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        housePosition = housePrefabs[0].transform.position;
        Instantiate(housePrefabs[0], housePosition, Quaternion.identity);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
