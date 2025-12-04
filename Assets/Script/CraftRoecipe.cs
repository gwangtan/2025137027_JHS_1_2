using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recipe", menuName = "조합법 생성")]

public class CraftingRecipe : ScriptableObject
{

    [Serializable] public struct Ingredient
    {
        public ItemType type;
        public int count;
    }

    
   [Serializable] public struct Product
    {
        public ItemType type;
        public int count;
    }

    public string displayName;
    public List<Ingredient> inputs = new();
    public List<Product> outputs = new();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
