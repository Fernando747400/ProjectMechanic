using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI _timerText;

    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;
        _timerText.text = Mathf.Round(_timer).ToString();
    }
}
