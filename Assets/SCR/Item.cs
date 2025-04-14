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
        rigid = GetComponent<Rigidbody>(); //GetComponent() 함수는 첫번째 컴포넌트를 가져옴
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
    }   // Open Prefab하여 Move up으로 물리 효과를 담당하는 콜라이더를 위로 올리기
}
