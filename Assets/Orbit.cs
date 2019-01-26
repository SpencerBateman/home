using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
  GameObject Planet;
  public Transform center;
  public Vector3 axis;
  public Vector3 desiredPosition;
  public float radius;
  public float radiusSpeed;
  private float rotationSpeed;

  void Start () {
    Planet = gameObject.transform.parent.gameObject;
    center = Planet.transform;
    //transform.position = (transform.position - center.position).normalized * radius + center.position;
    radius = .7f;
    radiusSpeed = 10f;
    rotationSpeed = 10f;
    axis = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f,1.0f));
  }

  void Update () {
    transform.RotateAround (center.position, axis, rotationSpeed * Time.deltaTime);
    desiredPosition = (transform.position - center.position).normalized * radius + center.position;
    transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
  }
}
