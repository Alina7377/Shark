using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _windowSetting;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _audioOn;
    [SerializeField] private Sprite _audioOff;
    private bool _isSoundActive = true;

    private void Start()
    {
        SetSoundValue(SafeData.Instance.IsActiveSound);
    }

    /// <summary>
    /// Используем глобальное управление звуком
    /// </summary>
    /// <param name="isActive"></param>
    private void SetSoundValue(bool isActive)
    {
        if(isActive)
            AudioListener.volume = 1.0f;
        else
            AudioListener.volume = 0.0f;
    }

    public void StartGame() 
    {
        SceneManager.LoadScene(1);       
    }

    public void ChangeSoundActive() 
    {
        _isSoundActive = !_isSoundActive;
        if(_isSoundActive)
            _soundImage.sprite = _audioOn;
        else
            _soundImage.sprite = _audioOff;
        SetSoundValue(_isSoundActive);
        SafeData.Instance.IsActiveSound = _isSoundActive;
    }

    public void ChangeVisibleSetting() 
    {
        _windowSetting.SetActive(!_windowSetting.activeSelf);
        if (_isSoundActive)
            _soundImage.sprite = _audioOn;
        else _soundImage.sprite = _audioOff;
    }
}
