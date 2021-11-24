using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig: ScriptableObject
{
    [System.Serializable]
    public struct ObjectContainer
    {
        public GameObject _object;
        public Vector3 _position;
    }
    
    [System.Serializable]
    public struct ZombieContainer
    {
        public GameObject _zombie;
        public Vector3 _position;
        public BotDifficulty _Difficulty;
    }
    
    public ObjectContainer[] Walls;
    public ZombieContainer[] Zombies;
    public ObjectContainer[] Rocks;

    public Vector3 _spawnPoint;
}
