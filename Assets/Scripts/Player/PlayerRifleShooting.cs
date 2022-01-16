using UnityEngine;
using TMPro;

using UnityEngine.UI;

public class PlayerRifleShooting : MonoBehaviour
{

    public int dealDamage = 10;
    public float range = 20f;
    public float hitRadius = 2f;
    public float hitForce = 500f;
    public GameObject rifle;
    private Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitWallEffect;

    public static int RIFLE_MAX_AMMO = 30;
    private int ammoLeft;
    public TMP_Text ammoText;

    void Start()
    {
        fpsCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!rifle.activeSelf || ammoLeft <= 0)
        {
            return;
        }
        ammoLeft--;
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag != "Allies" && !hit.transform.tag.Contains("Enemy") && hit.transform.tag != "Ground")
            {
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                var hitBox = hit.collider.GetComponent<HitBox>();
                if (rb)
                {
                    rb.AddForce(-hit.normal * hitForce);
                }

            }
            else if (hit.transform.tag.Contains("Enemy"))
            {
                hitEnemy(hit.transform.gameObject);
            } 
            else if (hit.transform.tag == "Ground")
            {
                hitWallEffect.transform.position = hit.point;
                hitWallEffect.transform.forward = hit.normal;
                hitWallEffect.Emit(1);
            }
        }
        UpdateAmmoTextCanvas();
        AudioManager.Instance.Play("Rifle");
    }

    public void SetAmmo(int amount)
    {
        ammoLeft = amount;
        UpdateAmmoTextCanvas();
    }

   public void UpdateAmmoTextCanvas()
    {
        ammoText.text = ammoLeft.ToString();
    }
    public void hitEnemy(GameObject enemy)
    {
        enemy.GetComponent<Health>().TakeDamage(dealDamage, (this.transform.position - enemy.transform.position));
    }
}
