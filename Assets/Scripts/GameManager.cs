using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ set; get; }
    void Awake()
    {
        Instance = this;
    }
    public bool isGameStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        isGameStarted = true;
    }
}
