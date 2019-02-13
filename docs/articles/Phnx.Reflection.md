# Phnx.Reflection

This library contains tools to help reflecting on types, with easy access to public fields and properties

## Quick Examples

For a full list of all things possible with Phnx.Reflection, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Reflection.html)

#### Sample class to reflect on
```cs
public class SampleClass
{
    [DisplayName("Sample Display Name")]
    public DateTime PublicProperty { get; set; }
}
```

## MemberInfo Extensions

#### Get Attribute

```cs
Type sampleClassType = typeof(SampleClass);

MemberInfo publicPropertyMember = sampleClassType.GetProperty(nameof(SampleClass.PublicProperty));

DisplayNameAttribute attribute = publicPropertyMember.GetAttribute<DisplayNameAttribute>();

// Output: Sample Display Name
Console.WriteLine(attribute.DisplayName);
```

#### Get Display Name
```cs
Type sampleClassType = typeof(SampleClass);

MemberInfo publicPropertyMember = sampleClassType.GetProperty(nameof(SampleClass.PublicProperty));

// Output: Sample Display Name
Console.WriteLine(publicPropertyMember.GetDisplayName());
```

## PropertyFieldInfo&lt;T, U&gt;

```cs
PropertyFieldInfo<SampleClass, DateTime> property = new PropertyFieldInfo<SampleClass, DateTime>(s => s.PublicProperty);

// Output: False
Console.WriteLine(property.IsAutoProperty);

// Output: False
Console.WriteLine(property.IsProperty);

SampleClass sampleInstance = new SampleClass();

property.SetValue(sampleInstance, new DateTime(2000, 1, 1));

// Output: 2000-01-01
Console.WriteLine(sampleInstance.PublicProperty.ToString("yyyy-MM-dd"));

DateTime dateValue = property.GetValue(sampleInstance);

// Output: 2000-01-01
Console.WriteLine(dateValue.ToString("yyyy-MM-dd"));
```