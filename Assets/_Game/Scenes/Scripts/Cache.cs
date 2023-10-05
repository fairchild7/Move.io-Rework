using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Cache
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            characters.Add(collider, character);
        }
        return characters[collider];
    }

    private static Dictionary<Collider, Bullet> bullets = new Dictionary<Collider, Bullet>();

    public static Bullet GetBullet(Collider collider)
    {
        if (!bullets.ContainsKey(collider))
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            bullets.Add(collider, bullet);
        }
        return bullets[collider];
    }

    private static Dictionary<Collider, Renderer> wallRenders = new Dictionary<Collider, Renderer>();

    public static Renderer GetWallRenderer(Collider collider)
    {
        if (!wallRenders.ContainsKey(collider))
        {
            Renderer render = collider.GetComponent<Renderer>();
            wallRenders.Add(collider, render);
        }
        return wallRenders[collider];
    }
}
