using UnityEngine;

[CreateAssetMenu(
    fileName = "ElementData",
    menuName = "ElementalTetris/Element"
)]
public class ElementData : ScriptableObject
{
    [Header("Šî–{î•ñ")]
    public string elementName;

    [Header("Œ©‚½–Ú")]
    public Color elementColor;
}
