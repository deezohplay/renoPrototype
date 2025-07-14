using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;
    [Range(0f, 1f)] public float highlightAlpha = 1f;

    [Header("UI")]
    public GameObject popupButtonPrefab;
    public Canvas canvas;

    [HideInInspector]
    public GameObject selectedObject;

    private SpriteRenderer previousSpriteRenderer;
    private Color previousOriginalColor;
    private GameObject currentPopup;
    private ConstructionManager constructionManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        constructionManager = ConstructionManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPoint);

            if (hitCollider != null)
            {
                SelectObject(hitCollider.gameObject, worldPoint);
            }
            else
            {
                ClearSelection();
            }
        }
    }

    public void SelectObject(GameObject obj, Vector2 worldPoint)
    {
        if (previousSpriteRenderer != null)
        {
            previousSpriteRenderer.color = previousOriginalColor;
            previousSpriteRenderer = null;
        }

        selectedObject = obj;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            previousSpriteRenderer = sr;
            previousOriginalColor = sr.color;

            Color newColor = highlightColor;
            newColor.a = highlightAlpha;
            sr.color = newColor;
        }

        ShowPopupButton(worldPoint);
    }

    private void ShowPopupButton(Vector2 worldPoint)
    {
        ClearPopup();

        int coinsToSpend = constructionManager.CoinsAmount();
        Debug.Log("Coins required for renovation: " + coinsToSpend);

        if (coinsToSpend == 0)
        {
            Debug.Log("No renovation cost found for this object.");
            return;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPoint);

        currentPopup = Instantiate(popupButtonPrefab, canvas.transform);
        RectTransform rt = currentPopup.GetComponent<RectTransform>();
        rt.position = screenPos;

        TextMeshProUGUI txt = currentPopup.GetComponentInChildren<TextMeshProUGUI>();
        if (txt != null)
        {
            txt.text = "$"+ coinsToSpend.ToString();
        }

        Button btn = currentPopup.GetComponent<Button>();
        if (btn == null)
        {
            btn = currentPopup.GetComponentInChildren<Button>();
        }

        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                Debug.Log("Popup button clicked!");
                constructionManager.AttemptRenovation();
                ClearSelection();
            });
        }
        else
        {
            Debug.LogError("Popup prefab missing Button component!");
        }
    }

    private void ClearPopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
    }

    public void ClearSelection()
    {
        if (previousSpriteRenderer != null)
        {
            previousSpriteRenderer.color = previousOriginalColor;
            previousSpriteRenderer = null;
        }
        selectedObject = null;
        ClearPopup();
    }
}
