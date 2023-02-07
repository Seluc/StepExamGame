using UnityEngine;

public class SpellBall : MonoBehaviour {
  public SpellBallCollisions ballCollisions;
  public float Damage = 25f;
  public float speed = 2f;
  public float radius = 2f;

  public Transform ball;

  private float _moveValue;

  private void OnEnable() {
    ballCollisions.CollisionWithEnemy += OnCollisionWithEnemy;
  }

  private void OnDisable() {
    ballCollisions.CollisionWithEnemy -= OnCollisionWithEnemy;
  }

  void Start() {

  }

  private void OnCollisionWithEnemy(Health hp) {
    hp.SetDamage(Damage);
  }

  // Update is called once per frame
  void Update() {
    _moveValue += Time.deltaTime * speed;

    var x = Mathf.Sin(_moveValue) * radius;
    var z = Mathf.Cos(_moveValue) * radius;

    ball.localPosition = new Vector3(x, ball.localPosition.y, z);
  }
}
