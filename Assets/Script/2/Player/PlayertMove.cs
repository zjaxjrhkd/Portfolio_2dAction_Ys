using UnityEngine;


public class PlayertMove : MonoBehaviour
{
    enum Posture //ĳ������ ���¸� ��Ÿ���� enum ������ ���������� �������� �ʾҴ�.
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

    public float speed; //�ӵ� : Inspector�� ����
    public float run; //�ӵ� : Inspector�� ����

    private Rigidbody2D rb2d;

    public Animator moveStance;

    private bool isRunning = false;                     // ĳ���Ͱ� �޸��� �ִ��� ���θ� ��Ÿ���� ����
    private bool isWalking = false;                     // ĳ���Ͱ� �޸��� �ִ��� ���θ� ��Ÿ���� ����

    private bool isKeyPressed = false;                  // ����Ű�� �����ִ��� ���θ� ��Ÿ���� ����
    private float keyPressTime = 0f;                    // ����Ű�� ó�� ���� �ð��� �����ϴ� ����
    private float keyPressThreshold = 0.1f;             // ����Ű�� �� �� ���� �޸��� ���·� ��ȯ�ϴ� �ð� ����
    private float keyPressSpareTime = 0f;                    // ����Ű�� ó�� ���� �ð��� �����ϴ� ����
    private float keyPressSpare = 0.5f;
    private bool isWaiting = false;

    private bool isAttackWaiting = false;
    private float keyPressAttackTime = 0f;                    // ����Ű�� ó�� ���� �ð��� �����ϴ� ����
    private float keyPressAttack = 0.08f;

    public GameObject AttackObject; // Ȱ��ȭ�� ������Ʈ

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

        // ����Ű �Է� ����
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        bool shift = Input.GetKey(KeyCode.LeftShift);//�ȱ� ��ư

        if (shift==false)//�ȱ��ư�� �������� Ȯ��
        {   
            if (!isRunning)//���� �޸��� �������� Ȯ��
            {

                if (horizontalInput != 0 || verticalInput != 0)//����Ű�� ���������� Ȯ���ϰ�
                {
                    StartWalking();//�ȱ� �޼ҵ� ȣ��
                    keyPressTime = Time.time;           // ����Ű�� ó�� ���� �ð��� �����ϴ� ����         
                }
                else if (isWalking && !isKeyPressed && Time.time - keyPressTime <= keyPressThreshold)
                {//(�ȱ� �����̰�)(����Ű�� �������� �ʰ�), �޼ҵ尡 ���������� �ð����� Ű�� �������� �ð��� Ű�� ������ �ʾҴ� �ð��� ������ �ð����� ���ٸ� �޸��� ����
                    StartRunning();
                    moveStance.SetBool("checkRun", true);
                }
            }
            else if (isRunning)//�� if���� �޸��� ���¿��� �����·� ���� ����� �Ѵ�
            {
                if (horizontalInput == 0 && verticalInput == 0)//����Ű�� �������� �ʴٸ�
                {
                    if (!isWaiting)//�����°� �ƴ϶��
                    {
                        keyPressSpareTime = 0f; //����Ű�� ������ �ð��� �ʱ�ȭ
                        isWaiting = true; //�����·�
                    }
                    else
                    {
                        keyPressSpareTime += Time.deltaTime; //Ű�� ���ȴ� �ð��� ����
                        if (keyPressSpareTime >= keyPressSpare) //Ű�� ������ �ð����� ���ذ��� ���ų� �۴ٸ� 
                        {
                            StopMove();//�������� �����.
                            moveStance.SetBool("checkRun", false);//�޸��� �ִϸ��̼� bool���� ����
                            isWaiting = false;//�����¸� �����Ѵ�
                        }//�� if���� �̵���ư�� ������ �ִٸ� ������� ����
                    }
                }
            }
        }
        Vector2 movement = new Vector2(horizontalInput * speed, verticalInput * speed);
        rb2d.velocity = movement * run;
    }

    private void StartWalking()
    {
        if (!isRunning)  // �޸��� ���°� �ƴ� ��쿡�� �ȱ�� ��ȯ
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
        keyPressAttackTime += Time.deltaTime;//����Ű�� �����ִ� �ð� ����

        if (Input.GetKey(KeyCode.Z))
        {
            if (!isAttackWaiting)
            {
                keyPressAttackTime = 0f;
                isAttackWaiting = true;
                moveStance.SetBool("checkAttack", true);
            }
        }
        if (keyPressAttackTime >= keyPressAttack)//����Ű�� ���� �ð��� ������ �ð����� ũ�ٸ�
        {
            moveStance.SetBool("checkAttack", false);
            isAttackWaiting = false;
        }//�ڴ����� �ʹ� ������ ���� ������ �Ǽ� �ð� ������ �ξ���

    }
    private void PlayerAttackEnd() //�ִϸ��̼ǿ��� ����� �ִϸ��̼� ���� �޼ҵ�
    {
        moveStance.SetBool("checkAttack", false);

    }

    private void AttackEffect() //�ִϸ��̼ǿ��� ����� �ִϸ��̼� ���� �޼ҵ�
    {
        AttackObject.SetActive(true);
    }
}