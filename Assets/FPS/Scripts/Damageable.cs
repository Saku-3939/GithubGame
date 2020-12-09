using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Tooltip("Multiplier to apply to the received damage")]
    public float damageMultiplier = 1f;
    [Range(0, 1)]
    [Tooltip("Multiplier to apply to self damage")]
    public float sensibilityToSelfdamage = 0.5f;

    public Health health { get; private set; }

    public static float totalPlayerTakenDamage;
    public static float totalPlayerDamage;

    void Awake()
    {
        
        // find the health component either at the same level, or higher in the hierarchy
        health = GetComponent<Health>();
        if (!health)
        {
            health = GetComponentInParent<Health>();
        }
    }

    public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
    {

        if(health)
        {
            var totalDamage = damage;

            if (damageSource == gameObject.CompareTag("Enemy"))
            {
                
                totalDamage +=  StatusSetManage.userTotalContribute * 3;
                totalPlayerDamage += totalDamage;
            }

            if(damageSource == gameObject.CompareTag("Player"))
            {
                totalPlayerTakenDamage += totalDamage;
            }

            // skip the crit multiplier if it's from an explosion
            if (!isExplosionDamage)
            {
                totalDamage *= damageMultiplier;

            }

            // potentially reduce damages if inflicted by self
            if (health.gameObject == damageSource)
            {
                totalDamage *= sensibilityToSelfdamage;
            }

            // apply the damages
            health.TakeDamage(totalDamage, damageSource);
        }
    }
}
