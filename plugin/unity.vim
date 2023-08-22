if exists("g:loaded_unity_vim")
    finish
endif
let g:loaded_unity_vim = 1
let s:has_buffer = 0

if !exists(":UnityVimJumpErrorLine")
    command UnityVimJumpErrorLine :call s:unityvim_jump()
endif

function! s:unityvim_jump()
"    if !s:has_buffer
"        vnew
"        setlocal buftype=nofile
"        setlocal bufhidden=hide
"        setlocal noswapfile
"        file [Unity]
"        let s:has_buffer = 1
"    endif
"    buffer \[Unity]
    let s:test = systemlist("dotnet run")
    echo s:test[-2]
    execute "e " . s:test[-2]
    execute ":" . s:test[-1]
"    r !dotnet run
endfunction



