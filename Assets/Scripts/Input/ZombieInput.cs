using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieInput : MonoBehaviour
{
    public abstract (Vector3 moveDirection, Vector3 distance, Quaternion viewDirection, bool found) CurrentInput();
    public abstract (Vector3 moveRockDirection, float clDistance, Transform rock, bool rocksExist, bool reachableRockExist) GetRock();
}
