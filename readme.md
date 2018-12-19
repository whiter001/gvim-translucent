# gvim-translucent
Transparency settings for GVIM.exe  
# Usage
Put trans.exe in environmental variables

Run in Gvm.exe `:!trans 158` or `:!trans 255`

Add the following code to _vimrc

```vimfiles
nmap <F12> :call ToggleGvimTrans()<cr>
let g:gvim_trans = 0
function! ToggleGvimTrans()
    if g:gvim_trans == 0
        let g:gvim_trans = 1 
        ":!trans 158
        :AsyncRun trans 158 " Skywind3000/asyncrun.vim needs to be installed
    else
        let g:gvim_trans = 0
        ":!trans 255
        :AsyncRun trans 255
    endif
endfunction
```
