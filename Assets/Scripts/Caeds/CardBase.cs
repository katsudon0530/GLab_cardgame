using System;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] string Name;
    [SerializeField] SynthesisType synthesisType;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [SerializeField] Color color;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] CardEffect cardEffect;

    public CardType Type { get => type; set => type = value; }
    public int Number { get => number; set => number = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public string Description { get => description; set => description = value; }
    public string Name1 { get => Name; set => Name = value; }
    public Color Color { get => color; set => color = value; }
    public SynthesisType SynthesisType { get => synthesisType; set => synthesisType = value; }
    public CardEffect CardEffect { get => cardEffect; set => cardEffect = value; }
}

public enum CardType
{
    Sword,
    Witchcraft,
    Protection,
    Heal,
    Reset,
}

public enum SynthesisType
{
    Normal,
    Plus,
    DoublePlus,
}


//後ろのカードに与えるエフェクト
[Serializable]
public class CardEffect
{
    [SerializeField] float attack_Effect;
    [SerializeField] float magic_Effect;
    [SerializeField] float protection_Effect;
    [SerializeField] float heal_Effect;

    public float Attack_Effect { get => attack_Effect; set => attack_Effect = value; }
    public float Magic_Effect { get => magic_Effect; set => magic_Effect = value; }
    public float Protection_Effect { get => protection_Effect; set => protection_Effect = value; }
    public float Heal_Effect { get => heal_Effect; set => heal_Effect = value; }
}
