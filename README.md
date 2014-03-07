Goose
=====
- Provides an integrated Lua scripting environment to .NET applications via NLua (http://nlua.org). 
- Facilitates runtime C# code compilation into this environment for scripting. 
- Free and open source (MIT license).


Getting started...
---
Create a Goose VM, then use it to compile a C# class from source and create an instance.
```csharp

	//Spin up a VM
    VM vm = new VM();

    //Compile some C# classes
    vm.Compile("GooseFeatures", new string[]{
    @"
    using System;
    namespace GooseInYourFaceWhatchaGonnaDo 
    {
            public class MotherGoose 
            { 
                public MotherGoose()
                {
                    Console.WriteLine(""Moo!"");
                }
            }    
    }" 
                });
    
    //Create an instance of our new class with Lua
    vm.DoString("m = MotherGoose()");

```

Invoke a method on the instance and obtain the result.

```csharp
    Console.WriteLine(vm.DoString("return m:WhatsUpMotherGoose()")[0]);
```

.NET 4's _dynamic_ type means you can pull a scripted object straight into the CLR and start working with it.

```csharp
    dynamic motherGoose = vm.DoString("return m")[0];
    string whatsUp = motherGoose.WhatsUpMotherGoose();
    Console.WriteLine(whatsUp);
```

Obtain a reference to a Lua function and invoke directly from the CLR

```csharp
    LuaFunction func = vm.DoString("return function(whatsUp) Console.WriteLine(whatsUp) end")[0] 
                as LuaFunction;                
    func.Call(whatsUp);
```

