using UnityEngine;
using System.Collections;

public class ScroolParallax : MonoBehaviour {

    public Transform[] background;
    private float[] parallaxScales;
    public float smoothing;

    private Vector3 previousCamPos;


    // Use this for initialization
    void Start()
    {
        previousCamPos = transform.position;

        parallaxScales = new float[background.Length];
        for (int i = 0; i < parallaxScales.Length; i++)
        {
            parallaxScales[i] = background[i].position.z * -1;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < background.Length; i++)
        {
            Vector3 parrallax = (previousCamPos - transform.position) * (parallaxScales[i] / (smoothing * Time.deltaTime));

            background[i].position = new Vector3(background[i].position.x + parrallax.x, background[i].position.y + parrallax.y, background[i].position.z);
        }
        previousCamPos = transform.position;
    }
}
