using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOffset : MonoBehaviour
{
    private Vector3 offsetVec;
    private GameObject camFollow;
    [SerializeField] private float speed = 3.0f;

    private void Start() {
        camFollow = Camera.main.gameObject;
        offsetVec = transform.position = camFollow.transform.position;
    }

    private void Update() {
        transform.position = camFollow.transform.position + offsetVec;
        transform.rotation = Quaternion.Slerp(transform.rotation, camFollow.transform.rotation, speed * Time.deltaTime);
    }
}
