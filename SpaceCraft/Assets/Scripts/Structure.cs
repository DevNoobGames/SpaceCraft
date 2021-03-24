using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Structure
{

    public static Queue<VoxelMod> GenerateMajorFlora(int index, Vector3 position, int minTrunkHeight, int maxTrunkHeight)
    {

        switch (index)
        {

            case 0:
                return MakeTree(position, minTrunkHeight, maxTrunkHeight);
            case 1:
                return MakeCacti(position, minTrunkHeight, maxTrunkHeight);
            case 2:
                return MakeSpaceHome(position, minTrunkHeight, maxTrunkHeight);
            case 3:
                return MakeSpaceCactus(position, minTrunkHeight, maxTrunkHeight);

        }

        return new Queue<VoxelMod>();

    }


    public static Queue<VoxelMod> MakeTree(Vector3 position, int minTrunkHeight, int maxTrunkHeight)
    {

        Queue<VoxelMod> queue = new Queue<VoxelMod>();

        int height = (int)(maxTrunkHeight * Noise.Get2DPerlin(new Vector2(position.x, position.z), 250f, 3f));

        if (height < minTrunkHeight)
            height = minTrunkHeight;

        for (int i = 1; i < height; i++)
            queue.Enqueue(new VoxelMod(new Vector3(position.x, position.y + i, position.z), 6)); //6 = wood

        for (int x = -3; x < 4; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                for (int z = -3; z < 4; z++)
                {
                    queue.Enqueue(new VoxelMod(new Vector3(position.x + x, position.y + height + y, position.z + z), 11)); //11 is leaves
                }
            }
        }

        return queue;

    }


    public static Queue<VoxelMod> MakeCacti(Vector3 position, int minTrunkHeight, int maxTrunkHeight)
    {

        Queue<VoxelMod> queue = new Queue<VoxelMod>();

        int height = (int)(maxTrunkHeight * Noise.Get2DPerlin(new Vector2(position.x, position.z), 23456f, 2f));

        if (height < minTrunkHeight)
            height = minTrunkHeight;

        for (int i = 1; i <= height; i++)
            queue.Enqueue(new VoxelMod(new Vector3(position.x, position.y + i, position.z), 12)); //12 is cacti

        return queue;
    }

    public static Queue<VoxelMod> MakeSpaceCactus(Vector3 position, int minTrunkHeight, int maxTrunkHeight)
    {
        Queue<VoxelMod> queue = new Queue<VoxelMod>();

        int height = (int)(maxTrunkHeight * Noise.Get2DPerlin(new Vector2(position.x, position.z), 23456f, 2f));

        if (height < minTrunkHeight)
            height = minTrunkHeight;

        for (int i = 1; i <= height; i++)
            queue.Enqueue(new VoxelMod(new Vector3(position.x, position.y + i, position.z), 17)); //16 is space cact

        return queue;
    }

    public static Queue<VoxelMod> MakeSpaceHome(Vector3 position, int minTrunkHeight, int maxTrunkHeight)
    {

        Queue<VoxelMod> queue = new Queue<VoxelMod>();
 
            int height = 5;
            for (int i = 1; i <= height; i++) //5 is the height here
            {
                //BUILD ALL SIDES
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 1), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 2), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 3), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 4), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 5), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 6), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 7), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 8), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 9), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 0, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 7), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 7), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 3), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 3), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 7), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 8), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 9), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 10), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 3), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 7), 16));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 0), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 3), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 4), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 5), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 6), 16));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 7), 16));

                //AIRBLOCKS FOR IN BETWEEN
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 3), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 6), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 8), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 1, position.y + i, position.z + 9), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 3), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 6), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 8), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 2, position.y + i, position.z + 9), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 3), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 6), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 7), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 8), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 3, position.y + i, position.z + 9), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 6), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 7), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 8), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 4, position.y + i, position.z + 9), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 5, position.y + i, position.z + 6), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 2), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 4), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 5), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 6, position.y + i, position.z + 6), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 7, position.y + i, position.z + 2), 0));

                //Airblocks in front door
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 8, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 8, position.y + i, position.z + 2), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 9, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 9, position.y + i, position.z + 2), 0));

                queue.Enqueue(new VoxelMod(new Vector3(position.x + 10, position.y + i, position.z + 1), 0));
                queue.Enqueue(new VoxelMod(new Vector3(position.x + 10, position.y + i, position.z + 2), 0));
            }

            int blocksOnFloorX = 7;
            int blocksOnFloorz = 10;

            for (int a = 0; a <= blocksOnFloorX; a++)
            {
                for (int b = 0; b <= blocksOnFloorz; b++)
                {
                    //Build roof
                    queue.Enqueue(new VoxelMod(new Vector3(position.x + a, position.y + 4, position.z + b), 16));
                    //BUILD BOTTOM
                    queue.Enqueue(new VoxelMod(new Vector3(position.x + a, position.y - 1, position.z + b), 15));
                    queue.Enqueue(new VoxelMod(new Vector3(position.x + a, position.y - 2, position.z + b), 15));
                    queue.Enqueue(new VoxelMod(new Vector3(position.x + a, position.y - 3, position.z + b), 15));
                }
            }

            //Build floor
            int pos = 0;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 1;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 2;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 3;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 4;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 5;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 8), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 9), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 10), 16));
            pos = 6;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));
            pos = 7;
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 0), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 1), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 2), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 3), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 4), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 5), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 6), 16));
            queue.Enqueue(new VoxelMod(new Vector3(position.x + pos, position.y, position.z + 7), 16));

            return queue;
        
    }

    
}

