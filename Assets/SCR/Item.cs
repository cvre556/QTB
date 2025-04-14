using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type{ Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rigid;
    SphereCollider sphereCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); //GetComponent() �Լ��� ù��° ������Ʈ�� ������
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }   // Open Prefab�Ͽ� Move up���� ���� ȿ���� ����ϴ� �ݶ��̴��� ���� �ø���
}
