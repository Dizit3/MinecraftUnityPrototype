using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]


public class ChunkRenderer : MonoBehaviour
{
    public const int ChunkWidth = 10;
    public const int ChunkHeight = 128;
    public int[,,] Blocks = new int[ChunkWidth, ChunkHeight, ChunkWidth];

    private List<Vector3> verticles = new List<Vector3>();
    private List<int> triangles = new List<int>();


    private void Start()
    {
        Mesh chunkMesh = new Mesh();

        Blocks = TerrainGenerator.GenerateTerrain();

        for (int y = 0; y < ChunkHeight; y++)
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkWidth; z++)
                {
                    GenerateBlock(x, y, z);
                }
            }
        }


        chunkMesh.vertices = verticles.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();



        GetComponent<MeshFilter>().mesh = chunkMesh;
    }



    private void GenerateBlock(int x, int y, int z)
    {
        Vector3Int blockPosition = new Vector3Int(x, y, z);

        if (GetBlockAtPosition(blockPosition) == 0) return;


        if (GetBlockAtPosition(blockPosition + Vector3Int.left) == 0) GenerateRightSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.right) == 0) GenerateLeftSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0) GenerateFrontSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.back) == 0) GenerateBackSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.up) == 0) GenerateTopSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.down) == 0) GenerateBottomSide(blockPosition);
    }


    private int GetBlockAtPosition(Vector3Int blockPosition)
    {
        if (blockPosition.x >= 0 && blockPosition.x < ChunkWidth &&
            blockPosition.y >= 0 && blockPosition.y < ChunkHeight &&
            blockPosition.z >= 0 && blockPosition.z < ChunkWidth)
        {
            return Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
        }
        else
        {
            return 0;
        }
    }


    void GenerateRightSide(Vector3Int blockPosition)
    {
        verticles.Add(new Vector3(0, 0, 0) + blockPosition);
        verticles.Add(new Vector3(0, 0, 1) + blockPosition);
        verticles.Add(new Vector3(0, 1, 0) + blockPosition);
        verticles.Add(new Vector3(0, 1, 1) + blockPosition);


        AddLastVerticlesSquare();

    }
    void GenerateLeftSide(Vector3Int blockPosition)
    {
        verticles.Add(new Vector3(1, 0, 0) + blockPosition);
        verticles.Add(new Vector3(1, 1, 0) + blockPosition);
        verticles.Add(new Vector3(1, 0, 1) + blockPosition);
        verticles.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticlesSquare();
    }
    void GenerateFrontSide(Vector3Int blockPosition)
    {

        verticles.Add(new Vector3(0, 0, 1) + blockPosition);
        verticles.Add(new Vector3(1, 0, 1) + blockPosition);
        verticles.Add(new Vector3(0, 1, 1) + blockPosition);
        verticles.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticlesSquare();
    }

    void GenerateBackSide(Vector3Int blockPosition)
    {
        verticles.Add(new Vector3(0, 0, 0) + blockPosition);
        verticles.Add(new Vector3(0, 1, 0) + blockPosition);
        verticles.Add(new Vector3(1, 0, 0) + blockPosition);
        verticles.Add(new Vector3(1, 1, 0) + blockPosition);

        AddLastVerticlesSquare();
    }

    void GenerateTopSide(Vector3Int blockPosition)
    {
        verticles.Add(new Vector3(0, 1, 0) + blockPosition);
        verticles.Add(new Vector3(0, 1, 1) + blockPosition);
        verticles.Add(new Vector3(1, 1, 0) + blockPosition);
        verticles.Add(new Vector3(1, 1, 1) + blockPosition);

        AddLastVerticlesSquare();
    }

    void GenerateBottomSide(Vector3Int blockPosition)
    {
        verticles.Add(new Vector3(0, 0, 0) + blockPosition);
        verticles.Add(new Vector3(1, 0, 0) + blockPosition);
        verticles.Add(new Vector3(0, 0, 1) + blockPosition);
        verticles.Add(new Vector3(1, 0, 1) + blockPosition);

        AddLastVerticlesSquare();
    }

    private void AddLastVerticlesSquare()
    {
        triangles.Add(verticles.Count - 4);
        triangles.Add(verticles.Count - 3);
        triangles.Add(verticles.Count - 2);

        triangles.Add(verticles.Count - 3);
        triangles.Add(verticles.Count - 1);
        triangles.Add(verticles.Count - 2);
    }
}
