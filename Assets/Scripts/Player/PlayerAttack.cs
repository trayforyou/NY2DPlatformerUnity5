using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerHand))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAttacks = 0.1f;
    [SerializeField] private LayerMask _enemiesLayer;

    private WaitForSeconds _wait;
    private WeaponAttack _weaponAttack;
    private PlayerInput _inputPlayer;
    private PlayerHand _playerHand;
    private Coroutine _coroutine;
    private bool _isReadyToAttack;
    private bool _canTakeDamage;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetweenAttacks);
        _inputPlayer = GetComponent<PlayerInput>();
        _playerHand = GetComponent<PlayerHand>();
        _canTakeDamage = true;
        _isReadyToAttack = true;

        _playerHand.ChangedWeapon += ChangeWeapon;
        _inputPlayer.Attacked += Attack;
    }

    private void OnDisable()
    {
        if(_coroutine != null)
            StopCoroutine( _coroutine );

        _playerHand.ChangedWeapon -= ChangeWeapon;
        _inputPlayer.Attacked -= Attack;
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _weaponAttack = weapon.GetWeaponAttack();

        _weaponAttack.SetNewTarget(_enemiesLayer);
    }

    private void Attack()
    {
        if (_isReadyToAttack && _weaponAttack != null)
            _coroutine = StartCoroutine(WaitNextAttack());
    }

    private IEnumerator WaitNextAttack()
    {
        _isReadyToAttack = false;

        _weaponAttack.Attack(_canTakeDamage);

        yield return _wait;

        _isReadyToAttack = true;

        _weaponAttack.Attack(!_canTakeDamage);

        yield break;
    }
}