using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
	[SerializeField] private List<StatusEffect> statusEffects;

	private void Update()
	{
		statusEffects.RemoveAll(statusEffect => statusEffect.duration <= 0);

		statusEffects.ForEach(statusEffects =>
		{
			statusEffects.duration -= Time.deltaTime;
		});
	}

	public void AddStatusEffect(StatusEffect statusEffect)
	{
		statusEffects.Add(statusEffect);
	}
}