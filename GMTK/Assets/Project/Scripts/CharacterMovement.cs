using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float CharacterMovementDelay;
    public Transform mesh;

    public PhysicalDice DicePrefab;
    public Transform DiceSpawn;
    public float DiceDelay;
    public bool CanThrow;

    public bool CanMove;
    private bool inputsAreFree;
    private bool gameStarted = false;
    public bool CanMoveForward;
    public bool CanMoveBackward;
    public bool CanMoveLeft;
    public bool CanMoveRight;


    public GameObject[] playerDirection;
    private int dir;

    public AudioSource playerAudioSource;
    public AudioClip[] playerMovementAudioClips;

    public AudioSource diceAudioSource;
    public AudioClip[] diceSpawnAudioClips;
    public GameObject gameStartDirector;

    private void Start()
    {
        FreeInputsCoroutine();
    }
    private void Update()
    {
        ProcessPlayerInput();
        ProcessPlayerAttack();
        ProcessPlayerDirection();
    }

    private void ProcessPlayerDirection()
    {
        switch (Mathf.Round(mesh.transform.localRotation.eulerAngles.y))
        {
            case 180:
                dir = 1;
                break;
            case 270:
                dir = 2;
                break;
            case 0:
                dir = 0;
                break;
            case 90:
                dir = 3;
                break;
        }
        for (int i = 0; i < playerDirection.Length; i++)
        {
            if (i == dir)
            {
                playerDirection[i].gameObject.SetActive(true);
            }
            else
            {
                playerDirection[i].gameObject.SetActive(false);

            }
        }
    }

    private void ProcessPlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanMove && CanThrow)
        {
            if (inputsAreFree && !gameStarted) {
                gameStartDirector.SetActive(true);
                gameStarted = true;
            }
            Vector3 position = transform.position;
            position.y += 0.5f;

            if (Physics.Raycast(position, mesh.forward, out RaycastHit hit, .5f))
            {
                Debug.Log(hit.transform.gameObject.name);

                return;
            }

            PhysicalDice instance = Instantiate(DicePrefab, DiceSpawn.position, Quaternion.Euler(-90, mesh.eulerAngles.y, 0));

            int selectedClip = UnityEngine.Random.Range(0, diceSpawnAudioClips.Length);
            diceAudioSource.clip = diceSpawnAudioClips[selectedClip];
            diceAudioSource.Play();

            instance.MoveUntilHitWall(mesh.forward);

            ThrowDelayCoroutine();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        position.y += 0.5f;

        Gizmos.DrawLine(position, position + mesh.forward);
        Gizmos.DrawLine(position, position - mesh.forward);
        Gizmos.DrawLine(position, position + mesh.right);
        Gizmos.DrawLine(position, position - mesh.right);
    }

    private void ProcessPlayerInput()
    {
        if (!CanMove)
        {
            return;
        }


        
        Vector3 position = transform.position;
        position.y += 0.5f;


        if (Physics.Raycast(position, transform.forward, out RaycastHit hit, .8f))
        {
            Debug.DrawRay(position, transform.forward, Color.magenta);
            CanMoveForward = false;

            if (hit.transform.gameObject != null)
            {
                Debug.Log(hit.transform.name);
            }
        }
        else
        {
            CanMoveForward = true;
        }

        if (Physics.Raycast(position, -transform.forward, out RaycastHit hit2, .8f))
        {
            Debug.DrawRay(position, -transform.forward, Color.green);
            CanMoveBackward = false;

            if (hit2.transform.gameObject != null)
            {
                Debug.Log(hit2.transform.name);
            }
        }
        else
        {
            CanMoveBackward = true;
        }

        if (Physics.Raycast(position, transform.right, out RaycastHit hit3, .8f))
        {
            Debug.DrawRay(position, transform.right, Color.red);
            CanMoveRight = false;

            if (hit3.transform.gameObject != null)
            {
                Debug.Log(hit3.transform.name);
            }
        }
        else
        {
            CanMoveRight = true;
        }

        if (Physics.Raycast(position, -transform.right, out RaycastHit hit4, .8f))
        {
            Debug.DrawRay(position, -transform.right, Color.blue);
            CanMoveLeft = false;

            if (hit4.transform.gameObject != null)
            {
                Debug.Log(hit4.transform.name);
            }
        }
        else
        {
            CanMoveLeft = true;
        }

        Vector2Int input = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));

        Move(input);
    }

    public void Move(Vector2Int input)
    {
        bool movingOnBlocked = false;

        // if both movement axis are pressed
        if (input.x != 0 && input.y != 0)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveForward && input.y == 1)
        {
            input.y = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveBackward && input.y == -1)
        {
            input.y = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveRight && input.x == 1)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        if (!CanMoveLeft && input.x == -1)
        {
            input.x = 0;
            movingOnBlocked = true;
        }

        Vector3 targetPosition = transform.position + (transform.forward * input.y) + (transform.right * input.x);

        if (input.magnitude == 0 || movingOnBlocked)
        {

            return;
        }
        else
        {
            if (inputsAreFree && !gameStarted)
            {
                gameStartDirector.SetActive(true);
                gameStarted = true;
                CanMove = true;
            }
        }

        InputDelayCoroutine();
        //mesh.LookAt(targetPosition);
        mesh.DOLookAt(targetPosition, CharacterMovementDelay);
        transform.DOMove(targetPosition, CharacterMovementDelay).SetEase(Ease.OutQuad);
    }


    private async void InputDelayCoroutine()
    {
        CanMove = false;
        int selectedClip = UnityEngine.Random.Range(0, playerMovementAudioClips.Length);
        playerAudioSource.clip = playerMovementAudioClips[selectedClip];
        playerAudioSource.Play();

        TimeSpan duration = TimeSpan.FromSeconds(CharacterMovementDelay);
        await UniTask.Delay(duration);

        CanMove = true;
    }

    private async void ThrowDelayCoroutine()
    {
        CanThrow = false;

        TimeSpan duration = TimeSpan.FromSeconds(DiceDelay);
        await UniTask.Delay(duration);

        CanThrow = true;
    }
    private async void FreeInputsCoroutine()
    {
        CanThrow = false;
        CanMove = false;

        TimeSpan duration = TimeSpan.FromSeconds(4f);
        await UniTask.Delay(duration);

        CanMove = true;
        CanThrow = true;
        inputsAreFree = true;
    }
}
