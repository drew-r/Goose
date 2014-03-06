Goose
=====
- Provides an integrated Lua scripting environment to .NET applications via NLua (http://nlua.org). 
- Facilitates runtime C# code compilation into this environment for scripting. 
- Free and open source (MIT license).


Like this :)
---

```csharp
            VM vm = new VM();
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
            vm.DoString("MotherGoose()");
```
