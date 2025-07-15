using UnityEngine;
using TMPro;

public class ConstructionManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 500;
    public GameObject[] oldHousePrefabs;
    public GameObject[] renoLevelOnePrefabs;
    public static ConstructionManager Instance { get; private set; }
    private SelectionManager selectionManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selectionManager = FindObjectOfType<SelectionManager>();
        UpdateWallet();
    }
    void Update()
    {
        UpdateWallet();
    }

    public int CoinsAmount()
    {
        int coinsToSpend = 0;

        if (selectionManager != null && selectionManager.selectedObject != null)
        {
            string buildingSection = selectionManager.selectedObject.name;

            if (buildingSection == "floor_old")
                coinsToSpend = 100;
            else if (buildingSection == "wall_old")
                coinsToSpend = 45;
            else if (buildingSection == "pole_old")
                coinsToSpend = 70;
            else if (buildingSection == "window_old")
                coinsToSpend = 35;
        }
        return coinsToSpend;
    }

    public void AttemptRenovation()
    {
        if (selectionManager.selectedObject == null)
        {
            return;
        }

        int coinsToSpend = CoinsAmount();

        if (coinsToSpend == 0)
        {
            return;
        }

        if (coinsToSpend > coins)
        {
            return;
        }

        Construct(coinsToSpend);
    }

    private void Construct(int coinsToSpend)
    {
        GameObject selected = selectionManager.selectedObject;
        string buildingSection = selected.name;
        int prefabIndex = -1;

        if (buildingSection == "floor_old")
            prefabIndex = 0;
        else if (buildingSection == "wall_old")
            prefabIndex = 1;
        else if (buildingSection == "pole_old")
            prefabIndex = 2;
        else if (buildingSection == "window_old")
            prefabIndex = 3;

        if (prefabIndex >= 0 && selectionManager.selectedObject != null)
        {
            coins -= coinsToSpend;

            Vector2 sectionPos = selected.transform.position;

            GameObject newPrefab = Instantiate(renoLevelOnePrefabs[prefabIndex],sectionPos,Quaternion.identity);
            newPrefab.SetActive(true);
            Destroy(selected);
        }

        selectionManager.ClearSelection();
    }

    private void UpdateWallet()
    {
        if (coinText != null)
        {
            coinText.text = "$" + coins.ToString();
        }
    }
}
