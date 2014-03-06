
function arr(t,...)
arg = table.pack(...)
if arg.n>0 then arg.n = nil table.insert(arg,1,t) t = arg 
elseif type(t) ~= "table" and type(t) ~= "userdata" then t = {t}
end
return Utility.ObjArrayFromTable(t)
end

function obj(t)
return Utility.ObjFromTable(t)
end

function str_arr(t,...)
arg = table.pack(...)
if arg.n>0 then arg.n = nil table.insert(arg,1,t) t = arg 
elseif type(t) ~= "table" and type(t) ~= "userdata" then t = {t}
end
return Utility.StringArrayFromTable(t)
end
