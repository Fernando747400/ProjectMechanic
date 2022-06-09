using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _timerText;

    public bool IsPlaying = false;
    private float _timer = 0f;

    private void Start()
    {
        StopTimer();
        _timerText.text = "0";
    }

    private void Update()
    {
        if (IsPlaying)
        {
            _timer += Time.deltaTime;
            _timerText.text = Mathf.Round(_timer).ToString();
        }      
    }

    public void StartTimer()
    {
        IsPlaying = true;
    }

    public void StopTimer()
    {
        IsPlaying = false;
    }
}
