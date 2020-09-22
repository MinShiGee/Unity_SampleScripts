using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private string gunName = "";
    [Header("Gun stat")]
    [SerializeField] [Range(0,500f)] private float gunDamage = 10f;
    public float GetGunDamage() { return gunDamage; }

    [SerializeField] [Range(0, 5f)] private float shotSpeed = 0.1f;
    [SerializeField] [Range(0, 5f)] private float reloadSpeed = 2.5f;

    [SerializeField] [Range(0, 30f)] private float bulletSpeed = 1;
    [SerializeField] [Range(0, 200)] private int maxCurBulletAmount = 30;
    [SerializeField] [Range(0, 100)] private int magazineAmount = 3;
    [SerializeField] [Range(0, 10f)] private float bulletDrag = 5f;

    [Header("Bullet Storage")]
    [SerializeField] private Transform bulletStorage = default;

    [Header("Bullet Setting")]
    [SerializeField] private GameObject bulletPrefeb;
    [SerializeField] private Bullet[] bullets;

    [Header("Check")]
    [SerializeField] private Transform bulletStartPosObject = default;
    [SerializeField] private int curBulletAmount = 0;
    [SerializeField] private int curAllBulletAmount = 0;
    [SerializeField] private Vector3 shotStartPosition = default;
    [SerializeField] private float shotTimer = 1f;
    [SerializeField] private float reloadTimer = 0f;
    [SerializeField] private bool isReloading = false;

    private void Start()
    {
        GunInit();    
    }
    private void GunInit()
    {

        curBulletAmount = maxCurBulletAmount;
        curAllBulletAmount = maxCurBulletAmount * magazineAmount;

        bullets = new Bullet[maxCurBulletAmount];

        for(int i = 0; i < maxCurBulletAmount; i++)
        {
            bullets[i] = CreateNewBullet();
        }

    }
    private Bullet CreateNewBullet()
    {
        Bullet tempBullet = Instantiate(bulletPrefeb, shotStartPosition, transform.rotation, bulletStorage).GetComponent<Bullet>();
        tempBullet.gameObject.SetActive(false);
        return tempBullet;
    }

    // Update is called once per frame
    void Update()
    {
        ReloadUpdate();
        ShotUpdate();
    }
    private void ShotUpdate()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) == true)
        {
            if (!isReloading && shotTimer >= shotSpeed)
            {
                if (curBulletAmount > 0)
                {
                    Shot();
                }
                else
                {
                    // Just Tick Sound
                }

            }
        }
    }
    private void ReloadUpdate()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadSpeed)
            {
                if(curAllBulletAmount - (maxCurBulletAmount - curBulletAmount) >= 0)
                {
                    curAllBulletAmount -= (maxCurBulletAmount - curBulletAmount);
                    curBulletAmount = maxCurBulletAmount;
                }
                else
                {
                    curBulletAmount += curAllBulletAmount;
                    curAllBulletAmount = 0;
                }
                
                reloadTimer = 0f;
                isReloading = false;
            }
        }
        if (!isReloading && Input.GetKeyDown(KeyCode.R))
        {
            if (curBulletAmount < maxCurBulletAmount && curAllBulletAmount > 0)
            {
                isReloading = true;
            }
        }
    }
    private void Shot()
    {
        shotTimer = 0f;
        curBulletAmount--;

        Bullet bul = bullets[curBulletAmount];
        bul.gameObject.SetActive(true);
        ShotInit(bul);
    }
    private void ShotInit(Bullet bul)
    {
        bul.transform.position = bulletStartPosObject.position + transform.forward * 0.5f;
        bul.bulRig.velocity = transform.forward * bulletSpeed * 30f;
        bul.bulRig.drag = bulletDrag;
        bul.damage = gunDamage;
        bul.Shot(reloadSpeed);
    }
}
