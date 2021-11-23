using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelConfig level;

    [Header("Трансформы рутовых объектов")]
    [SerializeField] private Transform _wallRoot;
    [SerializeField] private Transform _zombieRoot;
    [SerializeField] private Transform _rockRoot;

    [Header("Префабы")]
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private GameObject _rockPrefab;

    [ContextMenu("Instantinate")]
    public void InstantinateMap()
    {
        DeleteMap();
        foreach (var wall in level.Walls)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(wall._object, _wallRoot) as GameObject;
            _gameObject.transform.position = wall._position;
        }
        
        foreach (var zombie in level.Zombies)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(zombie._zombie, _zombieRoot) as GameObject;
            _gameObject.transform.position = zombie._position;
            _gameObject.GetComponent<ZombieComponent>()._botDifficulty = zombie._Difficulty;
        }
        
        foreach (var rock in level.Rocks)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(rock._object, _rockRoot) as GameObject;
            _gameObject.transform.position = rock._position;
        }

        transform.GetChild(4).position = level._spawnPoint;
    }

    [ContextMenu("DeletePrevMap")]
    public void DeleteMap()
    {
        while(_wallRoot.childCount>0){
            DestroyImmediate(_wallRoot.GetChild(0).gameObject);
        }
        while(_zombieRoot.childCount>0){
            DestroyImmediate(_zombieRoot.GetChild(0).gameObject);
        }
        while (_rockRoot.childCount > 0)
        {
            DestroyImmediate(_rockRoot.GetChild(0).gameObject);
        }
    }

    [ContextMenu("SpawnMainWalls")]
    public void SpawnMainWall()
    {
        for (int i = -12; i <= 12; i++)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(i, 1, 12);
            
            _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(i, 1, -12);
        }
        for (int i = -11; i <= 11; i++)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(12, 1, i);
            
            _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(-12, 1, i);
        }
    }
    
    [ContextMenu("SpawnWall")]
    public void SpawnWall()
    {
        var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
        _gameObject.transform.position = new Vector3(0, 1, 0) + new Vector3(-20, 0, 0);
    }

    [ContextMenu("SpawnZombie")]
    public void SpawnZombie()
    {
        var _gameObject = PrefabUtility.InstantiatePrefab(_zombiePrefab, _zombieRoot) as GameObject;
        _gameObject.transform.position = new Vector3(0, 0, 0);
    }
    
    [ContextMenu("SpawnRock")]
    public void SpawnRock()
    {
        var _gameObject = PrefabUtility.InstantiatePrefab(_rockPrefab, _rockRoot) as GameObject;
        _gameObject.transform.position = new Vector3(0, 0.1f, 0);
    }
    
    [ContextMenu("SpawnLine5")]
    public void SpawnLine()
    {
        for (int i = 0; i < 5; i++)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(i, 1, 0) + new Vector3(-20, 0, 0);
        }
    }
    
    [ContextMenu("SpawnSquare3x3")]
    public void SpawnSquare()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i != 4)
            {
                var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
                _gameObject.transform.position = new Vector3(i%3, 1, i/3) + new Vector3(-20, 0, 0);
            }
        }
    }
    
    [ContextMenu("SpawnStairFlat5")]
    public void SpawnStairFlat()
    {
        for (int i = 0; i < 5; i++)
        {
            var _gameObject = PrefabUtility.InstantiatePrefab(_wallPrefab, _wallRoot) as GameObject;
            _gameObject.transform.position = new Vector3(i, 1, i) + new Vector3(-20, 0, 0);
        }
    }
}
