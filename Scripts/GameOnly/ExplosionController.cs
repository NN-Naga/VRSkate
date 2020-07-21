using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject currentDetonator;
    public float explosionLife = 10;
    public float detailLevel = 1.0f;
    private GameObject bomb;

    private void Start()
    {
        bomb = transform.Find("Creeper_Head_Waluigi_SwissyWAH_5Gt1_dGDIEY(Clone)").gameObject;
    }

    public IEnumerator Indicate()
    {
        GameObject bombTexture = transform.Find("Creeper_Head_Waluigi_SwissyWAH_5Gt1_dGDIEY(Clone)/node_MeshObject236085248-PolyPaper18").gameObject;
        Color defColor = bombTexture.GetComponent<Renderer>().material.color;
        bool colorSet = true;

        while (bomb)
        {
            if (colorSet)
                bombTexture.GetComponent<Renderer>().material.color = Color.Lerp(defColor, new Color(255, 0, 0), 0.5f);
            else
                bombTexture.GetComponent<Renderer>().material.color = defColor;

            colorSet = !colorSet;
            yield return new WaitForSeconds(.2f);
        }
    }

    public IEnumerator Explode()
    {
        SpawnExplosion();
        Destroy(bomb);
        yield return new WaitForSeconds(2);
        Destroy(this);
    }

    private void SpawnExplosion()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");

            float offsetSize = dTemp.size / 3;
            Vector3 hitPoint = hit.point +
                                      ((Vector3.Scale(hit.normal, new Vector3(offsetSize, offsetSize, offsetSize))));
            GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);
            dTemp = (Detonator)exp.GetComponent("Detonator");
            dTemp.detail = detailLevel;

            Destroy(exp, explosionLife);
        }


    }
}
