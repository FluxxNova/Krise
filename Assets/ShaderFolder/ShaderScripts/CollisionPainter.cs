using UnityEngine;

public class CollisionPainter : MonoBehaviour
{
    public Color paintColor;
    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;
    public float StopRadius = 1;
    public float MovingRadius = 1;
    private CharacterController _controller;

    public float speed = 1;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _controller.detectCollisions = true;
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("Estoy pintando");
        Paintable p = other.collider.GetComponent<Paintable>();

        

        if (p != null){
            Vector3 pos = other.contacts[0].point;
          
            PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
            
        }
        
    }

     

}
