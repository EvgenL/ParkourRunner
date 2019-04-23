using PathMagic.Scripts.Meshes;
using UnityEngine;

namespace ParkourRunner.Scripts.Track
{
    [ExecuteInEditMode]
    public class TrolleyPathSetter : MonoBehaviour
    {
        public Transform Point1;
        public Transform Point2;

        public PathMagic.Scripts.PathMagic Path;
            
        void Update ()
        {
            if (Point1 != null && Point2 != null && Path != null)
            {
                Path.transform.position = Point1.position;
                Path.transform.rotation = Point1.rotation;

                Path.waypoints[0].position = Vector3.zero;
                //Создаёт эффект прогнувшийся верёвки под свой тяжестью. Значения подобраны экспериментально. Не трогать.
                Path.waypoints[0].InTangent = -Point1.transform.forward * 3 + Point1.transform.up * 2; 


                Path.waypoints[1].position = Point2.position - Point1.position;
                Path.waypoints[1].InTangent = -Point1.transform.forward * 3 + Point1.transform.up * -1;

                Path.GetComponent<PathMagicMesh>().Generate();
            }
        }
    }
}
