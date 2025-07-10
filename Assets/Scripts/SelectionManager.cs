using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    void Start()
    {

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
                    }
                }
        #endif
        //mobile touch input to be implemented here...
    }

    
    
}
