using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private RockMap _rockMap;

    public PlayerController GetPlayerController()
    {
        return _player;
    }

    public Transform GetPlayerTransform()
    {
        return _playerTransform;
    }

    public RockMap GetRockMap()
    {
        return _rockMap;
    }
}
