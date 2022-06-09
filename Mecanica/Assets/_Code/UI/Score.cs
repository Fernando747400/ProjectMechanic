using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] public int _target = 1;
    [SerializeField] public int _current = 0;

    
    void Update()
    {
        _score.text = _current + "/" + _target;
    }

    public void TargetReached()
    {
        if (_current == _target)
        {
            Debug.Log("Won Game");
        }
    }
}
