using UnityEngine;

[ExecuteInEditMode]
public class PostEffects: MonoBehaviour
{
	public Material PostMaterial;

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{

		Graphics.Blit(src, dst, PostMaterial);

	}
}
