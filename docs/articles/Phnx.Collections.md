# Phnx.Benchmark

This library contains tools to help you manage collections, including extensions for Linq, and new Tree types to help with more complex collection data structures

# Quick Examples

For a full list of all things possible with Phnx.Collections, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Collections.html)

## Linq Extensions

#### DistinctBy
```cs
List<string> examples = new List<string>
{
    "Alphabet Soup",
    "Banana",
    "Cherry",
    "Cake"
};

// Distinct by first letter, so only strings with a unique first letter are preserved
IEnumerable<string> examplesDistinctByFirstLetter = examples.DistinctBy(s => s[0]);

// Cake is omitted, as it's not unique by first letter - Cherry is already present with a first letter of C

/* Output:
Alphabet Soup
Banana
Cherry
*/
foreach(string example in examplesDistinctByFirstLetter)
{
    Console.WriteLine(example);
}
```

#### MaxBy
```cs
List<string> examples = new List<string>
{
    "Alphabet Soup",
    "Banana",
    "Cherry",
    "Cake"
};

// Gets the longest word, by getting the word with the maximum Length
string longestWord = examples.MaxBy(s => s.Length);

// Output: Alphabet Soup
Console.WriteLine(longestWord);
```

## Tree
```cs
Tree<string> tree = new Tree<string>();

TreeNode<string> johnNode = tree.AddTopNode("John");
TreeNode<string> danielNode = johnNode.AddChild("Daniel");
danielNode.AddChild("Rebecca");
danielNode.AddChild("Stephan");

TreeNode<string> sarahNode = tree.AddTopNode("Sarah");
TreeNode<string> steveNode = sarahNode.AddChild("Steve");
steveNode.AddChild("Brian");
steveNode.AddChild("Patrick");

/*
Illustrated structure:
        John
        /   \
    Daniel  Sarah
    /    \       \
Rebecca Stephan Steve
                /   \
            Brian   Patrick
*/

/* Output:
John
Sarah
Daniel
Steve
Rebecca
Stephan
Brian
Patrick
*/
foreach(string entry in tree.TraverseBreadthFirst())
{
    Console.WriteLine(entry);
}

/* Output:
John
Daniel
Rebecca
Stephan
Sarah
Steve
Brian
Patrick
*/
foreach(string entry in tree.TraverseDepthFirst())
{
    Console.WriteLine(entry);
}
```