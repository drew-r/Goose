using NLua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goose
{
    public static class Util
    {
        public static object[] ObjArrayFromTable(LuaTable t)
        {
            object[] r = new object[t.Values.Count];
            int i = 0;
            foreach (object value in t.Values)
            {
                if (value is LuaTable) { r[i] = ObjFromTable((LuaTable)value); }
                else
                {
                    r[i] = value;
                }
                i++;
            }
            return r;
        }

        public static IEnumerable<string> StringArrayFromTable(LuaTable t)
        {
            return ObjArrayFromTable(t).Cast<string>().ToArray();
        }

        public static object ObjFromTable(LuaTable t)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();

            foreach (DictionaryEntry item in t)
            {
                object obj;
                if (item.Key is double) { return ObjArrayFromTable(t); }
                if (item.Value is LuaTable) { obj = ObjFromTable((LuaTable)item.Value); } else { obj = item.Value; }
                args.Add(item.Key.ToString(), obj);
            }
            return args;
        }
    }
}
