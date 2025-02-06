using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraningUI : MonoBehaviour
{
    [SerializeField] private GameObject _traningPanel;
    [SerializeField] private List<GameObject> _instructions = new List<GameObject>();

    private int _currentInstruction = 0;

    public void StartTraning() 
    {
        _traningPanel.SetActive(true);
        _instructions[_currentInstruction].SetActive(true);
    }

    public void NextInstruvtion() 
    {
        _currentInstruction++;
        if (_currentInstruction < _instructions.Count)
        {
            _instructions[_currentInstruction-1].SetActive(false);
            _instructions[_currentInstruction].SetActive(true);
        }
        else
            _currentInstruction = _instructions.Count - 1;
    }

    public void PrevInstruvtion()
    {
        _currentInstruction--;
        if (_currentInstruction >= 0)
        {
            _instructions[_currentInstruction+1].SetActive(false);
            _instructions[_currentInstruction].SetActive(true);
        }
        else
            _currentInstruction = 0;
    }

    public void Close() 
    {
        foreach (GameObject instruction in _instructions)
        {
            instruction.SetActive(false);          
        }
        _traningPanel.SetActive(false);
        GameMode.Instance.ContinueGame();

    }
}
