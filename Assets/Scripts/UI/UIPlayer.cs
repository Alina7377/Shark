using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private Text _outScore;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private GameObject _dethDisplay;
    [SerializeField] private Text _outDethScore;
    [SerializeField] private Text _outRecordScore;
    [SerializeField] private GameObject _messaegePanel;

    private List<GameObject> _healthList = new List<GameObject>(); 

    public void CreateHelthIcon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _healthList.Add(Instantiate(_healthPrefab,_container));
        }
    }

    public void UpdateScore(int score) 
    {
        _outScore.text = score.ToString();
    }

    public void UpdateHealth(int count) 
    {
        for (int i = 1; i <= _healthList.Count; i++)
        {
            if (i <= count)
                _healthList[i - 1].SetActive(true);
            else
                _healthList[i - 1].SetActive(false);
        }
    }

    public void GameOver(int score) 
    {
        _dethDisplay.SetActive(true);
        _outDethScore.text = score.ToString();
        _outRecordScore.text = SafeData.Instance.RecordScore.ToString();
    }

    public void ShowMessage() 
    {
        _messaegePanel.SetActive(true);
        GameMode.Instance.StopGame();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume() 
    {
        // Зпускаем игру снова, если игра не закончена
        if (!_dethDisplay.activeSelf)
            GameMode.Instance.ContinueGame();
        _messaegePanel.SetActive(false);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(1);
    }

}
