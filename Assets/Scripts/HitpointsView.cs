using UnityEngine;
using UnityEngine.UI;

public class HitpointsView : MonoBehaviour
{
	public RectTransform TimeContainer;
	public RectTransform Line2;
	public RectTransform Container;
	public RectTransform Line;
	public Text PlayerName;


	public void SetValue(string type, float value)
	{
		switch (type)
		{
			case "hp":
				SetHealthValue(value);
				break;
			case "time":
				SetTimeValue(value);
				break;
		}
	}
	
	private void SetHealthValue(float value)
	{
		Vector2 v = Container.sizeDelta;
		v.x = Mathf.Max(0.0f, v.x * value);
		Line.sizeDelta = v;
	}

	private void SetTimeValue(float value)
	{
		Vector2 v = TimeContainer.sizeDelta;
		v.x = Mathf.Max(0.0f, v.x * value);
		Line2.sizeDelta = v;
	}
}