sc delete CurrencyService.BackgroundService 
sc create CurrencyService.BackgroundService binPath="%~dp0\CurrencyService.BackgroundService.exe"
sc start CurrencyService.BackgroundService 