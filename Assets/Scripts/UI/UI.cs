using UnityEngine;

public class UI : MonoBehaviour
{
    [field: SerializeField]
    public MetaScreen MetaScreen { get; set; }

    [field: SerializeField]
    public VictoryScreen VictoryScreen { get; set; }

    [field: SerializeField]
    public DefeatScreen DefeatScreen { get; set; }

    [field: SerializeField]
    public HealthWidget Health { get; set; }

    //2 hardcoded abilities, could've been created in runtime based on character config
    [field: SerializeField]
    public AbilityWidget Ability1 { get; set; }

    [field: SerializeField]
    public AbilityWidget Ability2 { get; set; }

    public void Refresh(GameState gameState)
    {
        MetaScreen.gameObject.SetActive(gameState == GameState.Meta);
        VictoryScreen.gameObject.SetActive(gameState == GameState.Victory);
        DefeatScreen.gameObject.SetActive(gameState == GameState.Defeat);
        Health.gameObject.SetActive(gameState == GameState.Fight);
        Ability1.gameObject.SetActive(gameState == GameState.Fight);
        Ability2.gameObject.SetActive(gameState == GameState.Fight);
    }

    public void Bind(Character character)
    {
        Health.Bind(character.Health);

        Ability1.Bind(character.Abilities[0]);
        Ability2.Bind(character.Abilities[1]);
    }

    public void Tick()
    {
        Health.Tick();
        Ability1.Tick();
        Ability2.Tick();
    }
}