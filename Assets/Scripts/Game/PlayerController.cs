using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput PlayerInput;

        public Rigidbody Rigidbody;
        public Renderer BodyRenderer;
        public Transform BulletSpawnPoint;

        [Header("Views")] 
        public HitpointsView HitpointsView;

        public HitpointsView TimepointView;

        [Header("Gameplay")] 
        public Material NormalBodyMaterial;
        public Material FiremanBodyMaterial;
        public GameObject BulletPrefab;
        public float Speed = 5f;
        public float FireTime = 1f;
        public float HitpointsMax = 100f;
        public float GameTime = 10f;

        private float _fireTimer;

        public string PlayerName { get; set; } = "Fireman";

        private bool _isFireman;

        public bool IsFireman
        {
            get => _isFireman;
            set
            {
                _isFireman = value;
                BodyRenderer.material = value ? FiremanBodyMaterial : NormalBodyMaterial;
            }
        }

        public float Hitpoints { get; set; }

        public float GameTimeLeft { get; set; }

        public bool CanShoot => _fireTimer <= 0f;
        private void OnEnable()
        {
            _fireTimer = 0f;
            IsFireman = true;
            Hitpoints = HitpointsMax;
            GameTimeLeft = GameTime;
            HitpointsView.SetValue("hp", Hitpoints / HitpointsMax);
            HitpointsView.SetValue("time", GameTimeLeft / GameTime);
            HitpointsView.PlayerName.text = PlayerName;
        }

        private void Update()
        {
            if (GameTimeLeft <= 0 || Hitpoints <= 0)
                return;
            
            HitpointsView.SetValue("hp", Hitpoints / HitpointsMax);
            HitpointsView.SetValue("time", GameTimeLeft / GameTime);
        
            if (PlayerInput == null)
                return;

            var (moveDirection, viewDirection, shoot) = PlayerInput.CurrentInput();
            ProcessShoot(shoot);
            Rigidbody.velocity = moveDirection.normalized * Speed;
            transform.rotation = viewDirection;
        }

        private void ProcessShoot(bool isShoot)
        {
            if (!IsFireman)
                return;

            _fireTimer -= Time.deltaTime;

            if (isShoot && CanShoot)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Instantiate(BulletPrefab, BulletSpawnPoint.position, transform.rotation);
            _fireTimer = FireTime;
        }
    }
}