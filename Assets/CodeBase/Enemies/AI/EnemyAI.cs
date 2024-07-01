using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _playerMask;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) > _agent.stoppingDistance + _agent.radius)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetBool("IsAttacking", false);
        }
        else
        {
            _animator.SetBool("IsAttacking", true);
            _animator.SetBool("IsWalking", false);
        }

        _agent.SetDestination(_target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Sword tool))
        {
            _health.ApplyDamage(1);

            if (gameObject.activeSelf)
                StartCoroutine(KnocningBack(tool.transform.position));
        }
    }

    private IEnumerator KnocningBack(Vector3 from)
    {
        int directionX = transform.position.x < from.x ? -1 : 1;
        Vector3 firstPoint = transform.position + new Vector3(directionX * 2.2f, 0);
        Vector3 secondPoint = transform.position + new Vector3(directionX * 1.7f, 2);
        Vector3 thirdPoint = transform.position + new Vector3(directionX * 1.2f, 3);
        Vector3 fourthPoint = transform.position;
        float time = 1; // 0 - 1
        float speed = 2;

        while (time > 0)
        {
            transform.position = Bezier.GetPoint(firstPoint, secondPoint, thirdPoint, fourthPoint, time);

            time -= Time.deltaTime * speed;

            yield return null;
        }
    }
}
