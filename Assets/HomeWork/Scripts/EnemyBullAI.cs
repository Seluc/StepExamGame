using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBullAI : MonoBehaviour {
  public Transform Target;
  public float speed = 1.5f;
  public float rushTriggerDistance = 6f;
  public UnityEvent AroundPlayerEvent;
  public Material NormalHead;
  public Material RushHead;

  private Rigidbody rigidbody;
  private Health health;

  private bool isInRush = false;
  private bool canMove = true;
  private Vector3 lockedTarget;
  private Vector3 lockedDistance;

  private MeshRenderer rend;
  [SerializeField] private Transform head;


  void Start() {
    rigidbody = GetComponent<Rigidbody>();
    health = GetComponent<Health>();

    rend = head.GetComponent<MeshRenderer>();
  }

  private void OnDeath() { 
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180f));
  }

  void Update() {
    if (!health.IsAlive) {
      OnDeath();
      return;
    }

    var distToTarget = Vector3.Distance(transform.position, Target.position);

    if ((distToTarget <= rushTriggerDistance || isInRush) && canMove) {
      Rush();
    } else if (distToTarget > 2f && !isInRush && canMove) {
      Move();
      Rotate();
    } else {
      Rotate();
      Stop();
    }
  }

  private void Move() {
    var dir = (Target.position - transform.position).normalized;
    //_rigidbody.AddForce(dir * speed * Time.deltaTime);
    transform.position += dir * speed * Time.deltaTime;
  }

  private void Rotate() {
    transform.LookAt(Target);
  }

  private void Stop() {
    AroundPlayerEvent?.Invoke();
  }

  private IEnumerator RushCooldown() {
    rigidbody.isKinematic = false;
    rigidbody.useGravity = true;

    yield return new WaitForSeconds(5f);

    rigidbody.isKinematic = true;
    rigidbody.useGravity = false;
    canMove = true;
  }

  private void Rush() {
    if (!isInRush) {
      lockedTarget = (Target.position - transform.position).normalized;
      lockedDistance = Target.position;

      isInRush = true;
      rend.material = RushHead;
    }

    if (Vector3.Distance(transform.position, lockedDistance) > 0.1f) {
      transform.position += lockedTarget * (speed * 10) * Time.deltaTime;
    } else {
      isInRush = false;
      canMove = false;
      rend.material = NormalHead;
      StartCoroutine(RushCooldown());
    }
  }
}
