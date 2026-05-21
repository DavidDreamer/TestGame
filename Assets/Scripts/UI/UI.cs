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

    public void Refresh(GameState gameState)
    {
        MetaScreen.gameObject.SetActive(gameState == GameState.Meta);
        VictoryScreen.gameObject.SetActive(gameState == GameState.Victory);
        DefeatScreen.gameObject.SetActive(gameState == GameState.Defeat);
        Health.gameObject.SetActive(gameState == GameState.Fight);
    }
}