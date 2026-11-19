using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField, Header("敵を倒した数")]public Text _killEnemyCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HP(Image hpimage, float hp, float maxhp)
    {
        hpimage.fillAmount = hp / maxhp;
    }
    public void EnemyDisabled(int count)
    {
        _killEnemyCounter.text = count.ToString("000");
    }
}
