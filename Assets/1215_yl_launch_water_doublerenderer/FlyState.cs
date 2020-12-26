using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;


namespace YL
{
    public class FlyState : YB.StateBase
    {
        //retrive from editor
        Animator animator;
        YB.HumanControll humanControll;
        StarAnimation starAnimation;
        TrailRenderer trail;

        [SerializeField]
        public GameObject OrginalParentObject;
        public AnimationCurve pathCurve;

        [Range(0, 50)]

        //每秒飞行的距离 米
        public float speed = 10f;
        float speedModifier = 1;

        [Space]
        [Header("Booleans for debug")]
        [SerializeField]
        private bool insideLaunchStar, flying, almostFinished;
        private Rigidbody Rb;

        public bool InsideLaunchStar {
            get { return insideLaunchStar;}
        }

        //可以触发“发射”的物体
        private Transform launchObject;

        [Space]
        [Header("场景中需要引用的物体")]
        public CinemachineDollyCart dollyCart;
        public Transform playerParent;

        [Space]
        [Header("发射准备动画序列 参数")]
        public float prepMoveDuration = .15f;
        public float launchInterval = .5f;

        [Space]
        [Header("粒子特效")]
        public ParticleSystem followParticles;
        public ParticleSystem smokeParticle;

        private void Awake()
        {
            if (flyState == null)
            {
                flyState = this;
            }
            else
            {
                Destroy(this);
            }
        }
        void Start()
        {
            animator = GetComponent<Animator>();
            humanControll = GetComponent<YB.HumanControll>();
            trail = dollyCart.GetComponentInChildren<TrailRenderer>();
            insideLaunchStar = false;
        }

        void Update()
        {


            if (flying)
            {
                animator.SetFloat("Path", dollyCart.m_Position);

                playerParent.transform.position = dollyCart.transform.position;
                if (!almostFinished)
                {
                    //还在空中时，更新父物体跟随车的位置
                    playerParent.transform.rotation = dollyCart.transform.rotation;
                }
            }

            if (dollyCart.m_Position > .65f && !almostFinished && flying)
            {
                almostFinished = true;
                //thirdPersonCamera.m_XAxis.Value = cameraRotation;

                playerParent.DORotate(new Vector3(360 + 180, 0, 0), .5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
                    .OnComplete(() => playerParent.DORotate(new Vector3(-90, playerParent.eulerAngles.y, playerParent.eulerAngles.z), .2f));
            }
        }

        public override void handleInput()
        {
            base.handleInput();
            
                
        }

        

        IEnumerator CenterLaunch()
        {
            transform.parent = null;
            DOTween.KillAll();

            //Checks to see if there is a Camera Trigger at the DollyTrack object - if there is activate its camera
           // if (launchObject.GetComponent<CameraTrigger>() != null)
           //     launchObject.GetComponent<CameraTrigger>().SetCamera();
            
            if (launchObject.GetComponent<SpeedModifier>() != null)
                speedModifier = launchObject.GetComponent<SpeedModifier>().modifier;

            //Checks to see if there is a Star Animation at the DollyTrack object
            if (launchObject.GetComponentInChildren<StarAnimation>() != null)
                starAnimation = launchObject.GetComponentInChildren<StarAnimation>();

            dollyCart.m_Position = 0;
            dollyCart.m_Path = null;
            dollyCart.m_Path = launchObject.GetComponent<CinemachineSmoothPath>();
            dollyCart.enabled = true;

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            Sequence CenterLaunch = DOTween.Sequence();
            CenterLaunch.Append(transform.DOMove(dollyCart.transform.position, .2f));
            CenterLaunch.Join(transform.DORotate(dollyCart.transform.eulerAngles + new Vector3(90, 0, 0), .2f));
            CenterLaunch.Join(starAnimation.Reset(.2f));
            CenterLaunch.OnComplete(() => LaunchSequence());
        }

        void LaunchSequence()
        {
            CinemachineSmoothPath path = launchObject.GetComponent<CinemachineSmoothPath>();
            //Debug.Log(path.PathLength);

            //this.transform.parent = null;

            float finalSpeed = path.PathLength / (speed * speedModifier);

            playerParent.transform.position = launchObject.position;
            playerParent.transform.rotation = transform.rotation;

            flying = true;
            animator.SetBool("flying", true);
            Sequence s = DOTween.Sequence();

            s.AppendCallback(() => transform.parent = playerParent.transform);                                           // Attatch the player to the empty gameObject
            s.Append(transform.DOMove(transform.localPosition - transform.up, prepMoveDuration));                        // Pull player a little bit back
            s.Join(transform.DOLocalRotate(new Vector3(0, 360 , 0), prepMoveDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuart));
            s.Join(starAnimation.PullStar(prepMoveDuration));
            s.AppendInterval(launchInterval);                                                                            // Wait for a while before the launch
            s.AppendCallback(() => trail.emitting = true);
            s.AppendCallback(() => followParticles.Play());
            s.Append(DOVirtual.Float(dollyCart.m_Position, 1, finalSpeed, SetCartPosition).SetEase(pathCurve));                // Lerp the value of the Dolly Cart position from 0 to 1
            s.Join(starAnimation.PunchStar(.5f));
            s.Join(transform.DOLocalMove(new Vector3(0, 0, -.5f), .5f));                                                   // Return player's Y position
            s.Join(transform.DOLocalRotate(new Vector3(0, 360, 0),                                                       // Slow rotation for when player is flying
                (finalSpeed / 1.3f), RotateMode.LocalAxisAdd)).SetEase(Ease.InOutSine);
            s.AppendCallback(() => Land());                                                                              // Call Land Function

        }

        void Land()
        {
            playerParent.DOComplete();
            dollyCart.enabled = false;
            dollyCart.m_Position = 0;
            transform.parent = null;

            flying = false;
            almostFinished = false;
            animator.SetBool("flying", false);

            followParticles.Stop();
            trail.emitting = false;
            humanControll.enabled = true;

            smokeParticle.Play();
            turnToState(groundedState);

        }

        void SetCartPosition(float x)
        {
            dollyCart.m_Position = x;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Launch"))
            {
                print(other.gameObject.name);
                insideLaunchStar = true;
                launchObject = other.transform;
            }

            SetCameraToParent freeLookCamSetUp = other.GetComponent<SetCameraToParent>();

            if (other.CompareTag("CameraTrigger") && freeLookCamSetUp)
            {
                Debug.Log("detected setcamera to parent");
                other.GetComponent<SetCameraToParent>().SetCamera();
            }
            if (other.CompareTag("CameraTrigger") && !freeLookCamSetUp)
            {
                other.GetComponent<CameraTrigger>().SetCamera();
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Launch"))
            {
                insideLaunchStar = false;
            }
        }

        public override void enter()
        {
            base.enter();
            Debug.Log("entering flystate!");
            StartCoroutine(CenterLaunch());
            this.GetComponent<CharacterController>().enabled = false;
            Rb = this.gameObject.AddComponent<Rigidbody>();
            Rb.useGravity = false;
            Rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        }
        public override void exit()
        {
            Debug.Log("leaving flystate!");

            base.exit();
            Destroy(Rb);

            this.GetComponent<CharacterController>().enabled = true;
            //this.transform.SetParent(OrginalParentObject.transform);

        }
    }

}
