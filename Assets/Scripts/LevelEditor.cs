using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{

    public LevelConfig Level;

    [SerializeField] private Transform _wallRoot;
    [SerializeField] private Transform _zombieRoot;
    [SerializeField] private Transform _rockRoot;

    [ContextMenu("Save")]
    public void Save()
    {
        if (Level == null)
        {
            Level = ScriptableObject.CreateInstance<LevelConfig>();
            AssetDatabase.CreateAsset(Level, $"Assets/Resources/Levels/Level000.asset");
        }
        else
        {
            Level = ScriptableObject.CreateInstance<LevelConfig>();
            AssetDatabase.CreateAsset(Level, $"Assets/Resources/Levels/Level000.asset");
        }

        Level.Walls = new LevelConfig.ObjectContainer[_wallRoot.childCount];
        Level.Zombies = new LevelConfig.ZombieContainer[_zombieRoot.childCount];
        Level.Rocks = new LevelConfig.ObjectContainer[_rockRoot.childCount];

        for (int i = 0; i < _wallRoot.childCount; i++)
        {
            var obj = _wallRoot.GetChild(i);
            Level.Walls[i] = new LevelConfig.ObjectContainer
            {
                _object = PrefabUtility.GetCorrespondingObjectFromSource(obj.gameObject),
                _position = obj.position
            };
        }
        
        for (int i = 0; i < _rockRoot.childCount; i++)
        {
            var obj = _rockRoot.GetChild(i);
            Level.Rocks[i] = new LevelConfig.ObjectContainer
            {
                _object = PrefabUtility.GetCorrespondingObjectFromSource(obj.gameObject),
                _position = obj.position
            };
        }
        
        for (int i = 0; i < _zombieRoot.childCount; i++)
        {
            var obj = _zombieRoot.GetChild(i);
            Level.Zombies[i] = new LevelConfig.ZombieContainer
            {
                _zombie = PrefabUtility.GetCorrespondingObjectFromSource(obj.gameObject),
                _position = obj.position,
                _Difficulty = obj.GetComponent<ZombieComponent>()._botDifficulty
            };
        }

        Level._spawnPoint = transform.GetChild(4).position;

        EditorUtility.SetDirty(Level);
        AssetDatabase.SaveAssets();
    }
}
