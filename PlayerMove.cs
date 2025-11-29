using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    [Header("Speed")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotationSpeed;


    [Header("Force")]
    [SerializeField] private float JumpForce;



    private PlayerInput _playerInput;
    private InputAction _jumpAction;
    private InputAction _moveAction;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _moveDirection;

    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
        _playerInput.SwitchCurrentActionMap("Player");

        _moveAction = _playerInput.actions["Move"];

        _jumpAction = _playerInput.actions["Jump"];
        _jumpAction.performed += JumpFNC;
    }

    void Update()
    {
        if (UIManager.UIinstance.PuzzleState)
        {
            // Puzzle açıkken animasyonları kapat
            _animator.SetBool("isRun", false);
            return;
        }

        HareketInput();
    }

    private void HareketInput()
    {

        Vector2 inutVector2 = _moveAction.ReadValue<Vector2>();
        _moveDirection = new Vector3(inutVector2.x, 0, inutVector2.y).normalized;

        bool IsRun = _moveDirection.sqrMagnitude > 0.01f;

        _animator.SetBool("isRun", IsRun);
    }


    void FixedUpdate()
    {
        if (UIManager.UIinstance.PuzzleState)
        {
            // Puzzle açık → tamamen don
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            return;
        }



        if (_moveDirection.sqrMagnitude < 0.01f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, RotationSpeed * Time.fixedDeltaTime));

        Vector3 HorizontalMove = new Vector3(_moveDirection.x, 0f, _moveDirection.z);
        _rigidbody.MovePosition(_rigidbody.position + HorizontalMove * MoveSpeed * Time.fixedDeltaTime);


    }



    private void JumpFNC(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _animator.SetTrigger("JumpTrigger");
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

        }
    }



    private bool IsGrounded()
    {
        Vector3 orgin = transform.position + Vector3.up * 0.1f;
        float rayLength = 0.2f;
        return Physics.Raycast(orgin, Vector3.down, rayLength);
    }



    void OnDisable()
    {
        _jumpAction.performed -= JumpFNC;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetPlayerToCheckpointNextFrame());


    }

    private IEnumerator SetPlayerToCheckpointNextFrame()
    {
        yield return null; // Bir frame bekle, sahne objeleri hazır olsun

        Vector3 lastCheckpoint = GameManager.GMinstance.GetLastChecpointPosition();

        if (lastCheckpoint != Vector3.zero)
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // physics’i durdur
            }

            transform.position = lastCheckpoint;

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false; // physics’i yeniden aç
            }
        }
    }

}
