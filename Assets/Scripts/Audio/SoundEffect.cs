using UnityEngine;

[CreateAssetMenu(fileName = "Sound Effect", menuName = "Sound Effect")]
public class SoundEffect : ScriptableObject
{
	public AudioClip audioClip;
	public float volume;
}
