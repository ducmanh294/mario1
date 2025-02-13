using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    private float TocDo = 0;
    private bool DuoiDat = true;
    private bool ChuyenHuong = false;
    private bool QuayPhai = true;
    public float VanToc;
    public float VanTocToiDa = 11f;
    public float NhayCao;
    public float RoiXuong;
    public float NhatThap;
    private float TGGiuPhim = 0.2f;
    private float KTGiuPhim = 0;
    private Vector2 ViTriChet;

    public int CapDo = 0;
    public bool BienHinh = false;
    private Rigidbody2D r2d;
    private Animator HoatHoa;
    public int score = 0;

    private AudioSource AmThanh;


    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        HoatHoa = GetComponent<Animator>();
        AmThanh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        BanDanVaTangToc();
        if (BienHinh==true)
        {
            switch (CapDo)
            {
                case 0:
                    {
                        StartCoroutine(MarioThuNho());
                        BienHinh=false;
                        TaoAmThanh("smb_powerup_appears");
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioAnNam());
                        BienHinh=false;
                        TaoAmThanh("smb_powerup");
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioAnHoa());
                        BienHinh=false;
                        TaoAmThanh("smb_powerup");
                        break;
                    }
                default: BienHinh=false; break;
            }
        }

        if (gameObject.transform.position.y < -7.1f)
        {
            TaoAmThanh("smb_mariodie");
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        DiChuyen();
    }

    void DiChuyen()
    {
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc*PhimNhanPhaiTrai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc*PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMatMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMatMario();
    }

    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x*=-1;
        transform.localScale = HuongQuay;
        if (TocDo > 0) StartCoroutine(MarioChuyenHuong());
    }

    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.X) && DuoiDat == true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
            TaoAmThanh("smb_jump-super");
        }


        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong-1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.X))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhatThap-1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }

    void BanDanVaTangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TGGiuPhim = Time.deltaTime;
            if (TGGiuPhim < KTGiuPhim)
            {

            }
            else
            {
                VanToc = VanToc * 1.5f;
                if (VanToc>VanTocToiDa) VanToc=VanTocToiDa;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            VanToc=7f;
            TGGiuPhim=0;
        }

    }

    IEnumerator MarioAnHoa()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }

    IEnumerator MarioThuNho()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);

    }

    IEnumerator MarioAnNam()
    {
        float DoTre = 0.1f;
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioNho"), 0);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("MarioLon"), 1);
        HoatHoa.SetLayerWeight(HoatHoa.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);

    }

    public void TaoAmThanh(string FileAmThanh)
    {
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Audio/"+FileAmThanh));
    }

    public void MarioChet()
    {
        ViTriChet = transform.localPosition;
        GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
        MarioChet.transform.localPosition = ViTriChet;
        Destroy(gameObject);
       
    }
}
