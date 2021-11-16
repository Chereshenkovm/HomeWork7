using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ZombieComponent : MonoBehaviour
    {
        [Header("ZombieAIDifficulty")] [SerializeField]
        private BotDifficulty _botDifficulty = BotDifficulty.HARD;
        [Header("Rock Prefab")]
        [SerializeField] private GameObject _rockPrefab;
        
        [Header("ZombieController")]
        public ZombieInput ZombieInput;

        [SerializeField] private Rigidbody Rigidbody;

        [SerializeField] private float ZombieSpeed = 3f;
        
        [Header("ZombieLogic")]
        
        [SerializeField] private GameObject _aliveView;

        [SerializeField] private GameObject _diedView;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Vector3[] _deltaPath;

        private int _currentPoint = 0;
        private Vector3 _initPosition;
        private float HitTime = 1f;
        private float _hitTimer = 0f;
        private bool hasRock = false;
        private string currentTask = "";

        private void Awake()
        {
            _initPosition = transform.position;
            switch (_botDifficulty)
            {
                case BotDifficulty.EASY:
                    ZombieSpeed = ZombieSpeed;
                    break;
                case BotDifficulty.MEDIUM:
                    ZombieSpeed = ZombieSpeed * 1.5f;
                    break;
                case BotDifficulty.HARD:
                    ZombieSpeed = ZombieSpeed * 2;
                    break;
            }
        }

        private void OnEnable()
        {
            SetState(true);
        }

        private void Update()
        {
            if (_deltaPath == null || _deltaPath.Length < 2)
                return;

            if (IsAlive)
            {
                var (moveDirection, distance, viewDirection,hasFound) = ZombieInput.CurrentInput();
                var (moveRockDirection, rockDistance, rockTrans,isThereRocks) = ZombieInput.GetRock();

                switch (getActionName(distance, rockDistance, hasFound, isThereRocks, _botDifficulty))
                {
                    case "player":
                        Rigidbody.velocity = moveDirection.normalized * ZombieSpeed;
                        transform.rotation = viewDirection;
                        break;
                    case "rock":
                        Rigidbody.velocity = moveRockDirection.normalized * ZombieSpeed;
                        break;
                    case "pickRock":
                        takeRock(rockTrans);
                        break;
                    case "throwRock":
                        throwRock(distance);
                        break;
                    case "hitPlayer":
                        Rigidbody.velocity = Vector3.zero;
                        hitPlayer();
                        break;
                }
            }

        }

        public void SetState(bool alive)
        {
            _aliveView.SetActive(alive);
            _diedView.SetActive(!alive);
        }

        public bool IsAlive => _aliveView.activeInHierarchy;

        private string getActionName(Vector3 playerDist, float rockDist, bool foundPlayer, bool rocksExist, BotDifficulty bDiff)
        {
            List<int> pointsAc = new List<int> {0, 0, 0, 0, 0};

            pointsAc[0] = (playerDist.magnitude <= 10 ? 50 : 0) + (!rocksExist ? 100 : 0) + (hasRock ? 100: 0);
            pointsAc[1] = (playerDist.magnitude > 10 ? 150 : 0) + (rockDist <= 10 ? 50 : 0) + (hasRock ? -1000 : 0) +
                          (!rocksExist ? -1000 : 0) + (bDiff == BotDifficulty.HARD ? 0 : -10000);
            pointsAc[2] = (rockDist < 1f ? 1000 : 0) + (hasRock ? -1000 : 0) + (bDiff == BotDifficulty.HARD ? 0 : -10000);
            pointsAc[3] = (playerDist.magnitude < 5 ? 200 : 0) + (hasRock ? 100 : -1000) + (bDiff == BotDifficulty.HARD ? 0 : -10000);
            pointsAc[4] = (playerDist.magnitude <= 1f ? 1001 : 0) + (bDiff == BotDifficulty.EASY ? -10000 : 0);

            switch (pointsAc.IndexOf(pointsAc.Max()))
            {
                case 0:
                    return "player";
                    break;
                case 1:
                    return "rock";
                    break;
                case 2:
                    return "pickRock";
                    break;
                case 3:
                    return "throwRock";
                    break;
                case 4:
                    return "hitPlayer";
                    break;
            }

            return "player";
        }

        private void hitPlayer()
        {
            if (_hitTimer <= 0)
            {
                GetComponentInParent<PlayerContainer>().GetPlayerController().Hitpoints -= 10;
                _hitTimer = HitTime;
            }
            else
            {
                _hitTimer -= Time.deltaTime;
            }
        }

        private void throwRock(Vector3 playerDir)
        {
            var rockObj = Instantiate(_rockPrefab, transform.GetChild(2).position, Quaternion.identity);
            rockObj.GetComponent<RockLogic>().SetVelocity(playerDir);
            hasRock = false;
        }

        private void takeRock(Transform rock)
        {
            Destroy(rock.gameObject);
            hasRock = true;
        }
    }
}