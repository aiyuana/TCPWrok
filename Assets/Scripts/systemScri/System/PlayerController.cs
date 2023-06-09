﻿using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
#endif


//人物移动跳跃，攻击，动画，镜头移动缩放，鼠标点击，触发事件，
namespace StarterAssets
{
    #region hand

    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif

    #endregion
   
    public class PlayerController : MonoBehaviour
    {
        #region float

        public float MoveSpeed = 4.0f;

        
        public float SprintSpeed = 5.335f;

       
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
    
        public float JumpHeight = 2f;

      
        public float Gravity = -15.0f;

        [Space(10)]
       
        public float JumpTimeout = 0.50f;

        public float FallTimeout = 0.15f;

     
        public bool Grounded = true;

   
        public float GroundedOffset = -0.14f;

     
        public float GroundedRadius = 0.28f;

        public LayerMask GroundLayers;

        
        public GameObject CinemachineCameraTarget;

      
        public float TopClamp = 70.0f;

        
        public float BottomClamp = -30.0f;

  
        public float CameraAngleOverride = 0.0f;

        
        public bool LockCameraPosition = false;

        public bool weaint=true;//武器切换

        #endregion


        #region gameobjectflaot

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        public GameObject hand;
        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDRoll;
        private int _animIDArttac;
        private int _animIDArttac1;
        
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
            
        private const float _threshold = 0.01f;

        private bool _hasAnimator;
        public CinemachineVirtualCamera cinemachineVirtualCamera;
public float zoomminDistance=1.5f;
public float zoommaxDistance=6f;
public float zoomspeed = 0.02f;
public float zoomfactor = 0.5f;
private float zoom = 4f;
private float camerDistance = 4f;
private float zoomvelocity = 0f;
    
        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        #endregion
       

//背包部分 
        public GameObject mybag;
        private bool isopen;
        public GameObject tacks;
        private bool istack;
        private void tack()
        { if(!tacks.activeSelf)
            {
               istack = false;
               
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (istack==true)
                {
                    tacks.SetActive(false);
                    startgame();
                }

                else
                {   Cursor.visible = true;
                    ismouse = true;
                    tacks.SetActive(true);
                    istack = true;
                    stopgame();
                }
                
                
                
            }
            
        }
      private  void openmybag()
        {//防止bug
            if(!mybag.activeSelf)
            {
                isopen = false;
                
               
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isopen = !isopen;
                mybag.SetActive(isopen);
             
            }
           
        }
        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void OnDisable()
        {
            EVentcontorl.RemoveListener(Eventnum.GAMOVer,GameOver);
        }
        private void Start()
        { 
            EVentcontorl.AddListener(Eventnum.GAMOVer,GameOver);
            this.zoomvelocity = 0f;
            _button = GameObject.Find("usemange");
            

            GameState.time = (e )=>
            {
                if (e)
                {//暂停游戏
                    stopgame();
                   
                }
                else
                {//游戏继续
                    startgame();
                }
            };
                //相机获取
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            //检测动画是否执行
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            //获取新输入系统，因为预留了安卓和box手柄的输入系统
            _playerInput = GetComponent<PlayerInput>();
            AssignAnimationIDs();

            // 在启动时重置超时
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

     //开始游戏
     void GameOver()
     {
         stopgame();
     }
     //游戏继续
     public   void startgame()
        {
            paused = false;
        }
//暂停游戏
    public    void stopgame()
        {
            paused = true;
        }
        
        protected bool paused;
        private bool ismouse=true;
        public GameObject clothbox;
        private void Update()
        {
            if (clothbox.gameObject.activeSelf==true)
            {
                ismouse = false;
            }
            

            if (ismouse)
            {
                Cursor.visible = false;
            }

            else
            {
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (ismouse==true)
                {
                    ismouse = false;
                }
                else
                {
                    ismouse = true;
                }
               
            }
            openmybag();
            tack();
            if (!paused)
            {
                JumpAndGravity();
                GroundedCheck();
                Move();
                roll();
                ARttack();
                weachage();
            }
            
           
            
        }

        private void FixedUpdate()
        {
           
            _hasAnimator = TryGetComponent(out _animator);
           
        }
        private void LateUpdate()
        {
            if (!paused)
            { 
                CameraRotation();
                camerzoom();
            }
          
        }

        
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDRoll = Animator.StringToHash("roll");
            _animIDArttac = Animator.StringToHash("Arttack");
            _animIDArttac1 = Animator.StringToHash("Arttack1");
            

        }
        private float main_time;
        public float time;
        private float two_twoClicks ;
        private int count;

