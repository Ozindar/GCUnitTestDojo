start for /d /r . %%d in (bin,obj, ClientBin,Generated_Code, packages) do @if exist "%%d" rd /s /q "%%d"