using UnityEngine;

public class GameMode : MonoBehaviour
{
    public delegate void EndGame();
    public event EndGame GamePause;

    public delegate void ReturnGame();
    public event ReturnGame GameResume;

    public static GameMode Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // —читывем данные по звуку
        if (SafeData.Instance.IsActiveSound)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }
    

    public void StopGame() 
    {
        GamePause?.Invoke();
    }

    public void ContinueGame() 
    {
        GameResume?.Invoke();
    }
}
