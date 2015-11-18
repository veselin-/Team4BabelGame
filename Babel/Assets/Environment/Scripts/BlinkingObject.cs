using System.Linq;
using UnityEngine;

namespace Assets.Environment.Scripts
{
    public class BlinkingObject : MonoBehaviour
    {

        private Material[] _materials;

        // Use this for initialization
        void Start () {
            _materials = GetComponentsInChildren<Renderer>().Select(r => r.material).ToArray();
            foreach (var mat in _materials)
                mat.EnableKeyword("_EMISSION");
        }
	
        // Update is called once per frame
        void Update ()
        {
            foreach (var mat in _materials)
                mat.SetColor("_EmissionColor", Color.Lerp(Color.black,
                    new Color(0.3f, 0.3f, 0.3f), Mathf.PingPong(Time.time, 1)));
        }
    }
}
