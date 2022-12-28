using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<Material> faceMaterials = new List<Material>();

    public List<GameObject> noseList = new List<GameObject>();
    public List<GameObject> leftList = new List<GameObject>();
    public List<GameObject> rightList = new List<GameObject>();

    MeshRenderer faceRenderer;

    int curFaceIndex = 0;
    int curNoseIndex = 0;

    public void SwitchFaceUp()
    {
        curFaceIndex = (curFaceIndex + 1) % faceMaterials.Count;
        faceRenderer.materials[0] = faceMaterials[curFaceIndex];
    }

    public void SwitchFaceDown()
    {
        curFaceIndex = (curFaceIndex - 1) % faceMaterials.Count;
        faceRenderer.materials[0] = faceMaterials[curFaceIndex];
    }


    public void SwitchAcc()
    {
        foreach (var obj in noseList)
            obj.SetActive(false);
        foreach (var obj in leftList)
            obj.SetActive(false);
        foreach (var obj in rightList)
            obj.SetActive(false);

        curNoseIndex = (curNoseIndex + 1) % faceMaterials.Count;
        noseList[curNoseIndex].SetActive(true);
        leftList[curNoseIndex].SetActive(true);
        rightList[curNoseIndex].SetActive(true);

    }
}
