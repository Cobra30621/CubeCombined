using System;
using Cube;
using Event;
using GameState;
using Sirenix.OdinInspector;
using UnityEngine;

public class UnityManager : SerializedMonoBehaviour
{
    [SerializeField] private  GameProgressHandler _gameProgressHandler;

    [SerializeField] private  ICubeManager _cubeManager;

    [Required]
    [SerializeField] private ICubeUI _cubeUI;
    [Required]
    [SerializeField] private IEventUI _eventUI;
    [Required]
    [SerializeField] private GameOverUI _gameOverUI;
    
    [Button("開始遊戲")]
    public void Start()
    {
        _gameProgressHandler = new GameProgressHandler();
        _gameProgressHandler.GameOverUI = _gameOverUI;
        
        _cubeManager = new CubeManager();
        _cubeManager.SetCubeUI(_cubeUI);
        _cubeManager.SetEventUI(_eventUI);
        
        _gameProgressHandler.Init(_cubeManager, _cubeUI);
        _gameProgressHandler.StartGame();
    }


    private void Update()
    {
        _gameProgressHandler.Update();

        // if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        // {
        //     AddCube(0);
        // }
        //
        // if (UnityEngine.Input.GetKeyDown(KeyCode.S))
        // {
        //     AddCube(1);
        // }
        //
        // if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        // {
        //     AddCube(2);
        // }
    }

    public void AddCube(int column)
    {
        _cubeManager.AddCube(column);
        
    }
}