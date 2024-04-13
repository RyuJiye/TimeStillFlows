using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;
    GameObject player;
    Rigidbody2D playerRigid;
    private Vector2 lastPosition;
    private float deltaX;
    private float direction = 0;
    private bool isDragging = false;
    private bool isGround;
    Action moveFunc;

    void Awake()
    {
        player = GameObject.Find("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        lastPosition = eventData.position;
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        moveFunc = null;
        isDragging = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isDragging = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        deltaX = eventData.position.x - lastPosition.x;
        moveFunc -= Move;
        moveFunc += Move;
        lastPosition = eventData.position;
    }
    private void Move()
    {
        if (Mathf.Abs(deltaX) > 4) // ���� ������ ���ؼ� 
            direction = Mathf.Sign(deltaX);
        player.transform.Translate(direction * moveSpeed * Time.deltaTime * Vector3.right);
    }

    public void Jump()
    {
        if (isGround)
        {
            isGround = false;
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
            playerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    public void GroundCheck()
    {

    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            moveFunc?.Invoke();
        }
    }
    void Update()
    {
        isGround = Physics2D.Raycast(playerRigid.position, Vector3.down, 0.7f, LayerMask.GetMask("Platform")); // ����ĳ��Ʈ�� ���� ������ ���� �����ϰ� // Layer�� Playform�� �ֵ鸸 ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
