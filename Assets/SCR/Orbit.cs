using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;
    Vector3 offSet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offSet = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offSet;
        transform.RotateAround(target.position,
                               Vector3.up,
                               orbitSpeed * Time.deltaTime);
        offSet = transform.position - target.position;
    }
}
