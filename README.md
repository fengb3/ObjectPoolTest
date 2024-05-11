## 基于代码生成的对象池Demo

这个项目是一个基于代码生成的对象池示例。主要包含两个部分：`ObjectPoolTest` 和 `SourceGenerators1`

`ObjectPoolTest` 是主项目，它是一个.NET 8.0 控制台应用程序，项目文件在 `ObjectPoolTest.csproj`。主要的执行代码在 Program.cs 文件中。

`SourceGenerators1` 是一个源代码生成器项目，它包含一个名为 `RecycleObjectSourceGenerator` 的源代码生成器。这个生成器的目的是为了生成对象池相关的代码。

将`SourceGenerators1`作为代码分析项目引用至 `ObjectPoolTest` 

```xml
<ItemGroup>
    <ProjectReference Include="..\SourceGenerators1\SourceGenerators1\SourceGenerators1.csproj" OutputItemType="Analyzer" />
</ItemGroup>
```

这个项目的目标是展示如何使用源代码生成器来自动化生成对象池相关的代码，从而减少手动编写和维护这部分代码的工作量。