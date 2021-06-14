using UnityEngine;
using Mirror;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("Pas de camera renseigner sur le systeme de tir.");
            this.enabled = false;   //Desactive le script PlayerShoot sur le joueur.
        }

        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {

        currentWeapon = weaponManager.GetCurrentWeapon();

        if(Input.GetButtonDown("Fire1"))    //detection du clique gauche.
        {
            Shoot();
        }
    }

    [Client]    //uniquement appeler par le client.
    private void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, mask))
        {
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name, currentWeapon.damage);
            }
        }
    }

    [Command]   //sur le client vers le serveur.
    private void CmdPlayerShot(string playerId, float damage)
    {
        Debug.Log(playerId + " a été touché.");

        Player player = GameManager.GetPlayer(playerId);    //on recup le scrit Player du joeuur toucher;
        player.RpcTakeDamage(damage);

    }

}
