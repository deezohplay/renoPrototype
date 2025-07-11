using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    
    public GameObject selectedObject;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hitCollider = Physics2D.OverlapPoint(worldPoint);

            if (hitCollider != null)
            {
                Debug.Log("Clicked on object: " + hitCollider.gameObject.name);
                
                selectedObject = hitCollider.gameObject;

                // Optional: highlight the selected object visually
                // e.g. enable an outline component
            }
            else
            {
                // Clicked empty space → clear selection
                selectedObject = null;
            }
        }
#endif

        // TODO: add mobile touch support here if needed
    }
}
