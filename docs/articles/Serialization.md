# Phnx.Serialization

This library contains tools to help serialize and de-serialize data, using high speed compression

# Quick Examples

For a full list of all things possible with Phnx.Serialization, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Serialization.html)

## Object extensions

Creating a deep copy of an object involved duplicating all properties and child properties of an object.
The end result is that any member, and its child members can be changed in the copy (or the original) without affecting the other

Creating a shallow copy only clones the first level. Any child properties of its members will not be cloned, and so changes to them will affect both the copy and the original.

```cs
using Phnx.Serialization;

object myObject = new
{
    a = new
    {
        a = "1",
        b = "2"
    },
    b = "c",
    c = "2"
};

object copy = myObject.DeepCopy();

// copy and myObject are now deep clones of each other

object copy = myObject.ShallowCopy();

// copy and myObject are now shallow clones of each other
```