using R3;
using UnityEngine;

public sealed class CharacterControlSwap : MonoBehaviour
{
    [SerializeField] private Character[] characters;

    private void Start()
    {
        foreach (var character in characters)
        {
            character.Setup();
            character.OnSwapObservable?
                .Subscribe(swap => OnSwap(swap.ownPanel, swap.otherPanel))
                .RegisterTo(destroyCancellationToken);
        }
    }

    private void OnSwap(CharacterPanel ownPanel, CharacterPanel otherPanel)
    {
        var ownCharacter = GetCharacter(ownPanel);
        var otherCharacter = GetCharacter(otherPanel);

        var ownType = ownCharacter.type;
        ownCharacter.ChangeType(otherCharacter.type);
        otherCharacter.ChangeType(ownType);
    }

    private Character GetCharacter(CharacterPanel panel)
    {
        foreach (var character in characters)
        {
            if (character.panel == panel)
            {
                return character;
            }
        }

        return null;
    }
}