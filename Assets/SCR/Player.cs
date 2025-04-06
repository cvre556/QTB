/*
using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    float hAxis;   //�������� ����
    float vAxis;
    bool wDown;
    bool jDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isJump;
    bool isDodge;
    bool isSwap;

    Vector3 moveVec;    //�� �� ���� ���ļ� �����
    Vector3 dodgeVec;

    Rigidbody rigid;

    Animator anim;
    GameObject nearObject;
    GameObject equipWeapon;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Interation();
        Swap();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse); //������Ʈ ���� -> ���� -> �߷�
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Dodge()
    {
        if (jDown &&moveVec != Vector3.zero && !isJump && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2; 
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f);   //�Լ��� �ð��� �Լ� ȣ��
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    void Swap()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3) && !isJump && !isDodge)  //OR����

        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Interation()
    {
        if(iDown && nearObject != null && !isJump && !isDodge)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);    //���� ������ �ִ� ������ ����
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;

        Debug.Log(nearObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}
*/
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    public float speed;     //ĳ���� �ӵ� ����
    public GameObject[] waepons; //���� �迭 ����
    public bool[] hasWaepons; //���� ���� ���� �迭 ����


    float hAxis;
    float vAxis;
    bool wDown;     //����Ʈ Ű�� ���ȴ��� Ȯ���ϴ� ���� ����]
    bool jDown;
    bool iDown;     //EŰ �Է�
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isJump;
    bool isDodge;
    bool isSwap;


    bool isSide; //�� �浹 ����
    Vector3 sideVec; //�� �浹 ���� ����



    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;

    GameObject nearObject; //��ó�� �ִ� ������Ʈ�� ������ ���� ����
    GameObject equipWaepon; //������ ���⸦ ������ ���� ����

    int equipWaeponIndex = -1; //������ ������ �ε��� ������ ���� ����
    //ù �ε��� ���� -1�� ������ �ظ��� �ε��� ���� 0�̰�, int�� �ʱⰪ��0���� �����ǹǷ� �ظӰ� ��� ������ ������ �߻��ϱ� �����̴�

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>(); //������ٵ� ������Ʈ ��������
        anim = GetComponentInChildren<Animator>(); //�ִϸ����� ������Ʈ ��������

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    /// <summary>
    /// ������Ʈ ����
    /// </summary>
    void Update()
    {

        GetInput(); //�Է°��� �޾ƿ��� �Լ� ȣ��

        Move();
        //�̵��ϴ� �Լ� ȣ��

        PlayerTurn(); //ĳ���Ͱ� �ٶ󺸴� ������ �ٲ��ִ� �Լ� ȣ��
        Jump(); //�����ϴ� �Լ� ȣ��
        Dodge();
        Interaction();
        Swap();


    }


    /// <summary>
    /// �÷��̾��� Ű���� �Է��� �޾ƿ��� �Լ�
    /// </summary>
    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        wDown = Input.GetButton("Walk"); //����ƮŰ�� ���ȴ��� Ȯ���ϴ� ������ ����
        jDown = Input.GetButtonDown("Jump"); //����Ű�� ���ȴ��� Ȯ���ϴ� ������ ����
        iDown = Input.GetButtonDown("Interaction"); //����Ű�� ���ȴ��� Ȯ���ϴ� ������ ����
        sDown1 = Input.GetButtonDown("Swap1"); //����1����
        sDown2 = Input.GetButtonDown("Swap2"); //����2����
        sDown3 = Input.GetButtonDown("Swap3"); //����3����
    }


    /// <summary>
    /// ĳ���͸� �̵���Ű�� �Լ� 
    /// </summary>
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            dodgeVec = moveVec;

        if (isSwap)
            moveVec = Vector3.zero; //���� �߿��� �̵��� ���� ����

        //�浹�ϴ� ������ ����
        if (isSide && moveVec == sideVec)
            moveVec = Vector3.zero;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }


    /// <summary>
    /// ĳ���Ͱ� �ٶ󺸴� ������ �̵� �������� �ٲ��ִ� �Լ�
    /// </summary>
    void PlayerTurn()
    {
        transform.LookAt(transform.position + moveVec); //ĳ���Ͱ� �ٶ󺸴� ������ �ٲ���
    }

    /// <summary>
    /// �������� �� �����̽��ٸ� ������ �����ϴ� �Լ�
    /// </summary>
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)        //�������� �ʰ� ���� �� �����̽� �ٸ� ������
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse); //�������� ���� ������ (����)_ , ForceMode.Impulse�� ���������� ���� �����ִ� ��
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true; //������ �ߴٰ� ǥ��
        }

    }

    /// <summary>
    /// �����̰� �ִ� ���¿���, �����̽��ٸ� ������ �� �⺻ �̼��� 2��� ������ �ϴ� �Լ�
    /// </summary>
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);

        }

    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    /// <summary>
    /// ���⸦ ��ü�ϴ� �Լ� , �ߺ� ���� ��ü������ ������ ���� ��ü ������ ���ִ� �Լ�
    /// </summary>
    void Swap()
    {

        if (sDown1 && (!hasWaepons[0] || equipWaeponIndex == 0)) return;
        if (sDown2 && (!hasWaepons[1] || equipWaeponIndex == 1)) return;
        if (sDown3 && (!hasWaepons[2] || equipWaeponIndex == 2)) return;
        //���⸦ ���������� �ʰų� ���� ������ ���⿡�� �Ȱ��� ����� �����Ϸ��� ��� �������� ����


        //�� ���� ��� = ���⸦ ������ ������, ���� ������ ����� �ٸ� ����� ������ ��
        int waeponIndex = -1;   //���� �ʱ�ȭ�ϰ�

        //�Է� ���� ���� ���ⵥ���� �ε��� ������
        if (sDown1) waeponIndex = 0;                //����1����
        if (sDown2) waeponIndex = 1;                //����1����
        if (sDown3) waeponIndex = 2;                //����1����

        if ((sDown1 || sDown2 || sDown3) && !isJump && !isDodge)
        {
            if (equipWaepon != null)                //������ ���Ⱑ ���� ��쿡��
            {
                equipWaepon.SetActive(false);       //������ ���⸦ ��Ȱ��ȭ��
            }

            equipWaeponIndex = waeponIndex;        //������ ������ �ε����� ������
            equipWaepon = waepons[waeponIndex];     //������ ���Ⱑ �������� ������ �Ŀ�
            waepons[waeponIndex].SetActive(true);   //������ ���⸦ Ȱ��ȭ��

            anim.SetTrigger("doSwap");              //���� ��ü �ִϸ��̼� ����

            isSwap = true;

            Invoke("SwapOut", 0.4f); //0.4�� �Ŀ� SwapOut() �Լ��� ������

        }


    }

    void SwapOut()
    {
        isSwap = false;
    }


    /// <summary>
    /// EŰ�� ������ �� �ֺ� �������� ������ ��� �������� hasWaepons �迭�� �����ϴ� �Լ�
    /// </summary>
    void Interaction()
    {
        if (iDown && nearObject != null && !isJump && !isDodge) //EŰ�� ������ �� �ֺ� �������� ���� ���
        {
            if (nearObject.tag == "Waepon")
            {
                Item item = nearObject.GetComponent<Item>(); //������ ������Ʈ�� ������
                int waeponIndex = item.value; //�������� ���� ������
                hasWaepons[waeponIndex] = true; //�������� �����ϰ� �ִٰ� ǥ��
                //��� : ������ �������� ������Ʈ �������� -> �� �������� ���� ������ ex)��ġ= 0 ���� = 1, ������ = 2.
                // -> �� ���� hasWaepons �迭�� �ּҸ� ���, �� �������� ������ �ִٰ� ǥ����.
                // hasWaepons[1] = true ---> ������ ������ �ִ�
                Destroy(nearObject); //�������� ����
            }

        }

    }


    /// <summary>
    /// �ٴڿ� ���� �ʾ����� �������� �ʱ� ���� �Լ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") //�ٴڿ� �������
        {
            anim.SetBool("isJump", false);
            isJump = false; //������ ���� �ʾҴٰ� ǥ��
        }
    }
    //�� �浹 In üũ
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = true;
            sideVec = moveVec;
        }
    }
    //�� �浹 Out üũ
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isSide = false;
            sideVec = Vector3.zero;
        }
    }

    /// <summary>
    /// ���� ������Ʈ�� ����� �� �� ���⸦ nearObject�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Waepon")
        {
            nearObject = other.gameObject; //��ó�� �ִ� ������Ʈ�� ����

            //Debug.Log(nearObject.name); //��ó�� �ִ� ������Ʈ�� �̸��� ��� (�����)
        }



    }

    /// <summary>
    /// ���� ������Ʈ���� ����� �� nearObject�� null�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Waepon")
        {
            nearObject = null; //��ó�� �ִ� ������Ʈ�� ����
        }
    }





}
