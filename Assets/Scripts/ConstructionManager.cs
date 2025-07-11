using UnityEngine;
using TMPro;

public class ConstructionManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coins;
    public GameObject popUpBox;
    public static ConstructionManager Instance { get; private set; }
    private SelectionManager selectionManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coins = 180;
        selectionManager = FindObjectOfType<SelectionManager>();
        UpdateWallet();
    }

    void Update()
    {
        // Optional: test construction with a key press
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TryConstruct();
        // }
    }

    public void TryConstruct()
    {
        int cost = CoinsAmount();

        if (cost > 0)
        {
            Construct(cost);
        }
        else
        {
            Debug.Log("No valid object selected.");
        }
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
                    PopUpBox(selectionManager.selectedObject, "floor_new");
                    //Renovate(selectionManager.selectedObject, "floor_new");
                }
                else if (buildingSection == "wall_old")
                {
                    //Renovate(selectionManager.selectedObject, "wall_new");
                }
                else if (buildingSection == "pole_old")
                {
                    //Renovate(selectionManager.selectedObject, "pole_new");
                }
                else if (buildingSection == "window_old")
                {
                    //Renovate(selectionManager.selectedObject, "window_new");
                }

                selectionManager.selectedObject = null;
            }
            coins -= coinsToSpend;
            UpdateWallet();
        }
    }
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
    private void UpdateWallet()
    {
        if (coinText != null)
        {
            coinText.text = $" {coins}";
        }
    }

    private void PopUpBox(GameObject oldSection, string newSectionName)
    {
        Vector2 position = oldSection.transform.position;
        Vector2 offset = new Vector2(-0.5f, 0.5f);
        Vector2 spawnPos = position + offset;

        Instantiate(popUpBox, spawnPos, Quaternion.identity);
    }
}
