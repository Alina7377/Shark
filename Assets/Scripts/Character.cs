using System;
using UnityEngine;

public class Character : MonoBehaviour, IGettingBuff
{
    [SerializeField] private float _speedHorizontalMove;
    [SerializeField] private int _maxHelth;
    [SerializeField] private UIPlayer _playerUI;

    private bool _isHooked;
    private Transform _target;
    private LineRenderer _lineRenderer;
    private int _currentHelth;
    private int _currentScore;
    private bool _isCanMove = true;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerUI.CreateHelthIcon(_maxHelth);
        _currentHelth = _maxHelth;
        _currentScore = 0;

        GameMode.Instance.GamePause += StopMove;
        GameMode.Instance.GameResume += ResumeMove;
    }

    private void OnDisable()
    {
        GameMode.Instance.GamePause -= StopMove;
        GameMode.Instance.GameResume -= ResumeMove;
    }

    private void ResumeMove()
    {
        _isCanMove = true;
    }

    private void StopMove()
    {
        _isCanMove= false;
    }

    private void Update()
    {
        if (!_isHooked || !_isCanMove) return;

        HorizontalMove();
        
    }

    private void HorizontalMove()
    {
        Vector2 newPosition = transform.position;

        if ( CanMove(newPosition.x))
            // ѕеремещение в право
            if (_target.position.x > newPosition.x)
                newPosition.x += _speedHorizontalMove * Time.deltaTime;
            else
            // ѕеремещение в лево
            if (_target.position.x < newPosition.x)
                newPosition.x -= _speedHorizontalMove * Time.deltaTime;

        if (_target.position.y <= 0)
        {
            StopHooked();
            return;
        }
        transform.position = newPosition;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _target.position);

    }

    /// <summary>
    /// ѕровер€ем, хватает ли нам шага, чтобы переместитьс€ к цели
    /// Ёто решает проблему с раскачиванием
    /// </summary>
    /// <param name="posX"></param>
    /// <returns></returns>
    private bool CanMove(float posX)
    {
        Vector2 targetX = Vector2.zero;
        Vector2 playerX = Vector2.zero;
        targetX.x = _target.position.x;
        playerX.x = posX;
        if (Vector2.Distance(targetX,playerX) > (_speedHorizontalMove * Time.deltaTime))
            return true;
        return false;
    }

    private void StartHooked(Transform target) 
    {
        _target = target;
        _isHooked = true;
    }

    private void StopHooked() 
    {
        _isHooked = false;
        _lineRenderer.SetPosition(0, Vector2.zero);
        _lineRenderer.SetPosition(1, Vector2.zero);
        _target = null;

    }

    private void Death()
    {
        int recordScore = SafeData.Instance.RecordScore;

        if (recordScore < _currentScore)
            SafeData.Instance.RecordScore = _currentScore;

        GameMode.Instance.StopGame();
        _playerUI.GameOver(_currentScore);
    }

    public void GetBuff(EBuffType type, int val)
    {
        switch (type)
        {
            case EBuffType.Damage:
                _currentHelth -= val;
                _playerUI.UpdateHealth(_currentHelth);
                if (_currentHelth <= 0)
                    Death();
                break;
            case EBuffType.Helth:
                _currentHelth += val;
                if (_currentHelth > _maxHelth)
                    _currentHelth = _maxHelth;
                else
                    _playerUI.UpdateHealth(_currentHelth);
                break;
            case EBuffType.Score:
                _currentScore += val;
                _playerUI.UpdateScore(_currentScore);
                break;
        }
    }

    public void NewTarget(Vector2 tapPoint)
    {
        if (_isHooked)
        {
            StopHooked();
            return;
        }
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(tapPoint);
        Vector2 origin = new Vector2(worldPoint.x, worldPoint.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit && hit.transform.tag == "Hooked")
        {
            StartHooked(hit.transform);
        }
        else
            StopHooked();
    }

}
