using UnityEngine;
using System.Collections;
using dnSR_Coding.Utilities;
using NaughtyAttributes;
using System.Collections.Generic;
using Unity.Mathematics;

namespace dnSR_Coding
{
    ///<summary> FoliageShaderSupport description <summary>
    [Component("FoliageShaderSupport", "Helps the foliage shader to perform well by finding the shape normals")]
    public class FoliageShaderSupport : MonoBehaviour
    {
        [Tooltip( "Shape normals will point away from the closest position in this list" )]
        [SerializeField] private List<Transform> _normalFoci;
        [Tooltip( "Store wind data in this TEXCOORD" )]
        [SerializeField] private int _storeWindDataInTexCoord = 3;

        private void Run()
        {
            // Grab the mesh and its data
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            var positions = mesh.vertices;
            // Instantiate final data arrays
            var colors = new Color [ positions.Length ];
            var windData = new Vector4 [ positions.Length ];

            // Create bounds object to find mesh world space bounds
            Bounds bounds = new Bounds();
            // Convert to world space. This is OK since this mesh is static
            for ( int vertex = 0; vertex < positions.Length; vertex++ )
            {
                positions [ vertex ] = transform.TransformPoint( positions [ vertex ] );
                bounds.Encapsulate( positions [ vertex ] );
            }

            float3 boundsCenter = bounds.center;
            float farthestDistanceFromCenter = math.length( bounds.extents );

            for ( int vertex = 0; vertex < positions.Length; vertex++ )
            {
                // Find the closest focus to the vertex. Then, calculate the distance to it
                float3 focusPosn = FindClosestNormalFocus( positions [ vertex ] );
                float3 fromFocusToVertex = ( float3 ) positions [ vertex ] - focusPosn;
                float distanceFromFocusToVertex = math.length( fromFocusToVertex );
                // Set the wind data value. XYZ = wind noise "anchor" position. W = wind dampening multiplier
                // For now, wind is stronger farther from a focus, but this can be changed
                float windStrength = math.saturate( distanceFromFocusToVertex / farthestDistanceFromCenter );
                windData [ vertex ] = new Vector4( boundsCenter.x, boundsCenter.y, boundsCenter.z, windStrength );
                // Normalize the vector pointing from the nearest normal focii to this vertex
                float3 normal = math.normalize( fromFocusToVertex / distanceFromFocusToVertex );
                // And store that as the shape normal
                colors [ vertex ] = new Color( normal.x, normal.y, normal.z, 0 );
            }

            // Store wind data in a texcoord
            mesh.SetUVs( _storeWindDataInTexCoord, windData );
            // Store shape normals in the vertex color data
            mesh.SetColors( colors );
        }

        // This function receives a position and returned the position of the closest
        // normal center transform
        private float3 FindClosestNormalFocus( float3 pos )
        {
            int closestID = 0;
            float closestDistanceSq = float.MaxValue;
            for ( int i = 0; i < _normalFoci.Count; i++ )
            {
                float distanceSq = math.distancesq( pos, _normalFoci [ i ].position );
                if ( distanceSq < closestDistanceSq )
                {
                    closestID = i;
                    closestDistanceSq = distanceSq;
                }
            }
            return _normalFoci [ closestID ].position;
        }

    }
}