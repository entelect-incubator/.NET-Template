cls 
$path = Read-Host -Prompt 'Input your solution folder'
cd $path 

$oldstring = Read-Host -Prompt 'Input your old project name'
$newstring = Read-Host -Prompt 'Input your new project name'

$files = Get-ChildItem -Path $path *.* -rec -file -exclude obj,debug,bin,*.msi,*.exe,*.dll,*.snk 

$loopCounter = 1 

foreach ($file in $files) 
{
    ##(-join [char[]](Get-Content $file.PSPath -Encoding Byte -TotalCount 2)) -eq 'ÿþ'
    Write-Host "FileName: $($file.Fullname)" 
    $fileContents = Get-Content $file.FullName 
    $fileContentsUpdated = $fileContents -replace $oldstring, $newstring 

    if ($filecontents -ne $null) 
    {
        #Write-Host "---BEFORE----" 
        #Write-Host $fileContents 
        #Write-Host "---AFTER----" 
        #Write-Host $fileContentsUpdated 

        if ($fileContents -ne $fileContentsUpdated) 
        {
            if ($fileContents[0..2] -eq 'ÿþ') 
            {
                Write-Host "Updating File $($file.Name) [Unicode] "
                Set-Content -path $file.FullName -value $fileContentsUpdated -Encoding Unicode 
            }
            else 
            {
                Write-Host "Updating File $($file.Name) [NOT-Unicode] "
                Set-Content -path $file.FullName -value $fileContentsUpdated ## not Unicode 
            }
        }

        $fileNameUpdate = $file.Name -replace $oldstring, $newstring 
        if ($file.Name -ne $fileNameUpdate) 
        {
           Write-Host "Rename $($file.FullName) to $fileNameUpdate" 
           Rename-Item -Path $file.FullName -NewName $fileNameUpdate 
        }

        $loopCounter = $loopCounter + 1 
        if ($loopCounter -gt 2) 
          {
             #exit 
          }

   }
}

// Repeat again, to rename folders 
$files = Get-ChildItem -Path $path *.* -rec -directory -exclude obj,debug,bin

foreach ($file in $files) 
{

        $fileNameUpdate = $file.Name -replace $oldstring, $newstring 
        if ($file.Name -ne $fileNameUpdate) 
        {
           Write-Host "Rename $($file.FullName) to $fileNameUpdate" 
           Rename-Item -Path $file.FullName -NewName $fileNameUpdate 
        }

}
