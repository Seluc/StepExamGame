using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace lesson2 {
  [RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
  public class EnemyAI : MonoBehaviour {
    public Transform Target;
    public float speed = 250f;
    public float jumpForce = 200f;
    public UnityEvent AroundPlayerEvent;
    public SkinnedMeshRenderer[] ModelMeshRenderer;
    public Animator ModelAnimator;

    public GameObject DropPrefab;

    [Header("Sound effects")]
    public AudioClip JumpClip;

    private AudioSource _audioSource;

    private bool _isDroppedPrefab;

    private List<Material> _modelMaterials;
    private Rigidbody _rigidbody;
    private Health _health;



    void Awake() {
      _health = GetComponent<Health>();
      _audioSource = GetComponent<AudioSource>();

      _modelMaterials = new List<Material>();
      foreach (var renderer in ModelMeshRenderer) {
        _modelMaterials.Add(renderer.material);
      }

      _rigidbody = GetComponent<Rigidbody>();

      StartCoroutine(Jump());
    }


    private void FixedUpdate() {
      if (_health.IsAlive == false) {
        OnDeath();
        return;
      }

      var distToPlayer = Vector3.Distance(transform.position, Target.position);

      if (distToPlayer > 2f) {
        Move();
        Rotate();
      } else {
        Rotate();
        Stop();
      }
    }

    private void OnDeath() {
      if (_isDroppedPrefab == false) {
        Instantiate(DropPrefab, transform.position, Quaternion.identity);
        _isDroppedPrefab = true;
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
      foreach (var material in _modelMaterials) {
        material.SetFloat("_Clipping", value);
      }
    }


    private IEnumerator Jump() {
      while (_health.IsAlive) {
        _rigidbody.AddForce(transform.up * jumpForce);
        ModelAnimator.SetTrigger("Jump");
        PlayJumpSound();
        yield return new WaitForSeconds(1f);
      }
    }

    private void PlayJumpSound() {
      _audioSource.clip = JumpClip;
      if (_audioSource.isPlaying == false) {
        _audioSource.Play();
      }
    }

    private void Stop() {
      AroundPlayerEvent?.Invoke();
    }

    private void Move() {
      var dir = (Target.position - transform.position).normalized;
      _rigidbody.AddForce(dir * speed * Time.deltaTime);
    }

    private void Rotate() {
      transform.LookAt(Target);
    }
  }


}
