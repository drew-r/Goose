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
            
            vm.DoString("m = MotherGoose()");
            
            Console.WriteLine(vm.DoString("return m:WhatsUpMotherGoose()")[0]);
            
            dynamic motherGoose = vm.DoString("return m")[0];
            string whatsUp = motherGoose.WhatsUpMotherGoose();
            Console.WriteLine(whatsUp);

            LuaFunction func = vm.DoString("return function(whatsUp) Console.WriteLine(whatsUp) end")[0] as LuaFunction;
            func.Call(whatsUp);
```

