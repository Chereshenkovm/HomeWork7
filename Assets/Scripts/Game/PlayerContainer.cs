using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    public PlayerController GetPlayerController()
    {
        return _player;
    }
}
