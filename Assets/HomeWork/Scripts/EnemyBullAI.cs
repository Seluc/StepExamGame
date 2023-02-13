using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBullAI : MonoBehaviour {
  public Transform Target;
  public float speed = 1.5f;
  public float rushTriggerDistance = 6f;
  public UnityEvent AroundPlayerEvent;
  public Material NormalBody;
  public Material RushBody;
  public Animator ModelAnimator;
  public GameObject DropPrefab;
  public SkinnedMeshRenderer ModelMeshRenderer;

  private Rigidbody rigidBody;
  private Health health;

  private bool isDroppedPrefab;
  private bool isMoveAnimation = false;

  private bool isInRush = false;
  private bool canMove = true;
  private Vector3 lockedTarget;
  private Vector3 lockedDistance;

  private SkinnedMeshRenderer rend;
  [SerializeField] private Transform body;


  private void Awake() {
    rigidBody = GetComponent<Rigidbody>();
    health = GetComponent<Health>();

    rend = body.GetComponent<SkinnedMeshRenderer>();
  }

  void Start() {
    ModelAnimator.SetBool("Move", true);
  }

  void Update() {
    if (!health.IsAlive) {
      OnDeath();
      return;
    }

    var distToTarget = Vector3.Distance(transform.position, Target.position);

    if ((distToTarget <= rushTriggerDistance || isInRush) && canMove) {
      ModelAnimator.SetBool("Move", true); Rush();
    } else if (distToTarget > 2f && !isInRush && canMove) {
      ModelAnimator.SetBool("Move", true);
      Move();
      Rotate();
    } else {
      Rotate();
      Stop();
    }
  }

  private void OnDeath() {
    if (isDroppedPrefab == false) {
      Instantiate(DropPrefab, transform.position, Quaternion.identity);
      isDroppedPrefab = true;
    }
    ModelAnimator.SetTrigger("Die");
    StartCoroutine(SmoothClipping(0f, 1f, 1f));
  }

  private IEnumerator SmoothClipping(float start, float end, float duration) {
    var elapsed = 0f;
    var value = 0f;
    while (elapsed < duration) {
      value = Mathf.Lerp(start, end, elapsed / duration);
      elapsed += Time.deltaTime;
      SetMaterialsClipping(value);
      yield return null;
    }
    SetMaterialsClipping(1f);
  }

  private void SetMaterialsClipping(float value) {
    ModelMeshRenderer.material.SetFloat("_Clipping", value);
  }

  private IEnumerator RushCooldown() {
    rigidBody.isKinematic = false;
    rigidBody.useGravity = true;

    yield return new WaitForSeconds(5f);

    rigidBody.isKinematic = true;
    rigidBody.useGravity = false;
    canMove = true;
    ModelAnimator.SetBool("Move", true);
  }

  private void Rush() {
    if (!isInRush) {
      lockedTarget = (Target.position - transform.position).normalized;
      lockedDistance = Target.position;

      isInRush = true;
      rend.material = new Material(RushBody);
    }

    if (Vector3.Distance(transform.position, lockedDistance) > 0.1f) {
      transform.position += lockedTarget * (speed * 10) * Time.deltaTime;
    } else {
      isInRush = false;
      canMove = false;
      rend.material = new Material(NormalBody);
      ModelAnimator.SetBool("Move", false);
      StartCoroutine(RushCooldown());
    }
  }

  private void Move() {
    var dir = (Target.position - transform.position).normalized;
    transform.position += dir * speed * Time.deltaTime;
  }

  private void Rotate() {
    transform.LookAt(Target);
  }

  private void Stop() {
    AroundPlayerEvent?.Invoke();
  }
}
