Goose
=====
- Provides an integrated Lua scripting environment to .NET applications via NLua (http://nlua.org). 
- Facilitates runtime C# code compilation into this environment for scripting. 
- Free and open source (MIT license).
- See http://drew-r.github.io/Goose/

Getting started...
---
Create a Goose VM, then use it to compile a C# class from source and create an instance.
```csharp

//Spin up a VM
VM vm = new VM();

//Compile some C# classes
vm.Compile("MyAwesomeAssembly", new string[]{
@"
using System;
namespace MyAwesomeNamespace 
{
    public class Pigeon
    { 
        public Pigeon()
        {
            Console.WriteLine(""Moo!"");
        }
        public string Wassup()
        {
            return "I am the pigeon to end all pigeons.";
        }
    }    
}" 
        });

//Create an instance of our new class with Lua and assign it to a Lua global
vm.DoString("p = Pigeon()");
```

Invoke a method on the instance and obtain the result.

```csharp
Console.WriteLine(vm.DoString("return p:Wassup()")[0]);
```

.NET 4's _dynamic_ type means you can pull a scripted object straight into the CLR and start working with it.

```csharp
dynamic pigeon = vm.DoString("return p")[0];
string whatsUp = pigeon.Wassup();
Console.WriteLine(whatsUp);
```

Obtain a reference to a Lua function and invoke directly from the CLR

```csharp
LuaFunction func = vm.DoString("return function(whatsUp) Console.WriteLine(whatsUp) end")[0] 
        as LuaFunction;                
func.Call(whatsUp);
```

License
---

The MIT License (MIT)

Copyright (c) 2014 Drew R

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
