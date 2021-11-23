using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEditor.AI.NavMeshBuilder;

namespace Game
{
    public class LevelMap : MonoBehaviour
    {
        public LevelConfig level;
        
        [SerializeField] private GameObject _prefab;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform _zombieRoot;
        [SerializeField] private Transform _rockRoot;

        [SerializeField] private Transform _player;

        [ContextMenu("Clear Map")]
        public void Clear()
        {
            while(_root.childCount>0){
                DestroyImmediate(_root.GetChild(0).gameObject);
            }
            while(_zombieRoot.childCount>0){
                DestroyImmediate(_zombieRoot.GetChild(0).gameObject);
            }
            while(_rockRoot.childCount>0){
                DestroyImmediate(_rockRoot.GetChild(0).gameObject);
            }
        }

        [ContextMenu("Instantiate From Map")]
        public void InstantFromMap()
        {
            Clear();
            foreach (var wall in level.Walls)
            {
                var _gameObject = PrefabUtility.InstantiatePrefab(wall._object, _root) as GameObject;
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

            _player.position = level._spawnPoint;
            
            NavMeshBuilder.ClearAllNavMeshes();
            NavMeshBuilder.BuildNavMesh();
        }
    }
}