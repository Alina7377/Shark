using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _couintPartLevel;
    [SerializeField] private float _verticalOffsetPart;
    [SerializeField] private float _speedVertical;
    [SerializeField] private List<GameObject> _partPrefabs;
    [SerializeField] private GameObject _startPartPref;
    [SerializeField] private TraningUI _traningUI;

    private float _destroyPoint = -12f;
    private List<GameObject> _parts = new List<GameObject>();
    private bool _isMove = true;

    private void Start()
    {
        CreateLevel();
        GameMode.Instance.GamePause += Pause;
        GameMode.Instance.GameResume += Resume;
        if (SafeData.Instance.RecordScore == 0)
        {
            GameMode.Instance.StopGame();
            _traningUI.StartTraning();
        }
    }

    private void OnDisable()
    {
        GameMode.Instance.GamePause -= Pause;
        GameMode.Instance.GameResume -= Resume;
    }

    private void Pause()
    {
        _isMove = false;
    }

    private void Resume() 
    {
        _isMove = true;
    }

    private void Update()
    {
        if (_isMove)
            MoveParts();
    }

    private void MoveParts()
    {
        Vector2 movePosition = Vector2.zero;
        foreach (var part in _parts)
        {
            movePosition = part.transform.position;
            movePosition.y -= _speedVertical * Time.deltaTime;
            part.transform.position = movePosition;            
        }
        if (_parts[0].transform.position.y < _destroyPoint)
            DestroyAndCreate();
    }


    /// <summary>
    /// Удаление не нужной части и запуск создания новой
    /// </summary>
    private void DestroyAndCreate()
    {
        Destroy(_parts[0].gameObject);
        _parts.RemoveAt(0);
        Vector2 creatingPosition = transform.position;
        creatingPosition.y = transform.position.y + (_verticalOffsetPart * (_couintPartLevel-1));
        CreatePart(creatingPosition);
    }

    /// <summary>
    /// Создание первой, всегда одинаковой части
    /// Это для того, чтобы игрок успел освоится
    /// </summary>
    private void CreateFirstPart() 
    {
        _parts.Add(Instantiate(_startPartPref, transform.position, Quaternion.identity));
    }

    /// <summary>
    /// Создание новой части уровня
    /// </summary>
    /// <param name="spawnPosition"></param>
    private void CreatePart(Vector2 spawnPosition)
    {
        int j = Random.Range(0, _partPrefabs.Count);
        _parts.Add(Instantiate(_partPrefabs[j], spawnPosition, Quaternion.identity));
    }


    /// <summary>
    /// Стартовыая генерация уровня
    /// </summary>
    private void CreateLevel() 
    {
        Vector2 creatingPosition = transform.position;
        CreateFirstPart();
        for (int i = 1; i < _couintPartLevel; i++)
        {
            creatingPosition.y = transform.position.y + (_verticalOffsetPart * i);
            CreatePart(creatingPosition);
        }
    }
        
}
