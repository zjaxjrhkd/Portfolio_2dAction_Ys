using UnityEngine;


public class PlayertMove : MonoBehaviour
{
    enum Posture //캐릭터의 상태를 나타내는 enum 하지만 실질적으로 사용되지는 않았다.
    {
        idle,
        walk,
        run,
        attack,
        rightMiddle,
        rightUp,
        rightDown,
        down,
        up,
        leftMiddle,
        leftUp,
        leftDown
    }

    private Posture posture;

    public float speed; //속도 : Inspector에 지정
    public float run; //속도 : Inspector에 지정

    private Rigidbody2D rb2d;

    public Animator moveStance;

    private bool isRunning = false;                     // 캐릭터가 달리고 있는지 여부를 나타내는 변수
    private bool isWalking = false;                     // 캐릭터가 달리고 있는지 여부를 나타내는 변수

    private bool isKeyPressed = false;                  // 방향키가 눌려있는지 여부를 나타내는 변수
    private float keyPressTime = 0f;                    // 방향키를 처음 누른 시간을 저장하는 변수
    private float keyPressThreshold = 0.1f;             // 방향키를 두 번 눌러 달리기 상태로 전환하는 시간 간격
    private float keyPressSpareTime = 0f;                    // 방향키를 처음 누른 시간을 저장하는 변수
    private float keyPressSpare = 0.5f;
    private bool isWaiting = false;

    private bool isAttackWaiting = false;
    private float keyPressAttackTime = 0f;                    // 방향키를 처음 누른 시간을 저장하는 변수
    private float keyPressAttack = 0.08f;

    public GameObject AttackObject; // 활성화할 오브젝트

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        run = 1;
        posture = Posture.idle;
    }
    private void Update()
    {
        PlayerMoveSystem();
        PlayerAttack();
    }

    private void PlayerMoveSystem()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveStance.SetBool("checkRight", true);
            moveStance.SetBool("checkLeft", false);
            moveStance.SetBool("checkDown", false);
            moveStance.SetBool("checkUp", false);

            posture = Posture.rightMiddle;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveStance.SetBool("checkRight", false);
            moveStance.SetBool("checkLeft", true);
            moveStance.SetBool("checkDown", false);
            moveStance.SetBool("checkUp", false);

            posture = Posture.leftMiddle;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveStance.SetBool("checkRight", false);
            moveStance.SetBool("checkLeft", false);
            moveStance.SetBool("checkDown", false);
            moveStance.SetBool("checkUp", true);

            posture = Posture.up;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveStance.SetBool("checkRight", false);
            moveStance.SetBool("checkLeft", false);
            moveStance.SetBool("checkDown", true);
            moveStance.SetBool("checkUp", false);

            posture = Posture.down;

        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            moveStance.SetBool("checkRight", true);
            moveStance.SetBool("checkLeft", false);
            moveStance.SetBool("checkDown", false);
            moveStance.SetBool("checkUp", true);

            posture = Posture.rightUp;

        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            moveStance.SetBool("checkRight", true);
            moveStance.SetBool("checkLeft", false);
            moveStance.SetBool("checkDown", true);
            moveStance.SetBool("checkUp", false);

            posture = Posture.rightDown;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            moveStance.SetBool("checkRight", false);
            moveStance.SetBool("checkLeft", true);
            moveStance.SetBool("checkDown", false);
            moveStance.SetBool("checkUp", true);

            posture = Posture.leftUp;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            moveStance.SetBool("checkRight", false);
            moveStance.SetBool("checkLeft", true);
            moveStance.SetBool("checkDown", true);
            moveStance.SetBool("checkUp", false);

            posture = Posture.leftDown;
        }

        // 방향키 입력 감지
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        bool shift = Input.GetKey(KeyCode.LeftShift);//걷기 버튼

        if (shift==false)//걷기버튼을 눌렀는지 확인
        {   
            if (!isRunning)//지금 달리기 상태인지 확인
            {

                if (horizontalInput != 0 || verticalInput != 0)//방향키가 눌려있음을 확인하고
                {
                    StartWalking();//걷기 메소드 호출
                    keyPressTime = Time.time;           // 방향키를 처음 누른 시간을 저장하는 변수         
                }
                else if (isWalking && !isKeyPressed && Time.time - keyPressTime <= keyPressThreshold)
                {//(걷기 상태이고)(방향키가 눌려있지 않고), 메소드가 실행됬을때 시간에서 키가 눌렸을때 시간을 키가 눌리지 않았던 시간이 지정한 시간보다 적다면 달리기 시행
                    StartRunning();
                    moveStance.SetBool("checkRun", true);
                }
            }
            else if (isRunning)//이 if문은 달리기 상태에서 대기상태로 가는 기능을 한다
            {
                if (horizontalInput == 0 && verticalInput == 0)//방향키가 눌려있지 않다면
                {
                    if (!isWaiting)//대기상태가 아니라면
                    {
                        keyPressSpareTime = 0f; //방향키를 눌렀던 시간을 초기화
                        isWaiting = true; //대기상태로
                    }
                    else
                    {
                        keyPressSpareTime += Time.deltaTime; //키를 눌렸던 시간을 측정
                        if (keyPressSpareTime >= keyPressSpare) //키를 눌렀던 시간보다 기준값이 같거나 작다면 
                        {
                            StopMove();//움직임을 멈춘다.
                            moveStance.SetBool("checkRun", false);//달리기 애니메이션 bool값을 해제
                            isWaiting = false;//대기상태를 해제한다
                        }//이 if문은 이동버튼을 누르고 있다면 실행되지 않음
                    }
                }
            }
        }
        Vector2 movement = new Vector2(horizontalInput * speed, verticalInput * speed);
        rb2d.velocity = movement * run;
    }

    private void StartWalking()
    {
        if (!isRunning)  // 달리기 상태가 아닌 경우에만 걷기로 전환
        {
            isKeyPressed = false;
            isWalking = true;
        }
    }
    private void StartRunning()
    {
        isRunning = true;
        run = 5;
    }

    private void StopMove()
    {
        isKeyPressed = false;
        isWalking = false;
        isRunning = false;
        run = 1;
    }

    private void PlayerAttack()
    {
        keyPressAttackTime += Time.deltaTime;//공격키가 눌려있는 시간 저장

        if (Input.GetKey(KeyCode.Z))
        {
            if (!isAttackWaiting)
            {
                keyPressAttackTime = 0f;
                isAttackWaiting = true;
                moveStance.SetBool("checkAttack", true);
            }
        }
        if (keyPressAttackTime >= keyPressAttack)//공격키가 눌린 시간이 지정한 시간보다 크다면
        {
            moveStance.SetBool("checkAttack", false);
            isAttackWaiting = false;
        }//꾹누르면 너무 빠르고 많이 공격이 되서 시간 제한을 두었음

    }
    private void PlayerAttackEnd() //애니메이션에서 사용할 애니메이션 종료 메소드
    {
        moveStance.SetBool("checkAttack", false);

    }

    private void AttackEffect() //애니메이션에서 사용할 애니메이션 종료 메소드
    {
        AttackObject.SetActive(true);
    }
}