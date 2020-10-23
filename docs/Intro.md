## Introduction

The **Metadata** library can be used with or without dependency injection library, and it come with a default binding for Microsoft [IServiceCollection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/) and the first step is to register the default services

```csharp
public void ConfigureServices(IServiceCollection services)
{
   ...

   services.RegisterMetadataDefaultServices();

   ...
}
```

After adding the default services you need to configure the behavior of the library by using **IMetadataBuilder**.

```csharp
public void ConfigureServices(IServiceCollection services)
{
   ...

   var builder = services.RegisterDefaultMetadataBuilder();

   ...
}
```

A primary goal of **IMetadataBuilder** is to mark a type to be cached later and such behavior can be done using **IMetadataManager**, However **IMetadataBuilder** allow for other configurations which is not exist in the **IMetadataManager** and we are going to talk about them later.

```csharp
public class MyClass
{
   public string Name;

   public int Age { get; set; }
}

public class YourClass {}

public void ConfigureServices(IServiceCollection services)
{
   ...

   var builder = services.RegisterDefaultMetadataBuilder();
   builder
      .AddType<MyClass>()
      .AddType<YourClass>();
   ...
}
```

We added the classes `MyClass` and `YourClass` to the cache using the extension method `AddType`. You can add both classes at the same time using `AddTypes` method which allow adding up to 5 classes at a time.

```csharp
   builder.AddTypes<MyClass, YourClass>();
```

The next step is to inject the **IMetadataManager** into your class

```csharp
public class UserInformation
{
   public UserInformation(IMetadataManager metadataManager)
   {
   }
}
```

then you use the `GetMetadata` extension method to get the cached type information

```csharp
var typeInfo = metadataManager.GetMetadata<MyClass>();

Console.WriteLine("number of public members: {0}", typeInfo.Members.Count);
```

to set the value of a member

```csharp
var instance = new MyClass();

var memberInfo = typeInfo.GetProperty(nameof(MyClass.Age));

var writeContext = memberInfo.CreateWriteContext(12, instance);
memberInfo.SetValue(writeContext); 

```

to get the value of a member

```csharp
var readContext = memberInfo.CreateReadContext(instance);
var value = memberInfo.GetValue(readContext);
```
