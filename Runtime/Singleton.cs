using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	/// <summary>
	/// 
	/// </summary>
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this as T;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}

		OnAwake();
	}

	/// <summary>
	/// 
	/// </summary>
	protected virtual void OnAwake() { }
}