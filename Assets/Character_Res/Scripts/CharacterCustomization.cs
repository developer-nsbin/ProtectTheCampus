using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCustomization : Singleton<CharacterCustomization>
{
    SkinnedMeshRenderer target;
    SkinnedMeshRenderer skm;
    Dictionary<string, BlendShape> BlendShapesList = new Dictionary<string, BlendShape>();

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        target= GameObject.Find("Man/char_ethan_body").GetComponent<SkinnedMeshRenderer>();
        skm = target;
        SaveBlendShapesList();
    }

    void SaveBlendShapesList()
    {
        BlendShapesList.Add("Ear_Min",new BlendShape(0));
        BlendShapesList.Add("Ear_Max",new BlendShape(1));
        BlendShapesList.Add("Head_Max",new BlendShape(2));
    }

    public void ChangeBlendShapeValue(string blendShapeName, float value)
    {
        if (!BlendShapesList.ContainsKey(blendShapeName))
        {
            Debug.LogError(blendShapeName + "不存在");
            return;
        }

        BlendShape blendshape = BlendShapesList[blendShapeName];
        value = Mathf.Clamp(value, -100, 100);
        skm.SetBlendShapeWeight(blendshape.Index, value);
    }
}

public struct BlendShape
{
    public int Index { get; set; }

    public BlendShape(int index)
    {
        Index = index;
    }
}
