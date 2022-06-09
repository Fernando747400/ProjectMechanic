using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    private void Awake()
    {
        current = this;
    }

    [SerializeField] private UnityEvent _startGame;
    public void StartGame() => _startGame?.Invoke();

    [SerializeField] private UnityEvent _wonGame;
    public void WonGame() => _wonGame?.Invoke();

    [SerializeField] private UnityEvent _lostGame;
    public void LostGame() => _lostGame?.Invoke();

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
