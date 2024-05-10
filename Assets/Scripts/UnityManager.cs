using System;
using Cube;
using GameState;
using Sirenix.OdinInspector;
using UnityEngine;

public class UnityManager : SerializedMonoBehaviour
{
    [SerializeField] private  GameProgressHandler _gameProgressHandler;

    [SerializeField] private  CubeManager _cubeManager;

    [SerializeField] private ICubeUI _cubeUI;
        
    [Button("開始遊戲")]
    public void Start()
    {
        _gameProgressHandler = new GameProgressHandler();
        _cubeManager = new CubeManager();
        
        
            
        _gameProgressHandler.Init(_cubeManager, _cubeUI);
        _gameProgressHandler.StartGame();
    }


    [Button("重新產生 UI")]
    private void InitCube()
    {
        _cubeUI.InitCubes(_cubeManager.MAX_COLUMN, _cubeManager.MAX_ROW);
    }

    private void Update()
    {
        _gameProgressHandler.Update();

        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            AddCube(0);
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            AddCube(1);
        }
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        {
            AddCube(2);
        }
    }

    public void AddCube(int column)
    {
        _cubeManager.AddCube(column);
        _cubeUI.UpdateCubeDisplay(_cubeManager.GetCurrentMap());
    }
}