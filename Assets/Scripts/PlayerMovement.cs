using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    NavMeshAgent playerAgent;
    public LayerMask layerMask;
    public LayerMask ignoreMask;
    public event Action destinationReached;
    public event Action PlayerMoving;
    [SerializeField]
    float targetTreshold = 0.5f;
    Vector3 targetPos;
    bool isMoving = false;
    bool isNormalWalk = false;
    public Transform destObj;
    Animator playerAnimator;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    playerAgent = GetComponent<NavMeshAgent>();
    //    playerAnimator = GetComponent<Animator>();
    //}
    private void OnEnable()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerAgent.gameObject.activeInHierarchy) return;
        if (!PlayerStateManager.Instance.IsNotSleeping) return;
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    SendToInteractable(destObj);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreMask))
            {

            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.collider.gameObject.name);
                playerAgent.SetDestination(hit.point);
                isMoving = true;
                PlayerMoving?.Invoke();
                isNormalWalk = true;
                playerAgent.isStopped = false;

                targetPos = hit.point;
                PlayWalkAnim();
            }
        }

        if (isMoving)
        {
            CheckDestinationReached();
        }
    }

    public void SendToInteractable(Transform intPos)
    {
        if (!playerAgent.gameObject.activeInHierarchy) return;

        isMoving = true;
        var p = intPos.transform.position;
        p.y = transform.position.y;
        playerAgent.SetDestination(p);
        playerAgent.isStopped = false;
        targetPos = p;
        PlayerMoving?.Invoke();
        PlayWalkAnim();
    }

    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);
        if (distanceToTarget < targetTreshold)
        {
            if (!isNormalWalk)
            {
                destinationReached?.Invoke();
            }
            isMoving = false;
            isNormalWalk = false;
            if (playerAgent.gameObject.activeInHierarchy)
            {
                playerAgent.isStopped = true;
                playerAgent.destination = transform.position;
            }
            PlayIdleAnim();
        }
    }

    //public void DestinationReachedCallBack(Action isReached)
    //{
    //    destinationReached = isReached;
    //}

    void PlayWalkAnim()
    {
        playerAnimator.SetBool("Walk", true);
        playerAnimator.SetBool("Idle", false);
    }

    void PlayIdleAnim()
    {
        playerAnimator.SetBool("Walk", false);
        playerAnimator.SetBool("Idle", true);
    }
}
