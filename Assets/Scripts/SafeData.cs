using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeData : MonoBehaviour
{

    private bool _isActiveSound = true;
    private int _recordScore = 0;
    public static SafeData Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int RecordScore 
    {
        get { return _recordScore; }
        set { _recordScore = value; }
    }

    public bool IsActiveSound 
    {
        get { return _isActiveSound; }
        set { _isActiveSound = value; }
    }
}
