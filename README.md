# VSH Patcher
[![License](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](https://github.com/iMCSx/VSHPatcher/blob/master/LICENSE)

This will patch any `vsh.self` to allow the PSN connection !

# How to use

- Extract VSH Patcher + the Tools folder somewhere.
- Run a FTP server on ps3 (example multiman / rebug toolbox) and go here `dev_flash/vsh/module/vsh.self`
- Copy the `vsh.self` somewhere on your computer. (For any problems, take it via `dev_blind/dev_rebug`)
- Run the VSH Patcher, select your `vsh.self` in the application and click on the button `Patch`
- If it's working a message will be displayed with the location of the new file called `vsh.self`, the `vsh_patched.elf` is the format non-crypted patched.
- Now you need to write into the flash dir but you can't, you need to mount it, with multiman (find an option to mount flash as `dev_blind`) or via rebug toolbox (`dev_rebug`) and you don't see these folders, on FileZilla press F5 that refresh everything.
- Go here `dev_blind/vsh/module/` (or `dev_rebug/vsh/module/`) and move the `vsh.self` patched by the program in the list, FileZilla will ask you to replace it, select `Yes`!
- Once replaced, restart your console, and connect on the PSN.

Available on [NextGenUpdate.com](http://www.nextgenupdate.com/forums/ps3-cheats-customization/802765-release-imcsxs-vsh-patcher-psn-back-any-cfw-cex-dex.html) & [RealityGaming.fr](http://realitygaming.fr/threads/release-imcsxs-vsh-patcher-psn-is-back.414414/) (French)

# How to build

- Extract the `src` folder and use the `.csproj` file

# Credits

- The PS3ITA Team
- Naehrwert
- HeAd