        private GameObject _button;
        public RuntimeAnimatorController controller;
        public RuntimeAnimatorController controller1;
        private void weachage()
        {
            //武器切换
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (weaint==true)
                {
                    _animator.runtimeAnimatorController = controller;
                    hand.transform.Find("Finger_01 1").transform.Find("fring").gameObject.SetActive(false);
                    hand.transform.Find("SM_Wep_Rifle_Plasma_01").gameObject.SetActive(true);
                    weaint = false;
                }

                else
                {_animator.runtimeAnimatorController = controller1;
                    hand.transform.Find("Finger_01 1").transform.Find("fring").gameObject.SetActive(true);
                    hand.transform.Find("SM_Wep_Rifle_Plasma_01").gameObject.SetActive(false);
                    weaint = true;
                }
            }
            
        }
     //攻击  
     public GameObject came;
     public AudioClip attrtSound;
       private void ARttack()
       {
           if (weaint==false)
           {
               if (Input.GetMouseButton(1))
               {
                   //cinemachineVirtualCamera.gameObject.SetActive(false);
                   came.gameObject.SetActive(true);
               }

               if (Input.GetMouseButtonUp(1))
               {
                   came.gameObject.SetActive(false);
                  // cinemachineVirtualCamera.gameObject.SetActive(true);
               }
           }
           

           if (Input.GetMouseButton(0))
           {//ui图标没有点不会去A
               GameObject btn = EventSystem.current.currentSelectedGameObject;
               if (btn == null)
               {



                   if (main_time == 0.0f)
                   {
                       main_time = Time.time;
                       if (weaint==false)
                       {fireshot();
                           
                       }
                       
                   }

                   if (Time.time - main_time > 0.2f)
                   {
                       _animator.SetBool(_animIDArttac, false);
                       //长按时执行的动作放这里
                       _animator.SetBool(_animIDArttac1, true);
                       
                   }
                   else if(Input.GetKey(KeyCode.LeftShift))
                   {
                       //_animator.SetBool("atc3", true);
                       _animator.Play("Attack_C2_1");
                       if (weaint==false)
                       {
                           fireshot();
                           
                       }

                       
                   }
                   else
                 
                   {
                       _animator.SetBool(_animIDArttac, true);
                      

                   }

               }
           }

           if (Input.GetMouseButtonUp(0))
           {
                       main_time = 0.0f;
                       _animator.SetBool(_animIDArttac1, false);
                       _animator.SetBool(_animIDArttac, false);//_animator.SetBool("atc3", false);
           }

           
       }
//翻滚
        public GameObject shoot;
        public GameObject bullet;
       
        private Vector3 targetPoint;



        public GameObject point;
        void fireshot()
        {
            

           
            Vector3 vector3 = new Vector3(shoot.transform.position.x, shoot.transform.position.y, shoot.transform.position.z);
            GameObject clone =  Instantiate(bullet, vector3,shoot.transform.rotation);
         clone.gameObject.transform.LookAt(point.transform);
         AudioManager.instance.AudioPlay(attrtSound);
            Destroy(clone,1.5f);
        }
       private void  roll()
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                _animator.SetBool(_animIDRoll,true);
            }
            if (Input.GetKeyUp(KeyCode.F)) 
            {
                _animator.SetBool(_animIDRoll,false);
            }
        }
       //地面检测
        private void GroundedCheck()
        {
            //旋转数值
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

           //动画更新
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }
//相机缩放
        private void camerzoom()
        {
            this.zoom -= _input.Zoom / 240 * this.zoomfactor;
            this.zoom = Mathf.Clamp(this.zoom, this.zoomminDistance, this.zoommaxDistance);
            this.camerDistance = Mathf.SmoothDamp(this.camerDistance, this.zoom, ref zoomvelocity,
                Time.unscaledTime * this.zoomspeed);

            this.cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance =
                this.camerDistance;
        }
        //相机旋转
        private void CameraRotation()
        {
           
           //相机看向
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
               //鼠标移动速率
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

           //相机360度的调整
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

           //相机是否会跟着人走
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }
//人物角色移动
        private void Move()
        {
            //检查移动速度是否按下
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            //一种简单的加速和减速，设计为易于移除、替换或重复
//注意：Vector2的==运算符使用近似值，因此不易出现浮点错误，而且比幅度更便宜
//如果没有输入，将目标速度设置为0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            //当前水平速度的参考
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                //转弯
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // 小数点后三位
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
//需要执行的动画
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

          
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

           
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

             
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

           
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

           
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }
//跳跃
        private void JumpAndGravity()
        {
            if (Grounded)
            {
               
                _fallTimeoutDelta = FallTimeout;

               
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    //H * -2 * G 的平方根 = 达到所需高度所需的速度
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // 检查是否使用动画
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // 条的时间
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // 重置跳跃时间
                _jumpTimeoutDelta = JumpTimeout;

                // 落下时间
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                //如果不在地上
                _input.jump = false;
            }

            //如果在终端下方，则随时间推移施加重力（乘以增量时间两次以随时间线性加速）
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                    AudioManager.instance.AudioPlay(FootstepAudioClips[index]);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
                AudioManager.instance.AudioPlay(LandingAudioClip);
            }
        }
        
       
    }
    
    
}