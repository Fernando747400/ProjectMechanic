using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _timerText;

    public bool IsPlaying = true;
    private float _timer = 0f;

    private void Update()
    {
        if (IsPlaying)
        {
            _timer += Time.deltaTime;
            _timerText.text = Mathf.Round(_timer).ToString();
        }      
    }
}
