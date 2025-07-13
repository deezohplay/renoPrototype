using UnityEngine;
using TMPro;

public class ConstructionManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coins;
    public GameObject[] oldHousePrefabs;
    public GameObject[] renoLevelOnePrefabs;
    public static ConstructionManager Instance { get; private set; }
    private SelectionManager selectionManager;
    private CityManager cityManager;
    public GameObject floor_repair;
    private int confirmedCoin;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coins = 180;
        selectionManager = FindObjectOfType<SelectionManager>();
       //UpdateWallet();
    }

    void Update()
    {
        //runtime program here
        UpdateWallet();
        Construct(confirmedCoin);
    }

    private int CoinsAmount()
    {
        int coinsToSpend = 0;

        if (selectionManager != null && selectionManager.selectedObject != null)
        {
            string buildingSection = selectionManager.selectedObject.name;

            if (buildingSection == "floor_old")
            {
                coinsToSpend = 100;
            }
            else if (buildingSection == "wall_old")
            {
                coinsToSpend = 45;
            }
            else if (buildingSection == "pole_old")
            {
                coinsToSpend = 70;
            }
            else if (buildingSection == "window_old")
            {
                coinsToSpend = 35;
            }
        }

        return coinsToSpend;
    }

    private void Construct(int coinsToSpend)
    {
        if (coinsToSpend > coins)
        {
            //Not enough money in the wallet
        }
        else
        {
            if (selectionManager.selectedObject != null)
            {
                string buildingSection = selectionManager.selectedObject.name;
                // actions on selected section
                if (buildingSection == "floor_old")
                {
                    /*
                    coins-=coinsToSpend;
                    Vector2 sectionPos = renoLevelOnePrefabs[0].transform.position;
                    Instantiate(renoLevelOnePrefabs[0], sectionPos, Quaternion.identity);
                    renoLevelOnePrefabs[0].SetActive(true);
                    */
                    floor_repair.SetActive(true);
                    Debug.Log("show me");
                }
                else if (buildingSection == "wall_old")
                {
                  
                }
                else if (buildingSection == "pole_old")
                {
                    
                }
                else if (buildingSection == "window_old")
                {
                   
                }
                selectionManager.selectedObject = null;
            }
            coins -= coinsToSpend;
            confirmedCoin = coinsToSpend;
            UpdateWallet();
        }
    }
    /*
    private void Renovate(GameObject oldSection, string newSectionName)
    {
        Vector3 position = oldSection.transform.position;

        GameObject newSections = Resources.Load<GameObject>(newSectionName);
        if (newSections != null)
        {
            Instantiate(newSections, position, Quaternion.identity);
            Destroy(oldSection);
        }
        else
        {
            //not sure quite
        }
    }
    */
    private void UpdateWallet()
    {
        if (coinText != null)
        {
            coinText.text = "$ " + coins.ToString();
        }
    }
}
