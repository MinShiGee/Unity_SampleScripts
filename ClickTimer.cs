using UnityEngine;

public class ClickTimer : MonoBehaviour
{

    //[SerializeField] private GameObject Sample = default;
    [SerializeField] private float clickTime = 0f;
    [SerializeField] private bool isClick = false;

    // Update is called once per frame
    void Update() 
    {
        Timer();
    }
    void Timer()
    {
        text.text = clickTime.ToString();
        if (Input.GetMouseButtonUp(0))
        {
            //Shoot(clickTime);
            clickTimeInit();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
        }
        if (isClick)
            clickTime += Time.deltaTime;

    }
    void clickTimeInit()
    {
        clickTime = 0;
        isClick = false;
    }
    /*void Shoot(float power)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (col.Raycast(ray, out hit, 600) && Input.GetMouseButtonUp(0))
        {
            GameObject sample = Instantiate(Sample, hit.point, Sample.transform.rotation);
            SampleTest st = sample.GetComponent<SampleTest>();
            st.Shoot(power);
        }
    }*/
    
}
